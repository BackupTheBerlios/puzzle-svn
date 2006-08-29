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

        public BotBase(World world) : this(world, new Point(0, 0), new Point(0, 0)) {}

        public BotBase(World world, Point location) : this(world, location, new Point(0, 0)) {}

        public BotBase(World world, Point location, Point vector)
        {
            if (world == null)
                throw new ArgumentNullException("world");
            this.world = world;
            world.Bots.Add(this);

            this.location = location;
            this.vector = vector;

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

        #region Vector Property

        private Point vector = new Point(0,0);
        public Point Vector
        {
            get { return vector; }
            set { vector = value; }
        }

        #endregion

        #region Energy Property

        private double energy = 0;
        public double Energy
        {
            get { return energy; }
            set { energy = value; }
        }

        #endregion

        #endregion

        #endregion

        #region Methods

        #region Public Methods

        #region Final Methods

        #region Move Method

        public void Move()
        {
            Move(this.Vector);
        }

        public void Move(Point vector)
        {
            Point location = FindNextLocation(this.Location, vector);

            BounceOnWalls(location);

            if (IsValidLocation(location))
            {
                this.Location = location;
                this.Energy -= CalculateEnergyConsumptionForMove(vector);
            }
        }

        #endregion

        #region Scan Method



        #endregion

        #endregion

        #region Virtual Methods

        #endregion

        #region Abstract Methods

        public abstract void Tick();

        #endregion

        #endregion

        #region Protected Methods

        #region ValidateLocation Method

        protected virtual void ValidateLocation()
        {
            ValidateLocation(this.Location);
        }

        protected virtual void ValidateLocation(Point location)
        {
            if (!IsValidLocation(location))
                throw new Exception(BORDER_COLLISION_MSG);
        }

        protected virtual bool IsValidLocation()
        {
            return IsValidLocation(this.Location);
        }

        protected virtual bool IsValidLocation(Point location)
        {
            if (location.Y < 0)
                return false;

            if (location.Y > this.World.Size.Height)
                return false;

            if (location.X < 0)
                return false;

            if (location.X > this.World.Size.Width)
                return false;

            return true;
        }

        #endregion

        #region BounceOnWalls Method

        protected void BounceOnWalls(Point location)
        {
            if (location.Y < 0 || location.Y > this.World.Size.Height)
                this.Vector = new Point(this.Vector.X, -this.Vector.Y);

            if (location.X < 0 || location.X > this.World.Size.Width)
                this.Vector = new Point(-this.Vector.X, this.Vector.Y);
        }

        #endregion

        #region FindNextLocation Method

        protected Point FindNextLocation(Point location, Point vector)
        {
            return new Point(location.X + vector.X, location.Y + vector.Y);
        }

        #endregion

        protected double CalculateEnergyConsumptionForMove(Point vector)
        {
            //Pythagoras
            return Math.Sqrt((vector.X * vector.X) + (vector.Y * vector.Y));
        }


        #endregion

        #endregion


    }
}
