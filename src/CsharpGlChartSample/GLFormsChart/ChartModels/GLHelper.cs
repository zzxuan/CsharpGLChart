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
        public static IntPtr LinePtr(float[] xs, float[] ys)
        {
            int len = xs.Length;
            var ff = new float[len * 3];
            for (int i = 0; i < len; i++)
            {
                int n = i * 3;
                ff[n] = xs[i];
                ff[n + 1] = ys[i];
                ff[n + 2] = ChartContants.Z;
            }
            return floatstoIntptr(ff);
        }

        public static void GLDraw(IntPtr ptr, int length, Color col)
        {
            GL.glVertexPointer(3, GL.GL_FLOAT, 0, ptr);
            GL.glEnableClientState(GL.GL_VERTEX_ARRAY);
            GL.glColor4f(col.R, col.G, col.B, col.A);
            GL.glEnableClientState(GL.GL_TEXTURE_COORD_ARRAY);
            GL.glDrawArrays(GL.GL_LINE_LOOP, 0, length / 3);
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
    }
}
