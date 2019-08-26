/*******************************************************************************

    Units of Measurement for C# applications

    Copyright (C) Marek Aniola

    This program is provided to you under the terms of the license
    as published at https://github.com/mangh/unitsofmeasurement


********************************************************************************/
using System;

namespace UnitsOfMeasurement
{
    public partial struct CubicInch : IQuantity<double>, IEquatable<CubicInch>, IComparable<CubicInch>, IFormattable
    {
        #region Fields
        internal readonly double m_value;
        #endregion

        #region Properties
        public double Value { get { return m_value; } }
        Unit<double> IQuantity<double>.Unit { get { return CubicInch.Proxy; } }
        #endregion

        #region Constructor(s)
        public CubicInch(double value)
        {
            m_value = value;
        }
        #endregion

        #region Conversions
        public static explicit operator CubicInch(double q) { return new CubicInch(q); }
        public static explicit operator CubicInch(Liter q) { return new CubicInch((CubicInch.Factor / Liter.Factor) * q.m_value); }
        public static explicit operator CubicInch(CubicCentimeter q) { return new CubicInch((CubicInch.Factor / CubicCentimeter.Factor) * q.m_value); }
        public static explicit operator CubicInch(CubicMeter q) { return new CubicInch((CubicInch.Factor / CubicMeter.Factor) * q.m_value); }
        public static explicit operator CubicInch(CubicYard q) { return new CubicInch((CubicInch.Factor / CubicYard.Factor) * q.m_value); }
        public static explicit operator CubicInch(CubicFoot q) { return new CubicInch((CubicInch.Factor / CubicFoot.Factor) * q.m_value); }
        public static CubicInch From(IQuantity<double> q)
        {
            if (q.Unit.Family != CubicInch.Family) throw new InvalidOperationException(string.Format("Cannot convert \"{0}\" to \"CubicInch\"", q.GetType().Name));
            return new CubicInch((CubicInch.Factor / q.Unit.Factor) * q.Value);
        }
        #endregion

        #region IObject / IEquatable<CubicInch>
        public override int GetHashCode() { return m_value.GetHashCode(); }
        public override bool /* IObject */ Equals(object obj) { return (obj is CubicInch) && Equals((CubicInch)obj); }
        public bool /* IEquatable<CubicInch> */ Equals(CubicInch other) { return this.m_value == other.m_value; }
        #endregion

        #region Comparison / IComparable<CubicInch>
        public static bool operator ==(CubicInch lhs, CubicInch rhs) { return lhs.m_value == rhs.m_value; }
        public static bool operator !=(CubicInch lhs, CubicInch rhs) { return lhs.m_value != rhs.m_value; }
        public static bool operator <(CubicInch lhs, CubicInch rhs) { return lhs.m_value < rhs.m_value; }
        public static bool operator >(CubicInch lhs, CubicInch rhs) { return lhs.m_value > rhs.m_value; }
        public static bool operator <=(CubicInch lhs, CubicInch rhs) { return lhs.m_value <= rhs.m_value; }
        public static bool operator >=(CubicInch lhs, CubicInch rhs) { return lhs.m_value >= rhs.m_value; }
        public int /* IComparable<CubicInch> */ CompareTo(CubicInch other) { return this.m_value.CompareTo(other.m_value); }
        #endregion

        #region Arithmetic
        // Inner:
        public static CubicInch operator +(CubicInch lhs, CubicInch rhs) { return new CubicInch(lhs.m_value + rhs.m_value); }
        public static CubicInch operator -(CubicInch lhs, CubicInch rhs) { return new CubicInch(lhs.m_value - rhs.m_value); }
        public static CubicInch operator ++(CubicInch q) { return new CubicInch(q.m_value + 1d); }
        public static CubicInch operator --(CubicInch q) { return new CubicInch(q.m_value - 1d); }
        public static CubicInch operator -(CubicInch q) { return new CubicInch(-q.m_value); }
        public static CubicInch operator *(double lhs, CubicInch rhs) { return new CubicInch(lhs * rhs.m_value); }
        public static CubicInch operator *(CubicInch lhs, double rhs) { return new CubicInch(lhs.m_value * rhs); }
        public static CubicInch operator /(CubicInch lhs, double rhs) { return new CubicInch(lhs.m_value / rhs); }
        public static double operator /(CubicInch lhs, CubicInch rhs) { return lhs.m_value / rhs.m_value; }
        // Outer:
        public static Inch operator /(CubicInch lhs, SquareInch rhs) { return new Inch(lhs.m_value / rhs.m_value); }
        public static SquareInch operator /(CubicInch lhs, Inch rhs) { return new SquareInch(lhs.m_value / rhs.m_value); }
        #endregion

        #region Formatting
        public override string ToString() { return ToString(CubicInch.Format, null); }
        public string ToString(string format) { return ToString(format, null); }
        public string ToString(IFormatProvider fp) { return ToString(CubicInch.Format, fp); }
        public string /* IFormattable */ ToString(string format, IFormatProvider fp)
        {
            return string.Format(fp, format ?? CubicInch.Format, m_value, CubicInch.Symbol.Default);
        }
        #endregion

        #region Static fields
        private static readonly Dimension s_sense = SquareInch.Sense * Inch.Sense;
        private static readonly int s_family = CubicMeter.Family;
        private static /*mutable*/ double s_factor = SquareInch.Factor * Inch.Factor;
        private static /*mutable*/ string s_format = "{0} {1}";
        private static readonly SymbolCollection s_symbol = new SymbolCollection("in\u00B3", "cu in");
        private static readonly Unit<double> s_proxy = new CubicInch_Proxy();

        private static readonly CubicInch s_one = new CubicInch(1d);
        private static readonly CubicInch s_zero = new CubicInch(0d);
        #endregion

        #region Static Properties
        public static Dimension Sense { get { return s_sense; } }
        public static int Family { get { return s_family; } }
        public static double Factor { get { return s_factor; } set { s_factor = value; } }
        public static string Format { get { return s_format; } set { s_format = value; } }
        public static SymbolCollection Symbol { get { return s_symbol; } }
        public static Unit<double> Proxy { get { return s_proxy; } }

        public static CubicInch One { get { return s_one; } }
        public static CubicInch Zero { get { return s_zero; } }
        #endregion
    }

    public partial class CubicInch_Proxy : Unit<double>
    {
        #region Properties
        public override int Family { get { return CubicInch.Family; } }
        public override Dimension Sense { get { return CubicInch.Sense; } }
        public override SymbolCollection Symbol { get { return CubicInch.Symbol; } }
        public override double Factor { get { return CubicInch.Factor; } set { CubicInch.Factor = value; } }
        public override string Format { get { return CubicInch.Format; } set { CubicInch.Format = value; } }
        #endregion

        #region Constructor(s)
        public CubicInch_Proxy() :
            base(typeof(CubicInch))
        {
        }
        #endregion

        #region Methods
        public override IQuantity<double> Create(double value)
        {
            return new CubicInch(value);
        }
        public override IQuantity<double> From(IQuantity<double> quantity)
        {
            return CubicInch.From(quantity);
        }
        #endregion
    }
}
