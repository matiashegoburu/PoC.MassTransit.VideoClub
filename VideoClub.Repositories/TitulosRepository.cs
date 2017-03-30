using System.Data;
using VideoClub.Entities;
using Dapper;
using System.Collections.Generic;

namespace VideoClub.Repositories
{
    public class TitulosRepository : IBaseRepository<TituloEntity>
    {
        private readonly IDbConnection _cnn;

        public TitulosRepository(IDbConnection cnn)
        {
            _cnn = cnn;
        }

        public void Create(TituloEntity entity)
        {
            _cnn.Execute("INSERT INTO Titulos(Titulo, Descripcion, Genero) VALUES(@titulo, @descripcion, @genero)", new { titulo = entity.Titulo, descripcion = entity.Descripcion, genero = entity.Genero });
        }

        public List<TituloEntity> List()
        {
            return _cnn.Query<TituloEntity>("SELECT * FROM Titulos").AsList();
        }
    }
}
