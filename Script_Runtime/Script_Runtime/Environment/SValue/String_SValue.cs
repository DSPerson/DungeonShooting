
/// <summary>
/// 字符串
/// </summary>
internal class String_SValue : ISValue
{
    private string _value;

    public String_SValue(string value)
    {
        _value = value;
    }
    
    public SValueType GetValueType()
    {
        return SValueType.String;
    }

    public SDataType GetDataType()
    {
        return SDataType.String;
    }

    public object GetValue()
    {
        return _value;
    }

    public ISValue GetProperty(string key)
    {
        throw new System.NotImplementedException();
    }

    public void SetProperty(string key, ISValue value)
    {
        throw new System.NotImplementedException();
    }
}