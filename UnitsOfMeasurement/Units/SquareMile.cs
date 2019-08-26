/*******************************************************************************

    Units of Measurement for C# applications

    Copyright (C) Marek Aniola

    This program is provided to you under the terms of the license
    as published at https://github.com/mangh/unitsofmeasurement


********************************************************************************/
using System;

namespace UnitsOfMeasurement
{
    public partial struct SquareMile : IQuantity<double>, IEquatable<SquareMile>, IComparable<SquareMile>, IFormattable
    {
        #region Fields
        internal readonly double m_value;
        #endregion

        #region Properties
        public double Value { get { return m_value; } }
        Unit<double> IQuantity<double>.Unit { get { return SquareMile.Proxy; } }
        #endregion

        #region Constructor(s)
        public SquareMile(double value)
        {
            m_value = value;
        }
        #endregion

        #region Conversions
        public static explicit operator SquareMile(double q) { return new SquareMile(q); }
        public static explicit operator SquareMile(SquareYard q) { return new SquareMile((SquareMile.Factor / SquareYard.Factor) * q.m_value); }
        public static explicit operator SquareMile(SquareFoot q) { return new SquareMile((SquareMile.Factor / SquareFoot.Factor) * q.m_value); }
        public static explicit operator SquareMile(SquareInch q) { return new SquareMile((SquareMile.Factor / SquareInch.Factor) * q.m_value); }
        public static explicit operator SquareMile(SquareCentimeter q) { return new SquareMile((SquareMile.Factor / SquareCentimeter.Factor) * q.m_value); }
        public static explicit operator SquareMile(SquareMeter q) { return new SquareMile((SquareMile.Factor / SquareMeter.Factor) * q.m_value); }
        public static SquareMile From(IQuantity<double> q)
        {
            if (q.Unit.Family != SquareMile.Family) throw new InvalidOperationException(string.Format("Cannot convert \"{0}\" to \"SquareMile\"", q.GetType().Name));
            return new SquareMile((SquareMile.Factor / q.Unit.Factor) * q.Value);
        }
        #endregion

        #region IObject / IEquatable<SquareMile>
        public override int GetHashCode() { return m_value.GetHashCode(); }
        public override bool /* IObject */ Equals(object obj) { return (obj is SquareMile) && Equals((SquareMile)obj); }
        public bool /* IEquatable<SquareMile> */ Equals(SquareMile other) { return this.m_value == other.m_value; }
        #endregion

        #region Comparison / IComparable<SquareMile>
        public static bool operator ==(SquareMile lhs, SquareMile rhs) { return lhs.m_value == rhs.m_value; }
        public static bool operator !=(SquareMile lhs, SquareMile rhs) { return lhs.m_value != rhs.m_value; }
        public static bool operator <(SquareMile lhs, SquareMile rhs) { return lhs.m_value < rhs.m_value; }
        public static bool operator >(SquareMile lhs, SquareMile rhs) { return lhs.m_value > rhs.m_value; }
        public static bool operator <=(SquareMile lhs, SquareMile rhs) { return lhs.m_value <= rhs.m_value; }
        public static bool operator >=(SquareMile lhs, SquareMile rhs) { return lhs.m_value >= rhs.m_value; }
        public int /* IComparable<SquareMile> */ CompareTo(SquareMile other) { return this.m_value.CompareTo(other.m_value); }
        #endregion

        #region Arithmetic
        // Inner:
        public static SquareMile operator +(SquareMile lhs, SquareMile rhs) { return new SquareMile(lhs.m_value + rhs.m_value); }
        public static SquareMile operator -(SquareMile lhs, SquareMile rhs) { return new SquareMile(lhs.m_value - rhs.m_value); }
        public static SquareMile operator ++(SquareMile q) { return new SquareMile(q.m_value + 1d); }
        public static SquareMile operator --(SquareMile q) { return new SquareMile(q.m_value - 1d); }
        public static SquareMile operator -(SquareMile q) { return new SquareMile(-q.m_value); }
        public static SquareMile operator *(double lhs, SquareMile rhs) { return new SquareMile(lhs * rhs.m_value); }
        public static SquareMile operator *(SquareMile lhs, double rhs) { return new SquareMile(lhs.m_value * rhs); }
        public static SquareMile operator /(SquareMile lhs, double rhs) { return new SquareMile(lhs.m_value / rhs); }
        public static double operator /(SquareMile lhs, SquareMile rhs) { return lhs.m_value / rhs.m_value; }
        // Outer:
        public static Mile operator /(SquareMile lhs, Mile rhs) { return new Mile(lhs.m_value / rhs.m_value); }
        #endregion

        #region Formatting
        public override string ToString() { return ToString(SquareMile.Format, null); }
        public string ToString(string format) { return ToString(format, null); }
        public string ToString(IFormatProvider fp) { return ToString(SquareMile.Format, fp); }
        public string /* IFormattable */ ToString(string format, IFormatProvider fp)
        {
            return string.Format(fp, format ?? SquareMile.Format, m_value, SquareMile.Symbol.Default);
        }
        #endregion

        #region Static fields
        private static readonly Dimension s_sense = Mile.Sense * Mile.Sense;
        private static readonly int s_family = SquareMeter.Family;
        private static /*mutable*/ double s_factor = Mile.Factor * Mile.Factor;
        private static /*mutable*/ string s_format = "{0} {1}";
        private static readonly SymbolCollection s_symbol = new SymbolCollection("mil\u00B2", "sq mil");
        private static readonly Unit<double> s_proxy = new SquareMile_Proxy();

        private static readonly SquareMile s_one = new SquareMile(1d);
        private static readonly SquareMile s_zero = new SquareMile(0d);
        #endregion

        #region Static Properties
        public static Dimension Sense { get { return s_sense; } }
        public static int Family { get { return s_family; } }
        public static double Factor { get { return s_factor; } set { s_factor = value; } }
        public static string Format { get { return s_format; } set { s_format = value; } }
        public static SymbolCollection Symbol { get { return s_symbol; } }
        public static Unit<double> Proxy { get { return s_proxy; } }

        public static SquareMile One { get { return s_one; } }
        public static SquareMile Zero { get { return s_zero; } }
        #endregion
    }

    public partial class SquareMile_Proxy : Unit<double>
    {
        #region Properties
        public override int Family { get { return SquareMile.Family; } }
        public override Dimension Sense { get { return SquareMile.Sense; } }
        public override SymbolCollection Symbol { get { return SquareMile.Symbol; } }
        public override double Factor { get { return SquareMile.Factor; } set { SquareMile.Factor = value; } }
        public override string Format { get { return SquareMile.Format; } set { SquareMile.Format = value; } }
        #endregion

        #region Constructor(s)
        public SquareMile_Proxy() :
            base(typeof(SquareMile))
        {
        }
        #endregion

        #region Methods
        public override IQuantity<double> Create(double value)
        {
            return new SquareMile(value);
        }
        public override IQuantity<double> From(IQuantity<double> quantity)
        {
            return SquareMile.From(quantity);
        }
        #endregion
    }
}
