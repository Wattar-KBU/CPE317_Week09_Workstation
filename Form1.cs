using cpe317_week09;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CPE317_Week09_Workstation
{
    public partial class Form1 : Form
    {
        //هنا قمنا بإنشاء ليست عادية ولكن هذه الليست النوع تبعها هوة نوع طالب في عندها أسم وفي عندها أشياء أخرى للطالب 
        private List<Student> Students = new List<Student>();
        public Form1()
        {
            InitializeComponent();
            Students.Add( new Student { Name = "Muhammed vattar", Department = "CENG", GPA = 3.5 });
            Students.Add(new Student {Name = "zaid wattar", Department ="CENG", GPA =3.7});

            bindingSource1.DataSource = Students; //Connect
            dataGridView1.DataSource = bindingSource1; //Connect
            bindingNavigator1.BindingSource = bindingSource1; //    

            txtName.DataBindings.Add("Text", bindingSource1, "Name", true);
            txtDepartment.DataBindings.Add("Text", bindingSource1, "Department", true);
            txtGPA.DataBindings.Add("Text", bindingSource1, "GPA",true);
            txtMail.DataBindings.Add("Text", bindingSource1, "Email", true);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void txtGPA_Validating(object sender, CancelEventArgs e)
        {
            if (!double.TryParse(txtGPA.Text, out double gpa) || gpa < 0 || gpa > 4)
            {
                errorProvider1.SetError(txtGPA, "Please enter a valid GPA between 0 and 4.");
                e.Cancel = true;
            }
            else 
            {
                errorProvider1.SetError(txtGPA, "");
            }
            
        }

        private void txtName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                errorProvider1.SetError(txtName, "Name cannot be empty.");
                e.Cancel = true;
            }
            else 
            {
                errorProvider1.SetError(txtName, "");
                e.Cancel = false;
            }
        }

        private void txtDepartment_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDepartment.Text))
            {
                errorProvider1.SetError(txtDepartment, "Department cannot be empty");
                e.Cancel = true;
            }
            else 
            {
                errorProvider1.SetError(txtDepartment, "");
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(searchText))
            {
                bindingSource1.DataSource = Students;
            }
            else
            {
                var filtered = Students
                    .Where(s => !string.IsNullOrEmpty(s.Name) && s.Name.ToLower().Contains(searchText))
                    .ToList();

                bindingSource1.DataSource = filtered; 
            }
        }
    }
}
