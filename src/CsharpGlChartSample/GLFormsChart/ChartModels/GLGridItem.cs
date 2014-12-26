using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GLFormsChart.ChartModels
{
    class GLGridItem:IGLItem
    {
        float minX, maxX, minY, maxY, xGridWidth, yGridWidth;
        Color clx;
        Color cly;
        List<GLLineItem> lines = new List<GLLineItem>();
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
            init();
        }

        void init()
        {
            lines.Add(new GLLineItem(new double[] { minX, maxX }, new double[] { minY, minY }, clx));
            lines.Add(new GLLineItem(new double[] { minX, maxX }, new double[] { maxY, maxY }, clx));
            lines.Add(new GLLineItem(new double[] { minX, minX }, new double[] { minY, maxY }, cly));
            lines.Add(new GLLineItem(new double[] { maxX, maxX }, new double[] { minY, maxY }, cly));

            for (float i = minY; i < maxY; i += yGridWidth)
            {
                lines.Add(new GLLineItem(new double[] { minX, maxX }, new double[] { i, i }, clx));
            }
            for (float i = minX; i < maxX; i += xGridWidth)
            {
                lines.Add(new GLLineItem(new double[] { i, i }, new double[] { minY, maxY }, cly));
            }
        }


        public void DrawSelf()
        {
            foreach (var item in lines)
            {
                item.DrawSelf();
            }
        }
    }
}
