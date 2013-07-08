using System;
using SnakeGame.Common;

namespace SnakeGame.Data
{
    public class Snake : IDisposable
    {
        public Fragment Head { get; private set; }

        public Fragment Tail;

        public Snake()
        {
            Grow();
        }

        public Fragment Grow()
        {
            if (Tail == null)
                return (Head = Tail = new Fragment(true));

            return Tail = Tail.Grow();
        }

        public void Move(Direction direction)
        {
            if (Head != null)
                Head.Move(direction);
        }

        public void Dispose()
        {
            if (Head != null)
                Head.Dispose();

            this.Head.Dispose();
        }
    }
}
