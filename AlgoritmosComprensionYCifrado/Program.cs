using CompresionHuffman;
using CompresionLempelZivWelch;
using RSA_Algoritmo;

Console.WriteLine("------------------------------------------------");
Console.WriteLine("Seleccione una opción:");
Console.WriteLine("1. Cifrar con RC4");
Console.WriteLine("2. Cifrar con RSA");
Console.WriteLine("3. Comprimir con Huffman");
Console.WriteLine("4. Comprimir con Lempel-Ziv-Welch (LZW)");
Console.WriteLine("5. Cambiar frase");
Console.WriteLine("6. Salir");
Console.WriteLine("------------------------------------------------");
// crea un enum para cada una de las opciones

while (true)
{
    Console.WriteLine("Ingrese la opción");
    int opcion = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("----------------------------------------------------------------------------------------");
    switch (opcion)
    {
        case 1:
            Console.WriteLine("RC4:");
            TestRC4.Run();
            break;

        case 2:
            Console.WriteLine("RSA:");
            TestRSA.Run();
            break;

        case 3:
            Console.WriteLine("Huffman:");
            Huffman.Run();
            break;
        case 4:
            Console.WriteLine("LZW:");
            LempelZivWelch.Run();
            break;
        case 5:
            Console.WriteLine("Ingrese la frase:");
            string? frase = Console.ReadLine();
            if (frase is "" or "test" or null )
            {
                Console.WriteLine("Frase inválida, intente de nuevo.");
                break;
            }

            File.WriteAllText("frase.txt", frase);
            Console.WriteLine("Frase cambiada.");
            break;
        case 6:
            Console.WriteLine("Saliendo del programa...");
            return;
        default:
            Console.WriteLine("Opción inválida, intente de nuevo.");
            break;
    }

    Console.WriteLine("----------------------------------------------------------------------------------------");
}
enum Opciones
{
    RC4 = 1,
    RSA = 2,
    Huffman = 3,
    LZW = 4,
    CambiarFrase = 5,
    Salir = 6
}