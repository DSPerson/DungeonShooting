
using System.Text;

namespace DScript.Runtime
{
    /// <summary>
    /// 字符串
    /// </summary>
    public class String_SValue : SValue
    {
        internal static readonly String_SValue _false = new String_SValue("false");
        internal static readonly String_SValue _true = new String_SValue("true");
        internal static readonly String_SValue _nullFunction = new String_SValue("null[function]");
        internal static readonly String_SValue _falseFunction = new String_SValue("false[function]");
        internal static readonly String_SValue _trueFunction = new String_SValue("true[function]");
        internal static readonly String_SValue _functionNull = new String_SValue("[function]null");
        internal static readonly String_SValue _functionTrue = new String_SValue("[function]true");
        internal static readonly String_SValue _functionFalse = new String_SValue("[function]false");
        internal static readonly String_SValue _functionFunction = new String_SValue("[function][function]");
        
        public readonly string Value;

        public String_SValue(string value)
        {
            Value = value;
            dataType = DataType.String;
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
            return false;
        }

        public override bool Operator_Equal_String(string v2)
        {
            return Value == v2;
        }

        public override bool Operator_Equal_SValue(SValue v2)
        {
            if (v2.dataType == DataType.String)
            {
                return Value == ((String_SValue)v2).Value;
            }

            return false;
        }

        public override bool Operator_Not_Equal_Double(double v2)
        {
            return true;
        }

        public override bool Operator_Not_Equal_String(string v2)
        {
            return Value != v2;
        }

        public override bool Operator_Not_Equal_SValue(SValue v2)
        {
            if (v2.dataType == DataType.String)
            {
                return Value != ((String_SValue)v2).Value;
            }

            return true;
        }

        public override SValue Operator_Add_Double(double v2)
        {
            return new String_SValue(Value + v2);
        }

        public override SValue Operator_Append_Add_Double(double v1)
        {
            return new String_SValue(v1 + Value);
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
                case DataType.String:
                    return new String_SValue(Value + ((String_SValue)v2).Value);
                case DataType.Null:
                    return new String_SValue(Value + "null");
                case DataType.True:
                    return new String_SValue(Value + "true");
                case DataType.False:
                    return new String_SValue(Value + "false");
                case DataType.Object:
                    return new String_SValue(Value + ((Object_SValue)v2).Value);
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

            return new String_SValue(Value + v2.GetValue());
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
            return Value.Length == 0;
        }

        public override bool Operator_Less_Equal_SValue(SValue v2)
        {
            return false;
        }

        public override bool Operator_True()
        {
            return Value.Length > 0;
        }

        public override bool Operator_False()
        {
            return Value.Length == 0;
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
                    return new Number_SValue((int)((Number_SValue)v2).Value);
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
                    return new Number_SValue((int)((Number_SValue)v2).Value);
                case DataType.True:
                    return One;
            }

            return Zero;
        }
    }
}