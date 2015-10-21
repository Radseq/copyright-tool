using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace copyright
{
    /// <summary>
    /// Config
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// Config
        /// </summary>
        public Configuration()
        {

        }

        //Attributes
        public String stringAuthor      = "";
        public String stringYear        = "";
        public String projectName       = "";
        public String projectUrl        = "";

        public String firstLine         = "";
        public String lastLine          = "";
        public String used_char         = "";

        public bool cBoxContent_IsTrue      = false;
        public bool cBoxFirstLine_IsTrue    = false;
        public bool cBoxLastLine_IsTrue     = false;

        //private String m_ConfigFileName = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath), "config.xml");

       // private Configuration m_Config = new Configuration();
        /*
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
                autorTB.Text        = stringAuthor.ToString();
                rokTB.Text          = stringYear.ToString();
                nazwaProjTB.Text    = projectName.ToString();
                urlTB.Text          = projectUrl.ToString();

                firstLineTB.Text    = firstLine.ToString();
                lastLineTB.Text     = lastLine.ToString();
                CharTB.Text         = used_char.ToString();

                if (cBoxContent_IsTrue == false)
                    cBoxContent.IsChecked   = false;
                else cBoxContent.IsChecked  = true;

                //if (cBoxFirstLine_IsTrue   == false ? cBoxFirstLine.IsChecked = false : cBoxFirstLine.IsChecked = true);
                if (cBoxFirstLine_IsTrue == false)
                    cBoxFirstLine.IsChecked = false;
                else
                    cBoxFirstLine.IsChecked = true;

                if (cBoxLastLine_IsTrue == false)
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
        private bool FormToConfig(string a,string b, string c, string d, string e, string f, string g)
        {
            try
            {
                stringAuthor   = a;
                stringYear     = b;
                projectName    = c;
                projectUrl     = d;

                firstLine      = e;
                lastLine       = f;
                used_char      = g;

                if (cBoxContent_IsTrue == false)
                    cBoxContent_IsTrue = false;
                else
                    cBoxContent_IsTrue = true;

                if (cBoxFirstLine_IsTrue == false)
                    cBoxFirstLine_IsTrue = false;
                else
                    cBoxFirstLine_IsTrue = true;

                if (cBoxLastLine_IsTrue == false)
                    cBoxLastLine_IsTrue = false;
                else
                    cBoxLastLine_IsTrue = true;


                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Blad", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
        #endregion*/

    }
}
