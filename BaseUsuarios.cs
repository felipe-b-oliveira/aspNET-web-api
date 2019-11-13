using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webApp
{
    public class BaseUsuarios
    {
        public static IEnumerable<Usuario> Usuarios()
        {
            return new List<Usuario>
            {
                new Usuario { Nome = "Fulano", Senha = "123"},
                new Usuario { Nome = "Beltrano", Senha = "123456"},
                new Usuario { Nome = "Sicrano", Senha = "123456"},
            };
        }
    }

    public class Usuario
    {
        public string Nome { get; set; }
        public string Senha { get; set; }
    }
}