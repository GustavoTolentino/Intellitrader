using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoGustavo.Models;

namespace ProjetoGustavo.Controllers.Service
{
    public interface IUsuarioService
    {
        public Guid create(UsuarioRequest user);
        public void update(Guid id, UsuarioRequest userUpdate);
        public List<UsuarioResponse> get();
        public UsuarioResponse getById(Guid id);
        public void delete(Guid id);

    }
}
