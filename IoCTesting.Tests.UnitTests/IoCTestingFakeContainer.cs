namespace IoCTesting.Tests.UnitTests
{
    using System;

    internal class IoCTestingFakeContainer : IoCTesting
    {
        protected override void CreateContainer(string registrationAssemblyFullPath, string registrationClassQualifiedName)
        {
            CreateContainer<bool>(registrationAssemblyFullPath, registrationClassQualifiedName);
        }

        protected override Maybe<Type> DefaultTypeFor(Type type)
        {
            return new Maybe<Type>();
        }
    }
}