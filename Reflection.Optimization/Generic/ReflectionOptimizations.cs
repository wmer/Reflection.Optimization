using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.Optimization.Generic {
    public class ReflectionOptimizations<T> {
        public U CreateDelegate<U>(String methodName, Type[] paramsType) {
            return new ReflectionOptimizations().CreateMethod<U>(typeof(T), methodName, paramsType);
        }

        public U CreateDelegateForGenericMethod<U>(String methodName, Type[] argumentsType, Type[] paramsType) {
            return new ReflectionOptimizations().CreateGenericMethod<U>(typeof(T), methodName, argumentsType, paramsType);
        }

        public U CreateDelegateForGenericMethod<U>(object instanceClass, String methodName, Type[] argumentsType, Type[] paramsType) {
            return new ReflectionOptimizations().CreateGenericMethod<U>(instanceClass, methodName, argumentsType, paramsType);
        }

        public ObjectActivator CreateConstructor(Type[] arguments) {
            return new ReflectionOptimizations().CreateConstructor(typeof(T), arguments);
        }
    }
}
