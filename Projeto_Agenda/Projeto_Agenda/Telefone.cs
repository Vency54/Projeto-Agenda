using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Projeto_Agenda
{
    public class Telefone
    {
        public string tipo { get; set; }
        public string numero { get; set; }
        public bool principal { get; set; }

        public Telefone(string tipo, string numero, bool principal)
        {
            this.tipo = tipo;
            this.numero = numero;
            this.principal = principal;
        }

        public override string ToString()
        {
            string marcador = principal ? " (Principal)" : "";
            return $"{tipo}: {numero}{marcador}";
        }
    }
}
