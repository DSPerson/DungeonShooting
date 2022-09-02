﻿
public struct SValue
{

    #region 定义函数委托

    public delegate SValue Function_0();

    public delegate SValue Function_1(SValue p0);

    public delegate SValue Function_2(SValue p0, SValue p1);

    public delegate SValue Function_3(SValue p0, SValue p1, SValue p2);

    public delegate SValue Function_4(SValue p0, SValue p1, SValue p2, SValue p3);

    public delegate SValue Function_5(SValue p0, SValue p1, SValue p2, SValue p3, SValue p4);

    public delegate SValue Function_6(SValue p0, SValue p1, SValue p2, SValue p3, SValue p4, SValue p5);

    public delegate SValue Function_7(SValue p0, SValue p1, SValue p2, SValue p3, SValue p4, SValue p5, SValue p6);

    public delegate SValue Function_8(SValue p0, SValue p1, SValue p2, SValue p3, SValue p4, SValue p5, SValue p6,
        SValue p7);

    public delegate SValue Function_9(SValue p0, SValue p1, SValue p2, SValue p3, SValue p4, SValue p5, SValue p6,
        SValue p7, SValue p8);

    public delegate SValue Function_10(SValue p0, SValue p1, SValue p2, SValue p3, SValue p4, SValue p5, SValue p6,
        SValue p7, SValue p8, SValue p9);

    public delegate SValue Function_11(SValue p0, SValue p1, SValue p2, SValue p3, SValue p4, SValue p5, SValue p6,
        SValue p7, SValue p8, SValue p9, SValue p10);

    public delegate SValue Function_12(SValue p0, SValue p1, SValue p2, SValue p3, SValue p4, SValue p5, SValue p6,
        SValue p7, SValue p8, SValue p9, SValue p10, SValue p11);

    public delegate SValue Function_13(SValue p0, SValue p1, SValue p2, SValue p3, SValue p4, SValue p5, SValue p6,
        SValue p7, SValue p8, SValue p9, SValue p10, SValue p11, SValue p12);

    public delegate SValue Function_14(SValue p0, SValue p1, SValue p2, SValue p3, SValue p4, SValue p5, SValue p6,
        SValue p7, SValue p8, SValue p9, SValue p10, SValue p11, SValue p12, SValue p13);

    public delegate SValue Function_15(SValue p0, SValue p1, SValue p2, SValue p3, SValue p4, SValue p5, SValue p6,
        SValue p7, SValue p8, SValue p9, SValue p10, SValue p11, SValue p12, SValue p13, SValue p14);

    public delegate SValue Function_16(SValue p0, SValue p1, SValue p2, SValue p3, SValue p4, SValue p5, SValue p6,
        SValue p7, SValue p8, SValue p9, SValue p10, SValue p11, SValue p12, SValue p13, SValue p14, SValue p15);

    public delegate SValue Function_0_Params(params SValue[] ps);

    #endregion

    public enum SValueType
    {
        /// <summary>
        /// 空类型
        /// </summary>
        Null,

        /// <summary>
        /// 数字类型
        /// </summary>
        Number,

        /// <summary>
        /// 值为 true
        /// </summary>
        BooleanTrue,

        /// <summary>
        /// 值为 false
        /// </summary>
        BooleanFalse,

        /// <summary>
        /// 字符串类型
        /// </summary>
        String,

        /// <summary>
        /// 函数类型
        /// </summary>
        Function,

        /// <summary>
        /// 对象类型
        /// </summary>
        Object,
    }

    private enum SObjectType
    {
        /// <summary>
        /// 类
        /// </summary>
        Class,

        /// <summary>
        /// 字典
        /// </summary>
        Map,

        /// <summary>
        /// 数组
        /// </summary>
        Array,

        /// <summary>
        /// 参数数量为0的函数
        /// </summary>
        Function_0,

        /// <summary>
        /// 参数数量为1的函数
        /// </summary>
        Function_1,

        /// <summary>
        /// 参数数量为2的函数
        /// </summary>
        Function_2,

        /// <summary>
        /// 参数数量为3的函数
        /// </summary>
        Function_3,

        /// <summary>
        /// 参数数量为4的函数
        /// </summary>
        Function_4,

        /// <summary>
        /// 参数数量为5的函数
        /// </summary>
        Function_5,

        /// <summary>
        /// 参数数量为6的函数
        /// </summary>
        Function_6,

        /// <summary>
        /// 参数数量为7的函数
        /// </summary>
        Function_7,

        /// <summary>
        /// 参数数量为8的函数
        /// </summary>
        Function_8,

        /// <summary>
        /// 参数数量为9的函数
        /// </summary>
        Function_9,

        /// <summary>
        /// 参数数量为10的函数
        /// </summary>
        Function_10,

