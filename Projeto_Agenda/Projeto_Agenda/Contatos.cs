using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Collections.Generic;

namespace Projeto_Agenda
{
    public class Contatos
    {
        private readonly List<Contato> agenda;

        public Contatos()
        {
            agenda = new List<Contato>();
        }

        public bool adicionar(Contato c)
        {
            if (c == null || pesquisar(c) != null)
                return false;

            agenda.Add(c);
            return true;
        }

        public Contato? pesquisar(Contato c)
        {
            if (c == null)
                return null;

            return agenda.Find(contato => contato.Equals(c));
        }

        public bool alterar(Contato c)
        {
            if (c == null)
                return false;

            Contato? existente = pesquisar(c);

            if (existente == null)
                return false;

            int indice = agenda.IndexOf(existente);
            agenda[indice] = c;

            return true;
        }

        public bool remover(Contato c)
        {
            Contato? existente = pesquisar(c);

            if (existente == null)
                return false;

            return agenda.Remove(existente);
        }

        public void listar()
        {
            if (agenda.Count == 0)
            {
                System.Console.WriteLine(
                    "A agenda está vazia.");
                return;
            }

            foreach (Contato contato in agenda)
            {
                System.Console.WriteLine(
                    "------------------------------");
                System.Console.WriteLine(contato);
            }

            System.Console.WriteLine(
                $"Total de contatos: {agenda.Count}");
        }
    }
}
