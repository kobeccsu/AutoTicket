namespace AutoTicket
{
    partial class QueryTicket
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
            this.components = new System.ComponentModel.Container();
            this.cmbstartStation = new System.Windows.Forms.ComboBox();
            this.cmbendStation = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpTrainDate = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.lblCDNSite = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.station_train_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonTextInfo = new System.Windows.Forms.DataGridViewButtonColumn();
            this.from_station_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.to_station_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.start_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.arrive_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lishi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.swz_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zy_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ze_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yz_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rz_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yw_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rw_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wz_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.secretStr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbstartStation
            // 
            this.cmbstartStation.FormattingEnabled = true;
            this.cmbstartStation.Location = new System.Drawing.Point(89, 34);
            this.cmbstartStation.Name = "cmbstartStation";
            this.cmbstartStation.Size = new System.Drawing.Size(121, 20);
            this.cmbstartStation.TabIndex = 0;
            // 
            // cmbendStation
            // 
            this.cmbendStation.FormattingEnabled = true;
            this.cmbendStation.Location = new System.Drawing.Point(284, 34);
            this.cmbendStation.Name = "cmbendStation";
            this.cmbendStation.Size = new System.Drawing.Size(121, 20);
            this.cmbendStation.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "出发地";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(237, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "目的地";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "出发时间";
            // 
            // dtpTrainDate
            // 
            this.dtpTrainDate.Location = new System.Drawing.Point(89, 72);
            this.dtpTrainDate.Name = "dtpTrainDate";
            this.dtpTrainDate.Size = new System.Drawing.Size(121, 21);
            this.dtpTrainDate.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(472, 31);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(26, 576);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(741, 96);
            this.richTextBox1.TabIndex = 7;
            this.richTextBox1.Text = "";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(472, 67);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(119, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "选好某趟车的票";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(773, 660);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "CDN站点:";
            // 
            // lblCDNSite
            // 
            this.lblCDNSite.AutoSize = true;
            this.lblCDNSite.Location = new System.Drawing.Point(78, 331);
            this.lblCDNSite.Name = "lblCDNSite";
            this.lblCDNSite.Size = new System.Drawing.Size(0, 12);
            this.lblCDNSite.TabIndex = 10;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.station_train_code,
            this.buttonTextInfo,
            this.from_station_name,
            this.to_station_name,
            this.start_time,
            this.arrive_time,
            this.lishi,
            this.swz_num,
            this.zy_num,
            this.ze_num,
            this.yz_num,
            this.rz_num,
            this.yw_num,
            this.rw_num,
            this.wz_num,
            this.secretStr});
            this.dataGridView1.Location = new System.Drawing.Point(26, 140);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(918, 420);
            this.dataGridView1.TabIndex = 11;
            // 
            // station_train_code
            // 
            this.station_train_code.DataPropertyName = "station_train_code";
            this.station_train_code.HeaderText = "车次";
            this.station_train_code.Name = "station_train_code";
            this.station_train_code.Width = 44;
            // 
            // buttonTextInfo
            // 
            this.buttonTextInfo.DataPropertyName = "buttonTextInfo";
            this.buttonTextInfo.HeaderText = "抢票";
            this.buttonTextInfo.Name = "buttonTextInfo";
            this.buttonTextInfo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.buttonTextInfo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.buttonTextInfo.Width = 44;
            // 
            // from_station_name
            // 
            this.from_station_name.DataPropertyName = "from_station_name";
            this.from_station_name.HeaderText = "出发站";
            this.from_station_name.Name = "from_station_name";
            this.from_station_name.Width = 44;
            // 
            // to_station_name
            // 
            this.to_station_name.DataPropertyName = "to_station_name";
            this.to_station_name.HeaderText = "到达站";
            this.to_station_name.Name = "to_station_name";
            this.to_station_name.Width = 44;
            // 
            // start_time
            // 
            this.start_time.DataPropertyName = "start_time";
            this.start_time.HeaderText = "出发时间";
            this.start_time.Name = "start_time";
            this.start_time.Width = 44;
            // 
            // arrive_time
            // 
            this.arrive_time.DataPropertyName = "arrive_time";
            this.arrive_time.HeaderText = "到达时间";
            this.arrive_time.Name = "arrive_time";
            this.arrive_time.Width = 44;
            // 
            // lishi
            // 
            this.lishi.DataPropertyName = "lishi";
            this.lishi.HeaderText = "历时";
            this.lishi.Name = "lishi";
            this.lishi.Width = 44;
            // 
            // swz_num
            // 
            this.swz_num.DataPropertyName = "swz_num";
            this.swz_num.HeaderText = "商务座";
            this.swz_num.Name = "swz_num";
            this.swz_num.Width = 44;
            // 
            // zy_num
            // 
            this.zy_num.DataPropertyName = "zy_num";
            this.zy_num.HeaderText = "一等座";
            this.zy_num.Name = "zy_num";
            this.zy_num.Width = 44;
            // 
            // ze_num
            // 
            this.ze_num.DataPropertyName = "ze_num";
            this.ze_num.HeaderText = "二等座";
            this.ze_num.Name = "ze_num";
            this.ze_num.Width = 44;
            // 
            // yz_num
            // 
            this.yz_num.DataPropertyName = "yz_num";
            this.yz_num.HeaderText = "硬座";
            this.yz_num.Name = "yz_num";
            this.yz_num.Width = 44;
            // 
            // rz_num
            // 
            this.rz_num.DataPropertyName = "rz_num";
            this.rz_num.HeaderText = "软座";
            this.rz_num.Name = "rz_num";
            this.rz_num.Width = 44;
            // 
            // yw_num
            // 
            this.yw_num.DataPropertyName = "yw_num";
            this.yw_num.HeaderText = "硬卧";
            this.yw_num.Name = "yw_num";
            this.yw_num.Width = 44;
            // 
            // rw_num
            // 
            this.rw_num.DataPropertyName = "rw_num";
            this.rw_num.HeaderText = "软卧";
            this.rw_num.Name = "rw_num";
            this.rw_num.Width = 44;
            // 
            // wz_num
            // 
            this.wz_num.DataPropertyName = "wz_num";
            this.wz_num.HeaderText = "无座";
            this.wz_num.Name = "wz_num";
            this.wz_num.Width = 44;
            // 
            // secretStr
            // 
            this.secretStr.DataPropertyName = "secretStr";
            this.secretStr.HeaderText = "序列号";
            this.secretStr.Name = "secretStr";
            // 
            // QueryTicket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 683);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lblCDNSite);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dtpTrainDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbendStation);
            this.Controls.Add(this.cmbstartStation);
            this.Name = "QueryTicket";
            this.Text = "查询车票";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbstartStation;
        private System.Windows.Forms.ComboBox cmbendStation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpTrainDate;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblCDNSite;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn station_train_code;
        private System.Windows.Forms.DataGridViewButtonColumn buttonTextInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn from_station_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn to_station_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn start_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn arrive_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn lishi;
        private System.Windows.Forms.DataGridViewTextBoxColumn swz_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn zy_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn ze_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn yz_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn rz_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn yw_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn rw_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn wz_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn secretStr;
    }
}