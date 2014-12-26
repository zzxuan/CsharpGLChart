using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using CsGL.OpenGL;
using System.Drawing;

namespace GLFormsChart.ChartModels
{
    static class GLHelper
    {
        public static IntPtr LinePtr(double[] xs, double[] ys,double[] zs=null)
        {
            int len = xs.Length;
            var ff = new float[len * 3];
            for (int i = 0; i < len; i++)
            {
                int n = i * 3;
                ff[n] = (float)xs[i];
                ff[n + 1] = (float)ys[i];
                if (zs == null)
                    ff[n + 2] = ChartContants.Z;
                else
                    ff[n + 2] = (float)zs[i];
            }
            return floatstoIntptr(ff);
        }
        public static IntPtr LinePtr(float[] xs, float[] ys, float[] zs = null)
        {
            int len = xs.Length;
            var ff = new float[len * 3];
            for (int i = 0; i < len; i++)
            {
                int n = i * 3;
                ff[n] = xs[i];
                ff[n + 1] = ys[i];
                if (zs == null)
                    ff[n + 2] = ChartContants.Z;
                else
                    ff[n + 2] = zs[i];
            }
            return floatstoIntptr(ff);
        }

        public static void GLDrawPolyLine(IntPtr ptr, int length, Color col)
        {
            GL.glVertexPointer(3, GL.GL_FLOAT, 0, ptr);
            GL.glEnableClientState(GL.GL_VERTEX_ARRAY);
            glColor(col);
            GL.glEnableClientState(GL.GL_TEXTURE_COORD_ARRAY);
            GL.glDrawArrays(GL.GL_LINE_STRIP, 0, length / 3);
            GL.glDisableClientState(GL.GL_VERTEX_ARRAY);
            GL.glDisable(GL.GL_TEXTURE_2D);
        }

        public static IntPtr floatstoIntptr(float[] flos)
        {
            if (flos == null)
                return new IntPtr();
            IntPtr pszSendData0 = new IntPtr();
            pszSendData0 = Marshal.AllocHGlobal(flos.Length * 4);
            Marshal.Copy(flos, 0, pszSendData0, flos.Length);
            return pszSendData0;
        }

        public static void glColor(Color col)
        {
            GL.glColor4f(col.R / 255f, col.G / 255f, col.B / 255f, col.A / 255f);
        }
    }
}
