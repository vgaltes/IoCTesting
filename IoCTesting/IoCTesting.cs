namespace IoCTesting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Class to test that all the dependencies in a certain assembly are registered in the container.
    /// </summary>
    /// <remarks>Your code must have a method returning an instance of the container.
    /// The library only checks dependencies injected by constructor.
    /// </remarks>
    public abstract class IoCTesting
    {
        private readonly List<string> _errors = new List<string>();

        /// <summary>
        /// Checks if all the dependencies used in assemblyToScanFullPath are registered
        /// in registrationAssemblyFullPath.
        /// </summary>
        /// <param name="registrationAssemblyFullPath">Full path of the assembly where the registration is performed.</param>
        /// <param name="registrationClassQualifiedName">Qualified name of the class that performs the registration.</param>
        /// <param name="assemblyToScanFullPath">Assembly to scan for missing dependencies.</param>
        /// <returns>A list of the dependencies not registered.</returns>
        public IEnumerable<string> CheckDependencies(string registrationAssemblyFullPath,
            string registrationClassQualifiedName, string assemblyToScanFullPath)
        {
            Condition.NotNull(assemblyToScanFullPath, "assemblyToScanFullPath");
            Condition.NotNull(registrationClassQualifiedName, "registrationClassQualifiedName");
            Condition.NotNull(registrationAssemblyFullPath, "registrationAssemblyFullPath");
            Condition.FileExists(assemblyToScanFullPath, "assemblyToScanFullPath");
            Condition.FileExists(registrationAssemblyFullPath, "registrationAssemblyFullPath");

            CreateContainer(registrationAssemblyFullPath, registrationClassQualifiedName);

            CheckTypes(Assembly.LoadFrom(assemblyToScanFullPath).GetTypes());

            return _errors;
        }

        protected Maybe<T> CreateContainer<T>(string registrationAssembly, string registrationClassQualifiedName)
        {
            var registrationType = Assembly.LoadFrom(registrationAssembly).GetType(registrationClassQualifiedName);

            Condition.NotNull(registrationType, "registrationClassQualifiedName",
                string.Format("The class {0} does not exist", registrationClassQualifiedName));

            return new Maybe<T>(InvokeRegistrationMethod<T>(registrationType));
        }

        protected abstract void CreateContainer(string registrationAssemblyFullPath,
            string registrationClassQualifiedName);

        protected abstract Maybe<Type> DefaultTypeFor(Type type);

        private void CheckTypes(IEnumerable<Type> types)
        {
            var parameters = types.Where(t => t.IsClass)
                .SelectMany(t => t.GetConstructors())
                .SelectMany(c => c.GetParameters())
                .Where(p => !p.ParameterType.IsValueType && !(p.ParameterType == typeof (Type)));

            foreach (var parameter in parameters)
            {
                var parameterObject = DefaultTypeFor(parameter.ParameterType);
                parameterObject.Do(p => CheckTypes(new[] {p}), () => { AddError(parameter); });
            }
        }

        private void AddError(ParameterInfo parameter)
        {
            if (!_errors.Contains(parameter.ParameterType.FullName))
            {
                _errors.Add(parameter.ParameterType.FullName);
            }
        }

        private static T InvokeRegistrationMethod<T>(Type registrationType)
        {
            var registrationMethod = GetRegistrationMethod<T>(registrationType);
            var registrationClassInstance = CreateClassInstance<T>(registrationType);

            return (T) registrationMethod.Invoke(registrationClassInstance, null);
        }

        private static object CreateClassInstance<T>(Type registrationType)
        {
            var registrationClassInstance = Activator.CreateInstance(registrationType);
            return registrationClassInstance;
        }

        private static MethodInfo GetRegistrationMethod<T>(Type registrationType)
        {
            var registrationMethod =
                registrationType.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                    .FirstOrDefault(m => m.ReturnType == typeof (T));
            return registrationMethod;
        }
    }
}