using QLSinhVien.Services;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLSinhVien.Forms
{
    public partial class FormDangNhap : Form
    {
        public static string CurrentMaGV = ""; // Lưu mã giảng viên đăng nhập

        public FormDangNhap()
        {
            InitializeComponent();
        }

        private void FormDangNhap_Load(object sender, EventArgs e)
        {
            // Thiết lập thuộc tính cho TextBox mật khẩu
            txtMatKhau.PasswordChar = '*';
            this.AcceptButton = btnDangNhap; // Enter để đăng nhập
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string maGV = txtMaGV.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();

            if (string.IsNullOrEmpty(maGV) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mật khẩu mặc định
            if (matKhau != "123")
            {
                MessageBox.Show("Sai mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra mã giảng viên trong SQL
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM GiangVien WHERE MaGV=@MaGV", conn);
                cmd.Parameters.AddWithValue("@MaGV", maGV);
                int count = (int)cmd.ExecuteScalar();

                if (count > 0)
                {
                    CurrentMaGV = maGV;
                    MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Mở form lớp bộ môn
                    FormLopBoMon f = new FormLopBoMon();
                    f.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Mã giảng viên không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
