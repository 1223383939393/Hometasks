namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            printButton = new Button();
            listBox1 = new ListBox();
            comboBox1 = new ComboBox();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            groupBox1 = new GroupBox();
            label5 = new Label();
            groupBox2 = new GroupBox();
            button4 = new Button();
            textBox5 = new TextBox();
            label6 = new Label();
            groupBox3 = new GroupBox();
            textBox6 = new TextBox();
            textBox7 = new TextBox();
            textBox8 = new TextBox();
            textBox9 = new TextBox();
            dateTimePicker1 = new DateTimePicker();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            groupBox4 = new GroupBox();
            groupBox5 = new GroupBox();
            groupBox6 = new GroupBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox6.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(101, 181);
            button1.Name = "button1";
            button1.Size = new Size(144, 23);
            button1.TabIndex = 0;
            button1.Text = "Добавить клиента";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(22, 51);
            button2.Name = "button2";
            button2.Size = new Size(144, 23);
            button2.TabIndex = 1;
            button2.Text = "Удалить клиента";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(222, 54);
            button3.Name = "button3";
            button3.Size = new Size(187, 23);
            button3.TabIndex = 2;
            button3.Text = "Обновить телефон или почту";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // printButton
            // 
            printButton.Location = new Point(17, 26);
            printButton.Name = "printButton";
            printButton.Size = new Size(234, 23);
            printButton.TabIndex = 3;
            printButton.Text = "Вывести активных клиентов старше 21 года";
            printButton.UseVisualStyleBackColor = true;
            printButton.Click += printButton_Click;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(17, 55);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(453, 304);
            listBox1.TabIndex = 4;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Телефон", "Почта" });
            comboBox1.Location = new Point(82, 12);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 23);
            comboBox1.TabIndex = 5;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(64, 22);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(130, 23);
            textBox1.TabIndex = 6;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(64, 77);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(130, 23);
            textBox2.TabIndex = 7;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(40, 25);
            label1.Name = "label1";
            label1.Size = new Size(18, 15);
            label1.TabIndex = 8;
            label1.Text = "ID";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 80);
            label2.Name = "label2";
            label2.Size = new Size(55, 15);
            label2.TabIndex = 9;
            label2.Text = "Телефон";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(14, 81);
            label3.Name = "label3";
            label3.Size = new Size(41, 15);
            label3.TabIndex = 13;
            label3.Text = "Почта";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(37, 25);
            label4.Name = "label4";
            label4.Size = new Size(18, 15);
            label4.TabIndex = 12;
            label4.Text = "ID";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(61, 81);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(130, 23);
            textBox3.TabIndex = 11;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(61, 22);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(130, 23);
            textBox4.TabIndex = 10;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(textBox2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label2);
            groupBox1.Location = new Point(12, 41);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(200, 145);
            groupBox1.TabIndex = 14;
            groupBox1.TabStop = false;
            groupBox1.Text = "groupBox1";
            groupBox1.Visible = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(64, 103);
            label5.Name = "label5";
            label5.Size = new Size(0, 15);
            label5.TabIndex = 10;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(textBox4);
            groupBox2.Controls.Add(textBox3);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(label4);
            groupBox2.Location = new Point(15, 192);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(200, 133);
            groupBox2.TabIndex = 15;
            groupBox2.TabStop = false;
            groupBox2.Text = "groupBox2";
            groupBox2.Visible = false;
            // 
            // button4
            // 
            button4.Location = new Point(222, 12);
            button4.Name = "button4";
            button4.Size = new Size(142, 23);
            button4.TabIndex = 16;
            button4.Text = "Подтвердить выбор";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(46, 22);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(118, 23);
            textBox5.TabIndex = 17;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(22, 25);
            label6.Name = "label6";
            label6.Size = new Size(18, 15);
            label6.TabIndex = 11;
            label6.Text = "ID";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(textBox5);
            groupBox3.Controls.Add(label6);
            groupBox3.Controls.Add(button2);
            groupBox3.Location = new Point(817, 22);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(200, 100);
            groupBox3.TabIndex = 18;
            groupBox3.TabStop = false;
            groupBox3.Text = "groupBox3";
            // 
            // textBox6
            // 
            textBox6.Location = new Point(101, 20);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(100, 23);
            textBox6.TabIndex = 19;
            // 
            // textBox7
            // 
            textBox7.Location = new Point(101, 53);
            textBox7.Name = "textBox7";
            textBox7.Size = new Size(100, 23);
            textBox7.TabIndex = 20;
            // 
            // textBox8
            // 
            textBox8.Location = new Point(101, 85);
            textBox8.Name = "textBox8";
            textBox8.Size = new Size(100, 23);
            textBox8.TabIndex = 21;
            // 
            // textBox9
            // 
            textBox9.Location = new Point(101, 114);
            textBox9.Name = "textBox9";
            textBox9.Size = new Size(100, 23);
            textBox9.TabIndex = 22;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(101, 143);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(200, 23);
            dateTimePicker1.TabIndex = 23;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(64, 23);
            label7.Name = "label7";
            label7.Size = new Size(31, 15);
            label7.TabIndex = 18;
            label7.Text = "Имя";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(40, 56);
            label8.Name = "label8";
            label8.Size = new Size(58, 15);
            label8.TabIndex = 24;
            label8.Text = "Фамилия";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(40, 88);
            label9.Name = "label9";
            label9.Size = new Size(55, 15);
            label9.TabIndex = 25;
            label9.Text = "Телефон";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(56, 117);
            label10.Name = "label10";
            label10.Size = new Size(41, 15);
            label10.TabIndex = 26;
            label10.Text = "Почта";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(9, 149);
            label11.Name = "label11";
            label11.Size = new Size(90, 15);
            label11.TabIndex = 27;
            label11.Text = "Дата рождения";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(textBox7);
            groupBox4.Controls.Add(label11);
            groupBox4.Controls.Add(button1);
            groupBox4.Controls.Add(label10);
            groupBox4.Controls.Add(textBox6);
            groupBox4.Controls.Add(label9);
            groupBox4.Controls.Add(textBox8);
            groupBox4.Controls.Add(label8);
            groupBox4.Controls.Add(textBox9);
            groupBox4.Controls.Add(label7);
            groupBox4.Controls.Add(dateTimePicker1);
            groupBox4.Location = new Point(494, 28);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(317, 223);
            groupBox4.TabIndex = 28;
            groupBox4.TabStop = false;
            groupBox4.Text = "groupBox4";
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(listBox1);
            groupBox5.Controls.Add(printButton);
            groupBox5.Location = new Point(12, 12);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(476, 394);
            groupBox5.TabIndex = 29;
            groupBox5.TabStop = false;
            groupBox5.Text = "groupBox5";
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(comboBox1);
            groupBox6.Controls.Add(button3);
            groupBox6.Controls.Add(groupBox1);
            groupBox6.Controls.Add(groupBox2);
            groupBox6.Controls.Add(button4);
            groupBox6.Location = new Point(1032, 14);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(444, 343);
            groupBox6.TabIndex = 30;
            groupBox6.TabStop = false;
            groupBox6.Text = "groupBox6";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientActiveCaption;
            ClientSize = new Size(1488, 629);
            Controls.Add(groupBox6);
            Controls.Add(groupBox5);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Name = "Form1";
            Text = "Form1";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox6.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private Button printButton;
        private ListBox listBox1;
        private ComboBox comboBox1;
        private TextBox textBox1;
        private TextBox textBox2;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox textBox3;
        private TextBox textBox4;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label5;
        private Button button4;
        private TextBox textBox5;
        private Label label6;
        private GroupBox groupBox3;
        private TextBox textBox6;
        private TextBox textBox7;
        private TextBox textBox8;
        private TextBox textBox9;
        private DateTimePicker dateTimePicker1;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private GroupBox groupBox4;
        private GroupBox groupBox5;
        private GroupBox groupBox6;
    }
}
