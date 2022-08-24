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
    public class Guide_Executors  
    {
        public int id_executor { get; set; }
        public string executor_short_name { get; set; }
        public string executor_full_name { get; set; }
       
        public Guide_Executors() { }
        public Guide_Executors(int id_executor, string executor_short_name )
        { 
            this.id_executor = id_executor;
            this.executor_short_name= executor_short_name;
        }
        public Guide_Executors(string executor_short_name, string executor_full_name)
        {
            this.executor_short_name = executor_short_name;
            this.executor_full_name = executor_full_name;
        }

    }
    public class Guide_Stamps
    {
        public int id_stamps { get; set; }
        public string stamps_short_name { get; set; }
        public string stamps_full_name { get; set; }

       
        public Guide_Stamps() { }
        public Guide_Stamps(string stamps_short_name, string stamps_full_name)
        {
            this.stamps_short_name = stamps_short_name;
            this.stamps_full_name = stamps_full_name;
        }
        public Guide_Stamps( int id_stamps, string stamps_short_name )
        {
           
            this.id_stamps = id_stamps;
            this.stamps_short_name = stamps_short_name;
           
        }
        public Guide_Stamps(int id_stamps )
        {

            this.id_stamps = id_stamps;
             

        }
    }
    public class Guide_Type_Documents
    {
        public int id_type_documents { get; set; }
        public string type_documents_short_name { get; set; }
        public string type_documents_full_Name { get; set; }


        public Guide_Type_Documents() { }
        public Guide_Type_Documents(string type_documents_short_name, string type_documents_full_Name)
        {
            this.type_documents_short_name = type_documents_short_name;
            this.type_documents_full_Name = type_documents_full_Name;
        }
        public Guide_Type_Documents( int id_type_documents, string type_documents_short_name)
        {
           
            this.type_documents_short_name = type_documents_short_name;
            this.id_type_documents = id_type_documents;
        }
    }
    public class Project : Guide_Executors
    {
        public int id_project { get; set; }
        
        public string cipher { get; set; }
        public string name_project { get; set; }
         
        //public DateTime data_creation_design_object { get; set; }
        //public DateTime data_change_design_object { get; set; }
        //public Project(string Stamps_Short_Name, DateTime Dadata_Creation, DateTime Dadata_Change):base(Stamps_Short_Name)
        //{

        //}
        public Project() { }
        public Project(int id_project, string name_project)
        {
            this.id_project = id_project;
           
            this.name_project = name_project;
        }
        public Project(int id_project, string name_project, string executor_short_name) 
        {
            this.id_project = id_project;
            this.executor_short_name = executor_short_name;
            this.name_project = name_project;
        }
        public Project (string name_project)
        {
           
            this.name_project = name_project;
        }
        public Project(string cipher, string name_project, int id_executor)
        {
            this.cipher = cipher;
            this.id_executor = id_executor;
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
        public int number_set_documentation { get; set; }
        
        public string full_cipher_documents { get; set; }
        public DateTime data_creation_design_object { get; set; }
        public DateTime data_change_design_object { get; set; }
        public DateTime data_change_set_docment { get; set; }
        public DateTime data_creation_set_docment { get; set; }
        


        public int id_set_documentation { get; set; }
        public string stamps_short_name { get; set; }
        public  Design_Object(string code, string name_object, int id_parent, DateTime data_creation_design_object,
                DateTime data_change_design_object, int id_executor,int id_project, string full_code)
        {
            this.full_code = full_code;
            this.id_project = id_project;
            this.id_executor = id_executor;
            this.data_creation_design_object = data_creation_design_object;
            this.data_change_design_object = data_change_design_object;
            this.name_object = name_object;
            this.id_parent = id_parent;
            this.code = code;

        }
        public Design_Object(string code, string name_object,  DateTime data_creation_design_object,
                DateTime data_change_design_object, int id_executor, int id_project, string full_code)
        {
            this.full_code = full_code;
            this.id_project = id_project;
            this.id_executor = id_executor;
            this.data_creation_design_object = data_creation_design_object;
            this.data_change_design_object = data_change_design_object;
            this.name_object = name_object;
         
            this.code = code;

        }
        public Design_Object(string name_object, int id_parent, int id_design_object)
        {
            this.name_object = name_object;
            this.id_parent = id_parent;
            this.id_design_object = id_design_object;

        }
        public Design_Object( string name_object, int id_parent, string code)
        {
           this.name_object = name_object;
           this.id_parent = id_parent;
           this.code = code;
        }
        public Design_Object(string full_code, string cipher)
        {
            this.cipher = cipher;
            this.full_code = full_code;

        }
        public Design_Object(string full_code)
        {
            this.full_code = full_code;

        }
        public Design_Object(int id_parent)
        {
            this.id_parent = id_parent;
        }
        public Design_Object(int id_set_documentation, int id_project, string executor_full_name, 
            int id_design_object, string name_object, int id_parent, int id_design_object_parent,  string name_object_parent, 
            int id_parent_parent)
        {
            this.id_set_documentation = id_set_documentation;
            this.id_project = id_project;
            this.executor_full_name = executor_full_name;
            this.data_creation_set_docment = data_creation_set_docment;
            this.data_change_set_docment = data_change_set_docment;
            this.cipher = cipher;
            this.full_cipher_documents = full_cipher_documents;
           
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
            this.full_cipher_documents = full_cipher_documents;
        }
        public Design_Object(int id_set_documentation, int id_project, string executor_full_name, DateTime data_creation_set_docment,
           DateTime data_change_set_docment, string cipher,
           int id_design_object, string code, string name_object, string stamps_number, int id_parent,
           int id_design_object_parent, string code_parent, string name_object_parent, string stamps_number_parent, int id_parent_parent, string full_cipher_documents)
        {
            this.id_set_documentation = id_set_documentation;
            this.id_project = id_project;
            this.executor_full_name = executor_full_name;
            this.data_creation_set_docment = data_creation_set_docment;
            this.data_change_set_docment = data_change_set_docment;
            this.cipher = cipher;
            this.full_cipher_documents = full_cipher_documents;

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
            this.full_cipher_documents = full_cipher_documents;
        }
        public Design_Object(string full_code, string stamps_full_name, string cipher, string name_object, string stamps_short_name, int number_set_documentation,
                  DateTime data_creation_set_docment, DateTime data_change_set_docment, string executor_full_name)
          {
            this.number_set_documentation = number_set_documentation;
            this.id_project = id_project;
            this.executor_full_name = executor_full_name;
            this.data_creation_set_docment = data_creation_set_docment;
            this.data_change_set_docment = data_change_set_docment;
            this.cipher = cipher;
            this.full_cipher_documents = full_cipher_documents;

            this.id_design_object = id_design_object;
            this.name_object = name_object;
            this.stamps_number = stamps_number;
            this.id_parent = id_parent;
            this.stamps_full_name = stamps_full_name;

            this.full_code = full_code;
            this.name_object_parent = name_object_parent;
            this.stamps_short_name = stamps_short_name;
            this.id_parent_parent = id_parent_parent;
            this.code_parent = code_parent;
            this.full_cipher_documents = full_cipher_documents;
        }
       
        //output_set_documentation.Add(new Set_Documentation(get_data_sql[j].id_design_object, get_data_sql[j].cipher, date_projects[i].full_code,
        //                    get_data_sql[j].stamps_short_name, get_data_sql[j].number_set_documentation, get_data_sql[j].stamps_full_name,
        //                    get_data_sql[j].executor_full_name, get_data_sql[j].data_creation_set_docment, get_data_sql[j].data_change_set_docment));
        //    public Design_Object(int id_design_object, int cipher, string full_code, string stamps_short_name,
        //    string number_set_documentation, string number_set_documentation, string stamps_full_name, string executor_full_name,
        //    string data_creation_set_docment,
        //    int id_parent,
        //   int id_design_object_parent, string code_parent, string name_object_parent, string stamps_number_parent, int id_parent_parent)
        //{
        //    this.id_set_documentation = id_set_documentation;
        //    this.id_project = id_project;
        //    this.executor_full_name = executor_full_name;
        //    this.data_creation_set_docment = data_creation_set_docment;
        //    this.data_change_set_docment = data_change_set_docment;
        //    this.cipher = cipher;


        //    this.id_design_object = id_design_object;
        //    this.name_object = name_object;
        //    this.stamps_number = stamps_number;
        //    this.id_parent = id_parent;
        //    this.code = code;

        //    this.id_design_object_parent = id_design_object_parent;
        //    this.name_object_parent = name_object_parent;
        //    this.stamps_number_parent = stamps_number_parent;
        //    this.id_parent_parent = id_parent_parent;
        //    this.code_parent = code_parent;
        //}///
        public Design_Object( int id_design_object, int id_project, string executor_full_name, DateTime data_creation_set_docment,
            DateTime data_change_set_docment, string cipher,  string code, string name_object, string stamps_number, int id_parent,
           int id_design_object_parent, string code_parent, string name_object_parent, string stamps_number_parent, int id_parent_parent)
        {
            this.id_set_documentation = id_set_documentation;
            this.id_project = id_project;
            this.executor_full_name = executor_full_name;
            this.data_creation_set_docment = data_creation_set_docment;
            this.data_change_set_docment = data_change_set_docment;
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
        }///
        public Design_Object(string cipher,  string full_code, string stamps_short_name,
            int number_set_documentation, string stamps_full_name, string executor_full_name,
            DateTime data_creation_set_docment, DateTime data_change_set_docment )
        {

            //this.executor_full_name = executor_full_name;
            this.full_code = full_code;
            this.stamps_short_name = stamps_short_name;
            this.data_creation_set_docment = data_creation_set_docment;
            this.cipher = cipher;

            this.number_set_documentation = number_set_documentation;
            this.stamps_full_name = stamps_full_name;
            this.executor_full_name = executor_full_name;
            this.data_creation_set_docment = data_creation_set_docment;
            this.data_change_set_docment = data_change_set_docment;

           
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
        public Design_Object(int id_design_object, string name_object, int id_set_documentation)
        {
            this.id_design_object = id_design_object;
            this.name_object = name_object;
            this.id_set_documentation = id_set_documentation;

        }
       
        public Design_Object(int id_design_object, string name_object)
        {
            this.id_design_object = id_design_object;
            this.name_object = name_object;
          
        }
        public Design_Object(int id_design_object, string stamps_full_name,  string cipher, string name_object,
            string stamps_short_name)  
        {
            this.id_design_object = id_design_object;
            this.stamps_full_name = stamps_full_name;
            
            this.cipher = cipher;   
            this.name_object = name_object;
            this.stamps_short_name = stamps_short_name;
        }
        public Design_Object(  string full_code, string stamps_number, int id_parent)
        {
            this.id_design_object = id_design_object;
            this.name_object = name_object;
            this.stamps_number = stamps_number;
            this.id_parent = id_parent;
        }
        
        public Design_Object(){}
    }
    public class Set_Documentation  
    {
        
        public string full_code { get; set; }
        
        public string stamps_short_name { get; set; }
        public int number_set_documentation { get; set; }
        public string name_set_documentation { get; set; }
        public DateTime data_creation_set_docment { get; set; }
        public DateTime data_change_set_docment { get; set; }
        public string stamps_full_name { get; set; }
        public string full_cipher_project { get; set; }
        public int number_type_documents { get; set; }
        public int id_documents { get; set; }
        public int id_design_object  { get; set; }
        public string executor_full_name { get; set; }
        public string stamps_name { get; set; }
        public string cipher { get; set; }
        public int id_stamps { get; set; }
        public int id_set_documentation { get; set; }
        public Set_Documentation() { }

        public Set_Documentation(string stamps_name, int id_set_documentation)
        {
            this.stamps_name = stamps_name;
            this.id_set_documentation = id_set_documentation;
        }
        public Set_Documentation(int number_set_documentation, DateTime data_creation_set_docment, DateTime data_change_set_docment,
            int id_stamps, int id_design_object,  string stamps_full_name)
        {
            this.number_set_documentation = number_set_documentation;
            this.id_stamps = id_stamps;
            this.id_design_object = id_design_object;
            this.stamps_full_name = stamps_full_name;
            this.data_creation_set_docment = data_creation_set_docment;
            this.data_change_set_docment = data_change_set_docment;
        }
       
    }
    public class Documents  
    {
        public int number_document { get; set; }
        public string name_document { get; set; }
        public int id_type_documents { get; set; }
        public int id_set_documentation { get; set; }
        public string type_documents_short_name { get; set; }
        public DateTime data_creation_document { get; set; }
        public DateTime data_change_document { get; set; }
        public string full_cipher_documents { get; set; }
        public int id_stamps { get; set; }
        
        public Documents(string type_documents_short_name, int number_document, string name_document, string full_cipher_documents,
          DateTime data_creation_document, DateTime data_change_document)
        {
            this.type_documents_short_name = type_documents_short_name;
            this.number_document = number_document;
            this.name_document = name_document;
            this.data_creation_document = data_creation_document;
            this.data_change_document = data_change_document;
            this.full_cipher_documents = full_cipher_documents;
        }
        public Documents(string type_documents_short_name, int number_document, string name_document)
        {
            this.type_documents_short_name = type_documents_short_name;
            this.number_document = number_document;
            this.name_document = name_document;
        }
        public Documents(int id_stamps,  string type_documents_short_name, int number_document, string name_document,
            DateTime data_creation_document, DateTime data_change_document, string full_cipher_documents)
        {
            
            this.id_stamps = id_stamps;
            this.full_cipher_documents = full_cipher_documents;
            this.type_documents_short_name = type_documents_short_name;
            this.number_document = number_document;
            this.name_document = name_document;
            this.data_creation_document = data_creation_document;
            this.data_change_document = data_change_document;
        }
        public Documents( string type_documents_short_name, int number_document, string name_document,
           DateTime data_creation_document, DateTime data_change_document )
        {
            this.type_documents_short_name = type_documents_short_name;
            this.number_document = number_document;
            this.name_document = name_document;
            this.data_creation_document = data_creation_document;
            this.data_change_document = data_change_document;
        }
        public Documents(int number_document, int id_type_documents, string name_document,
          DateTime data_creation_document, DateTime data_change_document, int id_set_documentation,
          string full_cipher_documents)
        {
            this.number_document = number_document;
            this.id_type_documents = id_type_documents;
            this.id_set_documentation = id_set_documentation;
            this.name_document = name_document;
            this.data_creation_document = data_creation_document;
            this.data_change_document = data_change_document;
            this.full_cipher_documents = full_cipher_documents;
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
  
    public class Combo_Box_Output_Date
    {
        
        public int Id_Project { get; set; }
        public int Id_Parent_Object { get; set; }
        public int Id_Design_Object { get; set; }
        public int Id_Stamps { get; set; }
        public int Id_Type_Documents { get; set; }
        public int Id_Set_Documentation { get; set; }
        public int Id_Executor { get; set; }
        public string Chois_Stamps_CB { get; set; }
        public int Id_Update_Project { get; set; }
        public string Stamps_Name { get; set; }
        public Combo_Box_Output_Date(int number_Set_Doc_CB, string chois_Stamps_CB)
        {
            Id_Project = number_Set_Doc_CB;
            Chois_Stamps_CB = chois_Stamps_CB;
        }
        public Combo_Box_Output_Date(int Id_Project)
        {
            this.Id_Project = Id_Project;
            
        }
        public Combo_Box_Output_Date( )
        {
            

        }
        public Combo_Box_Output_Date(string chois_Stamps_CB)
        {
            Chois_Stamps_CB = chois_Stamps_CB;
        }
    }

}
