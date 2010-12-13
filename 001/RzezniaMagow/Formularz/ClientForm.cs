using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace RzezniaMagow
{
    public partial class ClientForm : Form
    {
        private string IPaddres;
        private int port;
        private string nick;
        private byte avatar;

        public ClientForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IPaddres = maskedTextBox1.Text;
            port = Int32.Parse(maskedTextBox2.Text);
            nick = textBox1.Text;

            if (ICEradioButton.Checked)
            {
                avatar = 2;
            }
            else if (FIREradioButton.Checked)
            {
                avatar = 1;
            }
            else if (LIFEradioButton.Checked)
            {
                avatar = 4;
            }
            else
            {
                avatar = 3;
            }


            Game.client = new ClientLogic();
            //Game.client.connect("192.168.1.3", 20000, "diubhdbbd", 1);
            Game.client.connect(IPaddres, port, nick, avatar);
            Game.czyKlient = true;
            Console.WriteLine("WAITING...");

            Game.screenManager.Visible = false;
            //Game.screenManager.RemoveScreen(this);

            for (int i = 0; i < Game.screenManager.GetScreens().Count; i++)
                Game.screenManager.RemoveScreen(Game.screenManager.GetScreens().ElementAt(i));
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }









        public string getIPaddres
        {
            get { return IPaddres; }
            set { IPaddres = value; }
        }

        public int getPort
        {
            get { return port; }
            set { port = value; }
        }

        public byte getAvatar
        {
            get { return avatar; }
            set { avatar = value; }
        }


        public string getNick
        {
            get { return nick; }
            set { nick = value; }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            FIREradioButton.Checked = false;
            LIFEradioButton.Checked = false;
            ICEradioButton.Checked = true;
            DEATHradioButton.Checked = false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            FIREradioButton.Checked = true;
            LIFEradioButton.Checked = false;
            ICEradioButton.Checked = false;
            DEATHradioButton.Checked = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FIREradioButton.Checked = false;
            LIFEradioButton.Checked = true;
            ICEradioButton.Checked = false;
            DEATHradioButton.Checked = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FIREradioButton.Checked = false;
            LIFEradioButton.Checked = false;
            ICEradioButton.Checked = false;
            DEATHradioButton.Checked = true;
        }
    }
}
