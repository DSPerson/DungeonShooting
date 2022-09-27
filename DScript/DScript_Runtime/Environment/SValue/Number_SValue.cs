﻿
namespace DScript.Runtime
{
    /// <summary>
    /// 数字类型
    /// </summary>
    public class Number_SValue : SValue
    {
        public readonly double Value;

        public Number_SValue(double value)
        {
            Value = value;
            dataType = DataType.Number;
        }

        public override object GetValue()
        {
            return Value;
        }

        public override SValue GetMember(string key)
        {
            return Null;
        }

        public override bool HasMember(string key)
        {
            return false;
        }

        public override void SetMember(string key, SValue value)
        {
            throw new OperationMemberException($"Member '{key}' not defined.");
        }

        public override SValue Invoke()
        {
            throw new InvokeMethodException($"'{Value}' is not a function.");
        }

        public override SValue Invoke(SValue v0)
        {
            throw new InvokeMethodException($"'{Value}' is not a function.");
        }

        public override SValue Invoke(SValue v0, SValue v1)
        {
            throw new InvokeMethodException($"'{Value}' is not a function.");
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2)
        {
            throw new InvokeMethodException($"'{Value}' is not a function.");
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3)
        {
            throw new InvokeMethodException($"'{Value}' is not a function.");
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4)
        {
            throw new InvokeMethodException($"'{Value}' is not a function.");
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5)
        {
            throw new InvokeMethodException($"'{Value}' is not a function.");
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6)
        {
            throw new InvokeMethodException($"'{Value}' is not a function.");
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
            SValue v7)
        {
            throw new InvokeMethodException($"'{Value}' is not a function.");
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
            SValue v7, SValue v8)
        {
            throw new InvokeMethodException($"'{Value}' is not a function.");
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
            SValue v7, SValue v8,
            SValue v9)
        {
            throw new InvokeMethodException($"'{Value}' is not a function.");
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
            SValue v7, SValue v8,
            SValue v9, SValue v10)
        {
            throw new InvokeMethodException($"'{Value}' is not a function.");
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
            SValue v7, SValue v8,
            SValue v9, SValue v10, SValue v11)
        {
            throw new InvokeMethodException($"'{Value}' is not a function.");
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
            SValue v7, SValue v8,
            SValue v9, SValue v10, SValue v11, SValue v12)
        {
            throw new InvokeMethodException($"'{Value}' is not a function.");
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
            SValue v7, SValue v8,
            SValue v9, SValue v10, SValue v11, SValue v12, SValue v13)
        {
            throw new InvokeMethodException($"'{Value}' is not a function.");
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
            SValue v7, SValue v8,
            SValue v9, SValue v10, SValue v11, SValue v12, SValue v13, SValue v14)
        {
            throw new InvokeMethodException($"'{Value}' is not a function.");
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
            SValue v7, SValue v8,
            SValue v9, SValue v10, SValue v11, SValue v12, SValue v13, SValue v14, SValue v15)
        {
            throw new InvokeMethodException($"'{Value}' is not a function.");
        }

        public override SValue InvokeMethod(string key)
        {
            throw new InvokeMethodException($"The member function '{Value}.{key}' was not found.");
        }

        public override SValue InvokeMethod(string key, SValue v0)
        {
            throw new InvokeMethodException($"The member function '{Value}.{key}' was not found.");
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1)
        {
            throw new InvokeMethodException($"The member function '{Value}.{key}' was not found.");
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2)
        {
            throw new InvokeMethodException($"The member function '{Value}.{key}' was not found.");
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3)
        {
            throw new InvokeMethodException($"The member function '{Value}.{key}' was not found.");
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4)
        {
            throw new InvokeMethodException($"The member function '{Value}.{key}' was not found.");
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5)
        {
            throw new InvokeMethodException($"The member function '{Value}.{key}' was not found.");
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
            SValue v6)
        {
            throw new InvokeMethodException($"The member function '{Value}.{key}' was not found.");
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
            SValue v6, SValue v7)
        {
            throw new InvokeMethodException($"The member function '{Value}.{key}' was not found.");
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
            SValue v6, SValue v7,
            SValue v8)
        {
            throw new InvokeMethodException($"The member function '{Value}.{key}' was not found.");
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
            SValue v6, SValue v7,
            SValue v8, SValue v9)
        {
            throw new InvokeMethodException($"The member function '{Value}.{key}' was not found.");
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
            SValue v6, SValue v7,
            SValue v8, SValue v9, SValue v10)
        {
            throw new InvokeMethodException($"The member function '{Value}.{key}' was not found.");
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
            SValue v6, SValue v7,
            SValue v8, SValue v9, SValue v10, SValue v11)
        {
            throw new InvokeMethodException($"The member function '{Value}.{key}' was not found.");
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
            SValue v6, SValue v7,
            SValue v8, SValue v9, SValue v10, SValue v11, SValue v12)
        {
            throw new InvokeMethodException($"The member function '{Value}.{key}' was not found.");
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
            SValue v6, SValue v7,
            SValue v8, SValue v9, SValue v10, SValue v11, SValue v12, SValue v13)
        {
            throw new InvokeMethodException($"The member function '{Value}.{key}' was not found.");
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
            SValue v6, SValue v7,
            SValue v8, SValue v9, SValue v10, SValue v11, SValue v12, SValue v13, SValue v14)
        {
            throw new InvokeMethodException($"The member function '{Value}.{key}' was not found.");
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
            SValue v6, SValue v7,
            SValue v8, SValue v9, SValue v10, SValue v11, SValue v12, SValue v13, SValue v14, SValue v15)
        {
            throw new InvokeMethodException($"The member function '{Value}.{key}' was not found.");
        }

