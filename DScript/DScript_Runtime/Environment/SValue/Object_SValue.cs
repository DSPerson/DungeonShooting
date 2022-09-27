﻿
namespace DScript.Runtime
{
    /// <summary>
    /// 对象类型
    /// </summary>
    internal class Object_SValue : SValue
    {
        internal readonly SObject _value;

        public Object_SValue(SObject value)
        {
            _value = value;
            dataType = DataType.Object;
        }

        public override object GetValue()
        {
            return _value;
        }

        public override SValue GetMember(string key)
        {
            return _value.__GetMember(key);
        }

        public override bool HasMember(string key)
        {
            return _value.__HasMember(key);
        }

        public override void SetMember(string key, SValue value)
        {
            _value.__SetMember(key, value);
        }

        public override SValue Invoke()
        {
            throw new InvokeMethodException($"'{_value}' is not a function.");
        }

        public override SValue Invoke(SValue v0)
        {
            throw new InvokeMethodException($"'{_value}' is not a function.");
        }

        public override SValue Invoke(SValue v0, SValue v1)
        {
            throw new InvokeMethodException($"'{_value}' is not a function.");
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2)
        {
            throw new InvokeMethodException($"'{_value}' is not a function.");
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3)
        {
            throw new InvokeMethodException($"'{_value}' is not a function.");
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4)
        {
            throw new InvokeMethodException($"'{_value}' is not a function.");
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5)
        {
            throw new InvokeMethodException($"'{_value}' is not a function.");
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6)
        {
            throw new InvokeMethodException($"'{_value}' is not a function.");
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
            SValue v7)
        {
            throw new InvokeMethodException($"'{_value}' is not a function.");
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
            SValue v7, SValue v8)
        {
            throw new InvokeMethodException($"'{_value}' is not a function.");
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
            SValue v7, SValue v8,
            SValue v9)
        {
            throw new InvokeMethodException($"'{_value}' is not a function.");
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
            SValue v7, SValue v8,
            SValue v9, SValue v10)
        {
            throw new InvokeMethodException($"'{_value}' is not a function.");
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
            SValue v7, SValue v8,
            SValue v9, SValue v10, SValue v11)
        {
            throw new InvokeMethodException($"'{_value}' is not a function.");
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
            SValue v7, SValue v8,
            SValue v9, SValue v10, SValue v11, SValue v12)
        {
            throw new InvokeMethodException($"'{_value}' is not a function.");
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
            SValue v7, SValue v8,
            SValue v9, SValue v10, SValue v11, SValue v12, SValue v13)
        {
            throw new InvokeMethodException($"'{_value}' is not a function.");
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
            SValue v7, SValue v8,
            SValue v9, SValue v10, SValue v11, SValue v12, SValue v13, SValue v14)
        {
            throw new InvokeMethodException($"'{_value}' is not a function.");
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
            SValue v7, SValue v8,
            SValue v9, SValue v10, SValue v11, SValue v12, SValue v13, SValue v14, SValue v15)
        {
            throw new InvokeMethodException($"'{_value}' is not a function.");
        }

        public override SValue InvokeMethod(string key)
        {
            return _value.__InvokeMethod(key);
        }

