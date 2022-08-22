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
            List<Project> output_name_projects = new List<Project>();
           
            List<Dictionary> output_dictionary = new List<Dictionary>();
            // comboBox_projects.DataContext = date_projects;
            foreach (Project project in date_projects)
            {
                output_name_projects.Add(new Project(project.id_project, project.name_project.ToString()));
            }

            //dataGrid.ItemsSource = date_projects;
            comboBox1.ItemsSource = output_name_projects;
            selectQuery = $@"select executor_short_name
                           from guide_executors
                           ORDER BY id_executor ASC ";
            List<Dictionary> dictionary = sql_Requests.Select_Dictionary(selectQuery);

            foreach (Dictionary d in dictionary)
            {
                output_dictionary.Add(new Dictionary(d.id_executor, d.executor_short_name, d.id_stamps, d.stamps_short_name,
                d.id_type_documents, d.type_documents_short_name));
            }
            set_project.ItemsSource = output_dictionary;
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
        /// 
        void Hyperlink_Click_Object_Name_Parent(object sender, RoutedEventArgs e)
        {


            Hyperlink link = (Hyperlink)sender;
            string tag = (string)link.Tag;
            Design_Object design_object_click = (Design_Object)link.DataContext;
            int disign_id = design_object_click.id_design_object_parent;
            Sql_Requests sql_Requests = new Sql_Requests();
            string selectQuery = string.Empty;
            List<int> all_id = new List<int>();

            for (int i = 0; i < date_projects.Count; i++)
            {
                if (i == 0)
                {
                    if (all_id.IndexOf(date_projects[i].id_design_object) == -1)
                    {
                        all_id.Add(date_projects[i].id_design_object);
                    }
                    if (all_id.IndexOf(date_projects[i].id_design_object_parent) == -1)
                    {
                        all_id.Add(date_projects[i].id_design_object_parent);
                    }


                    //stamps_number_list.Add(new Design_Object(date_projects[i].id_design_object, date_projects[i].id_parent, date_projects[i].stamps_number));
                }

                else if (i != date_projects.Count - 1 && date_projects[i].id_design_object == date_projects[i - 1].id_design_object)
                {
                    if (all_id.IndexOf(date_projects[i].id_design_object) == -1)
                    {
                        all_id.Add(date_projects[i].id_design_object);
                    }
                    if (all_id.IndexOf(date_projects[i].id_design_object_parent) == -1)
                    {
                        all_id.Add(date_projects[i].id_design_object_parent);
                    }
                    //stamps_number_list.Add(new Design_Object(date_projects[i].id_design_object, date_projects[i].id_parent, date_projects[i].stamps_number));
                }

                else if (i != date_projects.Count - 1 && date_projects[i].id_design_object != date_projects[i - 1].id_design_object &&
                   date_projects[i].id_design_object != date_projects[i + 1].id_design_object)

                {
                    if (all_id.IndexOf(date_projects[i].id_design_object) == -1)
                    {
                        all_id.Add(date_projects[i].id_design_object);
                    }
                    if (all_id.IndexOf(date_projects[i].id_design_object_parent) == -1)
                    {
                        all_id.Add(date_projects[i].id_design_object_parent);
                    }
                    //stamps_number_list_list.Add(new List<Design_Object>(stamps_number_list));

                    //stamps_number_list.Add(new Design_Object(date_projects[i].id_design_object, date_projects[i].id_parent, date_projects[i].stamps_number));

                }
                else if (i != date_projects.Count - 1 && date_projects[i].id_design_object != date_projects[i - 1].id_design_object &&
                   date_projects[i].id_design_object == date_projects[i + 1].id_design_object)

                {
                    if (all_id.IndexOf(date_projects[i].id_design_object) == -1)
                    {
                        all_id.Add(date_projects[i].id_design_object);
                    }
                    if (all_id.IndexOf(date_projects[i].id_design_object_parent) == -1)
                    {
                        all_id.Add(date_projects[i].id_design_object_parent);
                    }
                    //stamps_number_list_list.Add(new List<Design_Object>(stamps_number_list));

                    //stamps_number_list.Add(new Design_Object(date_projects[i].id_design_object, date_projects[i].id_parent, date_projects[i].stamps_number));

                }
                else if (i == date_projects.Count - 1 && date_projects[i].id_design_object != date_projects[i - 1].id_design_object)

                {
                    if (all_id.IndexOf(date_projects[i].id_design_object) == -1)
                    {
                        all_id.Add(date_projects[i].id_design_object);
                    }
                    if (all_id.IndexOf(date_projects[i].id_design_object_parent) == -1)
                    {
                        all_id.Add(date_projects[i].id_design_object_parent);
                    }
                    //stamps_number_list_list.Add(new List<Design_Object>(stamps_number_list));

                    //stamps_number_list.Add(new Design_Object(date_projects[i].id_design_object, date_projects[i].id_parent, date_projects[i].stamps_number));
                    //stamps_number_list_list.Add(new List<Design_Object>(stamps_number_list));
                }
                else if (i == date_projects.Count - 1 && date_projects[i].id_design_object == date_projects[i - 1].id_design_object)

                {
                    if (all_id.IndexOf(date_projects[i].id_design_object) == -1)
                    {
                        all_id.Add(date_projects[i].id_design_object);
                    }
                    if (all_id.IndexOf(date_projects[i].id_design_object_parent) == -1)
                    {
                        all_id.Add(date_projects[i].id_design_object_parent);
                    }
                    //stamps_number_list.Add(new Design_Object(date_projects[i].id_design_object, date_projects[i].id_parent, date_projects[i].stamps_number));
                    //stamps_number_list_list.Add(new List<Design_Object>(stamps_number_list));
                }

            }
            List<Design_Object> date_right_table = new List<Design_Object>();
            ObservableCollection<Design_Object> itog_date_list = new ObservableCollection<Design_Object>();
            for (int i = disign_id -1; i < all_id.Count; i++)
            {
                selectQuery = $"SELECT DISTINCT  design_object.id_design_object, " +
                                                $" full_code,  " +
                                                $" set_documentation.stamps_full_name, " +
                                                $" cipher, " +
                                                $" data_creation_design_object," +
                                                $" data_change_design_object, " +
                                                $" code," +
                                                $" name_object," +
                                                $" number_set_documentation, " +
                                                $" stamps_short_name,  " +
                                                $" stamps_short_name || number_set_documentation AS stamps_number," +
                                                $" data_creation_set_docment," +
                                                $" data_change_set_docment, " +
                                                $" executor_full_name," +
                                                $" name_object " +

                                                $" FROM project" +
                                                $" LEFT JOIN guide_executors ON project.id_executor = guide_executors.id_executor " +
                                                $" LEFT JOIN design_object   ON project.id_project = design_object.id_project" +
                                                $" LEFT JOIN set_documentation ON  design_object.id_design_object = set_documentation.id_design_object" +
                                                $" LEFT JOIN guide_stamps ON set_documentation.id_stamps = guide_stamps.id_stamps  " +
                                                $" LEFT JOIN documents ON set_documentation.id_set_documentation = documents.id_set_documentation  " +
                                                $" WHERE design_object.id_design_object = '{all_id[i]}'" +
                                                $" ORDER BY  set_documentation.stamps_full_name ";
                date_right_table = sql_Requests.Select_Object(selectQuery);
                for (int j = 0; j < date_right_table.Count; j++)
                {
                    itog_date_list.Add(new Design_Object(date_right_table[j].cipher, date_right_table[j].full_code, date_right_table[j].stamps_short_name,
                               date_right_table[j].number_set_documentation, date_right_table[j].stamps_full_name, date_right_table[j].executor_full_name,
                               date_right_table[j].data_creation_set_docment, date_right_table[j].data_change_set_docment));
                }

            }


            dataGrid.ItemsSource = itog_date_list;

        }
        void Hyperlink_Click_Object_Name(object sender, RoutedEventArgs e)
        {

            
            Hyperlink link = (Hyperlink)sender;
            string tag = (string)link.Tag;
            Design_Object design_object_click = (Design_Object)link.DataContext;
            int disign_id = design_object_click.id_design_object;
            Sql_Requests sql_Requests = new Sql_Requests();
            string selectQuery = string.Empty;
            List<int> all_id = new List<int>();
           
            for (int i = 0; i < date_projects.Count; i++)
            {
                if (i == 0)
                {
                    if (all_id.IndexOf(date_projects[i].id_design_object) == -1)
                    {
                        all_id.Add(date_projects[i].id_design_object);
                    }
                    if (all_id.IndexOf(date_projects[i].id_design_object_parent) == -1)
                    {
                        all_id.Add(date_projects[i].id_design_object_parent);
                    }


                    //stamps_number_list.Add(new Design_Object(date_projects[i].id_design_object, date_projects[i].id_parent, date_projects[i].stamps_number));
                }

                else if (i != date_projects.Count - 1 && date_projects[i].id_design_object == date_projects[i - 1].id_design_object)
                {
                    if (all_id.IndexOf(date_projects[i].id_design_object) == -1)
                    {
                        all_id.Add(date_projects[i].id_design_object);
                    }
                    if (all_id.IndexOf(date_projects[i].id_design_object_parent) == -1)
                    {
                        all_id.Add(date_projects[i].id_design_object_parent);
                    }
                    //stamps_number_list.Add(new Design_Object(date_projects[i].id_design_object, date_projects[i].id_parent, date_projects[i].stamps_number));
                }

                else if (i != date_projects.Count - 1 && date_projects[i].id_design_object != date_projects[i - 1].id_design_object &&
                   date_projects[i].id_design_object != date_projects[i + 1].id_design_object)

                {
                    if (all_id.IndexOf(date_projects[i].id_design_object) == -1)
                    {
                        all_id.Add(date_projects[i].id_design_object);
                    }
                    if (all_id.IndexOf(date_projects[i].id_design_object_parent) == -1)
                    {
                        all_id.Add(date_projects[i].id_design_object_parent);
                    }
                    //stamps_number_list_list.Add(new List<Design_Object>(stamps_number_list));

                    //stamps_number_list.Add(new Design_Object(date_projects[i].id_design_object, date_projects[i].id_parent, date_projects[i].stamps_number));

                }
                else if (i != date_projects.Count - 1 && date_projects[i].id_design_object != date_projects[i - 1].id_design_object &&
                   date_projects[i].id_design_object == date_projects[i + 1].id_design_object)

                {
                    if (all_id.IndexOf(date_projects[i].id_design_object) == -1)
                    {
                        all_id.Add(date_projects[i].id_design_object);
                    }
                    if (all_id.IndexOf(date_projects[i].id_design_object_parent) == -1)
                    {
                        all_id.Add(date_projects[i].id_design_object_parent);
                    }
                    //stamps_number_list_list.Add(new List<Design_Object>(stamps_number_list));

                    //stamps_number_list.Add(new Design_Object(date_projects[i].id_design_object, date_projects[i].id_parent, date_projects[i].stamps_number));

                }
                else if (i == date_projects.Count - 1 && date_projects[i].id_design_object != date_projects[i - 1].id_design_object)

                {
                    if (all_id.IndexOf(date_projects[i].id_design_object) == -1)
                    {
                        all_id.Add(date_projects[i].id_design_object);
                    }
                    if (all_id.IndexOf(date_projects[i].id_design_object_parent) == -1)
                    {
                        all_id.Add(date_projects[i].id_design_object_parent);
                    }
                    //stamps_number_list_list.Add(new List<Design_Object>(stamps_number_list));

                    //stamps_number_list.Add(new Design_Object(date_projects[i].id_design_object, date_projects[i].id_parent, date_projects[i].stamps_number));
                    //stamps_number_list_list.Add(new List<Design_Object>(stamps_number_list));
                }
                else if (i == date_projects.Count - 1 && date_projects[i].id_design_object == date_projects[i - 1].id_design_object)

                {
                    if (all_id.IndexOf(date_projects[i].id_design_object) == -1)
                    {
                        all_id.Add(date_projects[i].id_design_object);
                    }
                    if (all_id.IndexOf(date_projects[i].id_design_object_parent) == -1)
                    {
                        all_id.Add(date_projects[i].id_design_object_parent);
                    }
                    //stamps_number_list.Add(new Design_Object(date_projects[i].id_design_object, date_projects[i].id_parent, date_projects[i].stamps_number));
                    //stamps_number_list_list.Add(new List<Design_Object>(stamps_number_list));
                }

            }
            List<Design_Object> date_right_table = new List<Design_Object>();
            ObservableCollection<Design_Object> itog_date_list = new ObservableCollection<Design_Object>();
            for (int i = disign_id-1; i < all_id.Count; i++)
            {
                selectQuery = $"SELECT DISTINCT  design_object.id_design_object, " +
                                                $" full_code,  " +
                                                $" set_documentation.stamps_full_name, " +
                                                $" cipher, " +
                                                $" data_creation_design_object," +
                                                $" data_change_design_object, " +
                                                $" code," +
                                                $" name_object," +
                                                $" number_set_documentation, " +
                                                $" stamps_short_name,  " +
                                                $" stamps_short_name || number_set_documentation AS stamps_number," +
                                                $" data_creation_set_docment," +
                                                $" data_change_set_docment, " +
                                                $" executor_full_name," +
                                                $" name_object " +
                                                 
                                                $" FROM project" +
                                                $" LEFT JOIN guide_executors ON project.id_executor = guide_executors.id_executor " +
                                                $" LEFT JOIN design_object   ON project.id_project = design_object.id_project" +
                                                $" LEFT JOIN set_documentation ON  design_object.id_design_object = set_documentation.id_design_object" +
                                                $" LEFT JOIN guide_stamps ON set_documentation.id_stamps = guide_stamps.id_stamps  " +
                                                $" LEFT JOIN documents ON set_documentation.id_set_documentation = documents.id_set_documentation  " +
                                                $" WHERE design_object.id_design_object = '{all_id[i]}'" +
                                                $" ORDER BY  set_documentation.stamps_full_name ";
                date_right_table = sql_Requests.Select_Object(selectQuery);
                for (int j = 0; j < date_right_table.Count; j++)
                {
                    itog_date_list.Add(new Design_Object(date_right_table[j].cipher, date_right_table[j].full_code, date_right_table[j].stamps_short_name,
                               date_right_table[j].number_set_documentation, date_right_table[j].stamps_full_name, date_right_table[j].executor_full_name,
                               date_right_table[j].data_creation_set_docment, date_right_table[j].data_change_set_docment));
                }

            }
           
            
            dataGrid.ItemsSource = itog_date_list;
           
        }
        /// <summary>
        /// Создание таблицы при выборе комплекта документации 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Hyperlink_Click_Set_Documentation(object sender, RoutedEventArgs e)
        {
            Hyperlink link = (Hyperlink)sender;
            Design_Object set_doc_click = (Design_Object)link.DataContext;
            int set_doc_id = set_doc_click.id_set_documentation;
            Sql_Requests sql_Requests = new Sql_Requests();
            string selectQuery = string.Empty;
            List<int> all_id = new List<int>();
           
            List<Documents> date_right_table = new List<Documents>();
            ObservableCollection<Documents> itog_date_list = new ObservableCollection<Documents>();
            
                selectQuery = $"SELECT DISTINCT  type_documents_short_name, number_document, name_document, full_cipher_documents,"+
                              $" data_creation_document, data_change_document" +
                              $" FROM set_documentation" +
                              $" LEFT JOIN documents ON set_documentation.id_set_documentation = documents.id_set_documentation" +
                              $" LEFT JOIN guide_type_documents ON documents.id_type_documents = guide_type_documents.id_type_documents" +
                              $" WHERE set_documentation.id_set_documentation = '{set_doc_id}'";

                date_right_table = sql_Requests.Select_Documentation(selectQuery);
                for (int j = 0; j < date_right_table.Count; j++)
                {
                    itog_date_list.Add(new Documents(date_right_table[j].type_documents_short_name, date_right_table[j].number_document, date_right_table[j].name_document,
                               date_right_table[j].full_cipher_documents, date_right_table[j].data_creation_document, date_right_table[j].data_change_document));
                }
            dataGrid.ItemsSource = itog_date_list;
        }
        /// <summary>
        /// Создание дерева из обьектов проектирования и коплектов документации при выборе проекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            //MessageBox.Show(textBox.Text);
        }
       
            private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
            name_object_l.Clear();
            Sql_Requests sql_Requests = new Sql_Requests();
            comboBox1.DataContext = comboBox1.SelectedItem;
           
            Project project = comboBox1.DataContext as Project;
            int id_project = project.id_project;


            string selectQuery = $"SELECT DISTINCT set_documentation.id_set_documentation, project.id_project, o.full_code, r.full_code," +
                                 $" set_documentation.stamps_full_name, full_cipher_documents, o.id_parent, cipher, " +
                                 $"o.data_creation_design_object, o.data_change_design_object, " +
                                 $" o.id_design_object, o.code, o.name_object, stamps_short_name,  " +
                                 $" stamps_short_name || number_set_documentation AS stamps_number, data_creation_set_docment, data_change_set_docment, " +
                                 $" r.id_design_object AS id_design_object_parent, executor_full_name," +
                                 $" r.code AS code_parent,"  +
                                 $" r.name_object AS name_object_parent," +
                                 $" r.id_parent AS id_parent_parent" +
                                 $" FROM project" +
                                 
                                 $" LEFT JOIN guide_executors ON project.id_executor = guide_executors.id_executor " +
                                 $" LEFT JOIN design_object AS o ON project.id_project = o.id_project" +
                                 $" LEFT JOIN set_documentation ON o.id_design_object = set_documentation.id_design_object" +
                                 $" LEFT JOIN guide_stamps ON set_documentation.id_stamps = guide_stamps.id_stamps  " +
                                 $" LEFT JOIN documents ON set_documentation.id_set_documentation = documents.id_set_documentation  " +
                                 $" LEFT  JOIN design_object AS r ON o.id_design_object = r.id_parent" +
                                 $" WHERE project.id_project = '{project.id_project}'" +
                                 $" ORDER BY  r.id_design_object";

            date_projects = sql_Requests.Select_Object(selectQuery);
           
          
                foreach (Design_Object i in date_projects)
                {
                 name_object_l.Add(new Design_Object(i.id_set_documentation, id_project,  i.executor_full_name, i.data_creation_set_docment, i.data_change_set_docment, i.cipher,
                 i.id_design_object, i.code, i.name_object,  i.stamps_number, i.id_parent,
                 i.id_design_object_parent, i.code_parent, i.name_object_parent, i.stamps_number_parent, i.id_parent_parent));
                 
                }
          
            listBox.ItemsSource = name_object_l;

        }
        private void DG1_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            List<string> block_field_table = new List<string>()
            {
                "id_parent", 
                "id_documents",
                "code", 
                "id_design_object", 
                "stamps_number",
                "id_design_object",
                "stamps_number_parent", 
                "id_design_object_parent",
                "name_object_parent", 
                "code_parent", 
                "id_parent_parent",
                "id_project", 
                "name_object", 
                "id_set_documentation",
                "name_project", 
                "id_stamps",
            };

            string headername = e.Column.Header.ToString();
            for (int i = 0; i < block_field_table.Count; i++)
            {
                if (headername == block_field_table[i])
                {
                    e.Cancel = true;
                }
            }
            List<string> old_field_table = new List<string>()
            {
                "number_set_documentation",
                "data_creation_set_docment",
                "data_change_set_docment",
                "full_cipher_documents",
                "full_code_design_object",
                "cipher",
                "stamps_full_name",
                "type_documents_short_name",
                "executor_full_name",
                "number_document",
                "name_document",
                "data_creation_document",
                "data_change_document",
                "full_code",
                "number_set_documentation",
                "stamps_short_name"
                
            };
            List<string> new_field_table = new List<string>()
            {
                "Номер", 
                "Дата создания",
                "Дата изменения",
                "Полный шифр документа",
                "Полный код документа",
                "Шифр",
                "Марка - полное имя",
                "Тип документа",
                "Подрятчик",
                "Номер документа", 
                "Имя документа",
                "Дата создания",
                "Дата изменения", 
                "Полный код",
                "Номер комп. док.", 
                "Марка"
                 
            };
            
            for (int i = 0; i < old_field_table.Count; i++)
            {
                if (headername == old_field_table[i])
                {
                    e.Column.Header = new_field_table[i];
                }
            }
           
        }

        
        private void add_data_project_Click(object sender, RoutedEventArgs e)
        {

        }
        private void executor_set_project_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Sql_Requests sql_Requests = new Sql_Requests();
            
            comboBox1.DataContext = comboBox1.SelectedItem;
        }
    }
}

