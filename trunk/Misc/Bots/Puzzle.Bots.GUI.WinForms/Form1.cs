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
            TestBot bot = new TestBot(world, new Point(150, 150));
            TestBot bot2 = new TestBot(world, new Point(250, 150));
            TestBot bot3 = new TestBot(world, new Point(150, 250));
            for (int i = 0; i < 200; i++)
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
                foreach (BotBase bot in world.Bots)
                {
                    g.FillEllipse(new SolidBrush(Color.Black), 
                        bot.Location.X,
                        bot.Location.Y,
                        3,
                        3);

                }
            }
        }
    }
}