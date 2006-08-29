using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Puzzle.Bots.Domain
{
    public class World
    {
        public World(Size size)
        {
            this.size = size;
        }

        #region Properties

        #region Public Properties

        #region Size Property

        private Size size;
        public Size Size 
        {
            get { return size; }
        }

        #endregion

        #region Bots Property

        private IList<BotBase> bots = new List<BotBase>();
        public IList<BotBase> Bots
        {
            get { return bots; }
        }

        #endregion

        #endregion

        #endregion

        #region Methods

        #region Public Methods

        public void Tick()
        {
            foreach(BotBase bot in this.Bots)
            {
                bot.Tick();
            }
        }

        #endregion

        #endregion

    }
}
