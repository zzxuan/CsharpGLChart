using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CsGL.OpenGL;
using CsGL;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using GLFormsChart.ChartModels;

namespace GLFormsChart
{
    public partial class GLChart : OpenGLControl
    {
        IGLView _ChartView = null;
        List<IGLItem> _GLItems = new List<IGLItem>();
        int leftpad = 100, rightpad = 30,toppad=50,bottompad=30;
        internal double minX, minY, maxX, maxY;
        string title = "OPENGL曲线控件";

        GLText titletext;
        public GLChart()
        {

        }

        public void Init(double minX, double minY, double maxX, double maxY, CharMode mode = CharMode.Curve)
        {
            ChartZoomTo(minX, minY, maxX, maxY);
            if (mode == CharMode.Curve)
            {
                _ChartView = new GLCurveView();
            }
            _ChartView.OnCreat(this);
        }

        protected override void InitGLContext()// 此处开始对OpenGL进行所有设置
        {
            try
            {
                base.InitGLContext();

                GL.glShadeModel(GL.GL_SMOOTH);            // 启用阴影平滑
                GL.glClearDepth(1.0f);                                     // 设置深度缓存
                GL.glEnable(GL.GL_DEPTH_TEST);            // 启用深度测试
                GL.glDepthFunc(GL.GL_LEQUAL);               // 所作深度测试的类型
                GL.glHint(GL.GL_PERSPECTIVE_CORRECTION_HINT, GL.GL_NICEST); // 告诉系统对透视进行修正
            }
            catch { }

        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            GL.glMatrixMode(GL.GL_PROJECTION);     //  设置当前为投影矩阵
            GL.glLoadIdentity();    // 重置投影矩阵
            ChartZoomTo(minX, minY, maxX, maxY);
            //GL.gluPerspective(100.0f, aspect_ratio, 0.0f, 1000.0f);
            GL.glMatrixMode(GL.GL_MODELVIEW); // 设置当前为模型视图矩阵 
            GL.glLoadIdentity();    // 重置模型视图矩阵

            titletext = new GLText(this, title, Color.Blue, (float)(0.5 * (maxX - minX) + minX), (float)(maxY + ChartH(10)), new Font("Comic Sans MS", 50), GLTextMode.CenterBottom);

            _ChartView.OnSizeChange();
        }


        public override void glDraw()
        {
            GL.glClearColor(0x99 / 255f, 0x99 / 255f, 0x99 / 255f, 1.0f);
            GL.glClear(GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT);
            GL.glLoadIdentity();
            GL.gluLookAt(0, 0, 10, 0, 0, 0, 0, 1, 0);
            GL.glColor4f(0.1f, 0.1f, 0.2f, 1.0f);
            GL.glRectd(minX, minY, maxX, maxY);
            titletext.DrawSelf();
            

            _ChartView.OnDrawSelf();

            lock (_GLItems)
            {
                foreach (var item in _GLItems)
                    item.DrawSelf();
            }
            
            GL.glFlush();
        }

        public void AddGLitems(IGLItem item)
        {
            _GLItems.Add(item);
            item.OnCreat(this);
        }

        public void RemoveGLitems(IGLItem item)
        {
            if (_GLItems.Contains(item))
            {
                _GLItems.Remove(item);
            }
        }


        /// <summary>
        /// 设置控件缩放
        /// </summary>
        /// <param name="minX"></param>
        /// <param name="minY"></param>
        /// <param name="maxX"></param>
        /// <param name="maxY"></param>
        public void ChartZoomTo(double minX, double minY, double maxX, double maxY)
        {
            this.maxX = maxX;
            this.minX = minX;
            this.minY = minY;
            this.maxY = maxY;
            double wl = Width - leftpad - rightpad;
            double xx = (maxX - minX) / wl;
            double x3 = minX - leftpad * xx;
            double x4 = rightpad * xx + maxX;

            double hl = Height - toppad - bottompad;
            double yy = (maxY - minY) / hl;
            double y3 = minY - bottompad * yy;
            double y4 = maxY + toppad * yy;

            GL.glOrtho(x3, x4, y3, y4, -100, 100);//正交投影
        }

        public double ChartX(double scrx)
        {
            return (scrx - leftpad) * (maxX - minX) / (Width - leftpad - rightpad);
        }
        public double ChartY(double srcy)
        {
            double h = Height - srcy;
            return (h - bottompad) * (maxY - minY) / (Height - toppad - bottompad);
        }

        public double ChartH(double srch)
        {
            return ChartY(0) - ChartY(srch);
        }
        public double ChartW(double srcw)
        {
            return ChartX(srcw) - ChartX(0);
        }
    }
}
