using System;
using SnakeGame.Common;

namespace SnakeGame.Data
{
    public class Fragment : IDisposable
    {
        public bool IsHead { get; private set; }

        private static int count;

        public int Id { get; private set; }

        public Fragment Next { get; private set; }

        public Direction Direction { get; private set; }

        public void Move(Direction direction)
        {
            if (this.Next != null)
                this.Next.Move(this.Direction);

            this.Direction = direction;
        }

        public Fragment()
        {
            Id = count++;
        }

        public Fragment(bool ishead)
        {
            Id = count++;
            IsHead = ishead;
        }

        public Fragment Grow()
        {
            if (this.Next != null)
                return this.Next.Grow();

            var f = new Fragment() { Direction = this.Direction };
            return (this.Next = f);
        }

        public void Dispose()
        {
            if (this.Next != null)
                this.Next.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
