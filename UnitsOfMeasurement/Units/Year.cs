/*******************************************************************************

    Units of Measurement for C# applications

    Copyright (C) Marek Aniola

    This program is provided to you under the terms of the license
    as published at https://github.com/mangh/unitsofmeasurement


********************************************************************************/
using System;

namespace UnitsOfMeasurement
{
    public partial struct Year : IQuantity<double>, IEquatable<Year>, IComparable<Year>, IFormattable
    {
        #region Fields
        internal readonly double m_value;
        #endregion

        #region Properties
        public double Value { get { return m_value; } }
        Unit<double> IQuantity<double>.Unit { get { return Year.Proxy; } }
        #endregion

        #region Constructor(s)
        public Year(double value)
        {
            m_value = value;
        }
        #endregion

        #region Conversions
        public static explicit operator Year(double q) { return new Year(q); }
        public static explicit operator Year(Month q) { return new Year((Year.Factor / Month.Factor) * q.m_value); }
        public static explicit operator Year(Week q) { return new Year((Year.Factor / Week.Factor) * q.m_value); }
        public static explicit operator Year(Second q) { return new Year((Year.Factor / Second.Factor) * q.m_value); }
        public static explicit operator Year(Minute q) { return new Year((Year.Factor / Minute.Factor) * q.m_value); }
        public static explicit operator Year(Hour q) { return new Year((Year.Factor / Hour.Factor) * q.m_value); }
        public static explicit operator Year(Day q) { return new Year((Year.Factor / Day.Factor) * q.m_value); }
        public static Year From(IQuantity<double> q)
        {
            if (q.Unit.Family != Year.Family) throw new InvalidOperationException(string.Format("Cannot convert \"{0}\" to \"Year\"", q.GetType().Name));
            return new Year((Year.Factor / q.Unit.Factor) * q.Value);
        }
        #endregion

        #region IObject / IEquatable<Year>
        public override int GetHashCode() { return m_value.GetHashCode(); }
        public override bool /* IObject */ Equals(object obj) { return (obj is Year) && Equals((Year)obj); }
        public bool /* IEquatable<Year> */ Equals(Year other) { return this.m_value == other.m_value; }
        #endregion

        #region Comparison / IComparable<Year>
        public static bool operator ==(Year lhs, Year rhs) { return lhs.m_value == rhs.m_value; }
        public static bool operator !=(Year lhs, Year rhs) { return lhs.m_value != rhs.m_value; }
        public static bool operator <(Year lhs, Year rhs) { return lhs.m_value < rhs.m_value; }
        public static bool operator >(Year lhs, Year rhs) { return lhs.m_value > rhs.m_value; }
        public static bool operator <=(Year lhs, Year rhs) { return lhs.m_value <= rhs.m_value; }
        public static bool operator >=(Year lhs, Year rhs) { return lhs.m_value >= rhs.m_value; }
        public int /* IComparable<Year> */ CompareTo(Year other) { return this.m_value.CompareTo(other.m_value); }
        #endregion

        #region Arithmetic
        // Inner:
        public static Year operator +(Year lhs, Year rhs) { return new Year(lhs.m_value + rhs.m_value); }
        public static Year operator -(Year lhs, Year rhs) { return new Year(lhs.m_value - rhs.m_value); }
        public static Year operator ++(Year q) { return new Year(q.m_value + 1d); }
        public static Year operator --(Year q) { return new Year(q.m_value - 1d); }
        public static Year operator -(Year q) { return new Year(-q.m_value); }
        public static Year operator *(double lhs, Year rhs) { return new Year(lhs * rhs.m_value); }
        public static Year operator *(Year lhs, double rhs) { return new Year(lhs.m_value * rhs); }
        public static Year operator /(Year lhs, double rhs) { return new Year(lhs.m_value / rhs); }
        public static double operator /(Year lhs, Year rhs) { return lhs.m_value / rhs.m_value; }
        // Outer:
        #endregion

        #region Formatting
        public override string ToString() { return ToString(Year.Format, null); }
        public string ToString(string format) { return ToString(format, null); }
        public string ToString(IFormatProvider fp) { return ToString(Year.Format, fp); }
        public string /* IFormattable */ ToString(string format, IFormatProvider fp)
        {
            return string.Format(fp, format ?? Year.Format, m_value, Year.Symbol.Default);
        }
        #endregion

        #region Static fields
        private static readonly Dimension s_sense = Day.Sense;
        private static readonly int s_family = Second.Family;
        private static /*mutable*/ double s_factor = Day.Factor / 365d;
        private static /*mutable*/ string s_format = "{0} {1}";
        private static readonly SymbolCollection s_symbol = new SymbolCollection("yr");
        private static readonly Unit<double> s_proxy = new Year_Proxy();

        private static readonly Year s_one = new Year(1d);
        private static readonly Year s_zero = new Year(0d);
        #endregion

        #region Static Properties
        public static Dimension Sense { get { return s_sense; } }
        public static int Family { get { return s_family; } }
        public static double Factor { get { return s_factor; } set { s_factor = value; } }
        public static string Format { get { return s_format; } set { s_format = value; } }
        public static SymbolCollection Symbol { get { return s_symbol; } }
        public static Unit<double> Proxy { get { return s_proxy; } }

        public static Year One { get { return s_one; } }
        public static Year Zero { get { return s_zero; } }
        #endregion
    }

    public partial class Year_Proxy : Unit<double>
    {
        #region Properties
        public override int Family { get { return Year.Family; } }
        public override Dimension Sense { get { return Year.Sense; } }
        public override SymbolCollection Symbol { get { return Year.Symbol; } }
        public override double Factor { get { return Year.Factor; } set { Year.Factor = value; } }
        public override string Format { get { return Year.Format; } set { Year.Format = value; } }
        #endregion

        #region Constructor(s)
        public Year_Proxy() :
            base(typeof(Year))
        {
        }
        #endregion

        #region Methods
        public override IQuantity<double> Create(double value)
        {
            return new Year(value);
        }
        public override IQuantity<double> From(IQuantity<double> quantity)
        {
            return Year.From(quantity);
        }
        #endregion
    }
}
