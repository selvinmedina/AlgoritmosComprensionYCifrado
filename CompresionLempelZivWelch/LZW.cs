using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompresionLempelZivWelch
{
    public static class LZW
    {
        private const int TAMAÑO_MAXIMO_DICCIONARIO = 256;

        public static List<int> Comprimir(string input)
        {
            var dictionary = new Dictionary<string, int>();
            for (int i = 0; i < TAMAÑO_MAXIMO_DICCIONARIO; i++)
            {
                dictionary.Add(((char)i).ToString(), i);
            }

            string current = string.Empty;
            var output = new List<int>();
            foreach (char c in input)
            {
                string next = current + c;
                if (dictionary.ContainsKey(next))
                {
                    current = next;
                }
                else
                {
                    output.Add(dictionary[current]);
                    dictionary.Add(next, dictionary.Count);
                    current = c.ToString();
                }
            }

            if (!string.IsNullOrEmpty(current))
            {
                output.Add(dictionary[current]);
            }

            return output;
        }

        public static string Descomprimir(List<int> comprimido)
        {
            var dictionary = new Dictionary<int, string>();
            for (int i = 0; i < TAMAÑO_MAXIMO_DICCIONARIO; i++)
            {
                dictionary.Add(i, ((char)i).ToString());
            }

            string current = dictionary[comprimido[0]];
            var output = new StringBuilder(current);
            for (int i = 1; i < comprimido.Count; i++)
            {
                string entry = null;
                if (dictionary.ContainsKey(comprimido[i]))
                {
                    entry = dictionary[comprimido[i]];
                }
                else if (comprimido[i] == dictionary.Count)
                {
                    entry = current + current[0];
                }

                if (entry != null)
                {
                    output.Append(entry);
                    dictionary.Add(dictionary.Count, current + entry[0]);
                    current = entry;
                }
            }

            return output.ToString();
        }
    }
}
