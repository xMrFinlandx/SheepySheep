namespace _Scripts.Utilities.Classes
{
    public class ReactiveProperty<T>
    {
        private T _value;

        public delegate void ChangedDelegate(T previous, T current);
        public event ChangedDelegate ValueChanged;
        
        public T Value
        {
            get => _value;
            set
            {
                ValueChanged?.Invoke(_value, value);
                _value = value;
            }
        }

        public ReactiveProperty()
        {
        }

        public ReactiveProperty(T value)
        {
            _value = value;
        }
    }
}