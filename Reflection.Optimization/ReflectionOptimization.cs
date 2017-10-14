using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq.Expressions;

namespace Reflection.Optimization {
    public delegate object ObjectActivator(params object[] args);
    public class ReflectionOptimizations {
        //Methodos por tipos
        //Genericos
        public U CreateMethod<U>(Type type, String methodName) {
            lock (new object()) {
                var method = type.GetMethod(methodName);
                var del = method.CreateDelegate(typeof(U)) as object;
                return (U)del;
            }
        }

        public U CreateMethod<U>(Type type, String methodName, BindingFlags bindingFlags) {
            lock (new object()) {
                var method = type.GetMethod(methodName, bindingFlags);
                var del = method.CreateDelegate(typeof(U)) as object;
                return (U)del;
            }
        }

        public U CreateMethod<U>(Type type, String methodName, Type[] paramsType) {
            lock (new object()) {
                var method = type.GetMethod(methodName, paramsType);
                var del = method.CreateDelegate(typeof(U)) as object;
                return (U)del;
            }
        }

        //Não genéricos
        public Delegate CreateMethod(Type delegateType, Type[] delegateArguments, Type type, String methodName) {
            lock (new object()) {
                var dele = delegateType.MakeGenericType(delegateArguments);
                var method = type.GetMethod(methodName);
                return method.CreateDelegate(dele);
            }
        }

        public Delegate CreateMethod(Type delegateType, Type[] delegateArguments, Type type, String methodName, BindingFlags bindingFlags) {
            lock (new object()) {
                var dele = delegateType.MakeGenericType(delegateArguments);
                var method = type.GetMethod(methodName, bindingFlags);
                return method.CreateDelegate(dele);
            }
        }

        public Delegate CreateMethod(Type delegateType, Type[] delegateArguments, Type type, String methodName, Type[] paramsType) {
            lock (new object()) {
                var dele = delegateType.MakeGenericType(delegateArguments);
                var method = type.GetMethod(methodName, paramsType);
                return method.CreateDelegate(dele);
            }
        }

        //Methodos por Instâncias
        //Genericos
        public U CreateMethod<U>(object instanceClass, String methodName) {
            lock (new object()) {
                var method = instanceClass.GetType().GetMethod(methodName);
                var del = method.CreateDelegate(typeof(U), instanceClass) as object;
                return (U)del;
            }
        }

        public U CreateMethod<U>(object instanceClass, String methodName, BindingFlags bindingFlags) {
            lock (new object()) {
                var method = instanceClass.GetType().GetMethod(methodName, bindingFlags);
                var del = method.CreateDelegate(typeof(U), instanceClass) as object;
                return (U)del;
            }
        }

        public U CreateMethod<U>(object instanceClass, String methodName, Type[] paramsType) {
            lock (new object()) {
                var method = instanceClass.GetType().GetMethod(methodName, paramsType);
                var del = method.CreateDelegate(typeof(U), instanceClass) as object;
                return (U)del;
            }
        }

        //Nao genéricos
        public Delegate CreateMethod(Type delegateType, Type[] delegateArguments, object instanceClass, String methodName) {
            lock (new object()) {
                var dele = delegateType.MakeGenericType(delegateArguments);
                var method = instanceClass.GetType().GetMethod(methodName);
                return method.CreateDelegate(dele, instanceClass);
            }
        }

        public Delegate CreateMethod(Type delegateType, Type[] delegateArguments, object instanceClass, String methodName, BindingFlags bindingFlags) {
            lock (new object()) {
                var dele = delegateType.MakeGenericType(delegateArguments);
                var method = instanceClass.GetType().GetMethod(methodName, bindingFlags);
                return method.CreateDelegate(dele, instanceClass);
            }
        }

        public Delegate CreateMethod(Type delegateType, Type[] delegateArguments, object instanceClass, String methodName, Type[] paramsType) {
            lock (new object()) {
                var dele = delegateType.MakeGenericType(delegateArguments);
                var method = instanceClass.GetType().GetMethod(methodName, paramsType);
                return method.CreateDelegate(dele, instanceClass);
            }
        }

