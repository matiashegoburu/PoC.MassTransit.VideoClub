using Dapper;
using System.Collections.Generic;
using System.Data;
using VideoClub.Entities;

namespace VideoClub.Repositories
{
    public class MembersRepository : IBaseRepository<MemberEntity>
    {
        private readonly IDbConnection _cnn;
        public MembersRepository(IDbConnection cnn)
        {
            _cnn = cnn;
        }

        public void Create(MemberEntity entity)
        {
            _cnn.Execute("INSERT INTO Members(Name) VALUES(@name)", new { name = entity.Name });
        }

        public List<MemberEntity> List()
        {
            return _cnn.Query<MemberEntity>("SELECT * FROM Members").AsList();
        }
    }
}
