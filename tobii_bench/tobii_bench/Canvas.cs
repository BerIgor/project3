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
    public partial class Canvas : Form
    {

        private Point lastDrawPoint;

        // Graphic fields
        private System.Drawing.SolidBrush myBrush;
        private System.Drawing.Graphics myGraphics;
        private int circleRadius;
        private int circleDiameter;

        // Colors
        private System.Drawing.Color drawColor;
        private System.Drawing.Color eraseColor;

        public Canvas(int circleRadius)
        {
            InitializeComponent();

            // this should be used to override other objects that have focus
            //this.KeyPreview = true; 

            // Make form fullscreen
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.Bounds;

            // Add button even handler to quit (activated when ESC pressed)
            this.KeyDown += new KeyEventHandler(this.OnKeyDown);

            this.circleRadius = circleRadius;
            this.circleDiameter = 2 * circleRadius;

            this.drawColor = System.Drawing.Color.Red;
            this.eraseColor = System.Drawing.Color.White;

            this.myBrush = new System.Drawing.SolidBrush(this.drawColor);
            this.myGraphics = this.CreateGraphics();

        }

        ~Canvas()
        {
            this.myBrush.Dispose();
            this.myGraphics.Dispose();
        }

        public void DrawCircle(Point drawPoint)
        {
            Erase();
            lastDrawPoint = drawPoint;
            int rectCornerX = drawPoint.X - this.circleRadius;
            int rectCornerY = drawPoint.Y - this.circleRadius;

            System.Diagnostics.Debug.WriteLine("Next draw point: " + drawPoint.X.ToString() + "," + drawPoint.Y.ToString());
            myGraphics.FillEllipse(myBrush, rectCornerX, rectCornerY, this.circleDiameter, this.circleDiameter);
        }


        public Point GetLastDrawPoint()
        {
            return lastDrawPoint;
        }


        /* Private methods */

        // Clears the canvas of any drawings
        private void Erase()
        {
            if (lastDrawPoint.IsEmpty)
            {
                return;
            }
            this.myGraphics.Clear(this.eraseColor);
            return;
        }

        // Handles keyboard button down event
        private void OnKeyDown(Object sender, KeyEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Key press");
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        // Does... something
        private void Canvas_Load(object sender, EventArgs e)
        {

        }
    }
}
