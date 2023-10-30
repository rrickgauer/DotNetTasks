

namespace Tasks.Service.Domain.Other;

public class ChangeTracker<T>
{
    private T _value;

    public T Value
    {
        get => _value;
        set
        {
            if (!EqualityComparer<T>.Default.Equals(_value, value))
            {
                Changed = true;
            }

            _value = value;
        }
    }

    public bool Changed { get; private set; } = false;


    public ChangeTracker(T value)
    {
        _value = value;
    }

    public void Reset()
    {
        Changed = false;
    }

    public static implicit operator T(ChangeTracker<T> other) => other.Value;
    public static implicit operator ChangeTracker<T>(T value) => new(value);
}
