using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Printing;


namespace WindowsFormsApp7
{
    public partial class Form1 : Form
    {
        Bitmap b;
        Graphics g;
        public Color circuitColor;
        public Color fillcolor;
        public Color fillback;
        public SolidBrush fill;
        public int sizeofcir;
        public Form1()
        {
            InitializeComponent();

            SizeOfCircuit f1 = new SizeOfCircuit();                  
            b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = b;
            g = Graphics.FromImage(b);

            circuitColor = new Color();
            circuitColor = Color.Black;
            fillcolor = new Color();
            fillcolor = Color.White;
            fillback = new Color();
            fillback = Color.White;
            pictureBox1.BackColor = fillback;
         }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (checkBox1.Checked == false)
            {  Point[] mypoint =
                {
                    new Point (e.X - 30, e.Y),
                    new Point (e.X - 20, e.Y +20),
                    new Point (e.X + 20 , e.Y+20),
                    new Point (e.X + 30, e.Y),
                    new Point (e.X +20, e.Y- 20 ),
                    new Point (e.X -20, e.Y-20),
                    new Point (e.X - 30, e.Y),
                };
            drawcont(mypoint);
            }
            if (checkBox1.Checked == true)
            {
                Point[] mypoint =
                {
                    new Point (e.X - 30, e.Y),
                    new Point (e.X - 20, e.Y +20),
                    new Point (e.X + 20 , e.Y+20),
                    new Point (e.X + 30, e.Y),
                    new Point (e.X +20, e.Y- 20 ),
                    new Point (e.X -20, e.Y-20),
                    new Point (e.X - 30, e.Y),
                };
                drawcont(mypoint);
                pictureBox1.Image = null;
            }
        }

        public void drawcont(Point[] p)
        {           
            g = Graphics.FromImage(b);
            if (checkBox1.Checked)
            { Thread.Sleep(5); g.Clear(fillback); }
            g.DrawPolygon(new Pen(circuitColor, sizeofcir), p);
            fill = new SolidBrush(fillcolor);
            g.FillPolygon(fill, p);
            pictureBox1.Image = b;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SizeOfCircuit f = new SizeOfCircuit();
            f.ShowDialog();
            sizeofcir = f.Size;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            if (MyDialog.ShowDialog() == DialogResult.OK)
                circuitColor = MyDialog.Color;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            if (MyDialog.ShowDialog() == DialogResult.OK)
                fillcolor = MyDialog.Color;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            if (MyDialog.ShowDialog() == DialogResult.OK)
                fillback = MyDialog.Color;
            pictureBox1.BackColor = fillback;
            pictureBox1.Image = b;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SaveFileDialog st = new SaveFileDialog()
            {
                Filter = "Image files(*.bmp)|*.bmp"
            };
            Bitmap b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.DrawToBitmap(b, pictureBox1.ClientRectangle);

            if (st.ShowDialog() == DialogResult.OK)
            {
                b.Save(st.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 box = new AboutBox1();
            box.ShowDialog();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Text = "X:" + e.X + "\t" + "Y:" + e.Y;

            if (checkBox1.Checked == true)
            {
                Point[] mypoint =
                {
                    new Point (e.X - 30, e.Y),
                    new Point (e.X - 20, e.Y +20),
                    new Point (e.X + 20 , e.Y+20),
                    new Point (e.X + 30, e.Y),
                    new Point (e.X +20, e.Y- 20 ),
                    new Point (e.X -20, e.Y-20),
                    new Point (e.X - 30, e.Y),
                };
                drawcont(mypoint);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Bitmap image; //Bitmap для открываемого изображения

            OpenFileDialog open_dialog = new OpenFileDialog(); //создание диалогового окна для выбора файла
            open_dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*"; //формат загружаемого файла
            if (open_dialog.ShowDialog() == DialogResult.OK) //если в окне была нажата кнопка "ОК"
            {
                try
                {
                    image = new Bitmap(open_dialog.FileName);
                    this.pictureBox1.Size = image.Size;
                    pictureBox1.Image = image;
                    pictureBox1.Invalidate();
                }
                catch
                {
                    DialogResult rezult = MessageBox.Show("Невозможно открыть выбранный файл",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Drawing.Printing.PrintDocument myPrintDocument1 = new System.Drawing.Printing.PrintDocument();
            PrintDialog myPrinDialog1 = new PrintDialog();
            myPrintDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(myPrintDocument2_PrintPage);
            myPrinDialog1.Document = myPrintDocument1;
            if (myPrinDialog1.ShowDialog() == DialogResult.OK)
            {
                myPrintDocument1.Print();
            }
        }
        private void myPrintDocument2_PrintPage(System.Object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap myBitmap1 = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.DrawToBitmap(myBitmap1, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
            e.Graphics.DrawImage(myBitmap1, 0, 0);
            myBitmap1.Dispose();
        }
    }
}
