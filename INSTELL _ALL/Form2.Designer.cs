
namespace InstallPrograms
{
    partial class SqlSettingsForm
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
            this.btn_save = new System.Windows.Forms.Button();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.txt_pass = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_save
            // 
            this.btn_save.BackColor = System.Drawing.Color.OliveDrab;
            this.btn_save.Font = new System.Drawing.Font("LBC", 12F);
            this.btn_save.Location = new System.Drawing.Point(319, 109);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(104, 40);
            this.btn_save.TabIndex = 0;
            this.btn_save.Text = "حفظ";
            this.btn_save.UseVisualStyleBackColor = false;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // txt_name
            // 
            this.txt_name.Location = new System.Drawing.Point(12, 12);
            this.txt_name.Multiline = true;
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(317, 36);
            this.txt_name.TabIndex = 1;
            // 
            // txt_pass
            // 
            this.txt_pass.Location = new System.Drawing.Point(12, 63);
            this.txt_pass.Multiline = true;
            this.txt_pass.Name = "txt_pass";
            this.txt_pass.Size = new System.Drawing.Size(317, 36);
            this.txt_pass.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("LBC", 12F);
            this.label1.Location = new System.Drawing.Point(335, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "كلمة المرور";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("LBC", 12F);
            this.label2.Location = new System.Drawing.Point(335, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "اسم السيرفر";
            // 
            // FRM_ST
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 161);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_pass);
            this.Controls.Add(this.txt_name);
            this.Controls.Add(this.btn_save);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FRM_ST";
            this.Text = "اعدادات sql server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.TextBox txt_pass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}