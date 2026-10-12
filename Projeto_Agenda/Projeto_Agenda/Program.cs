
using System;
using Projeto_Agenda;

namespace Projeto_Agenda
{
    internal class Program
    {
        private static readonly Contatos agenda = new Contatos();

        static void Main()
        {
            Console.OutputEncoding =
                System.Text.Encoding.UTF8;

            int opcao;

            do
            {
                ExibirMenu();
                opcao = LerInteiro("Escolha uma opção: ");

                switch (opcao)
                {
                    case 0:
                        Console.WriteLine("Encerrando a Agenda...");
                        break;

                    case 1:
                        AdicionarContato();
                        break;

                    case 2:
                        PesquisarContato();
                        break;

                    case 3:
                        AlterarContato();
                        break;

                    case 4:
                        RemoverContato();
                        break;

                    case 5:
                        agenda.listar();
                        break;

                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }

                if (opcao != 0)
                {
                    Console.WriteLine(
                        "\nPressione ENTER para continuar...");
                    Console.ReadLine();
                }

            } while (opcao != 0);
        }

        static void ExibirMenu()
        {
            Console.Clear();
            Console.WriteLine("========== AGENDA ==========");
            Console.WriteLine("0 - Sair");
            Console.WriteLine("1 - Adicionar contato");
            Console.WriteLine("2 - Pesquisar contato");
            Console.WriteLine("3 - Alterar contato");
            Console.WriteLine("4 - Remover contato");
            Console.WriteLine("5 - Listar contatos");
            Console.WriteLine("============================");
        }

        static string LerTexto(string mensagem)
        {
            while (true)
            {
                Console.Write(mensagem);
                string texto = Console.ReadLine()?.Trim() ?? "";

                if (!string.IsNullOrWhiteSpace(texto))
                    return texto;

                Console.WriteLine(
                    "O campo não pode ficar vazio.");
            }
        }

        static int LerInteiro(string mensagem)
        {
            while (true)
            {
                Console.Write(mensagem);

                if (int.TryParse(
                    Console.ReadLine(), out int valor))
                    return valor;

                Console.WriteLine(
                    "Digite um número inteiro válido.");
            }
        }

        static Data LerData()
        {
            while (true)
            {
                Console.Write("Dia de nascimento: ");
                int dia = LerInteiro("");

                Console.Write("Mês de nascimento: ");
                int mes = LerInteiro("");

                Console.Write("Ano de nascimento: ");
                int ano = LerInteiro("");

                try
                {
                    Data data = new Data(dia, mes, ano);

                    if (data.ParaDateTime() > DateTime.Today)
                    {
                        Console.WriteLine(
                            "A data não pode estar no futuro.");
                        continue;
                    }

                    return data;
                }
                catch (ArgumentException)
                {
                    Console.WriteLine(
                        "Data inválida. Tente novamente.\n");
                }
            }
        }

        static Contato? BuscarPorEmail(string email)
        {
            Contato busca = new Contato(
                email, "", new Data(1, 1, 2000));

            return agenda.pesquisar(busca);
        }

        static void CadastrarTelefones(Contato contato)
        {
            bool continuar = true;

            while (continuar)
            {
                Console.WriteLine("\nCadastro de telefone");

                string tipo = LerTexto(
                    "Tipo (Celular, Residencial, Trabalho...): ");

                string numero = LerTexto("Número: ");

                bool principal;

                while (true)
                {
                    Console.Write(
                        "É o telefone principal? (S/N): ");

                    string resposta =
                        (Console.ReadLine() ?? "").Trim();

                    if (resposta.Equals(
                        "S", StringComparison.OrdinalIgnoreCase))
                    {
                        principal = true;
                        break;
                    }

                    if (resposta.Equals(
                        "N", StringComparison.OrdinalIgnoreCase))
                    {
                        principal = false;
                        break;
                    }

                    Console.WriteLine("Digite S ou N.");
                }

                contato.adicionarTelefone(
                    new Telefone(tipo, numero, principal));

                while (true)
                {
                    Console.Write(
                        "Deseja cadastrar outro telefone? (S/N): ");

                    string resposta =
                        (Console.ReadLine() ?? "").Trim();

                    if (resposta.Equals(
                        "S", StringComparison.OrdinalIgnoreCase))
                    {
                        continuar = true;
                        break;
                    }

                    if (resposta.Equals(
                        "N", StringComparison.OrdinalIgnoreCase))
                    {
                        continuar = false;
                        break;
                    }

                    Console.WriteLine("Digite S ou N.");
                }
            }
        }

        static Contato CriarContato(string email)
        {
            string nome = LerTexto("Nome: ");
            Data nascimento = LerData();

            Contato contato = new Contato(
                email, nome, nascimento);

            CadastrarTelefones(contato);

            return contato;
        }

        static void AdicionarContato()
        {
            Console.WriteLine("\n=== ADICIONAR CONTATO ===");

            string email = LerTexto("E-mail: ");

            if (BuscarPorEmail(email) != null)
            {
                Console.WriteLine(
                    "Já existe um contato com esse e-mail.");
                return;
            }

            Contato contato = CriarContato(email);

            if (agenda.adicionar(contato))
                Console.WriteLine(
                    "\nContato cadastrado com sucesso!");
            else
                Console.WriteLine(
                    "\nNão foi possível cadastrar o contato.");
        }

        static void PesquisarContato()
        {
            Console.WriteLine("\n=== PESQUISAR CONTATO ===");

            string email = LerTexto("E-mail do contato: ");
            Contato? contato = BuscarPorEmail(email);

            if (contato == null)
            {
                Console.WriteLine("Contato não encontrado.");
                return;
            }

            Console.WriteLine("\n" + contato);
        }

        static void AlterarContato()
        {
            Console.WriteLine("\n=== ALTERAR CONTATO ===");

            string email = LerTexto(
                "E-mail do contato que deseja alterar: ");

            Contato? existente = BuscarPorEmail(email);

            if (existente == null)
            {
                Console.WriteLine("Contato não encontrado.");
                return;
            }

            Console.WriteLine("\nDados atuais:");
            Console.WriteLine(existente);

            Console.WriteLine(
                "Informe os novos dados do contato.");

          
            Contato atualizado = CriarContato(existente.Email);

            if (agenda.alterar(atualizado))
                Console.WriteLine(
                    "\nContato alterado com sucesso!");
            else
                Console.WriteLine(
                    "\nNão foi possível alterar o contato.");
        }

        static void RemoverContato()
        {
            Console.WriteLine("\n=== REMOVER CONTATO ===");

            string email = LerTexto(
                "E-mail do contato que deseja remover: ");

            Contato? contato = BuscarPorEmail(email);

            if (contato == null)
            {
                Console.WriteLine("Contato não encontrado.");
                return;
            }

            Console.WriteLine("\nContato encontrado:");
            Console.WriteLine(contato);

            Console.Write("Confirma a remoção? (S/N): ");
            string resposta =
                (Console.ReadLine() ?? "").Trim();

            if (resposta.Equals(
                "S", StringComparison.OrdinalIgnoreCase))
            {
                if (agenda.remover(contato))
                    Console.WriteLine(
                        "Contato removido com sucesso!");
                else
                    Console.WriteLine(
                        "Não foi possível remover o contato.");
            }
            else
            {
                Console.WriteLine("Remoção cancelada.");
            }
        }
    }
}
