/*******************************************************************************

    Units of Measurement for C# applications

    Copyright (C) Marek Aniola

    This program is provided to you under the terms of the license
    as published at https://github.com/mangh/unitsofmeasurement


********************************************************************************/
using System;

namespace UnitsOfMeasurement
{
    public partial struct Month : IQuantity<double>, IEquatable<Month>, IComparable<Month>, IFormattable
    {
        #region Fields
        internal readonly double m_value;
        #endregion

        #region Properties
        public double Value { get { return m_value; } }
        Unit<double> IQuantity<double>.Unit { get { return Month.Proxy; } }
        #endregion

        #region Constructor(s)
        public Month(double value)
        {
            m_value = value;
        }
        #endregion

        #region Conversions
        public static explicit operator Month(double q) { return new Month(q); }
        public static explicit operator Month(Week q) { return new Month((Month.Factor / Week.Factor) * q.m_value); }
        public static explicit operator Month(Second q) { return new Month((Month.Factor / Second.Factor) * q.m_value); }
        public static explicit operator Month(Minute q) { return new Month((Month.Factor / Minute.Factor) * q.m_value); }
        public static explicit operator Month(Hour q) { return new Month((Month.Factor / Hour.Factor) * q.m_value); }
        public static explicit operator Month(Day q) { return new Month((Month.Factor / Day.Factor) * q.m_value); }
        public static explicit operator Month(Year q) { return new Month((Month.Factor / Year.Factor) * q.m_value); }
        public static Month From(IQuantity<double> q)
        {
            if (q.Unit.Family != Month.Family) throw new InvalidOperationException(string.Format("Cannot convert \"{0}\" to \"Month\"", q.GetType().Name));
            return new Month((Month.Factor / q.Unit.Factor) * q.Value);
        }
        #endregion

        #region IObject / IEquatable<Month>
        public override int GetHashCode() { return m_value.GetHashCode(); }
        public override bool /* IObject */ Equals(object obj) { return (obj is Month) && Equals((Month)obj); }
        public bool /* IEquatable<Month> */ Equals(Month other) { return this.m_value == other.m_value; }
        #endregion

        #region Comparison / IComparable<Month>
        public static bool operator ==(Month lhs, Month rhs) { return lhs.m_value == rhs.m_value; }
        public static bool operator !=(Month lhs, Month rhs) { return lhs.m_value != rhs.m_value; }
        public static bool operator <(Month lhs, Month rhs) { return lhs.m_value < rhs.m_value; }
        public static bool operator >(Month lhs, Month rhs) { return lhs.m_value > rhs.m_value; }
        public static bool operator <=(Month lhs, Month rhs) { return lhs.m_value <= rhs.m_value; }
        public static bool operator >=(Month lhs, Month rhs) { return lhs.m_value >= rhs.m_value; }
        public int /* IComparable<Month> */ CompareTo(Month other) { return this.m_value.CompareTo(other.m_value); }
        #endregion

        #region Arithmetic
        // Inner:
        public static Month operator +(Month lhs, Month rhs) { return new Month(lhs.m_value + rhs.m_value); }
        public static Month operator -(Month lhs, Month rhs) { return new Month(lhs.m_value - rhs.m_value); }
        public static Month operator ++(Month q) { return new Month(q.m_value + 1d); }
        public static Month operator --(Month q) { return new Month(q.m_value - 1d); }
        public static Month operator -(Month q) { return new Month(-q.m_value); }
        public static Month operator *(double lhs, Month rhs) { return new Month(lhs * rhs.m_value); }
        public static Month operator *(Month lhs, double rhs) { return new Month(lhs.m_value * rhs); }
        public static Month operator /(Month lhs, double rhs) { return new Month(lhs.m_value / rhs); }
        public static double operator /(Month lhs, Month rhs) { return lhs.m_value / rhs.m_value; }
        // Outer:
        #endregion

        #region Formatting
        public override string ToString() { return ToString(Month.Format, null); }
        public string ToString(string format) { return ToString(format, null); }
        public string ToString(IFormatProvider fp) { return ToString(Month.Format, fp); }
        public string /* IFormattable */ ToString(string format, IFormatProvider fp)
        {
            return string.Format(fp, format ?? Month.Format, m_value, Month.Symbol.Default);
        }
        #endregion

        #region Static fields
        private static readonly Dimension s_sense = Day.Sense;
        private static readonly int s_family = Second.Family;
        private static /*mutable*/ double s_factor = Day.Factor / 30d;
        private static /*mutable*/ string s_format = "{0} {1}";
        private static readonly SymbolCollection s_symbol = new SymbolCollection("mo");
        private static readonly Unit<double> s_proxy = new Month_Proxy();

        private static readonly Month s_one = new Month(1d);
        private static readonly Month s_zero = new Month(0d);
        #endregion

        #region Static Properties
        public static Dimension Sense { get { return s_sense; } }
        public static int Family { get { return s_family; } }
        public static double Factor { get { return s_factor; } set { s_factor = value; } }
        public static string Format { get { return s_format; } set { s_format = value; } }
        public static SymbolCollection Symbol { get { return s_symbol; } }
        public static Unit<double> Proxy { get { return s_proxy; } }

        public static Month One { get { return s_one; } }
        public static Month Zero { get { return s_zero; } }
        #endregion
    }

    public partial class Month_Proxy : Unit<double>
    {
        #region Properties
        public override int Family { get { return Month.Family; } }
        public override Dimension Sense { get { return Month.Sense; } }
        public override SymbolCollection Symbol { get { return Month.Symbol; } }
        public override double Factor { get { return Month.Factor; } set { Month.Factor = value; } }
        public override string Format { get { return Month.Format; } set { Month.Format = value; } }
        #endregion

        #region Constructor(s)
        public Month_Proxy() :
            base(typeof(Month))
        {
        }
        #endregion

        #region Methods
        public override IQuantity<double> Create(double value)
        {
            return new Month(value);
        }
        public override IQuantity<double> From(IQuantity<double> quantity)
        {
            return Month.From(quantity);
        }
        #endregion
    }
}
