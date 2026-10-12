using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Projeto_Agenda
{
    public class Data
    {
        private int dia;
        private int mes;
        private int ano;

        public int Dia => dia;
        public int Mes => mes;
        public int Ano => ano;

        public Data(int dia, int mes, int ano)
        {
            setData(dia, mes, ano);
        }

        public void setData(int dia, int mes, int ano)
        {
            if (!DateTime.TryParseExact(
                $"{dia:D2}/{mes:D2}/{ano:D4}",
                "dd/MM/yyyy",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out _))
            {
                throw new ArgumentException("Data inválida.");
            }

            this.dia = dia;
            this.mes = mes;
            this.ano = ano;
        }

        public DateTime ParaDateTime()
        {
            return new DateTime(ano, mes, dia);
        }

        public override string ToString()
        {
            return $"{dia:D2}/{mes:D2}/{ano:D4}";
        }
    }
}
