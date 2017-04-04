using System.Data;
using VideoClub.Entities;
using Dapper;
using System.Collections.Generic;

namespace VideoClub.Repositories
{
    public class TitlesRepository : IBaseRepository<TitleEntity>
    {
        private readonly IDbConnection _cnn;

        public TitlesRepository(IDbConnection cnn)
        {
            _cnn = cnn;
        }

        public void Create(TitleEntity entity)
        {
            _cnn.Execute("INSERT INTO Titles(Title, Description, Category) VALUES(@title, @description, @category)", new { title = entity.Title, description = entity.Description, category = entity.Category });
        }

        public List<TitleEntity> List()
        {
            return _cnn.Query<TitleEntity>("SELECT * FROM Titles").AsList();
        }
    }
}