        public override bool Operator_Equal_Double(double v2)
        {
            return Value == v2;
        }

        public override bool Operator_Equal_String(string v2)
        {
            return false;
        }

        public override bool Operator_Equal_SValue(SValue v2)
        {
            switch (v2.dataType)
            {
                case DataType.Number:
                    return Value == ((Number_SValue)v2).Value;
                case DataType.True:
                    return Value == 1;
                case DataType.False:
                    return Value == 0;
            }

            return false;
        }

        public override bool Operator_Not_Equal_Double(double v2)
        {
            return Value != v2;
        }

        public override bool Operator_Not_Equal_String(string v2)
        {
            return true;
        }

        public override bool Operator_Not_Equal_SValue(SValue v2)
        {
            switch (v2.dataType)
            {
                case DataType.Number:
                    return Value != ((Number_SValue)v2).Value;
                case DataType.True:
                    return Value != 1;
                case DataType.False:
                    return Value != 0;
            }

            return true;
        }

        public override SValue Operator_Add_Double(double v2)
        {
            return new Number_SValue(Value + v2);
        }

        public override SValue Operator_Append_Add_Double(double v1)
        {
            return new Number_SValue(v1 + Value);
        }

        public override SValue Operator_Add_String(string v2)
        {
            return new String_SValue(Value + v2);
        }

        public override SValue Operator_Append_Add_String(string v1)
        {
            return new String_SValue(v1 + Value);
        }

        public override SValue Operator_Add_SValue(SValue v2)
        {
            switch (v2.dataType)
            {
                case DataType.Number:
                    return new Number_SValue(Value + ((Number_SValue)v2).Value);
                case DataType.String:
                    return new String_SValue(Value + ((String_SValue)v2).Value);
                case DataType.True:
                    return new Number_SValue(Value + 1);
                case DataType.False:
                case DataType.Null:
                    return this;
                case DataType.Object:
                    return new String_SValue(Value + ((Object_SValue)v2).Value.ToString());
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
                    return new String_SValue(Value + "[function]");
            }

            return new String_SValue(Value + v2.GetValue().ToString());
        }

        public override SValue Operator_Subtract_Double(double v2)
        {
            return new Number_SValue(Value - v2);
        }

        public override SValue Operator_Append_Subtract_Double(double v1)
        {
            return new Number_SValue(v1 - Value);
        }

        public override SValue Operator_Subtract_SValue(SValue v2)
        {
            switch (v2.dataType)
            {
                case DataType.Number:
                    return new Number_SValue(Value - ((Number_SValue)v2).Value);
                case DataType.True:
                    return new Number_SValue(Value - 1);
                case DataType.False:
                case DataType.Null:
                    return this;
            }

            return NaN;
        }

        public override SValue Operator_Multiply_Double(double v2)
        {
            return new Number_SValue(Value * v2);
        }

        public override SValue Operator_Append_Multiply_Double(double v1)
        {
            return new Number_SValue(v1 * Value);
        }

        public override SValue Operator_Multiply_SValue(SValue v2)
        {
            switch (v2.dataType)
            {
                case DataType.Number:
                    return new Number_SValue(Value * ((Number_SValue)v2).Value);
                case DataType.True:
                    return this;
                case DataType.False:
                case DataType.Null:
                    return Zero;
            }

            return NaN;
        }

        public override SValue Operator_Divide_Double(double v2)
        {
            return new Number_SValue(Value / v2);
        }

        public override SValue Operator_Append_Divide_Double(double v1)
        {
            return new Number_SValue(v1 / Value);
        }

        public override SValue Operator_Divide_SValue(SValue v2)
        {
            switch (v2.dataType)
            {
                case DataType.Number:
                    return new Number_SValue(Value / ((Number_SValue)v2).Value);
                case DataType.True:
                    return this;
                case DataType.False:
                case DataType.Null:
                    return Value == 0 ? NaN : (Value > 0 ? PositiveInfinity : NegativeInfinity);
            }

            return NaN;
        }

        public override SValue Operator_SinceAdd()
        {
            return new Number_SValue(Value + 1);
        }

        public override SValue Operator_SinceReduction()
        {
            return new Number_SValue(Value - 1);
        }

