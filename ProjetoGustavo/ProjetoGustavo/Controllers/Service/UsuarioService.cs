using ProjetoGustavo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoGustavo.Controllers.Contexts;
using ProjetoGustavo.Controllers.Domains;
using log4net;
using log4net.Config;
using Newtonsoft.Json;

namespace ProjetoGustavo.Controllers.Service
{
    public class UsuarioService : IUsuarioService
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(UsuarioService));

        private GustavoContext db = new GustavoContext();
        public Guid create(UsuarioRequest user)
        {
            log.Debug($"Criando novo usuario. UsuarioRequest: {user.ToString()}");
            Usuario newUser = new Usuario();
            newUser.FirstName = user.FirstName;
            newUser.Surname = user.Surname;
            newUser.Age = user.Age;
            newUser.CreationDate = DateTime.Now;
            db.Usuarios.Add(newUser);
            log.Debug($"Salvando novo usuario. Usuario: {newUser.ToString()}");
            db.SaveChanges();

            return newUser.Id;
        }

        public void delete(Guid id)
        {
            Usuario user = db.Usuarios.Where(x => x.Id == id).FirstOrDefault();
            db.Usuarios.Remove(user);
            db.SaveChanges();
            log.Debug($"Usuario deletado: {id.ToString()}");
        }

        public List<UsuarioResponse> get()
        {
            List<Usuario> lstUser = db.Usuarios.ToList();
            List<UsuarioResponse> lstUserResp = new List<UsuarioResponse>();
            foreach (var item in lstUser)
            {
                UsuarioResponse userResp = new UsuarioResponse();
                userResp.Id = item.Id;
                userResp.FirstName = item.FirstName;
                userResp.Surname = item.Surname;
                userResp.Age = item.Age;
                userResp.CreationDate = item.CreationDate;

                lstUserResp.Add(userResp);
            }
            log.Debug($"Usuarios listados: {JsonConvert.SerializeObject(lstUserResp)}");
            return lstUserResp;
        }

        public UsuarioResponse getById(Guid id)
        {

            Usuario user = db.Usuarios.Where(x => x.Id == id).FirstOrDefault();
            UsuarioResponse userResp = new UsuarioResponse();
            if(user != null)
            {
            userResp.Id = user.Id;
            userResp.FirstName = user.FirstName;
            userResp.Surname = user.Surname;
            userResp.Age = user.Age;
            userResp.CreationDate = user.CreationDate;

            }
            return userResp;
        }

        public void update(Guid id, UsuarioRequest userUpdate)
        {
            Usuario userStorageDB = db.Usuarios.Where(x => x.Id == id).FirstOrDefault();
            userStorageDB.FirstName = userUpdate.FirstName;
            userStorageDB.Surname = userUpdate.Surname;
            userStorageDB.Age = userUpdate.Age;
            db.Usuarios.Update(userStorageDB);
            db.SaveChanges();
            log.Debug($"Usuario atualizado: {JsonConvert.SerializeObject(userUpdate)}");
        }
    }
}
