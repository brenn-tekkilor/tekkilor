/*******************************************************************************

    Units of Measurement for C# applications

    Copyright (C) Marek Aniola

    This program is provided to you under the terms of the license
    as published at https://github.com/mangh/unitsofmeasurement


********************************************************************************/
using System;

namespace UnitsOfMeasurement
{
    public partial struct CubicYard : IQuantity<double>, IEquatable<CubicYard>, IComparable<CubicYard>, IFormattable
    {
        #region Fields
        internal readonly double m_value;
        #endregion

        #region Properties
        public double Value { get { return m_value; } }
        Unit<double> IQuantity<double>.Unit { get { return CubicYard.Proxy; } }
        #endregion

        #region Constructor(s)
        public CubicYard(double value)
        {
            m_value = value;
        }
        #endregion

        #region Conversions
        public static explicit operator CubicYard(double q) { return new CubicYard(q); }
        public static explicit operator CubicYard(CubicFoot q) { return new CubicYard((CubicYard.Factor / CubicFoot.Factor) * q.m_value); }
        public static explicit operator CubicYard(CubicInch q) { return new CubicYard((CubicYard.Factor / CubicInch.Factor) * q.m_value); }
        public static explicit operator CubicYard(Liter q) { return new CubicYard((CubicYard.Factor / Liter.Factor) * q.m_value); }
        public static explicit operator CubicYard(CubicCentimeter q) { return new CubicYard((CubicYard.Factor / CubicCentimeter.Factor) * q.m_value); }
        public static explicit operator CubicYard(CubicMeter q) { return new CubicYard((CubicYard.Factor / CubicMeter.Factor) * q.m_value); }
        public static CubicYard From(IQuantity<double> q)
        {
            if (q.Unit.Family != CubicYard.Family) throw new InvalidOperationException(string.Format("Cannot convert \"{0}\" to \"CubicYard\"", q.GetType().Name));
            return new CubicYard((CubicYard.Factor / q.Unit.Factor) * q.Value);
        }
        #endregion

        #region IObject / IEquatable<CubicYard>
        public override int GetHashCode() { return m_value.GetHashCode(); }
        public override bool /* IObject */ Equals(object obj) { return (obj is CubicYard) && Equals((CubicYard)obj); }
        public bool /* IEquatable<CubicYard> */ Equals(CubicYard other) { return this.m_value == other.m_value; }
        #endregion

        #region Comparison / IComparable<CubicYard>
        public static bool operator ==(CubicYard lhs, CubicYard rhs) { return lhs.m_value == rhs.m_value; }
        public static bool operator !=(CubicYard lhs, CubicYard rhs) { return lhs.m_value != rhs.m_value; }
        public static bool operator <(CubicYard lhs, CubicYard rhs) { return lhs.m_value < rhs.m_value; }
        public static bool operator >(CubicYard lhs, CubicYard rhs) { return lhs.m_value > rhs.m_value; }
        public static bool operator <=(CubicYard lhs, CubicYard rhs) { return lhs.m_value <= rhs.m_value; }
        public static bool operator >=(CubicYard lhs, CubicYard rhs) { return lhs.m_value >= rhs.m_value; }
        public int /* IComparable<CubicYard> */ CompareTo(CubicYard other) { return this.m_value.CompareTo(other.m_value); }
        #endregion

        #region Arithmetic
        // Inner:
        public static CubicYard operator +(CubicYard lhs, CubicYard rhs) { return new CubicYard(lhs.m_value + rhs.m_value); }
        public static CubicYard operator -(CubicYard lhs, CubicYard rhs) { return new CubicYard(lhs.m_value - rhs.m_value); }
        public static CubicYard operator ++(CubicYard q) { return new CubicYard(q.m_value + 1d); }
        public static CubicYard operator --(CubicYard q) { return new CubicYard(q.m_value - 1d); }
        public static CubicYard operator -(CubicYard q) { return new CubicYard(-q.m_value); }
        public static CubicYard operator *(double lhs, CubicYard rhs) { return new CubicYard(lhs * rhs.m_value); }
        public static CubicYard operator *(CubicYard lhs, double rhs) { return new CubicYard(lhs.m_value * rhs); }
        public static CubicYard operator /(CubicYard lhs, double rhs) { return new CubicYard(lhs.m_value / rhs); }
        public static double operator /(CubicYard lhs, CubicYard rhs) { return lhs.m_value / rhs.m_value; }
        // Outer:
        public static Yard operator /(CubicYard lhs, SquareYard rhs) { return new Yard(lhs.m_value / rhs.m_value); }
        public static SquareYard operator /(CubicYard lhs, Yard rhs) { return new SquareYard(lhs.m_value / rhs.m_value); }
        #endregion

        #region Formatting
        public override string ToString() { return ToString(CubicYard.Format, null); }
        public string ToString(string format) { return ToString(format, null); }
        public string ToString(IFormatProvider fp) { return ToString(CubicYard.Format, fp); }
        public string /* IFormattable */ ToString(string format, IFormatProvider fp)
        {
            return string.Format(fp, format ?? CubicYard.Format, m_value, CubicYard.Symbol.Default);
        }
        #endregion

        #region Static fields
        private static readonly Dimension s_sense = SquareYard.Sense * Yard.Sense;
        private static readonly int s_family = CubicMeter.Family;
        private static /*mutable*/ double s_factor = SquareYard.Factor * Yard.Factor;
        private static /*mutable*/ string s_format = "{0} {1}";
        private static readonly SymbolCollection s_symbol = new SymbolCollection("yd\u00B3", "cu yd");
        private static readonly Unit<double> s_proxy = new CubicYard_Proxy();

        private static readonly CubicYard s_one = new CubicYard(1d);
        private static readonly CubicYard s_zero = new CubicYard(0d);
        #endregion

        #region Static Properties
        public static Dimension Sense { get { return s_sense; } }
        public static int Family { get { return s_family; } }
        public static double Factor { get { return s_factor; } set { s_factor = value; } }
        public static string Format { get { return s_format; } set { s_format = value; } }
        public static SymbolCollection Symbol { get { return s_symbol; } }
        public static Unit<double> Proxy { get { return s_proxy; } }

        public static CubicYard One { get { return s_one; } }
        public static CubicYard Zero { get { return s_zero; } }
        #endregion
    }

    public partial class CubicYard_Proxy : Unit<double>
    {
        #region Properties
        public override int Family { get { return CubicYard.Family; } }
        public override Dimension Sense { get { return CubicYard.Sense; } }
        public override SymbolCollection Symbol { get { return CubicYard.Symbol; } }
        public override double Factor { get { return CubicYard.Factor; } set { CubicYard.Factor = value; } }
        public override string Format { get { return CubicYard.Format; } set { CubicYard.Format = value; } }
        #endregion

        #region Constructor(s)
        public CubicYard_Proxy() :
            base(typeof(CubicYard))
        {
        }
        #endregion

        #region Methods
        public override IQuantity<double> Create(double value)
        {
            return new CubicYard(value);
        }
        public override IQuantity<double> From(IQuantity<double> quantity)
        {
            return CubicYard.From(quantity);
        }
        #endregion
    }
}