        /// <summary>
        /// 参数数量为11的函数
        /// </summary>
        Function_11,

        /// <summary>
        /// 参数数量为12的函数
        /// </summary>
        Function_12,

        /// <summary>
        /// 参数数量为13的函数
        /// </summary>
        Function_13,

        /// <summary>
        /// 参数数量为14的函数
        /// </summary>
        Function_14,

        /// <summary>
        /// 参数数量为15的函数
        /// </summary>
        Function_15,

        /// <summary>
        /// 参数数量为16的函数
        /// </summary>
        Function_16,

        /// <summary>
        /// 其他类型
        /// </summary>
        Other
    }

    /// <summary>
    /// 当前值的类型
    /// </summary>
    public SValueType Type;

    /// <summary>
    /// 值数据
    /// </summary>
    public object Value;

    /// <summary>
    /// 被包裹实例类型
    /// </summary>
    private SObjectType objectType;

    #region 创建SValue

    public SValue(SValue v)
    {
        Type = v.Type;
        Value = v.Value;
        objectType = v.objectType;
    }

    public SValue(SObject v)
    {
        Type = SValueType.Object;
        Value = v;
        objectType = SObjectType.Class;
    }

    public SValue(SArray v)
    {
        Type = SValueType.Object;
        Value = v;
        objectType = SObjectType.Array;
    }

    public SValue(SMap v)
    {
        Type = SValueType.Object;
        Value = v;
        objectType = SObjectType.Map;
    }

    public SValue(double v)
    {
        Type = SValueType.Number;
        Value = v;
        objectType = SObjectType.Other;
    }

    public SValue(string v)
    {
        Type = SValueType.String;
        Value = v;
        objectType = SObjectType.Other;
    }

    public SValue(Function_0 v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_0;
    }

    public SValue(Function_1 v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_1;
    }

    public SValue(Function_2 v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_2;
    }

    public SValue(Function_3 v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_3;
    }

    public SValue(Function_4 v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_4;
    }

    public SValue(Function_5 v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_5;
    }

    public SValue(Function_6 v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_6;
    }

    public SValue(Function_7 v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_7;
    }

    public SValue(Function_8 v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_8;
    }

    public SValue(Function_9 v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_9;
    }

    public SValue(Function_10 v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_10;
    }

    public SValue(Function_11 v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_11;
    }

    public SValue(Function_12 v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_12;
    }

    public SValue(Function_13 v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_13;
    }

    public SValue(Function_14 v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_14;
    }

    public SValue(Function_15 v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_15;
    }

    public SValue(Function_16 v)
    {
        Type = SValueType.Function;
        Value = v;
        objectType = SObjectType.Function_16;
    }

    public static SValue Null
    {
        get
        {
            if (!_initNullValue)
            {
                _initNullValue = true;
                _null.Type = SValueType.Null;
                _false.objectType = SObjectType.Other;
            }

            return _null;
        }
    }

    private static bool _initNullValue;
    private static SValue _null;

    public static SValue True
    {
        get
        {
            if (!_initTrueValue)
            {
                _initTrueValue = true;
                _true.Type = SValueType.BooleanTrue;
                _false.objectType = SObjectType.Other;
                _true.Value = 1;
            }

            return _true;
        }
    }

    private static bool _initTrueValue;
    private static SValue _true;

    public static SValue False
    {
        get
        {
            if (!_initFalseValue)
            {
                _initFalseValue = true;
                _false.Type = SValueType.BooleanFalse;
                _false.objectType = SObjectType.Other;
                _false.Value = 0;
            }

            return _false;
        }
    }

    private static bool _initFalseValue;
    private static SValue _false;

    #endregion

    #region 属性和方法的操作

    public SValue __GetValue__(string key)
    {
        switch (objectType)
        {
            case SObjectType.Class:
                return ((SObject)Value).__GetValue__(key);
            case SObjectType.Array:
                return ((SArray)Value).__GetValue__(key);
            case SObjectType.Map:
                return ((SMap)Value).__GetValue__(key);
        }

        if (Type == SValueType.Null)
        {
            //空指针异常
        }

        return Null;
    }

    public SValue __GetValue__(double key)
    {
        if (objectType == SObjectType.Array)
        {
            return ((SArray)Value).__GetValue__((int)key);
        }
        else
        {
            //只有array支持number类型索引取值
        }

        return Null;
    }

    public SValue __GetValue__(SValue key)
    {
        return Null;
    }

    public void __SetValue__()
    {

    }

    public SValue __InvokeMethod__(string key, params SValue[] ps)
    {
        switch (objectType)
        {
            case SObjectType.Class:
                return ((SObject)Value).__InvokeMethod__(key, ps);
            case SObjectType.Array:
                return ((SArray)Value).__InvokeMethod__(key, ps);
        }

        //不可执行函数, 报错
        return Null;
    }

