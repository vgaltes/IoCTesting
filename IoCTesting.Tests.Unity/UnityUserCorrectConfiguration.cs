namespace IoCTesting.Tests.Unity
{
    using Microsoft.Practices.Unity;
    using TestingLibrary;

    public class UnityUserCorrectConfiguration
    {
        public IUnityContainer CreateContainer()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IFoo, Foo>();
            container.RegisterType<IBar, Bar>();
            container.RegisterType<IBaz, Baz>();

            return container;
        }
    }
}