using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

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
        //public string type_documents_short_name { get; set; }
        public string type_documents_full_Name { get; set; }

        //public int id_executor { get; set; }
        //public string executor_short_name { get; set; }
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
         
        public DateTime data_creation_design_object { get; set; }
        public DateTime data_change_design_object { get; set; }
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
        public string stamps_full_name { get; set; }
        public string code { get; set; }
        public string full_code { get; set; }
        public string stamps_number { get; set; }
        public int id_parent_parent { get; set; }
        public string code_parent { get; set; }
        public string name_object_parent { get; set; }
        public int id_design_object_parent { get; set; }
        public string stamps_number_parent { get; set; }
        public ObservableCollection<Design_Object> Design_Object_List { get; set; }
        public int number_set_documentation { get; set; }
        public DateTime data_creation_set_docment { get; set; }
        public DateTime data_change_set_docment { get; set; }


        public int id_set_documentation { get; set; }
        public Design_Object( string executor_full_name, DateTime data_creation_design_object, DateTime data_change_design_object,
            string cipher,
            int id_design_object,  string code, string name_object, string stamps_number, int id_parent,
            int id_design_object_parent, string code_parent, string name_object_parent, string stamps_number_parent, int id_parent_parent)
        {
             
            this.executor_full_name = executor_full_name;
            this.data_creation_design_object = data_creation_design_object;
            this.data_change_design_object= data_change_design_object;
            this.cipher = cipher;


            this.id_design_object = id_design_object;
            this.name_object = name_object;
            this.stamps_number = stamps_number;
            this.id_parent = id_parent;
            this.code = code;

            this.id_design_object_parent = id_design_object_parent;
            this.name_object_parent = name_object_parent;
            this.stamps_number_parent = stamps_number_parent;
            this.id_parent_parent = id_parent_parent;
            this.code_parent = code_parent;
        }
        public Design_Object(int id_parent, string cipher,   DateTime data_creation_design_object, DateTime data_change_design_object,
          
          int id_design_object, string code, string name_object,  string stamps_number,
          int id_design_object_parent, string code_parent, string name_object_parent,   int id_parent_parent)
        {

            //this.executor_full_name = executor_full_name;
            this.data_creation_design_object = data_creation_design_object;
            this.data_change_design_object = data_change_design_object;
            this.cipher = cipher;

            this.id_design_object = id_design_object;
            this.name_object = name_object;
            this.stamps_number = stamps_number;
            this.id_parent = id_parent;
            this.code = code;

            this.id_design_object_parent = id_design_object_parent;
            this.name_object_parent = name_object_parent;
            //this.stamps_number_parent = stamps_number_parent;
            this.id_parent_parent = id_parent_parent;
            this.code_parent = code_parent;
        }
        public Design_Object(int id_design_object, int id_parent, string stamps_number)
        {
            this.stamps_number = stamps_number;
            this.id_parent = id_parent;
            this.id_design_object = id_design_object;


        }
        public Design_Object(ObservableCollection<Design_Object> Design_Object_List)
        {
            this.Design_Object_List = Design_Object_List;


        }
        public Design_Object(int id_design_object, string name_object)
        {
            this.id_design_object = id_design_object;
            this.name_object = name_object;
          
        }
        public Design_Object(int id_design_object, string name_object, string stamps_number, int id_parent)  
        {
            this.id_design_object = id_design_object;
            this.name_object = name_object;
            this.stamps_number = stamps_number;   
            this.id_parent = id_parent;
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
    public class Set_Documentation  
    {
        
        public string full_code { get; set; }
        public string stamps_full_name { get; set; }
        public string stamps_short_name { get; set; }
        public int number_set_documentation { get; set; }
        public string name_set_documentation { get; set; }
        public DateTime data_creation_set_docment { get; set; }
        public DateTime data_change_set_docment { get; set; }
        public string full_code_design_object { get; set; }
        public string full_cipher_project { get; set; }
        public string number_type_documents { get; set; }
        public int id_documents { get; set; }
        public int id_design_objectstring { get; set; }
        public string executor_full_name { get; set; }
        public string cipher { get; set; }
        public Set_Documentation() { }
        public Set_Documentation(string full_code_design_object, string cipher,  string full_cipher_project)
        {
            this.cipher = cipher;
            this.full_code_design_object = full_code_design_object;
            this.full_cipher_project = full_cipher_project;
           
        }
        public Set_Documentation(int id_design_objectstring, string cipher, string full_code_design_object, string stamps_short_name, int number_set_documentation, string stamps_full_name,  
             string executor_full_name,  DateTime data_creation_set_docment, DateTime data_change_set_docment)
        {
            this.id_design_objectstring = id_design_objectstring;
            this.stamps_short_name = stamps_short_name;
            this.cipher = cipher;
            this.full_code_design_object = full_code_design_object;
            this.full_cipher_project = full_cipher_project;
            this.stamps_full_name = stamps_full_name;
            this.number_set_documentation = number_set_documentation;
            this.data_creation_set_docment = data_creation_set_docment;
            this.data_change_set_docment = data_change_set_docment;
            this.executor_full_name = executor_full_name;

        }



    }
    public class Documents  
    {
        public int number_document { get; set; }
        public string name_document { get; set; }
        public int number_set_documentation { get; set; }
        string type_documents_full_name { get; set; }
        public DateTime data_creation_document { get; set; }
        public DateTime data_change_document { get; set; }
        public string full_cipher_documents { get; set; }
        public int id_stamps { get; set; }
        public string stamps_full_name { get; set; }


        public Documents(string type_documents_full_name, int number_document, string name_document)
        {
            this.type_documents_full_name = type_documents_full_name;
            this.number_document = number_document;
            this.name_document = name_document;
        }
        public Documents(int id_stamps, int number_set_documentation,  string type_documents_full_name, int number_document, string name_document,
            DateTime data_creation_document, DateTime data_change_document, string full_cipher_documents)
        {
            this.number_set_documentation = number_set_documentation;
            this.id_stamps = id_stamps;
            this.full_cipher_documents = full_cipher_documents;
            this.type_documents_full_name = type_documents_full_name;
            this.number_document = number_document;
            this.name_document = name_document;
            this.data_creation_document = data_creation_document;
            this.data_change_document = data_change_document;
        }
        public Documents(string stamps_full_name, int number_set_documentation, string type_documents_full_name, int number_document, string name_document,
           DateTime data_creation_document, DateTime data_change_document )
        {
            this.number_set_documentation = number_set_documentation;
            this.stamps_full_name = stamps_full_name;
            
            this.type_documents_full_name = type_documents_full_name;
            this.number_document = number_document;
            this.name_document = name_document;
            this.data_creation_document = data_creation_document;
            this.data_change_document = data_change_document;
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
