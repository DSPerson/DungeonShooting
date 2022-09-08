﻿
using System;

/// <summary>
/// 数字类型
/// </summary>
internal class Number_SValue : ISValue
{
    private double _value;
    
    public Number_SValue(double value)
    {
        _value = value;
    }
    
    public SValueType GetValueType()
    {
        return SValueType.Number;
    }

    public SDataType GetDataType()
    {
        return SDataType.Number;
    }

    public object GetValue()
    {
        return _value;
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