using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Banco_DIO
{
    public class Program
    {
        static List<Conta> _listaContas = new List<Conta>();

        static void Main(string[] args)
        {
            string opcao1 = MenuPrincipal();

            while (opcao1.ToUpper() != "X")
            {
                switch (opcao1)
                {
                    case "1":
                        {
                            CadastrarNovaConta();
                            break;
                        }

                    case "2":
                        {
                            AlterarDadosConta();
                            break;
                        }

                    case "3":
                        {
                            RemoverConta();
                            break;
                        }

                    case "4":
                        {

                            var sairMenuConta = false;
                            while (!sairMenuConta)
                            {
                                var opcao2 = MenuAcessoConta();

                                switch (opcao2)
                                {
                                    case "1":
                                        {
                                            RealizarSaque();
                                            break;
                                        }
                                    case "2":
                                        {
                                            RealizarDeposito();
                                            break;
                                        }
                                    case "3":
                                        {
                                            RealizarTransferencia();
                                            break;
                                        }
                                    case "4":
                                        {
                                            sairMenuConta = true;
                                            break;
                                        }

                                }

                            }


                            break;
                        }
                    case "5":
                        {
                            ListarContas();
                            break;
                        }
                }

                opcao1 = MenuPrincipal();
            }
        }
        private static void RealizarTransferencia()
        {
            Console.WriteLine("Realizar Transferencia");
            Console.WriteLine("Informe Guid da conta de Origem: ");
            var contaOrigem = Console.ReadLine();
            Console.WriteLine("Informe o Guid da conta de Destino: ");
            var contaDestino = Console.ReadLine();
            Console.WriteLine("Informe o valor da transferencia");
            double valorTransferencia = double.Parse(Console.ReadLine());
            var contaTransferente = _listaContas.FirstOrDefault(conta => conta.Id == Guid.Parse(contaOrigem));
            var contaDestinoObj = _listaContas.FirstOrDefault(conta => conta.Id == Guid.Parse(contaDestino));

            if (contaTransferente.RealizarTransferencia(valorTransferencia, contaDestinoObj))
            {
                Console.WriteLine("Transferencia realizada com sucesso.");
            }
            else
            {
                Console.WriteLine("Não foi possivel realizar transferencia");
            }
        }

        private static void RealizarDeposito()
        {
            Console.WriteLine("Realizar Depósito: ");
            Console.WriteLine("Informe a Guid da conta: ");
            var id = Console.ReadLine();
            Console.WriteLine("Informe o valor do depósito: ");
            double valorDeposito = double.Parse(Console.ReadLine());

            var ContaAdepositar = _listaContas.FirstOrDefault(conta => conta.Id == Guid.Parse(id));
            ContaAdepositar.RealizarDeposito(valorDeposito);
            Console.WriteLine("Deposito realizado com sucesso");
        }

        private static void RealizarSaque()
        {
            Console.WriteLine("Realizar Saque: ");
            Console.WriteLine("Informe a guid da conta: ");
            var id = Console.ReadLine();
            Console.WriteLine("Informe o valor a ser Sacado: ");
            double valorSaque = double.Parse(Console.ReadLine());

            var ContaAsacar = _listaContas.FirstOrDefault(conta => conta.Id == Guid.Parse(id));
            if (ContaAsacar.RealizarSaque(valorSaque))
            {
                Console.WriteLine("Saque realizado com sucesso.");
            }
            else
            {
                Console.WriteLine("Falha ao realizar saque.");
            }
        }

        private static string MenuPrincipal()
        {
            Console.WriteLine();
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Banco DIO");
            Console.WriteLine("Informe a opção desejada: ");
            Console.WriteLine("-------------------------------------");

            Console.WriteLine("1 - Cadastrar Conta");
            Console.WriteLine("2 - Alterar dados de uma conta");
            Console.WriteLine("3 - Remover uma Conta");
            Console.WriteLine("4 - Acessar uma Conta");
            Console.WriteLine("5 - Listar contas cadastradas");
            Console.WriteLine("X - Sair");
            string opcao1 = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcao1;
        }

        private static string MenuAcessoConta()        //Menu de opçoes para alguma conta cadastrada (saque, deposito e transferencia)
        {
            Console.WriteLine();
            Console.WriteLine("Opções de Conta ");
            Console.WriteLine();
            Console.WriteLine("1 - Realizar Saque");
            Console.WriteLine("2 - Realizar Depósito");
            Console.WriteLine("3 - Realizar Transferência");
            Console.WriteLine("4 - Voltar a tela anterior");
            string opcao2 = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcao2;
        }

        private static void CadastrarNovaConta()             //Cadastra novas contas 
        {
            Console.WriteLine("Inserir nova Conta");
            Console.WriteLine();
            Console.Write("Digite 1 para Pessoa Fisica e 2 para Pessoa Juridica ");

            int entradaTipoConta = 0;
            if (!int.TryParse(Console.ReadLine(), out entradaTipoConta) || entradaTipoConta > 2)         //Trata entradas com valores incorretos
            {
                Console.WriteLine("Tipo conta deve ser 1 (Pessoa Fisica) ou 2 (Pessoa Juridica).");
                return;
            }

            Console.Write("Digite o nome do Titular: ");
            string entradaNome = Console.ReadLine();

            Console.WriteLine("Digite o valor de depósito inicial para abertura de conta: ");
            double entradaDeposito = 0;
            if (!double.TryParse(Console.ReadLine(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out entradaDeposito))         //Trata entradas com valores incorretos
            {
                Console.WriteLine("Não foi possivel converter o valor para o tipo númerico");
                return;
            }

            Console.WriteLine("Digite o valor de crédito dispónivel para a conta: ");

            double entradaCredito = 0;
            if (!double.TryParse(Console.ReadLine(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out entradaCredito))         //Trata entradas com valores incorretos
            {
                Console.WriteLine("Não foi possivel converter o valor para o tipo númerico");
            }

            Conta novoCadastro = new Conta((TipoConta)entradaTipoConta, entradaNome, entradaDeposito, entradaCredito);
            _listaContas.Add(novoCadastro);
        }

        private static void ListarContas()                        //Lista todas as Contas cadastradas
        {
            Console.WriteLine("\n Contas cadastradas:\n");

            if (_listaContas.Count == 0)
            {
                Console.WriteLine("Não existem nenhuma conta cadastrada.\n");
            }
            else
            {
                foreach (var conta in _listaContas)
                {
                    Console.WriteLine($"Id: {conta.Id} \n");
                    Console.WriteLine(conta);
                }
            }
        }
        private static void AlterarDadosConta()                     //Altera dados do Cadastro (nome e credito) Falta Excessão 
        {
            Console.WriteLine(" Informe o guid da conta a ser alterado:");
            Guid id = default(Guid);

            if (!Guid.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Guid Inexistente ou Incorreto");
                return;
            }
            Console.WriteLine(" Informe um novo nome:");
            string novoNome = Console.ReadLine();


            Console.WriteLine("Informe um novo crédito para a conta: ");
            double novoCredito = 0;

            if (!double.TryParse(Console.ReadLine(), out novoCredito))
            {
                Console.WriteLine("Não foi possivel converter o valor do novo crédito.");
                return;
            }

            var contaASerAlterada = _listaContas.FirstOrDefault(conta => conta.Id == id);
            contaASerAlterada.AlterarNome(novoNome);
            contaASerAlterada.AlterarCredito(novoCredito);

        }
        private static void RemoverConta()    //Remove a Conta com base na GUID do Titular da Conta                                       
        {
            Console.WriteLine("\n Informe o guid da conta a ser removido:\n");
            var id = Console.ReadLine();

            var contaASerRemovida = _listaContas.FirstOrDefault(conta => conta.Id == Guid.Parse(id));
            _listaContas.Remove(contaASerRemovida);
        }
    }


}



