using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Reflection.Optimization {
    public class ConstructorHelper {
        private readonly object _lock1 = new object();
        private readonly object _lock2 = new object();

        public ObjectActivator CreateConstructor(Type type, Type[] arguments) {
            lock (_lock1) {
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
        }

        public Delegate CreateConstructorFroGenericClass(Type type, Type[] argumentsType, Type[] arguments) {
            lock (_lock2) {
                Type constructed = type.MakeGenericType(argumentsType);
                return CreateConstructor(constructed, arguments);
            }
        }
    }
}
