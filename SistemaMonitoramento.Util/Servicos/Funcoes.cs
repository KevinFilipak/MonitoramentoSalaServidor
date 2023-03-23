using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaMonitoramento.Utill.Servicos
{
    public static class Funcoes
    {
        public static string MesDescricao(int Mes)
        {
            var Descricao = "";

            switch (Mes)
            {
                case 1:
                    Descricao = "Janeiro";
                    break;
                case 2:
                    Descricao = "Fevereiro";
                    break;
                case 3:
                    Descricao = "Março";
                    break;
                case 4:
                    Descricao = "Abril";
                    break;
                case 5:
                    Descricao = "Maio";
                    break;
                case 6:
                    Descricao = "Junho";
                    break;
                case 7:
                    Descricao = "Julho";
                    break;
                case 8:
                    Descricao = "Agosto";
                    break;
                case 9:
                    Descricao = "Setembro";
                    break;
                case 10:
                    Descricao = "Outubro";
                    break;
                case 11:
                    Descricao = "Novembro";
                    break;
                case 12:
                    Descricao = "Dezembro";
                    break;

            }

            return Descricao;
        }

        public static string MesDescricao(string Mes)
        {
            var Descricao = "";

            switch (Mes)
            {
                case "Janeiro":
                    Descricao = "01";
                    break;
                case "Fevereiro":
                    Descricao = "02";
                    break;
                case "Março":
                    Descricao = "03";
                    break;
                case "Abril":
                    Descricao = "04";
                    break;
                case "Maio":
                    Descricao = "05";
                    break;
                case "Junho":
                    Descricao = "06";
                    break;
                case "Julho":
                    Descricao = "07";
                    break;
                case "Agosto":
                    Descricao = "08";
                    break;
                case "Setembro":
                    Descricao = "09";
                    break;
                case "Outubro":
                    Descricao = "10";
                    break;
                case "Novembro":
                    Descricao = "11";
                    break;
                case "Dezembro":
                    Descricao = "12";
                    break;

            }

            return Descricao;
        }

        public static string RegularizarCaracteres(string str)
        {
            /** Troca os caracteres acentuados por não acentuados **/
            string[] acentos = new string[] { "ç", "Ç", "á", "é", "í", "ó", "ú", "ý", "Á", "É", "Í", "Ó", "Ú", "Ý", "à", "è", "ì", "ò", "ù", "À", "È", "Ì", "Ò", "Ù", "ã", "õ", "ñ", "ä", "ë", "ï", "ö", "ü", "ÿ", "Ä", "Ë", "Ï", "Ö", "Ü", "Ã", "Õ", "Ñ", "â", "ê", "î", "ô", "û", "Â", "Ê", "Î", "Ô", "Û" };
            string[] semAcento = new string[] { "c", "C", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "Y", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U", "a", "o", "n", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "A", "O", "N", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U" };

            for (int i = 0; i < acentos.Length; i++)
            {
                str = str.Replace(acentos[i], semAcento[i]);
            }
            /** Troca os caracteres especiais da string por "" **/
            string[] caracteresEspeciais = { "\\.", ",", "-", ":", "\\(", "\\)", "ª", "\\|", "\\\\", "°" };

            for (int i = 0; i < caracteresEspeciais.Length; i++)
            {
                str = str.Replace(caracteresEspeciais[i], "");
            }

            /** Troca os espaços no início por "" **/
            str = str.Replace("^\\s+", "");
            /** Troca os espaços no início por "" **/
            str = str.Replace("\\s+$", "");
            /** Troca os espaços duplicados, tabulações e etc por " " **/
            str = str.Replace("\\s+", " ");
            return str;
        }
    }
}
