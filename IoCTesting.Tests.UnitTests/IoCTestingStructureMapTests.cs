namespace IoCTesting.Tests.UnitTests
{
    using System.IO;
    using System.Linq;
    using global::IoCTesting.StructureMap;
    using NUnit.Framework;

    [TestFixture]
    public class IoCTestingStructureMapTests
    {
        private static readonly string AssemblyLocation = Directory.GetCurrentDirectory() + "\\";
        private static readonly string RegisteringAssemblyPath = Path.Combine(Path.GetDirectoryName(AssemblyLocation), "IoCTesting.Tests.StructureMap.dll");
        private static readonly string TestingAssemblyPath = Path.Combine(Path.GetDirectoryName(AssemblyLocation), "IoCTesting.Tests.TestingLibrary.dll");

        [Test]
        public void TestDetectsMissingConfiguration()
        {
            var structureMapTesting = new IoCTestingStructureMap();
            var errors = structureMapTesting.CheckDependencies(RegisteringAssemblyPath,
                "IoCTesting.Tests.StructureMap.StructureMapUserMissingConfiguration", TestingAssemblyPath);

            Assert.AreEqual(1, errors.Count(), string.Join(", ", errors));
        }

        [Test]
        public void TestDetectsCorrectConfiguration()
        {
            var structureMapTesting = new IoCTestingStructureMap();
            var errors = structureMapTesting.CheckDependencies(RegisteringAssemblyPath,
                "IoCTesting.Tests.StructureMap.StructureMapUserCorrectConfiguration", TestingAssemblyPath);

            Assert.AreEqual(0, errors.Count(), string.Join(", ", errors));
        }
    }
}
