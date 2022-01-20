using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoGustavo.Models;
using ProjetoGustavo.Controllers.Service;

namespace ProjetoGustavo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        IUsuarioService usuarioService = new UsuarioService();

        //private readonly IUsuarioService _service;
        //public UsuarioController(IUsuarioService service)
        //{
        //    _service = service;
        //}

        // GET: api/<UsuarioController>
        [HttpGet]
        public IEnumerable<UsuarioResponse> Get()
        {
            List<UsuarioResponse> lstUsuarioResp = usuarioService.get();
            return lstUsuarioResp;
        }

        // GET api/<UsuarioController>/00000000-0000-0000-0000-000000000000
        [HttpGet("{id}")]
        public UsuarioResponse Get(Guid id)
        {
            return usuarioService.getById(id);
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public Guid Post([FromBody] UsuarioRequest userReq)
        {
            return usuarioService.create(userReq);
        }

        // PUT api/<UsuarioController>/00000000-0000-0000-0000-000000000000
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] UsuarioRequest userReq)
        {
            usuarioService.update(id, userReq);
        }

        // DELETE api/<UsuarioController>/00000000-0000-0000-0000-000000000000
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            usuarioService.delete(id);
        }
    }
}
