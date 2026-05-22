using QLSinhVien.Services;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using OfficeOpenXml;
using System.Windows.Forms.DataVisualization.Charting;

namespace QLSinhVien.Forms
{
    public partial class FormLopBoMon : Form
    {
        public FormLopBoMon()
        {
            InitializeComponent();
        }

        private void FormLopBoMon_Load(object sender, EventArgs e)
        {
            LoadNamHoc();
        }

        #region Helper
        private void SafeSetSelectedIndex(ComboBox cb, int index, EventHandler handler)
        {
            if (cb == null) return;
            cb.SelectedIndexChanged -= handler;
            if (index >= 0 && index < cb.Items.Count)
                cb.SelectedIndex = index;
            cb.SelectedIndexChanged += handler;
        }
        #endregion

        #region Load Năm học Học kỳ Lớp tự động

        private void LoadNamHoc()
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "SELECT DISTINCT NamHoc FROM Lop WHERE MaGV=@MaGV ORDER BY NamHoc DESC", conn);
                    cmd.Parameters.AddWithValue("@MaGV", FormDangNhap.CurrentMaGV);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cmbNamHoc.DisplayMember = "NamHoc";
                    cmbNamHoc.ValueMember = "NamHoc";
                    cmbNamHoc.DataSource = dt;

                    if (dt.Rows.Count > 0)
                    {
                        SafeSetSelectedIndex(cmbNamHoc, 0, cmbNamHoc_SelectedIndexChanged);
                        LoadHocKy();
                    }
                    else
                    {
                        cmbHocKy.DataSource = null;
                        cmbLop.DataSource = null;
                        gridSinhVien.DataSource = null;
                        CapNhatThongKe();
                        MessageBox.Show("Không có dữ liệu năm học cho giảng viên này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi truy xuất dữ liệu: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbNamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Khi người dùng thay đổi bằng tay, vẫn xử lý bình thường
            LoadHocKy();
        }

        private void LoadHocKy()
        {
            if (cmbNamHoc.SelectedValue == null) return;

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT DISTINCT HocKy FROM Lop WHERE MaGV=@MaGV AND NamHoc=@NamHoc ORDER BY HocKy", conn);
                cmd.Parameters.AddWithValue("@MaGV", FormDangNhap.CurrentMaGV);
                cmd.Parameters.AddWithValue("@NamHoc", cmbNamHoc.SelectedValue.ToString());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbHocKy.DisplayMember = "HocKy";
                cmbHocKy.ValueMember = "HocKy";
                cmbHocKy.DataSource = dt;

                if (dt.Rows.Count > 0)
                {
                    // Chọn học kỳ đầu tiên an toàn bằng SafeSetSelectedIndex
                    SafeSetSelectedIndex(cmbHocKy, 0, cmbHocKy_SelectedIndexChanged);

                    // Gọi handler để chuỗi load tiếp tục (dùng handler đã đăng ký)
                    cmbHocKy_SelectedIndexChanged(this, EventArgs.Empty);
                }
                else
                {
                    cmbLop.DataSource = null;
                    gridSinhVien.DataSource = null;
                    CapNhatThongKe();
                }
            }
        }

