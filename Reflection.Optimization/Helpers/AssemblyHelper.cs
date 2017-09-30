using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.Optimization.Helpers {
    public class AssemblyHelper {
        private static Dictionary<String, IReadOnlyList<RuntimeLibrary>> runtimeLibrary = new Dictionary<string, IReadOnlyList<RuntimeLibrary>>();
        private static Dictionary<String, Assembly[]> assemblies = new Dictionary<string, Assembly[]>();
        private static Dictionary<Predicate<Type>, IEnumerable<Type>> types = new Dictionary<Predicate<Type>, IEnumerable<Type>>();

        public static Assembly[] GetAssemblies() {
            if (assemblies.ContainsKey("loaded")) {
                return assemblies["loaded"];
            } else {
                Init();
                var list = new List<Assembly>();
                foreach (var library in runtimeLibrary["loaded"]) {
                    try {
                        var assembly = Assembly.Load(new AssemblyName(library.Name));
                        list.Add(assembly);
                    } catch (Exception) { }
                }
                var assembliesArray = list.ToArray();
                assemblies["loaded"] = assembliesArray;
                return assembliesArray;
            }
        }

        public static IEnumerable<Type> GetTypes(Predicate<Type> predicate) {
            foreach (var assembly in GetAssemblies()) {
                var types = assembly.GetTypes();
                foreach (var type in types) {
                    if (predicate(type)) {
                        yield return type;
                    }
                }
            }
        }

        public static IEnumerable<Type> GetTypes(Predicate<Type> predicate, Assembly assm, bool first = false) {
            if (types.ContainsKey(predicate)) {
                return types[predicate];
            }
            List<Type> listTypes = new List<Type>();
            foreach (var type in GetTypes(assm)) {
                if (predicate(type)) {
                    listTypes.Add(type);
                    if (first) {
                        break;
                    }
                }
            }
            if (listTypes.Count() == 0) {
                var stop = false;
                foreach (var assembly in GetAssemblies()) {
                    if (assembly != assm) {
                        try {
                            foreach (var type in GetTypes(assembly)) {
                                if (predicate(type)) {
                                    listTypes.Add(type);
                                    if (first) {
                                        stop = true;
                                        break;
                                    }
                                }
                            }
                        } catch (Exception) { }
                    }
                    if (stop) {
                        break;
                    }
                }
            }
            types[predicate] = listTypes;
            return listTypes;
        }

        public static Type GetType(Assembly assembly, string typeName) {
            Type myType = null;
            foreach (var type in GetTypes(assembly)) {
                if (type.Name.ToUpper() == typeName.ToUpper()) {
                    myType = type;
                    break;
                }
            }
            return myType;
        }

        public static IEnumerable<Type> GetTypes(Assembly assembly) {
            try {
                var types = assembly.GetTypes();
                foreach (var type in types) {
                    yield return type;
                }
            } finally { }
        }

        private static void Init() {
            if (!runtimeLibrary.ContainsKey("loaded")) {
                runtimeLibrary["loaded"] = DependencyContext.Default.RuntimeLibraries;
            }
        }
    }
}
