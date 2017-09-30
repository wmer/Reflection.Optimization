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
        public U CreateMethod<U>(Type type, String methodName) {
            var method = type.GetMethod(methodName);
            var del = method.CreateDelegate(typeof(U)) as object;
            return (U)del;
        }

        public U CreateMethod<U>(Type type, String methodName, BindingFlags bindingFlags) {
            var method = type.GetMethod(methodName, bindingFlags);
            var del = method.CreateDelegate(typeof(U)) as object;
            return (U)del;
        }

        public U CreateMethod<U>(Type type, String methodName, Type[] paramsType) {
            var method = type.GetMethod(methodName, paramsType);
            var del = method.CreateDelegate(typeof(U)) as object;
            return (U)del;
        }

        public U CreateMethod<U>(object instanceClass, String methodName, Type[] paramsType) {
            var method = instanceClass.GetType().GetMethod(methodName, paramsType);
            var del = method.CreateDelegate(typeof(U), instanceClass) as object;
            return (U)del;
        }

        public U CreateGenericMethod<U>(Type type, String methodName, Type[] argumentsType) {
            var method = type.GetMethod(methodName).MakeGenericMethod(argumentsType);
            var del = method.CreateDelegate(typeof(U)) as object;
            return (U)del;
        }

        public U CreateGenericMethod<U>(Type type, String methodName, BindingFlags bindingFlags, Type[] argumentsType) {
            var method = type.GetMethod(methodName, bindingFlags).MakeGenericMethod(argumentsType);
            var del = method.CreateDelegate(typeof(U)) as object;
            return (U)del;
        }

        public U CreateGenericMethod<U>(Type type, String methodName, Type[] paramsType, Type[] argumentsType) {
            var method = type.GetMethod(methodName, paramsType).MakeGenericMethod(argumentsType);
            var del = method.CreateDelegate(typeof(U)) as object;
            return (U)del;
        }

        public U CreateGenericMethod<U>(object instanceClass, String methodName, Type[] argumentsType, Type[] paramsType) {
            var method = instanceClass.GetType().GetMethod(methodName, paramsType).MakeGenericMethod(argumentsType);
            var del = method.CreateDelegate(typeof(U), instanceClass) as object;
            return (U)del;
        }

        public U CreateMethod<U>(MethodInfo method) {
            var del = method.CreateDelegate(typeof(U)) as object;
            return (U)del;
        }

        public U CreateMethod<U>(object instanceClass, MethodInfo method) {
            var del = method.CreateDelegate(typeof(U), instanceClass) as object;
            return (U)del;
        }

        public Delegate CreateMethod(Type typeClass, Type delegateType, String methodName, Type[] paramsType) {
            var method = typeClass.GetMethod(methodName, paramsType);
            return method.CreateDelegate(delegateType);
        }

        public Delegate CreateMethod(Type typeClass, Type delegateType, String methodName, Type[] paramsType, Type[] argumentsType) {
            var method = typeClass.GetMethod(methodName, paramsType).MakeGenericMethod(argumentsType);
            return method.CreateDelegate(delegateType);
        }

        public Delegate CreateMethod(object instanceClass, Type delegateType, String methodName, Type[] paramsType, Type[] argumentsType) {
            var method = instanceClass.GetType().GetMethod(methodName, paramsType).MakeGenericMethod(argumentsType);
            return method.CreateDelegate(delegateType, instanceClass);
        }

        public Delegate CreateMethod(Type delegateType, Type[] argumentsTypeFromDelegate, object instanceClass, MethodInfo method) {
            var typeG = delegateType.MakeGenericType(argumentsTypeFromDelegate);
            return method.CreateDelegate(typeG, instanceClass);
        }

        public Delegate CreateMethod(Type delegateType, Type[] argumentsTypeFromDelegate, object instanceClass, String methodName) {
            var method = instanceClass.GetType().GetMethod(methodName);
            return CreateMethod(delegateType, argumentsTypeFromDelegate, instanceClass, method);
        }

        public Delegate CreateMethod(Type delegateType, Type[] argumentsTypeFromDelegate, object instanceClass, String methodName, Type[] paramsType) {
            var method = instanceClass.GetType().GetMethod(methodName, paramsType);
            return CreateMethod(delegateType, argumentsTypeFromDelegate, instanceClass, method);
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
