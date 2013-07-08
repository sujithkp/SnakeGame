using System.Windows.Forms;
using SnakeGame.Common;

namespace SnakeGame
{
    public interface IDisplayable
    {
        IBlockPosition Position { get; }

        Control Control { get; }

        int Height { get; }

        int Width { get; }

        void UpdatePosition(AbsolutePosition position);
    }
}
