namespace IoCTesting
{
    using System;

    public struct Maybe<T>
    {
        private readonly bool _initialized;
        private readonly bool _isEmpty;
        private readonly T _value;

        public Maybe(T value)
        {
            _value = value;
            _isEmpty = value == null;
            _initialized = true;
        }

        public T Value
        {
            get { return _value; }
        }

        public bool IsEmpty
        {
            get { return (!_initialized) || _isEmpty; }
        }

        public static Maybe<T> Empty()
        {
            return new Maybe<T>();
        }

        public void Do(Action<T> action)
        {
            if (!IsEmpty) action(Value);
        }

        public void Do(Action<T> action, Action elseAction)
        {
            if (!IsEmpty)
            {
                action(Value);  
            }
            else
            {
                elseAction();
            }
        }

        public TR Do<TR>(Func<T, TR> action)
        {
            return Do(action, default(TR));
        }

        public TR Do<TR>(Func<T, TR> action, TR defaultValue)
        {
            return IsEmpty ? defaultValue : action(Value);
        }


        public Maybe<TR> Apply<TR>(Func<T, TR> action)
        {
            return IsEmpty ? Maybe<TR>.Empty() : new Maybe<TR>(action(Value));
        }
    }
}