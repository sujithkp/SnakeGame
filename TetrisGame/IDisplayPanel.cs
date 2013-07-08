using SnakeGame.Common;
using SnakeGame.Data;
using SnakeGame.UI;

namespace SnakeGame
{
    public interface IDisplayPanel
    {
        int Height { get; }

        int Width { get; }

        int Top { get; }

        int Left { get; }

        int Columns { get; }

        int Rows { get; }

        void Show(IDisplayable block);

        void Remove(IDisplayable block);

        void UpdatePosition(IDisplayable block);

        void Clear();
    }
}
