using System;

namespace SnakeGame
{
    public interface IGameDevice
    {
        event EventHandler StartButtonPressed;

        event EventHandler StopButtonPressed;

        event EventHandler LeftKeyPressed;

        event EventHandler RightKeyPressed;

        event EventHandler UpKeyPressed;

        event EventHandler DownKeyPressed;

        void Stop();

        void Start();
    }
}
