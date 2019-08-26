/*******************************************************************************

    Units of Measurement for C# applications

    Copyright (C) Marek Aniola

    This program is provided to you under the terms of the license
    as published at https://github.com/mangh/unitsofmeasurement


********************************************************************************/
using System;

namespace UnitsOfMeasurement
{
    public partial struct Point : IQuantity<double>, IEquatable<Point>, IComparable<Point>, IFormattable
    {
        #region Fields
        internal readonly double m_value;
        #endregion

        #region Properties
        public double Value { get { return m_value; } }
        Unit<double> IQuantity<double>.Unit { get { return Point.Proxy; } }
        #endregion

        #region Constructor(s)
        public Point(double value)
        {
            m_value = value;
        }
        #endregion

        #region Conversions
        public static explicit operator Point(double q) { return new Point(q); }
        public static explicit operator Point(Pica q) { return new Point((Point.Factor / Pica.Factor) * q.m_value); }
        public static explicit operator Point(Foot q) { return new Point((Point.Factor / Foot.Factor) * q.m_value); }
        public static explicit operator Point(Yard q) { return new Point((Point.Factor / Yard.Factor) * q.m_value); }
        public static explicit operator Point(Mile q) { return new Point((Point.Factor / Mile.Factor) * q.m_value); }
        public static explicit operator Point(Millimeter q) { return new Point((Point.Factor / Millimeter.Factor) * q.m_value); }
        public static explicit operator Point(Centimeter q) { return new Point((Point.Factor / Centimeter.Factor) * q.m_value); }
        public static explicit operator Point(Meter q) { return new Point((Point.Factor / Meter.Factor) * q.m_value); }
        public static explicit operator Point(Inch q) { return new Point((Point.Factor / Inch.Factor) * q.m_value); }
        public static Point From(IQuantity<double> q)
        {
            if (q.Unit.Family != Point.Family) throw new InvalidOperationException(string.Format("Cannot convert \"{0}\" to \"Point\"", q.GetType().Name));
            return new Point((Point.Factor / q.Unit.Factor) * q.Value);
        }
        #endregion

        #region IObject / IEquatable<Point>
        public override int GetHashCode() { return m_value.GetHashCode(); }
        public override bool /* IObject */ Equals(object obj) { return (obj is Point) && Equals((Point)obj); }
        public bool /* IEquatable<Point> */ Equals(Point other) { return this.m_value == other.m_value; }
        #endregion

        #region Comparison / IComparable<Point>
        public static bool operator ==(Point lhs, Point rhs) { return lhs.m_value == rhs.m_value; }
        public static bool operator !=(Point lhs, Point rhs) { return lhs.m_value != rhs.m_value; }
        public static bool operator <(Point lhs, Point rhs) { return lhs.m_value < rhs.m_value; }
        public static bool operator >(Point lhs, Point rhs) { return lhs.m_value > rhs.m_value; }
        public static bool operator <=(Point lhs, Point rhs) { return lhs.m_value <= rhs.m_value; }
        public static bool operator >=(Point lhs, Point rhs) { return lhs.m_value >= rhs.m_value; }
        public int /* IComparable<Point> */ CompareTo(Point other) { return this.m_value.CompareTo(other.m_value); }
        #endregion

        #region Arithmetic
        // Inner:
        public static Point operator +(Point lhs, Point rhs) { return new Point(lhs.m_value + rhs.m_value); }
        public static Point operator -(Point lhs, Point rhs) { return new Point(lhs.m_value - rhs.m_value); }
        public static Point operator ++(Point q) { return new Point(q.m_value + 1d); }
        public static Point operator --(Point q) { return new Point(q.m_value - 1d); }
        public static Point operator -(Point q) { return new Point(-q.m_value); }
        public static Point operator *(double lhs, Point rhs) { return new Point(lhs * rhs.m_value); }
        public static Point operator *(Point lhs, double rhs) { return new Point(lhs.m_value * rhs); }
        public static Point operator /(Point lhs, double rhs) { return new Point(lhs.m_value / rhs); }
        public static double operator /(Point lhs, Point rhs) { return lhs.m_value / rhs.m_value; }
        // Outer:
        #endregion

        #region Formatting
        public override string ToString() { return ToString(Point.Format, null); }
        public string ToString(string format) { return ToString(format, null); }
        public string ToString(IFormatProvider fp) { return ToString(Point.Format, fp); }
        public string /* IFormattable */ ToString(string format, IFormatProvider fp)
        {
            return string.Format(fp, format ?? Point.Format, m_value, Point.Symbol.Default);
        }
        #endregion

        #region Static fields
        private static readonly Dimension s_sense = Inch.Sense;
        private static readonly int s_family = Meter.Family;
        private static /*mutable*/ double s_factor = 72d * Inch.Factor;
        private static /*mutable*/ string s_format = "{0} {1}";
        private static readonly SymbolCollection s_symbol = new SymbolCollection("pt");
        private static readonly Unit<double> s_proxy = new Point_Proxy();

        private static readonly Point s_one = new Point(1d);
        private static readonly Point s_zero = new Point(0d);
        #endregion

        #region Static Properties
        public static Dimension Sense { get { return s_sense; } }
        public static int Family { get { return s_family; } }
        public static double Factor { get { return s_factor; } set { s_factor = value; } }
        public static string Format { get { return s_format; } set { s_format = value; } }
        public static SymbolCollection Symbol { get { return s_symbol; } }
        public static Unit<double> Proxy { get { return s_proxy; } }

        public static Point One { get { return s_one; } }
        public static Point Zero { get { return s_zero; } }
        #endregion
    }

    public partial class Point_Proxy : Unit<double>
    {
        #region Properties
        public override int Family { get { return Point.Family; } }
        public override Dimension Sense { get { return Point.Sense; } }
        public override SymbolCollection Symbol { get { return Point.Symbol; } }
        public override double Factor { get { return Point.Factor; } set { Point.Factor = value; } }
        public override string Format { get { return Point.Format; } set { Point.Format = value; } }
        #endregion

        #region Constructor(s)
        public Point_Proxy() :
            base(typeof(Point))
        {
        }
        #endregion

        #region Methods
        public override IQuantity<double> Create(double value)
        {
            return new Point(value);
        }
        public override IQuantity<double> From(IQuantity<double> quantity)
        {
            return Point.From(quantity);
        }
        #endregion
    }
}
