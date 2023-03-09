 
The project implements csv loading and saving data to the database, as well as output to the page 

DB_Import Class opens connection when creating an instance. It needs to send connection string to it.
class has Add_to_DB method which adds data to [Employees] table 
and Load_From_DB method which reads data from table in <List<ModelDB> >

ModelDB - > model of table [Employees] 

CSV_Impoert class receives TextFieldParser file as input and converts it to < List<ModelDB> >. then calls save method in database then redirects to index
