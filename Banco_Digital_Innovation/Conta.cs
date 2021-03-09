using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Banco_DIO
{
    public class Conta
    {
        public Guid Id { get; private set; }
        public TipoConta TipoConta { get; private set; }
        public string Nome { get; private set; }
        public double Saldo { get; private set; }
        public double Credito { get; private set; }

        public Conta(TipoConta tipoConta, string nome, double saldo, double credito)
        {
            this.TipoConta = tipoConta;
            this.Nome = nome;
            this.Saldo = saldo;
            this.Credito = credito;

            this.Id = Guid.NewGuid();
        }

        public bool RealizarSaque(double valorSaque)
        {
            if (this.Saldo - valorSaque < (this.Credito * -1))        //Faz a validação de Saldo Disponivel
            {
                return false;
            }
            this.Saldo -= valorSaque;
            return true;
        }

        public void RealizarDeposito(double valorDeposito)
        {
            this.Saldo += valorDeposito;
        }


        public bool RealizarTransferencia(double valorTransferencia, Conta contaDestino)
        {
            if (this.RealizarSaque(valorTransferencia))
            {
                contaDestino.RealizarDeposito(valorTransferencia);
                return true;
            }
            else
            {

                return false;
            }
        }

        public bool AlterarNome(string nome)
        {
            if (String.IsNullOrWhiteSpace(nome))
                return false;

            this.Nome = nome;

            return true;
        }

        public bool AlterarCredito(double credito)
        {
            if (double.IsNegative(credito))
                return false;
            this.Credito = credito;
            return true;
        }
        public override string ToString()
        {
            return $"Tipo de Conta: {TipoConta} |  Nome do Titular: {Nome}  |  Saldo: {Saldo.ToString("F2", CultureInfo.InvariantCulture)}  |  " +
                $"Crédito: {Credito.ToString("F2", CultureInfo.InvariantCulture)} \n" +
                "---------------------------------------------------------------------------------------------------------";



        }
    }
}


