using System;
using System.Drawing;
using SnakeGame.Business;
using SnakeGame.Common;
using SnakeGame.Data;
using SnakeGame.UI;

namespace SnakeGame
{
    public class GameController
    {
        public Snake snake;
        private DisplayManager DisplayManager;
        private IGameDevice Device;

        public GameController(IGameDevice device, DisplayManager displaymanager)
        {
            Device = device;
            device.StartButtonPressed += device_StartButtonPressed;
            device.StopButtonPressed += device_StopButtonPressed;

            device.LeftKeyPressed += device_LeftKeyPressed;
            device.RightKeyPressed += device_RightKeyPressed;
            device.UpKeyPressed += device_UpKeyPressed;
            device.DownKeyPressed += device_DownKeyPressed;

            DisplayManager = displaymanager;
            DisplayManager.WillCollideWithItself += DisplayManager_WillCollidedWithItself;
            DisplayManager.WillCollideWithWall += DisplayManager_WillCollidedWithWall;
            DisplayManager.FoodTaken += DisplayManager_FoodTaken;
        }

        void device_StopButtonPressed(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void DisplayManager_FoodTaken(object sender, EventArgs e)
        {
            DisplayManager.Show(snake.Tail, snake.Grow());
            DisplayManager.RemoveBait();
            ShowBait();
        }

        void DisplayManager_WillCollidedWithWall(object sender, System.EventArgs e)
        {
            RestartGame();
        }

        private void RestartGame()
        {
            snake = null;
            Device.Stop();
        }

        void DisplayManager_WillCollidedWithItself(object sender, System.EventArgs e)
        {
            RestartGame();
        }

        void device_DownKeyPressed(object sender, System.EventArgs e)
        {
            MoveSnake(Common.Direction.Down);
        }

        void device_UpKeyPressed(object sender, System.EventArgs e)
        {
            MoveSnake(Direction.Up);
        }

        void device_RightKeyPressed(object sender, System.EventArgs e)
        {
            MoveSnake(Direction.Right);
        }

        void device_LeftKeyPressed(object sender, System.EventArgs e)
        {
            MoveSnake(Direction.Left);
        }

        private Random randomgenerator = new Random();

        void ShowBait()
        {
            Block bait = null;
            do
            {
                var bp = new BlockPosition()
                {
                    Column = randomgenerator.Next(0, 60),
                    Row = randomgenerator.Next(0, 25)
                };
                bait = new Block(bp);
                bait.Control.BackColor = Color.Green;

            } while (!DisplayManager.ShowBait(bait));
        }

        void device_StartButtonPressed(object sender, System.EventArgs e)
        {
            DisplayManager.Clear();
            if (snake != null)
            {
                snake.Dispose();
                snake = null;
            }

            snake = new Snake();
            DisplayManager.Show(snake.Head);
            ShowBait();
        }

        private void MoveSnake(Direction direction)
        {
            snake.Move(direction);
            DisplayManager.UpdatePositions();
        }
    }
}
