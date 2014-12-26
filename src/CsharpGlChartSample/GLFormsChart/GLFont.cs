using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;

using CsGL.OpenGL;


namespace GLFormsChart
{
    public class GLFont
    {
        #region 私有字段
        protected IntPtr hDC = Win32.GetDC(IntPtr.Zero);             //暂存与交换字体句柄
        protected IntPtr m_hFont;         //新字体句柄
        protected uint MAX_CHAR = 255;
        protected uint lists = 1000;
        #endregion

        #region 构造函数

        public GLFont()
        {

        }

        #endregion

        #region  方法

        #region 初始化字体
        //初始化字体
        private bool InitFont(Font font)
        {
            try
            {
                if (font == null)
                    font = new Font("宋体", 32);
                m_hFont = Win32.CreateFont((int)font.Size,
                                  0,
                                  0,
                                  0,
                                  Win32.FW_DONTCARE,
                                  0,
                                  0,
                                  0,
                                  Win32.DEFAULT_CHARSET,
                                  Win32.OUT_OUTLINE_PRECIS,
                                  Win32.CLIP_DEFAULT_PRECIS,
                                  Win32.CLEARTYPE_QUALITY,
                                  Win32.VARIABLE_PITCH,
                                  font.FontFamily.Name);

                IntPtr hOldFont = Win32.SelectObject(hDC, m_hFont);//选择字体，得到老字体

                bool b = Win32.DeleteObject(hOldFont);//删除老字体(用新字体替换老字体),这里的bool b只是为了调试的时候监视一下，该不成功照样不成功- -!

                return b;
            }
            catch (Exception)
            { return false; }
        }

        #endregion

        #region 输出文字

        /// <summary>
        /// 只是输出文字，没有字体，不支持中文
        /// </summary>
        /// <param name="str">内容(英文)</param>
        /// <param name="locationX">x坐标</param>
        /// <param name="locationY">y坐标</param>
        /// <param name="locationZ">z坐标</param>
        public void Print(string str, float locationX, float locationY, float locationZ)
        {
            // 申请MAX_CHAR个连续的显示列表编号
            lists = GL.glGenLists((int)MAX_CHAR);
            // 把每个字符的绘制命令都装到对应的显示列表中

            bool b = Win32.wglUseFontBitmaps(hDC, 0, MAX_CHAR, lists); //这里的bool b只是为了调试的时候监视一下，该不成功照样不成功- -!

            GL.glRasterPos3f(locationX, locationY, locationZ);
            for (int i = 0; i < str.Length; i++)
            {
                GL.glCallList((lists + str[i]));
            }
        }

        #endregion

        /// <summary>
        /// 带字体样式输出，仅英文
        /// </summary>
        /// <param name="str">内容</param>
        /// <param name="font">字体</param>
        /// <param name="locationX">x坐标</param>
        /// <param name="locationY">y坐标</param>
        /// <param name="locationZ">z坐标</param>
        public void StylePrint(string str, Font font, float locationX, float locationY, float locationZ)
        {
            InitFont(font);
            Print(str, locationX, locationY, locationZ);
        }

        /// <summary>
        /// 输出中文并带字体样式
        /// </summary>
        /// <param name="str">内容</param>
        /// <param name="font">字体</param>
        /// <param name="locationX">x坐标</param>
        /// <param name="locationY">y坐标</param>
        /// <param name="locationZ">z坐标</param>
        public void PrintCN(string str, Font font, float locationX, float locationY, float locationZ)
        {
            //应用字体
            InitFont(font);
            lists = GL.glGenLists(1);

            GL.glRasterPos3f(locationX, locationY, locationZ);

            for (int i = 0; i < str.Length; i++)
            {
                //只是为了调试的bool b
                bool b = Win32.wglUseFontBitmapsW(hDC, (uint)(str[i]), 1, lists);//一定要注意这里调用的不一样
                GL.glCallList(lists);
            }
        }

        #endregion
    }
}
