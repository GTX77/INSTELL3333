using System;
using System.Windows.Forms;
using System.Security.Principal; // <-- إضافة using جديدة
using System.Diagnostics;       // <-- إضافة using جديدة

// مساحة الاسم الأصلية الخاصة بك
namespace INSTELL__ALL
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 1. الحصول على هوية المستخدم الحالي
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);

            // 2. التحقق مما إذا كان المستخدم يمتلك صلاحيات المسؤول
            bool isAdministrator = principal.IsInRole(WindowsBuiltInRole.Administrator);

            // 3. إذا لم يكن يعمل كمسؤول
            if (!isAdministrator)
            {
                // 4. قم بإنشاء عملية جديدة بنفس مسار البرنامج الحالي
                ProcessStartInfo proc = new ProcessStartInfo();
                proc.UseShellExecute = true;
                proc.WorkingDirectory = Environment.CurrentDirectory;
                proc.FileName = Application.ExecutablePath;

                // 5. أهم خطوة: اضبط "الفعل" على "runas" لطلب صلاحيات المسؤول
                proc.Verb = "runas";

                try
                {
                    // 6. ابدأ العملية الجديدة (التي ستطلب صلاحيات UAC)
                    Process.Start(proc);
                }
                catch (System.ComponentModel.Win32Exception)
                {
                    // المستخدم رفض نافذة UAC، لذا أظهر رسالة وأغلق البرنامج
                    MessageBox.Show(
                        "هذا البرنامج يتطلب صلاحيات المسؤول لتثبيت الخطوط والبرامج بشكل صحيح.",
                        "صلاحيات مطلوبة",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }

                // 7. أغلق النسخة الحالية من البرنامج (ذات الصلاحيات العادية)
                Application.Exit();
            }
            // 8. إذا كان يعمل كمسؤول بالفعل، قم بتشغيل البرنامج بشكل طبيعي
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                // لاحظ أنني استخدمت InstallPrograms.Form1 بناءً على الكود السابق
                // إذا كان Form1 موجودًا مباشرة تحت INSTELL__ALL، فاستخدم new Form1() فقط
                Application.Run(new InstallPrograms.Form1());
            }
        }
    }
}
