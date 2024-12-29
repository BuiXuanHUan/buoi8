using Lab05.BUS;
using Lab05.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lap5GUI
{
    
    public partial class Form1 : Form
    {
        private readonly StudentService studentService = new StudentService();
        private readonly FacultyService facultyService = new FacultyService();
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                setGridViewStyle(dgvStudent);
                var listFacultys = facultyService.GetAll();
                var listStudents = studentService.GetAll();
                FillFalcultyCombobox(listFacultys);
                BindGrid(listStudents);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FillFalcultyCombobox(List<Faculty> listFacultys)
        {
            listFacultys.Insert(0, new Faculty());
            this.cbxkhoa.DataSource = listFacultys;
            this.cbxkhoa.DisplayMember = "FacultyName";
            this.cbxkhoa.ValueMember = "FacultyID";
        }
        private void BindGrid(List<Student> listStudent)
        {
            dgvStudent.Rows.Clear();
            foreach (var item in listStudent)
            {
                int index = dgvStudent.Rows.Add();
                dgvStudent.Rows[index].Cells[0].Value = item.StudentID;
           
                dgvStudent.Rows[index].Cells[1].Value = item.FullName;


                if (item.Faculty != null)
                    dgvStudent.Rows[index].Cells[2].Value =
                    item.Faculty.FacultyName;
                dgvStudent.Rows[index].Cells[3].Value = item.AverageScore + "";
                if (item.MajorID != null)
                    dgvStudent.Rows[index].Cells[4].Value = item.Major.Name + "";
                
            }
        }
        public void setGridViewStyle(DataGridView dgview)
        {
            dgview.BorderStyle = BorderStyle.None;
            dgview.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgview.CellBorderStyle =
            DataGridViewCellBorderStyle.SingleHorizontal;
            dgview.BackgroundColor = Color.White;
            dgview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int.TryParse(txtma.Text, out int studentId);
                string fullName = cbxkhoa.Text.Trim();
                int.TryParse(cbxkhoa.SelectedValue?.ToString(), out int facultyId);
                double.TryParse(txtdiem.Text, out double averageScore);

                if (string.IsNullOrEmpty(fullName) || facultyId == 0)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                    return;
                }

                var student = new Student
                {
                    StudentID = studentId.ToString(),
                    FullName = fullName,
                    FacultyID = facultyId,
                    AverageScore = averageScore
                };


                var existingStudent = studentService.FinBtID(studentId.ToString());
                if (existingStudent == null)
                {

                    studentService.InsertUpdate(student);
                    MessageBox.Show("Thêm mới thành công!");
                }
                else
                {
                    existingStudent.FullName = fullName;
                    existingStudent.FacultyID = facultyId;
                    existingStudent.AverageScore = averageScore;

                    studentService.InsertUpdate(existingStudent);
                    MessageBox.Show("Cập nhật thành công!");
                }


                var listStudents = studentService.GetAll();
                BindGrid(listStudents);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.TryParse(txtma.Text, out int studentId))
                {
                    var confirmResult = MessageBox.Show(
                        "Bạn có chắc muốn xóa sinh viên này không?",
                        "Xác nhận xóa",
                        MessageBoxButtons.YesNo);

                    if (confirmResult == DialogResult.Yes)
                    {
                        studentService.Delete(studentId.ToString());

                        var listStudents = studentService.GetAll();
                        BindGrid(listStudents);

                        MessageBox.Show("Xóa thành công!");
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn hoặc nhập mã sinh viên hợp lệ.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void txtten_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbxkhoa_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvStudent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow selectedRow = dgvStudent.Rows[e.RowIndex];

                txtma.Text = selectedRow.Cells[0].Value?.ToString();
                txtten.Text = selectedRow.Cells[1].Value?.ToString();

                var facultyCell = selectedRow.Cells[2] as DataGridViewComboBoxCell;
                if (facultyCell != null)
                {
                    cbxkhoa.SelectedValue = facultyCell.Value;
                    cbxkhoa.Text = facultyCell.FormattedValue?.ToString();
                }

                txtdiem.Text = selectedRow.Cells[3].Value?.ToString();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Cấu hình các thuộc tính của OpenFileDialog
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";  // Cho phép chọn ảnh với các định dạng phổ biến
            openFileDialog.Title = "Chọn ảnh";  // Tiêu đề của hộp thoại

            // Kiểm tra xem người dùng đã chọn tệp và nhấn OK chưa
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Tải ảnh đã chọn vào PictureBox
                    pictureBox1.Image = Image.FromFile(openFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi khi tải ảnh: " + ex.Message);
                }
            }
        }


















    }
}
