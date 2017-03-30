using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoClub.Messages.Titulos
{
    public interface CreateTituloMessage
    {
        string Titulo { get; }
        string Descripcion { get; }
        string Genero { get; }
    }

    public class CreateTituloCommand : CreateTituloMessage
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Genero { get; set; }
    }
}
