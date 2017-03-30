using System;
using System.Collections.Generic;
using System.Data;

namespace VideoClub.Repositories
{
    public interface IBaseRepository<TEntity>
    {
        void Create(TEntity entity);
        List<TEntity> List();
    }
}
