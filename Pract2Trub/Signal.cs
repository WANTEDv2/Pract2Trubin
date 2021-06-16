using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract2Trub
{
    class Signal
    {

        public static List<Points> getSignal(int size, double T, double amp1, double amp2, double frequency1, double frequency2) {

            List<Points> points = new List<Points>();
            double f1;
            double f2;
            double y;
            for (int i = 0; i < size; i++)
            {
                f1 = frequency1 * i * T;
                f2 = frequency2 * i * T;
                y = amp1 * Math.Sin(f1) + amp2 * Math.Sin(f2);
                points.Add(new Points(i, y));
            }

            return points;
        }

        public static List<Points> getAmpSignal(int size, double T, double amp1, double amp2, double frequency1, double frequency2)
        {

            List<Points> points = new List<Points>();
            double f1;
            double f2;
            double y;
            for (int i = 0; i < size; i++)
            {
                f1 = frequency1 * i * T;
                f2 = frequency2 * i * T;
                y = amp1 * Math.Cos(f1) + amp2 * Math.Cos(f2);
                points.Add(new Points(i, y));
            }

            return points;
        }
    }
}
