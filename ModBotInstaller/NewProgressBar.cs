using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModBotInstaller
{
    public class NewProgressBar : ProgressBar
    {
        Brush _orangeBrush;
        Brush _backgroundBrush;

        public int Padding = 1;

        public NewProgressBar()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_orangeBrush == null)
                _orangeBrush = new SolidBrush(this.ForeColor);
            if (_backgroundBrush == null)
                _backgroundBrush = new SolidBrush(this.BackColor);

            Rectangle rec = e.ClipRectangle;

            e.Graphics.FillRectangle(_backgroundBrush, 0, 0, rec.Width, rec.Height);

            rec.Width = (int)(rec.Width * ((double)Value / Maximum)) - (Padding*2);
            //if (ProgressBarRenderer.IsSupported)
            //    ProgressBarRenderer.DrawHorizontalBar(e.Graphics, e.ClipRectangle);
            

            rec.Height = rec.Height - (Padding*2);
            e.Graphics.FillRectangle(_orangeBrush, Padding, Padding, rec.Width, rec.Height);
        }
    }
}
