using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CsGL.OpenGL;
using CsGL;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
//using System.Threading;
//using System.Windows.Threading;

namespace GLFormsChart
{
    public class GLChart : OpenGLControl
    {
        public GLChart()
        {
            init();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            Refresh();
        }
        float[] ff = new float[1 * 3];
        IntPtr ptr;
        void init()
        {
            ff = new float[] 
            {
                0,0,0,
                0,100,0,
                100,0,0
            };
          
            ptr = floatstoIntptr(ff);
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
            GL.glOrtho(-10, 110, -10, 110, 0.1, 100);//正交投影
            //GL.gluPerspective(100.0f, aspect_ratio, 0.0f, 1000.0f);
            GL.glMatrixMode(GL.GL_MODELVIEW); // 设置当前为模型视图矩阵 
            GL.glLoadIdentity();    // 重置模型视图矩阵
        }

        GLFont glFont = new GLFont();
        private int angle;

        public override void glDraw()
        {
            GL.glClearColor(0.1f, 0.1f, 0.2f, 1.0f);
            GL.glClear(GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT);
            GL.glLoadIdentity();
            ////GL.glDisable(GL.GL_DEPTH_TEST);
            //GL.glEnable(GL.GL_LIGHTING);
            //GL.glEnable(GL.GL_LIGHT0);
            GL.gluLookAt(0, 0, 10, 0, 0, 0, 0, 1, 0);
            GL.glPushMatrix();
            //GL.glRotatef(angle++, 1, 0, 0);
            draw(ptr, ff.Length, Color.Red);
            //dd1();
            GL.glColor4f(0, 255, 0, 255);
            
            glFont.PrintCN("12", new Font("宋体",30), 0, 0, 0);
            GL.glPopMatrix();
            GL.glFlush();
            System.Windows.Forms.Application.DoEvents();
        }


        void draw(IntPtr ptr, int length, Color col)
        {
            GL.glVertexPointer(3, GL.GL_FLOAT, 0, ptr);

           //GL.glEnable(GL.GL_TEXTURE_2D);
            // 生成一个纹理引用，并把它和当前的数组绑定  
            //GL.glBindTexture(GL.GL_TEXTURE_2D, textureid);

            GL.glEnableClientState(GL.GL_VERTEX_ARRAY);

            GL.glColor4f(col.R, col.G, col.B, 255);

            GL.glEnableClientState(GL.GL_TEXTURE_COORD_ARRAY);

            //GL.glTexCoordPointer(2, GL.GL_FLOAT, 0, texptr);
            // Draw the vertices as triangle strip  
            GL.glDrawArrays(GL.GL_LINE_LOOP, 0, length / 3);
            // Disable the client state before leaving  
            GL.glDisableClientState(GL.GL_VERTEX_ARRAY);
            //GL.glDisableClientState(GL.GL_TEXTURE_COORD_ARRAY);
            GL.glDisable(GL.GL_TEXTURE_2D);
        }

        void dd1()
        {
            GL.glLineWidth(2);
            GL.glTranslatef(0f, 0f, -6f);
            GL.glColor4f(0, 0, 255, 255);
            GL.glBegin(GL.GL_LINE_LOOP);
            //GL.glLineWidth(2);
            for (int i = 0; i < 50000; i++)
            {
                GL.glVertex3f(0.0f, 1.0f, 0.0f);
                GL.glVertex3f(-1.0f, -1.0f, 0.0f);
                GL.glVertex3f(1.0f, -1.0f, 0.0f);
            }
            GL.glEnd();
            GL.glFlush();
        }

        public static IntPtr floatstoIntptr(float[] flos)
        {
            if (flos == null)
                return new IntPtr();
            IntPtr pszSendData0 = new IntPtr();
            //string str = identity;// +"." + data;

            pszSendData0 = Marshal.AllocHGlobal(flos.Length * 4);

            Marshal.Copy(flos, 0, pszSendData0, flos.Length);

            return pszSendData0;
        }


    }
}
