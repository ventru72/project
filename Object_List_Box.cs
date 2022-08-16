using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    interface IRequests
    {

    }
    public class Dictionary: IRequests
    {
        public int id_stamps { get; set; }
        public string stamps_short_name { get; set; }
        public string stamps_full_name { get; set; }

        public int id_type_documents { get; set; }
        public string type_documents_short_name { get; set; }
        public string type_documents_full_Name { get; set; }

        public int id_executor { get; set; }
        public string executor_short_name { get; set; }
        public string executor_full_name { get; set; }
        //public Dictionary(string Stamps_Short_Name)
        //{
        //    this.Stamps_Short_Name = Stamps_Short_Name;
        //}
        public Dictionary() { }

    }
    public class Project_name : IRequests
    {
        public List<string> name_project { get; set; }
    }
    public class Project: Dictionary,  IRequests
    {
        public int id_project { get; set; }
        
        public string cipher { get; set; }
        public string name_project { get; set; }
         
        public DateTime dadata_Creation { get; set; }
        public DateTime Dadata_Change { get; set; }
        //public Project(string Stamps_Short_Name, DateTime Dadata_Creation, DateTime Dadata_Change):base(Stamps_Short_Name)
        //{

        //}
        public Project() { }
        public Project(int id_project, string name_project) 
        { 
            this.id_project = id_project;
            this.name_project = name_project;
        }

    }
    
    public class Design_Object : Project, IRequests
    {
        public int id_design_object { get; set; }
        public int id_parent { get; set; }
        public string name_object { get; set; }
        public string code { get; set; }
        public string full_code { get; set; }
        public string stamps_number { get; set; }
        
        public int id_set_documentation { get; set; }
       
        public Design_Object(int id_design_object, string name_object, string stamps_number)
        {
            this.id_design_object = id_design_object;
            this.name_object = name_object;
            this.stamps_number = stamps_number;   
        }
        //public Design_Object(int Id_Design_Object, string Full_Code, string Stamps_Short_Name, string Сomposite_Stamps_Do, string Executor, DateTime Dadata_Creation, DateTime Dadata_Change) : base(Stamps_Short_Name, Dadata_Creation, Dadata_Change)
        //{
        //    this.Id_Design_Object = Id_Design_Object;
        //    this.Full_Code = Full_Code;
        //    this.Stamps_Short_Name = Stamps_Short_Name;
        //    this.Сomposite_Stamps_Do = Сomposite_Stamps_Do;
        //    this.executor = Executor;
        //    this.Dadata_Creation = Dadata_Creation;
        //    this.Dadata_Change = Dadata_Change;
        //}
        public Design_Object(){}
    }
    public class Set_Documentation : Design_Object, IRequests
    {
    

        public int number_set_documentation { get; set; }
        public string name_set_documentation { get; set; }
       
        public string number_type_documents { get; set; }
        public int id_documents { get; set; }
        public Set_Documentation() { }
       

    }
    public class Documents : Set_Documentation, IRequests
    {
        public int number_document { get; set; }
        public string name_document { get; set; }
        string type_documents_full_name { get; set; }

        public Documents(string type_documents_full_name, int number_document, string name_document)
        {
            this.type_documents_full_name = type_documents_full_name;
            this.number_document = number_document;
            this.name_document = name_document;
        }

        //public Documents(int Id_Documents, string Stamps_Short_Name, int Number_Documents, string Name_Documents, string Сomposite_Stamps_Doc,  DateTime Dadata_Creation, DateTime Dadata_Change)  
        //{
        //    this.Id_Documents = Id_Documents;
        //    this.Number_Documents = Number_Documents;
        //    this.Stamps_Short_Name = Stamps_Short_Name;
        //    this.Сomposite_Stamps_Doc = Сomposite_Stamps_Doc;
        //    this.Name_Documents = Name_Documents;
        //    this.Dadata_Creation = Dadata_Creation;
        //    this.Dadata_Change = Dadata_Change;
        //}
    }
  

}
