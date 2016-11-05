using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace arackontrl
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string ileri="000"; // Hız değişkenimiz   bu değer 0-255 arası olacak aslında bu değişkeni byte türünde yapmamız gerekiyor ama anlaşılırlık açısından stringden çevirdim
        string sagsol="095"; // Yön değişkenimiz  sağa ve sola kaç derece döneceği  
        string vites = "1"; // vites değişkenimiz 1 ise ileri 0 ise geri gidecek arabamız
        int sayi1 = 80, sayi2 = 110; // Direksiyon değişkenimiz. Faremizi çevirdiğimizde fare konumuna göre araba dönecek
        
        string far = "0"; // far değişkenimiz   0 ise  farlar sönük 1 ise açık olacak
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            
            ileri = trackBar1.Value.ToString();
            lblileri.Text = ileri;
            if (ileri.ToString().Length==1)
            {
                ileri = "00" + ileri;
            }
            else if (ileri.ToString().Length==2)
            {
                ileri = "0" + ileri;
            }
            serialPort1.Write(vites+ ileri + sagsol+ far + ".");
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            serialPort1.PortName = "COM6"; // Serialportumuzun  bağlanacağı port numarasını yazıyoruz arduinonun bağlı olduğu port gelecek buraya arduino ide üzerinden Araçlar>port sekmesinden görebilirsiniz
            serialPort1.Open(); // Arduinomuza bağlandık
            rbtilerivites.Checked = true; // vitesimizi ileri ayarladık
            lblbaglantidurum.Text = "Bağlı"; // Arduinomuza bağlandığımızı ekrana yazdırdık
            lblbaglantidurum.ForeColor = Color.Green;//Bağlandığımız için   bağlandı yazısını yeşil yaptık
        }
       
        
        private void trackBar2_Scroll(object sender, EventArgs e)
        {

            sagsol = trackBar2.Value.ToString();
            lblsagsol.Text = sagsol;
            if (sagsol.ToString().Length == 1)
            {
                sagsol = "00" + sagsol;
            }
            else if (sagsol.ToString().Length == 2)
            {
                sagsol = "0" + sagsol;
            }
            serialPort1.Write(vites + ileri  +sagsol+ far + ".");
        }

        private void rbtilerivites_CheckedChanged(object sender, EventArgs e)
        {
            vites = "1";
            trackBar1.Value = 0;
        }

        private void rbtgerivites_CheckedChanged(object sender, EventArgs e)
        {
            vites = "0";
            trackBar1.Value = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        private void Form1_Move(object sender, EventArgs e)
        {
            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            MessageBox.Show(e.KeyCode.ToString());
        }

        private void Form1_Click(object sender, EventArgs e)
        {
           
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
       
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right) // Faremizin sağ tuşuna basılı ise yapılacak işlemler
            {
                vites = "0"; // vitesi geriye ayarladık
                ileri = "250"; // Hızı  250 yani son hız yaptık  bu hızı  buradan değiştirebilirsiniz  veya trakbar ile kontrol edebilirsiniz.
                sayi1 = 80 + Cursor.Position.X / 52; // Faremizin x pozisyonunu   52 ye böldük ve 80 ile topladık bunu yapmamızdaki sebep bilgisayar çözünürlüğümüzü 85-110 arası yön değeri ile eşitlemek.
                label4.Text = sayi1.ToString();// labela   yön açımızı yazdırdık bunu isterseniz yazdırmayabilrisiniz çok önemli değil bu
                sagsol = sayi1.ToString(); //  arduinomuza gidecek olan yön değişkenimize açımızı verdik
              //  lblsagsol.Text = sagsol; 
                if (sagsol.ToString().Length == 1) // arduino 9 haneli değer okuyacağı için    sayımız 1 haneli ise başına 2 adet sıfır ekledik
                {
                    sagsol = "00" + sagsol;
                }
                else if (sagsol.ToString().Length == 2) // 2 haneli ise 1 adet sıfır başına ekledik
                {
                    sagsol = "0" + sagsol;
                }
                serialPort1.Write(0 + ileri + sagsol + far + "."); // arduinomuza sırası ile vitesimizi, hızı, yönü ve  far değerimizi gönderdik
            }
            else // farenin sol tuşuna bastıysak yapılmasını istediğimiz  şeyleri aşağıda yazıyoruz
            {
                vites = "1"; // vitesi 1 yani ileriye ayarladık  aşağıdaki kodlar yukarıdakinin aynısı
                ileri = "250";
                sayi1 = 80 + Cursor.Position.X / 52;
                label4.Text = sayi1.ToString();
                sagsol = sayi1.ToString();
                lblsagsol.Text = sagsol;
                if (sagsol.ToString().Length == 1)
                {
                    sagsol = "00" + sagsol;
                }
                else if (sagsol.ToString().Length == 2)
                {
                    sagsol = "0" + sagsol;
                }
                serialPort1.Write(1 + ileri + sagsol + far + ".");
            }
           
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e) 
        {
            ileri = "000"; // Hızımızı 0 a düşürdük yani araba durdu
            sayi1 = 80 + Cursor.Position.X / 52;   // yön v.s  kodlarımız  diğer kodların içinde açıkladım tekrar açıklamıyorum
            label4.Text = sayi1.ToString();
            sagsol = sayi1.ToString();
            lblsagsol.Text = sagsol;
            if (sagsol.ToString().Length == 1)
            {
                sagsol = "00" + sagsol;
            }
            else if (sagsol.ToString().Length == 2)
            {
                sagsol = "0" + sagsol;
            }
            serialPort1.Write(vites + 000 + sagsol +far+ ".");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                far = "0";
            }
            else
            {
                far ="1";
            }
            lblileri.Text = ileri;
            if (ileri.ToString().Length == 1)
            {
                ileri = "00" + ileri;
            }
            else if (ileri.ToString().Length == 2)
            {
                ileri = "0" + ileri;
            }
            serialPort1.Write(vites + ileri + sagsol + far + ".");
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            
            sayi1 = 80 + Cursor.Position.X / 42; // bu işlemlerin aynısı yukarıda mevcut. Bunları aslında metotlara ayıracaktım fakat  bana göre böyle daha anlaşılır ondan dolayı ayırmadım kafa karışmaması için
            label4.Text = sayi1.ToString();
            sagsol = sayi1.ToString();
            lblsagsol.Text = sagsol;
            if (sagsol.ToString().Length == 1)
            {
                sagsol = "00" + sagsol;
            }
            else if (sagsol.ToString().Length == 2)
            {
                sagsol = "0" + sagsol;
            }
            serialPort1.Write(vites + ileri + sagsol +far + ".");


        }
    }
}
