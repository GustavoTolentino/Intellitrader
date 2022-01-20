using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ProjetoGustavo.Models
{
    public class UsuarioRequest
    {
        [Required(ErrorMessage = "O primeiro nome deve ser informado")]
        public string FirstName { get; set; }
        public string Surname { get; set; }
        [Range(1, 120,
        ErrorMessage = "Idade invalida: {0}. Deve estar entre: {1} e {2}.")]
        public int Age { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
