using System.Drawing;
using System.Windows.Forms;
using SnakeGame.Common;

namespace SnakeGame.UI
{
    /// <summary>
    /// This is the control where snake is displayed.
    /// </summary>
    public class DisplayPanel : IDisplayPanel
    {
        private Control panel;

        #region Properties

        public int Height
        {
            get { return panel.Height; }
        }

        public int Width
        {
            get { return panel.Width; }
        }

        public int Top
        {
            get { return panel.Top; }
        }

        public int Left
        {
            get { return panel.Left; }
        }

        public int Columns
        {
            get { return DisplaySettings.Columns; }
        }

        public int Rows
        {
            get { return DisplaySettings.Rows; }
        }

        public int BorderWidth
        {
            get { return DisplaySettings.CellBorderWidth; }
        }

        #endregion

        #region Constructor

        public DisplayPanel(Control control)
        {
            panel = control;
            Caliberate();
        }

        #endregion

        #region Public Methods

        private bool headadded = false;

        /// <summary>
        /// puts the block in the display panel.
        /// </summary>
        /// <param name="block"></param>
        public void Show(IDisplayable block)
        {
            UpdatePosition(block);
            AddToPanel(block);
            if (!headadded)
            {
                block.Control.BackColor = Color.Pink;
                headadded = true;
            }
        }

        /// <summary>
        /// Removes the block from the Display panel.
        /// </summary>
        /// <param name="block"></param>
        public void Remove(IDisplayable block)
        {
            panel.Controls.Remove(block.Control);
        }

        /// <summary>
        /// Updates the position of the block.
        /// </summary>
        /// <param name="block"></param>
        public void UpdatePosition(IDisplayable block)
        {
            block.UpdatePosition(ConvertBlockToPixel(block));
        }

        /// <summary>
        /// Clears the display panel.
        /// </summary>
        public void Clear()
        {
            panel.Controls.Clear();
            DrawBorder();
        }

        #endregion

        #region Privates

        /// <summary>
        /// adds the block to the panel.
        /// </summary>
        /// <param name="block"></param>
        private void AddToPanel(IDisplayable block)
        {
            panel.Controls.Add(block.Control);
        }

        /// <summary>
        /// Converts rows and columns to x and y in pixel.
        /// </summary>
        /// <param name="block"></param>
        /// <returns></returns>
        private AbsolutePosition ConvertBlockToPixel(IDisplayable block)
        {
            var position = block.Position;
            int top = this.BorderWidth + (block.Height + this.BorderWidth) * position.Row;
            int left = this.BorderWidth + (block.Width + this.BorderWidth) * position.Column;
            return new AbsolutePosition() { Left = left, Top = top };
        }

        /// <summary>
        /// Adjusts the Height and width of the panel to the number of rows and columns.
        /// </summary>
        private void Caliberate()
        {
            var widthforcells = (DisplaySettings.CellWidth + BorderWidth) * Columns + BorderWidth;

            var heightforcells = (DisplaySettings.CellHeight + BorderWidth) * Rows + BorderWidth;

            this.panel.Height = heightforcells;
            this.panel.Width = widthforcells;
        }

        /// <summary>
        /// Draws grid on the panel.
        /// </summary>
        private void DrawGrid()
        {
            var gridcolor = Color.Silver;

            for (int left = 0; left <= this.panel.Width; left += DisplaySettings.CellWidth + BorderWidth)
                panel.Controls.Add(new Label()
                {
                    Top = 0,
                    Left = left,
                    BackColor = gridcolor,
                    Height = panel.Height,
                    Width = BorderWidth
                });

            for (int top = 0; top <= panel.Height; top += DisplaySettings.CellHeight + BorderWidth)
                panel.Controls.Add(new Label()
                {
                    Top = top,
                    Left = 0,
                    Height = BorderWidth,
                    Width = panel.Width,
                    BackColor = gridcolor,
                });
        }

        /// <summary>
        /// Draw border.
        /// </summary>
        private void DrawBorder()
        {
            panel.Controls.Add(new Label() { BackColor = Color.Red, Left = 0, Top = 0, Width = Width, Height = BorderWidth });
            panel.Controls.Add(new Label() { BackColor = Color.Red, Left = 0, Top = 0, Width = BorderWidth, Height = Height });
            panel.Controls.Add(new Label() { BackColor = Color.Red, Left = Width - BorderWidth, Top = 0, Width = BorderWidth, Height = Height });
            panel.Controls.Add(new Label() { BackColor = Color.Red, Left = 0, Top = Height - BorderWidth, Width = Width, Height = BorderWidth });
        }

        #endregion
    }
}