        //Com tipo e MethodInfo
        //Genéricos
        public U CreateMethod<U>(MethodInfo method) {
            lock (new object()) {
                var del = method.CreateDelegate(typeof(U)) as object;
                return (U)del;
            }
        }

        //Não genéricos
        public Delegate CreateMethod(Type delegateType, Type[] delegateArguments, MethodInfo method) {
            lock (new object()) {
                var dele = delegateType.MakeGenericType(delegateArguments);
                return method.CreateDelegate(dele);
            }
        }

        //Methodos por Instâncias e MethodInfo
        //Genericos
        public U CreateMethod<U>(object instanceClass, MethodInfo method) {
            lock (new object()) {
                var del = method.CreateDelegate(typeof(U), instanceClass) as object;
                return (U)del;
            }
        }

        //Nao genéricos
        public Delegate CreateMethod(Type delegateType, Type[] delegateArguments, object instanceClass, MethodInfo method) {
            lock (new object()) {
                var dele = delegateType.MakeGenericType(delegateArguments);
                return method.CreateDelegate(dele, instanceClass);
            }
        }

        //Metodos genéricos por tipo
        //Genéricos
        public U CreateGenericMethod<U>(Type type, String methodName, Type[] methodTypeArguments) {
            lock (new object()) {
                var method = type.GetMethod(methodName).MakeGenericMethod(methodTypeArguments);
                var del = method.CreateDelegate(typeof(U)) as object;
                return (U)del;
            }
        }

        public U CreateGenericMethod<U>(Type type, String methodName, Type[] methodTypeArguments, BindingFlags bindingFlags) {
            lock (new object()) {
                var method = type.GetMethod(methodName, bindingFlags).MakeGenericMethod(methodTypeArguments);
                var del = method.CreateDelegate(typeof(U)) as object;
                return (U)del;
            }
        }

        public U CreateGenericMethod<U>(Type type, String methodName, Type[] methodTypeArguments, Type[] paramsType) {
            lock (new object()) {
                var method = type.GetMethod(methodName, paramsType).MakeGenericMethod(methodTypeArguments);
                var del = method.CreateDelegate(typeof(U)) as object;
                return (U)del;
            }
        }

        //Não genéricos
        public Delegate CreateGenericMethod(Type delegateType, Type[] delegateArguments, Type type, String methodName, Type[] methodTypeArguments) {
            lock (new object()) {
                var dele = delegateType.MakeGenericType(delegateArguments);
                var method = type.GetMethod(methodName).MakeGenericMethod(methodTypeArguments);
                return method.CreateDelegate(dele);
            }
        }

        public Delegate CreateGenericMethod(Type delegateType, Type[] delegateArguments, Type type, String methodName, Type[] methodTypeArguments, BindingFlags bindingFlags) {
            lock (new object()) {
                var dele = delegateType.MakeGenericType(delegateArguments);
                var method = type.GetMethod(methodName, bindingFlags).MakeGenericMethod(methodTypeArguments);
                return method.CreateDelegate(dele);
            }
        }

        public Delegate CreateGenericMethod(Type delegateType, Type[] delegateArguments, Type type, String methodName, Type[] methodTypeArguments, Type[] paramsType) {
            lock (new object()) {
                var dele = delegateType.MakeGenericType(delegateArguments);
                var method = type.GetMethod(methodName, paramsType).MakeGenericMethod(methodTypeArguments);
                return method.CreateDelegate(dele);
            }
        }

        //Methodos Genéricos por Instâncias
        //Genericos
        public U CreateGenericMethod<U>(object instanceClass, String methodName, Type[] methodTypeArguments) {
            lock (new object()) {
                var method = instanceClass.GetType().GetMethod(methodName).MakeGenericMethod(methodTypeArguments) ;
                var del = method.CreateDelegate(typeof(U), instanceClass) as object;
                return (U)del;
            }
        }

