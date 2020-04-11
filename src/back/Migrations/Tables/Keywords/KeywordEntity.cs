using System.Collections.Generic;
using Migrations.Tables.UsersToKeywords;

namespace Migrations.Tables.Keywords
{
    public class KeywordEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public List<UsersToKeywordsEntity> ToUsers { get; set; }
    }
}