    public SValue __Invoke__(params SValue[] ps)
    {
        switch (objectType)
        {
            case SObjectType.Function_0:
                AssertUtils.AssertParamsLength(0, ps.Length);
                return ((Function_0)Value)();
            case SObjectType.Function_1:
                AssertUtils.AssertParamsLength(1, ps.Length);
                return ((Function_1)Value)(ps[0]);
            case SObjectType.Function_2:
                AssertUtils.AssertParamsLength(2, ps.Length);
                return ((Function_2)Value)(ps[0], ps[1]);
            case SObjectType.Function_3:
                AssertUtils.AssertParamsLength(3, ps.Length);
                return ((Function_3)Value)(ps[0], ps[1], ps[2]);
            case SObjectType.Function_4:
                AssertUtils.AssertParamsLength(4, ps.Length);
                return ((Function_4)Value)(ps[0], ps[1], ps[2], ps[3]);
            case SObjectType.Function_5:
                AssertUtils.AssertParamsLength(5, ps.Length);
                return ((Function_5)Value)(ps[0], ps[1], ps[2], ps[3], ps[4]);
            case SObjectType.Function_6:
                AssertUtils.AssertParamsLength(6, ps.Length);
                return ((Function_6)Value)(ps[0], ps[1], ps[2], ps[3], ps[4], ps[5]);
            case SObjectType.Function_7:
                AssertUtils.AssertParamsLength(7, ps.Length);
                return ((Function_7)Value)(ps[0], ps[1], ps[2], ps[3], ps[4], ps[5], ps[6]);
            case SObjectType.Function_8:
                AssertUtils.AssertParamsLength(8, ps.Length);
                return ((Function_8)Value)(ps[0], ps[1], ps[2], ps[3], ps[4], ps[5], ps[6], ps[7]);
            case SObjectType.Function_9:
                AssertUtils.AssertParamsLength(9, ps.Length);
                return ((Function_9)Value)(ps[0], ps[1], ps[2], ps[3], ps[4], ps[5], ps[6], ps[7], ps[8]);
            case SObjectType.Function_10:
                AssertUtils.AssertParamsLength(10, ps.Length);
                return ((Function_10)Value)(ps[0], ps[1], ps[2], ps[3], ps[4], ps[5], ps[6], ps[7], ps[8], ps[9]);
            case SObjectType.Function_11:
                AssertUtils.AssertParamsLength(11, ps.Length);
                return ((Function_11)Value)(ps[0], ps[1], ps[2], ps[3], ps[4], ps[5], ps[6], ps[7], ps[8], ps[9],
                    ps[10]);
            case SObjectType.Function_12:
                AssertUtils.AssertParamsLength(12, ps.Length);
                return ((Function_12)Value)(ps[0], ps[1], ps[2], ps[3], ps[4], ps[5], ps[6], ps[7], ps[8], ps[9],
                    ps[10], ps[11]);
            case SObjectType.Function_13:
                AssertUtils.AssertParamsLength(13, ps.Length);
                return ((Function_13)Value)(ps[0], ps[1], ps[2], ps[3], ps[4], ps[5], ps[6], ps[7], ps[8], ps[9],
                    ps[10], ps[11], ps[12]);
            case SObjectType.Function_14:
                AssertUtils.AssertParamsLength(14, ps.Length);
                return ((Function_14)Value)(ps[0], ps[1], ps[2], ps[3], ps[4], ps[5], ps[6], ps[7], ps[8], ps[9],
                    ps[10], ps[11], ps[12], ps[13]);
            case SObjectType.Function_15:
                AssertUtils.AssertParamsLength(15, ps.Length);
                return ((Function_15)Value)(ps[0], ps[1], ps[2], ps[3], ps[4], ps[5], ps[6], ps[7], ps[8], ps[9],
                    ps[10], ps[11], ps[12], ps[13], ps[14]);
            case SObjectType.Function_16:
                AssertUtils.AssertParamsLength(16, ps.Length);
                return ((Function_16)Value)(ps[0], ps[1], ps[2], ps[3], ps[4], ps[5], ps[6], ps[7], ps[8], ps[9],
                    ps[10], ps[11], ps[12], ps[13], ps[14], ps[15]);
        }

        //不是function, 报错
        return Null;
    }

    #endregion

    #region 自动转型

    public static implicit operator SValue(double value)
    {
        return new SValue(value);
    }

    public static implicit operator SValue(bool value)
    {
        return value ? True : False;
    }

    public static implicit operator SValue(string value)
    {
        return new SValue(value);
    }

    public static implicit operator SValue(SObject value)
    {
        return new SValue(value);
    }

    public static implicit operator SValue(SArray value)
    {
        return new SValue(value);
    }

