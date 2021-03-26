using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;
using System.IO;
using System.Resources;

namespace breakApp
{
    public partial class Form1 : Form
    {
        public int second = 0;
        public int minuts = 0;
        public int hour = 0;

        public int Countdown = 0;
        private WaveOut waveOut = new WaveOut();
        private Setting setting = new Setting();
        private DialogWindow dialog = new DialogWindow();
        public Instruction inst = new Instruction();

        public Form1()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.WorkingArea.Width - 414, System.Windows.Forms.SystemInformation.WorkingArea.Height - 232);
            ViewWin();
            timer1_Tick(null, null);
            timer1.Enabled = true;
            if (Properties.Settings.Default.view)
                Opacity = 1;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ++second;
            if (second == 60)
            {
                second = 0;
                ++minuts;
                ++Countdown;
            }
            if (minuts == 60)
            {
                minuts = 0;
                ++hour;
            }
            if (Countdown == Properties.Settings.Default.workTime - 1 && second == 50)
            {
                if(!Properties.Settings.Default.view)
                    Opacity = 0.1;
                TopMost = true;
            }
            if(Countdown <= Properties.Settings.Default.workTime - 1 && second < 50 && !Properties.Settings.Default.view)
            {
                Opacity = 0;
            }
            if (Countdown == Properties.Settings.Default.workTime - 1 && second == 55 && !Properties.Settings.Default.view)
            {
                Opacity = 1;
            }
            string h = hour.ToString().Length < 2 ? "0" + hour : hour.ToString(); ;
            string min = minuts.ToString().Length < 2 ? "0" + minuts : minuts.ToString();
            string sec = second.ToString().Length < 2 ? "0" + second : second.ToString();

            label1.Text = $"{h}:{min}:{sec}";

            if (Countdown == Properties.Settings.Default.workTime && second == 0)
            {
                alert();
                if(!Properties.Settings.Default.view)
                    Opacity = 0;
                TopMost = false;
                Countdown = 0;
            }

            if (Countdown > Properties.Settings.Default.workTime)
            {
                Countdown = 0;
            }
        }
        private void alert()
        {
            playSound();
            dialog.ShowDialog();
            if (dialog.isViewInstruction)
            {
                inst.ShowDialog();
                dialog.isViewInstruction = false;
            }
            waveOut.Stop();
        }

        private void clearTimer()
        {
            minuts = 0;
            second = 0;
            hour = 0;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            setting.ShowDialog();
        }

        public void playSound()
        {
            Stream st = new MemoryStream(sound());
            var reader = new Mp3FileReader(st);
            waveOut.Init(reader);
            waveOut.Play();
        }

        private Byte[] sound()
        {
            switch (setting.soundActive)
            {
                case "vals" : return Properties.Resources.vals; 
                case "bethoven" : return Properties.Resources.bethoven; 
                case "bethoven2_0" : return Properties.Resources.bethoven2_0;
                default: return null;
            }
        }

        private void ViewWin()
        {
            if (Properties.Settings.Default.view)
            {
                this.pictureBox2.Image = global::breakApp.Properties.Resources.pushpinOn;

                Opacity = 1;
            }
            else
            {
                pictureBox2.Image = global::breakApp.Properties.Resources.pushpin;
                if(Countdown == Properties.Settings.Default.workTime - 1 && second > 50 && second < 55 && !Properties.Settings.Default.view)
                    Opacity = 0.1;
                if (Countdown == Properties.Settings.Default.workTime - 1 && second < 50 && !Properties.Settings.Default.view)
                    Opacity = 0;
            }
        }

        private void viewToggleWin(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.view)
                Properties.Settings.Default.view = false;
            else
                Properties.Settings.Default.view = true;

            Properties.Settings.Default.Save();
            ViewWin();
        }
    }
}
