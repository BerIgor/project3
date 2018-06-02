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
        private Point[] points;
        private StreamWriter outputFile;

        private Hashtable indexHash;
        private Random random;
        private ITracker tracker;

        private double radius = 5.0;

        // A counter to stop randomizing indices
        private int counter;

        public DataCollector(StreamWriter outputFile)
        {
            this.outputFile = outputFile;
            
            // Initialize the canvas with radius 5
            this.canvas = new Canvas((int)radius);
            canvas.MouseClick += this.OnCanvasMouseClick;

            // Initialize the points
            InitializePoints();

            // Initialize the hashtable
            indexHash = new Hashtable();

            this.random = new Random();

            this.tracker = new TobiiTracker();

            counter = 0;
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

            // Draw a new circle
            Point nextPoint = new Point();
            try
            {
                nextPoint = GetNextPoint();
            }
            catch (InvalidOperationException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                canvas.Close();
                this.outputFile.Close();
                Application.Exit();
                return;
            }

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
        // Throws InvalidOperationException
        private Point GetNextPoint()
        {
            if (counter >= this.points.Length)
            {
                System.Diagnostics.Debug.WriteLine("Reached max indices at: " + counter.ToString());
                throw new InvalidOperationException("All indices used");
            }
            int nextIndex = random.Next(0, this.points.Length);
            while(indexHash.ContainsKey(nextIndex))
            {
                nextIndex = random.Next(0, this.points.Length);
            }
            indexHash.Add(nextIndex, "done");

            counter++;
            return points[nextIndex];
        }

        // TODO: Implement?
        private void Terminate()
        {

        }


        private void InitializePoints()
        {
            Point point0 = new Point(100, 100);
            Point point1 = new Point(200, 200);
            Point point2 = new Point(300, 300);
            Point point3 = new Point(400, 400);
            points = new Point[4] { point0, point1, point2, point3 };
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
            outputFile.WriteLine("Point Coords | Click Coords | Gaze Coords");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DataCollector dataCollector = new DataCollector(outputFile);
            dataCollector.StartDataCollection();
        }

    }
}



