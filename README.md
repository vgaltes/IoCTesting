# IoCTesting
A tiny library to test your IoC configuration

## Installation ##
IoCTesting consist of three libraries


- IoCTesting.Unity: to test a Unity container
- IoCTesting.StructureMap: to test a StructreMap container
- IoCTesting: the shared library which performs the test

So, if you are testing a Unity container you should install the IoCTesting.Unity and IoCTesting library. You can do it in a single step installing the IoCTesting.Unity package:

    install-package IoCTesting.Unity

If you are testing a StructureMap container, you should install the IoCTesting.StructerMap package:


    install-package IoCTesting.StructureMap

## How it works ##
IoCTesting scans the assembly you specify and looks for all the constructors of that assembly. If the constructor has a parameter that is a class or an interface, it looks at the container to see if that class or interface is registered. No more, no less.

The only requirement in your code is that you must have a function that returns the container. Something like this:

    public IContainer CreateContainer()
    {
        return new Container(x =>
        {
            x.For<IFoo>().Use<Foo>();
            x.For<IBar>().Use<Bar>();
            x.For<IBaz>().Use<Baz>();
        });
    }

The library has only one function, called CheckDependencies, that takes four parameters:

- The full path of the assembly where the registration is performed.
- The qualified name of the class that performs the registration.
- The assembly you want to scan
- The root namespace you want to scan. Types that its namespace doesn't start with it won't be scanned.

It returns an IEnumerable<string> with the classes or interfaces that can not be found.

It also throws exceptions if some of the arguments are null or invalid.

The library only analyses constructor injection, not property injection.