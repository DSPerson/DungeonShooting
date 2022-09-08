
using System;

public class Function_1_SValue : ISValue
{
    private Function_1 _value;
    
    public Function_1_SValue(Function_1 value)
    {
        _value = value;
    }
    
    public SValueType GetValueType()
    {
        return SValueType.Function;
    }

    public SDataType GetDataType()
    {
        return SDataType.Function_0;
    }

    public object GetValue()
    {
        return _value;
    }

    public ISValue Invoke(ISValue v0)
    {
        return _value(v0);
    }

    public ISValue GetProperty(string key)
    {
        return NullSValue.Instance;
    }

    public void SetProperty(string key, ISValue value)
    {
        throw new Exception($"Property {key} is not define!");
    }
}