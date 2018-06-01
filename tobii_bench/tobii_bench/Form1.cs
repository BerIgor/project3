using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace tobii_bench
{
    public partial class Form1 : Form
    {

        private CoordinateGenerater coordGenerator;
        private System.Drawing.SolidBrush myBrush;
        private System.Drawing.Graphics formGraphics;

        private System.Drawing.Color drawColor = System.Drawing.Color.Red;
        private System.Drawing.Color eraseColor = System.Drawing.Color.White;

        private int CircleRadius;
        private int CircleDiameter;

        



        public Form1()
        {
            InitializeComponent();

            // this should be used to override other objects that have focus
            //this.KeyPreview = true; 

            // Make form fullscreen
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.Bounds;


            // Add mouse even handler
            this.MouseClick += new MouseEventHandler(this.Form1_MouseClick);

            // Add button even handler to quit (activated when ESC pressed)
            this.KeyDown += new KeyEventHandler(this.Form1_KeyDown);

            this.CircleRadius = 5;
            this.CircleDiameter = 2 * this.CircleRadius;

            // Initialize the coordinate randomizeer
            this.coordGenerator = InitCoordGenerator(this.CircleRadius);

            // Create graphics object
            this.myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            formGraphics = this.CreateGraphics();

        }

        ~Form1()
        {
            this.myBrush.Dispose();
            this.formGraphics.Dispose();
        }

        private CoordinateGenerater InitCoordGenerator(int radius)
        {
            // Please note that there are two extra values in the rectangle: X, Y
            Rectangle rect = Screen.FromControl(this).Bounds;
            return new CoordinateGenerater(rect.Width, rect.Height, radius);
        }

        private void Form1_Load(object sender, EventArgs e) {}

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Mouse press at: (" + e.X.ToString() + "," + e.Y.ToString() + ")");
            Coordinates newCoords = this.coordGenerator.GetRandomCoordinates();
            Form1_RedrawCircle(newCoords.getX(), newCoords.getY());
        }

        private void Form1_KeyDown(Object sender, KeyEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Key press");
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void Form1_RedrawCircle(int circleCenterX, int circleCenterY)
        {
            // Clear current drawing
            this.formGraphics.Clear(this.eraseColor);
            
            // System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Red);
            int lowX = circleCenterX - this.CircleRadius;
            int lowY = circleCenterY - this.CircleRadius;
            int highX = circleCenterX + this.CircleRadius;
            int highY = circleCenterY + this.CircleRadius;

            System.Diagnostics.Debug.WriteLine(lowX.ToString() + " " + lowY.ToString() + " " + highX.ToString() + " " + highY.ToString());

            int circleDiameter = 2 * this.CircleRadius;
            formGraphics.FillEllipse(myBrush, circleCenterX, circleCenterY, this.CircleDiameter, this.CircleDiameter);
        }

        private class CoordinateGenerater
        {
            public readonly int radius;

            private Random random;
            private int minX, minY, maxX, maxY;


            // METHODS //
            
            public CoordinateGenerater(int windowWidth, int windowHeight, int radius)
            {
                this.radius = radius;
                this.minX = radius;
                this.minY = radius;
                this.maxX = windowWidth - radius;
                this.maxY = windowHeight - radius;
                this.random = new Random();
            }

            public Coordinates GetRandomCoordinates()
            {
                int x = random.Next(this.minX, this.maxX);
                int y = random.Next(this.minY, this.maxY);
                return new Coordinates(x, y);
            }
        }

        //TODO: Maybe add ToString method
        private class Coordinates
        {
            private readonly int X;
            private readonly int Y;

            public Coordinates(int X, int Y)
            {
                this.X = X;
                this.Y = Y;
            }

            public int getX() {
                return this.X;
            }

            public int getY()
            {
                return this.Y;
            }
        }
    }

}
