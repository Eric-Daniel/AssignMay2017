using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EricDaniel_Assignment.Classes
{
    [Serializable]
    public class Car
    {
        public Car()
        {

        }

        private string registrationNumber;

        public string RegistrationNumber
        {
            get { return registrationNumber; }
            set { registrationNumber = value; }
        }

        private string model;

        public string Model
        {
            get { return model; }
            set { model = value; }
        }

        private int year;

        public int Year
        {
            get { return year; }
            set { year = value; }
        }

        public Car(string registrationNumber, string model, int year)
        {
            this.registrationNumber = registrationNumber;
            this.model = model;
            this.year = year;

        }
    }
}
