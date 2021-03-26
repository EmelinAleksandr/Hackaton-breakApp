using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using breakApp.Properties;

namespace breakApp
{
    public partial class Setting : Form
    {
        public string soundActive = Settings.Default.activeSound;
        public int time;      

        public Setting()
        {
            InitializeComponent();
            test();
        }

        private void test()
        {
            List<object> items = new List<object>() {
                "vals",
                "bethoven",
                "bethoven2_0",
            };

            comboBox1.DataSource = items;
            comboBox1.SelectedItem = Settings.Default.activeSound;

            List<object> items2 = new List<object>() {
                55,
                45,
                30,
                25,
                20,
                15,
                2,
                1
            };

            comboBox2.DataSource = items2;
            comboBox2.SelectedItem = Settings.Default.workTime;
        }

        private void selectedSound(object sender, EventArgs e)
        {
            soundActive = ((ComboBox)sender).SelectedItem.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Settings.Default.activeSound = soundActive;
            Settings.Default.workTime = time;
            Settings.Default.Save();
            MessageBox.Show("Настройки сохранены");
        }

        private void selectedWorkTime(object sender, EventArgs e)
        {
            time = Convert.ToInt32(((ComboBox)sender).SelectedItem);
        }
    }
}
