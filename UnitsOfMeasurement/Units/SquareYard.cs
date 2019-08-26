/*******************************************************************************

    Units of Measurement for C# applications

    Copyright (C) Marek Aniola

    This program is provided to you under the terms of the license
    as published at https://github.com/mangh/unitsofmeasurement


********************************************************************************/
using System;

namespace UnitsOfMeasurement
{
    public partial struct SquareYard : IQuantity<double>, IEquatable<SquareYard>, IComparable<SquareYard>, IFormattable
    {
        #region Fields
        internal readonly double m_value;
        #endregion

        #region Properties
        public double Value { get { return m_value; } }
        Unit<double> IQuantity<double>.Unit { get { return SquareYard.Proxy; } }
        #endregion

        #region Constructor(s)
        public SquareYard(double value)
        {
            m_value = value;
        }
        #endregion

        #region Conversions
        public static explicit operator SquareYard(double q) { return new SquareYard(q); }
        public static explicit operator SquareYard(SquareFoot q) { return new SquareYard((SquareYard.Factor / SquareFoot.Factor) * q.m_value); }
        public static explicit operator SquareYard(SquareInch q) { return new SquareYard((SquareYard.Factor / SquareInch.Factor) * q.m_value); }
        public static explicit operator SquareYard(SquareCentimeter q) { return new SquareYard((SquareYard.Factor / SquareCentimeter.Factor) * q.m_value); }
        public static explicit operator SquareYard(SquareMeter q) { return new SquareYard((SquareYard.Factor / SquareMeter.Factor) * q.m_value); }
        public static explicit operator SquareYard(SquareMile q) { return new SquareYard((SquareYard.Factor / SquareMile.Factor) * q.m_value); }
        public static SquareYard From(IQuantity<double> q)
        {
            if (q.Unit.Family != SquareYard.Family) throw new InvalidOperationException(string.Format("Cannot convert \"{0}\" to \"SquareYard\"", q.GetType().Name));
            return new SquareYard((SquareYard.Factor / q.Unit.Factor) * q.Value);
        }
        #endregion

        #region IObject / IEquatable<SquareYard>
        public override int GetHashCode() { return m_value.GetHashCode(); }
        public override bool /* IObject */ Equals(object obj) { return (obj is SquareYard) && Equals((SquareYard)obj); }
        public bool /* IEquatable<SquareYard> */ Equals(SquareYard other) { return this.m_value == other.m_value; }
        #endregion

        #region Comparison / IComparable<SquareYard>
        public static bool operator ==(SquareYard lhs, SquareYard rhs) { return lhs.m_value == rhs.m_value; }
        public static bool operator !=(SquareYard lhs, SquareYard rhs) { return lhs.m_value != rhs.m_value; }
        public static bool operator <(SquareYard lhs, SquareYard rhs) { return lhs.m_value < rhs.m_value; }
        public static bool operator >(SquareYard lhs, SquareYard rhs) { return lhs.m_value > rhs.m_value; }
        public static bool operator <=(SquareYard lhs, SquareYard rhs) { return lhs.m_value <= rhs.m_value; }
        public static bool operator >=(SquareYard lhs, SquareYard rhs) { return lhs.m_value >= rhs.m_value; }
        public int /* IComparable<SquareYard> */ CompareTo(SquareYard other) { return this.m_value.CompareTo(other.m_value); }
        #endregion

        #region Arithmetic
        // Inner:
        public static SquareYard operator +(SquareYard lhs, SquareYard rhs) { return new SquareYard(lhs.m_value + rhs.m_value); }
        public static SquareYard operator -(SquareYard lhs, SquareYard rhs) { return new SquareYard(lhs.m_value - rhs.m_value); }
        public static SquareYard operator ++(SquareYard q) { return new SquareYard(q.m_value + 1d); }
        public static SquareYard operator --(SquareYard q) { return new SquareYard(q.m_value - 1d); }
        public static SquareYard operator -(SquareYard q) { return new SquareYard(-q.m_value); }
        public static SquareYard operator *(double lhs, SquareYard rhs) { return new SquareYard(lhs * rhs.m_value); }
        public static SquareYard operator *(SquareYard lhs, double rhs) { return new SquareYard(lhs.m_value * rhs); }
        public static SquareYard operator /(SquareYard lhs, double rhs) { return new SquareYard(lhs.m_value / rhs); }
        public static double operator /(SquareYard lhs, SquareYard rhs) { return lhs.m_value / rhs.m_value; }
        // Outer:
        public static Yard operator /(SquareYard lhs, Yard rhs) { return new Yard(lhs.m_value / rhs.m_value); }
        public static CubicYard operator *(SquareYard lhs, Yard rhs) { return new CubicYard(lhs.m_value * rhs.m_value); }
        public static CubicYard operator *(Yard lhs, SquareYard rhs) { return new CubicYard(lhs.m_value * rhs.m_value); }
        #endregion

        #region Formatting
        public override string ToString() { return ToString(SquareYard.Format, null); }
        public string ToString(string format) { return ToString(format, null); }
        public string ToString(IFormatProvider fp) { return ToString(SquareYard.Format, fp); }
        public string /* IFormattable */ ToString(string format, IFormatProvider fp)
        {
            return string.Format(fp, format ?? SquareYard.Format, m_value, SquareYard.Symbol.Default);
        }
        #endregion

        #region Static fields
        private static readonly Dimension s_sense = Yard.Sense * Yard.Sense;
        private static readonly int s_family = SquareMeter.Family;
        private static /*mutable*/ double s_factor = Yard.Factor * Yard.Factor;
        private static /*mutable*/ string s_format = "{0} {1}";
        private static readonly SymbolCollection s_symbol = new SymbolCollection("yd\u00B2", "sq yd");
        private static readonly Unit<double> s_proxy = new SquareYard_Proxy();

        private static readonly SquareYard s_one = new SquareYard(1d);
        private static readonly SquareYard s_zero = new SquareYard(0d);
        #endregion

        #region Static Properties
        public static Dimension Sense { get { return s_sense; } }
        public static int Family { get { return s_family; } }
        public static double Factor { get { return s_factor; } set { s_factor = value; } }
        public static string Format { get { return s_format; } set { s_format = value; } }
        public static SymbolCollection Symbol { get { return s_symbol; } }
        public static Unit<double> Proxy { get { return s_proxy; } }

        public static SquareYard One { get { return s_one; } }
        public static SquareYard Zero { get { return s_zero; } }
        #endregion
    }

    public partial class SquareYard_Proxy : Unit<double>
    {
        #region Properties
        public override int Family { get { return SquareYard.Family; } }
        public override Dimension Sense { get { return SquareYard.Sense; } }
        public override SymbolCollection Symbol { get { return SquareYard.Symbol; } }
        public override double Factor { get { return SquareYard.Factor; } set { SquareYard.Factor = value; } }
        public override string Format { get { return SquareYard.Format; } set { SquareYard.Format = value; } }
        #endregion

        #region Constructor(s)
        public SquareYard_Proxy() :
            base(typeof(SquareYard))
        {
        }
        #endregion

        #region Methods
        public override IQuantity<double> Create(double value)
        {
            return new SquareYard(value);
        }
        public override IQuantity<double> From(IQuantity<double> quantity)
        {
            return SquareYard.From(quantity);
        }
        #endregion
    }
}
