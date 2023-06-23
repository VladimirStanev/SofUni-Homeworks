namespace _05._Football_Team_Generator
{
    using System;

    public class Stats
    {
        private int endurance;
        private int sprint;
        private int dribble;
        private int passing;
        private int shooting;

        public Stats(int endurance, int sprint, int dribble, int passing, int shooting)
        {
            this.Endurance = endurance;
            this.Sprint = sprint;
            this.Dribble = dribble;
            this.Passing = passing;
            this.Shooting = shooting;
        }

        public int Endurance 
        { 
            get 
            {
                return this.endurance; 
            }
            private set
            {
                if(value < 0 || value > 100)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidStats, nameof(this.Endurance)));
                }
                this.endurance = value;
            }
        }
        public int Sprint
        {
            get
            {
                return this.sprint;
            }
            private set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidStats, nameof(this.Sprint)));
                }
                this.sprint = value;
            }
        }
        public int Dribble
        {
            get
            {
                return this.dribble;
            }
            private set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidStats, nameof(this.Dribble)));
                }
                this.dribble = value;
            }
        }
        public int Passing
        {
            get
            {
                return this.passing;
            }
            private set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidStats, nameof(this.Passing)));
                }
                this.passing = value;
            }
        }
        public int Shooting
        {
            get
            {
                return this.shooting;
            }
            private set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidStats, nameof(this.Shooting)));
                }
                this.shooting = value;
            }
        }
    }
}
