using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3_UniversalCalculator
{
    /// <summary>
    /// This class is for calculating the car milage
    /// </summary>
    class FuelCalculator
    {
        private double prevDist = 0; //km
        private double curDist = 0; //km
        private double fuel = 0; //liters
        private double price = 0; //No unit can use any currency

        #region Getters & Setters
        public double GetPrice()
        {
            return price;
        }
        public void SetPrice(double value)
        {
            if (value >= 0.0)
                price = value;
        }
        public double GetPrevDist()
        {
            return prevDist;
        }
        public void SetPrevDist(double value)
        {
            if (value > 0.0)
                prevDist = value;
        }
        public double GetCurDist()
        {
            return curDist;
        }
        public void SetCurDist(double value)
        {
            if (value > 0.0)
                curDist = value;
        }
        public double GetFuel()
        {
            return fuel;
        }
        public void SetFuel(double value)
        {
            if (value > 0.0)
                fuel = value;
        }
        #endregion
        #region Calculations
        public double LitPerKm() //Calculates liters per km
        {
            double result = 0;

            result = fuel / (curDist - prevDist);

            return result;
        }
        public double KmPerLit() //Calculates km per liter
        {
            double result = 0;

            result = (curDist - prevDist) / fuel;

            return result;
        }
        public double LitPerMetricMile() //Calculates liters per metric mile
        {
            const double kmToMileFactor = 0.621371192;
            double result = 0;

            result = LitPerKm() / kmToMileFactor;

            return result;
        }
        public double LitPerSweMil() //Calculates liters per swedish mile
        {
            double result = 0;

            result = LitPerKm() * 10;

            return result;
        }
        public double CostPerKm() //Calculates the cost per km
        {
            double result = 0;

            result = LitPerKm() * price;

            return result;
        }
        #endregion
    }
}
