/*******************************************************************************

    Units of Measurement for C# applications

    Copyright (C) Marek Aniola

    This program is provided to you under the terms of the license
    as published at https://github.com/mangh/unitsofmeasurement


********************************************************************************/
using System;

namespace UnitsOfMeasurement
{
    public partial struct SquareCentimeter : IQuantity<double>, IEquatable<SquareCentimeter>, IComparable<SquareCentimeter>, IFormattable
    {
        #region Fields
        internal readonly double m_value;
        #endregion

        #region Properties
        public double Value { get { return m_value; } }
        Unit<double> IQuantity<double>.Unit { get { return SquareCentimeter.Proxy; } }
        #endregion

        #region Constructor(s)
        public SquareCentimeter(double value)
        {
            m_value = value;
        }
        #endregion

        #region Conversions
        public static explicit operator SquareCentimeter(double q) { return new SquareCentimeter(q); }
        public static explicit operator SquareCentimeter(SquareMeter q) { return new SquareCentimeter((SquareCentimeter.Factor / SquareMeter.Factor) * q.m_value); }
        public static explicit operator SquareCentimeter(SquareMile q) { return new SquareCentimeter((SquareCentimeter.Factor / SquareMile.Factor) * q.m_value); }
        public static explicit operator SquareCentimeter(SquareYard q) { return new SquareCentimeter((SquareCentimeter.Factor / SquareYard.Factor) * q.m_value); }
        public static explicit operator SquareCentimeter(SquareFoot q) { return new SquareCentimeter((SquareCentimeter.Factor / SquareFoot.Factor) * q.m_value); }
        public static explicit operator SquareCentimeter(SquareInch q) { return new SquareCentimeter((SquareCentimeter.Factor / SquareInch.Factor) * q.m_value); }
        public static SquareCentimeter From(IQuantity<double> q)
        {
            if (q.Unit.Family != SquareCentimeter.Family) throw new InvalidOperationException(string.Format("Cannot convert \"{0}\" to \"SquareCentimeter\"", q.GetType().Name));
            return new SquareCentimeter((SquareCentimeter.Factor / q.Unit.Factor) * q.Value);
        }
        #endregion

        #region IObject / IEquatable<SquareCentimeter>
        public override int GetHashCode() { return m_value.GetHashCode(); }
        public override bool /* IObject */ Equals(object obj) { return (obj is SquareCentimeter) && Equals((SquareCentimeter)obj); }
        public bool /* IEquatable<SquareCentimeter> */ Equals(SquareCentimeter other) { return this.m_value == other.m_value; }
        #endregion

        #region Comparison / IComparable<SquareCentimeter>
        public static bool operator ==(SquareCentimeter lhs, SquareCentimeter rhs) { return lhs.m_value == rhs.m_value; }
        public static bool operator !=(SquareCentimeter lhs, SquareCentimeter rhs) { return lhs.m_value != rhs.m_value; }
        public static bool operator <(SquareCentimeter lhs, SquareCentimeter rhs) { return lhs.m_value < rhs.m_value; }
        public static bool operator >(SquareCentimeter lhs, SquareCentimeter rhs) { return lhs.m_value > rhs.m_value; }
        public static bool operator <=(SquareCentimeter lhs, SquareCentimeter rhs) { return lhs.m_value <= rhs.m_value; }
        public static bool operator >=(SquareCentimeter lhs, SquareCentimeter rhs) { return lhs.m_value >= rhs.m_value; }
        public int /* IComparable<SquareCentimeter> */ CompareTo(SquareCentimeter other) { return this.m_value.CompareTo(other.m_value); }
        #endregion

