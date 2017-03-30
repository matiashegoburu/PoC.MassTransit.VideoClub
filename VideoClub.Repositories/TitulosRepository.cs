using System.Data;
using VideoClub.Entities;
using Dapper;

namespace VideoClub.Repositories
{
    public class TitulosRepository : BaseRepository<TituloEntity>
    {
        public TitulosRepository(IDbConnection cnn) : base(cnn)
        {

        }

        public override void Create(TituloEntity entity)
        {
            _cnn.Execute("INSERT INTO Titulos(Titulo, Descripcion, Genero) VALUES(@titulo, @descripcion, @genero)", new { titulo = entity.Titulo, descripcion = entity.Descripcion, genero = entity.Genero });
        }
    }
}
