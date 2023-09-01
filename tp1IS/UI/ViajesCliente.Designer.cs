
namespace UI
{
    partial class ViajesCliente
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.metroButton2 = new MetroFramework.Controls.MetroButton();
            this.metroButton3 = new MetroFramework.Controls.MetroButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.barrarProgreso1 = new UI.controles.BarrarProgreso();
            this.userControl11 = new UI.controles.UserControl1();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(112, 117);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(502, 194);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.formating);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(112, 95);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(86, 19);
            this.metroLabel1.TabIndex = 1;
            this.metroLabel1.Text = "pending trips";
            // 
            // metroButton1
            // 
            this.metroButton1.Location = new System.Drawing.Point(550, 317);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(29, 23);
            this.metroButton1.TabIndex = 2;
            this.metroButton1.Text = "<";
            this.metroButton1.UseSelectable = true;
            // 
            // metroButton2
            // 
            this.metroButton2.Location = new System.Drawing.Point(585, 317);
            this.metroButton2.Name = "metroButton2";
            this.metroButton2.Size = new System.Drawing.Size(29, 23);
            this.metroButton2.TabIndex = 3;
            this.metroButton2.Text = ">";
            this.metroButton2.UseSelectable = true;
            // 
            // metroButton3
            // 
            this.metroButton3.Location = new System.Drawing.Point(112, 365);
            this.metroButton3.Name = "metroButton3";
            this.metroButton3.Size = new System.Drawing.Size(105, 23);
            this.metroButton3.TabIndex = 5;
            this.metroButton3.Text = "Write a message";
            this.metroButton3.UseSelectable = true;
            this.metroButton3.Click += new System.EventHandler(this.metroButton3_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(479, 373);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(200, 20);
            this.textBox1.TabIndex = 6;
            // 
            // barrarProgreso1
            // 
            this.barrarProgreso1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.barrarProgreso1.Location = new System.Drawing.Point(660, 132);
            this.barrarProgreso1.Name = "barrarProgreso1";
            this.barrarProgreso1.ProgressValue = 0;
            this.barrarProgreso1.Size = new System.Drawing.Size(243, 32);
            this.barrarProgreso1.TabIndex = 8;
            // 
            // userControl11
            // 
            this.userControl11.BackColor = System.Drawing.Color.LightCoral;
            this.userControl11.BorderColor = System.Drawing.Color.DarkRed;
            this.userControl11.BorderFocusColor = System.Drawing.Color.HotPink;
            this.userControl11.BorderSize = 2;
            this.userControl11.Location = new System.Drawing.Point(223, 365);
            this.userControl11.Multiline = false;
            this.userControl11.Name = "userControl11";
            this.userControl11.Padding = new System.Windows.Forms.Padding(7);
            this.userControl11.PasswordChar = false;
            this.userControl11.Size = new System.Drawing.Size(250, 28);
            this.userControl11.TabIndex = 7;
            this.userControl11.Texts = "";
            this.userControl11.UnderlinedStyle = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(657, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Estado del viaje:";
            // 
            // ViajesCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.barrarProgreso1);
            this.Controls.Add(this.userControl11);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.metroButton3);
            this.Controls.Add(this.metroButton2);
            this.Controls.Add(this.metroButton1);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "ViajesCliente";
            this.Text = "my travels";
            this.Load += new System.EventHandler(this.ViajesCliente_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroButton metroButton1;
        private MetroFramework.Controls.MetroButton metroButton2;
        private MetroFramework.Controls.MetroButton metroButton3;
        private System.Windows.Forms.TextBox textBox1;
        private controles.UserControl1 userControl11;
        private controles.BarrarProgreso barrarProgreso1;
        private System.Windows.Forms.Label label1;
    }
}