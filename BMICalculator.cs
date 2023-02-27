using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3_UniversalCalculator
{
    /// <summary>
    /// This class is for calculating the BMI
    /// </summary>
    class BMICalculator
    {
        private string Name = "No Name";
        private double height = 0; //m or inches
        private double weight = 0; //kg or lbs
        private UnitTypes unit;

        #region Getters & Setters
        public string GetName()
        {
            return Name;
        }
        public void SetName(string value)
        {
            if (!string.IsNullOrEmpty(value))
                Name = value;
        }
        public double GetHeight()
        {
            return height;
        }
        public void SetHeight(double setheight)
        {
            if (setheight > 0.0)
                height = setheight;
        }
        public double GetWeight()
        {
            return weight;
        }
        public void SetWeight(double setweight)
        {
            if (setweight > 0.0)
                weight = setweight;
        }
        public UnitTypes GetUnit()
        {
            return unit;
        }
        public void SetUnit (UnitTypes value)
        {
            unit = value;
        }
        #endregion
        /// <summary>
        /// This method calculates the input and gives the BMI accordingly, it can calculate with both metric and US units
        /// </summary>
        /// <returns>The BMI result</returns>
        public double CalculateBMI()
        {
            double result = 0.0;
            if (GetUnit() == UnitTypes.American)
            {
                if (height > 0.0) //Checks division by zero
                    result = 703.0*weight / (height * height); //BMI calculation with american units
            }
            else
            if (height > 0.0) //Checks division by zero
                result = weight / (height * height); //BMI calculation with metric units


            return result;
        }

        /// <summary>
        /// Method that sets the weight category
        /// </summary>
        /// <returns>Weight category based on BMI result</returns>
         public string BmiWeightCategory()
        {
            double bmi = CalculateBMI(); 
        string stringout = string.Empty;
            if (bmi > 40)
                stringout = "Overweight (Obesity class III)";
            else if (bmi < 18.5)
                stringout = "Underweight";
            else if (bmi < 25)
                stringout = "Normal weight";
            else if (bmi < 30)
                stringout = "Overweight (Pre-obesity)";
            else if (bmi < 35)
                stringout = "overweight (Obesity class I)";
            else if (bmi < 40)
                stringout = "Overweight (Obesity class II)";

            return stringout;
        }
}

}
