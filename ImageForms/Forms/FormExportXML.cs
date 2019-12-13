using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Xsl;

namespace ImageForms
{
    public partial class FormExportXML : Form
    {
        public FormExportXML()
        {
            InitializeComponent();
        }

        private void buttonExpotXML_Click(object sender, EventArgs e)
        {

            XmlWriterSettings xws = new XmlWriterSettings();
            xws.Encoding = System.Text.Encoding.UTF8;
            xws.Indent = true;

            System.Xml.XmlWriter xw = XmlWriter.Create("D:/model.xml", xws);

            xw.WriteStartElement("root");

            for (int i = 0; i < 10; i++)
            {
                xw.WriteStartElement("level");
                xw.WriteAttributeString("id", i.ToString());
                xw.WriteAttributeString("name", i.ToString());

                xw.WriteStartElement("level2");
                xw.WriteAttributeString("id", i.ToString());
                xw.WriteString("root - text Юра - " + i.ToString());
                xw.WriteEndElement();

                xw.WriteEndElement();
            }

            xw.WriteEndElement();

            xw.Flush();
            xw.Close();
        }

        private void buttonXSLTransform_Click(object sender, EventArgs e)
        {
            XslCompiledTransform xslt = new XslCompiledTransform();

            xslt.Load("..\\..\\XSL\\xsl_model.xsl");
            
            xslt.Transform("D:\\model.xml", "D:\\model_inputdata.txt");
        }
    }
}
