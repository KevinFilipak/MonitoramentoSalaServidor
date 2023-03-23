using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaMonitoramento.Util.Servicos
{
    public static class Extensions
    {
        public static object GetPropertyValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> input, string queryString)
        {
            if (string.IsNullOrEmpty(queryString))
                return input;

            int i = 0;
            foreach (string propname in queryString.Split(','))
            {
                var subContent = propname.Split('|');
                if (subContent[1].Trim() == "asc")
                {
                    if (i == 0)
                        input = input.OrderBy(x => GetPropertyValue(x, subContent[0].Trim()));
                    else
                        input = ((IOrderedEnumerable<T>)input).ThenBy(x => GetPropertyValue(x, subContent[0].Trim()));
                }
                else
                {
                    if (i == 0)
                        input = input.OrderByDescending(x => GetPropertyValue(x, subContent[0].Trim()));
                    else
                        input = ((IOrderedEnumerable<T>)input).ThenByDescending(x => GetPropertyValue(x, subContent[0].Trim()));
                }
                i++;
            }

            return input;
        }

        public static string SomenteNumeros(this string value)
        {

            string Digitos = "0123456789";
            string temp = "";
            string digito = "";

            for (var i = 0; i < value.Length; i++)
            {
                digito = value[i].ToString();

                if (Digitos.IndexOf(digito) >= 0)
                {
                    temp = temp + digito;
                }
            }

            return temp;
        }

        public static string SomenteLetrasNumerosPonto(this string value)
        {
            
            string Digitos = "0123456789abcdefghijqlmnopqrstuvwxyzABCDEFGHIJQLMNOPQRSTUVWXYZ.";
            string temp = "";
            string digito = "";

            for (var i = 0; i < value.Length; i++)
            {
                digito = value[i].ToString();

                if (Digitos.IndexOf(digito) >= 0) 
                {
                    temp = temp + digito;
                }
            }

            return temp;            
        }

        public static string RemoverAcentos(this string value)
        {

            value = value.Replace("ç", "c");
            value = value.Replace("Ç", "C");
            value = value.Replace("ã", "a");
            value = value.Replace("Ã", "A");
            value = value.Replace("á", "a");
            value = value.Replace("Á", "A");
            value = value.Replace("é", "e");
            value = value.Replace("É", "E");
            value = value.Replace("ó", "o");
            value = value.Replace("Ó", "O");
            value = value.Replace("â", "a");
            value = value.Replace("Â", "A");
            value = value.Replace("í", "i");
            value = value.Replace("Í", "I");
            value = value.Replace("õ", "o");
            value = value.Replace("Õ", "O");


            return value;
        }

        public static double DesvioPadrao(this IEnumerable<double> sequence)
        {
            double average = sequence.Average();
            double sum = sequence.Sum(d => Math.Pow(d - average, 2));
            return Math.Sqrt((sum) / sequence.Count());
        }

        public static HashSet<T> ToHashSet<T>(
        this IEnumerable<T> source,
        IEqualityComparer<T> comparer = null)
        {
            return new HashSet<T>(source, comparer);
        }
    }
}
