/*******************************************************************************

    Units of Measurement for C# applications

    Copyright (C) Marek Aniola

    This program is provided to you under the terms of the license
    as published at https://github.com/mangh/unitsofmeasurement


********************************************************************************/
using System;

namespace UnitsOfMeasurement
{
    public partial struct CubicCentimeter : IQuantity<double>, IEquatable<CubicCentimeter>, IComparable<CubicCentimeter>, IFormattable
    {
        #region Fields
        internal readonly double m_value;
        #endregion

        #region Properties
        public double Value { get { return m_value; } }
        Unit<double> IQuantity<double>.Unit { get { return CubicCentimeter.Proxy; } }
        #endregion

        #region Constructor(s)
        public CubicCentimeter(double value)
        {
            m_value = value;
        }
        #endregion

        #region Conversions
        public static explicit operator CubicCentimeter(double q) { return new CubicCentimeter(q); }
        public static explicit operator CubicCentimeter(CubicMeter q) { return new CubicCentimeter((CubicCentimeter.Factor / CubicMeter.Factor) * q.m_value); }
        public static explicit operator CubicCentimeter(CubicYard q) { return new CubicCentimeter((CubicCentimeter.Factor / CubicYard.Factor) * q.m_value); }
        public static explicit operator CubicCentimeter(CubicFoot q) { return new CubicCentimeter((CubicCentimeter.Factor / CubicFoot.Factor) * q.m_value); }
        public static explicit operator CubicCentimeter(CubicInch q) { return new CubicCentimeter((CubicCentimeter.Factor / CubicInch.Factor) * q.m_value); }
        public static explicit operator CubicCentimeter(Liter q) { return new CubicCentimeter((CubicCentimeter.Factor / Liter.Factor) * q.m_value); }
        public static CubicCentimeter From(IQuantity<double> q)
        {
            if (q.Unit.Family != CubicCentimeter.Family) throw new InvalidOperationException(string.Format("Cannot convert \"{0}\" to \"CubicCentimeter\"", q.GetType().Name));
            return new CubicCentimeter((CubicCentimeter.Factor / q.Unit.Factor) * q.Value);
        }
        #endregion

        #region IObject / IEquatable<CubicCentimeter>
        public override int GetHashCode() { return m_value.GetHashCode(); }
        public override bool /* IObject */ Equals(object obj) { return (obj is CubicCentimeter) && Equals((CubicCentimeter)obj); }
        public bool /* IEquatable<CubicCentimeter> */ Equals(CubicCentimeter other) { return this.m_value == other.m_value; }
        #endregion

        #region Comparison / IComparable<CubicCentimeter>
        public static bool operator ==(CubicCentimeter lhs, CubicCentimeter rhs) { return lhs.m_value == rhs.m_value; }
        public static bool operator !=(CubicCentimeter lhs, CubicCentimeter rhs) { return lhs.m_value != rhs.m_value; }
        public static bool operator <(CubicCentimeter lhs, CubicCentimeter rhs) { return lhs.m_value < rhs.m_value; }
        public static bool operator >(CubicCentimeter lhs, CubicCentimeter rhs) { return lhs.m_value > rhs.m_value; }
        public static bool operator <=(CubicCentimeter lhs, CubicCentimeter rhs) { return lhs.m_value <= rhs.m_value; }
        public static bool operator >=(CubicCentimeter lhs, CubicCentimeter rhs) { return lhs.m_value >= rhs.m_value; }
        public int /* IComparable<CubicCentimeter> */ CompareTo(CubicCentimeter other) { return this.m_value.CompareTo(other.m_value); }
        #endregion

