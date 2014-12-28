using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GLFormsChart.ChartModels
{
    class GLGridItem:IGLItem
    {
        bool isTextShow = true;
        float minX, maxX, minY, maxY, xGridWidth, yGridWidth;
        Color clx;
        Color cly;
        List<GLLineItem> lines = new List<GLLineItem>();
        List<GLText> texts = new List<GLText>();
        private GLChart glCtrl;
        public GLGridItem(float minX, float maxX, float minY, float maxY, float xGridWidth, float yGridWidth,Color clx,Color cly)
        {
            this.minX = minX;
            this.maxX = maxX;
            this.minY = minY;
            this.maxY = maxY;
            this.xGridWidth = xGridWidth;
            this.yGridWidth = yGridWidth;
            this.clx = clx;
            this.cly = cly;
            
        }

        public void init()
        {
            lines.Clear();
            texts.Clear();
            lines.Add(new GLLineItem(new double[] { minX, maxX }, new double[] { minY, minY }, clx));
            lines.Add(new GLLineItem(new double[] { minX, maxX }, new double[] { maxY, maxY }, clx));
            lines.Add(new GLLineItem(new double[] { minX, minX }, new double[] { minY, maxY }, cly));
            lines.Add(new GLLineItem(new double[] { maxX, maxX }, new double[] { minY, maxY }, cly));

            for (float i = minY; i < maxY; i += yGridWidth)
            {
                lines.Add(new GLLineItem(new double[] { minX, maxX }, new double[] { i, i }, clx));
                texts.Add(new GLText(glCtrl, i.ToString("0.000"), clx, minX, i, null, GLTextMode.RightCenter));
            }
            for (float i = minX; i < maxX; i += xGridWidth)
            {
                lines.Add(new GLLineItem(new double[] { i, i }, new double[] { minY, maxY }, cly));
                texts.Add(new GLText(glCtrl, i.ToString("0.000"),cly, i, minY, null, GLTextMode.CenterTop));
            }
        }


        public void DrawSelf()
        {
            foreach (var item in lines)
            {
                item.DrawSelf();
            }
            if (isTextShow)
            {
                foreach (var item in texts)
                {
                    item.DrawSelf();
                }
            }
        }


        public void OnCreat(GLChart glCtrl)
        {
            this.glCtrl = glCtrl;
            init();
        }


    }
}
