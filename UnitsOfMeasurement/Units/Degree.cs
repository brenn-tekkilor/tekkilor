/*******************************************************************************

    Units of Measurement for C# applications

    Copyright (C) Marek Aniola

    This program is provided to you under the terms of the license
    as published at https://github.com/mangh/unitsofmeasurement


********************************************************************************/
using System;

namespace UnitsOfMeasurement
{
    public partial struct Degree : IQuantity<double>, IEquatable<Degree>, IComparable<Degree>, IFormattable
    {
        #region Fields
        internal readonly double m_value;
        #endregion

        #region Properties
        public double Value { get { return m_value; } }
        Unit<double> IQuantity<double>.Unit { get { return Degree.Proxy; } }
        #endregion

        #region Constructor(s)
        public Degree(double value)
        {
            m_value = value;
        }
        #endregion

        #region Conversions
        public static explicit operator Degree(double q) { return new Degree(q); }
        public static explicit operator Degree(Radian q) { return new Degree((Degree.Factor / Radian.Factor) * q.m_value); }
        public static Degree From(IQuantity<double> q)
        {
            if (q.Unit.Family != Degree.Family) throw new InvalidOperationException(string.Format("Cannot convert \"{0}\" to \"Degree\"", q.GetType().Name));
            return new Degree((Degree.Factor / q.Unit.Factor) * q.Value);
        }
        #endregion

        #region IObject / IEquatable<Degree>
        public override int GetHashCode() { return m_value.GetHashCode(); }
        public override bool /* IObject */ Equals(object obj) { return (obj is Degree) && Equals((Degree)obj); }
        public bool /* IEquatable<Degree> */ Equals(Degree other) { return this.m_value == other.m_value; }
        #endregion

        #region Comparison / IComparable<Degree>
        public static bool operator ==(Degree lhs, Degree rhs) { return lhs.m_value == rhs.m_value; }
        public static bool operator !=(Degree lhs, Degree rhs) { return lhs.m_value != rhs.m_value; }
        public static bool operator <(Degree lhs, Degree rhs) { return lhs.m_value < rhs.m_value; }
        public static bool operator >(Degree lhs, Degree rhs) { return lhs.m_value > rhs.m_value; }
        public static bool operator <=(Degree lhs, Degree rhs) { return lhs.m_value <= rhs.m_value; }
        public static bool operator >=(Degree lhs, Degree rhs) { return lhs.m_value >= rhs.m_value; }
        public int /* IComparable<Degree> */ CompareTo(Degree other) { return this.m_value.CompareTo(other.m_value); }
        #endregion

        #region Arithmetic
        // Inner:
        public static Degree operator +(Degree lhs, Degree rhs) { return new Degree(lhs.m_value + rhs.m_value); }
        public static Degree operator -(Degree lhs, Degree rhs) { return new Degree(lhs.m_value - rhs.m_value); }
        public static Degree operator ++(Degree q) { return new Degree(q.m_value + 1d); }
        public static Degree operator --(Degree q) { return new Degree(q.m_value - 1d); }
        public static Degree operator -(Degree q) { return new Degree(-q.m_value); }
        public static Degree operator *(double lhs, Degree rhs) { return new Degree(lhs * rhs.m_value); }
        public static Degree operator *(Degree lhs, double rhs) { return new Degree(lhs.m_value * rhs); }
        public static Degree operator /(Degree lhs, double rhs) { return new Degree(lhs.m_value / rhs); }
        public static double operator /(Degree lhs, Degree rhs) { return lhs.m_value / rhs.m_value; }
        // Outer:
        #endregion

        #region Formatting
        public override string ToString() { return ToString(Degree.Format, null); }
        public string ToString(string format) { return ToString(format, null); }
        public string ToString(IFormatProvider fp) { return ToString(Degree.Format, fp); }
        public string /* IFormattable */ ToString(string format, IFormatProvider fp)
        {
            return string.Format(fp, format ?? Degree.Format, m_value, Degree.Symbol.Default);
        }
        #endregion

        #region Static fields
        private static readonly Dimension s_sense = Radian.Sense;
        private static readonly int s_family = Radian.Family;
        private static /*mutable*/ double s_factor = (180d / Math.PI) * Radian.Factor;
        private static /*mutable*/ string s_format = "{0}{1}";
        private static readonly SymbolCollection s_symbol = new SymbolCollection("\u00B0", "deg");
        private static readonly Unit<double> s_proxy = new Degree_Proxy();

        private static readonly Degree s_one = new Degree(1d);
        private static readonly Degree s_zero = new Degree(0d);
        #endregion

        #region Static Properties
        public static Dimension Sense { get { return s_sense; } }
        public static int Family { get { return s_family; } }
        public static double Factor { get { return s_factor; } set { s_factor = value; } }
        public static string Format { get { return s_format; } set { s_format = value; } }
        public static SymbolCollection Symbol { get { return s_symbol; } }
        public static Unit<double> Proxy { get { return s_proxy; } }

        public static Degree One { get { return s_one; } }
        public static Degree Zero { get { return s_zero; } }
        #endregion
    }

    public partial class Degree_Proxy : Unit<double>
    {
        #region Properties
        public override int Family { get { return Degree.Family; } }
        public override Dimension Sense { get { return Degree.Sense; } }
        public override SymbolCollection Symbol { get { return Degree.Symbol; } }
        public override double Factor { get { return Degree.Factor; } set { Degree.Factor = value; } }
        public override string Format { get { return Degree.Format; } set { Degree.Format = value; } }
        #endregion

        #region Constructor(s)
        public Degree_Proxy() :
            base(typeof(Degree))
        {
        }
        #endregion

        #region Methods
        public override IQuantity<double> Create(double value)
        {
            return new Degree(value);
        }
        public override IQuantity<double> From(IQuantity<double> quantity)
        {
            return Degree.From(quantity);
        }
        #endregion
    }
}
