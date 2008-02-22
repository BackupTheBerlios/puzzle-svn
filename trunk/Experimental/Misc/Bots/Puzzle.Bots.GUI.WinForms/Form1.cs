using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Puzzle.Bots.Domain;
using Puzzle.Bots.Test;

namespace Puzzle.Bots.GUI.WinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Test();
        }

        private World world = null;

        private void Test()
        {
            world = new World(new Size(300, 300));
            TestBot bot = new TestBot(world, new Point(150, 150), new Point(2, 5));
            TestBot bot2 = new TestBot(world, new Point(250, 150), new Point(3, 1));
            TestBot bot3 = new TestBot(world, new Point(150, 250), new Point(-2, -3));
            //for (int i = 0; i < 200; i++)
            while (true)
            {
                world.Tick();
                this.Refresh();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
         
            if (world != null)
            {
                g.DrawRectangle(new Pen(Color.Black), 
                    0,
                    0,
                    world.Size.Width,
                    world.Size.Height);

                foreach (BotBase bot in world.Bots)
                {
                    g.FillEllipse(new SolidBrush(Color.Black), 
                        bot.Location.X,
                        bot.Location.Y,
                        10,
                        10);

                }
            }
        }
    }
}