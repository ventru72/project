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
    /// Логика взаимодействия для MainWindow.xamld
    /// </summary>
    public partial class MainWindow : Window
    {
       public ObservableCollection<Design_Object> name_object_l = new ObservableCollection<Design_Object>();
       public List<Design_Object> date_projects = new List<Design_Object>();
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

            //dataGrid.ItemsSource = date_projects;
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
            Hyperlink link = (Hyperlink)sender;
            string tag = (string)link.Tag;
            Design_Object design_object_click = (Design_Object)link.DataContext;
            int id_parent = 0;
            int id_design_object = 0;
            List <string> full_code_list_items = new List<string>();
            List<List<string>> full_code_list_list_items = new List<List<string>>();
            List<int> id_object_list = new List<int>();
            List<List<int>> id_object_list_list = new List<List<int>>();
            List<Design_Object> stamps_number_list = new List<Design_Object>();
            List<List<Design_Object>> stamps_number_list_list = new List<List<Design_Object>>();
            List<Set_Documentation> output_set_documentation = new List<Set_Documentation>();
            string first_code = string.Empty;
            string full_code = string.Empty;
            string condition = string.Empty;
            int first_id = 0;
            string full_stamps = string.Empty;
            string cipher = string.Empty;
            int count = 0;
            int distinct_id_design_object=0;
            bool stop_full_code = false;
            List<int> id_parent_list = new List<int>();
            Sql_Requests sql_Requests = new Sql_Requests();
            string selectQuery = string.Empty;
            List<Design_Object> get_data_sql = new List<Design_Object>();


        void Find_Parent_Object(List<Design_Object> date_projects)
            {

            
                for (int i = 0; i < date_projects.Count; i++)
                {
                    if (i == 0)
                    {
                        selectQuery = $"SELECT DISTINCT   full_code, set_documentation.stamps_full_name,  cipher, " +
                                $" name_object, stamps_short_name,  " +
                                $" stamps_short_name || number_set_documentation AS stamps_number, data_creation_set_docment, data_change_set_docment, " +
                                $" executor_full_name" +
                                $" FROM project" +
                                $" LEFT JOIN guide_executors ON project.id_executor = guide_executors.id_executor " +
                                $" LEFT JOIN design_object  ON project.id_project =design_object.id_project" +
                                $" LEFT JOIN set_documentation ON design_object.id_design_object = set_documentation.id_design_object" +
                                $" LEFT JOIN guide_stamps ON set_documentation.id_stamps = guide_stamps.id_stamps  " +
                                $" LEFT JOIN documents ON set_documentation.id_set_documentation = documents.id_set_documentation  " +
                                $" WHERE design_object.id_design_object = '{date_projects[i].id_design_object_parent}'" +
                                $" ORDER BY  set_documentation.stamps_full_name ";

                            get_data_sql = sql_Requests.Select_Object(selectQuery);
                            distinct_id_design_object = date_projects[i].id_design_object;
                            for (int j = 0; j < date_projects.Count; j++)
                            {

                            }
                            output_set_documentation.Add(new Set_Documentation(get_data_sql[0].id_design_object, get_data_sql[0].cipher, date_projects[i].full_code,
                            get_data_sql[0].stamps_short_name, get_data_sql[0].number_set_documentation, get_data_sql[0].stamps_full_name,
                            get_data_sql[0].executor_full_name, get_data_sql[0].data_creation_set_docment, get_data_sql[0].data_change_set_docment));
                            get_data_sql.Clear();
                            stamps_number_list.Add(new Design_Object(date_projects[i].id_design_object, date_projects[i].id_parent, date_projects[i].stamps_number));
                      
                            if (id_parent_list.IndexOf(date_projects[i].id_design_object_parent) == -1 &&
                            id_parent_list.IndexOf(distinct_id_design_object) == -1)
                            {
                              id_parent_list.Add(date_projects[i].id_design_object_parent);
                            }
                    }

                    else if (i != date_projects.Count - 1 && date_projects[i].id_design_object == date_projects[i - 1].id_design_object)
                    {
                        selectQuery = $"SELECT DISTINCT   full_code, set_documentation.stamps_full_name,  cipher, " +
                               $" name_object, stamps_short_name,  " +
                               $" stamps_short_name || number_set_documentation AS stamps_number, data_creation_set_docment, data_change_set_docment, " +
                               $" executor_full_name" +
                               $" FROM project" +
                               $" LEFT JOIN guide_executors ON project.id_executor = guide_executors.id_executor " +
                               $" LEFT JOIN design_object  ON project.id_project =design_object.id_project" +
                               $" LEFT JOIN set_documentation ON design_object.id_design_object = set_documentation.id_design_object" +
                               $" LEFT JOIN guide_stamps ON set_documentation.id_stamps = guide_stamps.id_stamps  " +
                               $" LEFT JOIN documents ON set_documentation.id_set_documentation = documents.id_set_documentation  " +
                               $" WHERE design_object.id_design_object = '{date_projects[i].id_design_object_parent}'" +
                               $" ORDER BY  set_documentation.stamps_full_name ";

                        get_data_sql = sql_Requests.Select_Object(selectQuery);
                        distinct_id_design_object = date_projects[i].id_design_object;
                        output_set_documentation.Add(new Set_Documentation(get_data_sql[0].id_design_object, get_data_sql[0].cipher, date_projects[i].full_code,
                        get_data_sql[0].stamps_short_name, get_data_sql[0].number_set_documentation, get_data_sql[0].stamps_full_name,
                        get_data_sql[0].executor_full_name, get_data_sql[0].data_creation_set_docment, get_data_sql[0].data_change_set_docment));
                        get_data_sql.Clear(); stamps_number_list.Add(new Design_Object(date_projects[i].id_design_object, date_projects[i].id_parent, date_projects[i].stamps_number));
                    }

                    else if (i != date_projects.Count - 1 && date_projects[i].id_design_object != date_projects[i - 1].id_design_object &&
                       date_projects[i].id_design_object != date_projects[i + 1].id_design_object)

                    {
                        if (id_parent_list.IndexOf(date_projects[i].id_design_object_parent) == -1 &&
                               id_parent_list.IndexOf(distinct_id_design_object) == -1)
                        {
                        id_parent_list.Add(date_projects[i].id_design_object_parent);
                    }
                        selectQuery = $"SELECT DISTINCT   full_code, set_documentation.stamps_full_name,  cipher, " +
                              $" name_object, stamps_short_name,  " +
                              $" stamps_short_name || number_set_documentation AS stamps_number, data_creation_set_docment, data_change_set_docment, " +
                              $" executor_full_name" +
                              $" FROM project" +
                              $" LEFT JOIN guide_executors ON project.id_executor = guide_executors.id_executor " +
                              $" LEFT JOIN design_object  ON project.id_project =design_object.id_project" +
                              $" LEFT JOIN set_documentation ON design_object.id_design_object = set_documentation.id_design_object" +
                              $" LEFT JOIN guide_stamps ON set_documentation.id_stamps = guide_stamps.id_stamps  " +
                              $" LEFT JOIN documents ON set_documentation.id_set_documentation = documents.id_set_documentation  " +
                              $" WHERE design_object.id_design_object = '{date_projects[i].id_design_object_parent}'" +
                              $" ORDER BY  set_documentation.stamps_full_name ";

                        get_data_sql = sql_Requests.Select_Object(selectQuery);
                        distinct_id_design_object = date_projects[i].id_design_object;
                        output_set_documentation.Add(new Set_Documentation(get_data_sql[0].id_design_object, get_data_sql[0].cipher, date_projects[i].full_code,
                        get_data_sql[0].stamps_short_name, get_data_sql[0].number_set_documentation, get_data_sql[0].stamps_full_name,
                        get_data_sql[0].executor_full_name, get_data_sql[0].data_creation_set_docment, get_data_sql[0].data_change_set_docment));
                        get_data_sql.Clear(); stamps_number_list.Add(new Design_Object(date_projects[i].id_design_object, date_projects[i].id_parent, date_projects[i].stamps_number));

                    }
                    else if (i != date_projects.Count - 1 && date_projects[i].id_design_object != date_projects[i - 1].id_design_object &&
                       date_projects[i].id_design_object == date_projects[i + 1].id_design_object)

                    {
                        if (id_parent_list.IndexOf(date_projects[i].id_design_object_parent) == -1 &&
                               id_parent_list.IndexOf(distinct_id_design_object) == -1)
                        {
                        id_parent_list.Add(date_projects[i].id_design_object_parent);
                    }
                        selectQuery = $"SELECT DISTINCT   full_code, set_documentation.stamps_full_name,  cipher, " +
                               $" name_object, stamps_short_name,  " +
                               $" stamps_short_name || number_set_documentation AS stamps_number, data_creation_set_docment, data_change_set_docment, " +
                               $" executor_full_name" +
                               $" FROM project" +
                               $" LEFT JOIN guide_executors ON project.id_executor = guide_executors.id_executor " +
                               $" LEFT JOIN design_object  ON project.id_project =design_object.id_project" +
                               $" LEFT JOIN set_documentation ON design_object.id_design_object = set_documentation.id_design_object" +
                               $" LEFT JOIN guide_stamps ON set_documentation.id_stamps = guide_stamps.id_stamps  " +
                               $" LEFT JOIN documents ON set_documentation.id_set_documentation = documents.id_set_documentation  " +
                               $" WHERE design_object.id_design_object = '{date_projects[i].id_design_object_parent}'" +
                               $" ORDER BY  set_documentation.stamps_full_name ";

                        get_data_sql = sql_Requests.Select_Object(selectQuery);
                        distinct_id_design_object = date_projects[i].id_design_object;
                        output_set_documentation.Add(new Set_Documentation(get_data_sql[0].id_design_object, get_data_sql[0].cipher, date_projects[i].full_code,
                        get_data_sql[0].stamps_short_name, get_data_sql[0].number_set_documentation, get_data_sql[0].stamps_full_name,
                        get_data_sql[0].executor_full_name, get_data_sql[0].data_creation_set_docment, get_data_sql[0].data_change_set_docment));
                        get_data_sql.Clear(); 
                        stamps_number_list.Add(new Design_Object(date_projects[i].id_design_object, date_projects[i].id_parent, date_projects[i].stamps_number));
                        stamps_number_list.Add(new Design_Object(date_projects[i].id_design_object, date_projects[i].id_parent, date_projects[i].stamps_number));

                    }
                    else if (i == date_projects.Count - 1 && date_projects[i].id_design_object != date_projects[i - 1].id_design_object)

                    {
                        if (id_parent_list.IndexOf(date_projects[i].id_design_object_parent) == -1 &&
                               id_parent_list.IndexOf(distinct_id_design_object) == -1)
                        {
                        id_parent_list.Add(date_projects[i].id_design_object_parent);
                        }
                        selectQuery = $"SELECT DISTINCT   full_code, set_documentation.stamps_full_name,  cipher, " +
                              $" name_object, stamps_short_name,  " +
                              $" stamps_short_name || number_set_documentation AS stamps_number, data_creation_set_docment, data_change_set_docment, " +
                              $" executor_full_name" +
                              $" FROM project" +
                              $" LEFT JOIN guide_executors ON project.id_executor = guide_executors.id_executor " +
                              $" LEFT JOIN design_object  ON project.id_project =design_object.id_project" +
                              $" LEFT JOIN set_documentation ON design_object.id_design_object = set_documentation.id_design_object" +
                              $" LEFT JOIN guide_stamps ON set_documentation.id_stamps = guide_stamps.id_stamps  " +
                              $" LEFT JOIN documents ON set_documentation.id_set_documentation = documents.id_set_documentation  " +
                              $" WHERE design_object.id_design_object = '{date_projects[i].id_design_object_parent}'" +
                              $" ORDER BY  set_documentation.stamps_full_name ";


                        get_data_sql = sql_Requests.Select_Object(selectQuery);
                        distinct_id_design_object = date_projects[i].id_design_object;
                        output_set_documentation.Add(new Set_Documentation(get_data_sql[0].id_design_object, get_data_sql[0].cipher, date_projects[i].full_code,
                        get_data_sql[0].stamps_short_name, get_data_sql[0].number_set_documentation, get_data_sql[0].stamps_full_name,
                        get_data_sql[0].executor_full_name, get_data_sql[0].data_creation_set_docment, get_data_sql[0].data_change_set_docment));
                        get_data_sql.Clear(); 
                        stamps_number_list.Add(new Design_Object(date_projects[i].id_design_object, date_projects[i].id_parent, date_projects[i].stamps_number));
                        stamps_number_list_list.Add(new List<Design_Object>(stamps_number_list));
                    }
                    else if (i == date_projects.Count - 1 && date_projects[i].id_design_object == date_projects[i - 1].id_design_object)

                    {
                        selectQuery = $"SELECT DISTINCT   full_code, set_documentation.stamps_full_name,  cipher, " +
                              $" name_object, stamps_short_name,  " +
                              $" stamps_short_name || number_set_documentation AS stamps_number, data_creation_set_docment, data_change_set_docment, " +
                              $" executor_full_name" +
                              $" FROM project" +
                              $" LEFT JOIN guide_executors ON project.id_executor = guide_executors.id_executor " +
                              $" LEFT JOIN design_object  ON project.id_project =design_object.id_project" +
                              $" LEFT JOIN set_documentation ON design_object.id_design_object = set_documentation.id_design_object" +
                              $" LEFT JOIN guide_stamps ON set_documentation.id_stamps = guide_stamps.id_stamps  " +
                              $" LEFT JOIN documents ON set_documentation.id_set_documentation = documents.id_set_documentation  " +
                              $" WHERE design_object.id_design_object = '{date_projects[i].id_design_object_parent}'" +
                              $" ORDER BY  set_documentation.stamps_full_name ";


                        get_data_sql = sql_Requests.Select_Object(selectQuery);
                        distinct_id_design_object = date_projects[i].id_design_object;
                        output_set_documentation.Add(new Set_Documentation(get_data_sql[0].id_design_object, get_data_sql[0].cipher, date_projects[i].full_code,
                        get_data_sql[0].stamps_short_name, get_data_sql[0].number_set_documentation, get_data_sql[0].stamps_full_name,
                        get_data_sql[0].executor_full_name, get_data_sql[0].data_creation_set_docment, get_data_sql[0].data_change_set_docment));
                        get_data_sql.Clear();
                        stamps_number_list.Add(new Design_Object(date_projects[i].id_design_object, date_projects[i].id_parent, date_projects[i].stamps_number));

                        if (id_parent_list.IndexOf(date_projects[i].id_design_object_parent) == -1 &&
                             id_parent_list.IndexOf(distinct_id_design_object) == -1)
                        {
                        id_parent_list.Add(date_projects[i].id_design_object_parent);
                      }
                    }
                }
            }
            Find_Parent_Object(date_projects);

           
            while(id_parent_list.Count !=0)
            {
              for (int i = 0; i < id_parent_list.Count; i++)
              {
                 selectQuery = $"SELECT DISTINCT  o.id_design_object, o.full_code, r.full_code, set_documentation.stamps_full_name, full_cipher_documents, o.id_parent, cipher, " +
                                 $"o.data_creation_design_object, o.data_change_design_object, " +
                                 $" o.id_design_object, o.code, o.name_object, stamps_short_name,  " +
                                 $" stamps_short_name || number_set_documentation AS stamps_number, data_creation_set_docment, data_change_set_docment, " +
                                 $" r.id_design_object AS id_design_object_parent, executor_full_name," +
                                 $" r.code AS code_parent,"                                 +
                                 $" r.name_object AS name_object_parent," +
                                 $" r.id_parent AS id_parent_parent" +
                                 $" FROM project" +
                                 $" LEFT JOIN guide_executors ON project.id_executor = guide_executors.id_executor " +
                                 $" LEFT JOIN design_object AS o ON project.id_project = o.id_project" +
                                 $" LEFT JOIN set_documentation ON o.id_design_object = set_documentation.id_design_object" +
                                 $" LEFT JOIN guide_stamps ON set_documentation.id_stamps = guide_stamps.id_stamps  " +
                                 $" LEFT JOIN documents ON set_documentation.id_set_documentation = documents.id_set_documentation  " +
                                 $"   JOIN design_object AS r ON o.id_design_object = r.id_parent" +
                                  $" WHERE o.id_design_object = '{id_parent_list[i]}'" +
                                  $" ORDER BY  set_documentation.stamps_full_name ";
                date_projects = sql_Requests.Select_Object(selectQuery);
                Find_Parent_Object(date_projects);
                id_parent_list.RemoveAt(i);
              }
            } 
            void Delete_Dublicat(List<Set_Documentation> output_set_documentation_)
            {
                for (int i = 0; i < output_set_documentation.Count; i++)
                {
                    if (i != output_set_documentation.Count - 1 && output_set_documentation[i].stamps_full_name == output_set_documentation[i + 1].stamps_full_name)
                    {
                        output_set_documentation.RemoveAt(i);
                    }
                    if (i == output_set_documentation.Count - 1 && output_set_documentation[i].stamps_full_name == output_set_documentation[i - 1].stamps_full_name)
                    {
                        output_set_documentation.RemoveAt(i);
                    }
                }
            }
            Delete_Dublicat(output_set_documentation);

;
            //stamps_number_list_list.Add(stamps_number_list);



            //while (stop_full_code == false)
            //{
            //    Get_Name_Object_List(id_design_object, id_object_list, first_code);
            //}

            ////Вычисление id дочерних обьектов
            //(int id_design_object, List<int> id_object_list, string first_code) Get_Name_Object_List(int id_design_object__, List<int> id_object_list_, string first_code_)
            //{

            //    for (int i = 0; i < name_object_l.Count; i++)
            //    {
            //        if (name_object_l[i].name_object == selected_object_name)
            //        {
            //            if (count == 0)
            //            {
            //                first_id = name_object_l[i].id_design_object;
            //            }
            //            count++;
            //            id_design_object = name_object_l[i].id_design_object;
            //            break;
            //        }
            //    }
            //  void Find_Id_List(Design_Object list, int id_design)
            //  {
            //     for (int i = 0; i < name_object_l.Count; i++)
            //     {
            //        if (name_object_l[i].id_parent == id_design_object)
            //        {
            //            id_object_list.Add(name_object_l[i].id_design_object);
            //            selected_object_name = name_object_l[i].name_object;
            //        }
            //        else if (i == name_object_l.Count - 1 && name_object_l[i].id_parent != id_design_object)
            //        {
            //            stop_full_code = true;
            //            id_object_list.Insert(0, first_id);
            //            break;
            //        }
            //     }
            //  }

            //    return (id_design_object, id_object_list, first_code);
            //}
            //count = 0;
            //stop_full_code = false;
            //selected_object_name = (sender as Hyperlink).Tag as string;

            //List<Set_Documentation> set_documentations_l = new List<Set_Documentation>();
            //for (int i = 0; i < id_object_list.Count; i++)
            //{
            //    id_design_object = id_object_list[i];
            //    full_code_list_items.Clear();
            //    stop_full_code = false;
            //    count = 0;
            //    while (stop_full_code == false)
            //    {
            //        Full_Code(id_design_object, full_code_list_items, first_code);
            //    }
            //}
            ////Вычисление  полного шифра и полного кода, на основе списка ID обьектов полученного выше
            //(int id_design_object, List<string> full_code, string first_code) Full_Code( int id_design_object__, List<string> full_code_, string first_code_)
            //{

            //   for (int i = 0; i < name_object_l.Count; i++)
            //   {
            //        if (name_object_l[i].id_design_object == id_design_object)
            //        {
            //            if(  count == 0)
            //            {
            //                first_code = name_object_l[i].code + ".";
            //                full_stamps = name_object_l[i].stamps_number;
            //                cipher = name_object_l[i].cipher;
            //            }

            //            id_parent = name_object_l[i].id_parent;
            //            count++;
            //            break;
            //        }
            //   }
            //   for (int i = 0; i < name_object_l.Count; i++)
            //    {
            //        if (name_object_l[i].id_design_object == id_parent && condition != name_object_l[i].code)
            //        {

            //            full_code_list_items.Add(name_object_l[i].code + ".");
            //            condition = name_object_l[i].code; //stop_add_list_items = true;
            //        }
            //        else if (i == name_object_l.Count - 1 && name_object_l[i].id_design_object != id_parent)
            //        {  stop_full_code = true;
            //            full_code_list_items.Insert(0,first_code);
            //            foreach (var n in full_code_list_items)
            //            {
            //                full_code = full_code + n;
            //            }

            //            Set_Documentation set_documentation = new Set_Documentation();
            //            set_documentation.full_code_design_object = full_code;
            //            set_documentation.full_cipher_project = cipher + "-" + full_code + "-" + full_stamps;
            //            set_documentation.cipher = cipher;
            //            set_documentations_l.Add(set_documentation);
            //            break;
            //        }
            //    }
            //    return (id_design_object, full_code_list_items, first_code);
            //}

            ////Формирование окончательного списка данных для вывода в таблицу

            //string selectquery_id_object_list = string.Empty;
            //ObservableCollection<Set_Documentation> Set_documents_l = new ObservableCollection<Set_Documentation>();
            //List<Set_Documentation> requests_set_documentation = new List<Set_Documentation>();

            //for (int i = 0; i < id_object_list.Count; i++)
            //{
            //    string selectQuery = $"SELECT  stamps_full_name, executor_full_name, number_set_documentation, " +
            //                   $"  data_creation_set_docment,  data_change_set_docment" +
            //                   $" FROM design_object " +
            //                   $" LEFT JOIN set_documentation ON design_object.stamps_number =" +
            //                   $" set_documentation.stamps_number " +
            //                   $" LEFT JOIN guide_executors ON design_object.id_executor =" +
            //                   $" guide_executors.id_executor " +
            //                   $" LEFT JOIN guide_stamps ON set_documentation.id_stamps =" +
            //                   $" guide_stamps.id_stamps " +
            //                   $" WHERE id_design_object = '{id_object_list[i]}'";

            //    requests_set_documentation = sql_Requests.Select_Set_Documentation(selectQuery);
            //    Set_documents_l.Add(new Set_Documentation(set_documentations_l[i].cipher, set_documentations_l[i].full_code_design_object,
            //    requests_set_documentation[0].stamps_full_name, requests_set_documentation[0].number_set_documentation,
            //    set_documentations_l[i].full_cipher_project, requests_set_documentation[0].executor_full_name,
            //    requests_set_documentation[0].data_creation_set_docment, requests_set_documentation[0].data_change_set_docment));

            //}
            dataGrid.ItemsSource = output_set_documentation;
           
        }
        /// <summary>
        /// Создание таблицы при выборе комплекта документации 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Hyperlink_Click_Set_Documentation(object sender, RoutedEventArgs e)
        {


            string selected_stamps_number = (sender as Hyperlink).Tag as string;

            Hyperlink link = (Hyperlink)sender;
            string tag = (string)link.Tag;
            Design_Object design_object_click = (Design_Object)link.DataContext;
            int id_parent = 0;
            int id_design_object = 0;
            List<string> full_code_list_items = new List<string>();
            List<int> id_object_list = new List<int>();

            string first_code = string.Empty;
            string full_code = string.Empty;
            string condition = string.Empty;
            int first_id = 0;
            string full_stamps = string.Empty;
            string cipher = string.Empty;
            int count = 0;
            bool stop_full_code = false;


            while (stop_full_code == false)
            {
                Get_Name_Object_List(id_design_object, id_object_list, first_code);
            }

            //Вычисление id дочерних обьектов
            (int id_design_object, List<int> id_object_list, string first_code) Get_Name_Object_List(int id_design_object_, List<int> id_object_list_, string first_code_)
            {

                for (int i = 0; i < name_object_l.Count; i++)
                {
                    if (name_object_l[i].stamps_number == selected_stamps_number)
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
                        selected_stamps_number = name_object_l[i].name_object;
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
            selected_stamps_number = (sender as Hyperlink).Tag as string;

            List<Set_Documentation> set_documentations_l = new List<Set_Documentation>();
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
            //Вычисление  полного шифра и полного кода, на основе списка ID обьектов полученного выше
            (int id_design_object, List<string> full_code, string first_code) Full_Code(int id_design_object_, List<string> full_code_, string first_code_)
            {

                for (int i = 0; i < name_object_l.Count; i++)
                {
                    if (name_object_l[i].id_design_object == id_design_object)
                    {
                        if (count == 0)
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
                    if (name_object_l[i].id_design_object == id_parent && condition != name_object_l[i].code)
                    {

                        full_code_list_items.Add(name_object_l[i].code + ".");
                        condition = name_object_l[i].code; //stop_add_list_items = true;
                    }
                    else if (i == name_object_l.Count - 1 && name_object_l[i].id_design_object != id_parent)
                    {
                        stop_full_code = true;
                        full_code_list_items.Insert(0, first_code);
                        foreach (var n in full_code_list_items)
                        {
                            full_code = full_code + n;
                        }

                        Set_Documentation set_documentation = new Set_Documentation();
                        set_documentation.full_code_design_object = full_code;
                        set_documentation.full_cipher_project = cipher + "-" + full_code + "-" + full_stamps;
                        set_documentation.cipher = cipher;
                        set_documentations_l.Add(set_documentation);
                        break;
                    }
                }
                return (id_design_object, full_code_list_items, first_code);
            }

            //Формирование окончательного списка данных для вывода в таблицу

            string selectquery_id_object_list = string.Empty;
            ObservableCollection<Set_Documentation> Set_documents_l = new ObservableCollection<Set_Documentation>();
            List<Set_Documentation> requests_set_documentation = new List<Set_Documentation>();
            Sql_Requests sql_Requests = new Sql_Requests();
            for (int i = 0; i < id_object_list.Count; i++)
            {
                string selectQuery = $"SELECT  stamps_full_name, executor_full_name, number_set_documentation, " +
                               $"  data_creation_set_docment,  data_change_set_docment" +
                               $" FROM design_object " +
                               $" LEFT JOIN set_documentation ON design_object.stamps_number =" +
                               $" set_documentation.stamps_number " +
                               $" LEFT JOIN guide_executors ON design_object.id_executor =" +
                               $" guide_executors.id_executor " +
                               $" LEFT JOIN guide_stamps ON set_documentation.id_stamps =" +
                               $" guide_stamps.id_stamps " +
                               $" WHERE id_design_object = '{id_object_list[i]}'";

                requests_set_documentation = sql_Requests.Select_Set_Documentation(selectQuery);
                Set_documents_l.Add(new Set_Documentation(date_projects[i].id_design_object, set_documentations_l[i].cipher, set_documentations_l[i].full_code_design_object,
                requests_set_documentation[0].stamps_full_name, requests_set_documentation[0].number_set_documentation,
                set_documentations_l[i].full_cipher_project, requests_set_documentation[0].executor_full_name,
                requests_set_documentation[0].data_creation_set_docment, requests_set_documentation[0].data_change_set_docment));

            }
            dataGrid.ItemsSource = Set_documents_l;

        }
        /// <summary>
        /// Создание дерева из обьектов проектирования и коплектов документации при выборе проекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            name_object_l.Clear();
            Sql_Requests sql_Requests = new Sql_Requests();
            comboBox1.DataContext = comboBox1.SelectedItem;

            Project project = comboBox1.DataContext as Project;

           

            string selectQuery = $"SELECT DISTINCT o.full_code, r.full_code, set_documentation.stamps_full_name, full_cipher_documents, o.id_parent, cipher, " +
                                 $"o.data_creation_design_object, o.data_change_design_object, " +
                                 $" o.id_design_object, o.code, o.name_object, stamps_short_name,  " +
                                 $" stamps_short_name || number_set_documentation AS stamps_number, data_creation_set_docment, data_change_set_docment, " +
                                 $" r.id_design_object AS id_design_object_parent, executor_full_name," +
                                 $" r.code AS code_parent," 
                                 +
                                 $" r.name_object AS name_object_parent," +
                                  
                                 $" r.id_parent AS id_parent_parent" +
                                 $" FROM project" +
                                 
                                 $" LEFT JOIN guide_executors ON project.id_executor = guide_executors.id_executor " +
                                 $" LEFT JOIN design_object AS o ON project.id_project = o.id_project" +
                                 $" LEFT JOIN set_documentation ON o.id_design_object = set_documentation.id_design_object" +
                                 $" LEFT JOIN guide_stamps ON set_documentation.id_stamps = guide_stamps.id_stamps  " +
                                 $" LEFT JOIN documents ON set_documentation.id_set_documentation = documents.id_set_documentation  " +

                                 $"   JOIN design_object AS r ON o.id_design_object = r.id_parent" +
                                 $" WHERE project.id_project = '{project.id_project}'" +
                                 $" ORDER BY  r.id_design_object";

            date_projects = sql_Requests.Select_Object(selectQuery);
         
            List<Design_Object> stamps_number_list = new List<Design_Object>();
            List<List<Design_Object>> stamps_number_list_list = new List<List<Design_Object>>();

            for (int i = 0; i < date_projects.Count; i++)
            {
                if (i ==0 )
                {
                    stamps_number_list.Add(new Design_Object(date_projects[i].id_design_object, date_projects[i].id_parent, date_projects[i].stamps_number));
                }

                 else if (i != date_projects.Count-1 && date_projects[i].id_design_object == date_projects[i-1].id_design_object)
                {
                    stamps_number_list.Add(new Design_Object(date_projects[i].id_design_object, date_projects[i].id_parent, date_projects[i].stamps_number));
                }

                else if(i != date_projects.Count - 1 && date_projects[i].id_design_object != date_projects[i - 1].id_design_object &&
                   date_projects[i].id_design_object != date_projects[i + 1].id_design_object)

                {
                    stamps_number_list_list.Add(new List<Design_Object>(stamps_number_list));
                    stamps_number_list.Clear();
                    stamps_number_list.Add(new Design_Object(date_projects[i].id_design_object, date_projects[i].id_parent, date_projects[i].stamps_number));
                   
                }
                else if (i != date_projects.Count - 1 && date_projects[i].id_design_object != date_projects[i - 1].id_design_object &&
                   date_projects[i].id_design_object == date_projects[i + 1].id_design_object)

                {
                    stamps_number_list_list.Add(new List<Design_Object>(stamps_number_list));
                    stamps_number_list.Clear();
                    stamps_number_list.Add(new Design_Object(date_projects[i].id_design_object, date_projects[i].id_parent, date_projects[i].stamps_number));
                   
                }
                else if (i == date_projects.Count - 1 && date_projects[i].id_design_object != date_projects[i - 1].id_design_object )

                {
                    stamps_number_list_list.Add(new List<Design_Object>(stamps_number_list));
                    stamps_number_list.Clear();
                    stamps_number_list.Add(new Design_Object(date_projects[i].id_design_object, date_projects[i].id_parent, date_projects[i].stamps_number));
                    stamps_number_list_list.Add(new List<Design_Object>(stamps_number_list));
                }
                else if (i == date_projects.Count - 1 && date_projects[i].id_design_object == date_projects[i - 1].id_design_object)

                {
                    stamps_number_list.Add(new Design_Object(date_projects[i].id_design_object, date_projects[i].id_parent, date_projects[i].stamps_number));
                    stamps_number_list_list.Add(new List<Design_Object>(stamps_number_list));
                }

            }

            foreach(var stamp in stamps_number_list_list)
            {
                listBox.ItemsSource = stamp;
                foreach (Design_Object stamp_obj in stamp)
                {
                    
                }
            }

                foreach (Design_Object i in date_projects)
                {
                 name_object_l.Add(new Design_Object(  i.executor_full_name, i.data_creation_design_object, i.data_change_design_object, i.cipher,
                 i.id_design_object, i.code, i.name_object,  i.stamps_number, i.id_parent,
                 i.id_design_object_parent, i.code_parent, i.name_object_parent, i.stamps_number_parent, i.id_parent_parent));
                 
                }
            //List<Design_Object> stamps_number_list_ = new List<Design_Object>();
            //List<List<Design_Object>> stamps_number_list_list_ = new List<List<Design_Object>>();
            //for (int i = 0; i < stamps_number_list_list.Count; i++)
            //{
            //    for(int j = 0; j < stamps_number_list_list[i].Count; j++)
            //    {
            //        stamps_number_list_.Add(new Design_Object(stamps_number_list_list[i][j]));
            //    }
            //    stamps_number_list_list_.Add(stamps_number_list_);
            //}

            //List<Design_Object> date_projects = sql_Requests.Select_Object(selectQuery);
            ////List<List<string>> stamps_number_list_list = new List<List<string>>();
            //List<List<string>> stamps_number_list_list = new List<List<string>>();
            //List<string> stamps_number_list = new List<string>();


            //for (int i = 0; i < date_projects.Count; i++)
            //{
            //    if (i == 0)
            //    {
            //        stamps_number_list.Add(date_projects[i].stamps_number);
            //    }

            //    else if (i != date_projects.Count - 1 && date_projects[i].id_design_object == date_projects[i - 1].id_design_object)
            //    {
            //        stamps_number_list.Add(date_projects[i].stamps_number);
            //    }

            //    else if (i != date_projects.Count - 1 && date_projects[i].id_design_object != date_projects[i - 1].id_design_object &&
            //       date_projects[i].id_design_object != date_projects[i + 1].id_design_object)

            //    {
            //        stamps_number_list_list.Add(new List<string>(stamps_number_list));
            //        stamps_number_list.Clear();
            //        stamps_number_list.Add(date_projects[i].stamps_number);

            //    }
            //    else if (i != date_projects.Count - 1 && date_projects[i].id_design_object != date_projects[i - 1].id_design_object &&
            //       date_projects[i].id_design_object == date_projects[i + 1].id_design_object)

            //    {
            //        stamps_number_list_list.Add(new List<string>(stamps_number_list));
            //        stamps_number_list.Clear();
            //        stamps_number_list.Add(date_projects[i].stamps_number);

            //    }
            //    else if (i == date_projects.Count - 1 && date_projects[i].id_design_object != date_projects[i - 1].id_design_object)

            //    {
            //        stamps_number_list_list.Add(new List<string>(stamps_number_list));
            //        stamps_number_list.Clear();
            //        stamps_number_list.Add(date_projects[i].stamps_number);
            //        stamps_number_list_list.Add(new List<string>(stamps_number_list));
            //    }
            //    else if (i == date_projects.Count - 1 && date_projects[i].id_design_object == date_projects[i - 1].id_design_object)

            //    {
            //        stamps_number_list.Add(date_projects[i].stamps_number);
            //        stamps_number_list_list.Add(new List<string>(stamps_number_list));
            //    }

            //}
            listBox.ItemsSource = name_object_l;

        }
        private void DG1_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headername = e.Column.Header.ToString();

            //Cancel the column you don't want to generate
            if (headername == "name_set_documentation")
            {
                e.Cancel = true;
            }
            else if (headername == "number_type_documents")
            {
                e.Cancel = true;
            }
            //else if (headername == "id_parent")
            //{
            //    e.Cancel = true;
            //}
            else if (headername == "id_documents")
            {
                e.Cancel = true;
            }
            //else if (headername == "code")
            //{
            //    e.Cancel = true;
            //}
            //else if (headername == "id_design_object")
            //{
            //    e.Cancel = true;
            //}
            //else if (headername == "stamps_number")
            //{
            //    e.Cancel = true;
            //}
            //else if (headername == "stamps_number_parent")
            //{
            //    e.Cancel = true;
            //}
            //else if (headername == "id_design_object_parent")
            //{
            //    e.Cancel = true;
            //}
            //else if (headername == "name_object_parent")
            //{
            //    e.Cancel = true;
            //}
            //else if (headername == "code_parent")
            //{
            //    e.Cancel = true;
            //}
            //else if (headername == "id_parent_parent")
            //{
            //    e.Cancel = true;
            //}
            //else if (headername == "id_project")
            //{
            //    e.Cancel = true;
            //}
            //else if (headername == "name_object")
            //{
            //    e.Cancel = true;
            //}
            //else if (headername == "id_set_documentation")
            //{
            //    e.Cancel = true;
            //}
            //else if (headername == "name_project")
            //{
            //    e.Cancel = true;
            //}
            //else if (headername == "data_creation_design_object")
            //{
            //    e.Cancel = true;
            //}

            //else if (headername == "data_change_design_object")
            //{
            //    e.Cancel = true;
            //}
            //else if (headername == "stamps_short_name")
            //{
            //    e.Cancel = true;
            //}
            //else if (headername == "id_type_documents")
            //{
            //    e.Cancel = true;
            //}
            //else if (headername == "id_stamps")
            //{
            //    e.Cancel = true;
            //}

            //update column details when generating
            if (headername == "number_set_documentation")
            {
                e.Column.Header = "Номер";
            }
            else if (headername == "data_creation_set_docment")
            {
                e.Column.Header = "Дата создания";
            }
            else if (headername == "data_change_set_docment")
            {
                e.Column.Header = "Дата изменения"; 
            }
            else if (headername == "full_cipher_documents")
            {
                e.Column.Header = "Полный шифр документа";
            }
            else if (headername == "full_code_design_object")
            {
                e.Column.Header = "Полный код проекта";
            }
            else if (headername == "full_cipher_project")
            {
                e.Column.Header = "Полный шифр документа";
            }
            else if (headername == "cipher")
            {
                e.Column.Header = "Шифр";
            }
            else if (headername == "stamps_full_name")
            {
                e.Column.Header = "Марка"; 
            }
            else if (headername == "type_documents_full_Name")
            {
                e.Column.Header = "Тип документа";
            }
            else if (headername == "executor_full_name")
            {
                e.Column.Header = "Подрядчик";
            }
        }
         
    }
}


//List<Documents> date_documents = new List<Documents>();
//for (int i = 0; i < id_object_list.Count; i++)
//{
//    string selectQuery = $"SELECT  set_documentation.id_stamps,  number_set_documentation,   type_documents_full_name,  number_document,  name_document," +
//                   $"  data_creation_document,  data_change_document" +
//                   $" FROM design_object " +
//                   $" LEFT JOIN set_documentation ON design_object.stamps_number =" +
//                   $" set_documentation.stamps_number " +
//                   $" LEFT JOIN documents ON documents.number_type_documents =" +
//                   $" set_documentation.number_type_documents " +

//                   $" LEFT JOIN guide_type_documents ON documents.id_type_documents =" +
//                   $" guide_type_documents.id_type_documents " +
//                   $" LEFT JOIN guide_stamps ON set_documentation.id_stamps =" +
//                   $" guide_stamps.id_stamps " +
//                   $" WHERE id_design_object = '{selectquery_id_object_list}'";
//    Sql_Requests sql_Requests = new Sql_Requests();
//    date_documents = sql_Requests.Select_Set_Documentation(selectQuery);
//}





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
