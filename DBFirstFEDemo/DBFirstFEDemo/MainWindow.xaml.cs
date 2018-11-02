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

namespace DBFirstFEDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        Sep19CHNEntities contextObj = new Sep19CHNEntities();
        private void btn_Display_Click(object sender, RoutedEventArgs e)
        {
            //Sep19CHNEntities contextObj = new Sep19CHNEntities();
            if (txt_emplocation.Text != string.Empty)
            {

                var query = from Employee emp in contextObj.Employees       //DATA SOURCE
                            where emp.EmpID >= 1005 && emp.EmpLocation == txt_emplocation.Text
                            select emp;

                List<Employee> elist = new List<Employee>();
                elist = query.ToList<Employee>();

                if (elist.Count <= 0)
                    MessageBox.Show("No Records found");
                else
                {
                    dg_emp.ItemsSource = query.ToList();
                }
            }
            else
            { MessageBox.Show("Please Enter Location !"); }
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Employee empToBeDeleted; //= new Employee();

                int empID = Convert.ToInt32(txt_empID.Text);
                empToBeDeleted = contextObj.Employees.FirstOrDefault(emp => emp.EmpID == empID);

                if (empToBeDeleted != null)
                {
                    contextObj.Employees.Remove(empToBeDeleted);    // delete operation
                    contextObj.SaveChanges();                       //Save changes to database
                    MessageBox.Show("Employee Details deleted");

                }
                else throw new Exception("Delete could not be done !");
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}



