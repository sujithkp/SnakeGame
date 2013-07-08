using System;
using System.Collections.Generic;
using SnakeGame.Data;
using SnakeGame.UI;
using System.Linq;

namespace SnakeGame.Business
{
    public class DisplayManager
    {
        private Dictionary<Fragment, Block> registery;

        private IDisplayPanel DisplayPanel;

        public event EventHandler WillCollideWithWall;

        public event EventHandler WillCollideWithItself;

        public event EventHandler FoodTaken;

        private IDisplayable bait;

        private Fragment head;

        public DisplayManager(IDisplayPanel panel)
        {
            this.DisplayPanel = panel;
            registery = new Dictionary<Fragment, Block>();
        }

        public void Show(Fragment head)
        {
            var block = GetNewBlock();
            DisplayPanel.Show(block);
            registery.Add(head, block);
        }

        public void Show(Fragment previous, Fragment next)
        {
            var previousblock = registery[previous];
            var bp = previousblock.GetTailPosition(next.Direction);
            var newblock = new Block(bp);
            DisplayPanel.Show(newblock);
            registery.Add(next, newblock);
        }

        private Block GetNewBlock()
        {
            return new Block();
        }

        public void UpdatePositions()
        {
            head = head ?? registery.Keys.Single(x => x.IsHead);
            var headblock = registery[head];
            var headblockpos = headblock.Move(head.Direction);

            if (headblockpos.Column < 0 || headblockpos.Column == DisplayPanel.Columns ||
                headblockpos.Row < 0 || headblockpos.Row == DisplayPanel.Rows)
            {
                WillCollideWithWall(this, new EventArgs());
                return;
            }

            if (registery.Values.Any(x => x != headblock && x.Position.Row == headblock.Position.Row
                && x.Position.Column == headblock.Position.Column))
            {
                WillCollideWithItself(this, new EventArgs());
                return;
            }

            headblock.UndoMove();

            if (bait != null && headblock.Position.Row == bait.Position.Row
                && headblock.Position.Column == bait.Position.Column)
            {
                FoodTaken(this, new EventArgs());
            }

            foreach (var pair in registery)
            {
                var f = pair.Key;
                var block = pair.Value;
                var bp = block.Move(f.Direction);//will not move.
                DisplayPanel.UpdatePosition(block);
            }
        }

        public int GetColumns()
        {
            return DisplayPanel.Rows;
        }

        public int GetRows()
        {
            return DisplayPanel.Columns;
        }

        public bool ShowBait(IDisplayable d)
        {
            if (!(registery.Values.Any(x => x.Position.Column == d.Position.Column
                && x.Position.Row == d.Position.Row)))
            {
                bait = d;
                DisplayPanel.Show(d);
                return true;
            }
            return false;
        }

        public void RemoveBait()
        {
            if (bait != null)
                DisplayPanel.Remove(bait);
        }

        public void Clear()
        {
            DisplayPanel.Clear();
            registery.Clear();
            head = null;
        }
    }
}
