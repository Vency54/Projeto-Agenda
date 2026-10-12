using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System;
using System.Collections.Generic;

namespace Projeto_Agenda
{
    public class Contato
    {
        private string email;
        private string nome;
        private Data dtNasc;
        private List<Telefone> telefones;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        public Data DtNasc
        {
            get { return dtNasc; }
            set { dtNasc = value; }
        }

        public List<Telefone> Telefones
        {
            get { return telefones; }
        }

        public Contato(string email, string nome, Data dtNasc)
        {
            this.email = email;
            this.nome = nome;
            this.dtNasc = dtNasc;
            this.telefones = new List<Telefone>();
        }

        public int getIdade()
        {
            DateTime nascimento = dtNasc.ParaDateTime();
            DateTime hoje = DateTime.Today;

            int idade = hoje.Year - nascimento.Year;

            if (nascimento.Date > hoje.AddYears(-idade))
            {
                idade--;
            }

            return idade;
        }

        public void adicionarTelefone(Telefone t)
        {
            if (t == null)
                throw new ArgumentNullException(nameof(t));

            if (t.principal)
            {
                foreach (Telefone telefone in telefones)
                {
                    telefone.principal = false;
                }
            }

            telefones.Add(t);
        }

        public string getTelefonePrincipal()
        {
            foreach (Telefone telefone in telefones)
            {
                if (telefone.principal)
                    return telefone.numero;
            }

            return "Não informado";
        }

        public override string ToString()
        {
            string resultado =
                $"Nome: {nome}\n" +
                $"E-mail: {email}\n" +
                $"Data de nascimento: {dtNasc}\n" +
                $"Idade: {getIdade()} anos\n" +
                $"Telefone principal: {getTelefonePrincipal()}\n" +
                "Telefones cadastrados:\n";

            if (telefones.Count == 0)
            {
                resultado += "Nenhum telefone cadastrado.\n";
            }
            else
            {
                foreach (Telefone telefone in telefones)
                {
                    resultado += $" - {telefone}\n";
                }
            }

            return resultado;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Contato outro)
                return false;

            return string.Equals(
                email.Trim(),
                outro.email.Trim(),
                StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return StringComparer.OrdinalIgnoreCase.GetHashCode(
                email.Trim());
        }
    }
}
