using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    public class Dictionary
    {
        public int Id_Stamps { get; set; }
        public string Stamps_Short_Name { get; set; }
        public string Stamps_Full_Name { get; set; }

        public int Id_Type_Documents { get; set; }
        public string Type_Documents_Short_Name { get; set; }
        public string Type_Documents_Full_Name { get; set; }

        public int Id_Executor { get; set; }
        public string Executor_Short_Name { get; set; }
        public string Executor_Full_Name { get; set; }
        public Dictionary(string Stamps_Short_Name)
        {
            this.Stamps_Short_Name = Stamps_Short_Name;
        }
        public Dictionary() { }

    }
    public class Project: Dictionary
    {
        public int Id_Project { get; set; }
       
        public string Name_Project { get; set; }
        public string Executor { get; set; }
        public DateTime Dadata_Creation { get; set; }
        public DateTime Dadata_Change { get; set; }
        public Project(string Stamps_Short_Name, DateTime Dadata_Creation, DateTime Dadata_Change):base(Stamps_Short_Name)
        {

        }
        public Project() { }

    }
    public class Design_Object : Project
    {
        public int Id_Design_Object { get; set; }
        public int Id_Parent { get; set; }
        public string Name_Object { get; set; }
        public string Code { get; set; }
        public string Full_Code { get; set; }
        public int Id_Set_Documentation { get; set; }
        public string Сomposite_Stamps_Do { get; set; }
        public Design_Object(int Id_Design_Object, string Full_Code, string Stamps_Short_Name, string Сomposite_Stamps_Do, string Executor, DateTime Dadata_Creation, DateTime Dadata_Change) : base(Stamps_Short_Name, Dadata_Creation, Dadata_Change)
        {
            this.Id_Design_Object = Id_Design_Object;
            this.Full_Code = Full_Code;
            this.Stamps_Short_Name = Stamps_Short_Name;
            this.Сomposite_Stamps_Do = Сomposite_Stamps_Do;
            this.Executor = Executor;
            this.Dadata_Creation = Dadata_Creation;
            this.Dadata_Change = Dadata_Change;
        }
        public Design_Object(){}
    }
    public class Set_Documentation : Design_Object
    {
    

        public int Number_Set_Documentation { get; set; }
        public string Name_Set_Documentation { get; set; }
        public string Сomposite_Stamps_Sd { get; set; }
        public int Id_Documents { get; set; }
        public Set_Documentation() { }
       

    }
    public class Documents : Set_Documentation
    {
        public int Number_Documents { get; set; }
        public string Name_Documents { get; set; }
        public string Сomposite_Stamps_Doc { get; set; }
       
        public Documents(int Id_Documents, string Stamps_Short_Name, int Number_Documents, string Name_Documents, string Сomposite_Stamps_Doc,  DateTime Dadata_Creation, DateTime Dadata_Change)  
        {
            this.Id_Documents = Id_Documents;
            this.Number_Documents = Number_Documents;
            this.Stamps_Short_Name = Stamps_Short_Name;
            this.Сomposite_Stamps_Doc = Сomposite_Stamps_Doc;
            this.Name_Documents = Name_Documents;
            this.Dadata_Creation = Dadata_Creation;
            this.Dadata_Change = Dadata_Change;
        }
    }
  

}
