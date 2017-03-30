using System;
using System.Data;

namespace VideoClub.Repositories
{
    public abstract class BaseRepository<TEntity>
    {
        protected readonly IDbConnection _cnn;

        public BaseRepository(IDbConnection cnn)
        {
            _cnn = cnn;
        }

        public abstract void Create(TEntity entity);
    }
}
