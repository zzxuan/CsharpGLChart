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
        public GLText(GLChart glCtrl,string text, Color cl, float locationX, float locationY, Font font=null, GLTextMode glmode = GLTextMode.Center)
        {
            this.text = text;
            this.cl = cl;
            this.glmode = glmode;
            this.font = font == null ? new Font("Comic Sans MS", 20) : font;
            this.locationX = locationX;
            this.locationY = locationY;
            this.glCtrl = glCtrl;
            getPad();
        }

        float padx = 0, pady = 0;
        private float locationX;
        float locationY;
        void getPad()
        {
            Size sz = TextRenderer.MeasureText(text, font);
            float w =(float)(glCtrl.ChartX(sz.Width) - glCtrl.ChartX(0));
            float h = (float)(-glCtrl.ChartY(sz.Height) + glCtrl.ChartY(0));
            switch (glmode)
            {
                case GLTextMode.Center:
                    padx = (float)(-0.5 * w);
                    pady = (float)(+0.5 * h);
                    break;
                case GLTextMode.CenterBottom:
                    padx = (float)(-0.25 * w);
                    pady = (float)(0);
                    break;
                case GLTextMode.RightCenter:
                    padx = (float)(-0.5*w);
                    pady = (float)(-0.25 * h);
                    break;
                case GLTextMode.CenterTop:
                    padx = (float)(-0.25 * w);
                    pady = (float)(-0.5 * h);
                    break;
                case GLTextMode.LeftBottom:
                    padx = 0f;
                    pady = 0f;
                    break;
            }
        }

        public void DrawSelf()
        {
            GLHelper.glColor(cl);
            GLFont.Instance.PrintCN(text, font, locationX + padx, locationY + pady, 0);
        }

        public void OnCreat(GLChart glCtrl)
        {

        }


    }
}
