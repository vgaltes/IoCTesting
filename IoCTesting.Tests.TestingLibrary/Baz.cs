namespace IoCTesting.Tests.TestingLibrary
{
    public interface IBaz
    {
        void MethodA();
        void MethodB();
    }

    public class Baz : IBaz
    {
        public Baz(IFoo foo, IBar bar)
        {
            
        }
        public void MethodA()
        {
        }

        public void MethodB()
        {
        }
    }
}