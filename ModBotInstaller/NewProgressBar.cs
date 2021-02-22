using System.Drawing;
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
            SetStyle(ControlStyles.UserPaint, true);
        }

        public float Progress
        {
            get
            {
                return Value / 100f;
            }
            set
            {
                Value = (int)(value * 100);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_orangeBrush == null)
                _orangeBrush = new SolidBrush(ForeColor);

            if (_backgroundBrush == null)
                _backgroundBrush = new SolidBrush(BackColor);

            Rectangle rec = e.ClipRectangle;

            e.Graphics.FillRectangle(_backgroundBrush, 0, 0, rec.Width, rec.Height);

            rec.Width = (int)(rec.Width * ((double)Value / Maximum)) - (Padding * 2);
            
            rec.Height -= Padding * 2;
            e.Graphics.FillRectangle(_orangeBrush, Padding, Padding, rec.Width, rec.Height);
        }
    }
}
