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

        // A counter to stop randomizing indices
        private int counter;

        public DataCollector(StreamWriter outputFile)
        {
            this.outputFile = outputFile;
            
            // Initialize the canvas with radius 5
            this.canvas = new Canvas(5);
            canvas.MouseClick += this.OnCanvasMouseClick;

            // Initialize the points
            InitializePoints();

            // Initialize the hashtable
            indexHash = new Hashtable();

            this.random = new Random();

            counter = 1;
        }

        public void StartDataCollection()
        {
            Application.Run(canvas);
        }

        private void OnCanvasMouseClick(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Mouse press at: (" + e.X.ToString() + "," + e.Y.ToString() + ")");

            // Save data
            Point drawPoint = canvas.GetLastDrawPoint();
            Point clickPoint = new Point(e.X, e.Y);
            RegisterData(drawPoint, clickPoint);

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
            }

            System.Diagnostics.Debug.WriteLine("Next point is: " + nextPoint.ToString());
            canvas.DrawCircle(nextPoint); // the canvas automatically erases the existing circle
        }

        private void RegisterData(Point drawPoint, Point clickPoint)
        {
            //Write clickPoint to file
            outputFile.WriteLine(drawPoint.ToString() + " " + clickPoint.ToString());
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

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DataCollector dataCollector = new DataCollector(outputFile);
            dataCollector.StartDataCollection();
        }

    }
}



