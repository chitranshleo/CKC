using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKC
{
    public static class Factors
    {
        public static readonly double FactorMilToInch       = (1.0/1000.0);
        public static readonly double FactorMicronsToInch = (1.0 / 25400.0);
        public static readonly double FactorMmToInch = (1.0 / 25.4);
        public static readonly double FactorCmToInch = (1.0 / 2.54);
        public static readonly double FactorInchToInch = 1.0;
        public static readonly double FactorOzToInch = (1.0 / 730.0);

    }

    public static class Util
    {
        public const string Mil = "mil";
        public const string Microns = "microns";
        public const string MM = "mm";
        public const string CM = "cm";
        public const string Inch = "inch";
        public const string OZ = "oz";
    }

    public interface IFormulae
    {
        double Calculate(double input);
    }

    #region Input Formulae

    public class MilToInch : IFormulae
    {
        public double Calculate(double input)
        {
            return input * Factors.FactorMilToInch;
        }
    }

    public class MicronsToInch : IFormulae
    {
        public double Calculate(double input)
        {
            return input * Factors.FactorMicronsToInch;
        }
    }

    public class MMToInch : IFormulae
    {
        public double Calculate(double input)
        {
            return input * Factors.FactorMmToInch;
        }
    }

    public class CMToInch : IFormulae
    {
        public double Calculate(double input)
        {
            return input * Factors.FactorCmToInch;
        }
    }

    public class OZToInch : IFormulae
    {
        public double Calculate(double input)
        {
            return input * Factors.FactorOzToInch;
        }
    }

    #endregion


    #region Output Formulae

    public class InchToMil : IFormulae
    {
        public double Calculate(double input)
        {
            return input / Factors.FactorMilToInch;
        }
    }

    public class InchToMicrons : IFormulae
    {
        public double Calculate(double input)
        {
            return input / Factors.FactorMicronsToInch;
        }
    }

    public class InchToMM : IFormulae
    {
        public double Calculate(double input)
        {
            return input / Factors.FactorMmToInch;
        }
    }

    public class InchToCM : IFormulae
    {
        public double Calculate(double input)
        {
            return input / Factors.FactorCmToInch;
        }
    }

    public class InchToInch : IFormulae
    {
        public double Calculate(double input)
        {
            return input / Factors.FactorInchToInch;
        }
    }

    public class InchToOZ : IFormulae
    {
        public double Calculate(double input)
        {
            return input / Factors.FactorOzToInch;
        }
    }

    #endregion
}
