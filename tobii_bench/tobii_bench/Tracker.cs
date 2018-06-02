using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace tobii_bench
{
    interface ITracker
    {
        Point GetGaze();
        void Calibrate();
    }
}
