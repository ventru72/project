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
       public List<Design_Object> name_object_l = new List<Design_Object>();
       public List<Design_Object> date_projects = new List<Design_Object>();
       //public int id_executor;
       //public int id_parent_object;
       //public int id_stamps;
       //public int id_project;
       //public int id_design_object;
       //public string stamps_short_name;
       //public int number_doc;
       public Combo_Box_Output_Date Combo_Box_Output = new Combo_Box_Output_Date();


        public MainWindow()
        {
            
            InitializeComponent();
         
           
           Sql_Requests sql_Requests = new Sql_Requests();
           void ComboBox_Select_Projects_and_Project_Change_and_Update_Project()
            {
                string selectQuery = $@"select 
                                 id_project,
                                 name_project,
                                 id_executor,
                                 cipher
                                 from project
                                 ORDER BY id_project ASC ";

                List<Project> date_projects = sql_Requests.Select_Project(selectQuery);
                List<Project> output_name_projects = new List<Project>();

               
                // comboBox_projects.DataContext = date_projects;
                foreach (Project project in date_projects)
                {
                    output_name_projects.Add(new Project(project.id_project, project.name_project.ToString()));
                }

                //dataGrid.ItemsSource = date_projects;
                select_projects.ItemsSource = output_name_projects;
                project_change.ItemsSource = output_name_projects;
                update_project.ItemsSource = output_name_projects;
            }
           void Combo_Box_Set_Project_and_Set_Executor_Object()
            {
                string selectQuery = $@" SELECT DISTINCT 
                                  id_executor,
                                 executor_short_name
                                 FROM guide_executors";

                List<Guide_Executors> dictionary = sql_Requests.Select_Guide_Executors(selectQuery);
                List<Guide_Executors> output_dictionary = new List<Guide_Executors>();
                foreach (Guide_Executors d in dictionary)
                {
                    output_dictionary.Add(new Guide_Executors(d.id_executor, d.executor_short_name));
                }
                //foreach (Guide_Executors d in dictionary)
                //{
                //    output_dictionary.Add(new Guide_Executors(d.id_executor, d.executor_short_name, d.id_stamps, d.stamps_short_name,
                //    d.id_type_documents, d.type_documents_short_name));
                //}
                set_project.ItemsSource = output_dictionary;
                set_executor_object.ItemsSource = output_dictionary;
            }
           void Combo_Box_Chois_Stamps()
            {
                string selectQuery = $@" SELECT DISTINCT 
                                  id_stamps,
                                 stamps_short_name
                                 FROM guide_stamps";

                List<Guide_Stamps> dictionary = sql_Requests.Select_Guide_Stamps(selectQuery);
                List<Guide_Stamps> output_dictionary = new List<Guide_Stamps>();
                foreach (Guide_Stamps d in dictionary)
                {
                    output_dictionary.Add(new Guide_Stamps(d.id_stamps, d.stamps_short_name));
                }
                //foreach (Guide_Executors d in dictionary)
                //{
                //    output_dictionary.Add(new Guide_Executors(d.id_executor, d.executor_short_name, d.id_stamps, d.stamps_short_name,
                //    d.id_type_documents, d.type_documents_short_name));
                //}
                chois_stamps.ItemsSource = output_dictionary;
                 
            }
           void Combo_Box_Chois_Design_Object_Set_Doc_And_Doc()
            {
                string selectQuery = $@" SELECT DISTINCT 
                                       id_design_object,
                                       name_object
                                       FROM design_object";

                List<Design_Object> dictionary = sql_Requests.Select_Object(selectQuery);
                List<Design_Object> output_dictionary = new List<Design_Object>();
                foreach (Design_Object d in dictionary)
                {
                    output_dictionary.Add(new Design_Object(d.id_design_object, d.name_object));
                }
                
                chois_design_object_set_doc.ItemsSource = output_dictionary;
                chois_design_object_doc.ItemsSource = output_dictionary;

            }
           void Combo_Box_Chois_Type_Documents_In_Documents()
            {
                string selectQuery = $@" SELECT DISTINCT 
                                  id_type_documents,
                                 type_documents_short_name
                                 FROM guide_type_documents";

                List<Guide_Type_Documents> dictionary = sql_Requests.Select_Guide_Type_Documents(selectQuery);
                List<Guide_Type_Documents> output_dictionary = new List<Guide_Type_Documents>();
                foreach (Guide_Type_Documents d in dictionary)
                {
                    output_dictionary.Add(new Guide_Type_Documents(d.id_type_documents, d.type_documents_short_name));
                }
                //foreach (Guide_Executors d in dictionary)
                //{
                //    output_dictionary.Add(new Guide_Executors(d.id_executor, d.executor_short_name, d.id_stamps, d.stamps_short_name,
                //    d.id_type_documents, d.type_documents_short_name));
                //}
                chois_type_documents_in_documents.ItemsSource = output_dictionary;

            }

            ComboBox_Select_Projects_and_Project_Change_and_Update_Project();
            Combo_Box_Set_Project_and_Set_Executor_Object();
            Combo_Box_Chois_Stamps();
            Combo_Box_Chois_Design_Object_Set_Doc_And_Doc();
            Combo_Box_Chois_Type_Documents_In_Documents();
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


            string selectQuery = string.Empty;
            Hyperlink link = (Hyperlink)sender;
            string tag = (string)link.Tag;
            Design_Object design_object_click = (Design_Object)link.DataContext;
            int disign_id = design_object_click.id_design_object_parent;
            Sql_Requests sql_Requests = new Sql_Requests();
            List<Design_Object> set_doc_chois_id_object = new List<Design_Object>();
            List<Design_Object> output_set_doc = new List<Design_Object>();


            void Get_Data_Chois_Id_Object(int id_design)
            {
                selectQuery = $"SELECT DISTINCT   full_code, set_documentation.stamps_full_name,  cipher, " +
                                              $" name_object, stamps_short_name, number_set_documentation,  " +
                                              $" stamps_short_name || number_set_documentation AS stamps_number, data_creation_set_docment, data_change_set_docment, " +
                                              $" executor_full_name" +
                                              $" FROM project" +
                                              $" LEFT JOIN guide_executors ON project.id_executor = guide_executors.id_executor " +
                                              $" LEFT JOIN design_object  ON project.id_project =design_object.id_project" +
                                              $" LEFT JOIN set_documentation ON design_object.id_design_object = set_documentation.id_design_object" +
                                              $" LEFT JOIN guide_stamps ON set_documentation.id_stamps = guide_stamps.id_stamps  " +
                                              $" LEFT JOIN documents ON set_documentation.id_set_documentation = documents.id_set_documentation  " +
                                              $" WHERE design_object.id_design_object = '{id_design}'" +
                                              $" ORDER BY  set_documentation.stamps_full_name ";
                set_doc_chois_id_object = sql_Requests.Select_Object(selectQuery);

                for (int j = 0; j < set_doc_chois_id_object.Count; j++)
                {
                    output_set_doc.Add(new Design_Object(set_doc_chois_id_object[j].full_code, set_doc_chois_id_object[j].stamps_full_name,
                    set_doc_chois_id_object[j].cipher, set_doc_chois_id_object[j].name_object, set_doc_chois_id_object[j].stamps_short_name,
                    set_doc_chois_id_object[j].number_set_documentation, set_doc_chois_id_object[j].data_creation_set_docment,
                    set_doc_chois_id_object[j].data_change_set_docment, set_doc_chois_id_object[j].executor_full_name));
                }
            }

            void Next_ID(int id_design)
            {
                selectQuery = $"SELECT id_design_object" +
                              $" FROM  design_object " +
                              $"WHERE id_parent = '{id_design}' ";
                List<Design_Object> count_id_object = new List<Design_Object>();
                count_id_object = sql_Requests.Select_Object(selectQuery);

                for (int i = 0; i < count_id_object.Count; i++)
                {
                    Get_Data_Chois_Id_Object(count_id_object[i].id_design_object);
                    Next_ID(count_id_object[i].id_design_object);

                }


            }

            Get_Data_Chois_Id_Object(disign_id);
            Next_ID(disign_id);

            dataGrid.ItemsSource = output_set_doc;

        }
        void Hyperlink_Click_Object_Name(object sender, RoutedEventArgs e)
        {
            string selectQuery = string.Empty;
            Hyperlink link = (Hyperlink)sender;
            string tag = (string)link.Tag;
            Design_Object design_object_click = (Design_Object)link.DataContext;
            int disign_id = design_object_click.id_design_object;
            Sql_Requests sql_Requests = new Sql_Requests();
            List< Design_Object> set_doc_chois_id_object = new List< Design_Object>();
            List<Design_Object> output_set_doc = new List<Design_Object>();
         
           
            void Get_Data_Chois_Id_Object (int id_design)
            {
                selectQuery = $"SELECT DISTINCT   full_code, set_documentation.stamps_full_name,  cipher, " +
                                              $" name_object, stamps_short_name, number_set_documentation, " +
                                              $" stamps_short_name || number_set_documentation AS stamps_number, data_creation_set_docment, data_change_set_docment, " +
                                              $" executor_full_name" +
                                              $" FROM project" +
                                              $" LEFT JOIN guide_executors ON project.id_executor = guide_executors.id_executor " +
                                              $" LEFT JOIN design_object  ON project.id_project =design_object.id_project" +
                                              $" LEFT JOIN set_documentation ON design_object.id_design_object = set_documentation.id_design_object" +
                                              $" LEFT JOIN guide_stamps ON set_documentation.id_stamps = guide_stamps.id_stamps  " +
                                              $" LEFT JOIN documents ON set_documentation.id_set_documentation = documents.id_set_documentation  " +
                                              $" WHERE design_object.id_design_object = '{id_design}'" +
                                              $" ORDER BY  set_documentation.stamps_full_name ";
                set_doc_chois_id_object = sql_Requests.Select_Object(selectQuery);

                for (int j = 0; j < set_doc_chois_id_object.Count; j++)
                {
                        output_set_doc.Add(new Design_Object(set_doc_chois_id_object[j].full_code, set_doc_chois_id_object[j].stamps_full_name,
                        set_doc_chois_id_object[j].cipher, set_doc_chois_id_object[j].name_object, set_doc_chois_id_object[j].stamps_short_name,
                        set_doc_chois_id_object[j].number_set_documentation, set_doc_chois_id_object[j].data_creation_set_docment,
                        set_doc_chois_id_object[j].data_change_set_docment, set_doc_chois_id_object[j].executor_full_name));
                }
            }
         
            void Next_ID(int id_design)
            {
                selectQuery = $"SELECT id_design_object" +
                              $" FROM  design_object " +
                              $"WHERE id_parent = '{id_design}' ";
                List<Design_Object> count_id_object = new List<Design_Object>();
                count_id_object = sql_Requests.Select_Object(selectQuery);
               
                for (int i = 0; i < count_id_object.Count; i++)
                {
                    Get_Data_Chois_Id_Object(count_id_object[i].id_design_object);
                    Next_ID(count_id_object[i].id_design_object);

                }
                 

            }

            Get_Data_Chois_Id_Object(disign_id);
            Next_ID(disign_id);
          

            dataGrid.ItemsSource = output_set_doc; 
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
            //int disign_id = set_doc_click.id_design_object;
            Sql_Requests sql_Requests = new Sql_Requests();
           
            List<Documents> date_right_table = new List<Documents>();
            ObservableCollection<Documents> itog_date_list = new ObservableCollection<Documents>();

            string selectQuery=   selectQuery = $"SELECT DISTINCT  type_documents_short_name, number_document, name_document, full_cipher_documents,"+
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

        private void select_projects_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
            name_object_l.Clear();
            Sql_Requests sql_Requests = new Sql_Requests();
            select_projects.DataContext = select_projects.SelectedItem;

            Project project = select_projects.DataContext as Project;
            Combo_Box_Output.Id_Project = project.id_project;
            //int id_project = project.id_project;


            string selectQuery = $"SELECT DISTINCT set_documentation.id_set_documentation, " +
                                 $"project.id_project, " +
                                 $"o.full_code, " +
                                 $"r.full_code," +
                                 $" set_documentation.stamps_full_name," +
                                 $" " +
                                 $" o.id_parent AS id_parent_o," +
                                 $" cipher, " +
                                 $"o.data_creation_design_object," +
                                 $" o.data_change_design_object, " +
                                 $" r.id_design_object, o.code," +
                                 $" o.name_object," +
                                 $" stamps_short_name,  " +
                                 $" stamps_short_name || number_set_documentation AS stamps_number," +
                                 $" data_creation_set_docment, data_change_set_docment, " +
                                 $" o.id_design_object AS id_design_object_parent," +
                                 $" executor_full_name," +
                                 $" r.code AS code_parent," +
                                 $" r.name_object AS name_object_parent," +
                                 $" r.id_parent AS id_parent_parent," +
                                 $" full_cipher_documents " +
                                 $" FROM project" +

                                 $" LEFT JOIN guide_executors ON project.id_executor = guide_executors.id_executor " +
                                 $" LEFT JOIN design_object AS o ON project.id_project = o.id_project" +
                                  $" LEFT  JOIN design_object AS r ON r.id_design_object = o.id_parent" +
                                 $" LEFT JOIN set_documentation ON r.id_design_object = set_documentation.id_design_object" +
                                 $" LEFT JOIN guide_stamps ON set_documentation.id_stamps = guide_stamps.id_stamps  " +
                                 $" LEFT JOIN documents ON set_documentation.id_set_documentation = documents.id_set_documentation  " +
                                
                                 $" WHERE project.id_project = '{project.id_project}'";

            date_projects = sql_Requests.Select_Object(selectQuery);


            foreach (Design_Object i in date_projects)
            {

                name_object_l.Add(new Design_Object(i.id_set_documentation, Combo_Box_Output.Id_Project, i.executor_full_name, i.data_creation_set_docment,
                     i.data_change_set_docment, i.cipher, i.id_design_object, i.code, i.name_object, i.stamps_number, i.id_parent,
                i.id_design_object_parent, i.code_parent, i.name_object_parent, i.stamps_number_parent, i.id_parent_parent, i.full_cipher_documents));

                //name_object_l.Add(new Design_Object(i.id_set_documentation, Combo_Box_Output.Id_Project, i.executor_full_name,
                //    i.id_design_object, i.name_object, i.id_parent,  i.id_design_object_parent,  i.name_object_parent,  i.id_parent_parent));

            }


            //Delete_Dublicat(name_object_l);

            //for (int i = 0; i < date_projects.Count; i++)
            //{
            //    date_projects[i].full_code
            //    name_object_l.Add(new Design_Object(date_projects[i].id_set_documentation, Combo_Box_Output.Id_Project,
            //        date_projects[i].executor_full_name, date_projects[i].data_creation_set_docment,
            //         date_projects[i].data_change_set_docment, date_projects[i].cipher, date_projects[i].id_design_object,
            //        date_projects[i].code, date_projects[i].name_object, date_projects[i].stamps_number, date_projects[i].id_parent,
            //   date_projects[i].id_design_object_parent, date_projects[i].code_parent, date_projects[i].name_object_parent,
            //  date_projects[i].stamps_number_parent, date_projects[i].id_parent_parent));

            //}

            listBox.ItemsSource = name_object_l;

        }
        private void DG1_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

            List<string> block_field_table = new List<string>()
            {
                "id_parent",
                 "id_executor",
                  "executor_short_name",
                  "data_change_design_object",
                   "id_type_documents",
                  "data_creation_design_object", 
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
        //кнопка добавление проекта
        private void add_data_project_Click(object sender, RoutedEventArgs e)
        {
            try

            { 
                Sql_Requests sql_Requests = new Sql_Requests();
                string selectQuery = $@"INSERT INTO project (cipher, name_project, id_executor  )" +
                                     $"VALUES (@cipher, @name_project, @id_executor)";
              
                sql_Requests.Execute_Project(selectQuery, new Project (ciher_project.Text, name_project.Text, Combo_Box_Output.Id_Executor));
                MessageBox.Show("Запись добавлена.");
                MainWindow mainWindow = new MainWindow();
                
            }
            catch (Exception ex)

            {
                MessageBox.Show("Ошибка при добавлении: " + ex.Message, " ",

                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }
        //кнопка редактирование проекта
        private void update_data_project_Click(object sender, RoutedEventArgs e)
        {
            try

            {
                Sql_Requests sql_Requests = new Sql_Requests();
                string selectQuery = $@"UPDATE  project set cipher = @cipher, name_project = @name_project, id_executor = @id_executor ";

                sql_Requests.Execute_Project(selectQuery, new Project(ciher_project.Text, name_project.Text, Combo_Box_Output.Id_Executor));
                MessageBox.Show("Запись отредактирована.");
            }
            catch (Exception ex)

            {
                MessageBox.Show("Ошибка при добавлении: " + ex.Message, " ",

                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        void executor_set_project_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
            set_project.DataContext = set_project.SelectedItem;
            Guide_Executors dictionary_executor = (Guide_Executors)set_project.DataContext;
            Combo_Box_Output.Id_Executor = dictionary_executor.id_executor;
           
        }
        void executor_object_set_project_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            set_executor_object.DataContext = set_executor_object.SelectedItem;
            Guide_Executors dictionary_executor_object = (Guide_Executors)set_executor_object.DataContext;
            Combo_Box_Output.Id_Executor = dictionary_executor_object.id_executor;
        }
        void project_change_in_design_object_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Sql_Requests sql_Requests = new Sql_Requests();
            project_change.DataContext = project_change.SelectedItem;
            Project project = (Project)project_change.DataContext;
            Combo_Box_Output.Id_Project = project.id_project;
            string selectQuery = $@"SELECT 
                                 name_object,
                                 id_parent,
                                 id_design_object
                                 FROM design_object
                                 LEFT JOIN project ON design_object.id_project = project.id_project
                                 WHERE project.id_project = '{Combo_Box_Output.Id_Project}'  
                                 ORDER BY name_object ASC ";

            List<Design_Object> parent_object = sql_Requests.Select_Object(selectQuery);
            ObservableCollection<Project> output_parent_object = new ObservableCollection <Project>();
            output_parent_object.Add(new Design_Object("Пусто", 0, 0));
            foreach (Design_Object o in parent_object)
            {
                output_parent_object.Add( new Design_Object(o.name_object, o.id_parent, o.id_design_object));
            }
            
            id_parent.ItemsSource = output_parent_object;
        }
        void chois_parent_design_object_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try

            {
                id_parent.DataContext = id_parent.SelectedItem;
                if ((Design_Object)id_parent.DataContext == null)
                {
                    Design_Object dictionary_id_parent_null = new Design_Object("Пусо", 0, 0);
                    Combo_Box_Output.Id_Parent_Object = dictionary_id_parent_null.id_parent;
                }
                else
                {
                    Design_Object dictionary_id_parent = (Design_Object)id_parent.DataContext;

                    Combo_Box_Output.Id_Parent_Object = dictionary_id_parent.id_design_object;
                }
                    
                //id_parent_object = dictionary_id_parent.id_parent;
            }
            catch (Exception ex)

            {
                MessageBox.Show("Ошибка:" + ex.Message, " ",

                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }
        private void add_data_object_Click(object sender, RoutedEventArgs e)
        {
            try

            {
                Sql_Requests sql_Requests = new Sql_Requests();
                List<Design_Object> parent_object_code = new List<Design_Object>();
                string selectQuery = string.Empty;
                string output_parent_object_code = string.Empty;
                if (Combo_Box_Output.Id_Parent_Object != 0)
                {
                    selectQuery = $@"SELECT 
                                 full_code
                                 FROM design_object
                                 WHERE design_object.id_design_object = '{Combo_Box_Output.Id_Parent_Object}'";
                     parent_object_code = sql_Requests.Select_Object(selectQuery);
                    
                    foreach (Design_Object o in parent_object_code)
                    {
                        output_parent_object_code = o.full_code + "." + cod.Text;
                    }
                    selectQuery = $@"INSERT INTO design_object (code, name_object, id_parent, data_creation_design_object,
                                             data_change_design_object, id_executor, id_project, full_code) 
                                            VALUES (@code, @name_object, @id_parent, @data_creation_design_object,
                                             @data_change_design_object, @id_executor, @id_project, @full_code)";
                    sql_Requests.Execute_Project(selectQuery, new Design_Object(cod.Text, name_object.Text, Combo_Box_Output.Id_Parent_Object, DateTime.Now,
                   DateTime.Now, Combo_Box_Output.Id_Executor, Combo_Box_Output.Id_Project, output_parent_object_code));
                }
                   else
                   {
                    output_parent_object_code = cod.Text;
                    selectQuery = $@"INSERT INTO design_object (code, name_object, data_creation_design_object,
                                             data_change_design_object, id_executor, id_project, full_code) 
                                            VALUES (@code, @name_object,  @data_creation_design_object,
                                             @data_change_design_object, @id_executor, @id_project, @full_code)";
                    sql_Requests.Execute_Project(selectQuery, new Design_Object(cod.Text, name_object.Text,  DateTime.Now,
                   DateTime.Now, Combo_Box_Output.Id_Executor, Combo_Box_Output.Id_Project, output_parent_object_code));
                }
                
                
                MessageBox.Show("Запись добавлена.");
            }
            catch (Exception ex)

            {
                MessageBox.Show("Ошибка при добавлении: " + ex.Message, " ",

                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        void chois_design_object_set_doc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            chois_design_object_set_doc.DataContext = chois_design_object_set_doc.SelectedItem;
            Design_Object dictionary_design_object = (Design_Object)chois_design_object_set_doc.DataContext;
            Combo_Box_Output.Id_Design_Object = dictionary_design_object.id_design_object;
            //id_design_object = dictionary_design_object.id_design_object;
        }
         void chois_stamps_set_doc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try

            {
                chois_stamps.DataContext = chois_stamps.SelectedItem;
                Guide_Stamps dictionary_id_stamps = (Guide_Stamps)chois_stamps.DataContext;
                Combo_Box_Output.Id_Stamps = dictionary_id_stamps.id_stamps;
                Combo_Box_Output.Chois_Stamps_CB = dictionary_id_stamps.stamps_short_name;
            }
            catch (Exception ex)

            {
                MessageBox.Show("Ошибка:" + ex.Message, " ",

                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }

           
            //stamps_short_name = dictionary_id_stamps.stamps_short_name;
        }
        private void add_data_set_doc_Click(object sender, RoutedEventArgs e)
        {
            try

            {
                Sql_Requests sql_Requests = new Sql_Requests();

                string selectQuery = $@"SELECT 
                                 name_object,
                                 id_parent,
                                 code
                                 FROM design_object
                                 WHERE id_design_object = '{Combo_Box_Output.Id_Design_Object}'";

                List<Design_Object> parent_object = sql_Requests.Select_Object(selectQuery);
               int output_parent_object = 0;
               string outpunt_code = string.Empty;
                foreach (Design_Object o in parent_object)
                {
                    output_parent_object=  o.id_parent;
                    outpunt_code = o.code;
                }
               
               
                 selectQuery = $@"SELECT 
                                 full_code,
                                 cipher
                                 FROM design_object
                                 JOIN project ON project.id_project = design_object.id_project
                                 WHERE design_object.id_design_object = '{output_parent_object}'";



                List<Design_Object> parent_object_stamps_full_name = sql_Requests.Select_Object(selectQuery);
                string data1 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                
                string output_parent_object_code = string.Empty;
                foreach (Design_Object o in parent_object_stamps_full_name)
                {
                    output_parent_object_code = o.cipher + "-" + o.full_code + "." + outpunt_code + "-" + Combo_Box_Output.Chois_Stamps_CB + number_set_doc.Text;
                }
                
                selectQuery = $@"INSERT INTO set_documentation (number_set_documentation, data_creation_set_docment, data_change_set_docment,
                                            id_stamps, id_design_object, stamps_full_name) 
                                            VALUES (@number_set_documentation, @data_creation_set_docment, @data_change_set_docment,
                                           @id_stamps, @id_design_object, @stamps_full_name)";
                sql_Requests.Insert_Set_Documentation(selectQuery, new Set_Documentation(int.Parse(number_set_doc.Text), DateTime.Now, DateTime.Now, Combo_Box_Output.Id_Stamps,
                   Combo_Box_Output.Id_Design_Object, output_parent_object_code));

                MessageBox.Show("Запись добавлена.");
            }
            catch (Exception ex)

            {
                MessageBox.Show("Ошибка при добавлении: " + ex.Message, " ",

                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        void chois_type_documents_in_documents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try

            {
                chois_type_documents_in_documents.DataContext = chois_type_documents_in_documents.SelectedItem;
                Guide_Type_Documents dictionary_type_documents = (Guide_Type_Documents)chois_type_documents_in_documents.DataContext;
                Combo_Box_Output.Id_Type_Documents = dictionary_type_documents.id_type_documents;
                //id_executor = dictionary_executor.id_executor;
            }
            catch (Exception ex)

            {
                MessageBox.Show("Ошибка:" + ex.Message, " ",

                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
           
        }
        void chois_design_object_doc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try

            {
                chois_design_object_doc.DataContext = chois_design_object_doc.SelectedItem;
                Design_Object dictionary_design_object = (Design_Object)chois_design_object_doc.DataContext;
                Combo_Box_Output.Id_Design_Object = dictionary_design_object.id_design_object;
                Sql_Requests sql_Requests = new Sql_Requests();

                string selectQuery = $@"SELECT 
                                 set_documentation.id_set_documentation,
                                 number_set_documentation,
                                 guide_stamps.stamps_short_name
                                 FROM design_object
                                 LEFT JOIN set_documentation ON design_object.id_design_object = 
                                 set_documentation.id_design_object
                                 LEFT JOIN guide_stamps ON set_documentation.id_stamps = guide_stamps.id_stamps
                                 WHERE set_documentation.id_design_object = '{Combo_Box_Output.Id_Design_Object}'";

                List<Set_Documentation> stamps_name = sql_Requests.Select_Set_Documentation(selectQuery);
                ObservableCollection<Set_Documentation> output_stamps_name = new ObservableCollection<Set_Documentation>();

                foreach (Set_Documentation o in stamps_name)
                {
                    output_stamps_name.Add(new Set_Documentation(o.stamps_short_name + o.number_set_documentation, o.id_set_documentation));
                }
                chois_set_documentation.ItemsSource = output_stamps_name;
            }
            catch (Exception ex)

            {
                MessageBox.Show("Ошибка:" + ex.Message, " ",

                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
        }
        void chois_set_documentation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try

            {

                chois_set_documentation.DataContext = chois_set_documentation.SelectedItem;
                Set_Documentation set_documentation = (Set_Documentation)chois_set_documentation.DataContext;
                Combo_Box_Output.Id_Set_Documentation = set_documentation.id_set_documentation;
            }
            catch (Exception ex)

            {
                MessageBox.Show("Ошибка:" + ex.Message, " ",

                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void add_data_doc_Click(object sender, RoutedEventArgs e)
        {
            try

            {
                Sql_Requests sql_Requests = new Sql_Requests();

                string selectQuery = $@"SELECT 
                                 stamps_full_name
                                 FROM set_documentation
                                 WHERE id_set_documentation = '{Combo_Box_Output.Id_Set_Documentation}'";

                List<Set_Documentation> stamps_full_name = sql_Requests.Select_Set_Documentation(selectQuery);
              
                string full_cipher_documents = string.Empty;
                foreach (Set_Documentation o in stamps_full_name)
                {
                    full_cipher_documents = o.stamps_full_name + "-" + number_doc.Text;
                    
                }
                MainWindow mainWindow= new MainWindow();

                List<Design_Object> parent_object_stamps_full_name = sql_Requests.Select_Object(selectQuery);
                string data1 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                string output_parent_object_code = string.Empty;
                //foreach (Design_Object o in parent_object_stamps_full_name)
                //{
                //    output_parent_object_code = o.cipher + "-" + o.full_code + "." + outpunt_code + "-" + Combo_Box_Output.Chois_Stamps_CB + number_set_doc.Text;
                //}

                selectQuery = $@"INSERT INTO documents (number_document, id_type_documents, data_creation_document,
                                            data_change_document, name_document, id_set_documentation, full_cipher_documents) 
                                            VALUES (@number_document, @id_type_documents, @data_creation_document,
                                            @data_change_document, @name_document, @id_set_documentation, @full_cipher_documents)";
                sql_Requests.Insert_Document(selectQuery, new Documents(int.Parse(number_doc.Text), Combo_Box_Output.Id_Type_Documents,
                    name_doc.Text, DateTime.Now, DateTime.Now, Combo_Box_Output.Id_Set_Documentation, full_cipher_documents));
                   

                MessageBox.Show("Запись добавлена.");
            }
            catch (Exception ex)

            {
                MessageBox.Show("Ошибка при добавлении: " + ex.Message, " ",

                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void add_data_guide_executors_Click(object sender, RoutedEventArgs e)
        {
            try

            {
                Sql_Requests sql_Requests = new Sql_Requests();
                string selectQuery = $@"INSERT INTO guide_executors (executor_short_name, executor_full_name) 
                                            VALUES (@executor_short_name, @executor_full_name)";
                sql_Requests.Insert_Guide_Executors(selectQuery, new Guide_Executors(executor_short_name.Text, executor_full_name.Text));
                MessageBox.Show("Запись добавлена.");
                MainWindow mainWindow = new MainWindow();
            }

            catch (Exception ex)

            {
                MessageBox.Show("Ошибка при добавлении: " + ex.Message, " ",

                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void add_data_guide_stamps_Click(object sender, RoutedEventArgs e)
        {
            try

            {
                Sql_Requests sql_Requests = new Sql_Requests();
                string selectQuery = $@"INSERT INTO guide_stamps (stamps_short_name, stamps_full_name) 
                                            VALUES (@stamps_short_name, @stamps_full_name)";
                sql_Requests.Insert_Guide_Stamps(selectQuery, new Guide_Stamps(stamps_short_name.Text, stamps_full_name.Text));
                MessageBox.Show("Запись добавлена.");
                MainWindow mainWindow = new MainWindow();
            }
            catch (Exception ex)

            {
                MessageBox.Show("Ошибка при добавлении: " + ex.Message, " ",

                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void add_data_guide_type_documents_Click(object sender, RoutedEventArgs e)
        {
            try

            {
                Sql_Requests sql_Requests = new Sql_Requests();
                string selectQuery = $@"INSERT INTO guide_type_documents (type_documents_short_name, type_documents_full_name) 
                                            VALUES (@type_documents_short_name, @type_documents_full_name)";
                sql_Requests.Insert_Guide_Type_Documents(selectQuery, new Guide_Type_Documents(type_documents_short_name.Text, type_documents_full_name.Text));
                MessageBox.Show("Запись добавлена.");
                MainWindow mainWindow = new MainWindow();
            }
            catch (Exception ex)

            {
                MessageBox.Show("Ошибка при добавлении: " + ex.Message, " ",

                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        //Выбор проекта для редактирования
        void update_project_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            update_project.DataContext = update_project.SelectedItem;
            Project project = (Project)update_project.DataContext;
            Combo_Box_Output.Id_Update_Project = project.id_project;
            
        }
        private void update_doc_set_Click(object sender, RoutedEventArgs e)
        {
            try

            {
                Sql_Requests sql_Requests = new Sql_Requests();

                string selectQuery = $@"SELECT 
                                 stamps_full_name
                                 FROM set_documentation
                                 WHERE id_set_documentation = '{Combo_Box_Output.Id_Set_Documentation}'";

                List<Set_Documentation> stamps_full_name = sql_Requests.Select_Set_Documentation(selectQuery);

                string full_cipher_documents = string.Empty;
                foreach (Set_Documentation o in stamps_full_name)
                {
                    full_cipher_documents = o.stamps_full_name + "-" + number_doc.Text;

                }

                List<Design_Object> parent_object_stamps_full_name = sql_Requests.Select_Object(selectQuery);
                string data1 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                string output_parent_object_code = string.Empty;
                //foreach (Design_Object o in parent_object_stamps_full_name)
                //{
                //    output_parent_object_code = o.cipher + "-" + o.full_code + "." + outpunt_code + "-" + Combo_Box_Output.Chois_Stamps_CB + number_set_doc.Text;
                //}

                selectQuery = $@"INSERT INTO documents (number_document, id_type_documents, data_creation_document,
                                            data_change_document, name_document, id_set_documentation, full_cipher_documents) 
                                            VALUES (@number_document, @id_type_documents, @data_creation_document,
                                            @data_change_document, @name_document, @id_set_documentation, @full_cipher_documents)";
                sql_Requests.Insert_Document(selectQuery, new Documents(int.Parse(number_doc.Text), Combo_Box_Output.Id_Type_Documents,
                    name_doc.Text, DateTime.Now, DateTime.Now, Combo_Box_Output.Id_Set_Documentation, full_cipher_documents));


                MessageBox.Show("Запись добавлена.");
            }
            catch (Exception ex)

            {
                MessageBox.Show("Ошибка при добавлении: " + ex.Message, " ",

                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}




