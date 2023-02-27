using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3_UniversalCalculator
{
    /// <summary>
    /// This is the class for the BMR calculator
    /// </summary>
    class CalorieCalculator
    {
        private int age = 0; //Age input
        private double height = 0; //cm or inches
        private double weight = 0; //kg or lbs
        private double activity = 0; //Activity level
        private Gender gender;
        #region Getters and Setters

        public double GetActivity()
        {
            return activity;
        }
        public void SetActivity(double setactivity)
        {
            if (setactivity > 0.0)
                activity = setactivity;
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
        public int GetAge()
        {
            return age;
        }
        public void SetAge(int setage)
        {
            if (setage > 0.0)
                age = setage;
        }
        public Gender GetGender()
        {
            return gender;
        }
        public void SetGender(Gender value)
        {
            gender = value;
        }
        #endregion
        #region Calculations
        public double BMRCalculation() //Calculates the BMR
        {
            double BMR = 0.0;


            if (gender == Gender.Female)
                BMR = (10.0 * weight) + (6.25 * height) - (5 * age) - 161;

            else
                BMR = (10.0 * weight) + (6.25 * height) - (5 * age) + 5;
            BMR = Math.Round(BMR, 1);

            return BMR;
        }
        public double maintainWeightCalories() //Calculates the amount of calories to maintain weight
        {
            double maintain = 0.0;
            maintain = BMRCalculation() * activity;
            maintain = Math.Round(maintain, 1);

            return maintain;
        }

        public double lose500() //Calculates the amount of calories to lose 0.5kg
        {
            double result = 0.0;
            result = maintainWeightCalories() - 500;
            result = Math.Round(result, 1);

            return result;
        }
        public double lose1000() //Calculates the amount of calories to 1 kg
        {
            double result = 0.0;
            result = maintainWeightCalories() - 1000;
            result = Math.Round(result, 1);

            return result;
        }
        public double gain500() //Calculates the amount of calories to gain 0.5kg
        {
            double result = 0.0;
            result = maintainWeightCalories() + 500;
            result = Math.Round(result, 2);

            return result;
        }
        public double gain1000() //Calculates the amount of calories to gain 1kg
        {
            double result = 0.0;
            result = maintainWeightCalories() + 1000;
            result = Math.Round(result, 1);

            return result;
        }
        #endregion
    }
}