    public static implicit operator SValue(SMap value)
    {
        return new SValue(value);
    }

    public static implicit operator SValue(Function_0 value)
    {
        return new SValue(value);
    }

    public static implicit operator SValue(Function_1 value)
    {
        return new SValue(value);
    }

    public static implicit operator SValue(Function_2 value)
    {
        return new SValue(value);
    }

    public static implicit operator SValue(Function_3 value)
    {
        return new SValue(value);
    }

    public static implicit operator SValue(Function_4 value)
    {
        return new SValue(value);
    }

    public static implicit operator SValue(Function_5 value)
    {
        return new SValue(value);
    }

    public static implicit operator SValue(Function_6 value)
    {
        return new SValue(value);
    }

    public static implicit operator SValue(Function_7 value)
    {
        return new SValue(value);
    }

    public static implicit operator SValue(Function_8 value)
    {
        return new SValue(value);
    }

    public static implicit operator SValue(Function_9 value)
    {
        return new SValue(value);
    }

    public static implicit operator SValue(Function_10 value)
    {
        return new SValue(value);
    }

    public static implicit operator SValue(Function_11 value)
    {
        return new SValue(value);
    }

    public static implicit operator SValue(Function_12 value)
    {
        return new SValue(value);
    }

    public static implicit operator SValue(Function_13 value)
    {
        return new SValue(value);
    }

    public static implicit operator SValue(Function_14 value)
    {
        return new SValue(value);
    }

    public static implicit operator SValue(Function_15 value)
    {
        return new SValue(value);
    }

    public static implicit operator SValue(Function_16 value)
    {
        return new SValue(value);
    }

    #endregion

    #region 运算符重载