        private void cmbHocKy_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadLopTheoGiangVien();
        }

        private void LoadLopTheoGiangVien()
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "SELECT l.MaLop, l.HocKy, l.NamHoc, m.TenMH " +
                        "FROM Lop l JOIN MonHoc m ON l.MaMH = m.MaMH " +
                        "WHERE l.MaGV=@MaGV AND l.NamHoc=@NamHoc AND l.HocKy=@HocKy " +
                        "ORDER BY l.MaLop", conn);
                    cmd.Parameters.AddWithValue("@MaGV", FormDangNhap.CurrentMaGV);
                    cmd.Parameters.AddWithValue("@NamHoc", cmbNamHoc.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@HocKy", cmbHocKy.SelectedValue.ToString());
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        cmbLop.DataSource = null;
                        gridSinhVien.DataSource = null;
                        CapNhatThongKe();
                        MessageBox.Show("Lớp chưa có dữ liệu điểm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (!dt.Columns.Contains("Display"))
                        dt.Columns.Add("Display", typeof(string), "MaLop + ' - ' + TenMH");

                    cmbLop.DisplayMember = "Display";
                    cmbLop.ValueMember = "MaLop";
                    cmbLop.DataSource = dt;
                    SafeSetSelectedIndex(cmbLop, 0, cmbLop_SelectedIndexChanged);
                    cmbLop_SelectedIndexChanged(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi truy xuất dữ liệu lớp: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Sinh viên Thống kê Tìm kiếm

        private void cmbLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSinhVienTheoLop();
        }

        private void LoadSinhVienTheoLop()
        {
            if (cmbLop.SelectedValue == null) return;

            string maLop = cmbLop.SelectedValue.ToString();

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT s.MSSV, s.HoTen, d.DiemCC, d.DiemBT, d.DiemThi, d.DiemTB " +
                    "FROM SinhVien s JOIN Diem d ON s.MSSV = d.MSSV WHERE d.MaLop=@MaLop", conn);
                cmd.Parameters.AddWithValue("@MaLop", maLop);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gridSinhVien.DataSource = dt;
                CapNhatThongKe();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            var dt = gridSinhVien.DataSource as DataTable;

            if (dt == null || dt.Rows.Count == 0)
                return;

            dt.DefaultView.RowFilter = $"MSSV LIKE '%{keyword}%' OR HoTen LIKE '%{keyword}%'";
        }

        private void CapNhatThongKe()
        {
            int tongSV = gridSinhVien.Rows.Cast<DataGridViewRow>().Count(r => !r.IsNewRow);
            int soDau = 0;
            int soRot = 0;

            foreach (DataGridViewRow row in gridSinhVien.Rows)
            {
                if (row.IsNewRow) continue;
                object val = row.Cells["DiemTB"].Value;
                decimal diemTB = 0m;
                if (val != null && val != DBNull.Value)
                    decimal.TryParse(val.ToString(), out diemTB);

                if (diemTB >= 5m) soDau++;
                else soRot++;
            }

            // Cập nhật label thống kê
            lblTongSV.Text = $"Tổng sinh viên: {tongSV}";
            lblSoDau.Text = $"Số đậu: {soDau} ({(tongSV > 0 ? (soDau * 100 / tongSV) : 0)}%)";
            lblSoRot.Text = $"Số rớt: {soRot} ({(tongSV > 0 ? (soRot * 100 / tongSV) : 0)}%)";

            // Cập nhật Chart
            chartThongKe.Series.Clear();
            chartThongKe.ChartAreas.Clear();
            chartThongKe.ChartAreas.Add(new ChartArea("MainArea"));

            Series series = new Series("Kết quả");
            series.IsValueShownAsLabel = true; // hiển thị số trên biểu đồ

            series.ChartType = SeriesChartType.Pie; // kiểu biểu đồ
            // Thêm dữ liệu
            series.Points.AddXY("Đậu", soDau);
            series.Points.AddXY("Rớt", soRot);

            chartThongKe.Series.Add(series);

            // Thêm tiêu đề và legend
            chartThongKe.Titles.Clear();
            chartThongKe.Titles.Add("Thống kê kết quả học tập");
            chartThongKe.Legends[0].Enabled = true;
        }

        #endregion

        #region Lưu điểm Import Export

        private void btnLuuDiem_Click(object sender, EventArgs e)
        {
            if (cmbLop.SelectedValue == null) return;

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                bool coLoi = false;
                bool coLuu = false;

                foreach (DataGridViewRow row in gridSinhVien.Rows)
                {
                    if (row.IsNewRow) continue;

                    try
                    {
                        // Lấy giá trị điểm (cho phép null)
                        object valCC = row.Cells["DiemCC"].Value;
                        object valBT = row.Cells["DiemBT"].Value;
                        object valThi = row.Cells["DiemThi"].Value;
                        object valTB = row.Cells["DiemTB"].Value;

                        // Kiểm tra null
                        if (valCC == null || valCC == DBNull.Value ||
                            valBT == null || valBT == DBNull.Value ||
                            valThi == null || valThi == DBNull.Value ||
                            valTB == null || valTB == DBNull.Value)
                        {
                            MessageBox.Show($"Điểm chưa nhập cho MSSV {row.Cells["MSSV"].Value}. Không được để trống!",
                                            "Lỗi nhập điểm", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            coLoi = true;
                            continue;
                        }

                        // Chuyển sang decimal
                        decimal diemCC = Convert.ToDecimal(valCC);
                        decimal diemBT = Convert.ToDecimal(valBT);
                        decimal diemThi = Convert.ToDecimal(valThi);
                        decimal diemTB = Convert.ToDecimal(valTB);

                        // Kiểm tra hợp lệ (0–10)
                        if (diemCC < 0 || diemCC > 10 ||
                            diemBT < 0 || diemBT > 10 ||
                            diemThi < 0 || diemThi > 10 ||
                            diemTB < 0 || diemTB > 10)
                        {
                            MessageBox.Show($"Điểm không hợp lệ cho MSSV {row.Cells["MSSV"].Value}. Phải nằm trong khoảng 0–10!",
                                            "Lỗi nhập điểm", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            coLoi = true;
                            continue;
                        }

                        // Nếu hợp lệ thì lưu
                        SqlCommand cmd = new SqlCommand(
                            "UPDATE Diem SET DiemCC=@DiemCC, DiemBT=@DiemBT, DiemThi=@DiemThi, DiemTB=@DiemTB WHERE MaLop=@MaLop AND MSSV=@MSSV", conn);
                        cmd.Parameters.AddWithValue("@MaLop", cmbLop.SelectedValue);
                        cmd.Parameters.AddWithValue("@MSSV", row.Cells["MSSV"].Value);
                        cmd.Parameters.AddWithValue("@DiemCC", diemCC);
                        cmd.Parameters.AddWithValue("@DiemBT", diemBT);
                        cmd.Parameters.AddWithValue("@DiemThi", diemThi);
                        cmd.Parameters.AddWithValue("@DiemTB", diemTB);

                        cmd.ExecuteNonQuery();
                        coLuu = true;
                    }
                    catch (Exception ex)
                    {
                        coLoi = true;
                        MessageBox.Show($"Lỗi khi lưu MSSV {row.Cells["MSSV"].Value}: {ex.Message}");
                    }
                }

                // Thông báo kết quả
                if (coLuu && !coLoi)
                    MessageBox.Show("Lưu điểm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CapNhatThongKe();
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExcelPackage.License.SetNonCommercialPersonal("Risus");

            var dt = gridSinhVien.DataSource as DataTable;
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra dữ liệu trước khi export
            foreach (DataRow r in dt.Rows)
            {
                foreach (string col in new[] { "DiemCC", "DiemBT", "DiemThi", "DiemTB" })
                {
                    object v = r[col];
                    if (v == null || v == DBNull.Value)
                    {
                        MessageBox.Show($"Điểm trống ở MSSV {r["MSSV"]}. Không thể xuất!", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    decimal d;
                    if (!decimal.TryParse(v.ToString(), out d) || d < 0 || d > 10)
                    {
                        MessageBox.Show($"Điểm không hợp lệ ({v}) ở MSSV {r["MSSV"]}. Không thể xuất!", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel Files|*.xlsx";
                if (sfd.ShowDialog() != DialogResult.OK) return;

                FileInfo fi = new FileInfo(sfd.FileName);
                using (ExcelPackage pck = new ExcelPackage())
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet1");

                    // Header: thêm cột "Kết quả"
                    for (int i = 0; i < gridSinhVien.Columns.Count; i++)
                        ws.Cells[1, i + 1].Value = gridSinhVien.Columns[i].HeaderText;
                    ws.Cells[1, gridSinhVien.Columns.Count + 1].Value = "Kết quả";

                    // Data + Kết quả
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                            ws.Cells[i + 2, j + 1].Value = dt.Rows[i][j];

                        decimal diemTB = Convert.ToDecimal(dt.Rows[i]["DiemTB"]);
                        ws.Cells[i + 2, dt.Columns.Count + 1].Value = diemTB >= 5 ? "Đậu" : "Rớt";
                    }

                    // Thống kê
                    int tongSV = dt.Rows.Count;
                    int soDau = dt.AsEnumerable().Count(r => Convert.ToDecimal(r["DiemTB"]) >= 5m);
                    int soRot = tongSV - soDau;

                    int lastRow = tongSV + 3;
                    ws.Cells[lastRow, 1].Value = "Tổng sinh viên:";
                    ws.Cells[lastRow, 2].Value = tongSV;
                    ws.Cells[lastRow + 1, 1].Value = "Số đậu:";
                    ws.Cells[lastRow + 1, 2].Value = soDau;
                    ws.Cells[lastRow + 2, 1].Value = "Số rớt:";
                    ws.Cells[lastRow + 2, 2].Value = soRot;

                    // Save với xử lý lỗi
                    try
                    {
                        pck.SaveAs(fi);
                        MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (InvalidOperationException ex) when (ex.InnerException is IOException)
                    {
                        MessageBox.Show("Không thể ghi file Excel vì tệp đang được mở. Vui lòng đóng file rồi thử lại!",
                                        "Lỗi ghi file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (IOException)
                    {
                        MessageBox.Show("Không thể ghi file Excel vì tệp đang được mở. Vui lòng đóng file rồi thử lại!",
                                        "Lỗi ghi file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Không thể ghi đè file Excel vì tệp đang được mở. Vui lòng đóng file rồi thử lại!",
                                        "Lỗi ghi file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel Files|*.xlsx;*.xls";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string path = ofd.FileName;
                string connStr = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={path};Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                using (System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection(connStr))
                {
                    conn.Open();
                    System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter("SELECT * FROM [Sheet1$]", conn);
                    DataTable dtRaw = new DataTable();
                    da.Fill(dtRaw);

                    // ✅ Bỏ cột "Kết quả" nếu có
                    if (dtRaw.Columns.Contains("Kết quả"))
                        dtRaw.Columns.Remove("Kết quả");

                    // ✅ Lọc chỉ các dòng có MSSV hợp lệ (bắt đầu bằng "SV")
                    DataTable dt = dtRaw.Clone(); // tạo cấu trúc bảng mới
                    foreach (DataRow r in dtRaw.Rows)
                    {
                        var mssv = r["MSSV"]?.ToString()?.Trim();
                        if (!string.IsNullOrEmpty(mssv) && mssv.StartsWith("SV"))
                            dt.ImportRow(r);
                    }

                    // Nếu không có dòng hợp lệ
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu sinh viên hợp lệ trong file!", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Kiểm tra dữ liệu sau khi import
                    foreach (DataRow r in dt.Rows)
                    {
                        foreach (string col in new[] { "DiemCC", "DiemBT", "DiemThi", "DiemTB" })
                        {
                            object v = r[col];
                            if (v == null || v == DBNull.Value)
                            {
                                MessageBox.Show($"Điểm trống ở MSSV {r["MSSV"]}. Không thể import!", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            decimal d;
                            if (!decimal.TryParse(v.ToString(), out d) || d < 0 || d > 10)
                            {
                                MessageBox.Show($"Điểm không hợp lệ ({v}) ở MSSV {r["MSSV"]}. Không thể import!", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }

                    gridSinhVien.DataSource = dt;
                }

                MessageBox.Show("Import dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion
    }
}