        public override SValue InvokeMethod(string key, SValue v0)
        {
            return _value.__InvokeMethod(key, v0);
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1)
        {
            return _value.__InvokeMethod(key, v0, v1);
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2)
        {
            return _value.__InvokeMethod(key, v0, v1, v2);
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3)
        {
            return _value.__InvokeMethod(key, v0, v1, v2, v3);
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4)
        {
            return _value.__InvokeMethod(key, v0, v1, v2, v3, v4);
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4,
            SValue v5)
        {
            return _value.__InvokeMethod(key, v0, v1, v2, v3, v4, v5);
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4,
            SValue v5,
            SValue v6)
        {
            return _value.__InvokeMethod(key, v0, v1, v2, v3, v4, v5, v6);
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4,
            SValue v5,
            SValue v6, SValue v7)
        {
            return _value.__InvokeMethod(key, v0, v1, v2, v3, v4, v5, v6, v7);
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4,
            SValue v5,
            SValue v6, SValue v7, SValue v8)
        {
            return _value.__InvokeMethod(key, v0, v1, v2, v3, v4, v5, v6, v7, v8);
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4,
            SValue v5,
            SValue v6, SValue v7, SValue v8, SValue v9)
        {
            return _value.__InvokeMethod(key, v0, v1, v2, v3, v4, v5, v6, v7, v8, v9);
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4,
            SValue v5,
            SValue v6, SValue v7, SValue v8, SValue v9, SValue v10)
        {
            return _value.__InvokeMethod(key, v0, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10);
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4,
            SValue v5,
            SValue v6, SValue v7, SValue v8, SValue v9, SValue v10, SValue v11)
        {
            return _value.__InvokeMethod(key, v0, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11);
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4,
            SValue v5,
            SValue v6, SValue v7, SValue v8, SValue v9, SValue v10, SValue v11, SValue v12)
        {
            return _value.__InvokeMethod(key, v0, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12);
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4,
            SValue v5,
            SValue v6, SValue v7, SValue v8, SValue v9, SValue v10, SValue v11, SValue v12, SValue v13)
        {
            return _value.__InvokeMethod(key, v0, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13);
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4,
            SValue v5,
            SValue v6, SValue v7, SValue v8, SValue v9, SValue v10, SValue v11, SValue v12, SValue v13, SValue v14)
        {
            return _value.__InvokeMethod(key, v0, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14);
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4,
            SValue v5,
            SValue v6, SValue v7, SValue v8, SValue v9, SValue v10, SValue v11, SValue v12, SValue v13, SValue v14,
            SValue v15)
        {
            return _value.__InvokeMethod(key, v0, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15);
        }

        public override bool Operator_Equal_Double(double v2)
        {
            return false;
        }

        public override bool Operator_Equal_String(string v2)
        {
            return false;
        }

        public override bool Operator_Equal_SValue(SValue v2)
        {
            if (v2.dataType == DataType.Object)
            {
                return _value == ((Object_SValue)v2)._value;
            }

            return false;
        }

        public override bool Operator_Not_Equal_Double(double v2)
        {
            return true;
        }

        public override bool Operator_Not_Equal_String(string v2)
        {
            return true;
        }

        public override bool Operator_Not_Equal_SValue(SValue v2)
        {
            if (v2.dataType == DataType.Object)
            {
                return _value != ((Object_SValue)v2)._value;
            }

            return true;
        }

        public override SValue Operator_Add_Double(double v2)
        {
            return new String_SValue("[object]" + v2);
        }

        public override SValue Operator_Append_Add_Double(double v1)
        {
            return new String_SValue(v1 + "[object]");
        }

        public override SValue Operator_Add_String(string v2)
        {
            return new String_SValue("[object]" + v2);
        }

        public override SValue Operator_Append_Add_String(string v1)
        {
            return new String_SValue(v1 + "[object]");
        }

        public override SValue Operator_Add_SValue(SValue v2)
        {
            switch (v2.dataType)
            {
                case DataType.String:
                    return new String_SValue("[object]" + ((String_SValue)v2)._value);
                case DataType.Null:
                    return new String_SValue("[object]null");
                case DataType.True:
                    return new String_SValue("[object]true");
                case DataType.False:
                    return new String_SValue("[object]false");
                case DataType.Object:
                    return new String_SValue("[object][object]");
                case DataType.Function_0:
                case DataType.Function_1:
                case DataType.Function_2:
                case DataType.Function_3:
                case DataType.Function_4:
                case DataType.Function_5:
                case DataType.Function_6:
                case DataType.Function_7:
                case DataType.Function_8:
                case DataType.Function_9:
                case DataType.Function_10:
                case DataType.Function_11:
                case DataType.Function_12:
                case DataType.Function_13:
                case DataType.Function_14:
                case DataType.Function_15:
                case DataType.Function_16:
                    return new String_SValue("[object][function]");
            }

            return new String_SValue("[object]" + v2.GetValue());
        }

