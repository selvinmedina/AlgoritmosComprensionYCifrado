using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmosComprensionYCifrado.Algoritmos
{
    public class RC4
    {
        /// <summary>
        /// Dado unos datos y una clave de encriptación, aplica la criptografía RC4. RC4 es simétrico,
        /// lo que significa que este único método funcionará tanto para encriptar como para desencriptar.
        /// </summary>
        /// <remarks>
        /// https://en.wikipedia.org/wiki/RC4
        /// </remarks>
        /// <param name="data">
        /// Matriz de bytes que representa los datos a encriptar/desencriptar
        /// </param>
        /// <param name="key">
        /// Matriz de bytes que representa la clave a utilizar
        /// </param>
        /// <returns>
        /// Matriz de bytes que representa los datos encriptados/desencriptados.
        /// </returns>
        public static byte[] Apply(byte[] data, byte[] key)
        {
            // Fase de algoritmo de programación de clave:
            // Paso 1: primero, los elementos de S se establecen iguales a los valores de 0 a 255 en orden ascendente.
            int[] S = new int[256];
            for (int _ = 0; _ < 256; _++)
            {
                S[_] = _;
            }

            // Paso 2a: a continuación, se crea un vector temporal T.
            int[] T = new int[256];

            //  KSA Paso 2 2b: si el length de la key k es 256 bytes, entonces k es asignada a T.  
            if (key.Length == 256)
            {
                Buffer.BlockCopy(key, 0, T, 0, key.Length);
            }
            else
            {
                // De lo contrario, para una clave con una longitud determinada,
                // se copian los elementos de la clave en el vector T,
                // repitiéndolos tantas veces como sea necesario para llenar T.
                for (int _ = 0; _ < 256; _++)
                {
                    T[_] = key[_ % key.Length];
                }
            }

            // Paso 3: usamos T para producir la permutación inicial de S ...
            int i = 0;
            int j = 0;
            for (i = 0; i < 256; i++)
            {
                // incrementamos j por la suma de S[i] y T[i], manteniéndolo dentro del rango de 0 a 255 mediante la división mod (%).
                j = (j + S[i] + T[i]) % 256;

                // Intercambiamos los valores de S[i] y S[j]
                int temp = S[i];
                S[i] = S[j];
                S[j] = temp;
            }


            // Algoritmo de generación de pseudo-aleatorios (Generación de secuencia):
            // Una vez que el vector S está inicializado desde arriba en la fase de algoritmo de programación de clave, la clave de entrada ya no se usa.
            // En esta fase, para la longitud de los datos, hacemos lo siguiente...
            i = j = 0;
            byte[] result = new byte[data.Length];
            for (int iteration = 0; iteration < data.Length; iteration++)
            {
                // Paso 1: incrementamos continuamente i de 0 a 255, comenzándolo de nuevo en 0 una vez que superamos 255 (esto se hace con la división mod (%)).
                i = (i + 1) % 256;

                // Paso 2: busque el elemento i-ésimo de la matriz S y añádalo a j, manteniendo el
                // resultado dentro del rango de 0 a 255 usando la división mod (%).
                j = (j + S[i]) % 256;

                // Paso 3: intercambie los valores de S[i] y S[j].
                int temp = S[i];
                S[i] = S[j];
                S[j] = temp;

                // Paso 4: Use el resultado de la suma de S[i] y S[j], mod (%) por 256,
                // para obtener el índice de S que maneja el valor del flujo K.
                int K = S[(S[i] + S[j]) % 256];

                // Paso 5: Use la operación XOR (^) con el siguiente byte en los datos para
                // producir el siguiente byte del cifrado resultante (cuando se
                // cifra) o del texto plano (cuando se descifra).
                result[iteration] = Convert.ToByte(data[iteration] ^ K);
            }

            return result;
        }
    }
}
