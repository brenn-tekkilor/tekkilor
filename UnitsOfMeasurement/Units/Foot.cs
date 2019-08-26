/*******************************************************************************

    Units of Measurement for C# applications

    Copyright (C) Marek Aniola

    This program is provided to you under the terms of the license
    as published at https://github.com/mangh/unitsofmeasurement


********************************************************************************/
using System;

namespace UnitsOfMeasurement
{
    public partial struct Foot : IQuantity<double>, IEquatable<Foot>, IComparable<Foot>, IFormattable
    {
        #region Fields
        internal readonly double m_value;
        #endregion

        #region Properties
        public double Value { get { return m_value; } }
        Unit<double> IQuantity<double>.Unit { get { return Foot.Proxy; } }
        #endregion

        #region Constructor(s)
        public Foot(double value)
        {
            m_value = value;
        }
        #endregion

        #region Conversions
        public static explicit operator Foot(double q) { return new Foot(q); }
        public static explicit operator Foot(Yard q) { return new Foot((Foot.Factor / Yard.Factor) * q.m_value); }
        public static explicit operator Foot(Mile q) { return new Foot((Foot.Factor / Mile.Factor) * q.m_value); }
        public static explicit operator Foot(Millimeter q) { return new Foot((Foot.Factor / Millimeter.Factor) * q.m_value); }
        public static explicit operator Foot(Centimeter q) { return new Foot((Foot.Factor / Centimeter.Factor) * q.m_value); }
        public static explicit operator Foot(Meter q) { return new Foot((Foot.Factor / Meter.Factor) * q.m_value); }
        public static explicit operator Foot(Inch q) { return new Foot((Foot.Factor / Inch.Factor) * q.m_value); }
        public static explicit operator Foot(Point q) { return new Foot((Foot.Factor / Point.Factor) * q.m_value); }
        public static explicit operator Foot(Pica q) { return new Foot((Foot.Factor / Pica.Factor) * q.m_value); }
        public static Foot From(IQuantity<double> q)
        {
            if (q.Unit.Family != Foot.Family) throw new InvalidOperationException(string.Format("Cannot convert \"{0}\" to \"Foot\"", q.GetType().Name));
            return new Foot((Foot.Factor / q.Unit.Factor) * q.Value);
        }
        #endregion

        #region IObject / IEquatable<Foot>
        public override int GetHashCode() { return m_value.GetHashCode(); }
        public override bool /* IObject */ Equals(object obj) { return (obj is Foot) && Equals((Foot)obj); }
        public bool /* IEquatable<Foot> */ Equals(Foot other) { return this.m_value == other.m_value; }
        #endregion

        #region Comparison / IComparable<Foot>
        public static bool operator ==(Foot lhs, Foot rhs) { return lhs.m_value == rhs.m_value; }
        public static bool operator !=(Foot lhs, Foot rhs) { return lhs.m_value != rhs.m_value; }
        public static bool operator <(Foot lhs, Foot rhs) { return lhs.m_value < rhs.m_value; }
        public static bool operator >(Foot lhs, Foot rhs) { return lhs.m_value > rhs.m_value; }
        public static bool operator <=(Foot lhs, Foot rhs) { return lhs.m_value <= rhs.m_value; }
        public static bool operator >=(Foot lhs, Foot rhs) { return lhs.m_value >= rhs.m_value; }
        public int /* IComparable<Foot> */ CompareTo(Foot other) { return this.m_value.CompareTo(other.m_value); }
        #endregion

        #region Arithmetic
        // Inner:
        public static Foot operator +(Foot lhs, Foot rhs) { return new Foot(lhs.m_value + rhs.m_value); }
        public static Foot operator -(Foot lhs, Foot rhs) { return new Foot(lhs.m_value - rhs.m_value); }
        public static Foot operator ++(Foot q) { return new Foot(q.m_value + 1d); }
        public static Foot operator --(Foot q) { return new Foot(q.m_value - 1d); }
        public static Foot operator -(Foot q) { return new Foot(-q.m_value); }
        public static Foot operator *(double lhs, Foot rhs) { return new Foot(lhs * rhs.m_value); }
        public static Foot operator *(Foot lhs, double rhs) { return new Foot(lhs.m_value * rhs); }
        public static Foot operator /(Foot lhs, double rhs) { return new Foot(lhs.m_value / rhs); }
        public static double operator /(Foot lhs, Foot rhs) { return lhs.m_value / rhs.m_value; }
        // Outer:
        public static SquareFoot operator *(Foot lhs, Foot rhs) { return new SquareFoot(lhs.m_value * rhs.m_value); }
        #endregion

        #region Formatting
        public override string ToString() { return ToString(Foot.Format, null); }
        public string ToString(string format) { return ToString(format, null); }
        public string ToString(IFormatProvider fp) { return ToString(Foot.Format, fp); }
        public string /* IFormattable */ ToString(string format, IFormatProvider fp)
        {
            return string.Format(fp, format ?? Foot.Format, m_value, Foot.Symbol.Default);
        }
        #endregion

        #region Static fields
        private static readonly Dimension s_sense = Inch.Sense;
        private static readonly int s_family = Meter.Family;
        private static /*mutable*/ double s_factor = Inch.Factor / 12d;
        private static /*mutable*/ string s_format = "{0} {1}";
        private static readonly SymbolCollection s_symbol = new SymbolCollection("ft");
        private static readonly Unit<double> s_proxy = new Foot_Proxy();

        private static readonly Foot s_one = new Foot(1d);
        private static readonly Foot s_zero = new Foot(0d);
        #endregion

        #region Static Properties
        public static Dimension Sense { get { return s_sense; } }
        public static int Family { get { return s_family; } }
        public static double Factor { get { return s_factor; } set { s_factor = value; } }
        public static string Format { get { return s_format; } set { s_format = value; } }
        public static SymbolCollection Symbol { get { return s_symbol; } }
        public static Unit<double> Proxy { get { return s_proxy; } }

        public static Foot One { get { return s_one; } }
        public static Foot Zero { get { return s_zero; } }
        #endregion
    }

    public partial class Foot_Proxy : Unit<double>
    {
        #region Properties
        public override int Family { get { return Foot.Family; } }
        public override Dimension Sense { get { return Foot.Sense; } }
        public override SymbolCollection Symbol { get { return Foot.Symbol; } }
        public override double Factor { get { return Foot.Factor; } set { Foot.Factor = value; } }
        public override string Format { get { return Foot.Format; } set { Foot.Format = value; } }
        #endregion

        #region Constructor(s)
        public Foot_Proxy() :
            base(typeof(Foot))
        {
        }
        #endregion

        #region Methods
        public override IQuantity<double> Create(double value)
        {
            return new Foot(value);
        }
        public override IQuantity<double> From(IQuantity<double> quantity)
        {
            return Foot.From(quantity);
        }
        #endregion
    }
}
