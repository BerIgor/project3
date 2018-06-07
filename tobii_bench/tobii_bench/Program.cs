using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;


namespace tobii_bench
{

    class DataCollector
    {
        private Canvas canvas;
        private List<Point> points;
        private StreamWriter outputFile;

        private Hashtable indexHash;
        private Random random;
        private ITracker tracker;

        private double radius = 5.0;
        private int rowCount = 3;
        private int colCount = 5;


        public DataCollector(StreamWriter outputFile)
        {
            this.outputFile = outputFile;
            
            // Initialize the canvas with radius 5
            this.canvas = new Canvas((int)radius);
            canvas.MouseClick += this.OnCanvasMouseClick;

            // Initialize the points
            this.points = InitializePoints(this.canvas.Height, this.canvas.Width);

            // Initialize the hashtable
            indexHash = new Hashtable();

            this.random = new Random();

            this.tracker = new TobiiTracker();
        }

        public void StartDataCollection()
        {
            tracker.Calibrate();
            //canvas.BringToFront();
            Application.Run(canvas);
        }

        private void OnCanvasMouseClick(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Mouse press at: (" + e.X.ToString() + "," + e.Y.ToString() + ")");

            // Save data
            Point drawPoint = canvas.GetLastDrawPoint();
            Point clickPoint = new Point(e.X, e.Y);
            if (!CheckClickPoint(drawPoint, clickPoint))
            {
                return;
            }

            Point gazePoint = tracker.GetGaze();
            RegisterData(drawPoint, clickPoint, gazePoint);

            // Create next point
            if (points.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("No more points. Terminating.");
                canvas.Close();
                this.outputFile.Close();
                Application.Exit();
                return;
            }
            Point nextPoint = GetNextPoint();
            System.Diagnostics.Debug.WriteLine("Next point is: " + nextPoint.ToString());
            canvas.DrawCircle(nextPoint); // the canvas automatically erases the existing circle
        }

        private void RegisterData(Point drawPoint, Point clickPoint, Point gazePoint)
        {
            //Write clickPoint to file
            outputFile.WriteLine(drawPoint.ToString() + " " + clickPoint.ToString() + " " + gazePoint.ToString());
        }

        // Checks if the click point is within the drawn circle
        private bool CheckClickPoint(Point targetPoint, Point clickPoint)
        {

            double distX = Math.Abs(targetPoint.X - clickPoint.X);
            double distY = Math.Abs(targetPoint.Y - clickPoint.Y);
            System.Diagnostics.Debug.WriteLine("distX,distY == " + distX.ToString() +","+distY.ToString());
            if (Math.Pow(distX, 2) + Math.Pow(distY, 2) > Math.Pow(this.radius, 2))
            {
                return false;
            }
            return true;
        }

        // Returns the next point
        private Point GetNextPoint()
        {
            if (this.points.Count == 0)
            {
                throw new InvalidOperationException("All indices used");
            }
            int nextIndex = random.Next(0, this.points.Count);
            Point nextPoint = points[nextIndex];
            points.RemoveAt(nextIndex);
            return nextPoint;
        }

        private List<Point> InitializePoints(int height, int width)
        {
            List<Point> points = new List<Point>();
            int heightInterval = (int)Math.Floor((double)height / (double)(this.rowCount+1));
            int widthInterval = (int)Math.Floor((double)width / (double)(this.colCount+1));
            foreach (int row in Enumerable.Range(1, this.rowCount))
            {
                foreach (int col in Enumerable.Range(1, this.colCount))
                {
                    Point newPoint = new Point(col * widthInterval, row * heightInterval);
                    points.Add(newPoint);
                }
            }
            return points;
        }

    }

    class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
            string relativePath = "/data/output.txt";
            StreamWriter outputFile = new StreamWriter(exeFolder + relativePath);
            outputFile.WriteLine("");
            outputFile.WriteLine("Point Coords | Click Coords | Gaze Coords");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DataCollector dataCollector = new DataCollector(outputFile);
            dataCollector.StartDataCollection();
        }

    }
}



