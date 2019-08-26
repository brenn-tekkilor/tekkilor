/*******************************************************************************

    Units of Measurement for C# applications

    Copyright (C) Marek Aniola

    This program is provided to you under the terms of the license
    as published at https://github.com/mangh/unitsofmeasurement


********************************************************************************/
using System;

namespace UnitsOfMeasurement
{
    public partial struct BTU_Hour : IQuantity<double>, IEquatable<BTU_Hour>, IComparable<BTU_Hour>, IFormattable
    {
        #region Fields
        internal readonly double m_value;
        #endregion

        #region Properties
        public double Value { get { return m_value; } }
        Unit<double> IQuantity<double>.Unit { get { return BTU_Hour.Proxy; } }
        #endregion

        #region Constructor(s)
        public BTU_Hour(double value)
        {
            m_value = value;
        }
        #endregion

        #region Conversions
        public static explicit operator BTU_Hour(double q) { return new BTU_Hour(q); }
        public static explicit operator BTU_Hour(KiloWatt q) { return new BTU_Hour((BTU_Hour.Factor / KiloWatt.Factor) * q.m_value); }
        public static explicit operator BTU_Hour(Watt q) { return new BTU_Hour((BTU_Hour.Factor / Watt.Factor) * q.m_value); }
        public static BTU_Hour From(IQuantity<double> q)
        {
            if (q.Unit.Family != BTU_Hour.Family) throw new InvalidOperationException(string.Format("Cannot convert \"{0}\" to \"BTU_Hour\"", q.GetType().Name));
            return new BTU_Hour((BTU_Hour.Factor / q.Unit.Factor) * q.Value);
        }
        #endregion

        #region IObject / IEquatable<BTU_Hour>
        public override int GetHashCode() { return m_value.GetHashCode(); }
        public override bool /* IObject */ Equals(object obj) { return (obj is BTU_Hour) && Equals((BTU_Hour)obj); }
        public bool /* IEquatable<BTU_Hour> */ Equals(BTU_Hour other) { return this.m_value == other.m_value; }
        #endregion

        #region Comparison / IComparable<BTU_Hour>
        public static bool operator ==(BTU_Hour lhs, BTU_Hour rhs) { return lhs.m_value == rhs.m_value; }
        public static bool operator !=(BTU_Hour lhs, BTU_Hour rhs) { return lhs.m_value != rhs.m_value; }
        public static bool operator <(BTU_Hour lhs, BTU_Hour rhs) { return lhs.m_value < rhs.m_value; }
        public static bool operator >(BTU_Hour lhs, BTU_Hour rhs) { return lhs.m_value > rhs.m_value; }
        public static bool operator <=(BTU_Hour lhs, BTU_Hour rhs) { return lhs.m_value <= rhs.m_value; }
        public static bool operator >=(BTU_Hour lhs, BTU_Hour rhs) { return lhs.m_value >= rhs.m_value; }
        public int /* IComparable<BTU_Hour> */ CompareTo(BTU_Hour other) { return this.m_value.CompareTo(other.m_value); }
        #endregion

        #region Arithmetic
        // Inner:
        public static BTU_Hour operator +(BTU_Hour lhs, BTU_Hour rhs) { return new BTU_Hour(lhs.m_value + rhs.m_value); }
        public static BTU_Hour operator -(BTU_Hour lhs, BTU_Hour rhs) { return new BTU_Hour(lhs.m_value - rhs.m_value); }
        public static BTU_Hour operator ++(BTU_Hour q) { return new BTU_Hour(q.m_value + 1d); }
        public static BTU_Hour operator --(BTU_Hour q) { return new BTU_Hour(q.m_value - 1d); }
        public static BTU_Hour operator -(BTU_Hour q) { return new BTU_Hour(-q.m_value); }
        public static BTU_Hour operator *(double lhs, BTU_Hour rhs) { return new BTU_Hour(lhs * rhs.m_value); }
        public static BTU_Hour operator *(BTU_Hour lhs, double rhs) { return new BTU_Hour(lhs.m_value * rhs); }
        public static BTU_Hour operator /(BTU_Hour lhs, double rhs) { return new BTU_Hour(lhs.m_value / rhs); }
        public static double operator /(BTU_Hour lhs, BTU_Hour rhs) { return lhs.m_value / rhs.m_value; }
        // Outer:
        public static BTU operator *(BTU_Hour lhs, Hour rhs) { return new BTU(lhs.m_value * rhs.m_value); }
        public static BTU operator *(Hour lhs, BTU_Hour rhs) { return new BTU(lhs.m_value * rhs.m_value); }
        #endregion

        #region Formatting
        public override string ToString() { return ToString(BTU_Hour.Format, null); }
        public string ToString(string format) { return ToString(format, null); }
        public string ToString(IFormatProvider fp) { return ToString(BTU_Hour.Format, fp); }
        public string /* IFormattable */ ToString(string format, IFormatProvider fp)
        {
            return string.Format(fp, format ?? BTU_Hour.Format, m_value, BTU_Hour.Symbol.Default);
        }
        #endregion

        #region Static fields
        private static readonly Dimension s_sense = BTU.Sense / Hour.Sense;
        private static readonly int s_family = Watt.Family;
        private static /*mutable*/ double s_factor = BTU.Factor / Hour.Factor;
        private static /*mutable*/ string s_format = "{0} {1}";
        private static readonly SymbolCollection s_symbol = new SymbolCollection("btu/h");
        private static readonly Unit<double> s_proxy = new BTU_Hour_Proxy();

        private static readonly BTU_Hour s_one = new BTU_Hour(1d);
        private static readonly BTU_Hour s_zero = new BTU_Hour(0d);
        #endregion

        #region Static Properties
        public static Dimension Sense { get { return s_sense; } }
        public static int Family { get { return s_family; } }
        public static double Factor { get { return s_factor; } set { s_factor = value; } }
        public static string Format { get { return s_format; } set { s_format = value; } }
        public static SymbolCollection Symbol { get { return s_symbol; } }
        public static Unit<double> Proxy { get { return s_proxy; } }

        public static BTU_Hour One { get { return s_one; } }
        public static BTU_Hour Zero { get { return s_zero; } }
        #endregion
    }

    public partial class BTU_Hour_Proxy : Unit<double>
    {
        #region Properties
        public override int Family { get { return BTU_Hour.Family; } }
        public override Dimension Sense { get { return BTU_Hour.Sense; } }
        public override SymbolCollection Symbol { get { return BTU_Hour.Symbol; } }
        public override double Factor { get { return BTU_Hour.Factor; } set { BTU_Hour.Factor = value; } }
        public override string Format { get { return BTU_Hour.Format; } set { BTU_Hour.Format = value; } }
        #endregion

        #region Constructor(s)
        public BTU_Hour_Proxy() :
            base(typeof(BTU_Hour))
        {
        }
        #endregion

        #region Methods
        public override IQuantity<double> Create(double value)
        {
            return new BTU_Hour(value);
        }
        public override IQuantity<double> From(IQuantity<double> quantity)
        {
            return BTU_Hour.From(quantity);
        }
        #endregion
    }
}
