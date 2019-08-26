/*******************************************************************************

    Units of Measurement for C# applications

    Copyright (C) Marek Aniola

    This program is provided to you under the terms of the license
    as published at https://github.com/mangh/unitsofmeasurement


********************************************************************************/
using System;

namespace UnitsOfMeasurement
{
    public static partial class Catalog
    {
        #region Constructor
        static Catalog()
        {
            Allocate(
                48 + 14, // 48 units  + 14 entries for possible late units (1 for each family)
                3 + 1 // 3 scales + 1 entries for possible late scales (1 for each family)
            );
            Populate();
        }
        #endregion

        #region Populate
        public static void Populate()
        {
            // units:
            Add(Meter.Proxy);            // [L] Meter {"m"} : 1  (Meter)
            Add(Centimeter.Proxy);            // [L] Centimeter {"cm"} : 100  (Meter)
            Add(Millimeter.Proxy);            // [L] Millimeter {"mm"} : 1000  (Meter)
            Add(Inch.Proxy);            // [L] Inch {"in"} : 39.370078740157481  (Meter)
            Add(Foot.Proxy);            // [L] Foot {"ft"} : 3.2808398950131235  (Meter)
            Add(Yard.Proxy);            // [L] Yard {"yd"} : 1.0936132983377078  (Meter)
            Add(Mile.Proxy);            // [L] Mile {"mil"} : 0.000621371192237334  (Meter)
            Add(Point.Proxy);            // [L] Point {"pt"} : 2834.6456692913389  (Meter)
            Add(Pica.Proxy);            // [L] Pica {"pc"} : 236.22047244094492  (Meter)
            Add(Second.Proxy);            // [T] Second {"s"} : 1  (Second)
            Add(Minute.Proxy);            // [T] Minute {"min"} : 0.016666666666666666  (Second)
            Add(Hour.Proxy);            // [T] Hour {"h"} : 0.00027777777777777778  (Second)
            Add(Day.Proxy);            // [T] Day {"d"} : 1.1574074074074074E-05  (Second)
            Add(Week.Proxy);            // [T] Week {"wk"} : 1.6534391534391533E-06  (Second)
            Add(Month.Proxy);            // [T] Month {"mo"} : 3.8580246913580245E-07  (Second)
            Add(Year.Proxy);            // [T] Year {"yr"} : 3.1709791983764586E-08  (Second)
            Add(Kilogram.Proxy);            // [M] Kilogram {"kg"} : 1  (Kilogram)
            Add(Gram.Proxy);            // [M] Gram {"g"} : 1000  (Kilogram)
            Add(Pound.Proxy);            // [M] Pound {"lb"} : 2.2046226218487757  (Kilogram)
            Add(Ounce.Proxy);            // [M] Ounce {"ou"} : 35.273961949580411  (Kilogram)
            Add(DegKelvin.Proxy);            // [ϴ] DegKelvin {"K", "deg.K"} : 1  (DegKelvin)
            Add(DegCelsius.Proxy);            // [ϴ] DegCelsius {"\u00B0C", "deg.C"} : 1  (DegKelvin)
            Add(DegFahrenheit.Proxy);            // [ϴ] DegFahrenheit {"\u00B0F", "deg.F"} : 1.8  (DegKelvin)
            Add(Candela.Proxy);            // [J] Candela {"cd"} : 1  (Candela)
            Add(Radian.Proxy);            // [1] Radian {"rad"} : 1  (Radian)
            Add(Degree.Proxy);            // [1] Degree {"\u00B0", "deg"} : (180d / Math.PI) * Radian.Factor  (Radian)
            Add(SquareMeter.Proxy);            // [L2] SquareMeter {"m\u00B2", "m2"} : 1  (SquareMeter)
            Add(SquareCentimeter.Proxy);            // [L2] SquareCentimeter {"cm\u00B2", "cm2"} : 10000  (SquareMeter)
            Add(SquareInch.Proxy);            // [L2] SquareInch {"in\u00B2", "sq in"} : 1550.0031000062002  (SquareMeter)
            Add(SquareFoot.Proxy);            // [L2] SquareFoot {"ft\u00B2", "sq ft"} : 10.763910416709724  (SquareMeter)
            Add(SquareYard.Proxy);            // [L2] SquareYard {"yd\u00B2", "sq yd"} : 1.1959900463010804  (SquareMeter)
            Add(SquareMile.Proxy);            // [L2] SquareMile {"mil\u00B2", "sq mil"} : 3.8610215854244592E-07  (SquareMeter)
            Add(CubicMeter.Proxy);            // [L3] CubicMeter {"m\u00B3", "m3"} : 1  (CubicMeter)
            Add(CubicCentimeter.Proxy);            // [L3] CubicCentimeter {"cm\u00B3", "cm3"} : 1000000  (CubicMeter)
            Add(Liter.Proxy);            // [L3] Liter {"L"} : 0.001  (CubicMeter)
            Add(CubicInch.Proxy);            // [L3] CubicInch {"in\u00B3", "cu in"} : 61023.74409473229  (CubicMeter)
            Add(CubicFoot.Proxy);            // [L3] CubicFoot {"ft\u00B3", "cu ft"} : 35.314666721488592  (CubicMeter)
            Add(CubicYard.Proxy);            // [L3] CubicYard {"yd\u00B3", "cu yd"} : 1.3079506193143924  (CubicMeter)
            Add(Meter_Sec.Proxy);            // [LT-1] Meter_Sec {"m/s"} : 1  (Meter_Sec)
            Add(MPH.Proxy);            // [LT-1] MPH {"mph", "mi/h"} : 2.2369362920544025  (Meter_Sec)
            Add(Meter_Sec2.Proxy);            // [LT-2] Meter_Sec2 {"m/s2"} : 1  (Meter_Sec2)
            Add(Newton.Proxy);            // [LT-2M] Newton {"N"} : 1  (Newton)
            Add(Joule.Proxy);            // [L2T-2M] Joule {"J"} : 1  (Joule)
            Add(Calorie.Proxy);            // [L2T-2M] Calorie {"cal"} : 0.23884589662749595  (Joule)
            Add(BTU.Proxy);            // [L2T-2M] BTU {"btu"} : 0.00094781712031331725  (Joule)
            Add(Watt.Proxy);            // [L2T-3M] Watt {"W"} : 1  (Watt)
            Add(BTU_Hour.Proxy);            // [L2T-3M] BTU_Hour {"btu/h"} : 3.4121416331279422  (Watt)
            Add(NewtonMeter.Proxy);            // [L2T-2M] NewtonMeter {"N\u00B7m", "N*m"} : 1  (NewtonMeter)

            // scales:
            Add(Kelvin.Proxy);            // [ϴ] Kelvin : AbsoluteZero = DegKelvin 0  (Kelvin)
            Add(Celsius.Proxy);            // [ϴ] Celsius : AbsoluteZero = DegCelsius -273.15  (Kelvin)
            Add(Fahrenheit.Proxy);            // [ϴ] Fahrenheit : AbsoluteZero = DegFahrenheit -459.66999999999996  (Kelvin)
        }
        #endregion
    }
}
