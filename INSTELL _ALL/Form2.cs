using System;
using System.Windows.Forms;

namespace InstallPrograms
{
    public partial class SqlSettingsForm : Form
    {
        // خصائص ليقرأها الفورم الرئيسي بعد الإغلاق
        public string InstanceName { get; private set; }
        public string SaPassword { get; private set; }

        public SqlSettingsForm(string currentInstance, string currentPassword)
        {
            InitializeComponent();

            // عرض القيم الحالية في الحقول عند الفتح
            txt_name.Text = currentInstance ?? string.Empty;
            txt_pass.Text = currentPassword ?? string.Empty;
            txt_pass.UseSystemPasswordChar = true;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            // فحوصات بسيطة
            if (string.IsNullOrWhiteSpace(txt_name.Text))
            {
                MessageBox.Show("من فضلك ادخل اسم السيرفر (Instance Name).", "مطلوب", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_name.Focus();
                return;
            }

            // إن أردت التحقق من تعقيد كلمة المرور، أضفه هنا. حالياً نسمح بأن تكون فارغة بعد تأكيد المستخدم.
            if (string.IsNullOrWhiteSpace(txt_pass.Text))
            {
                var res = MessageBox.Show("كلمة المرور فارغة. هل تريد المتابعة بدون كلمة مرور SA؟", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.No)
                {
                    txt_pass.Focus();
                    return;
                }
            }

            // تخزين القيم في الخصائص وإغلاق الفورم
            InstanceName = txt_name.Text.Trim();
            SaPassword = txt_pass.Text;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
