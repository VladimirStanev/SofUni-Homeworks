using System;
using System.Collections.Generic;
using System.Linq;

namespace _05._Football_Team_Generator
{
    public class Team
    {
        private string name;
        private List<Player> playerList;
        private Team()
        {
            this.playerList = new List<Player>();
        }
        public Team(string name) : this()
        {
            this.Name = name;
        }

        public string Name
        {
            get { return this.name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.NameMessage);
                }
                this.name = value;
            }
        }
        public int Rating => this.playerList.Count > 0 ?
            (int)Math.Round(this.playerList.Average(p => p.OverallSkillLevel), 0) : 0;
        public void AddPlayer(Player player)
        {
            this.playerList.Add(player);
        }

        public void RemovePlayer(string player)
        {
            Player playerToRemove = this.playerList.FirstOrDefault(p => p.Name == player);
            if(playerToRemove == null)
            {
                throw new InvalidOperationException(string.Format(
                    ExceptionMessages.InvalidPlayer, player, this.Name));
            }
            this.playerList.Remove(playerToRemove);
        }
        public override string ToString()
        {
            return $"{this.Name} - {this.Rating}";
        }
    }
}
