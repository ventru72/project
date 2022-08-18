using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Npgsql;
using Dapper;
using System.Data;
using System.Collections.ObjectModel;

namespace project
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       public ObservableCollection<Design_Object> name_object_l = new ObservableCollection<Design_Object>();
        public MainWindow()
        {
            
            string selectQuery = $@"select 
        id_project,
        name_project,
        id_executor,
        cipher
        from project
        ORDER BY id_project ASC ";
            InitializeComponent();
          

            Sql_Requests sql_Requests = new Sql_Requests();
           
            List<Project> date_projects = sql_Requests.Select_Project(selectQuery);
            List<Project> name_projects = new List<Project>();
           // comboBox_projects.DataContext = date_projects;
            foreach (Project project in date_projects)
            {
                name_projects.Add(new Project(project.id_project, project.name_project.ToString()));
            }


            dataGrid.ItemsSource = date_projects;
            comboBox1.ItemsSource = name_projects;
            

        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Sql_Requests sql_Requests = new Sql_Requests();
            string selectQuery = $@"select 
            id_project,
            name_project
            from project";

           
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectQuery = $@"select 
        id_project,
        name_project,
        id_executor,
        cipher
        from project
        ORDER BY id_project ASC ";
            Sql_Requests sql_Requests = new Sql_Requests();
            List<Project> date_projects = sql_Requests.Select_Project(selectQuery);

            List<string> list_combo = new List<string>();

            foreach (Project project in date_projects)
            {
                list_combo.Add(project.name_project.ToString());
            }
            
        }
        /// <summary>
        /// Создание таблицы при выборе элемента - имя обьекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Hyperlink_Click_Object_Name(object sender, RoutedEventArgs e)
        {

            string selected_object_name = (sender as Hyperlink).Tag as string;
            int id_parent = 0;
            int id_design_object = 0;
            List <string> full_code_list_items = new List<string>();
            List<string> name_object_list = new List<string>();
            List<int> id_object_list = new List<int>();
            //List<Design_Object> design_objects_list = new List<Design_Object>();
            //design_objects_list.Add(new Design_Object(name_object_l[i].id_design_object, selected_object_name));
            List<string> full_code_list_parent = new List<string>();
            List<string> full_stamps_list_parent = new List<string>();
            string first_code = string.Empty;
            string full_code = string.Empty;
            int first_id = 0;
            string full_stamps = string.Empty;
            string cipher = string.Empty;
            int count = 0;
            
            bool stop_full_code = false;

            bool stop_add_list_items = false;

            while (stop_full_code == false)
            {
                Get_Name_Object_List(id_design_object, id_object_list, first_code);
            }
            (int id_design_object, List<int> id_object_list, string first_code) Get_Name_Object_List(int id_design_object_, List<int> id_object_list_, string first_code_)
            {

                for (int i = 0; i < name_object_l.Count; i++)
                {
                    if (name_object_l[i].name_object == selected_object_name)
                    {
                        if (count == 0)
                        {
                            first_id = name_object_l[i].id_design_object;
                        }
                        count++;
                        id_design_object = name_object_l[i].id_design_object;
                        break;
                    }
                }
                for (int i = 0; i < name_object_l.Count; i++)
                {
                    if (name_object_l[i].id_parent == id_design_object)
                    {
                        id_object_list.Add(name_object_l[i].id_design_object);
                        selected_object_name = name_object_l[i].name_object;
                    }
                    else if (i == name_object_l.Count - 1 && name_object_l[i].id_parent != id_design_object)
                    {
                        stop_full_code = true;
                        id_object_list.Insert(0, first_id);
                        break;
                    }
                }
                return (id_design_object, id_object_list, first_code);
            }
            count = 0;
            stop_full_code = false;
            selected_object_name = (sender as Hyperlink).Tag as string;


            for (int i = 0; i < id_object_list.Count; i++)
            {
                id_design_object = id_object_list[i];
                full_code_list_items.Clear();
                stop_full_code = false;
                count = 0;
                while (stop_full_code == false)
                {
                    Full_Code(id_design_object, full_code_list_items, first_code);
                }
            }
            
            (int id_design_object, List<string> full_code, string first_code) Full_Code( int id_design_object_, List<string> full_code_, string first_code_)
            {
                
               for (int i = 0; i < name_object_l.Count; i++)
               {
                    if (name_object_l[i].id_design_object == id_design_object)
                    {
                        if(  count == 0)
                        {
                            first_code = name_object_l[i].code + ".";
                            full_stamps = name_object_l[i].stamps_number;
                            cipher = name_object_l[i].cipher;
                        }
                        
                        id_parent = name_object_l[i].id_parent;
                        count++;
                        break;
                    }
               }
               for (int i = 0; i < name_object_l.Count; i++)
                {
                    if (name_object_l[i].id_design_object == id_parent)
                    {
                        full_code_list_items.Add(name_object_l[i].code + ".");
                        //stop_add_list_items = true;
                    }
                    else if (i == name_object_l.Count - 1 && name_object_l[i].id_design_object != id_parent)
                    {
                        stop_full_code = true;
                        full_code_list_items.Insert(0,first_code);
                        foreach (var n in full_code_list_items)
                        {
                            full_code = full_code + n;
                        }
                        full_code_list_parent.Add(full_code);
                        full_stamps = cipher + "-" + full_code + "-" + full_stamps;
                        full_stamps_list_parent.Add(full_stamps);
                        break;
                    }
                }
                stop_add_list_items = false;

                return (id_design_object, full_code_list_items, first_code);
            }
           
           
            
            //MessageBox.Show(full_code);
            //MessageBox.Show(full_stamps);
            string selectQuery = $"SELECT  set_documentation.id_stamps,  number_set_documentation,   type_documents_full_name,  number_document,  name_document," +
                                $"  data_creation_document,  data_change_document" +
                                $" FROM design_object " +
                                $" LEFT JOIN set_documentation ON design_object.stamps_number =" +
                                $" set_documentation.stamps_number " +
                                $" LEFT JOIN documents ON documents.number_type_documents =" +
                                $" set_documentation.number_type_documents " +
                               
                                $" LEFT JOIN guide_type_documents ON documents.id_type_documents =" +
                                $" guide_type_documents.id_type_documents " +
                                $" LEFT JOIN guide_stamps ON set_documentation.id_stamps =" +
                                $" guide_stamps.id_stamps " +
                                $" WHERE name_object = '{selected_object_name}'";
            Sql_Requests sql_Requests = new Sql_Requests();

            List<Documents> date_documents = sql_Requests.Select_Set_Documentation(selectQuery);
            List<Documents> name_projects = new List<Documents>();
            // comboBox_projects.DataContext = date_projects;
            foreach (Documents project in date_documents)
            {
                //name_projects.Add(new Design_Object(project.id_project, project.name_project.ToString()));
            }


            dataGrid.ItemsSource = date_documents;
            //comboBox1.ItemsSource = name_projects;



        }
        /// <summary>
        /// Создание таблицы при выборе комплекта документации 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Hyperlink_Click_Set_Documentation(object sender, RoutedEventArgs e)
        {


            string selected_set_documentation = (sender as Hyperlink).Tag as string;

            string selectQuery = $"SELECT type_documents_full_name, number_document, name_document" +
                                $" FROM design_object " +
                                 $" LEFT JOIN set_documentation ON design_object.stamps_number =" +
                                $" set_documentation.stamps_number " +
                                 $" LEFT JOIN documents ON set_documentation.number_type_documents =" +
                                $" documents.number_type_documents " +
                                $" LEFT JOIN guide_type_documents ON documents.id_type_documents =" +
                                $" guide_type_documents.id_type_documents " +
                                $" WHERE  set_documentation.stamps_number = '{selected_set_documentation}'";
            Sql_Requests sql_Requests = new Sql_Requests();

            List<Documents> date_documents = sql_Requests.Select_Set_Documentation(selectQuery);
            //List<Documents> name_projects = new List<Documents>();
            //// comboBox_projects.DataContext = date_projects;
            //foreach (Documents project in date_projects)
            //{
            //    name_projects.Add(new Documents(project.id_project, project.name_project.ToString()));
            //}


            //dataGrid.ItemsSource = date_projects;
            dataGrid.ItemsSource = date_documents;



        }
        /// <summary>
        /// Создание дерева из обьектов проектирования и коплектов документации при выборе проекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Sql_Requests sql_Requests = new Sql_Requests();
            comboBox1.DataContext = comboBox1.SelectedItem;

            Project project = comboBox1.DataContext as Project;
            //string selectQuery = $"SELECT id_design_object, name_object, design_object.stamps_number, id_parent" +
            //                     $" FROM project " +
            //                     $" LEFT JOIN design_object ON project.id_executor = design_object.id_executor " +
                                
            //                     $" WHERE id_project = '{project.id_project}'";

            string selectQuery = $"SELECT cipher,  o.data_creation_design_object, o.data_change_design_object, " +
                                 $" o.id_design_object, o.code, o.name_object, o.stamps_number, o.id_parent, " +
                                 $" r.id_design_object AS id_design_object_parent," +
                                 $" r.code AS code_parent," +
                                 $" r.name_object AS name_object_parent," +
                                 $" r.stamps_number AS stamps_number_parent," +
                                 $" r.id_parent AS id_parent_parent" +
                                 $" FROM project" +
                                 
                                 $" JOIN guide_executors ON project.id_executor = guide_executors.id_executor " +
                                 $" LEFT JOIN design_object AS o ON project.id_project = o.id_project" +
                                 $" JOIN set_documentation ON o.stamps_number = set_documentation.stamps_number " +
                              
                                 $" LEFT JOIN design_object AS r ON o.id_design_object = r.id_parent" +
                                 $" WHERE project.id_project = '{project.id_project}'";

            //string Requests_Parent(int id_parent)
            //{
            //    string parent_selectQuery = $"SELECT id_design_object, name_object, design_object.stamps_number, id_parent" +
            //                                    $" FROM project " +
            //                                    $" LEFT JOIN design_object ON project.id_executor =" +
            //                                    $" design_object.id_executor " +
            //                                    //$" LEFT JOIN set_documentation ON design_object.stamps_number =" +
            //                                    //$" set_documentation.stamps_number " +
            //                                    $" WHERE id_project = '{project.id_project}' AND id_parent = '{id_parent}' ";
            //    return parent_selectQuery;
            //}


            
            List<Design_Object> date_projects = sql_Requests.Select_Object(selectQuery);
            //List<Design_Object> parent_design_object = new List<Design_Object>();

           
            
            foreach (Design_Object i in date_projects)
            {
                name_object_l.Add(new Design_Object(  i.executor_full_name, i.data_creation_design_object, i.data_change_design_object, i.cipher, i.id_design_object, i.code, i.name_object,  i.stamps_number, i.id_parent,
                   i.id_design_object_parent, i.code_parent, i.name_object_parent, i.stamps_number_parent, i.id_parent_parent));
                 
            }
            //for (int i = 0; i < name_object_l.Count; i++)
            //{
            //    while (name_object_l[i].id_parent > 0)
            //    {

            //        //parent_design_object = sql_Requests.Select_Object(Requests_Parent(name_object_l[i].id_parent));
            //    }
            //}
            listBox.ItemsSource = name_object_l;

        }
    }
}







