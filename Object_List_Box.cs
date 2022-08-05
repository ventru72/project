using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    internal class Dictionary
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

    }
    internal class Project: Dictionary
    {
        public int Id_Project { get; set; }
       
        public string Name_Project { get; set; }
        public string Executor { get; set; }
    }
    internal class Design_Object: Project
    {
        public int Id_Design_Object { get; set; }
        public int Id_Parent { get; set; }
        public string Name_Object { get; set; }
        public string Code { get; set; }
        public int Id_Set_Documentation { get; set; }

    }
    internal class Set_Documentation : Design_Object
    {
        public int Number_Set_Documentation { get; set; }
        public int Id_Documents { get; set; }
    }
    internal class Documents : Set_Documentation
    {
        public int Number_Documen { get; set; }
        public string Name_Documen { get; set; }
    }


    }
