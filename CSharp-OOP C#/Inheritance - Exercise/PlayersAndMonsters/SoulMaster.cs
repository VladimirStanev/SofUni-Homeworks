namespace PlayersAndMonsters
{
    public class SoulMaster : DarkWizard
    {
        public SoulMaster(string username, int level) : base(username, level)
        {
          
        }
        public override string Username { get => base.Username; set => base.Username = value; }
        public override int Level { get => base.Level; set => base.Level = value; }
    }
}
