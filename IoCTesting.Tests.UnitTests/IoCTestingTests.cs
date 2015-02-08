namespace IoCTesting.Tests.UnitTests
{
    using System;
    using System.IO;
    using NUnit.Framework;

    [TestFixture]
    public class IoCTestingTests
    {
        private const string InvalidRegistrationAssemblyFullPath = "Invalid path";
        private const string AssemblyToScanFullPath = "IoCTesting.Tests.TestingLibrary.dll";
        private const string ValidAssemblyName = "IoCTesting.Tests.StructureMap.dll";
        private const string InvalidAssemblyPath = "InvalidAssemblyToScan.dll";
        private const string InvalidRegistrationClass = "InvalidRegistrationClassQualifiedName";
        private const string ValidRegistrationClass = "IoCTesting.Tests.StructureMap.StructureMapUserMissingConfiguration";
        private const string NamespaceToScan = "IoCTesting.Tests";


        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidRegistrationAssemblyPath()
        {
            var iocTesting = new IoCTestingFakeContainer();

            iocTesting.CheckDependencies(InvalidRegistrationAssemblyFullPath,
                ValidRegistrationClass, AssemblyToScanFullPath, NamespaceToScan);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullRegistrationAssemblyPath()
        {
            var iocTesting = new IoCTestingFakeContainer();

            iocTesting.CheckDependencies(null, ValidRegistrationClass, AssemblyToScanFullPath, NamespaceToScan);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidRegistrationClassQualifiedName()
        {
            var assemblyToScanFullPath = GetValidAssemblyToScanFullPath();
            var iocTesting = new IoCTestingFakeContainer();
            
            iocTesting.CheckDependencies(assemblyToScanFullPath,
                InvalidRegistrationClass, assemblyToScanFullPath, NamespaceToScan);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullRegistrationClassQualifiedName()
        {
            var assemblyToScanFullPath = GetValidAssemblyToScanFullPath();
            var iocTesting = new IoCTestingFakeContainer();

            iocTesting.CheckDependencies(assemblyToScanFullPath,
                null, assemblyToScanFullPath, NamespaceToScan);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidAssemblyToScanFullPath()
        {
            var assemblyToScanFullPath = GetValidAssemblyToScanFullPath();
            var iocTesting = new IoCTestingFakeContainer();

            iocTesting.CheckDependencies(assemblyToScanFullPath,
                ValidRegistrationClass, InvalidAssemblyPath, NamespaceToScan);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullAssemblyToScanFullPath()
        {
            var assemblyToScanFullPath = GetValidAssemblyToScanFullPath();
            var iocTesting = new IoCTestingFakeContainer();

            iocTesting.CheckDependencies(assemblyToScanFullPath,
                ValidRegistrationClass, null, NamespaceToScan);
        }

        private static string GetValidAssemblyToScanFullPath()
        {
            var assemblyLocation = Directory.GetCurrentDirectory() + "\\";
            var assemblyToScanFullPath = Path.Combine(Path.GetDirectoryName(assemblyLocation), ValidAssemblyName);
            return assemblyToScanFullPath;
        }
    }
}