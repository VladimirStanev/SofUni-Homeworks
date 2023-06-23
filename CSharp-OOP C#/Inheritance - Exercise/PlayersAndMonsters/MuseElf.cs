namespace PlayersAndMonsters
{
    public class MuseElf : Elf
    {
        public MuseElf(string username, int level) : base(username, level)
        {
            
        }
        public override string Username { get => base.Username; set => base.Username = value; }
        public override int Level { get => base.Level; set => base.Level = value; }
    }
}
