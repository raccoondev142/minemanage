using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;

namespace MineManage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists(Application.StartupPath + "/config.txt"))
            {
                string[] fr = File.ReadAllLines(Application.StartupPath + "/config.txt");
                if (Directory.Exists(fr[1] + "/mods"))
                {
                    textBox1.Text = fr[1];
                    foreach (string name in Directory.GetFiles(textBox1.Text + "/mods"))
                    {
                        listBox1.Items.Add(Path.GetFileName(name));
                    }
                    foreach (string name in Directory.GetDirectories(textBox1.Text + "/resourcepacks"))
                    {
                        listBox2.Items.Add(Path.GetFileName(name));
                    }
                    foreach (string name in Directory.GetDirectories(textBox1.Text + "/saves"))
                    {
                        listBox3.Items.Add(Path.GetFileName(name));
                    }
                }
                else
                {
                    MessageBox.Show("You don't have a version of forge installed.\nPlease install a version of forge at 'files.minecraftforge.net'.", "Forge not installed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                File.WriteAllText(Application.StartupPath + "/config.txt", "//MineManage Configuration : DO NOT MODIFY!\nC:/Users/" + Environment.UserName + "/AppData/Roaming/.minecraft");
                MessageBox.Show("Welcome to MineManage!\nThe setup is done, and now the app is yours.\nThe program will now close, and you can open it again to see the features.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog mod = new OpenFileDialog();
            mod.Title = "Import Mod";
            mod.Filter = "Java Mods (*.jar)|*.jar";
            if (mod.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.Move(mod.FileName, textBox1.Text + "/mods/" + Path.GetFileName(mod.FileName));
                    Reload();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Internal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process.Start("https://files.minecraftforge.net");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Process.Start(textBox1.Text + "/mods/");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(Application.StartupPath + "/config.txt", "//MineManage Configuration : DO NOT MODIFY!\n" + fbd.SelectedPath);
            }
        }

        private void refreshModsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reload();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog mod = new OpenFileDialog();
            mod.Title = "Import Resource Pack";
            mod.Filter = "Zipped Folder (*.zip)|*.zip";
            if (mod.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ZipFile.ExtractToDirectory(mod.FileName, textBox1.Text + "/resourcepacks/" + Path.GetFileNameWithoutExtension(mod.FileName));
                    Reload();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Internal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Process.Start(textBox1.Text + "/resourcepacks/");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Process.Start(textBox1.Text + "/saves/");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            OpenFileDialog mod = new OpenFileDialog();
            mod.Title = "Import Minecraft World";
            mod.Filter = "Zipped Folder (*.zip)|*.zip";
            if (mod.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ZipFile.ExtractToDirectory(mod.FileName, textBox1.Text + "/saves/" + Path.GetFileNameWithoutExtension(mod.FileName));
                    Reload();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Internal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Reload()
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            foreach (string name in Directory.GetFiles(textBox1.Text + "/mods"))
            {
                listBox1.Items.Add(Path.GetFileName(name));
            }
            foreach (string name in Directory.GetDirectories(textBox1.Text + "/resourcepacks"))
            {
                listBox2.Items.Add(Path.GetFileName(name));
            }
        }
    }
}
