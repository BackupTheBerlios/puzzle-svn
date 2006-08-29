using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.Bots.Domain;
using System.Drawing;

namespace Puzzle.Bots.Test
{
    public class TestBot : BotBase
    {
        public TestBot(World world, Point location) : base(world, location) {}

        public override void Tick()
        {
            cnt++;
            if (cnt < (length * 1))
                this.Move(Direction.Left);
            else if (cnt < (length * 2))
                this.Move(Direction.Up);
            else if (cnt < (length * 3))
                this.Move(Direction.Right);
            else if (cnt < (length * 4))
                this.Move(Direction.Down);
            else
                cnt = 0;
        }

        private int cnt = 0;
        private int length = 30;

    }
}
