namespace IoCTesting.Tests.StructureMap
{
    using global::StructureMap;
    using TestingLibrary;

    public class StructureMapUserMissingConfiguration
    {
        public IContainer CreateContainerWithMissingConfiguration()
        {
            return new Container(x =>
            {
                x.For<IBar>().Use<Bar>();
                x.For<IBaz>().Use<Baz>();
            });
        }
    }
}