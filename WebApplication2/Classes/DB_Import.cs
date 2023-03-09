using System.Collections.Generic;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WebApplication2.Models;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace WebApplication2.Classes
{
    public class DB_Import
    {

        List<ModelDB> data;
        SqlConnection connectionString;
        SqlCommand queryConnectDB;

        public DB_Import(string ConnectionString = @"Server=LEGION\SQLEXPRESS;Database=D_BASE_1;Trusted_Connection=True")
        {
            //open sql connection
            connectionString = new SqlConnection(ConnectionString);//Path
            try
            {
                connectionString.Open(); 
                queryConnectDB = new SqlCommand("use DataBase_1", connectionString);
                queryConnectDB.Connection = connectionString;
                queryConnectDB.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Console.WriteLine("БД подключено");
            }
        }


        public async Task Add_to_DB(List<ModelDB> data)
        {
            //query for write all data in table [Employees]
            foreach (var item in data)
            {
                queryConnectDB = new SqlCommand($"INSERT INTO [Employees] " +
                    $"VALUES ( '{item.Payroll_Number}','{item.Forenames}','{item.Surname}'," +
                    $"'{item.Date_of_Birth}',{item.Telephone},{item.Mobile},'{item.Address}','{item.Address_2}'," +
                    $"'{item.Postcode}','{item.EMail_Home}','{item.Start_Date}')", connectionString);
                
                queryConnectDB.ExecuteNonQuery(); // выполнить командлу
            }

        }

        //load all data from table [Employees]
        public async Task< List<ModelDB> > Load_From_DB()
        { 
            //query for get all data

            SqlCommand query = new SqlCommand("SELECT * FROM [Employees]", connectionString); // создать команду

            SqlDataReader answer = query.ExecuteReader(); // выполнить командлу

            data = new List<ModelDB>();

            if (answer.HasRows)//не пустой ли ответ
            {
                while (answer.Read())// ссчитать строку
                {
                    //create a model instance and add it to the list  
                    ModelDB temp = new ModelDB() // создать класс  который равен 1 строке
                    {
                        Payroll_Number = Convert.ToString(answer.GetValue(0)),
                        Forenames = Convert.ToString(answer.GetValue(1)),
                        Surname = Convert.ToString(answer.GetValue(2)),
                        Date_of_Birth = Convert.ToDateTime(answer.GetValue(3)),
                        Telephone = Convert.ToInt32(answer.GetValue(4)),
                        Mobile = Convert.ToInt32(answer.GetValue(5)),
                        Address = Convert.ToString(answer.GetValue(6)),
                        Address_2 = Convert.ToString(answer.GetValue(7)),
                        Postcode = Convert.ToString(answer.GetValue(8)),
                        EMail_Home = Convert.ToString(answer.GetValue(9)),
                        Start_Date = Convert.ToDateTime(answer.GetValue(10))
                    };

                    data.Add(temp);// добавить в
                }
                answer.Close();
            }
            //return list to index controller.
            return data;
        }

        public async Task Quit()
        {
            connectionString.Close();
            await connectionString.DisposeAsync();
        }
    }
}
