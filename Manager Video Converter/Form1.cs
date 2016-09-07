using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Manager_Video_Converter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DisplayOpenFiles();
            comboBox2.Items.Add("avi");
            comboBox2.Items.Add("flv");
            comboBox2.Items.Add("mkv");
            comboBox2.Items.Add("mov");
            comboBox2.Items.Add("mp4");
            comboBox2.Items.Add("mpg");
            comboBox2.Items.Add("wmv");
        }

        void SaveOpenFile(string FileName)
        {
            string path = "OpenFiles.txt";
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(FileName);
                }
            }
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(FileName);
            }
        }

        void DisplayOpenFiles()
        {
            string path = "OpenFiles.txt";
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                { }
            }
            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";
                comboBox1.Items.Clear();
                while ((s = sr.ReadLine()) != null)
                {
                    comboBox1.Items.Add(s);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                comboBox1.Text = openFileDialog1.FileName;
                SaveOpenFile(openFileDialog1.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process p = new Process();

            p.StartInfo.UseShellExecute = false;
            p.StartInfo.FileName = "ffmpeg";
            p.StartInfo.Arguments = " -i " + comboBox1.Text + " " + textBox1.Text + "." + comboBox2.Text;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;

            p.Start();
            textBox2.Text = Environment.CurrentDirectory + "\\" + textBox1.Text + "." + comboBox2.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Process.Start("explorer.exe", @"/select, " + textBox2.Text);

        }
    }
}


