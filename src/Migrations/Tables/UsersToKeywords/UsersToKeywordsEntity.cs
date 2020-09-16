using Migrations.Tables.Keywords;
using Migrations.Tables.Users;

namespace Migrations.Tables.UsersToKeywords
{
    public class UsersToKeywordsEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int KeywordId { get; set; }
        public bool Include { get; set; }

        public UserEntity User { get; set; }
        public KeywordEntity Keyword { get; set; }
    }
}