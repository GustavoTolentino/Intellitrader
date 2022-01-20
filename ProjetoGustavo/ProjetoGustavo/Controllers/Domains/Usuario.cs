using Newtonsoft.Json;
using System;
using System.Collections.Generic;

#nullable disable

namespace ProjetoGustavo.Controllers.Domains
{
    public partial class Usuario
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public DateTime CreationDate { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
