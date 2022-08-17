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

            string selectQuery = $"SELECT cipher, name_object, design_object.stamps_number" +
                                $" FROM project " +
                                $" LEFT JOIN design_object ON project.id_executor =" +
                                $" design_object.id_executor " +
                                //$" LEFT JOIN set_documentation ON design_object.stamps_number =" +
                                //$" set_documentation.stamps_number " +
                                $" WHERE name_object = '{selected_object_name}'";
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

            string selectQuery = $"SELECT o.id_design_object, o.code, o.name_object, o.stamps_number, o.id_parent, " +
                                 $" r.id_design_object AS id_design_object_parent," +
                                 $" r.code AS code_parent," +
                                 $" r.name_object AS name_object_parent," +
                                 $" r.stamps_number AS stamps_number_parent," +
                                 $" r.id_parent AS id_parent_parent" +
                                 $" FROM project " +
                                 $" LEFT JOIN design_object AS o ON project.id_executor = o.id_executor " +
                                 $" LEFT JOIN design_object AS r ON o.id_design_object = r.id_parent" +
                                 $" WHERE id_project = '{project.id_project}'";

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

            ObservableCollection<Design_Object> name_object_l = new ObservableCollection<Design_Object>();
            
            foreach (Design_Object i in date_projects)
            {
                name_object_l.Add(new Design_Object(i.id_design_object, i.code, i.name_object,  i.stamps_number, i.id_parent,
                   i.id_design_object_parent, i.code_parent, i.name_object_parent, i.stamps_number_parent, i.id_parent_parent));
                 
            }
           //for (int i = 0; i < name_object_l.Count; i++)
           // {
           //     while (name_object_l[i].id_parent > 0)
           //     {
           //         parent_design_object = sql_Requests.Select_Object(Requests_Parent(name_object_l[i].id_parent));
           //     }
           // }
            listBox.ItemsSource = name_object_l;

        }
    }
} 
