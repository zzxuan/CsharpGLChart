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
    public class GLChart : OpenGLControl
    {
        public GLChart()
        {
            Timer ti = new Timer();
            ti.Interval = 20;
            ti.Tick += new EventHandler(ti_Tick);
            ti.Start();
        }

        void ti_Tick(object sender, EventArgs e)
        {
            Refresh();
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
            GL.glOrtho(-10, 110, -10, 110, -100, 100);//正交投影
            //GL.gluPerspective(100.0f, aspect_ratio, 0.0f, 1000.0f);
            GL.glMatrixMode(GL.GL_MODELVIEW); // 设置当前为模型视图矩阵 
            GL.glLoadIdentity();    // 重置模型视图矩阵
        }

        GLFont glFont = new GLFont();
        private int angle;
        GLLineItem line = new GLLineItem("", new double[] { 0, 10, 20 }, new double[] { 2, 45, 36 }, Color.Green,2, "");
        GLGridItem grid = new GLGridItem(0, 100, 0, 100, 10, 10, Color.FromArgb(100, 0x36, 0x64, 0x8B), Color.FromArgb(100, 0x36, 0x64, 0x8B));
        public override void glDraw()
        {
            GL.glClearColor(0x99 / 255f, 0x99 / 255f, 0x99 / 255f, 1.0f);
            //GL.glClearColor(0.1f, 0.1f, 0.2f, 1.0f);
            GL.glClear(GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT);
            GL.glLoadIdentity();
            GL.glRotated(angle++, 1, 1, 0);
            ////GL.glDisable(GL.GL_DEPTH_TEST);
            //GL.glEnable(GL.GL_LIGHTING);
            //GL.glEnable(GL.GL_LIGHT0);
            GL.gluLookAt(0, 0, 10, 0, 0, 0, 0, 1, 0);
            GL.glColor4f(0.1f, 0.1f, 0.2f, 1.0f);
            GL.glRectf(0, 0, 100,100);
            
            GL.glColor4f(0, 255, 0, 255);
            //Size sz = TextRenderer.MeasureText("大家好", new Font("宋体", 30));
            //glFont.PrintCN("大家好", new Font("宋体", 30), 0 - (120f / Width) * sz.Width * 0.5f, 0 - (120f / Height) * sz.Height * 0.6f, 0);
            //glFont.PrintCN("大家好", new Font("宋体", 30), 10 - (120f / Width) * sz.Width * 0.5f, 0 - (120f / Height) * sz.Height * 0.5f, 0);
            grid.DrawSelf();
            line.DrawSelf();
            //glFont.PrintCN("曲线控件", new Font("黑体", 100), 50, 101, 0);
            GL.glFlush();
        }
    }
}
