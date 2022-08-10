﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    public class Dictionary
    {
        public int id_stamps { get; set; }
        public string stamps_Sshort_name { get; set; }
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
    public class Project_name
    {
        public List<string> name_project { get; set; }
    }
    public class Project: Dictionary
    {
        public int id_project { get; set; }
        
        public string cipher { get; set; }
        public string name_project { get; set; }
        public int  id_executors { get; set; }
        public DateTime dadata_Creation { get; set; }
        public DateTime Dadata_Change { get; set; }
        //public Project(string Stamps_Short_Name, DateTime Dadata_Creation, DateTime Dadata_Change):base(Stamps_Short_Name)
        //{

        //}
        public Project() { }

    }
    public class Design_Object : Project
    {
        public int id_design_object { get; set; }
        public int id_parent { get; set; }
        public string name_object { get; set; }
        public string code { get; set; }
        public string full_code { get; set; }
        public int id_set_documentation { get; set; }
        public string composite_stamps_do { get; set; }
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
    public class Set_Documentation : Design_Object
    {
    

        public int number_set_documentation { get; set; }
        public string name_set_documentation { get; set; }
        public string composite_stamps_sd { get; set; }
        public int id_documents { get; set; }
        public Set_Documentation() { }
       

    }
    public class Documents : Set_Documentation
    {
        public int number_documents { get; set; }
        public string name_documents { get; set; }
        public string composite_stamps_doc { get; set; }

       
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
