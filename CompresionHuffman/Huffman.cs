using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace CompresionHuffman
{

    public class Huffman
    {

        // Nodo del árbol de Huffman
        public class NodoHuffman
        {
            public char Caracter { get; set; }
            public int Frecuencia { get; set; }
            public NodoHuffman Izquierda { get; set; }
            public NodoHuffman Derecha { get; set; }

            public char Simbolo { get; set; } // propiedad Simbolo para almacenar el carácter único

            public bool EsHoja()
            {
                return Izquierda == null && Derecha == null;
            }

            public List<NodoHuffman> ObtenerHojas(NodoHuffman nodo)
            {
                List<NodoHuffman> hojas = new List<NodoHuffman>();

                if (nodo != null)
                {
                    if (nodo.EsHoja())
                    {
                        hojas.Add(nodo);
                    }
                    else
                    {
                        hojas.AddRange(ObtenerHojas(nodo.Izquierda));
                        hojas.AddRange(ObtenerHojas(nodo.Derecha));
                    }
                }

                return hojas;
            }
        }
        // Generar el árbol de Huffman a partir de un texto
        static NodoHuffman GenerarArbolHuffman(string texto)
        {
            // Calcular la frecuencia de cada símbolo
            var frecuenciaSimbolos = new Dictionary<char, int>();
            foreach (var simbolo in texto)
            {
                if (frecuenciaSimbolos.ContainsKey(simbolo))
                {
                    frecuenciaSimbolos[simbolo]++;
                }
                else
                {
                    frecuenciaSimbolos[simbolo] = 1;
                }
            }

            // Crear un nodo por cada símbolo con su frecuencia
            var nodosHuffman = frecuenciaSimbolos.Select(kv => new NodoHuffman { Simbolo = kv.Key, Frecuencia = kv.Value }).ToList();

            // Generar el árbol de Huffman
            while (nodosHuffman.Count > 1)
            {
                // Ordenar los nodos por frecuencia ascendente
                nodosHuffman = nodosHuffman.OrderBy(n => n.Frecuencia).ToList();

                // Tomar los dos nodos con menor frecuencia y unirlos en un nuevo nodo
                var nodoIzquierda = nodosHuffman[0];
                var nodoDerecha = nodosHuffman[1];
                var nodoNuevo = new NodoHuffman { Simbolo = '\0', Frecuencia = nodoIzquierda.Frecuencia + nodoDerecha.Frecuencia, Izquierda = nodoIzquierda, Derecha = nodoDerecha };

                // Eliminar los nodos antiguos y agregar el nuevo
                nodosHuffman.RemoveAt(0);
                nodosHuffman.RemoveAt(0);
                nodosHuffman.Add(nodoNuevo);
            }

            // Devolver el nodo raíz del árbol
            return nodosHuffman.FirstOrDefault()!;
        }

        // Generar la tabla de codificación a partir del árbol de Huffman
        static Dictionary<char, string> GenerarCodificacion(NodoHuffman nodo)
        {
            var codificacion = new Dictionary<char, string>();
            GenerarCodificacionRecursiva(nodo, "", codificacion);
            return codificacion;
        }

        static void GenerarCodificacionRecursiva(NodoHuffman nodo, string codigo, Dictionary<char, string> codificacion)
        {
            if (nodo == null)
            {
                return;
            }

            if (nodo.Simbolo != '\0')
            {
                codificacion[nodo.Simbolo] = codigo;
                return;
            }
            GenerarCodificacionRecursiva(nodo.Izquierda, codigo + "0", codificacion);
            GenerarCodificacionRecursiva(nodo.Derecha, codigo + "1", codificacion);
        }

        // Codificar un texto utilizando la tabla de codificación
        static string CodificarTexto(string texto, Dictionary<char, string> codificacion)
        {
            return new string(texto.SelectMany(c => codificacion[c]).ToArray());
        }

        // Decodificar un texto utilizando el árbol de Huffman
        static string DecodificarTexto(string texto, NodoHuffman raiz)
        {
            var resultado = new StringBuilder();
            var nodoActual = raiz;

            foreach (var bit in texto)
            {
                if (bit == '0')
                {
                    nodoActual = nodoActual.Izquierda;
                }
                else if (bit == '1')
                {
                    nodoActual = nodoActual.Derecha;
                }
                else
                {
                    throw new ArgumentException("El texto contiene caracteres que no son 0 ni 1.");
                }

                if (nodoActual.Izquierda == null && nodoActual.Derecha == null)
                {
                    resultado.Append(nodoActual.Simbolo);
                    nodoActual = raiz;
                }
            }

            return resultado.ToString();
        }

        public static string ObtenerRepresentacionBinaria(string texto)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in texto)
            {
                // Convertir cada carácter a su representación en binario
                string binario = Convert.ToString(c, 2);

                // Agregar ceros a la izquierda para que el binario tenga 8 bits
                while (binario.Length < 8)
                {
                    binario = "0" + binario;
                }

                // Agregar la representación binaria del carácter al resultado
                sb.Append(binario);
            }

            return sb.ToString();
        }

        public static int ContarBits(string texto)
        {
            int bits = 0;
            foreach (char c in texto)
            {
                bits += 8; // cada caracter ASCII se representa en 8 bits (1 byte)
            }
            return bits;
        }

        public static void ImprimirTablaHuffman(NodoHuffman raiz, string codigo)
        {
            if (raiz == null)
            {
                return;
            }

            // Si el nodo es una hoja, imprime su caracter y su código Huffman
            if (raiz.EsHoja())
            {
                Console.WriteLine(raiz.Caracter + " - " + codigo);
            }

            // Recorre los nodos del árbol en orden primero por la izquierda, luego por la derecha
            ImprimirTablaHuffman(raiz.Izquierda, codigo + "0");
            ImprimirTablaHuffman(raiz.Derecha, codigo + "1");
        }

        public static void ImprimirArbol(NodoHuffman nodo, string prefijo = "")
        {
            if (nodo != null)
            {
                Console.WriteLine($"{prefijo}├── {(nodo.EsHoja() ? $"{nodo.Simbolo} ({nodo.Frecuencia})" : nodo.Frecuencia.ToString())}");

                if (nodo.Izquierda != null || nodo.Derecha != null)
                {
                    ImprimirArbol(nodo.Izquierda!, prefijo + "│  ");
                    ImprimirArbol(nodo.Derecha, prefijo + "   ");
                }
            }
        }

        public static void Run()
        {
            // Texto a codificar
            var texto = File.ReadAllText("frase.txt");

            // Generar árbol de Huffman y tabla de codificación
            var raiz = GenerarArbolHuffman(texto);
            var codificacion = GenerarCodificacion(raiz);

            // Codificar texto
            var textoCodificado = CodificarTexto(texto, codificacion);

            // Imprimir resultados
            Console.WriteLine("Texto codificado: " + textoCodificado);

            // Decodificar texto
            var textoDecodificado = DecodificarTexto(textoCodificado, raiz);

            // Imprimir resultados
            Console.WriteLine("Texto decodificado: " + textoDecodificado);

            Console.WriteLine("Arbol:");
            ImprimirArbol(raiz);

        }
    }
}
