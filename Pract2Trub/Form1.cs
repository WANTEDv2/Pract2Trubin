using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Pract2Trub
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            chart1.Series[0].LegendText = "Сигнал";
            chart1.Series.Add("Взвешивающая функция");
            chart1.Series.Add("Взвешенная функции");

            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart1.Series[2].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            
            chart1.Series[1].Color = Color.Red;
            chart1.Series[2].Color = Color.Green;


            chart2.Series[0].LegendText = "Дискретное преобразование Фурье (взвешанного сигнала)";


            chart3.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart3.Series[0].LegendText = "Востановленный сигнал";

            chart3.Series.Add ("Востановленный сигнал (взвешанный)");
            chart3.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            

            chart4.Series[0].LegendText = "Амплитудно-частотный спектр (дискретное преобразование Фурье исходного сигнала)";

            label7.Visible = false;
            textBox7.Visible = false;

            groupBox2.Size = new Size(211, 84);

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FurieTrans furieTrans = new FurieTrans();
            Weight weight = new Weight();

            int size = Convert.ToInt32(textBox1.Text);
            double T = Convert.ToDouble(textBox2.Text);
            double amp1 = Convert.ToDouble(textBox3.Text); 
            double amp2 = Convert.ToDouble(textBox4.Text); 
            double frequency1 = Convert.ToDouble(textBox5.Text);
            double frequency2 = Convert.ToDouble(textBox6.Text);
            double a = Convert.ToDouble(textBox7.Text);

           

            List<Points> signalPoints = Signal.getSignal(size, T, amp1, amp2, frequency1, frequency2);
            drowSinusSignal(signalPoints);

            List<Complex> dpf =  furieTrans.dpf(signalPoints);
            drowDpf(dpf, chart4);

            List<Complex> undpf = furieTrans.undpf(dpf);
            drowdUndp(undpf, chart3);

            

            //List<Complex> ws = furieTrans.weightedDpf(dpf);
            //drowdUndp(ws, chart4);

            List<WeightPoints> wp = weight.getWindow(comboBox1, signalPoints, a);

            drowWindow(wp, chart1);

            List<Points> dp = new List<Points>();

            foreach (WeightPoints w in wp) {
                dp.Add(w.koefFunctionPoints);
            }

            List<Complex> dpf2 = furieTrans.dpf(dp);
            drowDpf(dpf2, chart2);

            List<Complex> undpf2 = furieTrans.undpf(dpf2);
            drowdUndp2(undpf2, chart3);
        }


        public void drowSinusSignal(List<Points> points) {

            chart1.Series[0].Points.Clear();
            for (int i = 1; i < points.Count; i++)
            {
                chart1.Series[0].Points.AddXY(points[i].X, points[i].Y);
            }
        }

        public void drowDpf(List<Complex> points, Chart chart)
        {
            chart.Series[0].Points.Clear();
            for (int i = 0; i < points.Count; i++)
            {   
                chart.Series[0].Points.AddXY(i, Math.Sqrt(Math.Pow(points[i].Real, 2) + (Math.Pow(points[i].Imaginary, 2))));
            }
        }

        public void drowWindow(List<WeightPoints> points, Chart chart)
        {
            chart.Series[1].Points.Clear();
            chart.Series[2].Points.Clear();
            for (int i = 0; i < points.Count; i++)
            {
                chart.Series[1].Points.AddXY(points[i].koefSignalPoints.X, points[i].koefSignalPoints.Y);
                chart.Series[2].Points.AddXY(points[i].koefFunctionPoints.X, points[i].koefFunctionPoints.Y);
            }
        }

        public void drowdUndp(List<Complex> points, Chart chart)
        {
            chart.Series[0].Points.Clear();
            for (int i = 0; i < points.Count; i ++)
            {
                chart.Series[0].Points.AddXY(i, points[i].Real);
            }
        }

        public void drowdUndp2(List<Complex> points, Chart chart)
        {
            chart.Series[1].Points.Clear();
            for (int i = 0; i < points.Count; i++)
            {
                chart.Series[1].Points.AddXY(i, points[i].Real);
            }
        }


        public void drowWeigth(List<Points> points, Chart chart)
        {
            chart.Series[0].Points.Clear();
            for (int i = 0; i < points.Count; i++)
            {
                chart.Series[0].Points.AddXY(points[i].X, points[i].Y);
            }
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.Text) {
                case "Усеченное гауссовское":
                case "Кайзера-Бесселя":
                    label7.Visible = true;
                    textBox7.Visible = true;
                    groupBox2.Size = new Size(211, 126);
                    break;

                default:
                    label7.Visible = false;
                    textBox7.Visible = false;
                    textBox7.Visible = true;
                    groupBox2.Size = new Size(211, 84);
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}