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
        from project";
            InitializeComponent();
          

            Sql_Requests sql_Requests = new Sql_Requests();
           
            List<Project> date_projects = sql_Requests.Select(selectQuery);
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
        from project";
            Sql_Requests sql_Requests = new Sql_Requests();
            List<Project> date_projects = sql_Requests.Select(selectQuery);

            List<string> list_combo = new List<string>();

            foreach (Project project in date_projects)
            {
                list_combo.Add(project.name_project.ToString());
            }
            
        }
        private void Combo_Box_loaded(object sender, SelectionChangedEventArgs e)
        {
           
             var combo = sender as ComboBox;
           
            List<string> list_combo = new List<string>();
            list_combo.Add("dfdff");
            list_combo.Add("fdfdf");
            combo.ItemsSource = list_combo;
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            
            Sql_Requests sql_Requests = new Sql_Requests();
            comboBox1.DataContext = comboBox1.SelectedItem;

            Project project = comboBox1.DataContext as Project;
            string selectQuery = $"SELECT id_design_object, name_object, code" +
                                 $" FROM project " +
                                 $" LEFT JOIN design_object ON project.id_executor = design_object.id_executor " +
                                 $" WHERE id_project = '{project.id_project}'";
            List<Project> date_projects = sql_Requests.Select(selectQuery);
            listBox.ItemsSource = date_projects;

        }
    }
} 
