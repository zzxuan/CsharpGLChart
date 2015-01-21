using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GLFormsChart.ChartModels;
using System.Drawing;

namespace GLFormsChart
{
    /// <summary>
    /// 曲线视图
    /// </summary>
    class GLCurveView:IGLView
    {
        GLChart chart;
        GLGridItem grid;
        public void OnCreat(GLChart chart)
        {
            this.chart = chart;
            chart.MouseDown += new System.Windows.Forms.MouseEventHandler(chart_MouseDown);
        }

        void chart_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            
        }

        public void OnSizeChange()
        {
            grid = new GLGridItem((float)chart.minX, (float)chart.maxX, (float)chart.minY, (float)chart.maxY, 10, 10, Color.FromArgb(100, 0x36, 0x64, 0x8B), Color.FromArgb(100, 0x36, 0x64, 0x8B));
            grid.OnCreat(chart);
        }

        public void OnDrawSelf()
        {
            grid.DrawSelf();
        }
    }
}