        public override SValue Operator_Subtract_Double(double v2)
        {
            return NaN;
        }

        public override SValue Operator_Append_Subtract_Double(double v1)
        {
            return NaN;
        }

        public override SValue Operator_Subtract_SValue(SValue v2)
        {
            return NaN;
        }

        public override SValue Operator_Multiply_Double(double v2)
        {
            return NaN;
        }

        public override SValue Operator_Append_Multiply_Double(double v1)
        {
            return NaN;
        }

        public override SValue Operator_Multiply_SValue(SValue v2)
        {
            return NaN;
        }

        public override SValue Operator_Divide_Double(double v2)
        {
            return NaN;
        }

        public override SValue Operator_Append_Divide_Double(double v1)
        {
            return NaN;
        }

        public override SValue Operator_Divide_SValue(SValue v2)
        {
            return NaN;
        }

        public override SValue Operator_SinceAdd()
        {
            return NaN;
        }

        public override SValue Operator_SinceReduction()
        {
            return NaN;
        }

        public override bool Operator_Greater_Double(double v2)
        {
            return false;
        }

        public override bool Operator_Less_Double(double v2)
        {
            return false;
        }

        public override bool Operator_Greater_SValue(SValue v2)
        {
            return false;
        }

        public override bool Operator_Less_SValue(SValue v2)
        {
            return false;
        }

        public override bool Operator_Greater_Equal_Double(double v2)
        {
            return false;
        }

        public override bool Operator_Less_Equal_Double(double v2)
        {
            return false;
        }

        public override bool Operator_Greater_Equal_SValue(SValue v2)
        {
            return false;
        }

        public override SValue Operator_Positive()
        {
            return NaN;
        }

        public override SValue Operator_Negative()
        {
            return NaN;
        }

        public override bool Operator_Not()
        {
            return false;
        }

        public override bool Operator_Less_Equal_SValue(SValue v2)
        {
            return false;
        }

        public override bool Operator_True()
        {
            return true;
        }

        public override bool Operator_False()
        {
            return false;
        }

        public override SValue Operator_Modulus_Double(double v2)
        {
            return NaN;
        }

        public override SValue Operator_Append_Modulus_Double(double v1)
        {
            return NaN;
        }

        public override SValue Operator_Modulus_SValue(SValue v2)
        {
            return NaN;
        }

        public override SValue Operator_Shift_Negation()
        {
            return NegativeOne;
        }

        public override SValue Operator_Shift_Right(int v1)
        {
            return Zero;
        }

        public override SValue Operator_Shift_Left(int v1)
        {
            return Zero;
        }

        public override SValue Operator_Shift_Or_Double(double v2)
        {
            return new Number_SValue((int)v2);
        }

        public override SValue Operator_Append_Shift_Or_Double(double v1)
        {
            return new Number_SValue((int)v1);
        }

        public override SValue Operator_Shift_Or_SValue(SValue v2)
        {
            switch (v2.dataType)
            {
                case DataType.Number:
                    return new Number_SValue((int)((Number_SValue)v2)._value);
                case DataType.True:
                    return One;
            }

            return Zero;
        }

        public override SValue Operator_Shift_And_Double(double v2)
        {
            return Zero;
        }

        public override SValue Operator_Append_Shift_And_Double(double v1)
        {
            return Zero;
        }

        public override SValue Operator_Shift_And_SValue(SValue v2)
        {
            return Zero;
        }

        public override SValue Operator_Shift_Xor_Double(double v2)
        {
            return new Number_SValue((int)v2);
        }

        public override SValue Operator_Append_Shift_Xor_Double(double v1)
        {
            return new Number_SValue((int)v1);
        }

        public override SValue Operator_Shift_Xor_SValue(SValue v2)
        {
            switch (v2.dataType)
            {
                case DataType.Number:
                    return new Number_SValue((int)((Number_SValue)v2)._value);
                case DataType.True:
                    return One;
            }

            return Zero;
        }
    }
}
