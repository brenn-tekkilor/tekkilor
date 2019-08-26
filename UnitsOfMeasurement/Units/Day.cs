/*******************************************************************************

    Units of Measurement for C# applications

    Copyright (C) Marek Aniola

    This program is provided to you under the terms of the license
    as published at https://github.com/mangh/unitsofmeasurement


********************************************************************************/
using System;

namespace UnitsOfMeasurement
{
    public partial struct Day : IQuantity<double>, IEquatable<Day>, IComparable<Day>, IFormattable
    {
        #region Fields
        internal readonly double m_value;
        #endregion

        #region Properties
        public double Value { get { return m_value; } }
        Unit<double> IQuantity<double>.Unit { get { return Day.Proxy; } }
        #endregion

        #region Constructor(s)
        public Day(double value)
        {
            m_value = value;
        }
        #endregion

        #region Conversions
        public static explicit operator Day(double q) { return new Day(q); }
        public static explicit operator Day(Year q) { return new Day((Day.Factor / Year.Factor) * q.m_value); }
        public static explicit operator Day(Month q) { return new Day((Day.Factor / Month.Factor) * q.m_value); }
        public static explicit operator Day(Week q) { return new Day((Day.Factor / Week.Factor) * q.m_value); }
        public static explicit operator Day(Second q) { return new Day((Day.Factor / Second.Factor) * q.m_value); }
        public static explicit operator Day(Minute q) { return new Day((Day.Factor / Minute.Factor) * q.m_value); }
        public static explicit operator Day(Hour q) { return new Day((Day.Factor / Hour.Factor) * q.m_value); }
        public static Day From(IQuantity<double> q)
        {
            if (q.Unit.Family != Day.Family) throw new InvalidOperationException(string.Format("Cannot convert \"{0}\" to \"Day\"", q.GetType().Name));
            return new Day((Day.Factor / q.Unit.Factor) * q.Value);
        }
        #endregion

        #region IObject / IEquatable<Day>
        public override int GetHashCode() { return m_value.GetHashCode(); }
        public override bool /* IObject */ Equals(object obj) { return (obj is Day) && Equals((Day)obj); }
        public bool /* IEquatable<Day> */ Equals(Day other) { return this.m_value == other.m_value; }
        #endregion

        #region Comparison / IComparable<Day>
        public static bool operator ==(Day lhs, Day rhs) { return lhs.m_value == rhs.m_value; }
        public static bool operator !=(Day lhs, Day rhs) { return lhs.m_value != rhs.m_value; }
        public static bool operator <(Day lhs, Day rhs) { return lhs.m_value < rhs.m_value; }
        public static bool operator >(Day lhs, Day rhs) { return lhs.m_value > rhs.m_value; }
        public static bool operator <=(Day lhs, Day rhs) { return lhs.m_value <= rhs.m_value; }
        public static bool operator >=(Day lhs, Day rhs) { return lhs.m_value >= rhs.m_value; }
        public int /* IComparable<Day> */ CompareTo(Day other) { return this.m_value.CompareTo(other.m_value); }
        #endregion

        #region Arithmetic
        // Inner:
        public static Day operator +(Day lhs, Day rhs) { return new Day(lhs.m_value + rhs.m_value); }
        public static Day operator -(Day lhs, Day rhs) { return new Day(lhs.m_value - rhs.m_value); }
        public static Day operator ++(Day q) { return new Day(q.m_value + 1d); }
        public static Day operator --(Day q) { return new Day(q.m_value - 1d); }
        public static Day operator -(Day q) { return new Day(-q.m_value); }
        public static Day operator *(double lhs, Day rhs) { return new Day(lhs * rhs.m_value); }
        public static Day operator *(Day lhs, double rhs) { return new Day(lhs.m_value * rhs); }
        public static Day operator /(Day lhs, double rhs) { return new Day(lhs.m_value / rhs); }
        public static double operator /(Day lhs, Day rhs) { return lhs.m_value / rhs.m_value; }
        // Outer:
        #endregion

        #region Formatting
        public override string ToString() { return ToString(Day.Format, null); }
        public string ToString(string format) { return ToString(format, null); }
        public string ToString(IFormatProvider fp) { return ToString(Day.Format, fp); }
        public string /* IFormattable */ ToString(string format, IFormatProvider fp)
        {
            return string.Format(fp, format ?? Day.Format, m_value, Day.Symbol.Default);
        }
        #endregion

        #region Static fields
        private static readonly Dimension s_sense = Hour.Sense;
        private static readonly int s_family = Second.Family;
        private static /*mutable*/ double s_factor = Hour.Factor / 24d;
        private static /*mutable*/ string s_format = "{0} {1}";
        private static readonly SymbolCollection s_symbol = new SymbolCollection("d");
        private static readonly Unit<double> s_proxy = new Day_Proxy();

        private static readonly Day s_one = new Day(1d);
        private static readonly Day s_zero = new Day(0d);
        #endregion

        #region Static Properties
        public static Dimension Sense { get { return s_sense; } }
        public static int Family { get { return s_family; } }
        public static double Factor { get { return s_factor; } set { s_factor = value; } }
        public static string Format { get { return s_format; } set { s_format = value; } }
        public static SymbolCollection Symbol { get { return s_symbol; } }
        public static Unit<double> Proxy { get { return s_proxy; } }

        public static Day One { get { return s_one; } }
        public static Day Zero { get { return s_zero; } }
        #endregion
    }

    public partial class Day_Proxy : Unit<double>
    {
        #region Properties
        public override int Family { get { return Day.Family; } }
        public override Dimension Sense { get { return Day.Sense; } }
        public override SymbolCollection Symbol { get { return Day.Symbol; } }
        public override double Factor { get { return Day.Factor; } set { Day.Factor = value; } }
        public override string Format { get { return Day.Format; } set { Day.Format = value; } }
        #endregion

        #region Constructor(s)
        public Day_Proxy() :
            base(typeof(Day))
        {
        }
        #endregion

        #region Methods
        public override IQuantity<double> Create(double value)
        {
            return new Day(value);
        }
        public override IQuantity<double> From(IQuantity<double> quantity)
        {
            return Day.From(quantity);
        }
        #endregion
    }
}
