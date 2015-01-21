using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CsGL.OpenGL;

namespace GLFormsChart
{
    public interface IGLItem
    {

        void OnCreat(GLChart glCtrl);

        void DrawSelf();

    }
}
