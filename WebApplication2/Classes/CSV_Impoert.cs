using Microsoft.VisualBasic.FileIO;
using System.Data;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using WebApplication2.Models;
using System.IO;
using System.Globalization;

namespace WebApplication2.Classes
{
    public class CSV_Impoert
    {
        List<ModelDB> data;

        public CSV_Impoert()
        {
            data = new List<ModelDB>(); 
        }
        //class converts csv to list<model>
        public async Task<List<ModelDB>> Load_from_CSV(TextFieldParser csvReader)
        {
            csvReader.SetDelimiters(new string[] { "," });
            
            csvReader.HasFieldsEnclosedInQuotes = true;

            bool firstStep = true;
            while (!csvReader.EndOfData)
            {
                string[] fieldData = csvReader.ReadFields();
                if(firstStep)
                {
                    firstStep = false;continue;
                }

                //line by line read the data from the csv and create a new model. then add the model to the list
                ModelDB temp = new ModelDB() // создать класс  который равен 1 строке
                {
                    Payroll_Number = fieldData[0], //типа 
                    Forenames = fieldData[1],
                    Surname = fieldData[2],
                    Date_of_Birth = DateTime.ParseExact(fieldData[3], "dd/M/yyyy", CultureInfo.InvariantCulture),
                    Telephone = Convert.ToInt32(fieldData[4]),
                    Mobile = Convert.ToInt32(fieldData[5]),
                    Address = fieldData[6],
                    Address_2 = fieldData[7],
                    Postcode = fieldData[8],
                    EMail_Home = fieldData[9],
                    Start_Date = DateTime.ParseExact(fieldData[10], "dd/M/yyyy", CultureInfo.InvariantCulture)
            };


                data.Add(temp);
            }
         
            return data;
        }
    }
}
