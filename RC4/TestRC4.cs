using AlgoritmosComprensionYCifrado.Algoritmos;
using System.Text;

public static class TestRC4
{
    public static void Run()
    {
        string frase = File.ReadAllText("frase.txt");

        string keyFrase = "Manten esto secreto. Manten esto seguro.";

        byte[] data = Encoding.UTF8.GetBytes(frase);

        byte[] key = Encoding.UTF8.GetBytes(keyFrase);

        byte[] dataEncriptada = RC4.Apply(data, key);

        byte[] dataDesencriptada = RC4.Apply(dataEncriptada, key);

        string fraseDesencriptada = Encoding.UTF8.GetString(dataDesencriptada);

        Console.WriteLine("Frase:\t\t\t{0}", frase);
        Console.WriteLine("Frase en Bytes:\t\t{0}", BitConverter.ToString(data));
        Console.WriteLine("Frase Key:\t\t{0}", keyFrase);
        Console.WriteLine("Key en Bytes:\t\t{0}", BitConverter.ToString(key));
        Console.WriteLine("Resultado de encriptación RC4:\t{0}", BitConverter.ToString(dataEncriptada));
        Console.WriteLine("Resultado de desencriptación RC4:\t{0}", BitConverter.ToString(dataDesencriptada));
        Console.WriteLine("Frase desencriptada Phrase:\t{0}", fraseDesencriptada);

        Console.WriteLine(Environment.NewLine + "Presione Enter para continuar");
        Console.ReadLine();
    }
}