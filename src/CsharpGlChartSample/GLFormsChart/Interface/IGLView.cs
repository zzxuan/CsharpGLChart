using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GLFormsChart
{
    /// <summary>
    /// 控件视图 定义控件视图样式,以及操作
    /// </summary>
    interface IGLView
    {
        void OnCreat(GLChart chart);

        void OnSizeChange();

        void OnDrawSelf();
    }
}
