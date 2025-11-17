
namespace InstallPrograms
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
            this.btnInstall = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.textBoxFolderPath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.listViewPrograms = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.lblCounter = new System.Windows.Forms.Label();
            this.btnClearList = new System.Windows.Forms.Button();
            this.btnApplySystemSetup = new System.Windows.Forms.Button();
            this.btnLoadList = new System.Windows.Forms.Button();
            this.btnSaveList = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnInstall
            // 
            this.btnInstall.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnInstall.Font = new System.Drawing.Font("LBC", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnInstall.Location = new System.Drawing.Point(542, 43);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(138, 54);
            this.btnInstall.TabIndex = 1;
            this.btnInstall.Text = "تتبيت البرامج";
            this.btnInstall.UseVisualStyleBackColor = false;
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.progressBar1.Location = new System.Drawing.Point(12, 352);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(522, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 2;
            // 
            // textBoxFolderPath
            // 
            this.textBoxFolderPath.Location = new System.Drawing.Point(12, 241);
            this.textBoxFolderPath.Name = "textBoxFolderPath";
            this.textBoxFolderPath.Size = new System.Drawing.Size(522, 20);
            this.textBoxFolderPath.TabIndex = 3;
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnBrowse.Font = new System.Drawing.Font("LBC", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnBrowse.Location = new System.Drawing.Point(542, 103);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(138, 50);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "اختر المجلد";
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // textBoxLog
            // 
            this.textBoxLog.Location = new System.Drawing.Point(12, 267);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLog.Size = new System.Drawing.Size(522, 79);
            this.textBoxLog.TabIndex = 3;
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Font = new System.Drawing.Font("LBC", 12F);
            this.chkSelectAll.Location = new System.Drawing.Point(571, 12);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(90, 25);
            this.chkSelectAll.TabIndex = 6;
            this.chkSelectAll.Text = "اختيار الكل";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // listViewPrograms
            // 
            this.listViewPrograms.CheckBoxes = true;
            this.listViewPrograms.HideSelection = false;
            this.listViewPrograms.Location = new System.Drawing.Point(12, 11);
            this.listViewPrograms.Name = "listViewPrograms";
            this.listViewPrograms.Size = new System.Drawing.Size(522, 224);
            this.listViewPrograms.SmallImageList = this.imageList1;
            this.listViewPrograms.TabIndex = 7;
            this.listViewPrograms.UseCompatibleStateImageBehavior = false;
            this.listViewPrograms.View = System.Windows.Forms.View.List;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("LBC", 12F);
            this.label4.Location = new System.Drawing.Point(725, 297);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 21);
            this.label4.TabIndex = 4;
            this.label4.Text = "عدد البرامج";
            // 
            // lblCounter
            // 
            this.lblCounter.AutoSize = true;
            this.lblCounter.Font = new System.Drawing.Font("LBC", 12F);
            this.lblCounter.Location = new System.Drawing.Point(703, 297);
            this.lblCounter.Name = "lblCounter";
            this.lblCounter.Size = new System.Drawing.Size(19, 21);
            this.lblCounter.TabIndex = 4;
            this.lblCounter.Text = "0";
            // 
            // btnClearList
            // 
            this.btnClearList.BackColor = System.Drawing.Color.Crimson;
            this.btnClearList.Font = new System.Drawing.Font("LBC", 12F);
            this.btnClearList.Location = new System.Drawing.Point(542, 159);
            this.btnClearList.Name = "btnClearList";
            this.btnClearList.Size = new System.Drawing.Size(138, 51);
            this.btnClearList.TabIndex = 8;
            this.btnClearList.Text = "مسح الكل ";
            this.btnClearList.UseVisualStyleBackColor = false;
            this.btnClearList.Click += new System.EventHandler(this.btnClearList_Click);
            // 
            // btnApplySystemSetup
            // 
            this.btnApplySystemSetup.BackColor = System.Drawing.Color.Khaki;
            this.btnApplySystemSetup.Font = new System.Drawing.Font("LBC", 12F);
            this.btnApplySystemSetup.Location = new System.Drawing.Point(542, 216);
            this.btnApplySystemSetup.Name = "btnApplySystemSetup";
            this.btnApplySystemSetup.Size = new System.Drawing.Size(138, 51);
            this.btnApplySystemSetup.TabIndex = 10;
            this.btnApplySystemSetup.Text = "تطبيق الاعدادات";
            this.btnApplySystemSetup.UseVisualStyleBackColor = false;
            // 
            // btnLoadList
            // 
            this.btnLoadList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnLoadList.Font = new System.Drawing.Font("LBC", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnLoadList.Location = new System.Drawing.Point(686, 43);
            this.btnLoadList.Name = "btnLoadList";
            this.btnLoadList.Size = new System.Drawing.Size(138, 54);
            this.btnLoadList.TabIndex = 1;
            this.btnLoadList.Text = "تحميل قائمة";
            this.btnLoadList.UseVisualStyleBackColor = false;
            this.btnLoadList.Click += new System.EventHandler(this.btnLoadList_Click);
            // 
            // btnSaveList
            // 
            this.btnSaveList.BackColor = System.Drawing.Color.Blue;
            this.btnSaveList.Font = new System.Drawing.Font("LBC", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnSaveList.Location = new System.Drawing.Point(686, 103);
            this.btnSaveList.Name = "btnSaveList";
            this.btnSaveList.Size = new System.Drawing.Size(138, 54);
            this.btnSaveList.TabIndex = 1;
            this.btnSaveList.Text = "حفظ قائمة";
            this.btnSaveList.UseVisualStyleBackColor = false;
            this.btnSaveList.Click += new System.EventHandler(this.btnSaveList_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.OldLace;
            this.button1.Font = new System.Drawing.Font("LBC", 12F);
            this.button1.Location = new System.Drawing.Point(686, 163);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(138, 51);
            this.button1.TabIndex = 10;
            this.button1.Text = "الاعدادات";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(849, 376);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnApplySystemSetup);
            this.Controls.Add(this.btnClearList);
            this.Controls.Add(this.listViewPrograms);
            this.Controls.Add(this.chkSelectAll);
            this.Controls.Add(this.lblCounter);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxLog);
            this.Controls.Add(this.textBoxFolderPath);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.btnSaveList);
            this.Controls.Add(this.btnLoadList);
            this.Controls.Add(this.btnInstall);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "متبت البرامج";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

       
        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox textBoxFolderPath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.ListView listViewPrograms;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblCounter;
        private System.Windows.Forms.Button btnClearList;
        private System.Windows.Forms.Button btnApplySystemSetup;
        private System.Windows.Forms.Button btnLoadList;
        private System.Windows.Forms.Button btnSaveList;
        private System.Windows.Forms.Button button1;
    }
}

