using Dapper;
using System.Collections.Generic;
using System.Data;
using VideoClub.Entities;

namespace VideoClub.Repositories
{
    public class RentalsRepository : IBaseRepository<RentalEntity>
    {
        private readonly IDbConnection _cnn;

        public RentalsRepository(IDbConnection cnn)
        {
            _cnn = cnn;
        }

        public void Create(RentalEntity entity)
        {
            _cnn.Execute("INSERT INTO Rentals(MemberId, TitleId, FromDate) VALUES(@memberId, @titleId, @fromDate)", new { memberId = entity.MemberId, titleId = entity.TitleId, fromDate = entity.FromDate});
        }

        public List<RentalEntity> List()
        {
            return _cnn.Query<RentalEntity>("SELECT * FROM Rentals").AsList();
        }
    }
}
