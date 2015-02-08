namespace IoCTesting.Tests.UnitTests
{
    using System.IO;
    using System.Linq;
    using global::IoCTesting.StructureMap;
    using NUnit.Framework;

    [TestFixture]
    public class IoCTestingStructureMapTests
    {
        private const string NamespaceToScan = "IoCTesting.Tests";
        private static readonly string AssemblyLocation = Directory.GetCurrentDirectory() + "\\";
        private static readonly string RegisteringAssemblyPath = Path.Combine(Path.GetDirectoryName(AssemblyLocation), "IoCTesting.Tests.StructureMap.dll");
        private static readonly string TestingAssemblyPath = Path.Combine(Path.GetDirectoryName(AssemblyLocation), "IoCTesting.Tests.TestingLibrary.dll");

        [Test]
        public void TestDetectsMissingConfiguration()
        {
            var structureMapTesting = new IoCTestingStructureMap();
            var errors = structureMapTesting.CheckDependencies(RegisteringAssemblyPath,
                "IoCTesting.Tests.StructureMap.StructureMapUserMissingConfiguration", TestingAssemblyPath, NamespaceToScan);

            Assert.AreEqual(1, errors.Count(), string.Join(", ", errors));
        }

        [Test]
        public void TestDetectsCorrectConfiguration()
        {
            var structureMapTesting = new IoCTestingStructureMap();
            var errors = structureMapTesting.CheckDependencies(RegisteringAssemblyPath,
                "IoCTesting.Tests.StructureMap.StructureMapUserCorrectConfiguration", TestingAssemblyPath, NamespaceToScan);

            Assert.AreEqual(0, errors.Count(), string.Join(", ", errors));
        }

        [Test]
        public void StaticMethod()
        {
            var iocTesting = new IoCTestingStructureMap();

            iocTesting.CheckDependencies(RegisteringAssemblyPath,
                "IoCTesting.Tests.StructureMap.StructureMapStaticMethod", TestingAssemblyPath, NamespaceToScan);
        }
    }
}