        public U CreateGenericMethod<U>(object instanceClass, String methodName, Type[] methodTypeArguments, BindingFlags bindingFlags) {
            lock (new object()) {
                var method = instanceClass.GetType().GetMethod(methodName, bindingFlags).MakeGenericMethod(methodTypeArguments);
                var del = method.CreateDelegate(typeof(U), instanceClass) as object;
                return (U)del;
            }
        }

        public U CreateGenericMethod<U>(object instanceClass, String methodName, Type[] methodTypeArguments, Type[] paramsType) {
            lock (new object()) {
                var method = instanceClass.GetType().GetMethod(methodName, paramsType).MakeGenericMethod(methodTypeArguments);
                var del = method.CreateDelegate(typeof(U), instanceClass) as object;
                return (U)del;
            }
        }

        //Nao genéricos
        public Delegate CreateGenericMethod(Type delegateType, Type[] delegateArguments, object instanceClass, String methodName, Type[] methodTypeArguments) {
            lock (new object()) {
                var dele = delegateType.MakeGenericType(delegateArguments);
                var method = instanceClass.GetType().GetMethod(methodName).MakeGenericMethod(methodTypeArguments);
                return method.CreateDelegate(dele, instanceClass);
            }
        }

        public Delegate CreateGenericMethod(Type delegateType, Type[] delegateArguments, object instanceClass, String methodName, Type[] methodTypeArguments, BindingFlags bindingFlags) {
            lock (new object()) {
                var dele = delegateType.MakeGenericType(delegateArguments);
                var method = instanceClass.GetType().GetMethod(methodName, bindingFlags).MakeGenericMethod(methodTypeArguments);
                return method.CreateDelegate(dele, instanceClass);
            }
        }

        public Delegate CreateGenericMethod(Type delegateType, Type[] delegateArguments, object instanceClass, String methodName, Type[] methodTypeArguments, Type[] paramsType) {
            lock (new object()) {
                var dele = delegateType.MakeGenericType(delegateArguments);
                var method = instanceClass.GetType().GetMethod(methodName, paramsType).MakeGenericMethod(methodTypeArguments);
                return method.CreateDelegate(dele, instanceClass);
            }
        }

        public Delegate CretatePropertySetterMethod(PropertyInfo propertyInfo, object objectInstance) {
            var method = propertyInfo.GetSetMethod();
            var parameterType = method.GetParameters().First().ParameterType;
            return CreateMethod(typeof(Action<>), new Type[] { parameterType }, objectInstance, method);
        }

        public Delegate CreatePropertyGetterMethod(PropertyInfo propertyInfo, object objectInstance) {
            var getMethod = propertyInfo.GetGetMethod();
            return CreateMethod(typeof(Func<>), new Type[] { getMethod.ReturnType }, objectInstance, getMethod);
        }

        public ObjectActivator CreateConstructor(Type type, Type[] arguments) {
            var constructor = type.GetConstructor(arguments);
            var activator = typeof(ObjectActivator);
            ParameterInfo[] paramsInfo = constructor.GetParameters();
            ParameterExpression param = Expression.Parameter(typeof(object[]), "args");
            Expression[] argsExp = new Expression[paramsInfo.Length];

            for (int i = 0; i < paramsInfo.Length; i++) {
                Expression index = Expression.Constant(i);
                Type paramType = paramsInfo[i].ParameterType;
                Expression paramAccessorExp = Expression.ArrayIndex(param, index);
                Expression paramCastExp = Expression.Convert(paramAccessorExp, paramType);
                argsExp[i] = paramCastExp;
            }

            NewExpression newExp = Expression.New(constructor, argsExp);
            LambdaExpression lambda = Expression.Lambda(activator, newExp, param);
            return (ObjectActivator)lambda.Compile();
        }

        public Delegate CreateConstructorFroGenericClass(Type type, Type[] argumentsType, Type[] arguments) {
            Type constructed = type.MakeGenericType(argumentsType);
            return CreateConstructor(constructed, arguments);
        }
    }
}
