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
using System.Windows.Shapes;
using System.IO;
using System.Xml.Serialization;

namespace copyright
{
    /// <summary>
    /// Interaction logic for settings.xaml
    /// </summary>
    public partial class settingsWindow : Window
    {
        private Configuration m_Config = new Configuration();
        private String m_ConfigFileName = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath), "config.xml");

        public settingsWindow()
        {
            InitializeComponent();
            if (File.Exists(m_ConfigFileName))
            {
                LoadConfig();
            }
        }

        #region Config
        //----------------------------------------------------------------
        //Write Data
        //----------------------------------------------------------------
        private void SaveConfig()
        {
            try
            {
                FormToConfig();
                XmlSerializer xmlSer = new XmlSerializer(typeof(Configuration));
                FileStream fStream = new FileStream(m_ConfigFileName, FileMode.Create);
                xmlSer.Serialize(fStream, m_Config);
                fStream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //----------------------------------------------------------------
        //Load Data
        //---------------------------------------------------------------- 
        private void LoadConfig()
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
                MessageBox.Show(ex.Message);
            }
        }

        private bool ConfigToForm()
        {
            try
            {
                autorTB.Text        = m_Config.stringAuthor.ToString();
                rokTB.Text          = m_Config.stringYear.ToString();
                nazwaProjTB.Text    = m_Config.projectName.ToString();
                urlTB.Text          = m_Config.projectUrl.ToString();

                firstLineTB.Text    = m_Config.firstLine.ToString();
                lastLineTB.Text     = m_Config.lastLine.ToString();
                CharTB.Text         = m_Config.used_char.ToString();

                if (m_Config.cBoxContent_IsTrue == false)
                    cBoxContent.IsChecked   = false;
                else cBoxContent.IsChecked  = true;

                //if (m_Config.cBoxFirstLine_IsTrue   == false ? cBoxFirstLine.IsChecked = false : cBoxFirstLine.IsChecked = true);
                if (m_Config.cBoxFirstLine_IsTrue == false)
                    cBoxFirstLine.IsChecked = false;
                else
                    cBoxFirstLine.IsChecked = true;

                if (m_Config.cBoxLastLine_IsTrue == false)
                    cBoxLastLine.IsChecked = false;
                else cBoxLastLine.IsChecked = true;

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// FormToConfig
        /// </summary>
        /// <returns></returns>
        private bool FormToConfig()
        {
            try
            {
                m_Config.stringAuthor   = autorTB.Text;
                m_Config.stringYear     = rokTB.Text;
                m_Config.projectName    = nazwaProjTB.Text;
                m_Config.projectUrl     = urlTB.Text;

                m_Config.firstLine      = firstLineTB.Text;
                m_Config.lastLine       = lastLineTB.Text;
                m_Config.used_char      = CharTB.Text;

                if (m_Config.cBoxContent_IsTrue == false)
                    m_Config.cBoxContent_IsTrue = false;
                else
                    m_Config.cBoxContent_IsTrue = true;

                if (m_Config.cBoxFirstLine_IsTrue == false)
                    m_Config.cBoxFirstLine_IsTrue = false;
                else
                    m_Config.cBoxFirstLine_IsTrue = true;

                if (m_Config.cBoxLastLine_IsTrue == false)
                    m_Config.cBoxLastLine_IsTrue = false;
                else
                    m_Config.cBoxLastLine_IsTrue = true;


                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Blad", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
        #endregion



        #region check box
        private void cBoxContent_Checked(object sender, RoutedEventArgs e)
        {
            CharTB.IsEnabled = true;
            m_Config.cBoxContent_IsTrue         = true;
        }
        private void cBoxFirstLine_Checked(object sender, RoutedEventArgs e)
        {
            firstLineTB.IsEnabled = true;
            m_Config.cBoxFirstLine_IsTrue       = true;
        }

        private void cBoxLastLine_Checked(object sender, RoutedEventArgs e)
        {
            lastLineTB.IsEnabled = true;
            m_Config.cBoxLastLine_IsTrue        = true;
        }







        private void cBoxContent_Unchecked(object sender, RoutedEventArgs e)
        {
            CharTB.IsEnabled = false;
            m_Config.cBoxContent_IsTrue         = false;
        }

        private void cBoxFirstLine_Unchecked(object sender, RoutedEventArgs e)
        {
            firstLineTB.IsEnabled = false;
            m_Config.cBoxFirstLine_IsTrue       = false;
        }

        private void cBoxLastLine_Unchecked(object sender, RoutedEventArgs e)
        {
            lastLineTB.IsEnabled = false;
            m_Config.cBoxLastLine_IsTrue        = false;
        }

        #endregion check box

        private void save()
        {
            //Save and LoadConfig from main window
            SaveConfig();
            MainWindow mw = new MainWindow();
            mw.LoadConfig();
            MessageBoxResult result = MessageBox.Show("Saved", "Exit", MessageBoxButton.OK, MessageBoxImage.Information);
            if (result == MessageBoxResult.OK)
            {
                Close();
            }
        }

        private bool exit()
        {
            if (cBoxFirstLine.IsChecked == true || cBoxLastLine.IsChecked == true || cBoxContent.IsChecked == true)
            {
                if (firstLineTB.Text == "" || lastLineTB.Text == "" || CharTB.Text == "")
                {
                    var response = MessageBox.Show("you mast set first line comment eg. /* last line comment eg. */ and middle char eg.* like in cpp, do you want leave?", "Exiting...",
                                   MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                    if (response == MessageBoxResult.No)
                    {
                        return false;
                    }
                    else
                    { 
                        return false; 
                    }
                }
                return false;
            }
            return true;
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            if(exit() ==  true)
            { 
                Close();
            }
            
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            save();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (exit() == false)
            {
                var response = MessageBox.Show("You not set up the program, it will cause its malfunction", "Exiting...",
                                   MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (response == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    Application.Current.Shutdown();
                }
            }
        }
    }
}
