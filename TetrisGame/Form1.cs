using System;
using System.Windows.Forms;
using System.Drawing;
using SnakeGame;
using SnakeGame.Business;
using SnakeGame.UI;
using System.Text;

namespace SnakeGame
{
    public partial class Form1 : Form, IGameDevice
    {
        #region Events

        public event EventHandler StartButtonPressed;

        public event EventHandler StopButtonPressed;

        public event EventHandler LeftKeyPressed;

        public event EventHandler RightKeyPressed;

        public event EventHandler UpKeyPressed;

        public event EventHandler DownKeyPressed;

        #endregion

        #region Components

        private GameController gameController;

        private IDisplayPanel DisplayPanel;

        private Timer timer;

        private Keys LastKey;

        #endregion

        #region Constructor

        public Form1()
        {
            InitializeComponent();
        }

        #endregion

        #region Privates

        /// <summary>
        /// Initializes the components.
        /// </summary>
        private void Initialize()
        {
            this.DisplayPanel = new DisplayPanel(this.panel1);

            this.Width = DisplayPanel.Left + DisplayPanel.Width + DisplayPanel.Left + 15;
            this.Height = DisplayPanel.Top + DisplayPanel.Height + DisplayPanel.Top + 25;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;

            timer = new Timer();
            timer.Interval = 200;
            timer.Tick += timer_Tick;
        }

        #endregion

        #region Event Handlers

        void timer_Tick(object sender, EventArgs e)
        {
            processkeypress(LastKey, sender, e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Initialize();
            this.BackColor = menuStrip1.BackColor = Color.Gray;
            gameController = new GameController(this, new DisplayManager(this.DisplayPanel));
            Start();
        }

        private void processkeypress(Keys key, object sender, EventArgs e)
        {
            if (LastKey == Keys.Up)
            {
                LastKey = Keys.Up;
                UpKeyPressed(sender, e);
            }

            else if (LastKey == Keys.Down)
            {
                LastKey = Keys.Down;
                DownKeyPressed(sender, e);
            }

            else if (LastKey == Keys.Right)
            {
                LastKey = Keys.Right;
                RightKeyPressed(sender, e);
            }

            else if (LastKey == Keys.Left)
            {
                LastKey = Keys.Left;
                LeftKeyPressed(sender, e);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up && LastKey != Keys.Down)
            {
                LastKey = Keys.Up;
            }

            else if (e.KeyCode == Keys.Down && LastKey != Keys.Up)
            {
                LastKey = Keys.Down;
            }

            else if (e.KeyCode == Keys.Right && LastKey != Keys.Left)
            {
                LastKey = Keys.Right;
            }

            else if (e.KeyCode == Keys.Left && LastKey != Keys.Right)
            {
                LastKey = Keys.Left;
            }

            else if (e.KeyCode == Keys.Z)
            {
                StopButtonPressed(sender, e);
            }

            timer.Start();
        }

        #endregion

        public void Stop()
        {
            timer.Stop();
            LastKey = Keys.Space;
            ShowGameOverMessage();
        }

        public void Start()
        {
            HideGameOverMessage();
            StartButtonPressed(this, new EventArgs());
        }

        private void ShowGameOverMessage()
        {
            this.Controls.Add(GameOverMessageBox);
            GameOverMessageBox.Visible = true;
            GameOverMessageBox.BringToFront();
            button1.Focus();
        }

        private void HideGameOverMessage()
        {
            this.Controls.Remove(GameOverMessageBox);
            GameOverMessageBox.Visible = false;
            this.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HideGameOverMessage();
            StartButtonPressed(sender, e);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var msg = new StringBuilder();
            msg.Append("Snake game");
            msg.Append(Environment.NewLine).Append(Environment.NewLine);
            msg.Append(Environment.NewLine).Append("     Author : Sujith");
            msg.Append(Environment.NewLine).Append("     Date released : June 2013");
            msg.Append(Environment.NewLine).Append(Environment.NewLine);
            MessageBox.Show(msg.ToString());
        }
    }
}
