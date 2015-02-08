namespace IoCTesting.Tests.StructureMap
{
    using global::StructureMap;
    using TestingLibrary;

    public static class StructureMapStaticMethod
    {
        public static IContainer CreateContainer()
        {
            return new Container(x =>
            {
                x.For<IFoo>().Use<Foo>();
                x.For<IBar>().Use<Bar>();
                x.For<IBaz>().Use<Baz>();
            });
        }
    }
}