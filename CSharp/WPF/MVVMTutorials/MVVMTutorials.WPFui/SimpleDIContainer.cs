using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace MVVMTutorials.WPFui
{
    public class SimpleDIContainer
    {
        readonly Dictionary<Type, ConcreteTypeCreationModel> _myTypes = 
            new Dictionary<Type, ConcreteTypeCreationModel>();

        public void Register<T>()
        {
            var myType = typeof(T);
            var myDefaultConstructor = myType.GetConstructor(Type.EmptyTypes);
            if (myDefaultConstructor == null)
            {
                _myTypes[myType] = new ConcreteTypeCreationModel { ConcreteType = myType };
            }
            else
            {
                _myTypes[myType] = new ConcreteTypeCreationModel { DefaultConstructor = myDefaultConstructor };
            }
        }

        public void Register<T>(Func<object> objectFactoryFunction)
        {
            var myType = typeof(T);
            _myTypes[myType] = new ConcreteTypeCreationModel { CustomCreationFunction = objectFactoryFunction };
        }

        public void Register<Tinterface, Timplementation>()
        {
            var myInterface = typeof(Tinterface);
            var myType = typeof(Timplementation);

            if (!myInterface.IsInterface)
                throw new ArgumentException("Left Type must be interface");

            if(!myInterface.IsAssignableFrom(myType))
                throw new ArgumentException("Right Type must be assignable from left Type");


            var myDefaultConstructor = myType.GetConstructor(Type.EmptyTypes);
            if (myDefaultConstructor == null)
            {
                _myTypes[myInterface] = new ConcreteTypeCreationModel { ConcreteType = myType };
            }
            else
            {
                _myTypes[myInterface] = new ConcreteTypeCreationModel { DefaultConstructor = myDefaultConstructor };
            }
        }

        public void Register<Tinterface, Timplementation>(Func<object> objectFactoryFunction)
        {
            var myInterface = typeof(Tinterface);
            var myType = typeof(Timplementation);

            if (!myInterface.IsInterface)
                throw new ArgumentException("Left Type must be interface");

            if (!myInterface.IsAssignableFrom(myType))
                throw new ArgumentException("Right Type must be assignable from left Type");

            _myTypes[myInterface] = new ConcreteTypeCreationModel { CustomCreationFunction = objectFactoryFunction };
        }

        public T Resolve<T>()
        {
            var myType = typeof(T);
            var constructedObject = Construct(myType);
            return (T)constructedObject;
        }

        private object Construct(Type myType)
        {
            if (!_myTypes.ContainsKey(myType))
                throw new ArgumentException("TypeStorage does not contain given Type!");
            var concreteTypCreationModel = _myTypes[myType];

            if (concreteTypCreationModel.DefaultConstructor != null)
                return concreteTypCreationModel.DefaultConstructor.Invoke(Type.EmptyTypes);

            if (concreteTypCreationModel.CustomCreationFunction != null)
                return concreteTypCreationModel.CustomCreationFunction.Invoke();

            var ctor = concreteTypCreationModel
                .ConcreteType
                .GetConstructors()
                .FirstOrDefault();
            var ctorParams = ctor.GetParameters();
                          
            var constructedObjectParameters = from parameterInfo in ctorParams
                          select Construct(parameterInfo.ParameterType);
            var requestedObject = ctor.Invoke(constructedObjectParameters.ToArray());
            return requestedObject;
        }
    }

}