        #region Arithmetic
        // Inner:
        public static CubicCentimeter operator +(CubicCentimeter lhs, CubicCentimeter rhs) { return new CubicCentimeter(lhs.m_value + rhs.m_value); }
        public static CubicCentimeter operator -(CubicCentimeter lhs, CubicCentimeter rhs) { return new CubicCentimeter(lhs.m_value - rhs.m_value); }
        public static CubicCentimeter operator ++(CubicCentimeter q) { return new CubicCentimeter(q.m_value + 1d); }
        public static CubicCentimeter operator --(CubicCentimeter q) { return new CubicCentimeter(q.m_value - 1d); }
        public static CubicCentimeter operator -(CubicCentimeter q) { return new CubicCentimeter(-q.m_value); }
        public static CubicCentimeter operator *(double lhs, CubicCentimeter rhs) { return new CubicCentimeter(lhs * rhs.m_value); }
        public static CubicCentimeter operator *(CubicCentimeter lhs, double rhs) { return new CubicCentimeter(lhs.m_value * rhs); }
        public static CubicCentimeter operator /(CubicCentimeter lhs, double rhs) { return new CubicCentimeter(lhs.m_value / rhs); }
        public static double operator /(CubicCentimeter lhs, CubicCentimeter rhs) { return lhs.m_value / rhs.m_value; }
        // Outer:
        public static Centimeter operator /(CubicCentimeter lhs, SquareCentimeter rhs) { return new Centimeter(lhs.m_value / rhs.m_value); }
        public static SquareCentimeter operator /(CubicCentimeter lhs, Centimeter rhs) { return new SquareCentimeter(lhs.m_value / rhs.m_value); }
        #endregion

        #region Formatting
        public override string ToString() { return ToString(CubicCentimeter.Format, null); }
        public string ToString(string format) { return ToString(format, null); }
        public string ToString(IFormatProvider fp) { return ToString(CubicCentimeter.Format, fp); }
        public string /* IFormattable */ ToString(string format, IFormatProvider fp)
        {
            return string.Format(fp, format ?? CubicCentimeter.Format, m_value, CubicCentimeter.Symbol.Default);
        }
        #endregion

        #region Static fields
        private static readonly Dimension s_sense = SquareCentimeter.Sense * Centimeter.Sense;
        private static readonly int s_family = CubicMeter.Family;
        private static /*mutable*/ double s_factor = SquareCentimeter.Factor * Centimeter.Factor;
        private static /*mutable*/ string s_format = "{0} {1}";
        private static readonly SymbolCollection s_symbol = new SymbolCollection("cm\u00B3", "cm3");
        private static readonly Unit<double> s_proxy = new CubicCentimeter_Proxy();

        private static readonly CubicCentimeter s_one = new CubicCentimeter(1d);
        private static readonly CubicCentimeter s_zero = new CubicCentimeter(0d);
        #endregion

        #region Static Properties
        public static Dimension Sense { get { return s_sense; } }
        public static int Family { get { return s_family; } }
        public static double Factor { get { return s_factor; } set { s_factor = value; } }
        public static string Format { get { return s_format; } set { s_format = value; } }
        public static SymbolCollection Symbol { get { return s_symbol; } }
        public static Unit<double> Proxy { get { return s_proxy; } }

        public static CubicCentimeter One { get { return s_one; } }
        public static CubicCentimeter Zero { get { return s_zero; } }
        #endregion
    }

    public partial class CubicCentimeter_Proxy : Unit<double>
    {
        #region Properties
        public override int Family { get { return CubicCentimeter.Family; } }
        public override Dimension Sense { get { return CubicCentimeter.Sense; } }
        public override SymbolCollection Symbol { get { return CubicCentimeter.Symbol; } }
        public override double Factor { get { return CubicCentimeter.Factor; } set { CubicCentimeter.Factor = value; } }
        public override string Format { get { return CubicCentimeter.Format; } set { CubicCentimeter.Format = value; } }
        #endregion

        #region Constructor(s)
        public CubicCentimeter_Proxy() :
            base(typeof(CubicCentimeter))
        {
        }
        #endregion

        #region Methods
        public override IQuantity<double> Create(double value)
        {
            return new CubicCentimeter(value);
        }
        public override IQuantity<double> From(IQuantity<double> quantity)
        {
            return CubicCentimeter.From(quantity);
        }
        #endregion
    }
}
