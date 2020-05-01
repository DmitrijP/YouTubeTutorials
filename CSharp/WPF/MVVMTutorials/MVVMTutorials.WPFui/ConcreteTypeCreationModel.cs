using System;
using System.Reflection;

namespace MVVMTutorials.WPFui
{
    class ConcreteTypeCreationModel
    {
        public Type ConcreteType { get; set; }
        public ConstructorInfo DefaultConstructor { get; set; }
        public Func<object> CustomCreationFunction { get; set; }
    }
}
