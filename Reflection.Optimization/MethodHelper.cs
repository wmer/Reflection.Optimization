using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq.Expressions;

namespace Reflection.Optimization {
    public delegate object ObjectActivator(params object[] args);
    public class MethodHelper {

        private object lock1 = new object();
        private object lock2 = new object();
        private object lock3 = new object();
        private object lock4 = new object();
        private object lock5 = new object();
        private object lock6 = new object();
        private object lock7 = new object();
        private object lock8 = new object();
        private object lock9 = new object();
        private object lock10 = new object();

        //Methodos por tipos
        public Delegate CreateMethod(Type delegateType, Type[] delegateArguments, Type type, String methodName) =>
            CreateMethodWithName(delegateType.MakeGenericType(delegateArguments), type, methodName, null);

        public Delegate CreateMethod(Type delegateType, Type[] delegateArguments, Type type, String methodName, BindingFlags bindingFlags) =>
            CreateMethodWithBindingFlags(delegateType.MakeGenericType(delegateArguments), type, methodName, null, bindingFlags);

        public Delegate CreateMethod(Type delegateType, Type[] delegateArguments, Type type, String methodName, Type[] paramsType) =>
            CreateMethodWitparamsType(delegateType.MakeGenericType(delegateArguments), type, methodName, null, paramsType);

        public Delegate CreateMethod<T>(Type type, String methodName) =>
            CreateMethodWithName(typeof(T), type, methodName, null);

        public Delegate CreateMethod<T>(Type type, String methodName, BindingFlags bindingFlags) =>
            CreateMethodWithBindingFlags(typeof(T), type, methodName, null, bindingFlags);

        public Delegate CreateMethod<T>(Type type, String methodName, Type[] paramsType) =>
            CreateMethodWitparamsType(typeof(T), type, methodName, null, paramsType);

        //Methodos por Instâncias
        public Delegate CreateMethod(Type delegateType, Type[] delegateArguments, object instanceClass, String methodName) =>
            CreateMethodWithName(delegateType.MakeGenericType(delegateArguments), instanceClass, methodName, null);

        public Delegate CreateMethod(Type delegateType, Type[] delegateArguments, object instanceClass, String methodName, BindingFlags bindingFlags) =>
            CreateMethodWithBindingFlags(delegateType.MakeGenericType(delegateArguments), instanceClass, methodName, null, bindingFlags);

        public Delegate CreateMethod(Type delegateType, Type[] delegateArguments, object instanceClass, String methodName, Type[] paramsType) =>
            CreateMethodWitparamsType(delegateType.MakeGenericType(delegateArguments), instanceClass, methodName, null, paramsType);

        public Delegate CreateMethod<T>(object instanceClass, String methodName) =>
            CreateMethodWithName(typeof(T), instanceClass, methodName, null);

        public Delegate CreateMethod<T>(object instanceClass, String methodName, BindingFlags bindingFlags) =>
            CreateMethodWithBindingFlags(typeof(T), instanceClass, methodName, null, bindingFlags);

        public Delegate CreateMethod<T>(object instanceClass, String methodName, Type[] paramsType) =>
            CreateMethodWitparamsType(typeof(T), instanceClass, methodName, null, paramsType);

        //Metodos genéricos por tipo
        public Delegate CreateGenericMethod(Type delegateType, Type[] delegateArguments, Type type, String methodName, Type[] methodTypeArguments) =>
            CreateMethodWithName(delegateType.MakeGenericType(delegateArguments), type, methodName, methodTypeArguments);

        public Delegate CreateGenericMethod(Type delegateType, Type[] delegateArguments, Type type, String methodName, Type[] methodTypeArguments, BindingFlags bindingFlags) =>
            CreateMethodWithBindingFlags(delegateType.MakeGenericType(delegateArguments), type, methodName, methodTypeArguments, bindingFlags);

        public Delegate CreateGenericMethod(Type delegateType, Type[] delegateArguments, Type type, String methodName, Type[] methodTypeArguments, Type[] paramsType) =>
            CreateMethodWitparamsType(delegateType.MakeGenericType(delegateArguments), type, methodName, methodTypeArguments, paramsType);

        public Delegate CreateGenericMethod<T>(Type type, String methodName, Type[] methodTypeArguments) =>
            CreateMethodWithName(typeof(T), type, methodName, methodTypeArguments);

        public Delegate CreateGenericMethod<T>(Type type, String methodName, Type[] methodTypeArguments, BindingFlags bindingFlags) =>
            CreateMethodWithBindingFlags(typeof(T), type, methodName, methodTypeArguments, bindingFlags);

        public Delegate CreateGenericMethod<T>(Type type, String methodName, Type[] methodTypeArguments, Type[] paramsType) =>
            CreateMethodWitparamsType(typeof(T), type, methodName, methodTypeArguments, paramsType);

        //Methodos Genéricos por Instâncias
        public Delegate CreateGenericMethod(Type delegateType, Type[] delegateArguments, object instanceClass, String methodName, Type[] methodTypeArguments) =>
            CreateMethodWithName(delegateType.MakeGenericType(delegateArguments), instanceClass, methodName, methodTypeArguments);

