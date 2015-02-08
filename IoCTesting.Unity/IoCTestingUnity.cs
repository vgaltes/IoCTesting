namespace IoCTesting.Unity
{
    using System;
    using System.Linq;
    using Microsoft.Practices.Unity;

    public class IoCTestingUnity : IoCTesting
    {
        private Maybe<IUnityContainer> _container;

        protected override void CreateContainer(string registrationAssembly, string registrationClassQualifiedName)
        {
            _container = CreateContainer<IUnityContainer>(registrationAssembly, registrationClassQualifiedName);
        }

        protected override Maybe<Type> DefaultTypeFor(Type type)
        {
            return
                new Maybe<Type>(
                    _container.Apply(c => c.Registrations.FirstOrDefault(r => r.RegisteredType == type))
                        .Do(r => r.MappedToType));
        }

        protected override void DisposeContainer()
        {
            _container.Do(c => c.Dispose());
        }
    }
}