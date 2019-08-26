﻿/*******************************************************************************

    Units of Measurement for C# applications

    Copyright (C) Marek Aniola

    This program is provided to you under the terms of the license
    as published at https://github.com/mangh/unitsofmeasurement


********************************************************************************/

namespace UnitsOfMeasurement
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IQuantity<T> where T : struct
    {
        // Quantity (instance) properties
        T Value { get; }

        // Unit (class) properties
        Unit<T> Unit { get; }
    }
}
