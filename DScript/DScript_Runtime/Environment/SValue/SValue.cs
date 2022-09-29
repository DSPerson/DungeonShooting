﻿
namespace DScript.Runtime
{
    /// <summary>
    /// 脚本中的数据描述接口
    /// </summary>
    public abstract class SValue
    {
        /// <summary>
        /// 获取该类存放的数据类型
        /// </summary>
        internal DataType dataType;

        /// <summary>
        /// 获取数据对象
        /// </summary>
        public abstract object GetValue();

        /// <summary>
        /// 获取成员属性
        /// </summary>
        public abstract SValue GetMember(string key);

        /// <summary>
        /// 返回是否存在属性
        /// </summary>
        public abstract bool HasMember(string key);

        /// <summary>
        /// 设置成员属性
        /// </summary>
        public abstract void SetMember(string key, SValue value);

        /// <summary>
        /// 把自己当成函数执行, 参数 0 个
        /// </summary>
        public abstract SValue Invoke();

        /// <summary>
        /// 把自己当成函数执行, 参数 1 个
        /// </summary>
        public abstract SValue Invoke(SValue v0);

        /// <summary>
        /// 把自己当成函数执行, 参数 2 个
        /// </summary>
        public abstract SValue Invoke(SValue v0, SValue v1);

        /// <summary>
        /// 把自己当成函数执行, 参数 3 个
        /// </summary>
        public abstract SValue Invoke(SValue v0, SValue v1, SValue v2);

        /// <summary>
        /// 把自己当成函数执行, 参数 4 个
        /// </summary>
        public abstract SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3);

