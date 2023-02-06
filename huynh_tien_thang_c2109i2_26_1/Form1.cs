using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace huynh_tien_thang_c2109i2_26_1
{
    public partial class Form1 : Form
    {
        private List<TblStudent> studentCreate1;
        private List<TblStudent> studentsCreate2;
        private List<TblStudent> studentsCreate3;
        private TblCourse courseCreate;
        //Dùng để xét xem phải trường hợp search ko 
        private bool studentCourseSearch = false;
        // w 2 thằng này dùng để tham chiếu để xóa cái cũ 
        private string ghet = " ";
        private string ghet2 = " ";
        //ông này dùng để lưa trữ dữ liệu trc khi edit của ông kia ;
        private int cellCheck = 0;
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection();
        SqlCommand command = new SqlCommand();
        SqlDataAdapter apdater = new SqlDataAdapter();
        DataSet ds = new DataSet();


        private void Form1_Load(object sender, EventArgs e)
        {

            // panelCourseCreate.Visible= false;
             loadStudent();
             loadCourse();
             loadCourseCreate();
          
            panelExam.Visible = false;
            panelCourseCreate.Visible = false;
            panelCourse.Visible = false;
            panelStudentDetail.Visible = false;

          
            

        }
        private void loadStudent()
        {
            studentTextClear();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["thang"].ConnectionString;
            command.Connection = con;
            command.CommandText = "Select StuId as [Id],StuName as [Name] , StuPass as [Pass]," +
                "StuEmail as Email,StuPhone as [Phone],deptId as [dept],StuAdress as [Adress] from TblStudent ;";
            command.CommandType = CommandType.Text;
            apdater.SelectCommand = command;
            ds.Tables.Clear();
            apdater.Fill(ds);

            studentSource.DataSource = ds.Tables[0];
            studentGridView.DataSource = studentSource;
            studentNavigator.BindingSource = studentSource;           
            txtStudentId.DataBindings.Add("text", studentSource, "Id", true, DataSourceUpdateMode.OnPropertyChanged);
            txtStudentName.DataBindings.Add("text", studentSource, "Name", true, DataSourceUpdateMode.OnPropertyChanged);
            txtStudentPhone.DataBindings.Add("text", studentSource, "Phone", true, DataSourceUpdateMode.OnPropertyChanged);
            txtStudentEmail.DataBindings.Add("text", studentSource, "Email", true, DataSourceUpdateMode.OnPropertyChanged);
            txtStudentAdress.DataBindings.Add("text", studentSource, "Adress", true, DataSourceUpdateMode.OnPropertyChanged);
            txtStudentPass.DataBindings.Add("text", studentSource, "Pass", true, DataSourceUpdateMode.OnPropertyChanged);
            txtStudentDeptId.DataBindings.Add("text", studentSource, "Dept", true, DataSourceUpdateMode.OnPropertyChanged);
            studentGridView.Columns[0].Width = 50;
            studentGridView.Columns[1].Width = 150;
            studentGridView.Columns[3].Width = 200;
            studentGridView.Columns[4].Width = 100;
            studentGridView.Columns[5].Width = 50;

        }
        
        private void loadCourse()
        {
            courseTextClear();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["thang"].ConnectionString;
            command.Connection = con;
            command.CommandText = "SELECT TblCourse.CouId as [Id], TblCourse.CouName as [Name], TblCourse.CouSemester as [Semester], TblExam.ExamName as [Exam] , TblExam.MarkPass as [Mark Pass], TblExam.ExamDate as  [Date],COUNT(TblCourse.CouId) as [Student]  FROM     TblExam INNER JOIN  TblCourse ON TblExam.CouId = TblCourse.CouId Group By TblCourse.CouId, TblCourse.CouName, TblCourse.CouSemester, TblExam.ExamName, TblExam.MarkPass, TblExam.ExamDate ;";
            var a = "";
            command.CommandType = CommandType.Text;
            apdater.SelectCommand = command;
            ds.Tables.Clear();
            apdater.Fill(ds);
          
                courseSource.DataSource = ds.Tables[0];
              
                courseGridView.DataSource = courseSource;
                courseNavigator.BindingSource = courseSource;


            courseGridView.Columns[1].Width = 200;
            txtCourseId.DataBindings.Add("text", courseSource, "Id", true, DataSourceUpdateMode.OnPropertyChanged);
            txtCourseName.DataBindings.Add("text", courseSource, "Name", true, DataSourceUpdateMode.OnPropertyChanged);
            txtCourseSemester.DataBindings.Add("text", courseSource, "Semester", true, DataSourceUpdateMode.OnPropertyChanged);
            txtCourseExamName.DataBindings.Add("text", courseSource, "Exam", true, DataSourceUpdateMode.OnPropertyChanged);
            txtCourseExamMarkPass.DataBindings.Add("text", courseSource, "Mark Pass", true, DataSourceUpdateMode.OnPropertyChanged);
            dtpCourseExamDate.DataBindings.Add("Value", courseSource, "Date", true, DataSourceUpdateMode.OnPropertyChanged); ;
            txtCourseExamStudent.DataBindings.Add("text", courseSource, "Student", true, DataSourceUpdateMode.OnPropertyChanged);



            // CouName,CouSemester
        }
        private async void loadCourseCreate()
        {
            using (var ef = new SinhVienEntities())
            {
                txtCourseCreateName.Text = " ";
                txtExamName.Text = " ";
                txtMarkPass.Text = "5";
                lbCourseCreate.SelectedIndex = 0;
                dtpCourseCreate.Value = DateTime.Now;
                dtpExamDate.Value= DateTime.Now;
                //
                txtStudentCourseSearch.Text = " ";
                studentCreate1 = new List<TblStudent>();
                studentsCreate2 = ef.TblStudents.ToList();
                studentCourseSearch = false;
                loadStudentCreate();
             
            }
        }

        private async void loadStudentCreate()
        {
            using (var ef = new SinhVienEntities())
            {
                txtStudentCreateId1.DataBindings.Clear();
                txtStudentCreateId2.DataBindings.Clear();
                txtStudenCreateCount.Text =studentCreate1.Count()+ " ";

                courseCreateSource1.DataSource = studentCreate1.Select(sta => new
                {
                    Id = sta.StuId,
                    Name = sta.StuName,
                    Pass = sta.StuPass,
                    Phone = sta.StuPhone,
                    lblStudentEmail = sta.StuEmail,
                    Adress = sta.StuAdress,
                    Dept = sta.deptId

                }).ToList();
                courseCreateSource2.DataSource = studentsCreate2.Select(sta => new
                {
                    Id = sta.StuId,
                    Name = sta.StuName,
                    Pass = sta.StuPass,
                    Phone = sta.StuPhone,
                    lblStudentEmail = sta.StuEmail,
                    Adress = sta.StuAdress,
                    Dept = sta.deptId

                }).ToList();

                CourseCreateGridView1.DataSource = courseCreateSource1;
                CourseCreateGridView2.DataSource = courseCreateSource2;
                courseCreateNavigator1.BindingSource = courseCreateSource1;
                courseCreateNavigator2.BindingSource = courseCreateSource2;
                txtStudentCreateId1.DataBindings.Add("text", courseCreateSource1, "Id", true, DataSourceUpdateMode.OnPropertyChanged);
                txtStudentCreateId2.DataBindings.Add("text", courseCreateSource2, "Id", true, DataSourceUpdateMode.OnPropertyChanged);
            }
        }
        private void loadStudentCreateSearch()
        {
            using (var ef = new SinhVienEntities())
            {
                txtStudentCreateId1.DataBindings.Clear();
                txtStudentCreateId2.DataBindings.Clear();
                txtStudenCreateCount.Text = studentCreate1.Count() + " ";
                var textStu = txtStudentCourseSearch.Text;
                courseCreateSource1.DataSource = studentCreate1.Select(sta => new
                {
                    Id = sta.StuId,
                    Name = sta.StuName,
                    Pass = sta.StuPass,
                    Phone = sta.StuPhone,
                    lblStudentEmail = sta.StuEmail,
                    Adress = sta.StuAdress,
                    Dept = sta.deptId

                }).ToList();
                courseCreateSource2.DataSource = studentsCreate2
                    .Where(stu =>
               stu.StuName.Contains(textStu) || stu.StuPass.Contains(textStu) ||
               stu.StuEmail.Contains(textStu) || stu.StuPhone.Contains(textStu) ||
               stu.StuId.ToString().Contains(textStu) || stu.StuAdress.Contains(textStu)
                )
                    .Select(sta => new
                {
                    Id = sta.StuId,
                    Name = sta.StuName,
                    Pass = sta.StuPass,
                    Phone = sta.StuPhone,
                    lblStudentEmail = sta.StuEmail,
                    Adress = sta.StuAdress,
                    Dept = sta.deptId

                }).ToList();

                CourseCreateGridView1.DataSource = courseCreateSource1;
                CourseCreateGridView2.DataSource = courseCreateSource2;
                courseCreateNavigator1.BindingSource = courseCreateSource1;
                courseCreateNavigator2.BindingSource = courseCreateSource2;
                txtStudentCreateId1.DataBindings.Add("text", courseCreateSource1, "Id", true, DataSourceUpdateMode.OnPropertyChanged);
                txtStudentCreateId2.DataBindings.Add("text", courseCreateSource2, "Id", true, DataSourceUpdateMode.OnPropertyChanged);
            }
        }
        

        private void loadStudentSearch(string search)
        {
            studentTextClear();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["thang"].ConnectionString;
            command.Connection = con;
            command.CommandText = $" Select StuId as [Id],StuName as [Name] , StuPass as [Pass],StuEmail as Email,StuPhone as [Phone],deptId as [dept], StuAdress as [Adress] from TblStudent    WHERE StuId like N'%{search}%' or StuName like N'%{search}%' or StuPass like N'%{search}%' or StuAdress like N'%{search}%' or StuEmail like N'%{search}%' or StuPhone like N'%{search}%' or deptId like N'%{search}%'  ;";
            command.CommandType = CommandType.Text;
            apdater.SelectCommand = command;
            ds.Tables.Clear();
            apdater.Fill(ds);

            studentSource.DataSource = ds.Tables[0];
            studentGridView.DataSource = studentSource;
            studentNavigator.BindingSource = studentSource;
            txtStudentId.DataBindings.Add("text", studentSource, "Id", true, DataSourceUpdateMode.OnPropertyChanged);
            txtStudentName.DataBindings.Add("text", studentSource, "Name", true, DataSourceUpdateMode.OnPropertyChanged);
            txtStudentPhone.DataBindings.Add("text", studentSource, "Phone", true, DataSourceUpdateMode.OnPropertyChanged);
            txtStudentEmail.DataBindings.Add("text", studentSource, "Email", true, DataSourceUpdateMode.OnPropertyChanged);
            txtStudentAdress.DataBindings.Add("text", studentSource, "Adress", true, DataSourceUpdateMode.OnPropertyChanged);
            txtStudentPass.DataBindings.Add("text", studentSource, "Pass", true, DataSourceUpdateMode.OnPropertyChanged);
            txtStudentDeptId.DataBindings.Add("text", studentSource, "Dept", true, DataSourceUpdateMode.OnPropertyChanged);
            studentGridView.Columns[0].Width = 50;
            studentGridView.Columns[1].Width = 150;
            studentGridView.Columns[3].Width = 200;
            studentGridView.Columns[4].Width = 100;
            studentGridView.Columns[5].Width = 50;
        }
        private void loadExam(int Id)
        {
            txtIdExam.DataBindings.Clear();
            txtNameExam.DataBindings.Clear();
            txtPassExam.DataBindings.Clear();
            txtAdressExam.DataBindings.Clear();
            txtPhoneExam.DataBindings.Clear();
            txtGmailExam.DataBindings.Clear();
            txtDeptExam.DataBindings.Clear();
            txtMarkExam.DataBindings.Clear();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["thang"].ConnectionString;
            command.Connection = con;
            command.CommandText = $"SELECT TblStudent.StuId as Id ,TblStudent.StuName as [Name], TblExam.ExamMark as [Điểm], TblStudent.StuPhone as [Phone] ,TblStudent.StuEmail as [Gmail], TblStudent.StuPass as Pass, TblStudent.DeptId as [Dept],TblStudent.StuAdress as Adress FROM TblExam INNER JOIN TblStudent ON TblExam.StuId = TblStudent.StuId Where CouId= {Id};";
            command.CommandType = CommandType.Text;
            apdater.SelectCommand = command;
            ds.Tables.Clear();
            apdater.Fill(ds);
            examSource.DataSource = ds.Tables[0];
            examGridView.DataSource = examSource;
            examNavigator.BindingSource = examSource;
            examGridView.Columns[2].Width = 50;
            examGridView.Columns[1].Width = 150;
            examGridView.Columns[4].Width = 200;
            examGridView.Columns[0].ReadOnly = true;
            examGridView.Columns[1].ReadOnly = true;
            examGridView.Columns[3].ReadOnly = true;
            examGridView.Columns[4].ReadOnly = true;
            examGridView.Columns[5].ReadOnly = true;
            examGridView.Columns[6].ReadOnly = true;
            examGridView.Columns[7].ReadOnly = true;

            txtIdExam.DataBindings.Add("text", examSource, "Id", true, DataSourceUpdateMode.OnPropertyChanged);
            txtNameExam.DataBindings.Add("text", examSource, "Name", true, DataSourceUpdateMode.OnPropertyChanged);
            txtPassExam.DataBindings.Add("text", examSource, "Pass", true, DataSourceUpdateMode.OnPropertyChanged);
            txtAdressExam.DataBindings.Add("text", examSource, "Adress", true, DataSourceUpdateMode.OnPropertyChanged);
            txtPhoneExam.DataBindings.Add("text", examSource, "Phone", true, DataSourceUpdateMode.OnPropertyChanged);
            txtGmailExam.DataBindings.Add("text", examSource, "Gmail", true, DataSourceUpdateMode.OnPropertyChanged);
            txtDeptExam.DataBindings.Add("text", examSource, "Dept", true, DataSourceUpdateMode.OnPropertyChanged);
            txtMarkExam.DataBindings.Add("text", examSource, "Điểm", true, DataSourceUpdateMode.OnPropertyChanged);


            //  txtIdExam.DataBindings.Add("text", examSource, "", true, DataSourceUpdateMode.OnPropertyChanged);
            //examGridView.Rows[0].Cells[0].Style.ForeColor = Color.Blue;
            //examGridView.Rows[2].Cells[2].ErrorText ="dfsafdsaf" ;
            // examGridView.Rows[2].Cells[2].ErrorText.Clear();
           // examGridView.CurrentCell = examGridView.Rows[2].Cells[2];



        }

        private void loadStudentDetail(int Id)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["thang"].ConnectionString;
            command.Connection = con;
            command.CommandText = $" SELECT TblCourse.CouName as [Course Name ], TblCourse.CouSemester as  [Semester],  TblExam.ExamName as [Exam Name] ,TblExam.ExamDate as [Date], TblExam.ExamMark as [Mark]  , TblExam.MarkPass as [Điểm Đậu],case  when (TblExam.ExamMark >= TblExam.MarkPass and DateDiff(day,GETDATE(),TblExam.ExamDate)<=0 )  then N'Đậu' when  (TblExam.ExamMark < TblExam.MarkPass and DateDiff(day,GETDATE(),TblExam.ExamDate)<=0 ) then N'Rớt'  else N'Chưa thi chưa rõ' end Result  FROM  TblExam INNER JOIN                  TblCourse ON TblExam.CouId = TblCourse.CouId WHERE TblExam.StuId = {Id}  ;";
            command.CommandType = CommandType.Text;
            apdater.SelectCommand = command;
            ds.Tables.Clear();
            apdater.Fill(ds);
            studentDetailSource.DataSource = ds.Tables[0];
            studentDetailGridView.DataSource = studentDetailSource;
            studentDetailNavigator.BindingSource = studentDetailSource;

            studentDetailGridView.Columns[0].Width = 180;
            studentDetailGridView.Columns[2].Width = 150;

        }

        private void studentGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {

        }
        private void studentTextClear() {
            txtStudentId.DataBindings.Clear();
            txtStudentName.DataBindings.Clear();
            txtStudentPhone.DataBindings.Clear();
            txtStudentEmail.DataBindings.Clear();
                txtStudentAdress.DataBindings.Clear();
            txtStudentPass.DataBindings.Clear();
            txtStudentDeptId.DataBindings.Clear();
        }
        private void courseTextClear()
        {
            txtCourseSearch.Text = "";
            txtCourseId.DataBindings.Clear();
            txtCourseName.DataBindings.Clear();
            txtCourseSemester.DataBindings.Clear();
            txtCourseExamName.DataBindings.Clear();
            txtCourseExamMarkPass.DataBindings.Clear();
            dtpCourseExamDate.DataBindings.Clear();
            txtCourseExamStudent.DataBindings.Clear();
        }
        private void courseCreateTextClear()
        {txtStudentCreateId1.DataBindings.Clear();
            txtStudentCreateId2.DataBindings.Clear();


        }
        private bool  checkStudentError()
        {
            //check = true thì có nghĩa là có vấn đề sai rồi ok 
            var check = false;
            studentError.Clear();
            //
            var text = "";
          
            text += !(txtStudentName.Text != null && txtStudentName.Text != "") ? "Nhập name đê ! \n" : "";
            if (text != "") { check = true;
                studentError.SetError(txtStudentName, text);
            }

            //
            text = "";
            text += !(txtStudentPhone.Text != null && txtStudentName.Text != "") ? "Nhập phone đê ! \n" : "";
            text += !(Regex.IsMatch(txtStudentPhone.Text.Trim(), helper.Regex.phone))?"Nhập phone sai kìa \n":"";
  
            if(text != "")
            {check = true;
             studentError.SetError(txtStudentPhone, text); }
            //
            text = "";
            text += !(Regex.IsMatch(txtStudentEmail.Text.Trim(), helper.Regex.gmail)) ? "Nhập gmail sai kìa \n" : "";
            if(text != "")
            {
                check = true;
                studentError.SetError(txtStudentEmail, text);
            }
            //
            text = "";
            text += !(txtStudentAdress.Text != null && txtStudentEmail.Text != "") ? "Nhập Adress đê \n" : "";
            if(text != "")
            {
                check = true;
                studentError.SetError(txtStudentAdress, text);
            }
            //
            text = "";
            text += !(txtStudentPass.Text != null && txtStudentPass.Text != "")?"Nhập pass đê ! \n":
                "";
            if(text != "")
            {
                check = true;
                studentError.SetError(txtStudentPass, text);
            }
            //
            text = "";
            //text += !(int.TryParse(txtStudentDeptId.Text,out var g)) ? "Nhập số đê" : "";
            if(text != "")
            {
                check = true;
                studentError.SetError(txtStudentDeptId, text);

            }
            
            return check;

        }
        private bool checkCourseError()
        {
            var check = false;
            var text = "";
            courseCreateError.Clear();
            //
            text = "";
            text += !(txtExamName.Text != null && txtExamName.Text != "") ? "Nhập Tên kiểm tra đê  name đê ! \n" : "";
            if (text != "")
            {
                check = true;
                studentError.SetError(txtExamName, text);
            }
            //
            text = "";
            text += !(txtCourseCreateName.Text != null && txtCourseCreateName.Text != "") ? "Nhập tên khóa học dùm cái ! \n" : "";
            if (text != "")
            {
                check = true;
                studentError.SetError(txtCourseCreateName, text);
            }
            //
            text = "";
             text += !(int.Parse(txtMarkPass.Text) >=3 && int.Parse(txtMarkPass.Text) <= 7) ? "Điểm đậu ko thể dưới 3 và trên 10 ! \n" : "";
            if (text != "")
            {
                check = true;
                studentError.SetError(txtMarkPass, text);
            }


            return check;
        }
        private async void btnAddStudent_Click(object sender, EventArgs e)
        {
            if (!(MessageBox.Show("Bạn muốn insert à !", "Insert", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)) return;

            if (checkStudentError())
            {
                MessageBox.Show("Nhập sai ở đâu kìa", "Error");
                return;
            }
          using (var ef = new SinhVienEntities())
            {
               
                if (await ef.TblStudents.FirstOrDefaultAsync(sta => sta.StuPhone == txtStudentPhone.Text) != null)
                {
                    MessageBox.Show("Phone Bị trùng rồi ", "Error");
                    studentError.SetError(txtStudentPhone, "Số hiện tại đã có rồi !");
                    return;
                }
                var stu = new TblStudent();

                stu.StuName = txtStudentName.Text;
                stu.StuPhone = txtStudentPhone.Text;
                stu.StuEmail = txtStudentEmail.Text;
                stu.StuAdress = txtStudentAdress.Text;
                stu.StuPass = txtStudentPass.Text;
                stu.deptId = int.Parse(txtStudentDeptId.Text);


                ef.TblStudents.Add(stu);
                
                await ef.SaveChangesAsync();
                studentTextClear();
                loadStudent();

              
                
                       //txtStudentId.DataBindings.Clear();
                       //txtStudentName.DataBindings.Clear();
                       //txtStudentPhone.DataBindings.Clear();
                       //txtStudentEmail.DataBindings.Clear();
                       //txtStudentAdress.DataBindings.Clear();
                       //txtStudentPass.Clear();
                       //txtStudentDeptId.Clear();
            }


        }
        private void CourseCreate()
        {
            using (var ef = new SinhVienEntities())
            {
                if (!(MessageBox.Show("Bạn muốn insert à !", "Insert", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)) return;

                if (checkCourseError())
                {
                    MessageBox.Show("Nhập sai ở đâu kìa  !", "Error");
                    return;
                }
                if (!(5 <= int.Parse(txtStudenCreateCount.Text) && int.Parse(txtStudenCreateCount.Text) <= 40))
                {

                    MessageBox.Show("Lớp học phải có ít nhất 5 học sinh   !", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    return;
                }
                var crs = new TblCourse();
                crs.CouName = txtCourseCreateName.Text;

                crs.CouSemester = ((lbCourseCreate.SelectedIndex == -1) ? lbCourseCreate.Items[0].ToString() : lbCourseCreate.Items[lbCourseCreate.SelectedIndex].ToString())
                    + "-" + dtpCourseCreate.Value.Year.ToString() + "";
                var ebj = ef.TblCourses.FirstOrDefault(sta => sta.CouName == crs.CouName && sta.CouSemester == crs.CouSemester);
                if (ebj != null)
                {
                    MessageBox.Show("Đã có môn này trong kỳ này năm này  !", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    return;
                }
                ef.TblCourses.Add(crs);
                ef.SaveChanges();
                courseCreate = ef.TblCourses.FirstOrDefault(sta => sta.CouName == crs.CouName && sta.CouSemester == crs.CouSemester);
                //
                var exe = new TblExam();
                exe.ExamMark = 0;
                exe.ExamDate = dtpExamDate.Value;
                exe.ExamName = txtExamName.Text;
                exe.MarkPass = int.Parse(txtMarkPass.Text);
                exe.CouId = courseCreate.CouId;
                exe.Comment = " ";
                studentCreate1.ForEach(student => {
                    var exa = exe;
                    exa.StuId = student.StuId;
                    ef.TblExams.Add(exa);
                    // tại sao cái ở đây phải save change ở đây
                    ef.SaveChanges();
                    Console.WriteLine(student.StuId);
                });

                txtCourseCreateName.Text = " ";
                txtExamName.Text = " ";
                txtMarkPass.Text = "5";
                lbCourseCreate.SelectedIndex = 0;
                dtpCourseCreate.Value = DateTime.Now;
                dtpExamDate.Value = DateTime.Now;



                MessageBox.Show("thêm thành công !", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
               
            }
        }
        private void CourseUpdate()
        {
            using (var ef = new SinhVienEntities())
            {
                if (!(MessageBox.Show("Bạn muốn Update à !", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)) return;

                if (checkCourseError())
                {
                    MessageBox.Show("Nhập sai ở đâu kìa  !", "Error");
                    return;
                }
                if (!(5 <= int.Parse(txtStudenCreateCount.Text) && int.Parse(txtStudenCreateCount.Text) <= 40))
                {

                    MessageBox.Show("Lớp học phải có ít nhất 5 học sinh   !", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    return;
                }
                var crs = new TblCourse();
                crs.CouName = txtCourseCreateName.Text;

                crs.CouSemester = ((lbCourseCreate.SelectedIndex == -1) ? lbCourseCreate.Items[0].ToString() : lbCourseCreate.Items[lbCourseCreate.SelectedIndex].ToString())
                    + "-" + dtpCourseCreate.Value.Year.ToString() + "";
                var ebj = ef.TblCourses.FirstOrDefault(sta => sta.CouName == crs.CouName && sta.CouSemester == crs.CouSemester && sta.CouId!=courseCreate.CouId);
                if (ebj != null)
                {
                    MessageBox.Show("Đã có môn này trong kỳ này năm này  !", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    return;
                }
                courseCreate = ef.TblCourses.FirstOrDefault(sta => sta.CouId==courseCreate.CouId);
                courseCreate.CouName=crs.CouName;
                courseCreate.CouSemester=crs.CouSemester;
                ef.SaveChanges();
                
                //
                var exe = new TblExam();
                
                exe.ExamDate = dtpExamDate.Value;
                exe.ExamName = txtExamName.Text;
                exe.MarkPass = int.Parse(txtMarkPass.Text);
                exe.CouId = courseCreate.CouId;
                exe.Comment = " ";
                ghet2 = " ";
                studentCreate1.ForEach(student => {
                  
                    if (!ghet.Contains(","+ student.StuId + ","))
                    {
                            var exa = exe;
                            exe.ExamMark = 0;
                            exa.StuId = student.StuId;
                            ef.TblExams.Add(exa);
                            // tại sao cái ở đây phải save change ở đây
                            ef.SaveChanges();
                        
                    }else
                    {
                        var hahah = ef.TblExams.FirstOrDefault(ex => ex.CouId == courseCreate.CouId && ex.StuId == student.StuId);


                        if (hahah != null)
                        {
                            hahah.ExamDate = exe.ExamDate;
                            hahah.ExamName = exe.ExamName;
                            hahah.MarkPass = exe.MarkPass;
                            ef.SaveChanges();
                        }
                    }
                    ghet2 += "," + student.StuId + ",";
                   
                });
                Console.WriteLine(ghet2 );
                Console.WriteLine(ghet);
                studentsCreate3.ForEach(students =>
                {
                       

                    if (!ghet2.Contains("," + students.StuId + ","))
                    {
                        var hahah = ef.TblExams.FirstOrDefault(ex => ex.CouId == courseCreate.CouId && ex.StuId == students.StuId);

                      
                        if (hahah != null)
                        {
                           ef.TblExams.Remove(hahah);
                            ef.SaveChanges();
                        }
                    }
                });
              



                MessageBox.Show("Update thành công ", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                loadCousrseUpdate(courseCreate.CouId);
            }
        }
        private void ExamUpdate(int StuId,int CouId,int examMark)
        {
            using (var ef = new SinhVienEntities())
            {
                var obj = ef.TblExams.FirstOrDefault(exm => exm.CouId == CouId && exm.StuId == StuId);
                if(obj!= null)
                {
                    obj.ExamMark = examMark;
                    ef.SaveChanges();
                }
            }
        }
        private  void btnCourseCreateAdd_Click(object sender, EventArgs e)
        {

            if (btnCourseCreateAdd.Text == "Add")
            {
                CourseCreate();

            }
            else
            {
                CourseUpdate();
            }
        }
        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            
            if (!(MessageBox.Show("Bạn muốn update à !", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)) return;

            if (checkStudentError())
            {
                MessageBox.Show("Nhập sai ở đâu kìa", "Error");
                return;
            }
            using (var ef = new SinhVienEntities())
            {
                
                var findId = Convert.ToInt32(txtStudentId.Text);
                var ibj = await ef.TblStudents.FirstOrDefaultAsync(sta => sta.StuPhone == txtStudentPhone.Text && sta.StuId !=findId);
                if (ibj != null)
                {
                    MessageBox.Show("Phone Bị trùng rồi ", "Error");
                    studentError.SetError(txtStudentPhone, "Số hiện tại đã có rồi !");
                    return;
                }
                var stu = await ef.TblStudents.FirstOrDefaultAsync(sta => sta.StuId == findId);
                if (stu != null)
                {
                    stu.StuName = txtStudentName.Text;
                    stu.StuPhone = txtStudentPhone.Text;
                    stu.StuEmail = txtStudentEmail.Text;
                    stu.StuAdress = txtStudentAdress.Text;
                    stu.StuPass = txtStudentPass.Text;
                    stu.deptId = int.Parse(txtStudentDeptId.Text);
                }
               await  ef.SaveChangesAsync();
                studentTextClear();
                loadStudent();

            }
        }
       
        private void btnStudentDelete_Click(object sender, EventArgs e)
        {
            if (!(MessageBox.Show("Bạn muốn delete thiệt  à !", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)) return;
            using (var ef = new SinhVienEntities())
            {
                var findId = Convert.ToInt32(txtStudentId.Text);

                var enj = ef.TblExams.Where(sas => sas.StuId == findId);
                if (enj != null)
                {
                    ef.TblExams.RemoveRange(enj);
                    ef.SaveChanges();
                }
                var ibj =  ef.TblStudents.FirstOrDefault(sta => sta.StuId==findId);
                if (ibj != null)
                {
                    ef.TblStudents.Remove(ibj);
                   ef.SaveChanges();
                   
                    loadStudent();
                }

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
          
            loadCourse();
        
        }

        private void btnCourseSearch_Click(object sender, EventArgs e)
        {
            var c = txtCourseSearch.Text;
            courseTextClear();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["thang"].ConnectionString;
            command.Connection = con;
            command.CommandText = $"SELECT TblCourse.CouId as [Id], TblCourse.CouName as [Name], TblCourse.CouSemester as [Semester], TblExam.ExamName as [Exam] , TblExam.MarkPass as [Mark Pass], TblExam.ExamDate as  [Date],COUNT(TblCourse.CouId) as [Student]  FROM     TblExam INNER JOIN  TblCourse ON TblExam.CouId = TblCourse.CouId Where TblCourse.CouName Like N'%{c}%' or TblExam.ExamName Like N'%{c}%' or TblCourse.CouSemester Like N'%{c}%' or Convert(NVARCHAR,TblExam.ExamDate,103) LIKE N'%{c}%' Group By TblCourse.CouId, TblCourse.CouName, TblCourse.CouSemester, TblExam.ExamName, TblExam.MarkPass, TblExam.ExamDate ;";
           
            command.CommandType = CommandType.Text;
            apdater.SelectCommand = command;
            ds.Tables.Clear();
            apdater.Fill(ds);

            courseSource.DataSource = ds.Tables[0];

            courseGridView.DataSource = courseSource;
            courseNavigator.BindingSource = courseSource;


            courseGridView.Columns[1].Width = 200;
            txtCourseId.DataBindings.Add("text", courseSource, "Id", true, DataSourceUpdateMode.OnPropertyChanged);
            txtCourseName.DataBindings.Add("text", courseSource, "Name", true, DataSourceUpdateMode.OnPropertyChanged);
            txtCourseSemester.DataBindings.Add("text", courseSource, "Semester", true, DataSourceUpdateMode.OnPropertyChanged);
            txtCourseExamName.DataBindings.Add("text", courseSource, "Exam", true, DataSourceUpdateMode.OnPropertyChanged);
            txtCourseExamMarkPass.DataBindings.Add("text", courseSource, "Mark Pass", true, DataSourceUpdateMode.OnPropertyChanged);
            dtpCourseExamDate.DataBindings.Add("Value", courseSource, "Date", true, DataSourceUpdateMode.OnPropertyChanged); ;
            txtCourseExamStudent.DataBindings.Add("text", courseSource, "Student", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private  void btnCourseDelete_Click(object sender, EventArgs e)
        {
            if (!(MessageBox.Show("Bạn muốn delete thiệt  à !", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)) return;
            using (var ef = new SinhVienEntities())
            {
                var ab = Convert.ToInt32(txtCourseId.Text);
               
                var obj =  ef.TblCourses.FirstOrDefault(crs => crs.CouId == ab);

                    if(obj != null)
                {
                    var chan = ef.TblExams.Where(exa => exa.CouId == ab);
                    foreach ( var exa in chan) {
                       
                    ef.TblExams.Remove(exa);
                     
                    }
                    ef.SaveChanges();
                    MessageBox.Show("Xóa Thành công !", "Delete");
                   
                    loadCourse();
                }
            }
        }

        private void courseGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel   = true;
        }

       

        private void btnCourseCreatePrevios_Click(object sender, EventArgs e)
        {
            panelCourseCreate.Visible = false;
            panelStudent.Visible=false;
            panelCourse.Visible=true;
            courseTextClear();
            loadCourse();
        }

        private void CourseCreateGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int  num = Convert.ToInt32(txtStudentCreateId2.Text);
            var ibj =studentsCreate2.FirstOrDefault(sta=>sta.StuId == num );
            studentCreate1.Add(ibj);
            studentsCreate2.Remove(ibj);
            if (studentCourseSearch) { loadStudentCreateSearch(); } else { loadStudentCreate(); }
           

           
        }

        private void CourseCreateGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int num = Convert.ToInt32(txtStudentCreateId1.Text);
            var ibj = studentCreate1.FirstOrDefault(sta => sta.StuId == num);
            studentsCreate2.Add(ibj);
            studentCreate1.Remove(ibj);
            if (studentCourseSearch) { loadStudentCreateSearch(); } else { loadStudentCreate(); }
        }

        private void btnCourseCreateSearch_Click(object sender, EventArgs e)
        {
            studentCourseSearch = true;
            loadStudentCreateSearch();
        }

        private void btnLoadStudentCourse_Click(object sender, EventArgs e)
        {
            studentCourseSearch = false;
            txtStudentCourseSearch.Text = " ";
            loadStudentCreate();
        }

        private void btnLoadAllData_Click(object sender, EventArgs e)
        {
            loadCourseCreate();
        }

        
        private void btnCourseCreate_Click(object sender, EventArgs e)
        {
            panelStudent.Visible = false;
            panelCourse.Visible = false;
            panelCourseCreate.Visible = true;
            //
            btnLoadAllData.Visible = true;
            richTextBox1.Visible = true;
            btnExam.Visible = false;
            btnLoadAllData.Visible = true;
            
            btnCourseCreateAdd.Text = "Add";
            txtCourseExam.Text = "Course Create";
            loadCourseCreate();


        }
        private void btnCourseDetail_Click(object sender, EventArgs e)
        {

            panelCourse.Visible = false;
            panelCourseCreate.Visible = true;
            //

           
            btnLoadAllData.Visible = false;
            richTextBox1.Visible = false;
            btnExam.Visible = true;
            btnCourseCreateAdd.Text = "Update";
            txtCourseExam.Text = "Course Detail";
            //
            loadCousrseUpdate(int.Parse(txtCourseId.Text));


        }
        private void loadCousrseUpdate(int Id)
        {
            using (var ef = new SinhVienEntities())
            {
                courseCreate = ef.TblCourses.FirstOrDefault(stu => stu.CouId == Id);
                studentCreate1 = new List<TblStudent>();
                studentsCreate2 = new List<TblStudent>();
                ghet = " ";
                ef.TblExams.Where(stu => stu.CouId == Id).ToList().ForEach(stu =>
                {
                    studentCreate1.Add(ef.TblStudents.FirstOrDefault(sta=>sta.StuId==stu.StuId));
                    ghet += "," + stu.StuId + ",";
                });

                ef.TblStudents.Where(stu=>
                !(ghet.Contains(","+stu.StuId+","))).ToList().ForEach(stu => {
                    studentsCreate2.Add(stu);
                });
                studentsCreate3 = new List<TblStudent>();
                studentCreate1.ForEach(
                    student => studentsCreate3.Add(student));

                var crs = ef.TblExams.FirstOrDefault(stu => stu.CouId == Id);
                if (crs != null)
                {
                    txtExamName.Text = crs.ExamName + "";
                    txtMarkPass.Text = crs.MarkPass + "";
                    dtpExamDate.Value = (DateTime)crs.ExamDate;
                }
                txtCourseCreateName.Text = courseCreate.CouName;
                dtpCourseCreate.Value = DateTime.TryParseExact(courseCreate.CouSemester.Substring(courseCreate.CouSemester.IndexOf("-") + 1), "yyyy", new CultureInfo("vi-Vn"), DateTimeStyles.None, out var g) ? g : DateTime.Now;
                lbCourseCreate.SelectedIndex = (courseCreate.CouSemester.Contains("Kỳ 2") ? 1 : (courseCreate.CouSemester.Contains("Học kỳ hè") ? 2 : 0));
               
                studentCourseSearch = false;
                loadStudentCreate();
                
                //ef.TblExams.ToList().ForEach(
                //    sta =>
                //    {
                //        if(sta.CouId==Id)
                //        {
                //            studentCreate1.Add(ef.TblStudents.FirstOrDefault(stx=>sta.StuId == sta.ExamId));
                //        }
                //        else
                //        {

                //        }
                //    }
                //    );

            }
        }
       

        private void btnExam_Click(object sender, EventArgs e)
        {
            panelExam.Visible = true;
            panelCourse.Visible = false;
            panelCourseCreate.Visible = false;
            panelStudent.Visible = false;
            loadExam(courseCreate.CouId);
                
        }

       

        private void examGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

            MessageBox.Show("Điểm chỉ chấp nhận nhập số nguyên từ 0->10","Error",MessageBoxButtons.YesNo,MessageBoxIcon.Error);
          

            e.Cancel = true;
        }
        private void examGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

          
            var b = int.Parse(examGridView.Rows[e.RowIndex].Cells[2].Value.ToString());
           
            if(0<=b && b <= 10)
            {
                ExamUpdate(int.Parse(txtIdExam.Text), courseCreate.CouId, b);
                examGridView.Rows[e.RowIndex].Cells[2].Style.ForeColor = Color.Red;
                examGridView.Rows[e.RowIndex].Cells[2].ErrorText = "";

                return;
            }
            examGridView.Rows[e.RowIndex].Cells[2].Style.ForeColor = Color.Blue;
            examGridView.Rows[e.RowIndex].Cells[2].ErrorText = "Điểm chỉ nhập từ 0->10 thôi !";
            MessageBox.Show("Điểm chỉ nhập từ 0->10 thôi!", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);


        }

        private void btnExamChange_Click(object sender, EventArgs e)
        {
            var b = int.Parse(txtMarkExam.Text);
            if (0 <= b && b <= 10)
            {
               
                ExamUpdate(int.Parse(txtIdExam.Text), courseCreate.CouId, b);
                examGridView.Rows[int.Parse(examGridView.SelectedRows[0].Index.ToString())].Cells[2].Value = b;
               
                return;
            }
            else
            {
                MessageBox.Show("Điểm chỉ nhập từ 0->10 thôi!", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                txtMarkExam.Focus();

            }
            

        }

        private void btnReturnExam_Click(object sender, EventArgs e)
        {
            panelCourseCreate.Visible = true;
            panelExam.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panelCourse.Visible= false;
            panelCourseCreate.Visible= false;
            panelExam.Visible= false;
            panelStudentDetail.Visible= false;
            panelStudent.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panelCourse.Visible = true;
            panelCourseCreate.Visible = false;
            panelExam.Visible = false;
            panelStudentDetail.Visible = false;
            panelStudent.Visible = false;
        }

        private void btnDetailStudent_Click(object sender, EventArgs e)
        {
            panelStudent.Visible = false;
            panelStudentDetail.Visible = true;
            
            txtStuDeId.Text = txtStudentId.Text;
            txtStuDeName.Text = txtStudentName.Text;
            txtStuDePass.Text = txtStudentPass.Text;
            txtStuDePhone.Text = txtStudentPhone.Text;
            txtStuDeEmail.Text = txtStudentEmail.Text;
            txtStuDeAdress.Text = txtStudentAdress.Text;
            txtStuDeDept.Text = txtStudentDeptId.Text;
            loadStudentDetail(int.Parse(txtStuDeId.Text));



        }

        private void btnStudentSearch_Click(object sender, EventArgs e)
        {
            loadStudentSearch(txtStudentSearch.Text);
        }

        private void txtStudentLoad_Click(object sender, EventArgs e)
        {
            loadStudent();
        }

        private void btnStuDeReturn_Click(object sender, EventArgs e)
        {
            panelStudent.Visible = true;
            panelStudentDetail.Visible = false;
        }

        private void panelStudentDetail_Paint(object sender, PaintEventArgs e)
        {

        }











        //private  void button4_Click(object sender, EventArgs e)
        //{
        //    var t = new List<int>();
        //    using (var ef = new SinhVienEntities())
        //    {

        //        ef.TblExams.Where(sta => sta.CouId == 3).ToList().ForEach
        //           (abc =>
        //           {
        //               //t.Add(abc.ExamId);
        //               ef.TblExams.Remove(abc);

        //           });


        //        ef.SaveChanges();
        //    }
        //    //foreach(var v in t)
        //    //{
        //    //    Console.WriteLine(v);
        //    //    deleteExam(v);
        //    //}
        //}
        //private  void deleteExam(int id)
        //{
        //    using (var ef = new SinhVienEntities())
        //    {
        //        ef.TblExams.Remove(ef.TblExams.FirstOrDefault(sta=>sta.ExamId== id));
        //        ef.SaveChanges();
        //    }

        //}




    }
}
