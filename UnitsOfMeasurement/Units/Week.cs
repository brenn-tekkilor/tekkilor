/*******************************************************************************

    Units of Measurement for C# applications

    Copyright (C) Marek Aniola

    This program is provided to you under the terms of the license
    as published at https://github.com/mangh/unitsofmeasurement


********************************************************************************/
using System;

namespace UnitsOfMeasurement
{
    public partial struct Week : IQuantity<double>, IEquatable<Week>, IComparable<Week>, IFormattable
    {
        #region Fields
        internal readonly double m_value;
        #endregion

        #region Properties
        public double Value { get { return m_value; } }
        Unit<double> IQuantity<double>.Unit { get { return Week.Proxy; } }
        #endregion

        #region Constructor(s)
        public Week(double value)
        {
            m_value = value;
        }
        #endregion

        #region Conversions
        public static explicit operator Week(double q) { return new Week(q); }
        public static explicit operator Week(Second q) { return new Week((Week.Factor / Second.Factor) * q.m_value); }
        public static explicit operator Week(Minute q) { return new Week((Week.Factor / Minute.Factor) * q.m_value); }
        public static explicit operator Week(Hour q) { return new Week((Week.Factor / Hour.Factor) * q.m_value); }
        public static explicit operator Week(Day q) { return new Week((Week.Factor / Day.Factor) * q.m_value); }
        public static explicit operator Week(Year q) { return new Week((Week.Factor / Year.Factor) * q.m_value); }
        public static explicit operator Week(Month q) { return new Week((Week.Factor / Month.Factor) * q.m_value); }
        public static Week From(IQuantity<double> q)
        {
            if (q.Unit.Family != Week.Family) throw new InvalidOperationException(string.Format("Cannot convert \"{0}\" to \"Week\"", q.GetType().Name));
            return new Week((Week.Factor / q.Unit.Factor) * q.Value);
        }
        #endregion

        #region IObject / IEquatable<Week>
        public override int GetHashCode() { return m_value.GetHashCode(); }
        public override bool /* IObject */ Equals(object obj) { return (obj is Week) && Equals((Week)obj); }
        public bool /* IEquatable<Week> */ Equals(Week other) { return this.m_value == other.m_value; }
        #endregion

        #region Comparison / IComparable<Week>
        public static bool operator ==(Week lhs, Week rhs) { return lhs.m_value == rhs.m_value; }
        public static bool operator !=(Week lhs, Week rhs) { return lhs.m_value != rhs.m_value; }
        public static bool operator <(Week lhs, Week rhs) { return lhs.m_value < rhs.m_value; }
        public static bool operator >(Week lhs, Week rhs) { return lhs.m_value > rhs.m_value; }
        public static bool operator <=(Week lhs, Week rhs) { return lhs.m_value <= rhs.m_value; }
        public static bool operator >=(Week lhs, Week rhs) { return lhs.m_value >= rhs.m_value; }
        public int /* IComparable<Week> */ CompareTo(Week other) { return this.m_value.CompareTo(other.m_value); }
        #endregion

        #region Arithmetic
        // Inner:
        public static Week operator +(Week lhs, Week rhs) { return new Week(lhs.m_value + rhs.m_value); }
        public static Week operator -(Week lhs, Week rhs) { return new Week(lhs.m_value - rhs.m_value); }
        public static Week operator ++(Week q) { return new Week(q.m_value + 1d); }
        public static Week operator --(Week q) { return new Week(q.m_value - 1d); }
        public static Week operator -(Week q) { return new Week(-q.m_value); }
        public static Week operator *(double lhs, Week rhs) { return new Week(lhs * rhs.m_value); }
        public static Week operator *(Week lhs, double rhs) { return new Week(lhs.m_value * rhs); }
        public static Week operator /(Week lhs, double rhs) { return new Week(lhs.m_value / rhs); }
        public static double operator /(Week lhs, Week rhs) { return lhs.m_value / rhs.m_value; }
        // Outer:
        #endregion

        #region Formatting
        public override string ToString() { return ToString(Week.Format, null); }
        public string ToString(string format) { return ToString(format, null); }
        public string ToString(IFormatProvider fp) { return ToString(Week.Format, fp); }
        public string /* IFormattable */ ToString(string format, IFormatProvider fp)
        {
            return string.Format(fp, format ?? Week.Format, m_value, Week.Symbol.Default);
        }
        #endregion

        #region Static fields
        private static readonly Dimension s_sense = Day.Sense;
        private static readonly int s_family = Second.Family;
        private static /*mutable*/ double s_factor = Day.Factor / 7d;
        private static /*mutable*/ string s_format = "{0} {1}";
        private static readonly SymbolCollection s_symbol = new SymbolCollection("wk");
        private static readonly Unit<double> s_proxy = new Week_Proxy();

        private static readonly Week s_one = new Week(1d);
        private static readonly Week s_zero = new Week(0d);
        #endregion

        #region Static Properties
        public static Dimension Sense { get { return s_sense; } }
        public static int Family { get { return s_family; } }
        public static double Factor { get { return s_factor; } set { s_factor = value; } }
        public static string Format { get { return s_format; } set { s_format = value; } }
        public static SymbolCollection Symbol { get { return s_symbol; } }
        public static Unit<double> Proxy { get { return s_proxy; } }

        public static Week One { get { return s_one; } }
        public static Week Zero { get { return s_zero; } }
        #endregion
    }

    public partial class Week_Proxy : Unit<double>
    {
        #region Properties
        public override int Family { get { return Week.Family; } }
        public override Dimension Sense { get { return Week.Sense; } }
        public override SymbolCollection Symbol { get { return Week.Symbol; } }
        public override double Factor { get { return Week.Factor; } set { Week.Factor = value; } }
        public override string Format { get { return Week.Format; } set { Week.Format = value; } }
        #endregion

        #region Constructor(s)
        public Week_Proxy() :
            base(typeof(Week))
        {
        }
        #endregion

        #region Methods
        public override IQuantity<double> Create(double value)
        {
            return new Week(value);
        }
        public override IQuantity<double> From(IQuantity<double> quantity)
        {
            return Week.From(quantity);
        }
        #endregion
    }
}
