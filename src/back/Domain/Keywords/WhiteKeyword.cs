namespace Domain.Keywords
{
    public sealed class WhiteKeyword
    {
        public readonly int Id;
        public readonly string Name;

        public WhiteKeyword(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}