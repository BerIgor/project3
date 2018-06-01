using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tobii.Interaction;

namespace tobii_bench
{
    class TobiiTracker : ITracker
    {
        private Host host;
        private GazePointDataStream gazePointDataStream;
        private Point lastGaze;
        private FixationDataStream fixationDataStream;

        public TobiiTracker()
        {
            host = new Host();

            // 2. Create stream. 
            GazePointDataBehavior b = new GazePointDataBehavior(Tobii.Interaction.Framework.GazePointDataMode.LightlyFiltered);
            gazePointDataStream = host.Streams.CreateGazePointDataStream();

            //FixationDataBehavior behavior = new FixationDataBehavior(Tobii.Interaction.Framework.FixationDataMode.Slow);
            //fixationDataStream = host.Streams.CreateFixationDataStream(behavior);
        }

        ~TobiiTracker()
        {
            host.DisableConnection();
        }

        public Point GetGaze()
        {
            gazePointDataStream.GazePoint(RegisterGaze);
            return lastGaze;
        }



        private void RegisterGaze(double X, double Y, double ts)
        {
            lastGaze = new Point((int)X, (int)Y);
        }

    }
}
