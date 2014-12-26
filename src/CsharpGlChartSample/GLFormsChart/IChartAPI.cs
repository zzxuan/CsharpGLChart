using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GLFormsChart
{
    public interface IChartAPI
    {
        void ChartInit(string title,Color backGround,Color gridColor);
        void ChartMode(int ModelID);
        void ChartSize(double minX, double minY, double maxX, double maxY);
        void ChartDrawLine(string lineid, double[] xpts, double[] ypts,Color cl, string linedesc);
        void ChartLineAdd(string lineid, double x, double y);
        void ChartLineAdd(string lineid, double[] xpts, double[] ypts);

        void ChartZoomTo(double minX, double minY, double maxX, double maxY);
        void ChartRecovery();

        double[] ChartScrXYtoPix(double scrx, double scry);
    }
}
