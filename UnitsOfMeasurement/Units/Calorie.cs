/*******************************************************************************

    Units of Measurement for C# applications

    Copyright (C) Marek Aniola

    This program is provided to you under the terms of the license
    as published at https://github.com/mangh/unitsofmeasurement


********************************************************************************/
using System;

namespace UnitsOfMeasurement
{
    public partial struct Calorie : IQuantity<double>, IEquatable<Calorie>, IComparable<Calorie>, IFormattable
    {
        #region Fields
        internal readonly double m_value;
        #endregion

        #region Properties
        public double Value { get { return m_value; } }
        Unit<double> IQuantity<double>.Unit { get { return Calorie.Proxy; } }
        #endregion

        #region Constructor(s)
        public Calorie(double value)
        {
            m_value = value;
        }
        #endregion

        #region Conversions
        public static explicit operator Calorie(double q) { return new Calorie(q); }
        public static explicit operator Calorie(Joule q) { return new Calorie((Calorie.Factor / Joule.Factor) * q.m_value); }
        public static explicit operator Calorie(BTU q) { return new Calorie((Calorie.Factor / BTU.Factor) * q.m_value); }
        public static Calorie From(IQuantity<double> q)
        {
            if (q.Unit.Family != Calorie.Family) throw new InvalidOperationException(string.Format("Cannot convert \"{0}\" to \"Calorie\"", q.GetType().Name));
            return new Calorie((Calorie.Factor / q.Unit.Factor) * q.Value);
        }
        #endregion

        #region IObject / IEquatable<Calorie>
        public override int GetHashCode() { return m_value.GetHashCode(); }
        public override bool /* IObject */ Equals(object obj) { return (obj is Calorie) && Equals((Calorie)obj); }
        public bool /* IEquatable<Calorie> */ Equals(Calorie other) { return this.m_value == other.m_value; }
        #endregion

        #region Comparison / IComparable<Calorie>
        public static bool operator ==(Calorie lhs, Calorie rhs) { return lhs.m_value == rhs.m_value; }
        public static bool operator !=(Calorie lhs, Calorie rhs) { return lhs.m_value != rhs.m_value; }
        public static bool operator <(Calorie lhs, Calorie rhs) { return lhs.m_value < rhs.m_value; }
        public static bool operator >(Calorie lhs, Calorie rhs) { return lhs.m_value > rhs.m_value; }
        public static bool operator <=(Calorie lhs, Calorie rhs) { return lhs.m_value <= rhs.m_value; }
        public static bool operator >=(Calorie lhs, Calorie rhs) { return lhs.m_value >= rhs.m_value; }
        public int /* IComparable<Calorie> */ CompareTo(Calorie other) { return this.m_value.CompareTo(other.m_value); }
        #endregion

        #region Arithmetic
        // Inner:
        public static Calorie operator +(Calorie lhs, Calorie rhs) { return new Calorie(lhs.m_value + rhs.m_value); }
        public static Calorie operator -(Calorie lhs, Calorie rhs) { return new Calorie(lhs.m_value - rhs.m_value); }
        public static Calorie operator ++(Calorie q) { return new Calorie(q.m_value + 1d); }
        public static Calorie operator --(Calorie q) { return new Calorie(q.m_value - 1d); }
        public static Calorie operator -(Calorie q) { return new Calorie(-q.m_value); }
        public static Calorie operator *(double lhs, Calorie rhs) { return new Calorie(lhs * rhs.m_value); }
        public static Calorie operator *(Calorie lhs, double rhs) { return new Calorie(lhs.m_value * rhs); }
        public static Calorie operator /(Calorie lhs, double rhs) { return new Calorie(lhs.m_value / rhs); }
        public static double operator /(Calorie lhs, Calorie rhs) { return lhs.m_value / rhs.m_value; }
        // Outer:
        #endregion

        #region Formatting
        public override string ToString() { return ToString(Calorie.Format, null); }
        public string ToString(string format) { return ToString(format, null); }
        public string ToString(IFormatProvider fp) { return ToString(Calorie.Format, fp); }
        public string /* IFormattable */ ToString(string format, IFormatProvider fp)
        {
            return string.Format(fp, format ?? Calorie.Format, m_value, Calorie.Symbol.Default);
        }
        #endregion

        #region Static fields
        private static readonly Dimension s_sense = Joule.Sense;
        private static readonly int s_family = Joule.Family;
        private static /*mutable*/ double s_factor = Joule.Factor / 4.1868d;
        private static /*mutable*/ string s_format = "{0} {1}";
        private static readonly SymbolCollection s_symbol = new SymbolCollection("cal");
        private static readonly Unit<double> s_proxy = new Calorie_Proxy();

        private static readonly Calorie s_one = new Calorie(1d);
        private static readonly Calorie s_zero = new Calorie(0d);
        #endregion

        #region Static Properties
        public static Dimension Sense { get { return s_sense; } }
        public static int Family { get { return s_family; } }
        public static double Factor { get { return s_factor; } set { s_factor = value; } }
        public static string Format { get { return s_format; } set { s_format = value; } }
        public static SymbolCollection Symbol { get { return s_symbol; } }
        public static Unit<double> Proxy { get { return s_proxy; } }

        public static Calorie One { get { return s_one; } }
        public static Calorie Zero { get { return s_zero; } }
        #endregion
    }

    public partial class Calorie_Proxy : Unit<double>
    {
        #region Properties
        public override int Family { get { return Calorie.Family; } }
        public override Dimension Sense { get { return Calorie.Sense; } }
        public override SymbolCollection Symbol { get { return Calorie.Symbol; } }
        public override double Factor { get { return Calorie.Factor; } set { Calorie.Factor = value; } }
        public override string Format { get { return Calorie.Format; } set { Calorie.Format = value; } }
        #endregion

        #region Constructor(s)
        public Calorie_Proxy() :
            base(typeof(Calorie))
        {
        }
        #endregion

        #region Methods
        public override IQuantity<double> Create(double value)
        {
            return new Calorie(value);
        }
        public override IQuantity<double> From(IQuantity<double> quantity)
        {
            return Calorie.From(quantity);
        }
        #endregion
    }
}
