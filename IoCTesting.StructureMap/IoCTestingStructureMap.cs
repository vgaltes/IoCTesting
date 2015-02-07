namespace IoCTesting.StructureMap
{
    using System;
    using global::StructureMap;
    
    public class IoCTestingStructureMap : IoCTesting
    {
        private Maybe<IContainer> _container;

        protected override void CreateContainer(string registrationAssembly, string registrationClassQualifiedName)
        {
            _container = CreateContainer<IContainer>(registrationAssembly, registrationClassQualifiedName);
        }

        protected override Maybe<Type> DefaultTypeFor(Type type)
        {
            var value = _container.Do(c => c.Model.DefaultTypeFor(type));
            return new Maybe<Type>(value);
        }
    }
}