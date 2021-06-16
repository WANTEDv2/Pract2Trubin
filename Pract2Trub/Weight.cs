using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Windows.Forms;

namespace Pract2Trub
{
    public class Weight
    {
        public List<WeightPoints> getWindow(ComboBox comboBox , List<Points> signal, double a) {
            List<WeightPoints> result = new List<WeightPoints>();

            double koefSignal;
            double koefFunction;

            switch (comboBox.Text)
            {
                case "Бартлетта":
                    for (int i = 0; i < signal.Count; i++)
                    {
                        koefSignal = (1 - Math.Abs(2.0 * i / (signal.Count - 1) - 1));
                        koefFunction = koefSignal * signal[i].Y;
                        result.Add(new WeightPoints(new Points(i, koefSignal), new Points(i, koefFunction)));
                    }
                    break;
                case "Ханна":
                    for (int i = 0; i < signal.Count; i++)
                    {
                        koefSignal = (0.5 - 0.5 * Math.Cos(2.0 * i * 3.14159 / (signal.Count - 1)));
                        koefFunction = koefSignal * signal[i].Y;
                        result.Add(new WeightPoints(new Points(i, koefSignal), new Points(i, koefFunction)));
                    }
                    break;
                case "Хэмминга":
                    for (int i = 0; i < signal.Count; i++)
                    {
                        koefSignal = (0.54 - 0.46 * Math.Cos(2.0 * i * 3.14159 / (signal.Count - 1)));
                        koefFunction = koefSignal * signal[i].Y;
                        result.Add(new WeightPoints(new Points(i, koefSignal), new Points(i, koefFunction)));
                    }
                    break;
                case "Блэкмана":
                    for (int i = 0; i < signal.Count; i++)
                    {
                        koefSignal = (0.42 - 0.5 * Math.Cos(2.0 * i * 3.14159 / (signal.Count - 1)) + 0.08 * Math.Cos(4.0 * i * 3.14159 / (signal.Count - 1)));
                        koefFunction = koefSignal * signal[i].Y;
                        result.Add(new WeightPoints(new Points(i, koefSignal), new Points(i, koefFunction)));
                    }
                    break;
                case "Натолла":
                    for (int i = 0; i < signal.Count; i++)
                    {
                        koefSignal = (0.3635819 - 0.4891775 * Math.Cos(2.0 * i * 3.14159 / (signal.Count - 1)) + 0.1365995 * Math.Cos(4.0 * i * 3.14159 / (signal.Count - 1)) + 0.0106411 * Math.Cos(6.0 * i * 3.14159 / (signal.Count - 1)));
                        koefFunction = koefSignal * signal[i].Y;
                        result.Add(new WeightPoints(new Points(i, koefSignal), new Points(i, koefFunction)));
                    }
                    break;
                case "Усеченное гауссовское":
                    for (int i = 0; i < signal.Count; i++)
                    {
                        koefSignal = Math.Exp(-0.5 * (Math.Pow((2.0 * i * a / (signal.Count - 1)) - a, 2)));
                        koefFunction = koefSignal * signal[i].Y;
                        result.Add(new WeightPoints(new Points(i, koefSignal), new Points(i, koefFunction)));
                    }
                    break;
                case "Кайзера-Бесселя":
                    double I0 = 0.0;
                    double r = 3.14 * a;
                    for (int i = 0; i < signal.Count; i++)
                    {
                        I0 = 3.14 * a * Math.Sqrt(1.0 - Math.Pow(((i - (signal.Count/ 2.0)) / (signal.Count/ 2.0)), 2.0));
                        koefSignal = KaiserBessel(I0) / KaiserBessel(r);
                        koefFunction = koefSignal * signal[i].Y;
                        result.Add(new WeightPoints(new Points(i, koefSignal), new Points(i, koefFunction)));
                    }
                    break;
            }

            return result;
        }

        private double KaiserBessel(double x)
        {
            double I = 0.0;
            for (int k = 0; k < 32; k++)
            {
                I += Math.Pow((Math.Pow((x / 2.0), k) / fact(k)), 2);
            }
            return I;
        }

        private long fact(int N)
        {
            if (N < 0)
                return 0;
            if (N == 0)
                return 1;
            else
                return N * fact(N - 1);
        }
    }
}
