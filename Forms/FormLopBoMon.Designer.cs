namespace QLSinhVien.Forms
{
    partial class FormLopBoMon
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            label1 = new Label();
            cmbLop = new ComboBox();
            lblHocKy = new Label();
            lblNamHoc = new Label();
            label4 = new Label();
            txtSearch = new TextBox();
            gridSinhVien = new DataGridView();
            btnLuuDiem = new Button();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            lblTongSV = new Label();
            lblSoDau = new Label();
            lblSoRot = new Label();
            btnImport = new Button();
            btnExport = new Button();
            label5 = new Label();
            cmbHocKy = new ComboBox();
            cmbNamHoc = new ComboBox();
            chartThongKe = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)gridSinhVien).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chartThongKe).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10.2F);
            label1.Location = new Point(467, 9);
            label1.Name = "label1";
            label1.Size = new Size(84, 23);
            label1.TabIndex = 0;
            label1.Text = "Chọn lớp:";
            // 
            // cmbLop
            // 
            cmbLop.Font = new Font("Segoe UI", 10.2F);
            cmbLop.FormattingEnabled = true;
            cmbLop.Location = new Point(557, 6);
            cmbLop.Name = "cmbLop";
            cmbLop.Size = new Size(229, 31);
            cmbLop.TabIndex = 1;
            cmbLop.SelectedIndexChanged += cmbLop_SelectedIndexChanged;
            // 
            // lblHocKy
            // 
            lblHocKy.AutoSize = true;
            lblHocKy.Font = new Font("Segoe UI", 10.2F);
            lblHocKy.Location = new Point(288, 9);
            lblHocKy.Name = "lblHocKy";
            lblHocKy.Size = new Size(61, 23);
            lblHocKy.TabIndex = 2;
            lblHocKy.Text = "Học kì:";
            // 
            // lblNamHoc
            // 
            lblNamHoc.AutoSize = true;
            lblNamHoc.Font = new Font("Segoe UI", 10.2F);
            lblNamHoc.Location = new Point(12, 9);
            lblNamHoc.Name = "lblNamHoc";
            lblNamHoc.Size = new Size(84, 23);
            lblNamHoc.TabIndex = 4;
            lblNamHoc.Text = "Năm học:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10.2F);
            label4.Location = new Point(12, 64);
            label4.Name = "label4";
            label4.Size = new Size(83, 23);
            label4.TabIndex = 6;
            label4.Text = "Tìm kiếm:";
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Segoe UI", 10.2F);
            txtSearch.Location = new Point(102, 61);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(308, 30);
            txtSearch.TabIndex = 7;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // gridSinhVien
            // 
            gridSinhVien.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridSinhVien.Location = new Point(12, 114);
            gridSinhVien.Name = "gridSinhVien";
            gridSinhVien.RowHeadersWidth = 51;
            gridSinhVien.Size = new Size(779, 216);
            gridSinhVien.TabIndex = 8;
            // 
            // btnLuuDiem
            // 
            btnLuuDiem.Location = new Point(537, 358);
            btnLuuDiem.Name = "btnLuuDiem";
            btnLuuDiem.Size = new Size(94, 29);
            btnLuuDiem.TabIndex = 10;
            btnLuuDiem.Text = "Lưu điểm";
            btnLuuDiem.UseVisualStyleBackColor = true;
            btnLuuDiem.Click += btnLuuDiem_Click;
            // 
            // lblTongSV
            // 
            lblTongSV.AutoSize = true;
            lblTongSV.Font = new Font("Segoe UI", 12F);
            lblTongSV.Location = new Point(12, 345);
            lblTongSV.Name = "lblTongSV";
            lblTongSV.Size = new Size(142, 28);
            lblTongSV.TabIndex = 12;
            lblTongSV.Text = "Tổng sinh viên:";
            // 
            // lblSoDau
            // 
            lblSoDau.AutoSize = true;
            lblSoDau.Font = new Font("Segoe UI", 12F);
            lblSoDau.Location = new Point(12, 382);
            lblSoDau.Name = "lblSoDau";
            lblSoDau.Size = new Size(77, 28);
            lblSoDau.TabIndex = 13;
            lblSoDau.Text = "Số đậu:";
            // 
            // lblSoRot
            // 
            lblSoRot.AutoSize = true;
            lblSoRot.Font = new Font("Segoe UI", 12F);
            lblSoRot.Location = new Point(12, 419);
            lblSoRot.Name = "lblSoRot";
            lblSoRot.Size = new Size(70, 28);
            lblSoRot.TabIndex = 14;
            lblSoRot.Text = "Số rớt:";
            // 
            // btnImport
            // 
            btnImport.Location = new Point(652, 358);
            btnImport.Name = "btnImport";
            btnImport.Size = new Size(94, 29);
            btnImport.TabIndex = 15;
            btnImport.Text = "Import";
            btnImport.UseVisualStyleBackColor = true;
            btnImport.Click += btnImport_Click;
            // 
            // btnExport
            // 
            btnExport.Location = new Point(652, 408);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(94, 29);
            btnExport.TabIndex = 16;
            btnExport.Text = "Export";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += btnExport_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F);
            label5.Location = new Point(160, 345);
            label5.Name = "label5";
            label5.Size = new Size(0, 28);
            label5.TabIndex = 17;
            // 
            // cmbHocKy
            // 
            cmbHocKy.Font = new Font("Segoe UI", 10.2F);
            cmbHocKy.FormattingEnabled = true;
            cmbHocKy.Location = new Point(355, 6);
            cmbHocKy.Name = "cmbHocKy";
            cmbHocKy.Size = new Size(81, 31);
            cmbHocKy.TabIndex = 18;
            // 
            // cmbNamHoc
            // 
            cmbNamHoc.Font = new Font("Segoe UI", 10.2F);
            cmbNamHoc.FormattingEnabled = true;
            cmbNamHoc.Location = new Point(114, 6);
            cmbNamHoc.Name = "cmbNamHoc";
            cmbNamHoc.Size = new Size(137, 31);
            cmbNamHoc.TabIndex = 19;
            // 
            // chartThongKe
            // 
            chartArea1.Name = "ChartArea1";
            chartThongKe.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chartThongKe.Legends.Add(legend1);
            chartThongKe.Location = new Point(272, 345);
            chartThongKe.Name = "chartThongKe";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chartThongKe.Series.Add(series1);
            chartThongKe.Size = new Size(206, 128);
            chartThongKe.TabIndex = 20;
            chartThongKe.Text = "chart1";
            // 
            // FormLopBoMon
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(798, 472);
            Controls.Add(chartThongKe);
            Controls.Add(cmbNamHoc);
            Controls.Add(cmbHocKy);
            Controls.Add(label5);
            Controls.Add(btnExport);
            Controls.Add(btnImport);
            Controls.Add(lblSoRot);
            Controls.Add(lblSoDau);
            Controls.Add(lblTongSV);
            Controls.Add(btnLuuDiem);
            Controls.Add(gridSinhVien);
            Controls.Add(txtSearch);
            Controls.Add(label4);
            Controls.Add(lblNamHoc);
            Controls.Add(lblHocKy);
            Controls.Add(cmbLop);
            Controls.Add(label1);
            Name = "FormLopBoMon";
            Text = "Form lớp bộ môn";
            Load += FormLopBoMon_Load;
            ((System.ComponentModel.ISupportInitialize)gridSinhVien).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartThongKe).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox cmbLop;
        private Label lblHocKy;
        private Label lblNamHoc;
        private Label label4;
        private TextBox txtSearch;
        private DataGridView gridSinhVien;
        private Button btnLuuDiem;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Label lblTongSV;
        private Label lblSoDau;
        private Label lblSoRot;
        private Button btnImport;
        private Button btnExport;
        private Label label5;
        private ComboBox cmbHocKy;
        private ComboBox cmbNamHoc;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartThongKe;
    }
}