using CompresionHuffman;
using RSA_Algoritmo;

Console.WriteLine("------------------------------------------------");
Console.WriteLine("Seleccione una opción:");
Console.WriteLine("1. Cifrar con RC4");
Console.WriteLine("2. Cifrar con RSA");
Console.WriteLine("3. Comprimir con Huffman");
Console.WriteLine("4. Cambiar frase");
Console.WriteLine("6. Salir");
Console.WriteLine("------------------------------------------------");

while (true)
{
    Console.WriteLine("Ingrese la opción");
    int opcion = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine(opcion);
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

        case 6:
            Console.WriteLine("Saliendo del programa...");
            return;
        default:
            Console.WriteLine("Opción inválida, intente de nuevo.");
            break;
    }
}
