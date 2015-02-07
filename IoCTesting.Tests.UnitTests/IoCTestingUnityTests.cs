namespace IoCTesting.Tests.UnitTests
{
    using System.IO;
    using System.Linq;
    using global::IoCTesting.Unity;
    using NUnit.Framework;

    [TestFixture]
    public class IoCTestingUnityTests
    {
        private static readonly string AssemblyLocation = Directory.GetCurrentDirectory() + "\\";
        private static readonly string RegisteringAssemblyPath = Path.Combine(Path.GetDirectoryName(AssemblyLocation), "IoCTesting.Tests.Unity.dll");
        private static readonly string TestingAssemblyPath = Path.Combine(Path.GetDirectoryName(AssemblyLocation), "IoCTesting.Tests.TestingLibrary.dll");

        [Test]
        public void TestDetectsMissingConfiguration()
        {
            var structureMapTesting = new IoCTestingUnity();
            var errors = structureMapTesting.CheckDependencies(RegisteringAssemblyPath,
                "IoCTesting.Tests.Unity.UnityUserMissingConfiguration", TestingAssemblyPath);

            Assert.AreEqual(1, errors.Count(), string.Join(", ", errors));
        }

        [Test]
        public void TestDetectsCorrectConfiguration()
        {
            var unityTesting = new IoCTestingUnity();
            var errors = unityTesting.CheckDependencies(RegisteringAssemblyPath,
                "IoCTesting.Tests.Unity.UnityUserCorrectConfiguration", RegisteringAssemblyPath);

            Assert.AreEqual(0, errors.Count(), string.Join(", ", errors));
        }
    }
}