    public static SValue operator +(SValue v1, double num)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                return new SValue((double)v1.Value + num);
            case SValueType.String:
                return new SValue(v1.Value + num.ToString());
            case SValueType.Object:
            case SValueType.Function:
            case SValueType.Null:
                return new SValue(double.NaN);
            case SValueType.BooleanTrue:
                if (1 + num > 0)
                    return True;
                return False;
            case SValueType.BooleanFalse:
                if (num > 0)
                    return True;
                return False;
        }

        return Null;
    }

    public static SValue operator +(double num, SValue v2)
    {
        switch (v2.Type)
        {
            case SValueType.Number:
                return new SValue(num + (double)v2.Value);
            case SValueType.String:
                return new SValue(num.ToString() + v2.Value);
            case SValueType.Object:
            case SValueType.Function:
            case SValueType.Null:
                return new SValue(double.NaN);
            case SValueType.BooleanTrue:
                return new SValue(num + 1);
            case SValueType.BooleanFalse:
                return new SValue(num);
        }

        return Null;
    }

    public static SValue operator +(SValue v1, string v2)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
            case SValueType.Object:
            case SValueType.String:
                return new SValue(v1.Value + v2);
            case SValueType.Null:
                return new SValue("null" + v1.Value);
            case SValueType.Function:
                return new SValue("[function]" + v1.Value);
            case SValueType.BooleanTrue:
                return new SValue("true" + v1.Value);
            case SValueType.BooleanFalse:
                return new SValue("false" + v1.Value);
        }

        return Null;
    }

    public static SValue operator +(string v1, SValue v2)
    {
        switch (v2.Type)
        {
            case SValueType.Number:
            case SValueType.Object:
            case SValueType.String:
                return new SValue(v1 + v2.Value);
            case SValueType.Null:
                return new SValue(v1 + "null");
            case SValueType.Function:
                return new SValue(v1 + "[function]");
            case SValueType.BooleanTrue:
                return new SValue(v1 + "true");
            case SValueType.BooleanFalse:
                return new SValue(v1 + "false");
        }

        return Null;
    }

    public static SValue operator +(SValue v1, SValue v2)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return new SValue((double)v1.Value + (double)v2.Value);
                    case SValueType.String:
                        return new SValue(v1.Value.ToString() + v2.Value);
                    case SValueType.Object:
                    case SValueType.Function:
                    case SValueType.Null:
                        return new SValue(double.NaN);
                    case SValueType.BooleanTrue:
                        return new SValue((double)v1.Value + 1);
                    case SValueType.BooleanFalse:
                        return new SValue(v1);
                }

                break;
            case SValueType.String:
                switch (v2.Type)
                {
                    case SValueType.Number:
                    case SValueType.Object:
                        return new SValue(v1.Value + v2.Value.ToString());
                    case SValueType.String:
                        return new SValue(v1.Value + (string)v2.Value);
                    case SValueType.Null:
                        return new SValue(v1.Value + "null");
                    case SValueType.Function:
                        return new SValue(v1.Value + "[function]");
                    case SValueType.BooleanTrue:
                        return new SValue(v1.Value + "true");
                    case SValueType.BooleanFalse:
                        return new SValue(v1.Value + "false");
                }

                break;
            case SValueType.Null:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return new SValue(double.NaN);
                    case SValueType.Object:
                    case SValueType.String:
                        return new SValue("null" + v2.Value);
                    case SValueType.Null:
                        return Null;
                    case SValueType.BooleanTrue:
                        return new SValue("nulltrue");
                    case SValueType.BooleanFalse:
                        return new SValue("nullfalse");
                    case SValueType.Function:
                        return new SValue("null[function]");
                }

                break;
            case SValueType.Object:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return new SValue(double.NaN);
                    case SValueType.Object:
                        return new SValue(v1.Value.ToString() + v2.Value);
                    case SValueType.String:
                        return new SValue(v1.Value + (string)v2.Value);
                    case SValueType.Null:
                        return new SValue("null" + v2.Value);
                    case SValueType.BooleanTrue:
                        return new SValue("true" + v2.Value);
                    case SValueType.BooleanFalse:
                        return new SValue("false" + v2.Value);
                    case SValueType.Function:
                        return new SValue("[function]" + v2.Value);
                }

                break;
            case SValueType.BooleanTrue:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return new SValue((double)v1.Value + 1);
                    case SValueType.Object:
                    case SValueType.String:
                        return new SValue("true" + v2.Value);
                    case SValueType.Function:
                        return new SValue("true[function]");
                    case SValueType.Null:
                        return new SValue("truenull");
                    case SValueType.BooleanTrue:
                    case SValueType.BooleanFalse:
                        return True;
                }

                break;
            case SValueType.BooleanFalse:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return new SValue(v1);
                    case SValueType.Object:
                    case SValueType.String:
                        return new SValue("false" + v2.Value);
                    case SValueType.Function:
                        return new SValue("false[function]");
                    case SValueType.Null:
                        return new SValue("falsenull");
                    case SValueType.BooleanTrue:
                        return True;
                    case SValueType.BooleanFalse:
                        return False;
                }

                break;
            case SValueType.Function:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return new SValue(double.NaN);
                    case SValueType.Object:
                    case SValueType.String:
                        return new SValue("[function]" + v2.Value);
                    case SValueType.Function:
                        return new SValue("[function][function]");
                    case SValueType.Null:
                        return new SValue("[function]null");
                    case SValueType.BooleanTrue:
                        return new SValue("[function]true");
                    case SValueType.BooleanFalse:
                        return new SValue("[function]false");
                }

                break;
        }

        return Null;
    }

    public static SValue operator -(SValue v1, double num)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                return new SValue((double)v1.Value - num);
            case SValueType.String:
            case SValueType.Object:
            case SValueType.Function:
            case SValueType.Null:
                return new SValue(double.NaN);
            case SValueType.BooleanTrue:
                if (1 - num > 0)
                    return True;
                return False;
            case SValueType.BooleanFalse:
                if (num <= 0)
                    return True;
                return False;
        }

        return Null;
    }

    public static SValue operator -(double num, SValue v2)
    {
        switch (v2.Type)
        {
            case SValueType.Number:
                return new SValue(num - (double)v2.Value);
            case SValueType.String:
            case SValueType.Object:
            case SValueType.Function:
            case SValueType.Null:
                return new SValue(double.NaN);
            case SValueType.BooleanTrue:
                return new SValue(num - 1);
            case SValueType.BooleanFalse:
                return new SValue(num);
        }

        return Null;
    }

    public static SValue operator -(SValue v1, SValue v2)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return new SValue((double)v1.Value - (double)v2.Value);
                    case SValueType.String:
                    case SValueType.Object:
                    case SValueType.Function:
                    case SValueType.Null:
                        return new SValue(double.NaN);
                    case SValueType.BooleanTrue:
                        return new SValue((double)v1.Value - 1);
                    case SValueType.BooleanFalse:
                        return new SValue(v1);
                }

                break;
            case SValueType.String:
                return new SValue(double.NaN);
            case SValueType.Null:
                return new SValue(double.NaN);
            case SValueType.Object:
                return new SValue(double.NaN);
            case SValueType.Function:
                return new SValue(double.NaN);
            case SValueType.BooleanTrue:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        if (1 - (double)v2.Value > 0)
                            return True;
                        return False;
                    case SValueType.Object:
                    case SValueType.String:
                    case SValueType.Function:
                    case SValueType.BooleanTrue:
                        return False;
                    case SValueType.Null:
                    case SValueType.BooleanFalse:
                        return True;
                }

                break;
            case SValueType.BooleanFalse:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        if ((double)v2.Value <= 0)
                            return True;
                        return False;
                    case SValueType.Object:
                    case SValueType.String:
                    case SValueType.Null:
                    case SValueType.Function:
                    case SValueType.BooleanTrue:
                    case SValueType.BooleanFalse:
                        return False;
                }

                break;
        }

        return Null;
    }

    public static SValue operator *(SValue v1, double num)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                return new SValue((double)v1.Value * num);
            case SValueType.String:
            case SValueType.Object:
            case SValueType.Function:
            case SValueType.Null:
                return new SValue(double.NaN);
            case SValueType.BooleanTrue:
                if (num > 0)
                    return True;
                return False;
            case SValueType.BooleanFalse:
                return False;
        }

        return Null;
    }

    public static SValue operator *(double num, SValue v2)
    {
        switch (v2.Type)
        {
            case SValueType.Number:
                return new SValue(num * (double)v2.Value);
            case SValueType.String:
            case SValueType.Object:
            case SValueType.Function:
            case SValueType.Null:
                return new SValue(double.NaN);
            case SValueType.BooleanTrue:
                return new SValue(num);
            case SValueType.BooleanFalse:
                return new SValue(0);
        }

        return Null;
    }

    public static SValue operator *(SValue v1, SValue v2)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return new SValue((double)v1.Value * (double)v2.Value);
                    case SValueType.String:
                    case SValueType.Object:
                    case SValueType.Function:
                    case SValueType.Null:
                        return new SValue(double.NaN);
                    case SValueType.BooleanTrue:
                        return new SValue(v1);
                    case SValueType.BooleanFalse:
                        return new SValue(0);
                }

                break;
            case SValueType.String:
                return new SValue(double.NaN);
            case SValueType.Null:
                return new SValue(double.NaN);
            case SValueType.Object:
                return new SValue(double.NaN);
            case SValueType.Function:
                return new SValue(double.NaN);
            case SValueType.BooleanTrue:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        if ((double)v2.Value > 0)
                            return True;
                        return False;
                    case SValueType.Object:
                    case SValueType.String:
                    case SValueType.Function:
                    case SValueType.BooleanTrue:
                        return True;
                    case SValueType.Null:
                    case SValueType.BooleanFalse:
                        return False;
                }

                break;
            case SValueType.BooleanFalse:
                return False;
        }

        return Null;
    }

    public static SValue operator /(SValue v1, double num)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                return new SValue((double)v1.Value / num);
            case SValueType.String:
            case SValueType.Object:
            case SValueType.Function:
            case SValueType.Null:
                return new SValue(double.NaN);
            case SValueType.BooleanTrue:
                if (num > 0)
                    return True;
                return False;
            case SValueType.BooleanFalse:
                return False;
        }

        return Null;
    }

    public static SValue operator /(double num, SValue v2)
    {
        switch (v2.Type)
        {
            case SValueType.Number:
                return new SValue(num / (double)v2.Value);
            case SValueType.String:
            case SValueType.Object:
            case SValueType.Function:
            case SValueType.Null:
                return new SValue(double.NaN);
            case SValueType.BooleanTrue:
                return new SValue(num);
            case SValueType.BooleanFalse:
                return new SValue(num / 0);
        }

        return Null;
    }

    public static SValue operator /(SValue v1, SValue v2)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return new SValue((double)v1.Value / (double)v2.Value);
                    case SValueType.String:
                    case SValueType.Object:
                    case SValueType.Function:
                    case SValueType.Null:
                    case SValueType.BooleanFalse:
                        return new SValue(double.NaN);
                    case SValueType.BooleanTrue:
                        return new SValue(v1);
                }

                break;
            case SValueType.String:
                return new SValue(double.NaN);
            case SValueType.Null:
                return new SValue(double.NaN);
            case SValueType.Object:
                return new SValue(double.NaN);
            case SValueType.Function:
                return new SValue(double.NaN);
            case SValueType.BooleanTrue:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        if ((double)v2.Value > 0)
                            return True;
                        return False;
                    case SValueType.Object:
                    case SValueType.String:
                    case SValueType.Function:
                    case SValueType.BooleanTrue:
                        return True;
                    case SValueType.Null:
                    case SValueType.BooleanFalse:
                        return new SValue(double.NaN);
                }

                break;
            case SValueType.BooleanFalse:
                switch (v2.Type)
                {
                    case SValueType.Number:
                    case SValueType.Object:
                    case SValueType.String:
                    case SValueType.Function:
                    case SValueType.BooleanTrue:
                        return False;
                    case SValueType.Null:
                    case SValueType.BooleanFalse:
                        return new SValue(double.NaN);
                }

                break;
        }

        return Null;
    }

    public static bool operator ==(SValue v1, double v2)
    {
        if (v1.Type == SValueType.Number)
        {
            return (double)v1.Value == v2;
        }

        return false;
    }

    public static bool operator ==(double v1, SValue v2)
    {
        if (v2.Type == SValueType.Number)
        {
            return v1 == (double)v2.Value;
        }

        return false;
    }

    public static bool operator ==(SValue v1, string v2)
    {
        if (v1.Type == SValueType.String)
        {
            return (string)v1.Value == v2;
        }

        return false;
    }

    public static bool operator ==(string v1, SValue v2)
    {
        if (v2.Type == SValueType.String)
        {
            return v1 == (string)v2.Value;
        }

        return false;
    }

    public static bool operator ==(SValue v1, SValue v2)
    {
        if (v1.Type == SValueType.Number && v2.Type == SValueType.Number)
        {
            return (double)v1.Value == (double)v2.Value;
        }

        return v1.Value == v2.Value;
    }


    public static bool operator !=(SValue v1, double v2)
    {
        if (v1.Type == SValueType.Number)
        {
            return (double)v1.Value != v2;
        }

        return true;
    }

    public static bool operator !=(double v1, SValue v2)
    {
        if (v2.Type == SValueType.Number)
        {
            return v1 != (double)v2.Value;
        }

        return true;
    }

    public static bool operator !=(SValue v1, string v2)
    {
        if (v1.Type == SValueType.String)
        {
            return (string)v1.Value != v2;
        }

        return true;
    }

    public static bool operator !=(string v1, SValue v2)
    {
        if (v2.Type == SValueType.String)
        {
            return v1 != (string)v2.Value;
        }

        return true;
    }

    public static bool operator !=(SValue v1, SValue v2)
    {
        if (v1.Type == SValueType.Number && v2.Type == SValueType.Number)
        {
            return (double)v1.Value != (double)v2.Value;
        }

        return v1.Value != v2.Value;
    }


    public static bool operator <(SValue v1, double v2)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                return (double)v1.Value < v2;
            case SValueType.BooleanTrue:
                return 1d < v2;
            case SValueType.BooleanFalse:
                return 0d < v2;
        }

        return false;
    }

    public static bool operator <(double v1, SValue v2)
    {
        switch (v2.Type)
        {
            case SValueType.Number:
                return v1 < (double)v2.Value;
            case SValueType.BooleanTrue:
                return v1 < 1d;
            case SValueType.BooleanFalse:
                return v1 < 0d;
        }

        return false;
    }

    public static bool operator <(SValue v1, SValue v2)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return (double)v1.Value < (double)v2.Value;
                    case SValueType.BooleanTrue:
                        return (double)v1.Value < 1d;
                    case SValueType.BooleanFalse:
                        return (double)v1.Value < 0d;
                }

                break;
            case SValueType.BooleanTrue:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return 1d < (double)v2.Value;
                    case SValueType.BooleanTrue:
                        return false;
                    case SValueType.BooleanFalse:
                        return false;
                }

                break;
            case SValueType.BooleanFalse:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return 0d < (double)v2.Value;
                    case SValueType.BooleanTrue:
                        return true;
                    case SValueType.BooleanFalse:
                        return false;
                }

                break;
        }

        return false;
    }

    public static bool operator >(SValue v1, double v2)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                return (double)v1.Value > v2;
            case SValueType.BooleanTrue:
                return 1d > v2;
            case SValueType.BooleanFalse:
                return 0d > v2;
        }

        return false;
    }

    public static bool operator >(double v1, SValue v2)
    {
        switch (v2.Type)
        {
            case SValueType.Number:
                return v1 > (double)v2.Value;
            case SValueType.BooleanTrue:
                return v1 > 1d;
            case SValueType.BooleanFalse:
                return v1 > 0d;
        }

        return false;
    }

    public static bool operator >(SValue v1, SValue v2)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return (double)v1.Value > (double)v2.Value;
                    case SValueType.BooleanTrue:
                        return (double)v1.Value > 1d;
                    case SValueType.BooleanFalse:
                        return (double)v1.Value > 0d;
                }

                break;
            case SValueType.BooleanTrue:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return 1d > (double)v2.Value;
                    case SValueType.BooleanTrue:
                        return false;
                    case SValueType.BooleanFalse:
                        return true;
                }

                break;
            case SValueType.BooleanFalse:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return 0d > (double)v2.Value;
                    case SValueType.BooleanTrue:
                        return false;
                    case SValueType.BooleanFalse:
                        return false;
                }

                break;
        }

        return false;
    }

    public static bool operator <=(SValue v1, double v2)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                return (double)v1.Value <= v2;
            case SValueType.BooleanTrue:
                return (double)v1.Value <= 1d;
            case SValueType.BooleanFalse:
                return (double)v1.Value <= 0d;
        }

        return false;
    }

    public static bool operator <=(double v1, SValue v2)
    {
        switch (v2.Type)
        {
            case SValueType.Number:
                return v1 <= (double)v2.Value;
            case SValueType.BooleanTrue:
                return v1 <= 1d;
            case SValueType.BooleanFalse:
                return v1 <= 0d;
        }

        return false;
    }

    public static bool operator <=(SValue v1, SValue v2)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return (double)v1.Value <= (double)v2.Value;
                    case SValueType.BooleanTrue:
                        return (double)v1.Value <= 1d;
                    case SValueType.BooleanFalse:
                        return (double)v1.Value <= 0d;
                }

                break;
            case SValueType.BooleanTrue:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return 1d <= (double)v2.Value;
                    case SValueType.BooleanTrue:
                        return true;
                    case SValueType.BooleanFalse:
                        return false;
                }

                break;
            case SValueType.BooleanFalse:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return 0d < (double)v2.Value;
                    case SValueType.BooleanTrue:
                        return true;
                    case SValueType.BooleanFalse:
                        return true;
                }

                break;
        }

        return false;
    }

    public static bool operator >=(SValue v1, double v2)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                return (double)v1.Value >= v2;
            case SValueType.BooleanTrue:
                return (double)v1.Value >= 1d;
            case SValueType.BooleanFalse:
                return (double)v1.Value >= 0d;
        }

        return false;
    }

    public static bool operator >=(double v1, SValue v2)
    {
        switch (v2.Type)
        {
            case SValueType.Number:
                return v1 >= (double)v2.Value;
            case SValueType.BooleanTrue:
                return v1 >= 1d;
            case SValueType.BooleanFalse:
                return v1 >= 0d;
        }

        return false;
    }

    public static bool operator >=(SValue v1, SValue v2)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return (double)v1.Value >= (double)v2.Value;
                    case SValueType.BooleanTrue:
                        return (double)v1.Value >= 1d;
                    case SValueType.BooleanFalse:
                        return (double)v1.Value >= 0d;
                }

                break;
            case SValueType.BooleanTrue:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return 1d >= (double)v2.Value;
                    case SValueType.BooleanTrue:
                        return true;
                    case SValueType.BooleanFalse:
                        return true;
                }

                break;
            case SValueType.BooleanFalse:
                switch (v2.Type)
                {
                    case SValueType.Number:
                        return 0d >= (double)v2.Value;
                    case SValueType.BooleanTrue:
                        return false;
                    case SValueType.BooleanFalse:
                        return true;
                }

                break;
        }

        return false;
    }

    public static SValue operator ++(SValue v1)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                v1.Value = (double)v1.Value + 1d;
                return v1;
            case SValueType.BooleanTrue:
            case SValueType.BooleanFalse:
                return True;
        }

        return new SValue(double.NaN);
    }

    public static SValue operator --(SValue v1)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                v1.Value = (double)v1.Value - 1d;
                return v1;
            case SValueType.BooleanTrue:
            case SValueType.BooleanFalse:
                return False;
        }

        return new SValue(double.NaN);
    }

    public static SValue operator -(SValue v1)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                return new SValue(-(double)v1.Value);
            case SValueType.BooleanTrue:
            case SValueType.BooleanFalse:
                return False;
        }

        return new SValue(double.NaN);
    }

    public static SValue operator +(SValue v1)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                return new SValue(v1);
            case SValueType.BooleanTrue:
                return True;
            case SValueType.BooleanFalse:
                return False;
        }

        return new SValue(double.NaN);
    }

    public static SValue operator !(SValue v1)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                return (double)v1.Value > 0 ? False : True;
            case SValueType.BooleanTrue:
                return False;
            case SValueType.BooleanFalse:
                return True;
        }

        return False;
    }

    public static bool operator true(SValue v1)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                return (double)v1.Value > 0;
            case SValueType.BooleanTrue:
                return true;
            case SValueType.BooleanFalse:
            case SValueType.Null:
                return false;
            case SValueType.String:
                return ((string)v1.Value).Length != 0;
        }

        return true;
    }

    public static bool operator false(SValue v1)
    {
        switch (v1.Type)
        {
            case SValueType.Number:
                return (double)v1.Value <= 0;
            case SValueType.BooleanTrue:
                return false;
            case SValueType.BooleanFalse:
            case SValueType.Null:
                return true;
            case SValueType.String:
                return ((string)v1.Value).Length == 0;
        }

        return false;
    }

    #endregion

    public override string ToString()
    {
        switch (Type)
        {
            case SValueType.Function:
                return "function";
            case SValueType.Null:
                return "null";
            case SValueType.Number:
                return Value.ToString();
            case SValueType.Object:
                return Value.ToString();
            case SValueType.BooleanTrue:
                return "true";
            case SValueType.BooleanFalse:
                return "false";
            case SValueType.String:
                return (string)Value;
        }

        return Value.ToString();
    }
}
