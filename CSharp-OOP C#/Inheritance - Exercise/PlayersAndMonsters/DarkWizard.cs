namespace PlayersAndMonsters
{
    public class DarkWizard : Wizard
    {
        public DarkWizard(string username, int level) : base(username,level)
        {
           
        }
        public override string Username { get => base.Username; set => base.Username = value; }
        public override int Level { get => base.Level; set => base.Level = value; }
    }
}