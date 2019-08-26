/*******************************************************************************

    Units of Measurement for C# applications

    Copyright (C) Marek Aniola

    This program is provided to you under the terms of the license
    as published at https://github.com/mangh/unitsofmeasurement


********************************************************************************/
using System;

namespace UnitsOfMeasurement
{
    public partial struct Pica : IQuantity<double>, IEquatable<Pica>, IComparable<Pica>, IFormattable
    {
        #region Fields
        internal readonly double m_value;
        #endregion

        #region Properties
        public double Value { get { return m_value; } }
        Unit<double> IQuantity<double>.Unit { get { return Pica.Proxy; } }
        #endregion

        #region Constructor(s)
        public Pica(double value)
        {
            m_value = value;
        }
        #endregion

        #region Conversions
        public static explicit operator Pica(double q) { return new Pica(q); }
        public static explicit operator Pica(Foot q) { return new Pica((Pica.Factor / Foot.Factor) * q.m_value); }
        public static explicit operator Pica(Yard q) { return new Pica((Pica.Factor / Yard.Factor) * q.m_value); }
        public static explicit operator Pica(Mile q) { return new Pica((Pica.Factor / Mile.Factor) * q.m_value); }
        public static explicit operator Pica(Millimeter q) { return new Pica((Pica.Factor / Millimeter.Factor) * q.m_value); }
        public static explicit operator Pica(Centimeter q) { return new Pica((Pica.Factor / Centimeter.Factor) * q.m_value); }
        public static explicit operator Pica(Meter q) { return new Pica((Pica.Factor / Meter.Factor) * q.m_value); }
        public static explicit operator Pica(Inch q) { return new Pica((Pica.Factor / Inch.Factor) * q.m_value); }
        public static explicit operator Pica(Point q) { return new Pica((Pica.Factor / Point.Factor) * q.m_value); }
        public static Pica From(IQuantity<double> q)
        {
            if (q.Unit.Family != Pica.Family) throw new InvalidOperationException(string.Format("Cannot convert \"{0}\" to \"Pica\"", q.GetType().Name));
            return new Pica((Pica.Factor / q.Unit.Factor) * q.Value);
        }
        #endregion

        #region IObject / IEquatable<Pica>
        public override int GetHashCode() { return m_value.GetHashCode(); }
        public override bool /* IObject */ Equals(object obj) { return (obj is Pica) && Equals((Pica)obj); }
        public bool /* IEquatable<Pica> */ Equals(Pica other) { return this.m_value == other.m_value; }
        #endregion

        #region Comparison / IComparable<Pica>
        public static bool operator ==(Pica lhs, Pica rhs) { return lhs.m_value == rhs.m_value; }
        public static bool operator !=(Pica lhs, Pica rhs) { return lhs.m_value != rhs.m_value; }
        public static bool operator <(Pica lhs, Pica rhs) { return lhs.m_value < rhs.m_value; }
        public static bool operator >(Pica lhs, Pica rhs) { return lhs.m_value > rhs.m_value; }
        public static bool operator <=(Pica lhs, Pica rhs) { return lhs.m_value <= rhs.m_value; }
        public static bool operator >=(Pica lhs, Pica rhs) { return lhs.m_value >= rhs.m_value; }
        public int /* IComparable<Pica> */ CompareTo(Pica other) { return this.m_value.CompareTo(other.m_value); }
        #endregion

        #region Arithmetic
        // Inner:
        public static Pica operator +(Pica lhs, Pica rhs) { return new Pica(lhs.m_value + rhs.m_value); }
        public static Pica operator -(Pica lhs, Pica rhs) { return new Pica(lhs.m_value - rhs.m_value); }
        public static Pica operator ++(Pica q) { return new Pica(q.m_value + 1d); }
        public static Pica operator --(Pica q) { return new Pica(q.m_value - 1d); }
        public static Pica operator -(Pica q) { return new Pica(-q.m_value); }
        public static Pica operator *(double lhs, Pica rhs) { return new Pica(lhs * rhs.m_value); }
        public static Pica operator *(Pica lhs, double rhs) { return new Pica(lhs.m_value * rhs); }
        public static Pica operator /(Pica lhs, double rhs) { return new Pica(lhs.m_value / rhs); }
        public static double operator /(Pica lhs, Pica rhs) { return lhs.m_value / rhs.m_value; }
        // Outer:
        #endregion

        #region Formatting
        public override string ToString() { return ToString(Pica.Format, null); }
        public string ToString(string format) { return ToString(format, null); }
        public string ToString(IFormatProvider fp) { return ToString(Pica.Format, fp); }
        public string /* IFormattable */ ToString(string format, IFormatProvider fp)
        {
            return string.Format(fp, format ?? Pica.Format, m_value, Pica.Symbol.Default);
        }
        #endregion

        #region Static fields
        private static readonly Dimension s_sense = Point.Sense;
        private static readonly int s_family = Meter.Family;
        private static /*mutable*/ double s_factor = Point.Factor / 12d;
        private static /*mutable*/ string s_format = "{0} {1}";
        private static readonly SymbolCollection s_symbol = new SymbolCollection("pc");
        private static readonly Unit<double> s_proxy = new Pica_Proxy();

        private static readonly Pica s_one = new Pica(1d);
        private static readonly Pica s_zero = new Pica(0d);
        #endregion

        #region Static Properties
        public static Dimension Sense { get { return s_sense; } }
        public static int Family { get { return s_family; } }
        public static double Factor { get { return s_factor; } set { s_factor = value; } }
        public static string Format { get { return s_format; } set { s_format = value; } }
        public static SymbolCollection Symbol { get { return s_symbol; } }
        public static Unit<double> Proxy { get { return s_proxy; } }

        public static Pica One { get { return s_one; } }
        public static Pica Zero { get { return s_zero; } }
        #endregion
    }

    public partial class Pica_Proxy : Unit<double>
    {
        #region Properties
        public override int Family { get { return Pica.Family; } }
        public override Dimension Sense { get { return Pica.Sense; } }
        public override SymbolCollection Symbol { get { return Pica.Symbol; } }
        public override double Factor { get { return Pica.Factor; } set { Pica.Factor = value; } }
        public override string Format { get { return Pica.Format; } set { Pica.Format = value; } }
        #endregion

        #region Constructor(s)
        public Pica_Proxy() :
            base(typeof(Pica))
        {
        }
        #endregion

        #region Methods
        public override IQuantity<double> Create(double value)
        {
            return new Pica(value);
        }
        public override IQuantity<double> From(IQuantity<double> quantity)
        {
            return Pica.From(quantity);
        }
        #endregion
    }
}
