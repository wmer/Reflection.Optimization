using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.Optimization.Helpers {
    public class AssemblyHelper {
        private static Assembly[] assembliesLoaded = AppDomain.CurrentDomain.GetAssemblies();
        private static Dictionary<String, Assembly[]> assemblies = new Dictionary<string, Assembly[]>();
        private static Dictionary<Predicate<Type>, IEnumerable<Type>> types = new Dictionary<Predicate<Type>, IEnumerable<Type>>();
        
        private static object lock1 = new object();
        private static object lock2 = new object();
        private static object lock3 = new object();
        private static object lock4 = new object();
        private static object lock5 = new object();
        private static object lock6 = new object();

        public static Assembly[] GetAssemblies() {
            lock (lock1) {
                if (assemblies.ContainsKey("loaded")) {
                    return assemblies["loaded"];
                }
                assemblies["loaded"] = assembliesLoaded;
                return assembliesLoaded;
            }
        }

        public static IEnumerable<Type> GetTypes(Predicate<Type> predicate) {
            lock (lock2) {
                foreach (var assembly in GetAssemblies()) {
                    var types = assembly.GetTypes();
                    foreach (var type in types) {
                        if (predicate(type)) {
                            yield return type;
                        }
                    }
                }
            }
        }

        public static IEnumerable<Type> GetTypes(Predicate<Type> predicate, Assembly assm, bool first = false) {
            lock (lock3) {
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
        }

        public static Type GetType(Assembly assembly, string typeName) {
            lock (lock4) {
                Type myType = null;
                foreach (var type in GetTypes(assembly)) {
                    if (type.Name.ToUpper() == typeName.ToUpper()) {
                        myType = type;
                        break;
                    }
                }
                return myType;
            }
        }

        public static IEnumerable<Type> GetTypes(Assembly assembly) {
            lock (lock5) {
                try {
                    var types = assembly.GetTypes();
                    foreach (var type in types) {
                        yield return type;
                    }
                } finally { }
            }
        }
    }
}