        public Delegate CreateGenericMethod(Type delegateType, Type[] delegateArguments, object instanceClass, String methodName, Type[] methodTypeArguments, BindingFlags bindingFlags) =>
            CreateMethodWithBindingFlags(delegateType.MakeGenericType(delegateArguments), instanceClass, methodName, methodTypeArguments, bindingFlags);

        public Delegate CreateGenericMethod(Type delegateType, Type[] delegateArguments, object instanceClass, String methodName, Type[] methodTypeArguments, Type[] paramsType) =>
            CreateMethodWitparamsType(delegateType.MakeGenericType(delegateArguments), instanceClass, methodName, methodTypeArguments, paramsType);

        public Delegate CreateGenericMethod<T>(object instanceClass, String methodName, Type[] methodTypeArguments) =>
           CreateMethodWithName(typeof(T), instanceClass, methodName, methodTypeArguments);

        public Delegate CreateGenericMethod<T>(object instanceClass, String methodName, Type[] methodTypeArguments, BindingFlags bindingFlags) =>
            CreateMethodWithBindingFlags(typeof(T), instanceClass, methodName, methodTypeArguments, bindingFlags);

        public Delegate CreateGenericMethod<T>(object instanceClass, String methodName, Type[] methodTypeArguments, Type[] paramsType) =>
            CreateMethodWitparamsType(typeof(T), instanceClass, methodName, methodTypeArguments, paramsType);

        //Com tipo e MethodInfo
        public Delegate CreateMethod(Type delegateType, Type[] delegateArguments, MethodInfo method) {
            lock (lock7) {
                var dele = delegateType.MakeGenericType(delegateArguments);
                return method.CreateDelegate(dele);
            }
        }

        //Methodos por Instâncias e MethodInfo
        public Delegate CreateMethod(Type delegateType, Type[] delegateArguments, object instanceClass, MethodInfo method) {
            lock (lock8) {
                var dele = delegateType.MakeGenericType(delegateArguments);
                return method.CreateDelegate(dele, instanceClass);
            }
        }

        public Delegate CretatePropertySetterMethod(PropertyInfo propertyInfo, object objectInstance) {
            lock (lock9) {
                var method = propertyInfo.GetSetMethod();
                var parameterType = method.GetParameters().First().ParameterType;
                return CreateMethod(typeof(Action<>), new Type[] { parameterType }, objectInstance, method);
            }
        }

        public Delegate CreatePropertyGetterMethod(PropertyInfo propertyInfo, object objectInstance) {
            lock (lock10) {
                var getMethod = propertyInfo.GetGetMethod();
                return CreateMethod(typeof(Func<>), new Type[] { getMethod.ReturnType }, objectInstance, getMethod);
            }
        }

        //metodo por tipo
        private Delegate CreateMethodWithName(Type delegateType, Type type, String methodName, Type[] methodTypeArguments) {
            lock (lock1) {
                var method = type.GetMethod(methodName);
                if (methodTypeArguments != null) {
                    method.MakeGenericMethod(methodTypeArguments);
                }
                return method.CreateDelegate(delegateType);
            }
        }

        private Delegate CreateMethodWithBindingFlags(Type delegateType, Type type, String methodName, Type[] methodTypeArguments, BindingFlags bindingFlags) {
            lock (lock2) {
                var method = type.GetMethod(methodName, bindingFlags);
                if (methodTypeArguments != null) {
                    method.MakeGenericMethod(methodTypeArguments);
                }
                return method.CreateDelegate(delegateType);
            }
        }

        private Delegate CreateMethodWitparamsType(Type delegateType, Type type, String methodName, Type[] methodTypeArguments, Type[] paramsType) {
            lock (lock3) {
                var method = type.GetMethod(methodName, paramsType);
                if (methodTypeArguments != null) {
                    method.MakeGenericMethod(methodTypeArguments);
                }
                return method.CreateDelegate(delegateType);
            }
        }

        //metodo por instância
        private Delegate CreateMethodWithName(Type delegateType, object instanceClass, String methodName, Type[] methodTypeArguments) {
            lock (lock4) {
                var method = instanceClass.GetType().GetMethod(methodName);
                if (methodTypeArguments != null) {
                    method.MakeGenericMethod(methodTypeArguments);
                }
                return method.CreateDelegate(delegateType, instanceClass);
            }
        }

        private Delegate CreateMethodWithBindingFlags(Type delegateType, object instanceClass, String methodName, Type[] methodTypeArguments, BindingFlags bindingFlags) {
            lock (lock5) {
                var method = instanceClass.GetType().GetMethod(methodName, bindingFlags);
                if (methodTypeArguments != null) {
                    method.MakeGenericMethod(methodTypeArguments);
                }
                return method.CreateDelegate(delegateType, instanceClass);
            }
        }

        private Delegate CreateMethodWitparamsType(Type delegateType, object instanceClass, String methodName, Type[] methodTypeArguments, Type[] paramsType) {
            lock (lock6) {
                var method = instanceClass.GetType().GetMethod(methodName, paramsType);
                if (methodTypeArguments != null) {
                    method.MakeGenericMethod(methodTypeArguments);
                }
                return method.CreateDelegate(delegateType, instanceClass);
            }
        }
    }
}
