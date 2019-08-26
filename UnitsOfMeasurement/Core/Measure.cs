﻿/*******************************************************************************

    Units of Measurement for C# applications

    Copyright (C) Marek Aniola

    This program is provided to you under the terms of the license
    as published at https://github.com/mangh/unitsofmeasurement


********************************************************************************/
using System;
using System.Reflection;

namespace UnitsOfMeasurement
{
    /// <summary>
    /// Base proxy representing either a unit or a scale and giving access to their common properties.
    /// </summary>
    public abstract class Measure : IEquatable<Measure>
    {
        #region Constants
        public static readonly RuntimeTypeHandle NoneMeasureTypeHandle = typeof(object).TypeHandle;
        public static readonly TypeCode NoneMeasureTypeCode = TypeCode.Object;
        #endregion

        #region Fields
        private readonly RuntimeTypeHandle m_handle;
        #endregion

        #region Properties

        public RuntimeTypeHandle Handle { get { return m_handle; } }
        public Type Type { get { return Type.GetTypeFromHandle(m_handle); } }
        public abstract int Family { get; }
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Measure (unit|scale) type proxy constructor
        /// </summary>
        /// <param name="t">measure (unit|scale) type</param>
        protected Measure(Type t)
        {
            m_handle = t.TypeHandle;
        }
        #endregion

        #region IEquatable<Measure>
        public override int GetHashCode() { return m_handle.GetHashCode(); }
        public override bool Equals(object obj) { return (obj is Measure) && Equals((Measure)obj); }
        public bool Equals(Measure other) { return this.m_handle.Equals(other.Handle); }
        #endregion

        #region Methods
        /// <summary>
        /// Get static property of the measure (unit|scale)
        /// </summary>
        /// <param name="T">Type code for a type parameter T used in Unit&lt;T&gt; or Scale&lt;T&gt; definition.</param>
        /// <returns>
        /// Unit.GenericTypeHandle (for Unit&lt;&gt; subclass) |
        /// Scale.GenericTypeHandle (for Scale&lt;&gt; subclass) |
        /// Measure.NoneMeasureTypeHandle (for any other type).
        /// </returns>
        public RuntimeTypeHandle Examine(out TypeCode T)
        {
            Type b = this.GetType().BaseType;
            while (b != null)
            {
                if (b.IsGenericType)
                {
                    Type[] args = b.GetGenericArguments();
                    if (args.Length == 1)
                    {
                        T = Type.GetTypeCode(args[0]);

                        if (b.FullName.StartsWith(Unit.GenericTypeFullName))
                            return Unit.GenericTypeHandle;

                        else if (b.FullName.StartsWith(Scale.GenericTypeFullName))
                            return Scale.GenericTypeHandle;
                    }
                }
                b = b.BaseType;
            }
            T = NoneMeasureTypeCode;
            return NoneMeasureTypeHandle;
        }
        #endregion

        #region Formatting
        public override string ToString()
        {
            return string.Format("({0}) {1}", Family, Type.Name);
        }
        #endregion

        #region Statics
        /// <summary>
        /// Converts an instance of a measure (unit|scale) to the unit of measurement of this measure.
        /// </summary>
        /// <param name="t">Input type to retrieve proxy from.</param>
        /// <returns>Proxy type (Unit&lt;T&gt; or Scale&lt;T&gt;) for a unit or scale input type; null for any other types.</returns>
        public static Measure TryRetrieveFrom(Type t)
        {
            if (t.IsValueType)
            {
                foreach (Type ifc in t.GetInterfaces())
                {
                    if (ifc.FullName.StartsWith(Unit.GenericInterfaceFullName) ||
                        ifc.FullName.StartsWith(Scale.GenericInterfaceFullName))
                    {
                        PropertyInfo proxyInfo = t.GetProperty("Proxy", BindingFlags.Static | BindingFlags.Public);
                        if (proxyInfo != null)
                        {
                            return proxyInfo.GetValue(t, null) as Measure;
                        }
                    }
                }
            }
            return null;
        }
        #endregion
    }
}