        #region Arithmetic
        // Inner:
        public static SquareCentimeter operator +(SquareCentimeter lhs, SquareCentimeter rhs) { return new SquareCentimeter(lhs.m_value + rhs.m_value); }
        public static SquareCentimeter operator -(SquareCentimeter lhs, SquareCentimeter rhs) { return new SquareCentimeter(lhs.m_value - rhs.m_value); }
        public static SquareCentimeter operator ++(SquareCentimeter q) { return new SquareCentimeter(q.m_value + 1d); }
        public static SquareCentimeter operator --(SquareCentimeter q) { return new SquareCentimeter(q.m_value - 1d); }
        public static SquareCentimeter operator -(SquareCentimeter q) { return new SquareCentimeter(-q.m_value); }
        public static SquareCentimeter operator *(double lhs, SquareCentimeter rhs) { return new SquareCentimeter(lhs * rhs.m_value); }
        public static SquareCentimeter operator *(SquareCentimeter lhs, double rhs) { return new SquareCentimeter(lhs.m_value * rhs); }
        public static SquareCentimeter operator /(SquareCentimeter lhs, double rhs) { return new SquareCentimeter(lhs.m_value / rhs); }
        public static double operator /(SquareCentimeter lhs, SquareCentimeter rhs) { return lhs.m_value / rhs.m_value; }
        // Outer:
        public static Centimeter operator /(SquareCentimeter lhs, Centimeter rhs) { return new Centimeter(lhs.m_value / rhs.m_value); }
        public static CubicCentimeter operator *(SquareCentimeter lhs, Centimeter rhs) { return new CubicCentimeter(lhs.m_value * rhs.m_value); }
        public static CubicCentimeter operator *(Centimeter lhs, SquareCentimeter rhs) { return new CubicCentimeter(lhs.m_value * rhs.m_value); }
        #endregion

        #region Formatting
        public override string ToString() { return ToString(SquareCentimeter.Format, null); }
        public string ToString(string format) { return ToString(format, null); }
        public string ToString(IFormatProvider fp) { return ToString(SquareCentimeter.Format, fp); }
        public string /* IFormattable */ ToString(string format, IFormatProvider fp)
        {
            return string.Format(fp, format ?? SquareCentimeter.Format, m_value, SquareCentimeter.Symbol.Default);
        }
        #endregion

        #region Static fields
        private static readonly Dimension s_sense = Centimeter.Sense * Centimeter.Sense;
        private static readonly int s_family = SquareMeter.Family;
        private static /*mutable*/ double s_factor = Centimeter.Factor * Centimeter.Factor;
        private static /*mutable*/ string s_format = "{0} {1}";
        private static readonly SymbolCollection s_symbol = new SymbolCollection("cm\u00B2", "cm2");
        private static readonly Unit<double> s_proxy = new SquareCentimeter_Proxy();

        private static readonly SquareCentimeter s_one = new SquareCentimeter(1d);
        private static readonly SquareCentimeter s_zero = new SquareCentimeter(0d);
        #endregion

        #region Static Properties
        public static Dimension Sense { get { return s_sense; } }
        public static int Family { get { return s_family; } }
        public static double Factor { get { return s_factor; } set { s_factor = value; } }
        public static string Format { get { return s_format; } set { s_format = value; } }
        public static SymbolCollection Symbol { get { return s_symbol; } }
        public static Unit<double> Proxy { get { return s_proxy; } }

        public static SquareCentimeter One { get { return s_one; } }
        public static SquareCentimeter Zero { get { return s_zero; } }
        #endregion
    }

    public partial class SquareCentimeter_Proxy : Unit<double>
    {
        #region Properties
        public override int Family { get { return SquareCentimeter.Family; } }
        public override Dimension Sense { get { return SquareCentimeter.Sense; } }
        public override SymbolCollection Symbol { get { return SquareCentimeter.Symbol; } }
        public override double Factor { get { return SquareCentimeter.Factor; } set { SquareCentimeter.Factor = value; } }
        public override string Format { get { return SquareCentimeter.Format; } set { SquareCentimeter.Format = value; } }
        #endregion

        #region Constructor(s)
        public SquareCentimeter_Proxy() :
            base(typeof(SquareCentimeter))
        {
        }
        #endregion

        #region Methods
        public override IQuantity<double> Create(double value)
        {
            return new SquareCentimeter(value);
        }
        public override IQuantity<double> From(IQuantity<double> quantity)
        {
            return SquareCentimeter.From(quantity);
        }
        #endregion
    }
}
