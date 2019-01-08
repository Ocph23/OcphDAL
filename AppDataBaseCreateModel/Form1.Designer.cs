namespace AppDataBaseCreateModel
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
            this.button1 = new System.Windows.Forms.Button();
            this.GroupModel = new System.Windows.Forms.GroupBox();
            this.Inpc = new System.Windows.Forms.CheckBox();
            this.aClass1File = new System.Windows.Forms.CheckBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.Custom = new System.Windows.Forms.RadioButton();
            this.OcphDal = new System.Windows.Forms.RadioButton();
            this.Poco = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Clasic = new System.Windows.Forms.RadioButton();
            this.Modern = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Header = new System.Windows.Forms.RichTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Body = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.namespacess = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.java = new System.Windows.Forms.RadioButton();
            this.php = new System.Windows.Forms.RadioButton();
            this.typeScript = new System.Windows.Forms.RadioButton();
            this.cSharp = new System.Windows.Forms.RadioButton();
            this.FolderPath = new System.Windows.Forms.TextBox();
            this.GroupModel.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(22, 329);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(816, 46);
            this.button1.TabIndex = 0;
            this.button1.Text = "Setting";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // GroupModel
            // 
            this.GroupModel.Controls.Add(this.Inpc);
            this.GroupModel.Controls.Add(this.aClass1File);
            this.GroupModel.Controls.Add(this.groupBox6);
            this.GroupModel.Controls.Add(this.groupBox1);
            this.GroupModel.Location = new System.Drawing.Point(210, 12);
            this.GroupModel.Name = "GroupModel";
            this.GroupModel.Size = new System.Drawing.Size(257, 175);
            this.GroupModel.TabIndex = 1;
            this.GroupModel.TabStop = false;
            this.GroupModel.Text = "Options";
            // 
            // Inpc
            // 
            this.Inpc.AutoSize = true;
            this.Inpc.Location = new System.Drawing.Point(17, 143);
            this.Inpc.Name = "Inpc";
            this.Inpc.Size = new System.Drawing.Size(157, 17);
            this.Inpc.TabIndex = 11;
            this.Inpc.Text = "With INotifyPropertyChange";
            this.Inpc.UseVisualStyleBackColor = true;
            // 
            // aClass1File
            // 
            this.aClass1File.AutoSize = true;
            this.aClass1File.Location = new System.Drawing.Point(17, 120);
            this.aClass1File.Name = "aClass1File";
            this.aClass1File.Size = new System.Drawing.Size(88, 17);
            this.aClass1File.TabIndex = 10;
            this.aClass1File.Text = "1 Class 1 File";
            this.aClass1File.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.Custom);
            this.groupBox6.Controls.Add(this.OcphDal);
            this.groupBox6.Controls.Add(this.Poco);
            this.groupBox6.Location = new System.Drawing.Point(17, 19);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(104, 95);
            this.groupBox6.TabIndex = 9;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Model";
            // 
            // Custom
            // 
            this.Custom.AutoSize = true;
            this.Custom.Location = new System.Drawing.Point(17, 65);
            this.Custom.Name = "Custom";
            this.Custom.Size = new System.Drawing.Size(60, 17);
            this.Custom.TabIndex = 5;
            this.Custom.TabStop = true;
            this.Custom.Text = "Custom";
            this.Custom.UseVisualStyleBackColor = true;
            // 
            // OcphDal
            // 
            this.OcphDal.AutoSize = true;
            this.OcphDal.Location = new System.Drawing.Point(17, 41);
            this.OcphDal.Name = "OcphDal";
            this.OcphDal.Size = new System.Drawing.Size(72, 17);
            this.OcphDal.TabIndex = 4;
            this.OcphDal.TabStop = true;
            this.OcphDal.Text = "OcphDAL";
            this.OcphDal.UseVisualStyleBackColor = true;
            // 
            // Poco
            // 
            this.Poco.AutoSize = true;
            this.Poco.Location = new System.Drawing.Point(17, 19);
            this.Poco.Name = "Poco";
            this.Poco.Size = new System.Drawing.Size(50, 17);
            this.Poco.TabIndex = 3;
            this.Poco.TabStop = true;
            this.Poco.Text = "Poco";
            this.Poco.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Clasic);
            this.groupBox1.Controls.Add(this.Modern);
            this.groupBox1.Location = new System.Drawing.Point(128, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(103, 91);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Type";
            // 
            // Clasic
            // 
            this.Clasic.AutoSize = true;
            this.Clasic.Location = new System.Drawing.Point(17, 44);
            this.Clasic.Name = "Clasic";
            this.Clasic.Size = new System.Drawing.Size(53, 17);
            this.Clasic.TabIndex = 1;
            this.Clasic.TabStop = true;
            this.Clasic.Text = "Clasic";
            this.Clasic.UseVisualStyleBackColor = true;
            // 
            // Modern
            // 
            this.Modern.AutoSize = true;
            this.Modern.Location = new System.Drawing.Point(17, 21);
            this.Modern.Name = "Modern";
            this.Modern.Size = new System.Drawing.Size(61, 17);
            this.Modern.TabIndex = 0;
            this.Modern.TabStop = true;
            this.Modern.Text = "Modern";
            this.Modern.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Header);
            this.groupBox2.Location = new System.Drawing.Point(482, 11);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(355, 175);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Header";
            // 
            // Header
            // 
            this.Header.Location = new System.Drawing.Point(17, 19);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(249, 142);
            this.Header.TabIndex = 4;
            this.Header.Text = "";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Body);
            this.groupBox3.Location = new System.Drawing.Point(22, 388);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(682, 324);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Preview";
            // 
            // Body
            // 
            this.Body.Location = new System.Drawing.Point(17, 26);
            this.Body.Name = "Body";
            this.Body.Size = new System.Drawing.Size(648, 280);
            this.Body.TabIndex = 4;
            this.Body.Text = "";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(714, 395);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(123, 317);
            this.button2.TabIndex = 6;
            this.button2.Text = "Proccess";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.namespacess);
            this.groupBox4.Location = new System.Drawing.Point(22, 201);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(445, 54);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Namespace";
            // 
            // namespacess
            // 
            this.namespacess.Location = new System.Drawing.Point(17, 19);
            this.namespacess.Name = "namespacess";
            this.namespacess.Size = new System.Drawing.Size(402, 20);
            this.namespacess.TabIndex = 5;
            this.namespacess.Text = "Namespace";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.FolderPath);
            this.groupBox5.Controls.Add(this.button3);
            this.groupBox5.Location = new System.Drawing.Point(22, 261);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(446, 63);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Save To ..";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(373, 15);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(46, 32);
            this.button3.TabIndex = 7;
            this.button3.Text = "Folder";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.java);
            this.groupBox8.Controls.Add(this.php);
            this.groupBox8.Controls.Add(this.typeScript);
            this.groupBox8.Controls.Add(this.cSharp);
            this.groupBox8.Location = new System.Drawing.Point(12, 18);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(192, 169);
            this.groupBox8.TabIndex = 10;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Programming Language";
            // 
            // java
            // 
            this.java.AutoSize = true;
            this.java.Location = new System.Drawing.Point(17, 88);
            this.java.Name = "java";
            this.java.Size = new System.Drawing.Size(128, 17);
            this.java.TabIndex = 6;
            this.java.Text = "Java (Comming Soon)";
            this.java.UseVisualStyleBackColor = true;
            // 
            // php
            // 
            this.php.AutoSize = true;
            this.php.Location = new System.Drawing.Point(17, 65);
            this.php.Name = "php";
            this.php.Size = new System.Drawing.Size(127, 17);
            this.php.TabIndex = 5;
            this.php.Text = "PHP (Comming Soon)";
            this.php.UseVisualStyleBackColor = true;
            // 
            // typeScript
            // 
            this.typeScript.AutoSize = true;
            this.typeScript.Location = new System.Drawing.Point(17, 41);
            this.typeScript.Name = "typeScript";
            this.typeScript.Size = new System.Drawing.Size(76, 17);
            this.typeScript.TabIndex = 4;
            this.typeScript.Text = "TypeScript";
            this.typeScript.UseVisualStyleBackColor = true;
            // 
            // cSharp
            // 
            this.cSharp.AutoSize = true;
            this.cSharp.Checked = true;
            this.cSharp.Location = new System.Drawing.Point(17, 19);
            this.cSharp.Name = "cSharp";
            this.cSharp.Size = new System.Drawing.Size(42, 17);
            this.cSharp.TabIndex = 3;
            this.cSharp.TabStop = true;
            this.cSharp.Text = "C #";
            this.cSharp.UseVisualStyleBackColor = true;
            // 
            // FolderPath
            // 
            this.FolderPath.Location = new System.Drawing.Point(22, 21);
            this.FolderPath.Name = "FolderPath";
            this.FolderPath.Size = new System.Drawing.Size(340, 20);
            this.FolderPath.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 743);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.GroupModel);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "OcphDAL MySql Generate Model";
            this.GroupModel.ResumeLayout(false);
            this.GroupModel.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox GroupModel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox Header;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox Body;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton Clasic;
        private System.Windows.Forms.RadioButton Modern;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton Custom;
        private System.Windows.Forms.RadioButton OcphDal;
        private System.Windows.Forms.RadioButton Poco;
        private System.Windows.Forms.CheckBox Inpc;
        private System.Windows.Forms.CheckBox aClass1File;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.RadioButton java;
        private System.Windows.Forms.RadioButton php;
        private System.Windows.Forms.RadioButton typeScript;
        private System.Windows.Forms.RadioButton cSharp;
        private System.Windows.Forms.TextBox namespacess;
        private System.Windows.Forms.TextBox FolderPath;
    }
}

