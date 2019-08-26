/*******************************************************************************

    Units of Measurement for C# applications

    Copyright (C) Marek Aniola

    This program is provided to you under the terms of the license
    as published at https://github.com/mangh/unitsofmeasurement


********************************************************************************/
using System;

namespace UnitsOfMeasurement
{
    public partial struct CubicFoot : IQuantity<double>, IEquatable<CubicFoot>, IComparable<CubicFoot>, IFormattable
    {
        #region Fields
        internal readonly double m_value;
        #endregion

        #region Properties
        public double Value { get { return m_value; } }
        Unit<double> IQuantity<double>.Unit { get { return CubicFoot.Proxy; } }
        #endregion

        #region Constructor(s)
        public CubicFoot(double value)
        {
            m_value = value;
        }
        #endregion

        #region Conversions
        public static explicit operator CubicFoot(double q) { return new CubicFoot(q); }
        public static explicit operator CubicFoot(CubicInch q) { return new CubicFoot((CubicFoot.Factor / CubicInch.Factor) * q.m_value); }
        public static explicit operator CubicFoot(Liter q) { return new CubicFoot((CubicFoot.Factor / Liter.Factor) * q.m_value); }
        public static explicit operator CubicFoot(CubicCentimeter q) { return new CubicFoot((CubicFoot.Factor / CubicCentimeter.Factor) * q.m_value); }
        public static explicit operator CubicFoot(CubicMeter q) { return new CubicFoot((CubicFoot.Factor / CubicMeter.Factor) * q.m_value); }
        public static explicit operator CubicFoot(CubicYard q) { return new CubicFoot((CubicFoot.Factor / CubicYard.Factor) * q.m_value); }
        public static CubicFoot From(IQuantity<double> q)
        {
            if (q.Unit.Family != CubicFoot.Family) throw new InvalidOperationException(string.Format("Cannot convert \"{0}\" to \"CubicFoot\"", q.GetType().Name));
            return new CubicFoot((CubicFoot.Factor / q.Unit.Factor) * q.Value);
        }
        #endregion

        #region IObject / IEquatable<CubicFoot>
        public override int GetHashCode() { return m_value.GetHashCode(); }
        public override bool /* IObject */ Equals(object obj) { return (obj is CubicFoot) && Equals((CubicFoot)obj); }
        public bool /* IEquatable<CubicFoot> */ Equals(CubicFoot other) { return this.m_value == other.m_value; }
        #endregion

        #region Comparison / IComparable<CubicFoot>
        public static bool operator ==(CubicFoot lhs, CubicFoot rhs) { return lhs.m_value == rhs.m_value; }
        public static bool operator !=(CubicFoot lhs, CubicFoot rhs) { return lhs.m_value != rhs.m_value; }
        public static bool operator <(CubicFoot lhs, CubicFoot rhs) { return lhs.m_value < rhs.m_value; }
        public static bool operator >(CubicFoot lhs, CubicFoot rhs) { return lhs.m_value > rhs.m_value; }
        public static bool operator <=(CubicFoot lhs, CubicFoot rhs) { return lhs.m_value <= rhs.m_value; }
        public static bool operator >=(CubicFoot lhs, CubicFoot rhs) { return lhs.m_value >= rhs.m_value; }
        public int /* IComparable<CubicFoot> */ CompareTo(CubicFoot other) { return this.m_value.CompareTo(other.m_value); }
        #endregion

        #region Arithmetic
        // Inner:
        public static CubicFoot operator +(CubicFoot lhs, CubicFoot rhs) { return new CubicFoot(lhs.m_value + rhs.m_value); }
        public static CubicFoot operator -(CubicFoot lhs, CubicFoot rhs) { return new CubicFoot(lhs.m_value - rhs.m_value); }
        public static CubicFoot operator ++(CubicFoot q) { return new CubicFoot(q.m_value + 1d); }
        public static CubicFoot operator --(CubicFoot q) { return new CubicFoot(q.m_value - 1d); }
        public static CubicFoot operator -(CubicFoot q) { return new CubicFoot(-q.m_value); }
        public static CubicFoot operator *(double lhs, CubicFoot rhs) { return new CubicFoot(lhs * rhs.m_value); }
        public static CubicFoot operator *(CubicFoot lhs, double rhs) { return new CubicFoot(lhs.m_value * rhs); }
        public static CubicFoot operator /(CubicFoot lhs, double rhs) { return new CubicFoot(lhs.m_value / rhs); }
        public static double operator /(CubicFoot lhs, CubicFoot rhs) { return lhs.m_value / rhs.m_value; }
        // Outer:
        public static Foot operator /(CubicFoot lhs, SquareFoot rhs) { return new Foot(lhs.m_value / rhs.m_value); }
        public static SquareFoot operator /(CubicFoot lhs, Foot rhs) { return new SquareFoot(lhs.m_value / rhs.m_value); }
        #endregion

        #region Formatting
        public override string ToString() { return ToString(CubicFoot.Format, null); }
        public string ToString(string format) { return ToString(format, null); }
        public string ToString(IFormatProvider fp) { return ToString(CubicFoot.Format, fp); }
        public string /* IFormattable */ ToString(string format, IFormatProvider fp)
        {
            return string.Format(fp, format ?? CubicFoot.Format, m_value, CubicFoot.Symbol.Default);
        }
        #endregion

        #region Static fields
        private static readonly Dimension s_sense = SquareFoot.Sense * Foot.Sense;
        private static readonly int s_family = CubicMeter.Family;
        private static /*mutable*/ double s_factor = SquareFoot.Factor * Foot.Factor;
        private static /*mutable*/ string s_format = "{0} {1}";
        private static readonly SymbolCollection s_symbol = new SymbolCollection("ft\u00B3", "cu ft");
        private static readonly Unit<double> s_proxy = new CubicFoot_Proxy();

        private static readonly CubicFoot s_one = new CubicFoot(1d);
        private static readonly CubicFoot s_zero = new CubicFoot(0d);
        #endregion

        #region Static Properties
        public static Dimension Sense { get { return s_sense; } }
        public static int Family { get { return s_family; } }
        public static double Factor { get { return s_factor; } set { s_factor = value; } }
        public static string Format { get { return s_format; } set { s_format = value; } }
        public static SymbolCollection Symbol { get { return s_symbol; } }
        public static Unit<double> Proxy { get { return s_proxy; } }

        public static CubicFoot One { get { return s_one; } }
        public static CubicFoot Zero { get { return s_zero; } }
        #endregion
    }

    public partial class CubicFoot_Proxy : Unit<double>
    {
        #region Properties
        public override int Family { get { return CubicFoot.Family; } }
        public override Dimension Sense { get { return CubicFoot.Sense; } }
        public override SymbolCollection Symbol { get { return CubicFoot.Symbol; } }
        public override double Factor { get { return CubicFoot.Factor; } set { CubicFoot.Factor = value; } }
        public override string Format { get { return CubicFoot.Format; } set { CubicFoot.Format = value; } }
        #endregion

        #region Constructor(s)
        public CubicFoot_Proxy() :
            base(typeof(CubicFoot))
        {
        }
        #endregion

        #region Methods
        public override IQuantity<double> Create(double value)
        {
            return new CubicFoot(value);
        }
        public override IQuantity<double> From(IQuantity<double> quantity)
        {
            return CubicFoot.From(quantity);
        }
        #endregion
    }
}
