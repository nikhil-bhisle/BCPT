namespace WindowsFormsApp1
{
    partial class Add_Project_Master
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Add_Project_Master));
            this.label1 = new System.Windows.Forms.Label();
            this.label_ptsid = new System.Windows.Forms.Label();
            this.label_date2 = new System.Windows.Forms.Label();
            this.label_date1 = new System.Windows.Forms.Label();
            this.label_cat = new System.Windows.Forms.Label();
            this.label_pi = new System.Windows.Forms.Label();
            this.label_pm = new System.Windows.Forms.Label();
            this.label_pt = new System.Windows.Forms.Label();
            this.label_pd = new System.Windows.Forms.Label();
            this.label_net0 = new System.Windows.Forms.Label();
            this.PTSID = new System.Windows.Forms.TextBox();
            this.Description = new System.Windows.Forms.TextBox();
            this.PM = new System.Windows.Forms.ComboBox();
            this.submit_newproject = new System.Windows.Forms.Button();
            this.piac = new System.Windows.Forms.ComboBox();
            this.progId = new System.Windows.Forms.ComboBox();
            this.projecttype = new System.Windows.Forms.ComboBox();
            this.NetL0 = new System.Windows.Forms.TextBox();
            this.date1 = new System.Windows.Forms.DateTimePicker();
            this.date2 = new System.Windows.Forms.DateTimePicker();
            this.label_cntry = new System.Windows.Forms.Label();
            this.country_name = new System.Windows.Forms.ComboBox();
            this.label_product = new System.Windows.Forms.Label();
            this.product_name = new System.Windows.Forms.ComboBox();
            this.label_status = new System.Windows.Forms.Label();
            this.product_status = new System.Windows.Forms.ComboBox();
            this.SecoreL1 = new System.Windows.Forms.TextBox();
            this.label_net1 = new System.Windows.Forms.Label();
            this.label_sec0 = new System.Windows.Forms.Label();
            this.SecoreL0 = new System.Windows.Forms.TextBox();
            this.label_sec1 = new System.Windows.Forms.Label();
            this.NetL1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(242, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(335, 46);
            this.label1.TabIndex = 0;
            this.label1.Text = "Add New Project";
            // 
            // label_ptsid
            // 
            this.label_ptsid.AutoSize = true;
            this.label_ptsid.Font = new System.Drawing.Font("Candara", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_ptsid.Location = new System.Drawing.Point(35, 102);
            this.label_ptsid.Name = "label_ptsid";
            this.label_ptsid.Size = new System.Drawing.Size(49, 18);
            this.label_ptsid.TabIndex = 1;
            this.label_ptsid.Text = "PTS ID";
            this.label_ptsid.Click += new System.EventHandler(this.label2_Click);
            // 
            // label_date2
            // 
            this.label_date2.AutoSize = true;
            this.label_date2.Font = new System.Drawing.Font("Candara", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_date2.Location = new System.Drawing.Point(438, 348);
            this.label_date2.Name = "label_date2";
            this.label_date2.Size = new System.Drawing.Size(140, 18);
            this.label_date2.TabIndex = 2;
            this.label_date2.Text = "Project Release Date";
            this.label_date2.Click += new System.EventHandler(this.label_date2_Click);
            // 
            // label_date1
            // 
            this.label_date1.AutoSize = true;
            this.label_date1.Font = new System.Drawing.Font("Candara", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_date1.Location = new System.Drawing.Point(35, 338);
            this.label_date1.Name = "label_date1";
            this.label_date1.Size = new System.Drawing.Size(135, 18);
            this.label_date1.TabIndex = 3;
            this.label_date1.Text = "Target Release Date";
            this.label_date1.Click += new System.EventHandler(this.label4_Click);
            // 
            // label_cat
            // 
            this.label_cat.AutoSize = true;
            this.label_cat.Font = new System.Drawing.Font("Candara", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_cat.Location = new System.Drawing.Point(438, 279);
            this.label_cat.Name = "label_cat";
            this.label_cat.Size = new System.Drawing.Size(97, 18);
            this.label_cat.TabIndex = 4;
            this.label_cat.Text = "PIAC Category";
            this.label_cat.Click += new System.EventHandler(this.label5_Click);
            // 
            // label_pi
            // 
            this.label_pi.AutoSize = true;
            this.label_pi.Font = new System.Drawing.Font("Candara", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_pi.Location = new System.Drawing.Point(35, 277);
            this.label_pi.Name = "label_pi";
            this.label_pi.Size = new System.Drawing.Size(78, 18);
            this.label_pi.TabIndex = 5;
            this.label_pi.Text = "Program Id";
            // 
            // label_pm
            // 
            this.label_pm.AutoSize = true;
            this.label_pm.Font = new System.Drawing.Font("Candara", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_pm.Location = new System.Drawing.Point(438, 214);
            this.label_pm.Name = "label_pm";
            this.label_pm.Size = new System.Drawing.Size(114, 18);
            this.label_pm.TabIndex = 6;
            this.label_pm.Text = "Project Manager";
            // 
            // label_pt
            // 
            this.label_pt.AutoSize = true;
            this.label_pt.Font = new System.Drawing.Font("Candara", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_pt.Location = new System.Drawing.Point(35, 210);
            this.label_pt.Name = "label_pt";
            this.label_pt.Size = new System.Drawing.Size(85, 18);
            this.label_pt.TabIndex = 7;
            this.label_pt.Text = "Project type";
            // 
            // label_pd
            // 
            this.label_pd.AutoSize = true;
            this.label_pd.Font = new System.Drawing.Font("Candara", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_pd.Location = new System.Drawing.Point(35, 152);
            this.label_pd.Name = "label_pd";
            this.label_pd.Size = new System.Drawing.Size(125, 18);
            this.label_pd.TabIndex = 8;
            this.label_pd.Text = "Project Decription";
            // 
            // label_net0
            // 
            this.label_net0.AutoSize = true;
            this.label_net0.Font = new System.Drawing.Font("Candara", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_net0.Location = new System.Drawing.Point(35, 571);
            this.label_net0.Name = "label_net0";
            this.label_net0.Size = new System.Drawing.Size(56, 18);
            this.label_net0.TabIndex = 9;
            this.label_net0.Text = ".NET L0";
            this.label_net0.Click += new System.EventHandler(this.label10_Click);
            // 
            // PTSID
            // 
            this.PTSID.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PTSID.Location = new System.Drawing.Point(193, 96);
            this.PTSID.Name = "PTSID";
            this.PTSID.Size = new System.Drawing.Size(180, 27);
            this.PTSID.TabIndex = 11;
            this.PTSID.TextChanged += new System.EventHandler(this.PTSID_TextChanged);
            // 
            // Description
            // 
            this.Description.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Description.Location = new System.Drawing.Point(193, 148);
            this.Description.Name = "Description";
            this.Description.Size = new System.Drawing.Size(570, 27);
            this.Description.TabIndex = 19;
            // 
            // PM
            // 
            this.PM.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.PM.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.PM.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PM.FormattingEnabled = true;
            this.PM.Location = new System.Drawing.Point(590, 213);
            this.PM.Name = "PM";
            this.PM.Size = new System.Drawing.Size(169, 27);
            this.PM.TabIndex = 22;
            this.PM.SelectedIndexChanged += new System.EventHandler(this.PM_SelectedIndexChanged);
            // 
            // submit_newproject
            // 
            this.submit_newproject.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.submit_newproject.Font = new System.Drawing.Font("Candara", 15F, System.Drawing.FontStyle.Bold);
            this.submit_newproject.Location = new System.Drawing.Point(34, 630);
            this.submit_newproject.Name = "submit_newproject";
            this.submit_newproject.Size = new System.Drawing.Size(339, 38);
            this.submit_newproject.TabIndex = 23;
            this.submit_newproject.Text = "Submit";
            this.submit_newproject.UseVisualStyleBackColor = false;
            this.submit_newproject.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // piac
            // 
            this.piac.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.piac.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.piac.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.piac.FormattingEnabled = true;
            this.piac.Location = new System.Drawing.Point(590, 275);
            this.piac.Name = "piac";
            this.piac.Size = new System.Drawing.Size(173, 27);
            this.piac.TabIndex = 24;
            this.piac.SelectedIndexChanged += new System.EventHandler(this.piac_SelectedIndexChanged);
            // 
            // progId
            // 
            this.progId.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.progId.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.progId.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.progId.FormattingEnabled = true;
            this.progId.Location = new System.Drawing.Point(193, 268);
            this.progId.Name = "progId";
            this.progId.Size = new System.Drawing.Size(180, 27);
            this.progId.TabIndex = 25;
            // 
            // projecttype
            // 
            this.projecttype.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.projecttype.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.projecttype.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.projecttype.FormattingEnabled = true;
            this.projecttype.Location = new System.Drawing.Point(193, 210);
            this.projecttype.Name = "projecttype";
            this.projecttype.Size = new System.Drawing.Size(180, 27);
            this.projecttype.TabIndex = 26;
            // 
            // NetL0
            // 
            this.NetL0.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NetL0.Location = new System.Drawing.Point(193, 559);
            this.NetL0.Name = "NetL0";
            this.NetL0.Size = new System.Drawing.Size(180, 27);
            this.NetL0.TabIndex = 27;
            this.NetL0.TextChanged += new System.EventHandler(this.NetL0_TextChanged);
            this.NetL0.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NetL0_KeyPress);
            // 
            // date1
            // 
            this.date1.Font = new System.Drawing.Font("Microsoft JhengHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.date1.Location = new System.Drawing.Point(193, 342);
            this.date1.MinDate = new System.DateTime(2023, 7, 6, 0, 0, 0, 0);
            this.date1.Name = "date1";
            this.date1.Size = new System.Drawing.Size(180, 25);
            this.date1.TabIndex = 28;
            this.date1.Value = new System.DateTime(2023, 7, 6, 0, 0, 0, 0);
            this.date1.ValueChanged += new System.EventHandler(this.date1_ValueChanged);
            // 
            // date2
            // 
            this.date2.Font = new System.Drawing.Font("Microsoft JhengHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.date2.Location = new System.Drawing.Point(594, 341);
            this.date2.Name = "date2";
            this.date2.Size = new System.Drawing.Size(169, 25);
            this.date2.TabIndex = 29;
            this.date2.Value = new System.DateTime(2023, 7, 7, 0, 0, 0, 0);
            this.date2.ValueChanged += new System.EventHandler(this.date2_ValueChanged);
            // 
            // label_cntry
            // 
            this.label_cntry.AutoSize = true;
            this.label_cntry.Font = new System.Drawing.Font("Candara", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_cntry.Location = new System.Drawing.Point(35, 432);
            this.label_cntry.Name = "label_cntry";
            this.label_cntry.Size = new System.Drawing.Size(58, 18);
            this.label_cntry.TabIndex = 30;
            this.label_cntry.Text = "Country";
            // 
            // country_name
            // 
            this.country_name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.country_name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.country_name.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.country_name.FormattingEnabled = true;
            this.country_name.Location = new System.Drawing.Point(193, 423);
            this.country_name.Name = "country_name";
            this.country_name.Size = new System.Drawing.Size(180, 27);
            this.country_name.TabIndex = 31;
            // 
            // label_product
            // 
            this.label_product.AutoSize = true;
            this.label_product.Font = new System.Drawing.Font("Candara", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_product.Location = new System.Drawing.Point(441, 432);
            this.label_product.Name = "label_product";
            this.label_product.Size = new System.Drawing.Size(58, 18);
            this.label_product.TabIndex = 32;
            this.label_product.Text = "Product";
            this.label_product.Click += new System.EventHandler(this.label_product_Click);
            // 
            // product_name
            // 
            this.product_name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.product_name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.product_name.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.product_name.FormattingEnabled = true;
            this.product_name.Items.AddRange(new object[] {
            "Product A",
            "Product B"});
            this.product_name.Location = new System.Drawing.Point(590, 423);
            this.product_name.Name = "product_name";
            this.product_name.Size = new System.Drawing.Size(169, 27);
            this.product_name.TabIndex = 33;
            this.product_name.SelectedIndexChanged += new System.EventHandler(this.product_name_SelectedIndexChanged);
            // 
            // label_status
            // 
            this.label_status.AutoSize = true;
            this.label_status.Font = new System.Drawing.Font("Candara", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_status.Location = new System.Drawing.Point(438, 103);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(96, 18);
            this.label_status.TabIndex = 34;
            this.label_status.Text = "Project Status";
            this.label_status.Click += new System.EventHandler(this.label14_Click);
            // 
            // product_status
            // 
            this.product_status.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.product_status.FormattingEnabled = true;
            this.product_status.Items.AddRange(new object[] {
            "In Progress",
            "Completed"});
            this.product_status.Location = new System.Drawing.Point(590, 94);
            this.product_status.Name = "product_status";
            this.product_status.Size = new System.Drawing.Size(169, 27);
            this.product_status.TabIndex = 35;
            this.product_status.SelectedIndexChanged += new System.EventHandler(this.product_status_SelectedIndexChanged);
            // 
            // SecoreL1
            // 
            this.SecoreL1.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SecoreL1.Location = new System.Drawing.Point(590, 493);
            this.SecoreL1.Name = "SecoreL1";
            this.SecoreL1.Size = new System.Drawing.Size(169, 27);
            this.SecoreL1.TabIndex = 37;
            this.SecoreL1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.SecoreL1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SecoreL1_KeyPress);
            // 
            // label_net1
            // 
            this.label_net1.AutoSize = true;
            this.label_net1.Font = new System.Drawing.Font("Candara", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_net1.Location = new System.Drawing.Point(441, 564);
            this.label_net1.Name = "label_net1";
            this.label_net1.Size = new System.Drawing.Size(53, 18);
            this.label_net1.TabIndex = 38;
            this.label_net1.Text = ".NET L1";
            this.label_net1.Click += new System.EventHandler(this.label_net1_Click);
            // 
            // label_sec0
            // 
            this.label_sec0.AutoSize = true;
            this.label_sec0.Font = new System.Drawing.Font("Candara", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_sec0.Location = new System.Drawing.Point(35, 499);
            this.label_sec0.Name = "label_sec0";
            this.label_sec0.Size = new System.Drawing.Size(71, 18);
            this.label_sec0.TabIndex = 10;
            this.label_sec0.Text = "Secore L0";
            this.label_sec0.Click += new System.EventHandler(this.label_sec0_Click);
            // 
            // SecoreL0
            // 
            this.SecoreL0.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SecoreL0.Location = new System.Drawing.Point(193, 491);
            this.SecoreL0.Name = "SecoreL0";
            this.SecoreL0.Size = new System.Drawing.Size(180, 27);
            this.SecoreL0.TabIndex = 12;
            this.SecoreL0.TextChanged += new System.EventHandler(this.SecoreL0_TextChanged);
            this.SecoreL0.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SecoreL0_KeyPress);
            // 
            // label_sec1
            // 
            this.label_sec1.AutoSize = true;
            this.label_sec1.Font = new System.Drawing.Font("Candara", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_sec1.Location = new System.Drawing.Point(441, 496);
            this.label_sec1.Name = "label_sec1";
            this.label_sec1.Size = new System.Drawing.Size(68, 18);
            this.label_sec1.TabIndex = 36;
            this.label_sec1.Text = "Secore L1";
            this.label_sec1.Click += new System.EventHandler(this.label15_Click);
            // 
            // NetL1
            // 
            this.NetL1.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NetL1.Location = new System.Drawing.Point(590, 559);
            this.NetL1.Name = "NetL1";
            this.NetL1.Size = new System.Drawing.Size(169, 27);
            this.NetL1.TabIndex = 39;
            this.NetL1.TextChanged += new System.EventHandler(this.NetL1_TextChanged);
            this.NetL1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NetL1_KeyPress);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(781, 70);
            this.panel1.TabIndex = 40;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(120, 66);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Font = new System.Drawing.Font("Candara", 15F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(441, 630);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(318, 38);
            this.button1.TabIndex = 41;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // Add_Project_Master
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(781, 696);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.NetL1);
            this.Controls.Add(this.label_net1);
            this.Controls.Add(this.SecoreL1);
            this.Controls.Add(this.label_sec1);
            this.Controls.Add(this.product_status);
            this.Controls.Add(this.label_status);
            this.Controls.Add(this.product_name);
            this.Controls.Add(this.label_product);
            this.Controls.Add(this.country_name);
            this.Controls.Add(this.label_cntry);
            this.Controls.Add(this.date2);
            this.Controls.Add(this.date1);
            this.Controls.Add(this.NetL0);
            this.Controls.Add(this.projecttype);
            this.Controls.Add(this.progId);
            this.Controls.Add(this.piac);
            this.Controls.Add(this.submit_newproject);
            this.Controls.Add(this.PM);
            this.Controls.Add(this.Description);
            this.Controls.Add(this.SecoreL0);
            this.Controls.Add(this.PTSID);
            this.Controls.Add(this.label_sec0);
            this.Controls.Add(this.label_net0);
            this.Controls.Add(this.label_pd);
            this.Controls.Add(this.label_pt);
            this.Controls.Add(this.label_pm);
            this.Controls.Add(this.label_pi);
            this.Controls.Add(this.label_cat);
            this.Controls.Add(this.label_date1);
            this.Controls.Add(this.label_date2);
            this.Controls.Add(this.label_ptsid);
            this.Font = new System.Drawing.Font("Candara", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Add_Project_Master";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_ptsid;
        private System.Windows.Forms.Label label_date2;
        private System.Windows.Forms.Label label_date1;
        private System.Windows.Forms.Label label_cat;
        private System.Windows.Forms.Label label_pi;
        private System.Windows.Forms.Label label_pm;
        private System.Windows.Forms.Label label_pt;
        private System.Windows.Forms.Label label_pd;
        private System.Windows.Forms.Label label_net0;
        private System.Windows.Forms.TextBox PTSID;
        private System.Windows.Forms.TextBox Description;
        private System.Windows.Forms.ComboBox PM;
        private System.Windows.Forms.Button submit_newproject;
        private System.Windows.Forms.ComboBox piac;
        private System.Windows.Forms.ComboBox progId;
        private System.Windows.Forms.ComboBox projecttype;
        private System.Windows.Forms.TextBox NetL0;
        private System.Windows.Forms.DateTimePicker date1;
        private System.Windows.Forms.DateTimePicker date2;
        private System.Windows.Forms.Label label_cntry;
        private System.Windows.Forms.ComboBox country_name;
        private System.Windows.Forms.Label label_product;
        private System.Windows.Forms.ComboBox product_name;
        private System.Windows.Forms.Label label_status;
        private System.Windows.Forms.ComboBox product_status;
        private System.Windows.Forms.TextBox SecoreL1;
        private System.Windows.Forms.Label label_net1;
        private System.Windows.Forms.Label label_sec0;
        private System.Windows.Forms.TextBox SecoreL0;
        private System.Windows.Forms.Label label_sec1;
        private System.Windows.Forms.TextBox NetL1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}