//all_id.Sort();
//all_id.Distinct();

//for (int i = 0; i < date_projects.Count; i++)
//{
//    if (i ==0 )
//    {
//        stamps_number_list.Add(new Design_Object(date_projects[i].id_design_object, date_projects[i].id_parent, date_projects[i].stamps_number));
//    }

//     else if (i != date_projects.Count-1 && date_projects[i].id_design_object == date_projects[i-1].id_design_object)
//    {
//        stamps_number_list.Add(new Design_Object(date_projects[i].id_design_object, date_projects[i].id_parent, date_projects[i].stamps_number));
//    }

//    else if(i != date_projects.Count - 1 && date_projects[i].id_design_object != date_projects[i - 1].id_design_object &&
//       date_projects[i].id_design_object != date_projects[i + 1].id_design_object)

//    {
//        stamps_number_list_list.Add(new List<Design_Object>(stamps_number_list));
//        stamps_number_list.Clear();
//        stamps_number_list.Add(new Design_Object(date_projects[i].id_design_object, date_projects[i].id_parent, date_projects[i].stamps_number));

//    }
//    else if (i != date_projects.Count - 1 && date_projects[i].id_design_object != date_projects[i - 1].id_design_object &&
//       date_projects[i].id_design_object == date_projects[i + 1].id_design_object)

//    {
//        stamps_number_list_list.Add(new List<Design_Object>(stamps_number_list));
//        stamps_number_list.Clear();
//        stamps_number_list.Add(new Design_Object(date_projects[i].id_design_object, date_projects[i].id_parent, date_projects[i].stamps_number));

//    }
//    else if (i == date_projects.Count - 1 && date_projects[i].id_design_object != date_projects[i - 1].id_design_object )

//    {
//        stamps_number_list_list.Add(new List<Design_Object>(stamps_number_list));
//        stamps_number_list.Clear();
//        stamps_number_list.Add(new Design_Object(date_projects[i].id_design_object, date_projects[i].id_parent, date_projects[i].stamps_number));
//        stamps_number_list_list.Add(new List<Design_Object>(stamps_number_list));
//    }
//    else if (i == date_projects.Count - 1 && date_projects[i].id_design_object == date_projects[i - 1].id_design_object)

//    {
//        stamps_number_list.Add(new Design_Object(date_projects[i].id_design_object, date_projects[i].id_parent, date_projects[i].stamps_number));
//        stamps_number_list_list.Add(new List<Design_Object>(stamps_number_list));
//    }

//}


