using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Threading;

namespace copyright
{
    /* Note
     * ai means additional information
     * ai_len means ai lenght
     * Program required dot.net 4,5(becouse of serching directories with ext) that means windows xp dont use this progam
     * 
     * main think: is make class whitch will have all Atribbutes(line: 44 to 69) and all functions
     * move LoadConfig and ConfigToForm to Configuration.cs class (if its possible to move ConfigToForm)
     * program need to be completed tested
     */
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadConfig();
        }

        //Attributes
        private Configuration m_Config = new Configuration();
        private String m_ConfigFileName = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath), "config.xml");

        string fileName             = "";
        string fileName_WithoutPatch  = "";

        string projectAuthor        = "";
        string projectYear          = "";
        string projectName          = "";
        string projectUrl           = "";

        string firstLine            = "";
        string lastLine             = "";
        string used_char            = "";

        int allFiles                = 0;

        bool contentChar_enable;
        bool firstLine_enable;
        bool lastLine_enable;

        //DateTime dateCreateFile;
        string dateCreateFile       ="";

        List<string> filteredFiles;

        #region Config
        //----------------------------------------------------------------
        //Load Data
        //---------------------------------------------------------------- 
        public void LoadConfig()
        {
            try
            {
                //If file exists
                if (File.Exists(m_ConfigFileName))
                {
                    XmlSerializer xmlSer = new XmlSerializer(typeof(Configuration));
                    StreamReader sReader = new StreamReader(m_ConfigFileName);
                    m_Config = (Configuration)xmlSer.Deserialize(sReader);
                    sReader.Close();
                }

                //Show Data
                ConfigToForm();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private bool ConfigToForm()
        {
            try
            {
                projectAuthor           = m_Config.stringAuthor.ToString();
                projectYear             = m_Config.stringYear.ToString();
                projectName             = m_Config.projectName.ToString();
                projectUrl              = m_Config.projectUrl.ToString();

                firstLine               = m_Config.firstLine.ToString();
                lastLine                = m_Config.lastLine.ToString();
                used_char               = m_Config.used_char.ToString();

                if (m_Config.cBoxContent_IsTrue == false)
                    contentChar_enable = false;
                else contentChar_enable = true;

                //if (m_Config.cBoxFirstLine_IsTrue   == false ? cBoxFirstLine.IsChecked = false : cBoxFirstLine.IsChecked = true);
                if (m_Config.cBoxFirstLine_IsTrue == false)
                    firstLine_enable = false;
                else
                    firstLine_enable = true;

                if (m_Config.cBoxLastLine_IsTrue == false)
                    lastLine_enable = false;
                else lastLine_enable = true;

                return true;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return false;
            }
        }
        #endregion Config

        private StringCollection GetTextLines(System.Windows.Controls.TextBox tb)
        {
            var lines = new StringCollection();
            int lineCount = tb.LineCount;
            for (int line = 0; line < lineCount; line++)
                this.Dispatcher.Invoke(delegate
                {
                    lines.Add(tb.GetLineText(line));
                });
            return lines;

        }

        private string fileNameWithoutPatch(string fileName)
        {
            fileName = System.IO.Path.GetFileName(fileName);
            return fileName;
        }

        private void saveLicence()
        {
            this.Dispatcher.Invoke(delegate
            {
                StreamWriter sw = new StreamWriter(fileName);

                var lines = GetTextLines(licenceTB);
                int len = 0;
                foreach (var line in lines)
                {
                    len++;
                }

                if (firstLine_enable == true)
                    sw.WriteLine(firstLine);

                string[] array = new string[len];

                len = 0;
                foreach (var line in lines)
                {
                    var oldLine = line;
                    var newLine = string.Join(" ", Regex.Split(oldLine, @"(?:\r\n|\n|\r)"));

                    // change ${date} and ${project_name}
                    string zmiana_daty = newLine.Replace("${year}", projectYear);
                    string zmiana_proj = zmiana_daty.Replace("${project_name}", projectName);
                    string zmiana_url = zmiana_proj.Replace("${url}", "<" + projectUrl + ">");

                    string linia = String.Format("{0} {1}", used_char, zmiana_url);

                    array[len] = linia;     //array of string's 
                    len++;                  //move to next row
                    sw.WriteLine(linia);    //write current line to array of string's
                }
                if (additionalInformationTB.IsEnabled == false)
                {
                    if (lastLine_enable == true)
                        sw.WriteLine(lastLine);

                }
                sw.Close();
            });
        }

        private void saveLicence_ai()
        {
            this.Dispatcher.Invoke(delegate
            {
                StreamWriter sw = new StreamWriter(fileName, true);

                var ai_lines = GetTextLines(additionalInformationTB);

                int ai_len = 0;
                foreach (var ai_line in ai_lines)
                {
                    ai_len++;
                }

                string[] ai_array = new string[ai_len];

                sw.WriteLine();

                //autor + file create date
                string aut_dat = String.Format("File Name: {0}\n \tCreated on: {1}\n \tAuthor: {2}"
                                                , fileNameTB.Text, dateCreateFile, projectAuthor);
                sw.WriteLine(aut_dat);

                ai_len = 0;

                foreach (var ai_line in ai_lines)
                {
                    var ai_oldLine = ai_line;
                    var ai_newLine = string.Join(" ", Regex.Split(ai_oldLine, @"(?:\r\n|\n|\r)"));

                    string ai_linia = String.Format("\t {0}", ai_newLine);
                    ai_array[ai_len] = ai_linia;
                    ai_len++;

                    sw.WriteLine(ai_linia);
                }
                if (lastLine_enable == true)
                    sw.WriteLine(lastLine);
                sw.Close();
            });
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(delegate()
            {
                int counter = 0;
                if (filteredFiles != null)
                {
                    this.Dispatcher.Invoke(delegate
                    {
                        allFiles = filteredFiles.Count();
                        WorkProgres.Maximum = allFiles - 1;
                        WorkProgres.Opacity = 100;
                    });
                    foreach (var file in filteredFiles)
                    {
                        fileName = file;

                        fileName_WithoutPatch = fileNameWithoutPatch(file);

                        this.Dispatcher.Invoke(delegate
                        {
                            fileNameTB.Text = fileName_WithoutPatch;
                        });

                        dateCreateFile = File.GetCreationTime(file).ToString("dd MM, yyyy");

                        saveLicence();

                        this.Dispatcher.Invoke(delegate
                        {
                            if (additionalInformationTB.IsEnabled == true)
                            {
                                saveLicence_ai();
                            }
                        
                            WorkProgres.Value = counter;
                            if (counter == (allFiles) - 1)
                            {
                                progressDone.Content = "Done";
                            }
                        });
                        counter++;
                    }
                }
                else
                {
                    saveLicence();
                    this.Dispatcher.Invoke(delegate
                    {
                        if (additionalInformationTB.IsEnabled == true)
                        {
                            saveLicence_ai();
                            progressDone.Content = "Done";
                        }
                    });
                }
            });
            thread.Start();
        }

    private void buttonLoad_Click(object sender, RoutedEventArgs e)
        {
            LoadConfig(); //we must have "update" variables

            FileDialog fDg = new OpenFileDialog();
            //fDg.Filter = "header files (*.h)|*.txt|All files (*.*)|*.*";
            if (fDg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fileName = fDg.FileName; //fileName with Extension
                //fileName_WithoutPatch = System.IO.Path.GetFileNameWithoutExtension(fDg.FileName);  //without Extension
                fileName_WithoutPatch = fileNameWithoutPatch(fDg.FileName);  //without Extension

                fileNameTB.Text = fileName_WithoutPatch;
                dateCreateFile = File.GetCreationTime(fileName).ToString("dd MM, yyyy");
                buttonSave.IsEnabled = true;

                WorkProgres.Value = 0;
                progressDone.Content = "";
                progressDone.IsEnabled = false;
            }
        }

        private void settingsBT_Click(object sender, RoutedEventArgs e)
        {
            settingsWindow settingWindow = new settingsWindow();
            settingWindow.ShowDialog();
        }

        private void additionalInformationCB_Checked(object sender, RoutedEventArgs e)
        {
            additionalInformationTB.IsEnabled = true;
        }

        private void additionalInformationCB_Unchecked(object sender, RoutedEventArgs e)
        {
            additionalInformationTB.IsEnabled = false;
        }

        private void buttonLoadPatch_Click(object sender, RoutedEventArgs e)
        {
            LoadConfig(); //we must have "update" variables

            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();


            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string patch = dialog.SelectedPath;
                //directory = Directory.GetFileSystemEntries(patch, "*", SearchOption.AllDirectories);
                // finding all files with extension cpp and h in patch
                filteredFiles = Directory.EnumerateFiles(patch, "*", SearchOption.AllDirectories)/*<--- .NET 4.5*/
                .Where(file => file.ToLower().EndsWith(".h") || file.ToLower().EndsWith(".cpp")).ToList();

                WorkProgres.Value = 0;

                buttonSave.IsEnabled = true;
                progressDone.Content = "";
            }
        }
    }
}