        public override bool Operator_Greater_Double(double v2)
        {
            return Value > v2;
        }

        public override bool Operator_Less_Double(double v2)
        {
            return Value < v2;
        }

        public override bool Operator_Greater_SValue(SValue v2)
        {
            switch (v2.dataType)
            {
                case DataType.Number:
                    return Value > ((Number_SValue)v2).Value;
                case DataType.True:
                    return Value > 1;
                case DataType.False:
                case DataType.Null:
                    return Value > 0;
            }

            return false;
        }

        public override bool Operator_Less_SValue(SValue v2)
        {
            switch (v2.dataType)
            {
                case DataType.Number:
                    return Value < ((Number_SValue)v2).Value;
                case DataType.True:
                    return Value < 1;
                case DataType.False:
                case DataType.Null:
                    return Value < 0;
            }

            return false;
        }

        public override bool Operator_Greater_Equal_Double(double v2)
        {
            return Value >= v2;
        }

        public override bool Operator_Less_Equal_Double(double v2)
        {
            return Value <= v2;
        }

        public override bool Operator_Greater_Equal_SValue(SValue v2)
        {
            switch (v2.dataType)
            {
                case DataType.Number:
                    return Value >= ((Number_SValue)v2).Value;
                case DataType.True:
                    return Value >= 1;
                case DataType.False:
                case DataType.Null:
                    return Value >= 0;
            }

            return false;
        }

        public override SValue Operator_Positive()
        {
            return this;
        }

        public override bool Operator_Less_Equal_SValue(SValue v2)
        {
            switch (v2.dataType)
            {
                case DataType.Number:
                    return Value <= ((Number_SValue)v2).Value;
                case DataType.True:
                    return Value <= 1;
                case DataType.False:
                case DataType.Null:
                    return Value <= 0;
            }

            return false;
        }

        public override SValue Operator_Negative()
        {
            return new Number_SValue(-Value);
        }

        public override bool Operator_Not()
        {
            return Value <= 0;
        }

        public override bool Operator_True()
        {
            return Value > 0;
        }

        public override bool Operator_False()
        {
            return Value <= 0;
        }

        public override SValue Operator_Modulus_Double(double v2)
        {
            return new Number_SValue(Value % v2);
        }

        public override SValue Operator_Append_Modulus_Double(double v1)
        {
            return new Number_SValue(v1 % Value);
        }

        public override SValue Operator_Modulus_SValue(SValue v2)
        {
            switch (v2.dataType)
            {
                case DataType.Number:
                    return new Number_SValue(Value % ((Number_SValue)v2).Value);
                case DataType.True:
                    return new Number_SValue(Value % 1);
            }

            return NaN;
        }

        public override SValue Operator_Shift_Negation()
        {
            return new Number_SValue(~(int)Value);
        }

        public override SValue Operator_Shift_Right(int v1)
        {
            return new Number_SValue((int)Value >> v1);
        }

        public override SValue Operator_Shift_Left(int v1)
        {
            return new Number_SValue((int)Value << v1);
        }

        public override SValue Operator_Shift_Or_Double(double v2)
        {
            return new Number_SValue((int)Value | (int)v2);
        }

        public override SValue Operator_Append_Shift_Or_Double(double v1)
        {
            return new Number_SValue((int)v1 | (int)Value);
        }

        public override SValue Operator_Shift_Or_SValue(SValue v2)
        {
            switch (v2.dataType)
            {
                case DataType.Number:
                    return new Number_SValue((int)Value | (int)((Number_SValue)v2).Value);
                case DataType.True:
                    return new Number_SValue((int)Value | 1);
            }

            return new Number_SValue((int)Value | 0);
        }

        public override SValue Operator_Shift_And_Double(double v2)
        {
            return new Number_SValue((int)Value & (int)v2);
        }

        public override SValue Operator_Append_Shift_And_Double(double v1)
        {
            return new Number_SValue((int)v1 & (int)Value);
        }

        public override SValue Operator_Shift_And_SValue(SValue v2)
        {
            switch (v2.dataType)
            {
                case DataType.Number:
                    return new Number_SValue((int)Value & (int)((Number_SValue)v2).Value);
                case DataType.True:
                    return new Number_SValue((int)Value & 1);
            }

            return new Number_SValue((int)Value & 0);
        }

        public override SValue Operator_Shift_Xor_Double(double v2)
        {
            return new Number_SValue((int)Value ^ (int)v2);
        }

        public override SValue Operator_Append_Shift_Xor_Double(double v1)
        {
            return new Number_SValue((int)v1 ^ (int)Value);
        }

        public override SValue Operator_Shift_Xor_SValue(SValue v2)
        {
            switch (v2.dataType)
            {
                case DataType.Number:
                    return new Number_SValue((int)Value ^ (int)((Number_SValue)v2).Value);
                case DataType.True:
                    return new Number_SValue((int)Value ^ 1);
            }

            return new Number_SValue((int)Value ^ 0);
        }
    }
}