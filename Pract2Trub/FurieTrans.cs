using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

namespace Pract2Trub
{
    /// Преобразование Фурье 
    /// </summary> 
    public class FurieTrans
    {

        /* 
         * Количество точек, которые закодированы в данном ДПФ 
         * */
        public double K;

        public FurieTrans()
        {

        }


        /// Выполнить дискретное преобразование Фурье 
        public List<Complex> dpf(List<Points> points)
        {
            List<Complex> koeffs = new List<Complex>();

            //Цикл вычисления коэффициентов 
            for (int i = 0; i < points.Count; i++)
            {
                Complex sum = new Complex(0, 0);
                for (int j = 0; j < points.Count; j++)
                {
                    double koeff = - (2 * Math.PI * j * i) / points.Count;
                    Complex z = new Complex(Math.Cos(koeff), Math.Sin(koeff));
                    sum += points[j].Y * z;
                }
                koeffs.Add(sum);
            }

            return koeffs;
        }


        /// Взвешенный сигнал
        public List<Complex> weightedDpf(List<Complex> dpf)
        {
            List<Complex> result = new List<Complex>();

            //Цикл вычисления коэффициентов 
            for (int i = 0; i < dpf.Count; i++)
            {
                Complex sum = new Complex(0, 0);
                for (int j = 0; j < dpf.Count; j++)
                {
                    double koeff = 2 * Math.PI * j * i / dpf.Count;
                    Complex z = new Complex(Math.Cos(koeff), Math.Sin(koeff));
                    sum += dpf[j] * z;
                }
                result.Add(sum / dpf.Count);
            }

            return result;
        }


        /*
         * Обратное преобразование Фурье
        */
        public List<Complex> undpf(List<Complex> koeffs)
        {
            List<Complex> res = new List<Complex>();
            for (int k = 0; k < koeffs.Count - 1; k++)
            {
                Complex summa = new Complex();
                for (int u = 0; u < koeffs.Count; u++)
                {
                    double koeff = 2 * Math.PI * u * k / koeffs.Count;
                    Complex e = new Complex(Math.Cos(koeff), Math.Sin(koeff));
                    summa += (koeffs[u] * e);
                }
                res.Add(summa / koeffs.Count);
            }
            return res;
        }

    }
}