using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;

namespace apprentissageSUP
{
    public partial class Form1 : Form
    {
        Graphics g;
        Bitmap bmp;
        Reseau reseau;
        bool learning = false;
        double seuil = -1;
        Thread procThread;
        List<List<double>> data;
        List<double> sortiesReseau;
        private static Random rng = new Random(); 
        int nbIt = 0;
        DirectoryInfo ScreenD;
        DirectoryInfo CurrentD;
        DateTime start;

        public void shuffle<T>(List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        public void screenShot(DateTime offset)
        {
            
            this.Invoke((MethodInvoker)delegate
            {
                if (!cbScreen.Checked)
                    return;
                var form = this;
                using (var bmpForm = new Bitmap(form.Width, form.Height))
                {
                    var diff = DateTime.Now - offset;
                    string outputfile = CurrentD.FullName + Path.DirectorySeparatorChar + nbIt + "-" + diff.Minutes + "m" + diff.Seconds + "s.png";
                    form.DrawToBitmap(bmpForm, new Rectangle(0, 0, bmpForm.Width, bmpForm.Height));
                    bmpForm.Save(outputfile, System.Drawing.Imaging.ImageFormat.Png);
                }
            });
        }
        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(pBoxOut.Width, pBoxOut.Height);
            pBoxOut.Image = bmp;
            g = Graphics.FromImage(pBoxOut.Image);
            Pen pen = new Pen(Color.White, 1);
            g.FillRectangle(pen.Brush, 0, 0, pBoxOut.Width, pBoxOut.Width);
            data = new List<List<double>>();
            sortiesReseau = new List<double>();
            string path ="Screens";
            ScreenD = Directory.CreateDirectory(path);            
        }

        private bool learn()
        {
            //1 shuffle
            shuffle(data);
            //boosting;
            //data.Sort(delegate(List<double> t1, List<double> t2) { return t1[3].CompareTo(t2[3]); });
            double perc =  Math.Min(1.0, (double)nbIt / 5000f);
            this.Invoke((MethodInvoker)delegate
            {
                nNbIt.Enabled = false;
                nNbNeu.Enabled = false;
                nAlpha.Enabled = false;
                lState.Text = "Learning";
            });
            List<List<double>> lvecteursentrees = new List<List<double>>();
            List<double> lsortiesdesirees = new List<double>();
            for(int i = 0; i<data.Count*perc;++i)
            {
                List<double> d = data[i];
                lvecteursentrees.Add(new List<double> { d[0] / 500f, d[1] / 500f });
                lsortiesdesirees.Add(d[2]);
            }
            reseau.backprop(lvecteursentrees, lsortiesdesirees,
                                (double)nAlpha.Value,
                                (int)nNbIt.Value);
            bool fini = false;
            this.Invoke((MethodInvoker)delegate
            {
                nNbIt.Enabled = true;
                nNbNeu.Enabled = true;
                nAlpha.Enabled = true;
                lState.Text = "waiting";
                nbIt += (int)nNbIt.Value;
                lClearn.Text = "" + nbIt;
                fini = checkThresh();
                if (fini)
                {
                    learning = false;
                    btnCLearn.Text = "loop learn";
                    lC1.Text += "Leaning Finished\nSeuil = " + seuil.ToString("0.000");
                    lState.Text = "Finished";
                }
                
            });
            return fini;
        }
        private void openD_Click(object sender, EventArgs e)
        {

            openFileDialog.Filter = "txt files (*.txt)|*.txt";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            seuil = -1;
            System.IO.StreamReader file = new System.IO.StreamReader(openFileDialog.FileName);
            string line;
            while ((line = file.ReadLine()) != null)
            {
                string[] res = line.Split(null);
                double x = Convert.ToDouble(res[0]);
                double y = Convert.ToDouble(res[1]);
                double classe = Convert.ToDouble(res[2]);
                //data : x,y,value,error;
                data.Add(new List<double> { x, y, classe,0 });
            }
            reseau = new Reseau(3, // x y + cste
                                3,
                                (int)nNbNeu.Value);

            //learn();
            drawData();
        }
        private void drawData()
        {            
            if(sortiesReseau.Count != 0)
                for(int x = 0;x<bmp.Width;++x)
                    for (int y = 0; y < bmp.Height; ++y)
                    {
                        double cValue = sortiesReseau[x * bmp.Width + y];
                        if (seuil > 0)
                            cValue = cValue > seuil ? 1 : 0;
                        int c = (int)(cValue * 255);
                        bmp.SetPixel(x, y, Color.FromArgb(255-c, 0, c));
                    }
            
            foreach (List<double> d in data)
            {
                int c = (int)(d[2]*255);
                bmp.SetPixel((int)d[0], (int)d[1], Color.FromArgb(c,c,c));
            }
            this.Invoke((MethodInvoker)delegate
            {
                pBoxOut.Refresh();
            });
        }

        
        private bool process()
        {
            bool lfini = learn();
            drawData();
            return lfini;
        }
        private void loopProcess()
        {
            start = DateTime.Now;

            string sub = ScreenD.FullName + Path.DirectorySeparatorChar + nNbNeu.Value + "NeurParCouche_alpha"+((double)nAlpha.Value).ToString("0.00");
            CurrentD = Directory.CreateDirectory(sub); 
            while (learning)
            {
                screenShot(start);
                if (process())
                    break;
            }
            screenShot(start);
            MessageBox.Show("Fini\r\nScreen dans :\r\n" + sub + Path.DirectorySeparatorChar);
        }
        private bool checkThresh()
        {
            List<List<double>> lvecteursentrees = new List<List<double>>();
            for (int x = 0; x < bmp.Width; ++x)
                for (int y = 0; y < bmp.Height; ++y)
                    lvecteursentrees.Add(new List<double> { x / 500f, y / 500f });

            sortiesReseau = reseau.ResultatsEnSortie(lvecteursentrees);
            //on sait qu'on veut deux classes pour 0.2 et 08
            double max02 = 0;
            double min08 = 1;
            double moyC1 = 0;
            int cpt = 0;
            double moyC2 = 0;
            foreach (List<double> d in data)
            {
                double outValue = sortiesReseau[(int)d[0] * bmp.Width + (int)d[1]];
                d[3] = Math.Abs(d[2] - outValue);
                if (d[2] < 0.5)
                {
                    moyC1 += outValue;
                    cpt++;
                    max02 = Math.Max(max02, outValue);
                }
                else
                {
                    moyC2 += outValue;
                    min08 = Math.Min(min08, outValue);
                }
            }
            moyC1 /= cpt;
            moyC2 /= data.Count - cpt;
            lC1.Text = "Max Value Classe    < 0.5 : " + max02.ToString("0.000")+"\n";
            lC1.Text += "Mean Value Classe < 0.5 : " + moyC1.ToString("0.000")+"\n\n";
            lC1.Text += "Min Value Classe    > 0.5 : " + min08.ToString("0.000")+"\n";
            lC1.Text += "Mean Value Classe > 0.5 : " + moyC2.ToString("0.000")+"\n";
            
            bool ret = min08-max02>0;
            if (min08 - max02 > 0)
                seuil = (min08 + max02) / 2;
            return ret;
        }
        private void btnCLearn_Click(object sender, EventArgs e)
        {
            if (learning)
            {
                learning = false;
                btnCLearn.Text = "loop learn";
            }
            else
            {
                learning = true;
                btnCLearn.Text = "stop learning";
                procThread = new Thread(loopProcess);
                procThread.Start();
            }
        }

    }
}
