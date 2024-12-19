namespace TabControlProject
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tCStudents = new System.Windows.Forms.TabControl();
            this.tpHome = new System.Windows.Forms.TabPage();
            this.btnShow = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.tpAddStudent = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAddStudentInfo = new System.Windows.Forms.Button();
            this.gbGender = new System.Windows.Forms.GroupBox();
            this.rbFemale = new System.Windows.Forms.RadioButton();
            this.rbMale = new System.Windows.Forms.RadioButton();
            this.txtAge = new System.Windows.Forms.TextBox();
            this.cBMajors = new System.Windows.Forms.ComboBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.LVStudents = new System.Windows.Forms.ListView();
            this.CHName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CHAGE = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CHcollege = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tCStudents.SuspendLayout();
            this.tpHome.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.tpAddStudent.SuspendLayout();
            this.gbGender.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // tCStudents
            // 
            this.tCStudents.Controls.Add(this.tpHome);
            this.tCStudents.Controls.Add(this.tpAddStudent);
            this.tCStudents.Controls.Add(this.tabPage3);
            this.tCStudents.ImageList = this.imageList1;
            this.tCStudents.Location = new System.Drawing.Point(12, 2);
            this.tCStudents.Name = "tCStudents";
            this.tCStudents.SelectedIndex = 0;
            this.tCStudents.Size = new System.Drawing.Size(795, 446);
            this.tCStudents.TabIndex = 0;
            // 
            // tpHome
            // 
            this.tpHome.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tpHome.Controls.Add(this.btnShow);
            this.tpHome.Controls.Add(this.btnAdd);
            this.tpHome.Controls.Add(this.label2);
            this.tpHome.Controls.Add(this.pictureBox2);
            this.tpHome.ImageIndex = 2;
            this.tpHome.Location = new System.Drawing.Point(4, 25);
            this.tpHome.Name = "tpHome";
            this.tpHome.Padding = new System.Windows.Forms.Padding(3);
            this.tpHome.Size = new System.Drawing.Size(787, 417);
            this.tpHome.TabIndex = 0;
            this.tpHome.Text = "Home";
            // 
            // btnShow
            // 
            this.btnShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShow.Location = new System.Drawing.Point(403, 333);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(146, 29);
            this.btnShow.TabIndex = 7;
            this.btnShow.Text = "Show Students List";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(237, 333);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(146, 29);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Add Student";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(353, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 29);
            this.label2.TabIndex = 5;
            this.label2.Text = "Home";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::TabControlProject.Properties.Resources.home_icon_icons_com_73532;
            this.pictureBox2.Location = new System.Drawing.Point(265, 105);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(250, 198);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // tpAddStudent
            // 
            this.tpAddStudent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tpAddStudent.Controls.Add(this.label4);
            this.tpAddStudent.Controls.Add(this.label3);
            this.tpAddStudent.Controls.Add(this.btnAddStudentInfo);
            this.tpAddStudent.Controls.Add(this.gbGender);
            this.tpAddStudent.Controls.Add(this.txtAge);
            this.tpAddStudent.Controls.Add(this.cBMajors);
            this.tpAddStudent.Controls.Add(this.txtName);
            this.tpAddStudent.Controls.Add(this.label1);
            this.tpAddStudent.ImageIndex = 0;
            this.tpAddStudent.Location = new System.Drawing.Point(4, 25);
            this.tpAddStudent.Name = "tpAddStudent";
            this.tpAddStudent.Padding = new System.Windows.Forms.Padding(3);
            this.tpAddStudent.Size = new System.Drawing.Size(787, 417);
            this.tpAddStudent.TabIndex = 1;
            this.tpAddStudent.Text = "Add Student";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(33, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 22);
            this.label4.TabIndex = 9;
            this.label4.Text = "College";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(33, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 22);
            this.label3.TabIndex = 8;
            this.label3.Text = "Age";
            // 
            // btnAddStudentInfo
            // 
            this.btnAddStudentInfo.BackColor = System.Drawing.Color.White;
            this.btnAddStudentInfo.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnAddStudentInfo.FlatAppearance.BorderSize = 3;
            this.btnAddStudentInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddStudentInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddStudentInfo.Location = new System.Drawing.Point(132, 335);
            this.btnAddStudentInfo.Name = "btnAddStudentInfo";
            this.btnAddStudentInfo.Size = new System.Drawing.Size(166, 40);
            this.btnAddStudentInfo.TabIndex = 7;
            this.btnAddStudentInfo.Text = "Add Student";
            this.btnAddStudentInfo.UseVisualStyleBackColor = false;
            this.btnAddStudentInfo.Click += new System.EventHandler(this.btnAddStudentInfo_Click);
            // 
            // gbGender
            // 
            this.gbGender.Controls.Add(this.rbFemale);
            this.gbGender.Controls.Add(this.rbMale);
            this.gbGender.Location = new System.Drawing.Point(60, 200);
            this.gbGender.Name = "gbGender";
            this.gbGender.Size = new System.Drawing.Size(300, 103);
            this.gbGender.TabIndex = 4;
            this.gbGender.TabStop = false;
            this.gbGender.Text = "Gender";
            // 
            // rbFemale
            // 
            this.rbFemale.AutoSize = true;
            this.rbFemale.Location = new System.Drawing.Point(185, 47);
            this.rbFemale.Name = "rbFemale";
            this.rbFemale.Size = new System.Drawing.Size(59, 17);
            this.rbFemale.TabIndex = 6;
            this.rbFemale.TabStop = true;
            this.rbFemale.Text = "Female";
            this.rbFemale.UseVisualStyleBackColor = true;
            // 
            // rbMale
            // 
            this.rbMale.AutoSize = true;
            this.rbMale.Location = new System.Drawing.Point(51, 47);
            this.rbMale.Name = "rbMale";
            this.rbMale.Size = new System.Drawing.Size(47, 17);
            this.rbMale.TabIndex = 5;
            this.rbMale.TabStop = true;
            this.rbMale.Text = "Male";
            this.rbMale.UseVisualStyleBackColor = true;
            // 
            // txtAge
            // 
            this.txtAge.Location = new System.Drawing.Point(111, 147);
            this.txtAge.Name = "txtAge";
            this.txtAge.Size = new System.Drawing.Size(210, 20);
            this.txtAge.TabIndex = 3;
            this.txtAge.Validating += new System.ComponentModel.CancelEventHandler(this.txtAge_Validating);
            // 
            // cBMajors
            // 
            this.cBMajors.FormattingEnabled = true;
            this.cBMajors.Items.AddRange(new object[] {
            "IT",
            "Engineering",
            "Finance",
            "Medicine",
            "Law"});
            this.cBMajors.Location = new System.Drawing.Point(111, 95);
            this.cBMajors.Name = "cBMajors";
            this.cBMajors.Size = new System.Drawing.Size(210, 21);
            this.cBMajors.TabIndex = 2;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(111, 44);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(210, 20);
            this.txtName.TabIndex = 1;
            this.txtName.Validating += new System.ComponentModel.CancelEventHandler(this.txtName_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(33, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tabPage3.Controls.Add(this.LVStudents);
            this.tabPage3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage3.ImageIndex = 1;
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(787, 417);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Students List";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "iconfinder-499-student-education-graduate-learning-4212915_114945.png");
            this.imageList1.Images.SetKeyName(1, "list-symbol-of-three-items-with-dots_icon-icons.com_72994.png");
            this.imageList1.Images.SetKeyName(2, "home_icon-icons.com_73532.png");
            this.imageList1.Images.SetKeyName(3, "bar_chart_22178.png");
            this.imageList1.Images.SetKeyName(4, "Book.png");
            this.imageList1.Images.SetKeyName(5, "Boy.png");
            this.imageList1.Images.SetKeyName(6, "download_folder_file_icon_219533.png");
            this.imageList1.Images.SetKeyName(7, "Girl.png");
            this.imageList1.Images.SetKeyName(8, "Pen.png");
            this.imageList1.Images.SetKeyName(9, "Strong.png");
            this.imageList1.Images.SetKeyName(10, "Weak.png");
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // LVStudents
            // 
            this.LVStudents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.CHName,
            this.CHAGE,
            this.CHcollege});
            this.LVStudents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LVStudents.HideSelection = false;
            this.LVStudents.Location = new System.Drawing.Point(3, 3);
            this.LVStudents.Name = "LVStudents";
            this.LVStudents.Size = new System.Drawing.Size(781, 411);
            this.LVStudents.SmallImageList = this.imageList1;
            this.LVStudents.TabIndex = 0;
            this.LVStudents.UseCompatibleStateImageBehavior = false;
            this.LVStudents.View = System.Windows.Forms.View.Details;
            // 
            // CHName
            // 
            this.CHName.Text = "Name";
            this.CHName.Width = 88;
            // 
            // CHAGE
            // 
            this.CHAGE.Text = "Age";
            this.CHAGE.Width = 86;
            // 
            // CHcollege
            // 
            this.CHcollege.Text = "College";
            this.CHcollege.Width = 90;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tCStudents);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tCStudents.ResumeLayout(false);
            this.tpHome.ResumeLayout(false);
            this.tpHome.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.tpAddStudent.ResumeLayout(false);
            this.tpAddStudent.PerformLayout();
            this.gbGender.ResumeLayout(false);
            this.gbGender.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tCStudents;
        private System.Windows.Forms.TabPage tpHome;
        private System.Windows.Forms.TabPage tpAddStudent;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ComboBox cBMajors;
        private System.Windows.Forms.GroupBox gbGender;
        private System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.RadioButton rbMale;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbFemale;
        private System.Windows.Forms.Button btnAddStudentInfo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ListView LVStudents;
        private System.Windows.Forms.ColumnHeader CHName;
        private System.Windows.Forms.ColumnHeader CHAGE;
        private System.Windows.Forms.ColumnHeader CHcollege;
    }
}

