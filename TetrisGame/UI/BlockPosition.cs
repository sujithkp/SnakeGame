
namespace SnakeGame.UI
{
    public class BlockPosition : IBlockPosition
    {
        public int Row
        {
            get;
            protected internal set;
        }

        public int Column
        {
            get;
            protected internal set;
        }
    }
}
