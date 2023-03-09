using System;

namespace WebApplication2.Models
{
    //model table [Employees]
    public class ModelDB
    {
        public string Payroll_Number { get; set; }
        public string Forenames { get; set; }
        public string Surname { get; set; }
        public DateTime Date_of_Birth { get; set; }
        public int Telephone { get; set; } 
        public int Mobile { get; set; }
        public string Address { get; set; }
        public string Address_2 { get; set; }
        public string Postcode { get; set; }
        public string EMail_Home { get; set; } 
        public DateTime Start_Date { get; set; }
    }
}
