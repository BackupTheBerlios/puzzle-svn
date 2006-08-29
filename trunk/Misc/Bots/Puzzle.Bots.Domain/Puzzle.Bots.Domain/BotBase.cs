using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Puzzle.Bots.Domain
{
    public abstract class BotBase
    {
        #region Constants

        public const string BORDER_COLLISION_MSG = "Your bot fucking died!! It ran into the goddam wall yo!";

        #endregion

        #region Constructors

        public BotBase(World world) : this(world, new Point(0, 0)) {}

        public BotBase(World world, Point location)
        {
            if (world == null)
                throw new ArgumentNullException("world");
            this.world = world;
            world.Bots.Add(this);

            this.location = location;

            ValidateLocation();
        }

        #endregion

        #region Properties

        #region Public Properties

        #region World Property

        private World world;
        public World World
        {
            get  { return this.world; }
        }

        #endregion

        #region Location Property

        private Point location = new Point();
        public Point Location 
        {
            get { return location; }
            set { this.location = value; }
        }

        #endregion

        #endregion

        #endregion

        #region Methods

        #region Public Methods

        #region Final Methods

        public void Move(Direction direction)
        {
            Move(direction, 1);
        }

        public void Move(Direction direction, int length)
        {
            Point location;

            switch (direction)
            {
                case Direction.Up:
                    location = new Point(this.Location.X, this.Location.Y - length);
                    break;
                case Direction.Down:
                    location = new Point(this.Location.X, this.Location.Y + length);
                    break;
                case Direction.Left:
                    location = new Point(this.Location.X - length, this.Location.Y);
                    break;
                case Direction.Right:
                    location = new Point(this.Location.X + length, this.Location.Y);
                    break;
                default:
                    throw new Exception("Unknown direction: " + direction.ToString());
            }
            ValidateLocation(location);

            this.Location = location;
        }

        #endregion

        #region Virtual Methods

        #endregion

        #region Abstract Methods

        public abstract void Tick();

        #endregion

        #endregion

        #region Protected Methods

        protected virtual void ValidateLocation()
        {
            ValidateLocation(this.Location);
        }

        protected virtual void ValidateLocation(Point location)
        {
            if (location.Y < 0)
                throw new Exception(BORDER_COLLISION_MSG);

            if (location.Y > this.World.Size.Height)
                throw new Exception(BORDER_COLLISION_MSG);

            if (location.X < 0)
                throw new Exception(BORDER_COLLISION_MSG);

            if (location.X > this.World.Size.Width)
                throw new Exception(BORDER_COLLISION_MSG);
        }

        #endregion

        #endregion


    }

    public enum Direction
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3
    }
}
