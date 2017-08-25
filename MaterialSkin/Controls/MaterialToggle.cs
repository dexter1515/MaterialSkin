using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MaterialSkin.Controls
{
    public class MaterialToggle : CheckBox, IMaterialControl
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager => MaterialSkinManager.Instance;
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        #region Variables

        Timer AnimationTimer = new Timer { Interval = 1 };
        GraphicsPath RoundedRectangle;

        int PointAnimationNum = 4;

        #endregion
        #region  Properties

        #endregion
        #region Events

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            AnimationTimer.Start();
        }

        protected override void OnResize(EventArgs e)
        {
            Height = 19; Width = 47;

            RoundedRectangle = new GraphicsPath();
            int radius = 10;

            RoundedRectangle.AddArc(11, 4, radius - 1, radius, 180, 90);
            RoundedRectangle.AddArc(Width - 21, 4, radius - 1, radius, -90, 90);
            RoundedRectangle.AddArc(Width - 21, Height - 15, radius - 1, radius, 0, 90);
            RoundedRectangle.AddArc(11, Height - 15, radius - 1, radius, 90, 90);

            RoundedRectangle.CloseAllFigures();
            Invalidate();
        }

        #endregion
        public MaterialToggle()
        {
            Height = 19; Width = 47; DoubleBuffered = true;
            AnimationTimer.Tick += new EventHandler(AnimationTick);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            var G = pevent.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.Clear(Parent.BackColor);

            var brush = new SolidBrush(Color.FromArgb(75, Enabled ? Checked ? SkinManager.ColorScheme.AccentColor : SkinManager.GetCheckBoxOffDisabledColor() : SkinManager.GetCheckBoxOffDisabledColor()));
            var brush2 = new SolidBrush(DrawHelper.BlendColor(Parent.BackColor, Enabled ? Checked ? SkinManager.ColorScheme.AccentColor : SkinManager.GetCheckboxOffColor() : SkinManager.GetCheckBoxOffDisabledColor()));
            var pen = new Pen(brush.Color);
   
            G.FillPath(brush, RoundedRectangle);
            G.DrawPath(pen, RoundedRectangle);

            G.FillEllipse(brush2, PointAnimationNum, 0, 18, 18);
            G.DrawEllipse(pen, PointAnimationNum, 0, 18, 18);
        }

        void AnimationTick(object sender, EventArgs e)
        {
            if (Checked)
            {
                if (PointAnimationNum < 24)
                {
                    PointAnimationNum += 3;
                    this.Invalidate();
                }
            }
            else if (PointAnimationNum > 4)
            {
                PointAnimationNum -= 3;
                this.Invalidate();
            }
        }
    }
}