        /// <summary>
        /// 把自己当成函数执行, 参数 5 个
        /// </summary>
        public abstract SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4);

        /// <summary>
        /// 把自己当成函数执行, 参数 6 个
        /// </summary>
        public abstract SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5);

        /// <summary>
        /// 把自己当成函数执行, 参数 7 个
        /// </summary>
        public abstract SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6);

        /// <summary>
        /// 把自己当成函数执行, 参数 8 个
        /// </summary>
        public abstract SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
            SValue v7);

        /// <summary>
        /// 把自己当成函数执行, 参数 9 个
        /// </summary>
        public abstract SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
            SValue v7, SValue v8);

        /// <summary>
        /// 把自己当成函数执行, 参数 10 个
        /// </summary>
        public abstract SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
            SValue v7, SValue v8, SValue v9);

        /// <summary>
        /// 把自己当成函数执行, 参数 11 个
        /// </summary>
        public abstract SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
            SValue v7, SValue v8, SValue v9, SValue v10);

        /// <summary>
        /// 把自己当成函数执行, 参数 12 个
        /// </summary>
        public abstract SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
            SValue v7, SValue v8, SValue v9, SValue v10, SValue v11);

        /// <summary>
        /// 把自己当成函数执行, 参数 13 个
        /// </summary>
        public abstract SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
            SValue v7, SValue v8, SValue v9, SValue v10, SValue v11, SValue v12);

        /// <summary>
        /// 把自己当成函数执行, 参数 14 个
        /// </summary>
        public abstract SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
            SValue v7, SValue v8, SValue v9, SValue v10, SValue v11, SValue v12, SValue v13);

        /// <summary>
        /// 把自己当成函数执行, 参数 15 个
        /// </summary>
        public abstract SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
            SValue v7, SValue v8, SValue v9, SValue v10, SValue v11, SValue v12, SValue v13, SValue v14);

        /// <summary>
        /// 把自己当成函数执行, 参数 16 个
        /// </summary>
        public abstract SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
            SValue v7, SValue v8, SValue v9, SValue v10, SValue v11, SValue v12, SValue v13, SValue v14, SValue v15);

        /// <summary>
        /// 执行该对象的成员函数, 传入 0 个参数
        /// </summary>
        public abstract SValue InvokeMethod(string key);

        /// <summary>
        /// 执行该对象的成员函数, 传入 1 个参数
        /// </summary>
        public abstract SValue InvokeMethod(string key, SValue v0);

        /// <summary>
        /// 执行该对象的成员函数, 传入 2 个参数
        /// </summary>
        public abstract SValue InvokeMethod(string key, SValue v0, SValue v1);

        /// <summary>
        /// 执行该对象的成员函数, 传入 3 个参数
        /// </summary>
        public abstract SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2);

        /// <summary>
        /// 执行该对象的成员函数, 传入 4 个参数
        /// </summary>
        public abstract SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3);

        /// <summary>
        /// 执行该对象的成员函数, 传入 5 个参数
        /// </summary>
        public abstract SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4);

        /// <summary>
        /// 执行该对象的成员函数, 传入 6 个参数
        /// </summary>
        public abstract SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4,
            SValue v5);

        /// <summary>
        /// 执行该对象的成员函数, 传入 7 个参数
        /// </summary>
        public abstract SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4,
            SValue v5,
            SValue v6);

        /// <summary>
        /// 执行该对象的成员函数, 传入 8 个参数
        /// </summary>
        public abstract SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4,
            SValue v5,
            SValue v6, SValue v7);

        /// <summary>
        /// 执行该对象的成员函数, 传入 9 个参数
        /// </summary>
        public abstract SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4,
            SValue v5,
            SValue v6, SValue v7, SValue v8);

        /// <summary>
        /// 执行该对象的成员函数, 传入 10 个参数
        /// </summary>
        public abstract SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4,
            SValue v5,
            SValue v6, SValue v7, SValue v8, SValue v9);

        /// <summary>
        /// 执行该对象的成员函数, 传入 11 个参数
        /// </summary>
        public abstract SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4,
            SValue v5,
            SValue v6, SValue v7, SValue v8, SValue v9, SValue v10);

        /// <summary>
        /// 执行该对象的成员函数, 传入 12 个参数
        /// </summary>
        public abstract SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4,
            SValue v5,
            SValue v6, SValue v7, SValue v8, SValue v9, SValue v10, SValue v11);

        /// <summary>
        /// 执行该对象的成员函数, 传入 13 个参数
        /// </summary>
        public abstract SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4,
            SValue v5,
            SValue v6, SValue v7, SValue v8, SValue v9, SValue v10, SValue v11, SValue v12);

        /// <summary>
        /// 执行该对象的成员函数, 传入 14 个参数
        /// </summary>
        public abstract SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4,
            SValue v5,
            SValue v6, SValue v7, SValue v8, SValue v9, SValue v10, SValue v11, SValue v12, SValue v13);

        /// <summary>
        /// 执行该对象的成员函数, 传入 15 个参数
        /// </summary>
        public abstract SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4,
            SValue v5,
            SValue v6, SValue v7, SValue v8, SValue v9, SValue v10, SValue v11, SValue v12, SValue v13, SValue v14);

        /// <summary>
        /// 执行该对象的成员函数, 传入 16 个参数
        /// </summary>
        public abstract SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4,
            SValue v5,
            SValue v6, SValue v7, SValue v8, SValue v9, SValue v10, SValue v11, SValue v12, SValue v13, SValue v14,
            SValue
                v15);

        #region 子类需重写的运算函数

        /// <summary>
        /// 判断等于, 和 double 比较
        /// </summary>
        public abstract bool Operator_Equal_Double(double v2);

        /// <summary>
        /// 判断等于, 和 string 比较
        /// </summary>
        public abstract bool Operator_Equal_String(string v2);

        /// <summary>
        /// 判断等于, 和 SValue 比较
        /// </summary>
        public abstract bool Operator_Equal_SValue(SValue v2);

        /// <summary>
        /// 判断不等于, 和 double 比较
        /// </summary>
        public abstract bool Operator_Not_Equal_Double(double v2);

        /// <summary>
        /// 判断不等于, 和 string 比较
        /// </summary>
        public abstract bool Operator_Not_Equal_String(string v2);

        /// <summary>
        /// 判断不等于, 和 SValue 比较
        /// </summary>
        public abstract bool Operator_Not_Equal_SValue(SValue v2);

        /// <summary>
        /// 和 double 相加
        /// </summary>
        public abstract SValue Operator_Add_Double(double v2);

        /// <summary>
        /// 和 double 相加, v1 在前
        /// </summary>
        public abstract SValue Operator_Append_Add_Double(double v1);

        /// <summary>
        /// 和 string 相加
        /// </summary>
        public abstract SValue Operator_Add_String(string v2);

        /// <summary>
        /// 和 string 相加, v1 在前
        /// </summary>
        public abstract SValue Operator_Append_Add_String(string v1);

        /// <summary>
        /// 和 SValue 相加
        /// </summary>
        public abstract SValue Operator_Add_SValue(SValue v2);

        /// <summary>
        /// 和 double 相减
        /// </summary>
        public abstract SValue Operator_Subtract_Double(double v2);

        /// <summary>
        /// 和 double 相减, v1 在前
        /// </summary>
        public abstract SValue Operator_Append_Subtract_Double(double v1);

        /// <summary>
        /// 和 SValue 相减
        /// </summary>
        public abstract SValue Operator_Subtract_SValue(SValue v2);

        /// <summary>
        /// 和 double 相乘
        /// </summary>
        public abstract SValue Operator_Multiply_Double(double v2);

        /// <summary>
        /// 和 double 相乘, v1 在前
        /// </summary>
        public abstract SValue Operator_Append_Multiply_Double(double v1);

        /// <summary>
        /// 和 SValue 相乘
        /// </summary>
        public abstract SValue Operator_Multiply_SValue(SValue v2);

        /// <summary>
        /// 和 double 相除
        /// </summary>
        public abstract SValue Operator_Divide_Double(double v2);

        /// <summary>
        /// 和 double 相除, v1 在前
        /// </summary>
        public abstract SValue Operator_Append_Divide_Double(double v1);

        /// <summary>
        /// 和 SValue 相除
        /// </summary>
        public abstract SValue Operator_Divide_SValue(SValue v2);

        /// <summary>
        /// 自加
        /// </summary>
        public abstract SValue Operator_SinceAdd();

        /// <summary>
        /// 自减
        /// </summary>
        public abstract SValue Operator_SinceReduction();

        /// <summary>
        /// 大于
        /// </summary>
        public abstract bool Operator_Greater_Double(double v2);

        /// <summary>
        /// 小于
        /// </summary>
        public abstract bool Operator_Less_Double(double v2);

        /// <summary>
        /// 大于
        /// </summary>
        public abstract bool Operator_Greater_SValue(SValue v2);

        /// <summary>
        /// 小于
        /// </summary>
        public abstract bool Operator_Less_SValue(SValue v2);

        /// <summary>
        /// 大于等于
        /// </summary>
        public abstract bool Operator_Greater_Equal_Double(double v2);

        /// <summary>
        /// 小于等于
        /// </summary>
        public abstract bool Operator_Less_Equal_Double(double v2);

        /// <summary>
        /// 大于等于
        /// </summary>
        public abstract bool Operator_Greater_Equal_SValue(SValue v2);

        /// <summary>
        /// 正数
        /// </summary>
        public abstract SValue Operator_Positive();

        /// <summary>
        /// 负数
        /// </summary>
        public abstract SValue Operator_Negative();

        /// <summary>
        /// 取反
        /// </summary>
        public abstract bool Operator_Not();

        /// <summary>
        /// 小于等于
        /// </summary>
        public abstract bool Operator_Less_Equal_SValue(SValue v2);

        /// <summary>
        /// 判断true
        /// </summary>
        public abstract bool Operator_True();

        /// <summary>
        /// 判断false
        /// </summary>
        public abstract bool Operator_False();

        /// <summary>
        /// 取模运算
        /// </summary>
        public abstract SValue Operator_Modulus_Double(double v2);

        /// <summary>
        /// 取模运算, v1 在前
        /// </summary>
        public abstract SValue Operator_Append_Modulus_Double(double v1);

        /// <summary>
        /// 取模运算
        /// </summary>
        public abstract SValue Operator_Modulus_SValue(SValue v2);

        /// <summary>
        /// 二进制位运算取反, 符号 ~
        /// </summary>
        public abstract SValue Operator_Shift_Negation();

        /// <summary>
        /// 二进制位运算, 右移
        /// </summary>
        public abstract SValue Operator_Shift_Right(int v1);

        /// <summary>
        /// 二进制位运算, 左移
        /// </summary>
        public abstract SValue Operator_Shift_Left(int v1);

        /// <summary>
        /// 二进制或运算, 单位: |
        /// </summary>
        public abstract SValue Operator_Shift_Or_Double(double v2);

        /// <summary>
        /// 二进制或运算, 单位: |, v1 在前
        /// </summary>
        public abstract SValue Operator_Append_Shift_Or_Double(double v1);

        /// <summary>
        /// 二进制或运算, 单位: |
        /// </summary>
        public abstract SValue Operator_Shift_Or_SValue(SValue v2);

        /// <summary>
        /// 二进制与运算
        /// </summary>
        public abstract SValue Operator_Shift_And_Double(double v2);

        /// <summary>
        /// 二进制与运算, v1 在前
        /// </summary>
        public abstract SValue Operator_Append_Shift_And_Double(double v1);

        /// <summary>
        /// 二进制与运算
        /// </summary>
        public abstract SValue Operator_Shift_And_SValue(SValue v2);

        /// <summary>
        /// 二进制异或运算, 单位: ^
        /// </summary>
        public abstract SValue Operator_Shift_Xor_Double(double v2);

        /// <summary>
        /// 二进制异或运算, 单位: ^, v1 在前
        /// </summary>
        public abstract SValue Operator_Append_Shift_Xor_Double(double v1);

        /// <summary>
        /// 二进制异或运算, 单位: ^
        /// </summary>
        public abstract SValue Operator_Shift_Xor_SValue(SValue v2);

        #endregion

        #region 运算符重载

        //
        // public static SValue operator +(SValue v1, double v2)
        // {
        //     return v1.Operator_Add_Double(v2);
        // }
        //
        // public static SValue operator +(double v1, SValue v2)
        // {
        //     return v2.Operator_Append_Add_Double(v1);
        // }
        //
        // public static SValue operator +(SValue v1, string v2)
        // {
        //     return v1.Operator_Add_String(v2);
        // }
        //
        // public static SValue operator +(string v1, SValue v2)
        // {
        //     return v2.Operator_Append_Add_String(v1);
        // }
        //
        // public static SValue operator +(SValue v1, SValue v2)
        // {
        //     return v1.Operator_Add_SValue(v2);
        // }
        //
        // public static SValue operator -(SValue v1, double v2)
        // {
        //     return v1.Operator_Subtract_Double(v2);
        // }
        //
        // public static SValue operator -(double v1, SValue v2)
        // {
        //     return v2.Operator_Append_Subtract_Double(v1);
        // }
        //
        // public static SValue operator -(SValue v1, SValue v2)
        // {
        //     return v1.Operator_Subtract_SValue(v2);
        // }
        //
        // public static SValue operator *(SValue v1, double v2)
        // {
        //     return v1.Operator_Multiply_Double(v2);
        // }
        //
        // public static SValue operator *(double v1, SValue v2)
        // {
        //     return v2.Operator_Append_Multiply_Double(v1);
        // }
        //
        // public static SValue operator *(SValue v1, SValue v2)
        // {
        //     return v1.Operator_Multiply_SValue(v2);
        // }
        //
        // public static SValue operator /(SValue v1, double v2)
        // {
        //     return v1.Operator_Divide_Double(v2);
        // }
        //
        // public static SValue operator /(double v1, SValue v2)
        // {
        //     return v2.Operator_Append_Divide_Double(v1);
        // }
        //
        // public static SValue operator /(SValue v1, SValue v2)
        // {
        //     return v1.Operator_Divide_SValue(v2);
        // }
        //
        // public static bool operator ==(SValue v1, double v2)
        // {
        //     return v1.Operator_Equal_Double(v2);
        // }
        //
        // public static bool operator ==(double v1, SValue v2)
        // {
        //     return v2.Operator_Equal_Double(v1);
        // }
        //
        // public static bool operator ==(SValue v1, string v2)
        // {
        //     return v1.Operator_Equal_String(v2);
        // }
        //
        // public static bool operator ==(string v1, SValue v2)
        // {
        //     return v2.Operator_Equal_String(v1);
        // }
        //
        // public static bool operator ==(SValue v1, SValue v2)
        // {
        //     return v1.Operator_Equal_SValue(v2);
        // }
        //
        // public static bool operator !=(SValue v1, double v2)
        // {
        //     return v1.Operator_Not_Equal_Double(v2);
        // }
        //
        // public static bool operator !=(double v1, SValue v2)
        // {
        //     return v2.Operator_Not_Equal_Double(v1);
        // }
        //
        // public static bool operator !=(SValue v1, string v2)
        // {
        //     return v1.Operator_Not_Equal_String(v2);
        // }
        //
        // public static bool operator !=(string v1, SValue v2)
        // {
        //     return v2.Operator_Not_Equal_String(v1);
        // }
        //
        // public static bool operator !=(SValue v1, SValue v2)
        // {
        //     return v1.Operator_Not_Equal_SValue(v2);
        // }
        //
        // public static SValue operator ++(SValue v1)
        // {
        //     return v1.Operator_SinceAdd();
        // }
        //
        // public static SValue operator --(SValue v1)
        // {
        //     return v1.Operator_SinceReduction();
        // }
        //
        // public static bool operator >(SValue v1, double v2)
        // {
        //     return v1.Operator_Greater_Double(v2);
        // }
        //
        // public static bool operator >(double v1, SValue v2)
        // {
        //     return v2.Operator_Less_Equal_Double(v1);
        // }
        //
        // public static bool operator >(SValue v1, SValue v2)
        // {
        //     return v1.Operator_Greater_SValue(v2);
        // }
        //
        // public static bool operator <(SValue v1, double v2)
        // {
        //     return v1.Operator_Less_Double(v2);
        // }
        //
        // public static bool operator <(double v1, SValue v2)
        // {
        //     return v2.Operator_Greater_Equal_Double(v1);
        // }
        //
        // public static bool operator <(SValue v1, SValue v2)
        // {
        //     return v1.Operator_Less_SValue(v2);
        // }
        //
        // public static bool operator >=(SValue v1, double v2)
        // {
        //     return v1.Operator_Greater_Equal_Double(v2);
        // }
        //
        // public static bool operator >=(double v1, SValue v2)
        // {
        //     return v2.Operator_Less_Double(v1);
        // }
        //
        // public static bool operator >=(SValue v1, SValue v2)
        // {
        //     return v1.Operator_Greater_Equal_SValue(v2);
        // }
        //
        // public static bool operator <=(SValue v1, double v2)
        // {
        //     return v1.Operator_Less_Equal_Double(v2);
        // }
        //
        // public static bool operator <=(double v1, SValue v2)
        // {
        //     return v2.Operator_Greater_Double(v1);
        // }
        //
        // public static bool operator <=(SValue v1, SValue v2)
        // {
        //     return v1.Operator_Less_Equal_SValue(v2);
        // }
        //
        // public static SValue operator +(SValue v1)
        // {
        //     return v1.Operator_Positive();
        // }
        //
        // public static SValue operator -(SValue v1)
        // {
        //     return v1.Operator_Negative();
        // }
        //
        // public static bool operator !(SValue v1)
        // {
        //     return v1.Operator_Not();
        // }
        //
        // public static bool operator true(SValue v1)
        // {
        //     return v1.Operator_True();
        // }
        //
        // public static bool operator false(SValue v1)
        // {
        //     return v1.Operator_False();
        // }
        //
        // public static SValue operator %(SValue v1, double v2)
        // {
        //     return v1.Operator_Modulus_Double(v2);
        // }
        //
        // public static SValue operator %(double v1, SValue v2)
        // {
        //     return v2.Operator_Append_Modulus_Double(v1);
        // }
        //
        // public static SValue operator %(SValue v1, SValue v2)
        // {
        //     return v1.Operator_Modulus_SValue(v2);
        // }
        //
        // public static SValue operator ~(SValue v1)
        // {
        //     return v1.Operator_Shift_Negation();
        // }
        //
        // public static SValue operator >> (SValue v1, int v2)
        // {
        //     return v1.Operator_Shift_Right(v2);
        // }
        //
        // public static SValue operator <<(SValue v1, int v2)
        // {
        //     return v1.Operator_Shift_Left(v2);
        // }
        //
        // public static SValue operator |(SValue v1, double v2)
        // {
        //     return v1.Operator_Shift_Or_Double(v2);
        // }
        //
        // public static SValue operator |(double v1, SValue v2)
        // {
        //     return v2.Operator_Append_Shift_Or_Double(v1);
        // }
        //
        // public static SValue operator |(SValue v1, SValue v2)
        // {
        //     return v1.Operator_Shift_Or_SValue(v2);
        // }
        //
        // public static SValue operator &(SValue v1, double v2)
        // {
        //     return v1.Operator_Shift_And_Double(v2);
        // }
        //
        // public static SValue operator &(double v1, SValue v2)
        // {
        //     return v2.Operator_Append_Shift_And_Double(v1);
        // }
        //
        // public static SValue operator &(SValue v1, SValue v2)
        // {
        //     return v1.Operator_Shift_And_SValue(v2);
        // }
        //
        // public static SValue operator ^(SValue v1, double v2)
        // {
        //     return v1.Operator_Shift_Xor_Double(v2);
        // }
        //
        // public static SValue operator ^(double v1, SValue v2)
        // {
        //     return v2.Operator_Append_Shift_Xor_Double(v1);
        // }
        //
        // public static SValue operator ^(SValue v1, SValue v2)
        // {
        //     return v1.Operator_Shift_Xor_SValue(v2);
        // }

        #endregion

        /// <summary>
        /// true
        /// </summary>
        public static readonly SValue True = new True_SValue();

        /// <summary>
        /// false
        /// </summary>
        public static readonly SValue False = new False_SValue();

        /// <summary>
        /// 空值
        /// </summary>
        public static readonly SValue Null = new Null_SValue();

        /// <summary>
        /// NaN
        /// </summary>
        public static readonly SValue NaN = new Number_SValue(double.NaN);

        /// <summary>
        /// 空的字符串
        /// </summary>
        public static readonly SValue EmptyString = new String_SValue("");

        /// <summary>
        /// 负无穷大
        /// </summary>
        public static readonly SValue NegativeInfinity = new Number_SValue(double.NegativeInfinity);

        /// <summary>
        /// 正无穷大
        /// </summary>
        public static readonly SValue PositiveInfinity = new Number_SValue(double.PositiveInfinity);

        /// <summary>
        /// 数字 -2
        /// </summary>
        public static readonly SValue NegativeTwo = new Number_SValue(-2);

        /// <summary>
        /// 数字 -1
        /// </summary>
        public static readonly SValue NegativeOne = new Number_SValue(-1);

        /// <summary>
        /// 数字 0
        /// </summary>
        public static readonly SValue Zero = new Number_SValue(0);

        /// <summary>
        /// 数字 1
        /// </summary>
        public static readonly SValue One = new Number_SValue(1);

        /// <summary>
        /// 数字 2
        /// </summary>
        public static readonly SValue Two = new Number_SValue(2);

        /// <summary>
        /// 数字 3
        /// </summary>
        public static readonly SValue Three = new Number_SValue(3);

        /// <summary>
        /// 数字 4
        /// </summary>
        public static readonly SValue Four = new Number_SValue(4);

        /// <summary>
        /// 数字 5
        /// </summary>
        public static readonly SValue Five = new Number_SValue(5);

        /// <summary>
        /// 数字 6
        /// </summary>
        public static readonly SValue Six = new Number_SValue(6);

        /// <summary>
        /// 数字 7
        /// </summary>
        public static readonly SValue Seven = new Number_SValue(7);

        /// <summary>
        /// 数字 8
        /// </summary>
        public static readonly SValue Eight = new Number_SValue(8);

        /// <summary>
        /// 数字 9
        /// </summary>
        public static readonly SValue Nine = new Number_SValue(9);

        /// <summary>
        /// 数字 10
        /// </summary>
        public static readonly SValue Ten = new Number_SValue(10);

        /// <summary>
        /// 数字 11
        /// </summary>
        public static readonly SValue Eleven = new Number_SValue(11);

        /// <summary>
        /// 数字 12
        /// </summary>
        public static readonly SValue Twelve = new Number_SValue(12);

        /// <summary>
        /// 数字 13
        /// </summary>
        public static readonly SValue Thirteen = new Number_SValue(13);

        /// <summary>
        /// 数字 14
        /// </summary>
        public static readonly SValue Fourteen = new Number_SValue(14);

        /// <summary>
        /// 数字 15
        /// </summary>
        public static readonly SValue Fifteen = new Number_SValue(15);

        /// <summary>
        /// 数字 16
        /// </summary>
        public static readonly SValue Sixteen = new Number_SValue(16);
    }
}