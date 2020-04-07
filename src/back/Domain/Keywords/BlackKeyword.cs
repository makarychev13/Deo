namespace Domain.Keywords
{
    public sealed class BlackKeyword
    {
        public readonly int Id;
        public readonly string Name;

        public BlackKeyword(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}