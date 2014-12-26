using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using CsGL.OpenGL;

namespace GLFormsChart.ChartModels
{
    class GLLineItem:IGLItem
    {
        List<double> xptslist = new List<double>();
        List<double> yptslist = new List<double>();
        IntPtr ptr;
        string lineid;
        string desc;
        Color clr;
        float lineWidth=1;
        public GLLineItem(string lineid, double[] xpts, double[] ypts, Color cl, int lineWidth,string linedesc)
        {
            this.lineid = lineid;
            this.desc = linedesc;
            xptslist.AddRange(xpts);
            yptslist.AddRange(ypts);
            this.clr = cl;
            ptr = GLHelper.LinePtr(xpts, ypts);
            this.lineWidth = lineWidth;
        }
        public GLLineItem(double[] xpts, double[] ypts, Color cl, int lineWidth = 1)
        {
            xptslist.AddRange(xpts);
            yptslist.AddRange(ypts);
            this.clr = cl;
            ptr = GLHelper.LinePtr(xpts, ypts);
            this.lineWidth = lineWidth;
        }
        public void ChartLineAdd(double x, double y)
        {
            xptslist.Add(x);
            yptslist.Add(y);
            ptr = GLHelper.LinePtr(xptslist.ToArray(), yptslist.ToArray());
        }
        void ChartLineAdd(double[] xpts, double[] ypts)
        {
            xptslist.AddRange(xpts);
            yptslist.AddRange(ypts);
            ptr = GLHelper.LinePtr(xptslist.ToArray(), yptslist.ToArray());
        }


        public void DrawSelf()
        {
            GL.glLineWidth(lineWidth);
            GLHelper.GLDrawPolyLine(ptr, xptslist.Count * 3, clr);
        }

        public void OnCreat(OpenGLControl glCtrl)
        {
            //throw new NotImplementedException();
        }

        public void OnCreat(GLChart glCtrl)
        {
            throw new NotImplementedException();
        }
    }
}
