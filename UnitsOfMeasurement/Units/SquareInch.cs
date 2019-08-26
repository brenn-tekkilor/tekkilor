/*******************************************************************************

    Units of Measurement for C# applications

    Copyright (C) Marek Aniola

    This program is provided to you under the terms of the license
    as published at https://github.com/mangh/unitsofmeasurement


********************************************************************************/
using System;

namespace UnitsOfMeasurement
{
    public partial struct SquareInch : IQuantity<double>, IEquatable<SquareInch>, IComparable<SquareInch>, IFormattable
    {
        #region Fields
        internal readonly double m_value;
        #endregion

        #region Properties
        public double Value { get { return m_value; } }
        Unit<double> IQuantity<double>.Unit { get { return SquareInch.Proxy; } }
        #endregion

        #region Constructor(s)
        public SquareInch(double value)
        {
            m_value = value;
        }
        #endregion

        #region Conversions
        public static explicit operator SquareInch(double q) { return new SquareInch(q); }
        public static explicit operator SquareInch(SquareCentimeter q) { return new SquareInch((SquareInch.Factor / SquareCentimeter.Factor) * q.m_value); }
        public static explicit operator SquareInch(SquareMeter q) { return new SquareInch((SquareInch.Factor / SquareMeter.Factor) * q.m_value); }
        public static explicit operator SquareInch(SquareMile q) { return new SquareInch((SquareInch.Factor / SquareMile.Factor) * q.m_value); }
        public static explicit operator SquareInch(SquareYard q) { return new SquareInch((SquareInch.Factor / SquareYard.Factor) * q.m_value); }
        public static explicit operator SquareInch(SquareFoot q) { return new SquareInch((SquareInch.Factor / SquareFoot.Factor) * q.m_value); }
        public static SquareInch From(IQuantity<double> q)
        {
            if (q.Unit.Family != SquareInch.Family) throw new InvalidOperationException(string.Format("Cannot convert \"{0}\" to \"SquareInch\"", q.GetType().Name));
            return new SquareInch((SquareInch.Factor / q.Unit.Factor) * q.Value);
        }
        #endregion

        #region IObject / IEquatable<SquareInch>
        public override int GetHashCode() { return m_value.GetHashCode(); }
        public override bool /* IObject */ Equals(object obj) { return (obj is SquareInch) && Equals((SquareInch)obj); }
        public bool /* IEquatable<SquareInch> */ Equals(SquareInch other) { return this.m_value == other.m_value; }
        #endregion

        #region Comparison / IComparable<SquareInch>
        public static bool operator ==(SquareInch lhs, SquareInch rhs) { return lhs.m_value == rhs.m_value; }
        public static bool operator !=(SquareInch lhs, SquareInch rhs) { return lhs.m_value != rhs.m_value; }
        public static bool operator <(SquareInch lhs, SquareInch rhs) { return lhs.m_value < rhs.m_value; }
        public static bool operator >(SquareInch lhs, SquareInch rhs) { return lhs.m_value > rhs.m_value; }
        public static bool operator <=(SquareInch lhs, SquareInch rhs) { return lhs.m_value <= rhs.m_value; }
        public static bool operator >=(SquareInch lhs, SquareInch rhs) { return lhs.m_value >= rhs.m_value; }
        public int /* IComparable<SquareInch> */ CompareTo(SquareInch other) { return this.m_value.CompareTo(other.m_value); }
        #endregion

        #region Arithmetic
        // Inner:
        public static SquareInch operator +(SquareInch lhs, SquareInch rhs) { return new SquareInch(lhs.m_value + rhs.m_value); }
        public static SquareInch operator -(SquareInch lhs, SquareInch rhs) { return new SquareInch(lhs.m_value - rhs.m_value); }
        public static SquareInch operator ++(SquareInch q) { return new SquareInch(q.m_value + 1d); }
        public static SquareInch operator --(SquareInch q) { return new SquareInch(q.m_value - 1d); }
        public static SquareInch operator -(SquareInch q) { return new SquareInch(-q.m_value); }
        public static SquareInch operator *(double lhs, SquareInch rhs) { return new SquareInch(lhs * rhs.m_value); }
        public static SquareInch operator *(SquareInch lhs, double rhs) { return new SquareInch(lhs.m_value * rhs); }
        public static SquareInch operator /(SquareInch lhs, double rhs) { return new SquareInch(lhs.m_value / rhs); }
        public static double operator /(SquareInch lhs, SquareInch rhs) { return lhs.m_value / rhs.m_value; }
        // Outer:
        public static Inch operator /(SquareInch lhs, Inch rhs) { return new Inch(lhs.m_value / rhs.m_value); }
        public static CubicInch operator *(SquareInch lhs, Inch rhs) { return new CubicInch(lhs.m_value * rhs.m_value); }
        public static CubicInch operator *(Inch lhs, SquareInch rhs) { return new CubicInch(lhs.m_value * rhs.m_value); }
        #endregion

        #region Formatting
        public override string ToString() { return ToString(SquareInch.Format, null); }
        public string ToString(string format) { return ToString(format, null); }
        public string ToString(IFormatProvider fp) { return ToString(SquareInch.Format, fp); }
        public string /* IFormattable */ ToString(string format, IFormatProvider fp)
        {
            return string.Format(fp, format ?? SquareInch.Format, m_value, SquareInch.Symbol.Default);
        }
        #endregion

        #region Static fields
        private static readonly Dimension s_sense = Inch.Sense * Inch.Sense;
        private static readonly int s_family = SquareMeter.Family;
        private static /*mutable*/ double s_factor = Inch.Factor * Inch.Factor;
        private static /*mutable*/ string s_format = "{0} {1}";
        private static readonly SymbolCollection s_symbol = new SymbolCollection("in\u00B2", "sq in");
        private static readonly Unit<double> s_proxy = new SquareInch_Proxy();

        private static readonly SquareInch s_one = new SquareInch(1d);
        private static readonly SquareInch s_zero = new SquareInch(0d);
        #endregion

        #region Static Properties
        public static Dimension Sense { get { return s_sense; } }
        public static int Family { get { return s_family; } }
        public static double Factor { get { return s_factor; } set { s_factor = value; } }
        public static string Format { get { return s_format; } set { s_format = value; } }
        public static SymbolCollection Symbol { get { return s_symbol; } }
        public static Unit<double> Proxy { get { return s_proxy; } }

        public static SquareInch One { get { return s_one; } }
        public static SquareInch Zero { get { return s_zero; } }
        #endregion
    }

    public partial class SquareInch_Proxy : Unit<double>
    {
        #region Properties
        public override int Family { get { return SquareInch.Family; } }
        public override Dimension Sense { get { return SquareInch.Sense; } }
        public override SymbolCollection Symbol { get { return SquareInch.Symbol; } }
        public override double Factor { get { return SquareInch.Factor; } set { SquareInch.Factor = value; } }
        public override string Format { get { return SquareInch.Format; } set { SquareInch.Format = value; } }
        #endregion

        #region Constructor(s)
        public SquareInch_Proxy() :
            base(typeof(SquareInch))
        {
        }
        #endregion

        #region Methods
        public override IQuantity<double> Create(double value)
        {
            return new SquareInch(value);
        }
        public override IQuantity<double> From(IQuantity<double> quantity)
        {
            return SquareInch.From(quantity);
        }
        #endregion
    }
}
