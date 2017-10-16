using System;
using System.Collections.Generic;
using System.Text;

namespace Reflection.Optimization.Generic {
    public class ConstructorHelper<T> {
        private ConstructorHelper _constructorHelper;
        
        public ConstructorHelper(ConstructorHelper constructorHelper) {
            _constructorHelper = constructorHelper;
        }

        public ObjectActivator CreateConstructor(Type[] arguments) => _constructorHelper.CreateConstructor(typeof(T), arguments);
    }
}
