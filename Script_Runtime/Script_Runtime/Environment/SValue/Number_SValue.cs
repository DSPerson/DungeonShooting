﻿
using System;

/// <summary>
/// 数字类型
/// </summary>
internal class Number_SValue : SValue
{
    internal double _value;

    public Number_SValue(double value)
    {
        _value = value;
    }

    public override ScriptType GetScriptType()
    {
        return ScriptType.Number;
    }

    public override DataType GetDataType()
    {
        return DataType.Number;
    }

    public override object GetValue()
    {
        return _value;
    }

    public override SValue GetMember(string key)
    {
        return Null;
    }

    public override SValue HasMember(string key)
    {
        return False;
    }

    public override void SetMember(string key, SValue value)
    {
        throw new OperationMemberException($"Member '{key}' not defined.");
    }

    public override SValue Invoke()
    {
        throw new InvokeMethodException($"{_value} is not a function.");
    }

    public override SValue Invoke(SValue v0)
    {
        throw new InvokeMethodException($"{_value} is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1)
    {
        throw new InvokeMethodException($"{_value} is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2)
    {
        throw new InvokeMethodException($"{_value} is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3)
    {
        throw new InvokeMethodException($"{_value} is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4)
    {
        throw new InvokeMethodException($"{_value} is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5)
    {
        throw new InvokeMethodException($"{_value} is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6)
    {
        throw new InvokeMethodException($"{_value} is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
        SValue v7)
    {
        throw new InvokeMethodException($"{_value} is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
        SValue v7, SValue v8)
    {
        throw new InvokeMethodException($"{_value} is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
        SValue v7, SValue v8,
        SValue v9)
    {
        throw new InvokeMethodException($"{_value} is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
        SValue v7, SValue v8,
        SValue v9, SValue v10)
    {
        throw new InvokeMethodException($"{_value} is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
        SValue v7, SValue v8,
        SValue v9, SValue v10, SValue v11)
    {
        throw new InvokeMethodException($"{_value} is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
        SValue v7, SValue v8,
        SValue v9, SValue v10, SValue v11, SValue v12)
    {
        throw new InvokeMethodException($"{_value} is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
        SValue v7, SValue v8,
        SValue v9, SValue v10, SValue v11, SValue v12, SValue v13)
    {
        throw new InvokeMethodException($"{_value} is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
        SValue v7, SValue v8,
        SValue v9, SValue v10, SValue v11, SValue v12, SValue v13, SValue v14)
    {
        throw new InvokeMethodException($"{_value} is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
        SValue v7, SValue v8,
        SValue v9, SValue v10, SValue v11, SValue v12, SValue v13, SValue v14, SValue v15)
    {
        throw new InvokeMethodException($"{_value} is not a function.");
    }

    public override SValue InvokeMethod(string key)
    {
        throw new InvokeMethodException($"The member function {_value}.{key} was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0)
    {
        throw new InvokeMethodException($"The member function {_value}.{key} was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1)
    {
        throw new InvokeMethodException($"The member function {_value}.{key} was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2)
    {
        throw new InvokeMethodException($"The member function {_value}.{key} was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3)
    {
        throw new InvokeMethodException($"The member function {_value}.{key} was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4)
    {
        throw new InvokeMethodException($"The member function {_value}.{key} was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5)
    {
        throw new InvokeMethodException($"The member function {_value}.{key} was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
        SValue v6)
    {
        throw new InvokeMethodException($"The member function {_value}.{key} was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
        SValue v6, SValue v7)
    {
        throw new InvokeMethodException($"The member function {_value}.{key} was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
        SValue v6, SValue v7,
        SValue v8)
    {
        throw new InvokeMethodException($"The member function {_value}.{key} was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
        SValue v6, SValue v7,
        SValue v8, SValue v9)
    {
        throw new InvokeMethodException($"The member function {_value}.{key} was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
        SValue v6, SValue v7,
        SValue v8, SValue v9, SValue v10)
    {
        throw new InvokeMethodException($"The member function {_value}.{key} was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
        SValue v6, SValue v7,
        SValue v8, SValue v9, SValue v10, SValue v11)
    {
        throw new InvokeMethodException($"The member function {_value}.{key} was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
        SValue v6, SValue v7,
        SValue v8, SValue v9, SValue v10, SValue v11, SValue v12)
    {
        throw new InvokeMethodException($"The member function {_value}.{key} was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
        SValue v6, SValue v7,
        SValue v8, SValue v9, SValue v10, SValue v11, SValue v12, SValue v13)
    {
        throw new InvokeMethodException($"The member function {_value}.{key} was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
        SValue v6, SValue v7,
        SValue v8, SValue v9, SValue v10, SValue v11, SValue v12, SValue v13, SValue v14)
    {
        throw new InvokeMethodException($"The member function {_value}.{key} was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
        SValue v6, SValue v7,
        SValue v8, SValue v9, SValue v10, SValue v11, SValue v12, SValue v13, SValue v14, SValue v15)
    {
        throw new InvokeMethodException($"The member function {_value}.{key} was not found");
    }

    internal override bool Operator_Equal_Double(double v2)
    {
        return _value == v2;
    }

    internal override bool Operator_Equal_String(string v2)
    {
        return false;
    }

    internal override bool Operator_Equal_SValue(SValue v2)
    {
        switch (v2.GetDataType())
        {
            case DataType.Number:
                return _value == ((Number_SValue)v2)._value;
            case DataType.True:
                return _value == 1;
            case DataType.False:
                return _value == 0;
        }

        return false;
    }

    internal override bool Operator_Not_Equal_Double(double v2)
    {
        return _value != v2;
    }

    internal override bool Operator_Not_Equal_String(string v2)
    {
        return true;
    }

    internal override bool Operator_Not_Equal_SValue(SValue v2)
    {
        switch (v2.GetDataType())
        {
            case DataType.Number:
                return _value != ((Number_SValue)v2)._value;
            case DataType.True:
                return _value != 1;
            case DataType.False:
                return _value != 0;
        }

        return true;
    }

    internal override SValue Operator_Add_Double(double v2)
    {
        return new Number_SValue(_value + v2);
    }

    internal override SValue Operator_Append_Add_Double(double v1)
    {
        return new Number_SValue(v1 + _value);
    }

    internal override SValue Operator_Add_String(string v2)
    {
        return new String_SValue(_value + v2);
    }

    internal override SValue Operator_Append_Add_String(string v1)
    {
        return new String_SValue(v1 + _value);
    }

    internal override SValue Operator_Add_SValue(SValue v2)
    {
        switch (v2.GetDataType())
        {
            case DataType.Number:
                return new Number_SValue(_value + ((Number_SValue)v2)._value);
            case DataType.String:
                return new String_SValue(_value + (string)v2.GetValue());
            case DataType.True:
                return new Number_SValue(_value + 1);
            case DataType.False:
            case DataType.Null:
                return this;
        }

        return new String_SValue(_value + v2.GetValue().ToString());
    }

    internal override SValue Operator_Subtract_Double(double v2)
    {
        return new Number_SValue(_value - v2);
    }

    internal override SValue Operator_Append_Subtract_Double(double v1)
    {
        return new Number_SValue(v1 - _value);
    }

    internal override SValue Operator_Subtract_SValue(SValue v2)
    {
        switch (v2.GetDataType())
        {
            case DataType.Number:
                return new Number_SValue(_value - ((Number_SValue)v2)._value);
            case DataType.True:
                return new Number_SValue(_value - 1);
            case DataType.False:
            case DataType.Null:
                return this;
        }

        return NaN;
    }

    internal override SValue Operator_Multiply_Double(double v2)
    {
        return new Number_SValue(_value * v2);
    }

    internal override SValue Operator_Append_Multiply_Double(double v1)
    {
        return new Number_SValue(v1 * _value);
    }

    internal override SValue Operator_Multiply_SValue(SValue v2)
    {
        switch (v2.GetDataType())
        {
            case DataType.Number:
                return new Number_SValue(_value * ((Number_SValue)v2)._value);
            case DataType.True:
                return this;
            case DataType.False:
            case DataType.Null:
                return Zero;
        }

        return NaN;
    }

    internal override SValue Operator_Divide_Double(double v2)
    {
        return new Number_SValue(_value / v2);
    }

    internal override SValue Operator_Append_Divide_Double(double v1)
    {
        return new Number_SValue(v1 / _value);
    }

    internal override SValue Operator_Divide_SValue(SValue v2)
    {
        switch (v2.GetDataType())
        {
            case DataType.Number:
                return new Number_SValue(_value / ((Number_SValue)v2)._value);
            case DataType.True:
                return this;
            case DataType.False:
            case DataType.Null:
                return _value == 0 ? NaN : (_value > 0 ? PositiveInfinity : NegativeInfinity);
        }

        return NaN;
    }

    internal override SValue Operator_SinceAdd()
    {
        return new Number_SValue(_value + 1);
    }

    internal override SValue Operator_SinceReduction()
    {
        return new Number_SValue(_value - 1);
    }

    internal override bool Operator_Greater_Double(double v2)
    {
        return _value > v2;
    }

    internal override bool Operator_Less_Double(double v2)
    {
        return _value < v2;
    }

    internal override bool Operator_Greater_SValue(SValue v2)
    {
        switch (v2.GetDataType())
        {
            case DataType.Number:
                return _value > ((Number_SValue)v2)._value;
            case DataType.True:
                return _value > 1;
            case DataType.False:
            case DataType.Null:
                return _value > 0;
        }

        return false;
    }

    internal override bool Operator_Less_SValue(SValue v2)
    {
        switch (v2.GetDataType())
        {
            case DataType.Number:
                return _value < ((Number_SValue)v2)._value;
            case DataType.True:
                return _value < 1;
            case DataType.False:
            case DataType.Null:
                return _value < 0;
        }

        return false;
    }

    internal override bool Operator_Greater_Equal_Double(double v2)
    {
        return _value >= v2;
    }

    internal override bool Operator_Less_Equal_Double(double v2)
    {
        return _value <= v2;
    }

    internal override bool Operator_Greater_Equal_SValue(SValue v2)
    {
        switch (v2.GetDataType())
        {
            case DataType.Number:
                return _value >= ((Number_SValue)v2)._value;
            case DataType.True:
                return _value >= 1;
            case DataType.False:
            case DataType.Null:
                return _value >= 0;
        }

        return false;
    }

    internal override SValue Operator_Positive()
    {
        return this;
    }

    internal override bool Operator_Less_Equal_SValue(SValue v2)
    {
        switch (v2.GetDataType())
        {
            case DataType.Number:
                return _value <= ((Number_SValue)v2)._value;
            case DataType.True:
                return _value <= 1;
            case DataType.False:
            case DataType.Null:
                return _value <= 0;
        }

        return false;
    }

    internal override SValue Operator_Negative()
    {
        return new Number_SValue(-_value);
    }

    internal override bool Operator_Not()
    {
        return _value <= 0;
    }

    internal override bool Operator_True()
    {
        return _value > 0;
    }

    internal override bool Operator_False()
    {
        return _value <= 0;
    }

    internal override SValue Operator_Modulus_Double(double v2)
    {
        return new Number_SValue(_value % v2);
    }

    internal override SValue Operator_Append_Modulus_Double(double v1)
    {
        return new Number_SValue(v1 % _value);
    }

    internal override SValue Operator_Modulus_SValue(SValue v2)
    {
        switch (v2.GetDataType())
        {
            case DataType.Number:
                return new Number_SValue(_value % ((Number_SValue)v2)._value);
            case DataType.True:
                return new Number_SValue(_value % 1);
        }

        return NaN;
    }

    internal override SValue Operator_Shift_Negation()
    {
        return new Number_SValue(~(int)_value);
    }

    internal override SValue Operator_Shift_Right(int v1)
    {
        return new Number_SValue((int)_value >> v1);
    }

    internal override SValue Operator_Shift_Left(int v1)
    {
        return new Number_SValue((int)_value << v1);
    }

    internal override SValue Operator_Shift_Or_Double(double v2)
    {
        return new Number_SValue((int)_value | (int)v2);
    }

    internal override SValue Operator_Append_Shift_Or_Double(double v1)
    {
        return new Number_SValue((int)v1 | (int)_value);
    }

    internal override SValue Operator_Shift_Or_SValue(SValue v2)
    {
        switch (v2.GetDataType())
        {
            case DataType.Number:
                return new Number_SValue((int)_value | (int)((Number_SValue)v2)._value);
            case DataType.True:
                return new Number_SValue((int)_value | 1);
        }

        return new Number_SValue((int)_value | 0);
    }

    internal override SValue Operator_Shift_And_Double(double v2)
    {
        return new Number_SValue((int)_value & (int)v2);
    }

    internal override SValue Operator_Append_Shift_And_Double(double v1)
    {
        return new Number_SValue((int)v1 & (int)_value);
    }

    internal override SValue Operator_Shift_And_SValue(SValue v2)
    {
        switch (v2.GetDataType())
        {
            case DataType.Number:
                return new Number_SValue((int)_value & (int)((Number_SValue)v2)._value);
            case DataType.True:
                return new Number_SValue((int)_value & 1);
        }

        return new Number_SValue((int)_value & 0);
    }

    internal override SValue Operator_Shift_Xor_Double(double v2)
    {
        return new Number_SValue((int)_value ^ (int)v2);
    }

    internal override SValue Operator_Append_Shift_Xor_Double(double v1)
    {
        return new Number_SValue((int)v1 ^ (int)_value);
    }

    internal override SValue Operator_Shift_Xor_SValue(SValue v2)
    {
        switch (v2.GetDataType())
        {
            case DataType.Number:
                return new Number_SValue((int)_value ^ (int)((Number_SValue)v2)._value);
            case DataType.True:
                return new Number_SValue((int)_value ^ 1);
        }

        return new Number_SValue((int)_value ^ 0);
    }
}