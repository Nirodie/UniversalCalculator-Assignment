using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment3_UniversalCalculator
{
    /// <summary>
    /// The main class where the GUI is created
    /// </summary>
    public partial class MainForm : Form
    {
        private BMICalculator bmiCalc = new BMICalculator();
        private FuelCalculator carMilage = new FuelCalculator();
        private CalorieCalculator bmrCalc = new CalorieCalculator();

        public MainForm()
        {
            InitializeComponent();
            InitializeGUI(); //Initialises the visuals
        }

        private void InitializeGUI()
        {
            this.Text = "Universal Calculator"; //The title text

            //Input
            rbtnMetric.Checked = true;
            lblHeight.Text = "Height (cm)";
            lblWeight.Text = "Weight (kg)";
            rbtnFemale.Checked = true;
            rbtnNo.Checked = true;
            listBMR.SelectionMode = SelectionMode.One;

            //Output
            txtHeight.Text = string.Empty;
            txtWeight.Text = string.Empty;
            txtCurrentReading.Text = string.Empty;
            txtPreviousReading.Text = string.Empty;
            txtCurrentFuel.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtAge.Text = string.Empty;
            lblList.Text = string.Empty;
        }
        //Reads the name input from the BMI calculator
        private string ReadName()
        {
          
            string value = txtName.Text;
            value = value.Trim();
            bmiCalc.SetName(value);

            return value;
        }

        //The calculation button for the BMI calculator.
        private void btnCalculateBMI_Click(object sender, EventArgs e)
        {
            bool ok = ReadInputBMI();

            if (ok)
            {
                DisplayResultsBMI(); //Calculates and displays
            }
        }   
        /// <summary>
        /// Displays the results of the whole BMI calculation
        /// </summary>
        private void DisplayResultsBMI()
        {
            lblBMI.Text = bmiCalc.CalculateBMI().ToString("f2");
            lblWeightCategory.Text = bmiCalc.BmiWeightCategory().ToString();
            grpResultsName.Text = "Results for " + bmiCalc.GetName(); //It always shows "results for no name" if i directly use the "ReadName" method it works
        }
        /// <summary>
        /// Displays the result of the fuel calculation
        /// </summary>
        private void DisplayResultsCarMilage()
        {
            lblConKmLit.Text = carMilage.KmPerLit().ToString("f2");
            lblConLitKm.Text = carMilage.LitPerKm().ToString("f2");
            lblConLitMM.Text = carMilage.LitPerMetricMile().ToString("f2");
            lblConLitSwe.Text = carMilage.LitPerSweMil().ToString("f2");
            lblCost.Text = carMilage.CostPerKm().ToString("f2");
        }
        /// <summary>
        /// Displays the result of the entire BMR calculation inside the listbox
        /// </summary>
        private void DisplayResultsBMR()
        {
            string yourbmiout = string.Format("Your BMR (calories per day){0,33}", bmrCalc.BMRCalculation());
            string maintainout = string.Format("Calories to maintain your current weight{0,20}", bmrCalc.maintainWeightCalories());
            string lose500out = string.Format("Calories to lose 0,5 kg per week{0,28}", bmrCalc.lose500());
            string lose1000out = string.Format("Calories to lose 1 kg per week{0,30}", bmrCalc.lose1000());
            string gain500out = string.Format("Calories to gain 0,5 kg per week{0,28}", bmrCalc.gain500());
            string gain1000out = string.Format("Calories to gain 1 kg per week{0,30}", bmrCalc.gain1000());

            listBMR.Items.Clear();
            listBMR.Items.Add("BMR results for " + bmiCalc.GetName());
            listBMR.Items.Add(""); //Empty slot
            listBMR.Items.Add(yourbmiout);
            listBMR.Items.Add(""); //Empty slot
            listBMR.Items.Add(maintainout);
            listBMR.Items.Add(""); //Empty slot
            listBMR.Items.Add(lose500out);
            listBMR.Items.Add(""); //Empty slot
            listBMR.Items.Add(lose1000out);
            listBMR.Items.Add(""); //Empty slot
            listBMR.Items.Add(gain500out);
            listBMR.Items.Add(""); //Empty slot
            listBMR.Items.Add(gain1000out);
            listBMR.Items.Add(""); //Empty slot
            listBMR.Items.Add(""); //Empty slot
            listBMR.Items.Add("Losing more than 1000 calories per day is to be avoided.");
        }
        private bool ReadInputBMR() //combines and reads the BMR calculator inputs and checks if they're ok
        {
            bool heightok = ReadHeight();
            bool weightok = ReadWeight();
            bool ageok = ReadAge();
            ReadName();

            return heightok && weightok && ageok;
        }
        //Reads all of the inputs for the BMI calculator and checks if they're OK
        private bool ReadInputBMI() //combines and reads the BMI calculator inputs and checks if they're ok
        {
            bool heightok = ReadHeight();
            bool weightok = ReadWeight();
            ReadName();

            return heightok && weightok;
        }
        private bool ReadInputCarMilage() //combines and reads the fuel calculator inputs and checks if they're ok
        {

            bool prevdistanceok = ReadPreviousDistance();
            bool curdisanceok = ReadCurrentDistance();
            bool curFuelok = ReadFuel();
            bool priceok = ReadPrice();

            return prevdistanceok && curdisanceok && curFuelok && priceok;
        }
        //Reads the height input for the BMI and BMR calculator
        private bool ReadHeight() //Reads the height input for both the BMR and the BMI calculators
        {
            double outValue = 0;
            bool ok = double.TryParse(txtHeight.Text, out outValue);
            if (ok)
            {
                if (bmiCalc.GetUnit() == UnitTypes.American)
                {
                    bmiCalc.SetHeight(outValue * 12.0);
                    bmrCalc.SetHeight(outValue * 30.48); //Converts inches into cm as well as sends it to the client
                }
                else
                {
                    bmiCalc.SetHeight(outValue / 100.0); //Converts cm to meter
                    bmrCalc.SetHeight(outValue);
                }
            }
            else ok = false;
            if (!ok)
                MessageBox.Show("Invalid height!");
            return ok;
        }
        //Reads the weight input
        private bool ReadWeight() //Reads the weight input for both the BMR and the BMI calculators
        {
            bool ok = true;
            string strWeight = txtWeight.Text;
            strWeight = strWeight.Trim();
            double weight = 0.0;

            ok = double.TryParse(strWeight, out weight);
            if (ok && bmiCalc.GetUnit() == UnitTypes.American)
            {
                bmrCalc.SetWeight(weight * 0.45359237); //Converts to kg as well as sends it to the client
                bmiCalc.SetWeight(weight);
            }
            else
            if (ok && bmiCalc.GetUnit() == UnitTypes.Metric)
            {
                bmiCalc.SetWeight(weight);
                bmrCalc.SetWeight(weight);
            }
            else
                MessageBox.Show("Invalid weight");

            return ok;
        }
        private bool ReadPreviousDistance() //Reads the previous distance for the fuel calculator
        {
            bool ok = true;
            string strprevDistance = txtPreviousReading.Text;
            strprevDistance = strprevDistance.Trim();
            double prevDis = 0.0;

            ok = double.TryParse(strprevDistance, out prevDis);
            if (ok)
                carMilage.SetPrevDist(prevDis);
            else
                MessageBox.Show("Invalid distance");

            return ok;
        }
        private bool ReadCurrentDistance() //Reads the current distance for the fuel calculator
        {
            bool ok = true;
            string strcurReading = txtCurrentReading.Text;
            strcurReading = strcurReading.Trim();
            double curDis = 0.0;

            ok = double.TryParse(strcurReading, out curDis);
            if (ok)
                carMilage.SetCurDist(curDis);
            else
                MessageBox.Show("Invalid distance");

            return ok;
        }
        private bool ReadFuel() //Reads the current fuel input for the fuel calculator
        {
            bool ok = true;
            string strFuel = txtCurrentFuel.Text;
            strFuel = strFuel.Trim();
            double curFuel = 0.0;

            ok = double.TryParse(strFuel, out curFuel);
            if (ok)
                carMilage.SetFuel(curFuel);
            else
                MessageBox.Show("Invalid fuel amount");

            return ok;
        }
        private bool ReadPrice() //Reads the price input for the fuel calculator
        {
            bool ok = true;
            string strPrice = txtPrice.Text;
            strPrice = strPrice.Trim();
            double cost = 0.0;

            ok = double.TryParse(strPrice, out cost);
            if (ok)
                carMilage.SetPrice(cost);
            else
                MessageBox.Show("Invalid price amount");

            return ok;
        }
        private bool ReadAge() //Reads the age input for the BMR calculator
        {
            bool ok = true;
            string strAge = txtAge.Text;
            strAge = strAge.Trim();
            int age = 0;

            ok = int.TryParse(strAge, out age);
            if (ok)
                bmrCalc.SetAge(age);
            else
                MessageBox.Show("Invalid age");

            return ok;
        }

        private void rbtnMetric_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnMetric.Checked)
            {
                lblHeight.Text = "Height (cm)";
                lblWeight.Text = "Weight (kg)";
                bmiCalc.SetUnit(UnitTypes.Metric);
            }
        }

        private void rbtnUS_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnUS.Checked)
            {
                lblHeight.Text = "Height (feet)";
                lblWeight.Text = "Weight (lbs)";
                bmiCalc.SetUnit(UnitTypes.American);
            }
        }

        private void btnCalculateFuel_Click(object sender, EventArgs e)
        {
            bool ok = ReadInputCarMilage();

            if (ok)
            {
                DisplayResultsCarMilage(); //Calculates and displays
            }
        }

        private void btnUnselect_Click(object sender, EventArgs e) //Unselects the listbox selection and clears the string
        {
            listBMR.ClearSelected();
            lblList.Text = string.Empty;
        }

        private void btnCalculateBMR_Click(object sender, EventArgs e)
        {
            bool ok = ReadInputBMR();

            if (ok)
            {
                DisplayResultsBMR(); //Calculates and displays
            }
        }

        private void rbtnFemale_CheckedChanged(object sender, EventArgs e)
        {
            bmrCalc.SetGender(Gender.Female);
        }

        private void rbtnMale_CheckedChanged(object sender, EventArgs e)
        {
            bmrCalc.SetGender(Gender.Male);
        }

        private void rbtnNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnNo.Checked)
                bmrCalc.SetActivity(1.2);
        }

        private void rbtnLight_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnLight.Checked)
                bmrCalc.SetActivity(1.375);
        }

        private void rbtnModerate_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnModerate.Checked)
                bmrCalc.SetActivity(1.550);
        }

        private void rbtnVery_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnVery.Checked)
                bmrCalc.SetActivity(1.725);
        }

        private void rbtnExtreme_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnExtreme.Checked)
                bmrCalc.SetActivity(1.9);
        }

        private void listBMR_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            lblList.Text = (listBMR.SelectedIndex.ToString());         
        }
    }
}
