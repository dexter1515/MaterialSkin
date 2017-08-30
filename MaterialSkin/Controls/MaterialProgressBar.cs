using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace MaterialSkin.Controls
{
    /// <summary>
    /// Material design-like progress bar
    /// </summary>
    public class MaterialProgressBar : ProgressBar, IMaterialControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialProgressBar"/> class.
        /// </summary>
        public MaterialProgressBar()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        /// <summary>
        /// Gets or sets the depth.
        /// </summary>
        /// <value>
        /// The depth.
        /// </value>
        [Browsable(false)]
        public int Depth { get; set; }

        /// <summary>
        /// Gets the skin manager.
        /// </summary>
        /// <value>
        /// The skin manager.
        /// </value>
        [Browsable(false)]
        public MaterialSkinManager SkinManager => MaterialSkinManager.Instance;

        /// <summary>
        /// Gets or sets the state of the mouse.
        /// </summary>
        /// <value>
        /// The state of the mouse.
        /// </value>
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        bool fixedHeight = true;

        public bool FixedHeight
        {
            get { return fixedHeight; }
            set { fixedHeight = value; }
        }


        /// <summary>
        /// Performs the work of setting the specified bounds of this control.
        /// </summary>
        /// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control.</param>
        /// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control.</param>
        /// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control.</param>
        /// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control.</param>
        /// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values.</param>
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if (FixedHeight)
                base.SetBoundsCore(x, y, width, 5, specified);
            else
                base.SetBoundsCore(x, y, width, height, specified);
        }

        System.Drawing.Brush _brush = MaterialSkinManager.Instance.ColorScheme.PrimaryBrush;
        bool useColor = false;

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            var doneProgress = (int)(e.ClipRectangle.Width * ((double)Value / Maximum));
            if (useColor == false)
            {
                _brush = SkinManager.ColorScheme.PrimaryBrush;
            }
            e.Graphics.FillRectangle(_brush, 0, 0, doneProgress, e.ClipRectangle.Height);
            e.Graphics.FillRectangle(SkinManager.GetDisabledOrHintBrush(), doneProgress, 0, e.ClipRectangle.Width, e.ClipRectangle.Height);
        }

        /// <summary>
        /// Sets manual ProgressBar color.
        /// </summary>
        /// <param name="value">The new <see cref="T:System.Drawing.Color" /> of the ProgressBar.</param>
        public void SetColor(Color value)
        {
            this._brush = (Brush)MaterialSkinManager.Instance.ColorScheme.PrimaryBrush.Clone();
            (this._brush as SolidBrush).Color = value;
            useColor = true;
        }

        /// <summary>
        /// Sets ProgressBar color to the Themes default color.
        /// </summary>
        public void ResetColorToThemeDefault()
        {
            useColor = false;
        }
    }
}
