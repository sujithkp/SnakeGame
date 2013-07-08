using System;
using System.Drawing;
using System.Windows.Forms;
using SnakeGame.Common;

namespace SnakeGame.UI
{
    public class Block : IDisposable, IDisplayable
    {
        #region Privates

        private static int Id;

        private static int _width;

        private static int _height;

        private IBlockPosition tempposition;

        #endregion

        #region Properties

        public int Height { get { return _height; } }

        public int Width { get { return _width; } }

        public IBlockPosition Position { get; private set; }

        public Control Control { get; protected set; }

        #endregion

        #region Static Constructor

        static Block()
        {
            Initialize();
        }

        private static void Initialize()
        {
            _height = DisplaySettings.CellHeight;
            _width = DisplaySettings.CellWidth;
        }

        #endregion

        #region Constructor

        public Block()
        {
            Initialize(new BlockPosition());
        }

        public Block(IBlockPosition bp)
        {
            Initialize(bp);
        }

        private void Initialize(IBlockPosition bp)
        {
            this.Control = new Label();
            this.Control.Height = Height;
            this.Control.Width = Width;
            this.Control.BackColor = Color.Black;
            this.Position = bp;
            //this.Control.Text = (Id++).ToString();
            //this.Control.ForeColor = Color.Red;
        }

        #endregion

        #region Public functions

        public virtual void UpdatePosition(AbsolutePosition position)
        {
            this.Control.Top = position.Top;
            this.Control.Left = position.Left;
        }

        public IBlockPosition Move(Direction direction)
        {
            tempposition = this.Position;
            return this.Position = GetNextPosition(direction);
        }

        public void UndoMove()
        {
            this.Position = tempposition;
        }

        public IBlockPosition GetTailPosition(Direction direction)
        {
            if (direction == Direction.Left)
                return new BlockPosition() { Column = this.Position.Column + 1, Row = this.Position.Row };

            if (direction == Direction.Right)
                return new BlockPosition() { Column = this.Position.Column - 1, Row = this.Position.Row };

            if (direction == Direction.Up)
                return new BlockPosition() { Column = this.Position.Column, Row = this.Position.Row + 1 };

            if (direction == Direction.Down)
                return new BlockPosition() { Column = this.Position.Column, Row = this.Position.Row - 1 };

            return null;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Privates

        private IBlockPosition GetNextPosition(Direction direction)
        {
            if (direction == Direction.Left)
                return new BlockPosition() { Column = this.Position.Column - 1, Row = this.Position.Row };

            if (direction == Direction.Right)
                return new BlockPosition() { Column = this.Position.Column + 1, Row = this.Position.Row };

            if (direction == Direction.Up)
                return new BlockPosition() { Column = this.Position.Column, Row = this.Position.Row - 1 };

            if (direction == Direction.Down)
                return new BlockPosition() { Column = this.Position.Column, Row = this.Position.Row + 1 };

            return null;
        }

        #endregion
    }
}
