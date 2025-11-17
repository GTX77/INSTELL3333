using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using INSTELL__ALL;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Drawing.Text;

namespace InstallPrograms
{
    public partial class Form1 : Form
    {
        string programsFolder = "";
        private ImageList imageListIcons = new ImageList();

        private string sqlInstanceName = "ABOGHRISSQL";
        private string saPassword = "999999999";

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool SendMessageTimeout(IntPtr hWnd, uint Msg, UIntPtr wParam, string lParam, SendMessageTimeoutFlags fuFlags, uint uTimeout, out UIntPtr lpdwResult);

        [Flags]
        public enum SendMessageTimeoutFlags : uint
        {
            SMTO_NORMAL = 0x0, SMTO_BLOCK = 0x1, SMTO_ABORTIFHUNG = 0x2,
            SMTO_NOTIMEOUTIFNOTHUNG = 0x8, SMTO_ERRORONEXIT = 0x20
        }
        private const int WM_SETTINGCHANGE = 0x001A;

        public Form1()
        {
            InitializeComponent();
            listViewPrograms.View = View.List;
            listViewPrograms.CheckBoxes = true;
            listViewPrograms.SmallImageList = imageListIcons;
            imageListIcons.ImageSize = new Size(32, 32);
            listViewPrograms.ItemChecked += ListViewPrograms_ItemChecked;
            UpdateCounter();
        }

        [DllImport("gdi32.dll", EntryPoint = "AddFontResourceW", SetLastError = true)]
        public static extern int AddFontResource([In][MarshalAs(UnmanagedType.LPWStr)] string lpFileName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        private const int WM_FONTCHANGE = 0x001D;
        private static readonly IntPtr HWND_BROADCAST = new IntPtr(0xffff);

        private void RegisterFont(string fontFileName)
        {
            try
            {
                string fontDestinationPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), fontFileName);
                string fontSourcePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fontFileName);

                if (!File.Exists(fontSourcePath)) return;
                if (!File.Exists(fontDestinationPath))
                {
                    File.Copy(fontSourcePath, fontDestinationPath, true);
                }

                AddFontResource(fontDestinationPath);

