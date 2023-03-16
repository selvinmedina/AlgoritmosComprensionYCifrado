using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompresionLempelZivWelch
{
    public static class LempelZivWelch
    {
        // consume la clase LZW creando un metodo main
        public static void Run()
        {
            // se crea un string con el texto a comprimir
            string texto = File.ReadAllText("frase.txt");
            // se llama al metodo comprimir y se le pasa el string
            var comprimido = LZW.Comprimir(texto);
            // se imprime el texto comprimido
            Console.Write("Texto comprimido: ");
            foreach (var item in comprimido)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();

            // se llama al metodo descomprimir y se le pasa el texto comprimido
            var descomprimido = LZW.Descomprimir(comprimido);
            // se imprime el texto descomprimido
            Console.WriteLine("Texto descomprimido: " + descomprimido);
            // se espera a que el usuario presione una tecla para cerrar la consola
            Console.ReadKey();
        }
    }
}