//while (stop_full_code == false)
//{
//    Get_Name_Object_List(selected_object_name, name_object_list, first_code);
//}
//(string selected_object_name, List<string> name_object_list, string first_code) Get_Name_Object_List(string str_object_name, List<string> name_object_list_, string first_code_)
//{

//    for (int i = 0; i < name_object_l.Count; i++)
//    {
//        if (name_object_l[i].name_object == selected_object_name)
//        {

//            if (count == 0)
//            {
//                first_code = name_object_l[i].code + ".";
//                full_stamps = name_object_l[i].stamps_number;
//                cipher = name_object_l[i].cipher;
//            }
//            count++;
//            id_design_object = name_object_l[i].id_design_object;
//            break;
//        }
//    }
//    for (int i = 0; i < name_object_l.Count; i++)
//    {
//        if (name_object_l[i].id_parent == id_design_object)
//        {
//            name_object_list.Add(name_object_l[i].code + ".");
//            selected_object_name = name_object_l[i].name_object;
//        }
//        else if (i == name_object_l.Count - 1 && name_object_l[i].id_parent != id_design_object)
//        {
//            stop_full_code = true;
//            name_object_list.Insert(0, first_code);
//            break;
//        }
//    }
//    return (selected_object_name, name_object_list, first_code);
//}
//count = 0;
//stop_full_code = false;
//selected_object_name = (sender as Hyperlink).Tag as string;
//for (int i = 0; i < name_object_list.Count; i++)
//{
//    selected_object_name = name_object_list[i];
//    while (stop_full_code == false)
//    {
//        Full_Code(selected_object_name, full_code_list_items, first_code);
//    }
//}

