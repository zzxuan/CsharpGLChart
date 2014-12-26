using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CsGL.OpenGL;
using System.Drawing;
using System.Windows.Forms;

namespace GLFormsChart.ChartModels
{
    /// <summary>
    /// 文字对齐方式
    /// </summary>
    enum GLTextMode
    {
        LeftBottom,
        CenterTop,
        RightCenter,
        Center,
        CenterBottom
    }
    class GLText:IGLItem
    {
        GLChart glCtrl;
        string text;
        Color cl;
        GLTextMode glmode;
        private Font font;
        public GLText(string text, Color cl, float locationX, float locationY, Font font=null, GLTextMode glmode = GLTextMode.Center)
        {
            this.text = text;
            this.cl = cl;
            this.glmode = glmode;
            this.font = font == null ? new Font("宋体", 30) : font;
        }

        public void DrawSelf()
        {
            GL.glColor4f(0, 255, 0, 255);

            Size sz = TextRenderer.MeasureText(text, font);



            //Size sz = TextRenderer.MeasureText("大家好", new Font("宋体", 30));
            //glFont.PrintCN("大家好", new Font("宋体", 30), 0 - (120f / Width) * sz.Width * 0.5f, 0 - (120f / Height) * sz.Height * 0.6f, 0);

        }

        public void OnCreat(GLChart glCtrl)
        {
            this.glCtrl = glCtrl;
        }
    }
}
