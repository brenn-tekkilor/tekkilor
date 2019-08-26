/*******************************************************************************

    Units of Measurement for C# applications

    Copyright (C) Marek Aniola

    This program is provided to you under the terms of the license
    as published at https://github.com/mangh/unitsofmeasurement


********************************************************************************/
using System;

namespace UnitsOfMeasurement
{
    public partial struct BTU : IQuantity<double>, IEquatable<BTU>, IComparable<BTU>, IFormattable
    {
        #region Fields
        internal readonly double m_value;
        #endregion

        #region Properties
        public double Value { get { return m_value; } }
        Unit<double> IQuantity<double>.Unit { get { return BTU.Proxy; } }
        #endregion

        #region Constructor(s)
        public BTU(double value)
        {
            m_value = value;
        }
        #endregion

        #region Conversions
        public static explicit operator BTU(double q) { return new BTU(q); }
        public static explicit operator BTU(Calorie q) { return new BTU((BTU.Factor / Calorie.Factor) * q.m_value); }
        public static explicit operator BTU(Joule q) { return new BTU((BTU.Factor / Joule.Factor) * q.m_value); }
        public static BTU From(IQuantity<double> q)
        {
            if (q.Unit.Family != BTU.Family) throw new InvalidOperationException(string.Format("Cannot convert \"{0}\" to \"BTU\"", q.GetType().Name));
            return new BTU((BTU.Factor / q.Unit.Factor) * q.Value);
        }
        #endregion

        #region IObject / IEquatable<BTU>
        public override int GetHashCode() { return m_value.GetHashCode(); }
        public override bool /* IObject */ Equals(object obj) { return (obj is BTU) && Equals((BTU)obj); }
        public bool /* IEquatable<BTU> */ Equals(BTU other) { return this.m_value == other.m_value; }
        #endregion

        #region Comparison / IComparable<BTU>
        public static bool operator ==(BTU lhs, BTU rhs) { return lhs.m_value == rhs.m_value; }
        public static bool operator !=(BTU lhs, BTU rhs) { return lhs.m_value != rhs.m_value; }
        public static bool operator <(BTU lhs, BTU rhs) { return lhs.m_value < rhs.m_value; }
        public static bool operator >(BTU lhs, BTU rhs) { return lhs.m_value > rhs.m_value; }
        public static bool operator <=(BTU lhs, BTU rhs) { return lhs.m_value <= rhs.m_value; }
        public static bool operator >=(BTU lhs, BTU rhs) { return lhs.m_value >= rhs.m_value; }
        public int /* IComparable<BTU> */ CompareTo(BTU other) { return this.m_value.CompareTo(other.m_value); }
        #endregion

        #region Arithmetic
        // Inner:
        public static BTU operator +(BTU lhs, BTU rhs) { return new BTU(lhs.m_value + rhs.m_value); }
        public static BTU operator -(BTU lhs, BTU rhs) { return new BTU(lhs.m_value - rhs.m_value); }
        public static BTU operator ++(BTU q) { return new BTU(q.m_value + 1d); }
        public static BTU operator --(BTU q) { return new BTU(q.m_value - 1d); }
        public static BTU operator -(BTU q) { return new BTU(-q.m_value); }
        public static BTU operator *(double lhs, BTU rhs) { return new BTU(lhs * rhs.m_value); }
        public static BTU operator *(BTU lhs, double rhs) { return new BTU(lhs.m_value * rhs); }
        public static BTU operator /(BTU lhs, double rhs) { return new BTU(lhs.m_value / rhs); }
        public static double operator /(BTU lhs, BTU rhs) { return lhs.m_value / rhs.m_value; }
        // Outer:
        public static BTU_Hour operator /(BTU lhs, Hour rhs) { return new BTU_Hour(lhs.m_value / rhs.m_value); }
        public static Hour operator /(BTU lhs, BTU_Hour rhs) { return new Hour(lhs.m_value / rhs.m_value); }
        #endregion

        #region Formatting
        public override string ToString() { return ToString(BTU.Format, null); }
        public string ToString(string format) { return ToString(format, null); }
        public string ToString(IFormatProvider fp) { return ToString(BTU.Format, fp); }
        public string /* IFormattable */ ToString(string format, IFormatProvider fp)
        {
            return string.Format(fp, format ?? BTU.Format, m_value, BTU.Symbol.Default);
        }
        #endregion

        #region Static fields
        private static readonly Dimension s_sense = Joule.Sense;
        private static readonly int s_family = Joule.Family;
        private static /*mutable*/ double s_factor = Joule.Factor / 1055.05585262d;
        private static /*mutable*/ string s_format = "{0} {1}";
        private static readonly SymbolCollection s_symbol = new SymbolCollection("btu");
        private static readonly Unit<double> s_proxy = new BTU_Proxy();

        private static readonly BTU s_one = new BTU(1d);
        private static readonly BTU s_zero = new BTU(0d);
        #endregion

        #region Static Properties
        public static Dimension Sense { get { return s_sense; } }
        public static int Family { get { return s_family; } }
        public static double Factor { get { return s_factor; } set { s_factor = value; } }
        public static string Format { get { return s_format; } set { s_format = value; } }
        public static SymbolCollection Symbol { get { return s_symbol; } }
        public static Unit<double> Proxy { get { return s_proxy; } }

        public static BTU One { get { return s_one; } }
        public static BTU Zero { get { return s_zero; } }
        #endregion
    }

    public partial class BTU_Proxy : Unit<double>
    {
        #region Properties
        public override int Family { get { return BTU.Family; } }
        public override Dimension Sense { get { return BTU.Sense; } }
        public override SymbolCollection Symbol { get { return BTU.Symbol; } }
        public override double Factor { get { return BTU.Factor; } set { BTU.Factor = value; } }
        public override string Format { get { return BTU.Format; } set { BTU.Format = value; } }
        #endregion

        #region Constructor(s)
        public BTU_Proxy() :
            base(typeof(BTU))
        {
        }
        #endregion

        #region Methods
        public override IQuantity<double> Create(double value)
        {
            return new BTU(value);
        }
        public override IQuantity<double> From(IQuantity<double> quantity)
        {
            return BTU.From(quantity);
        }
        #endregion
    }
}