//(string selected_object_name, List<string> full_code, string first_code) Full_Code(string str_object_name, List<string> full_code_, string first_code_)
//{

//    for (int i = 0; i < name_object_l.Count; i++)
//    {
//        if (name_object_l[i].name_object == selected_object_name)
//        {
//            if (count == 0)
//            {
//                first_code = name_object_l[i].code + ".";
//                full_stamps = name_object_l[i].stamps_number;
//                cipher = name_object_l[i].cipher;
//            }

//            id_parent = name_object_l[i].id_parent;
//            count++;
//            break;
//        }
//    }
//    for (int i = 0; i < name_object_l.Count; i++)
//    {
//        if (name_object_l[i].id_design_object == id_parent)
//        {
//            full_code_list_items.Add(name_object_l[i].code + ".");
//            selected_object_name = name_object_l[i].name_object;


//        }
//        else if (i == name_object_l.Count - 1 && name_object_l[i].id_design_object != id_parent)
//        {
//            stop_full_code = true;
//            full_code_list_items.Insert(0, first_code);
//            foreach (var n in full_code_list_items)
//            {
//                full_code = full_code + n;
//            }
//            full_code_list_parent.Add(full_code);
//            full_stamps = cipher + "-" + full_code + "-" + full_stamps;
//            full_stamps_list_parent.Add(full_stamps);
//            break;
//        }
//    }

//    return (selected_object_name, full_code_list_items, first_code);
//}