                using (RegistryKey key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts"))
                {
                    using (PrivateFontCollection pfc = new PrivateFontCollection())
                    {
                        pfc.AddFontFile(fontDestinationPath);
                        string fontName = pfc.Families[0].Name;

                        if (key.GetValue(fontName) == null)
                        {
                            key.SetValue(fontName, fontFileName, RegistryValueKind.String);
                        }
                    }
                }
                SendMessage(HWND_BROADCAST, WM_FONTCHANGE, IntPtr.Zero, IntPtr.Zero);
            }
            catch (Exception)
            {
                // تجاهل أي خطأ بصمت
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult choice = MessageBox.Show(
                "هل تريد اختيار مجلد كامل؟\n\n- اختر Yes لاختيار مجلد\n- اختر No لاختيار ملفات (EXE/MSI)",
                "خيارات الاختيار",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question
            );

            if (choice == DialogResult.Yes)
            {
                using (FolderBrowserDialog fbd = new FolderBrowserDialog())
                {
                    fbd.Description = "اختر مجلد البرامج";
                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        programsFolder = fbd.SelectedPath;
                        textBoxFolderPath.Text = programsFolder;
                        LoadProgramsFromFolder(programsFolder);
                    }
                }
            }
            else if (choice == DialogResult.No)
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Setup Files (*.exe;*.msi;*.cab)|*.exe;*.msi;*.cab";
                    ofd.Multiselect = true;
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        textBoxFolderPath.Text = "تم اختيار ملفات فردية";
                        foreach (string file in ofd.FileNames)
                        {
                            AddProgramToList(file);
                        }
                    }
                }
            }
        }

        private void LoadProgramsFromFolder(string folder)
        {
            listViewPrograms.Items.Clear();
            imageListIcons.Images.Clear();
            string[] extensions = { ".exe", ".msi", ".cab" };
            var files = Directory.GetFiles(folder, "*.*", SearchOption.TopDirectoryOnly)
                                 .Where(f => extensions.Contains(Path.GetExtension(f).ToLower()));

            foreach (string file in files)
            {
                AddProgramToList(file);
            }

            if (!listViewPrograms.Items.Cast<ListViewItem>().Any())
            {
                MessageBox.Show("لم يتم العثور على أي ملفات (EXE, MSI, CAB) في هذا المجلد.");
            }
            UpdateCounter();
        }

        private void AddProgramToList(string filePath)
        {
            if (!File.Exists(filePath)) return;

            foreach (ListViewItem item in listViewPrograms.Items)
            {
                if (item.Tag.ToString().Equals(filePath, StringComparison.OrdinalIgnoreCase))
                {
                    item.Checked = true;
                    return;
                }
            }

            try
            {
                string displayName = Path.GetFileName(filePath);
                Icon icon = Icon.ExtractAssociatedIcon(filePath);
                if (icon != null)
                {
                    imageListIcons.Images.Add(filePath, icon);
                }

                ListViewItem newItem = new ListViewItem(displayName)
                {
                    Tag = filePath,
                    Checked = true,
                    ImageKey = (icon != null) ? filePath : null
                };
                listViewPrograms.Items.Add(newItem);
            }
            catch
            {
                ListViewItem newItem = new ListViewItem(Path.GetFileName(filePath))
                {
                    Tag = filePath,
                    Checked = true
                };
                listViewPrograms.Items.Add(newItem);
            }
            UpdateCounter();
        }

        private bool IsProgramInstalled(string programNameFragment)
        {
            string[] registryPaths = {
                @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall",
                @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall"
            };

            foreach (string path in registryPaths)
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(path))
                {
                    if (key == null) continue;

                    foreach (string subKeyName in key.GetSubKeyNames())
                    {
                        using (RegistryKey subKey = key.OpenSubKey(subKeyName))
                        {
                            object displayNameObj = subKey.GetValue("DisplayName");
                            if (displayNameObj != null)
                            {
                                string displayName = displayNameObj.ToString();
                                if (displayName.IndexOf(programNameFragment, StringComparison.OrdinalIgnoreCase) >= 0)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        private async void btnInstall_Click(object sender, EventArgs e)
        {
            var selectedItems = listViewPrograms.CheckedItems;
            if (selectedItems.Count == 0)
            {
                MessageBox.Show("من فضلك اختر برنامج واحد على الأقل للتثبيت.");
                return;
            }

            btnInstall.Enabled = false;
            btnBrowse.Enabled = false;
            btnClearList.Enabled = false;
            btnApplySystemSetup.Enabled = false;
            string originalTitle = this.Text;

            int totalCount = selectedItems.Count;
            int completedCount = 0;

            progressBar1.Maximum = totalCount;
            progressBar1.Value = 0;
            textBoxLog.Clear();
            textBoxLog.AppendText($"===== بدء عملية التثبيت لـ {totalCount} برنامجًا =====\r\n\r\n");

            foreach (ListViewItem item in selectedItems)
            {
                completedCount++;
                double percentage = ((double)completedCount / totalCount) * 100;
                this.Text = $"جاري التثبيت... ({completedCount}/{totalCount}) - {percentage:F0}%";

                string programDisplayName = item.Text;
                string programFile = item.Tag.ToString();
                string fileName = Path.GetFileName(programFile).ToLower();

                textBoxLog.AppendText($"[{completedCount}/{totalCount}] 🔎 التحقق من: {programDisplayName}\r\n");

                string searchName = "";
                // =======================================================================
                // بداية التعديل: تصحيح التحقق ليتناسب مع SQL Server 2019
                // =======================================================================
                if (fileName.Contains("sqlexpr") || (fileName.Contains("sql") && fileName.Contains("2019")))
                {
                    searchName = "SQL Server 2019"; // تم التحديث للبحث عن إصدار 2019
                }
                // =======================================================================
                // نهاية التعديل
                // =======================================================================
                else if (fileName.Contains("netfx3")) searchName = ".NET Framework 3.5";
                else if (fileName.Contains("office")) searchName = "Microsoft Office";
                else searchName = Path.GetFileNameWithoutExtension(programFile);

                if (!string.IsNullOrEmpty(searchName) && IsProgramInstalled(searchName))
                {
                    textBoxLog.AppendText($"    ℹ️ تم التخطي: البرنامج '{searchName}' مثبت بالفعل.\r\n\r\n");
                    progressBar1.Value = completedCount;
                    continue;
                }

                textBoxLog.AppendText($"    ⏳ بدء تثبيت: {programDisplayName}\r\n");

                if (string.IsNullOrEmpty(programFile) || !File.Exists(programFile))
                {
                    textBoxLog.AppendText($"    ❌ خطأ: لم يتم العثور على الملف.\r\n\r\n");
                    progressBar1.Value = completedCount;
                    continue;
                }

                try
                {
                    Process p = new Process();
                    string ext = Path.GetExtension(programFile).ToLower();
                    string productName = "";
                    try { productName = (FileVersionInfo.GetVersionInfo(programFile).ProductName ?? "").ToLower(); } catch { }

                    if (fileName.Contains("sqlexpr") || productName.Contains("sql server"))
                    {
                        p.StartInfo.FileName = programFile;
                        p.StartInfo.Arguments =
                            $"/Q /ACTION=Install /IACCEPTSQLSERVERLICENSETERMS " +
                            $"/FEATURES=SQLEngine " +
                            $"/INSTANCENAME={sqlInstanceName} " +
                            $"/SQLSYSADMINACCOUNTS=\"BUILTIN\\Administrators\" " +
                            $"/SQLSVCACCOUNT=\"NT AUTHORITY\\SYSTEM\" " +
                            $"/SECURITYMODE=SQL /SAPWD=\"{saPassword}\" " +
                            $"/TCPENABLED=1 /NPENABLED=1 /BROWSERSVCSTARTUPTYPE=Automatic";
                    }
                    else if (ext == ".cab" && fileName.Contains("netfx3"))
                    {
                        p.StartInfo.FileName = "DISM.exe";
                        p.StartInfo.Arguments = $"/Online /Add-Package /PackagePath:\"{programFile}\" /NoRestart";
                    }
                    else if (productName.Contains("office 2010"))
                    {
                        p.StartInfo.FileName = programFile;
                        p.StartInfo.Arguments = "";
                    }
                    else if (ext == ".msi")
                    {
                        p.StartInfo.FileName = "msiexec.exe";
                        p.StartInfo.Arguments = $"/i \"{programFile}\" /quiet /norestart";
                    }
                    else
                    {
                        p.StartInfo.FileName = programFile;
                        p.StartInfo.Arguments = "/S";
                    }

                    p.StartInfo.Verb = "runas";
                    p.StartInfo.UseShellExecute = true;

                    await Task.Run(() =>
                    {
                        p.Start();
                        p.WaitForExit();
                    });

                    if (p.ExitCode == 0 || p.ExitCode == 3010)
                    {
                        string successMessage = p.ExitCode == 3010 ? " (يتطلب إعادة تشغيل)" : "";
                        textBoxLog.AppendText($"    ✅ نجاح: تم التثبيت بنجاح{successMessage}.\r\n\r\n");
                    }
                    else
                    {
                        textBoxLog.AppendText($"    ❌ فشل: انتهى التثبيت برمز خطأ (ExitCode: {p.ExitCode}).\r\n");
                    }
                }
                catch (Exception ex)
                {
                    textBoxLog.AppendText($"    ❌ خطأ فادح: {ex.Message}\r\n\r\n");
                }

                progressBar1.Value = completedCount;
            }

            this.Text = originalTitle;
            btnInstall.Enabled = true;
            btnBrowse.Enabled = true;
            btnClearList.Enabled = true;
            btnApplySystemSetup.Enabled = true;

            textBoxLog.AppendText($"===== ✅ اكتملت العملية! =====\r\n");
            MessageBox.Show("انتهت جميع عمليات التثبيت!", "اكتملت العملية", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewPrograms.Items)
            {
                item.Checked = chkSelectAll.Checked;
            }
        }

        private void ListViewPrograms_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            UpdateCounter();
        }

        private void UpdateCounter()
        {
            lblCounter.Text = $"المحدد: {listViewPrograms.CheckedItems.Count} / الكلي: {listViewPrograms.Items.Count}";
        }

        private void btnClearList_Click(object sender, EventArgs e)
        {
            listViewPrograms.Items.Clear();
            imageListIcons.Images.Clear();
            UpdateCounter();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RegisterFont("LBC.otf");
        }

        private void button1_Click(object sender, EventArgs e) { }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void btnApplySystemSetup_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("أنت على وشك تطبيق إعدادات النظام.\n\n" +
                                "الإعدادات التي سيتم تطبيقها:\n" +
                                "  • المنطقة، الأرقام، العملة، والتقويم.\n" +
                                "  • فتح منفذ SQL Server في جدار الحماية.\n\n" +
                                "هل تريد المتابعة؟",
                                "تأكيد إعداد النظام",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning) == DialogResult.No)
            {
                textBoxLog.AppendText("ℹ️ تم إلغاء عملية إعداد النظام من قبل المستخدم.\r\n");
                return;
            }

            textBoxLog.AppendText("===== بدء عملية الإعداد الكامل للنظام =====\r\n");

            btnApplySystemSetup.Enabled = false;
            btnInstall.Enabled = false;
            btnBrowse.Enabled = false;
            btnClearList.Enabled = false;

            ApplyRegionSettings();
            CreateFirewallRuleForSqlServer();

            textBoxLog.AppendText("===== ✅ اكتملت عملية إعداد النظام بنجاح! =====\r\n");

            btnApplySystemSetup.Enabled = true;
            btnInstall.Enabled = true;
            btnBrowse.Enabled = true;
            btnClearList.Enabled = true;

            MessageBox.Show("تم تطبيق الإعدادات الإقليمية وجدار الحماية بنجاح.\n\n" +
                            "**يجب** إعادة تشغيل الكمبيوتر لتطبيق التغييرات.",
                            "اكتملت العملية | إعادة تشغيل مطلوبة",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        private void ApplyRegionSettings()
        {
            textBoxLog.AppendText("--- ⚙️ بدء تطبيق إعدادات المنطقة ---\r\n");
            // ... (الكود الداخلي للدالة يبقى كما هو) ...
            textBoxLog.AppendText("--- ✅ اكتمل تطبيق إعدادات المنطقة ---\r\n");
        }

        private void CreateFirewallRuleForSqlServer()
        {
            textBoxLog.AppendText("--- ⚙️ بدء إنشاء قاعدة جدار الحماية ---\r\n");
            // ... (الكود الداخلي للدالة يبقى كما هو) ...
            textBoxLog.AppendText("--- ✅ اكتمل إنشاء قاعدة جدار الحماية ---\r\n");
        }

       

      
        private void btnLoadList_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Program List (*.txt)|*.txt|All Files (*.*)|*.*";
                ofd.Title = "تحميل قائمة البرامج";
                ofd.Multiselect = false;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;
                    textBoxLog.AppendText("⏳ جاري تحميل القائمة والبحث عن الملفات (مع تجاهل قرص C)...\r\n");
                    Application.DoEvents();

                    try
                    {
                        listViewPrograms.Items.Clear();
                        imageListIcons.Images.Clear();

                        var pathsFromFile = File.ReadAllLines(ofd.FileName);
                        var missingFiles = new List<string>();

                        var drivesToSearch = DriveInfo.GetDrives()
                            .Where(d => d.IsReady &&
                                        (d.DriveType == DriveType.Fixed || d.DriveType == DriveType.Removable) &&
                                        !d.Name.Equals("C:\\", StringComparison.OrdinalIgnoreCase));

                        if (!drivesToSearch.Any())
                        {
                            MessageBox.Show("لم يتم العثور على أي أقراص (غير C) للبحث فيها.", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.Cursor = Cursors.Default;
                            return;
                        }

                        string driveNames = string.Join(", ", drivesToSearch.Select(d => d.Name.Replace("\\", "")));
                        textBoxLog.AppendText($"ℹ️ سيتم البحث في الأقراص التالية: {driveNames}\r\n");
                        Application.DoEvents();

                        foreach (string oldPath in pathsFromFile)
                        {
                            if (string.IsNullOrWhiteSpace(oldPath)) continue;

                            string fileNameToFind = Path.GetFileName(oldPath);
                            string foundPath = null;

                            foreach (var drive in drivesToSearch)
                            {
                                try
                                {
                                    var searchResult = Directory.EnumerateFiles(drive.Name, fileNameToFind, SearchOption.AllDirectories).FirstOrDefault();
                                    if (searchResult != null)
                                    {
                                        foundPath = searchResult;
                                        break;
                                    }
                                }
                                catch (UnauthorizedAccessException) { }
                                catch (Exception) { }
                            }

                            if (foundPath != null)
                            {
                                AddProgramToList(foundPath);
                            }
                            else
                            {
                                missingFiles.Add(fileNameToFind);
                            }
                        }
                        UpdateCounter();

                        if (missingFiles.Count == 0)
                        {
                            MessageBox.Show("تم تحميل القائمة والعثور على جميع الملفات بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            string missingFilesMessage = string.Join("\n- ", missingFiles);
                            MessageBox.Show(
                                $"اكتمل تحميل القائمة، ولكن لم يتم العثور على الملفات التالية على هذا الجهاز:\n\n- {missingFilesMessage}",
                                "تحذير: ملفات مفقودة",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"حدث خطأ أثناء تحميل الملف: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                        textBoxLog.AppendText("✅ اكتملت عملية البحث.\r\n");
                    }
                }
            }
        }

        private void btnSaveList_Click(object sender, EventArgs e)
        {
            if (listViewPrograms.Items.Count == 0)
            {
                MessageBox.Show("القائمة فارغة، لا يوجد شيء لحفظه.", "معلومة", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Program List (*.txt)|*.txt";
                sfd.Title = "حفظ قائمة البرامج";
                sfd.FileName = "MyProgramList.txt";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var filePaths = listViewPrograms.Items.Cast<ListViewItem>()
                                                          .Select(item => item.Tag.ToString());

                        File.WriteAllLines(sfd.FileName, filePaths);
                        MessageBox.Show("تم حفظ القائمة بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"حدث خطأ أثناء حفظ الملف: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
