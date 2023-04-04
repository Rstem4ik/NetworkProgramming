using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Network_Programming
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button1.Text = "Информация о URI";
            button2.Text = "IP сайта";
            button3.Text = "Получение NETBIOS-имя машины";
            button4.Text = "Локальный IP";
            button5.Text = "Ping";
            button6.Text = "Проверка доступности сайта";
            button7.Text = "Получаем имя ПК";
            button8.Text = "Выясняем, подключена ли локальная система к сети, и узнать используемый тип соединения";
            button9.Text = "debug";
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string uri = textBox1.Text;
            UriBuilder  uriBuilder = new UriBuilder(uri);
            listBox2.Items.Add("Хост: " + uriBuilder.Host);
            listBox2.Items.Add("Порт: " + uriBuilder.Port.ToString());
            listBox2.Items.Add("Протокол: " + uriBuilder.Scheme);
            listBox2.Items.Add("URI: "  + uriBuilder.Uri.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Net.IPHostEntry host;
            host = System.Net.Dns.GetHostEntry(textBox2.Text);
            foreach (System.Net.IPAddress ip in host.AddressList)
            {
                MessageBox.Show(ip.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Environment.MachineName);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            String locIP = "";
            System.Net.IPHostEntry host;
            host = System.Net.Dns.GetHostEntry(locIP);
            foreach (System.Net.IPAddress ip in host.AddressList)
            {
                MessageBox.Show(ip.ToString());
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if (!string.IsNullOrEmpty(textBox3.Text))
            {
                Ping pingSend = new Ping();
                try
                {
                    PingReply reply = pingSend.Send(textBox3.Text);
                    if (reply.Status == IPStatus.Success)
                    {
                        listBox1.Items.Add("Address: " + reply.Address.ToString());
                        listBox1.Items.Add("RoundTrip" + reply.RoundtripTime);
                        if (reply.Options != null)
                        {
                            listBox1.Items.Add("Time to live: " + reply.Options.Ttl);
                            listBox1.Items.Add("Don't fragment: " + reply.Options.DontFragment);
                        }
                        if (reply.Buffer != null)
                        {
                            listBox1.Items.Add("Buffer size: " + reply.Buffer.Length);
                        }
                    }

                }
                catch (PingException ex)
                {
                    listBox1.Items.Add("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid IP address.");
            }
        }



        private void CheckWebsiteAvailability(string url)
        {
            try
            {
                Ping pingSender = new Ping();
                PingReply reply = pingSender.Send(url);
                if (reply.Status == IPStatus.Success)
                {
                    MessageBox.Show("Сайт доступен.");
                }
                else
                {
                    MessageBox.Show("Сайт недоступен.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при проверке доступности сайта: " + ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            CheckWebsiteAvailability(textBox4.Text);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string UserName = Environment.UserName;
            MessageBox.Show("Имя пользователя: " + UserName);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface networkInterface in interfaces)
                {
                    if (networkInterface.OperationalStatus == OperationalStatus.Up)
                    {
                        MessageBox.Show("Имя: " + networkInterface.Name);
                        MessageBox.Show("Описание: " + networkInterface.Description);
                        MessageBox.Show("Тип: " + networkInterface.NetworkInterfaceType);
                    }
                }
            }
            else
            {
                Console.WriteLine("Недоступно.");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text = "https://music.yandex.ru/home";
            textBox2.Text = "google.com";
            textBox3.Text = "ya.ru";
            textBox4.Text = "twitch.com";

        }

        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
