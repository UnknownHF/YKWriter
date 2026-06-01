using System.Diagnostics;
using System.IO.Compression;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Principal;

namespace YKWriter
{
    public partial class MainForm : Form
    {
        public string driveLetter;
        public MainForm()
        {
            InitializeComponent();
            btnRefresh.Image = new Bitmap(btnRefresh.Image, new Size(12, 12));

        }

        private void PopulateDriveComboBox()
        {
            cbDrives.Items.Clear();

            try
            {
                DriveInfo[] allDrives = DriveInfo.GetDrives();

                foreach (DriveInfo drive in allDrives)
                {
                    // Skip drives that aren't ready or aren't removable
                    if (!drive.IsReady || drive.DriveType != DriveType.Removable)
                        continue;
                    //string volumeLabel = string.IsNullOrEmpty(drive.VolumeLabel) ? "USB Drive" : drive.VolumeLabel;

                    cbDrives.Items.Add(drive.Name);
                }

                if (cbDrives.Items.Count > 0)
                {
                    cbDrives.SelectedIndex = 0;
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving drives: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            PopulateDriveComboBox();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            PopulateDriveComboBox();
            rtbLog.Clear();
            pbStatus.Value = 0;
        }

        private void cbDrives_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDrives.SelectedItem is string selectedText)
            {
                UpdateLog(rtbLog, $"[+] Current selected drive: {cbDrives.SelectedItem}");
                // Extracts the first character safely (e.g., "E" from "E:\")
                driveLetter = selectedText[0].ToString().ToUpper();
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(driveLetter))
            {
                MessageBox.Show("Please select a valid drive first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string warningMessage = $"WARNING: You are about to format drive {driveLetter}:\\ as exFAT.\n\n" +
                                    "This will permanently delete ALL data on this drive. Do you want to continue?";

            DialogResult confirmation = MessageBox.Show(
                warningMessage,
                "Confirm Drive Format",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2
            );

            if (confirmation == DialogResult.Cancel)
            {
                UpdateLog(rtbLog, "[-] Format operation cancelled by user."); 
                return;
            }

            UpdateLog(rtbLog, $"[+] Starting format for drive {driveLetter}:...");
            this.Cursor = Cursors.WaitCursor;

            bool success = FormatDriveEFAT(driveLetter);

            this.Cursor = Cursors.Default;

            if (success)
            {
                UpdateLog(rtbLog, $"[+] Drive {driveLetter}:\\ successfully formatted as exFAT");
                pbStatus.Value = 50;
            }
            else
            {
                UpdateLog(rtbLog, $"[-] Failed to format drive {driveLetter}:. Ensure app is running as Admin.");
            }

            ExtractResourceToSystemVolumeInfo(driveLetter);
            pbStatus.Value = 100;

        }
        private bool FormatDriveEFAT(string letter)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "cmd.exe";
                psi.Arguments = $"/c format {letter}: /fs:exFAT /q /x /y";

                psi.CreateNoWindow = true;
                psi.UseShellExecute = false;
                psi.Verb = "runas"; // Requests admin privileges

                using (Process process = Process.Start(psi))
                {
                    // Wait for cmd.exe to complete the format before moving on
                    process.WaitForExit();

                    return process.ExitCode == 0;
                }
            }
            catch (Exception ex)
            {
                UpdateLog(rtbLog,$"[-] Exception: {ex.Message}");
                return false;
            }
        }

        private void ExtractResourceToSystemVolumeInfo(string driveLetter)
        {
            string targetFolder = Path.Combine($"{driveLetter}:\\", "System Volume Information");

            try
            {
                if (!Directory.Exists(targetFolder))
                {
                    Directory.CreateDirectory(targetFolder);
                }

                UpdateLog(rtbLog,"[+] Modifying NTFS/exFAT permissions to allow file writing...");
                GrantAccessToFolder(targetFolder);

                UpdateLog(rtbLog, "[+] Loading embedded ZIP resource into memory...");

                // Access the byte array directly from your Resources
                byte[] zipBytes = Properties.Resources.FsTx;

                if (zipBytes == null || zipBytes.Length == 0)
                {
                    throw new Exception("Could not find or read the FsTx resource from Properties.Resources.");
                }

                // Wrap the byte array in a MemoryStream so ZipArchive can read it
                using (MemoryStream resourceStream = new MemoryStream(zipBytes))
                {
                    using (ZipArchive archive = new ZipArchive(resourceStream, ZipArchiveMode.Read))
                    {
                        UpdateLog(rtbLog, $"[+] Extracting {archive.Entries.Count} file(s) into System Volume Information...");

                        foreach (ZipArchiveEntry entry in archive.Entries)
                        {
                            string destinationPath = Path.Combine(targetFolder, entry.FullName);
                            string directoryPath = Path.GetDirectoryName(destinationPath);

                            if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
                            {
                                Directory.CreateDirectory(directoryPath);
                            }

                            if (!destinationPath.EndsWith("\\") && !destinationPath.EndsWith("/"))
                            {
                                entry.ExtractToFile(destinationPath, overwrite: true);
                                UpdateLog(rtbLog, $"[+] Extracted: {entry.FullName}");
                            }
                        }
                    }
                }

                UpdateLog(rtbLog, "[+] File transfer complete!\n[+] Success!! You may now eject your drive.");
                
            }
            catch (Exception ex)
            {
                UpdateLog(rtbLog, $"[-] Extraction Failed: {ex.Message}");
            }
        }

        private void GrantAccessToFolder(string folderPath)
        {
            try
            {
                DirectoryInfo dInfo = new DirectoryInfo(folderPath);
                DirectorySecurity dSecurity = dInfo.GetAccessControl();

                // Identify the current identity running this application (requires Admin)
                SecurityIdentifier currentUser = WindowsIdentity.GetCurrent().User;

                // Create an access rule giving Full Control over the directory and all subfiles
                FileSystemAccessRule accessRule = new FileSystemAccessRule(
                    currentUser,
                    FileSystemRights.FullControl,
                    InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                    PropagationFlags.None,
                    AccessControlType.Allow
                );

                dSecurity.AddAccessRule(accessRule);
                dInfo.SetAccessControl(dSecurity);
            }
            catch (Exception ex)
            {
                // If this throws an error, it almost always means the application isn't running as Administrator
                throw new Exception($"Failed to acquire directory permissions. Ensure app is run as Admin. Detail: {ex.Message}");
            }
        }

        private void UpdateLog(RichTextBox rtb, string message)
        {
            rtb.AppendText(message + "\n");
            rtb.ScrollToCaret();


        }
    }
}
