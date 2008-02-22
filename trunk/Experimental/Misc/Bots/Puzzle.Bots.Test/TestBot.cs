using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.Bots.Domain;
using System.Drawing;

namespace Puzzle.Bots.Test
{
    public class TestBot : BotBase
    {
        public TestBot(World world, Point location, Point vector) : base(world, location, vector) {}

        public override void Tick()
        {
            this.Move();
        }

    }
}
