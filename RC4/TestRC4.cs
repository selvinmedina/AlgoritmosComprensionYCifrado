using AlgoritmosComprensionYCifrado.Algoritmos;
using System.Text;

public static class TestRC4
{
    public static void Run()
    {
        //  Let's say we had the phrase "The one ring"

        string frase = File.ReadAllText("frase.txt");

        //  And we wanted to encrypt it, using the phrase "Keep it secret. Keep it safe."
        string keyFrase = "Keep it secret. Keep it safe.";

        //  First, let's get the byte data of the phrase
        byte[] data = Encoding.UTF8.GetBytes(frase);

        //  Next, let's get the byte data of the key phrase
        byte[] key = Encoding.UTF8.GetBytes(keyFrase);

        //  We can encrypt it like so
        byte[] dataEncriptada = RC4.Apply(data, key);

        //  Now, RC4 is a symetric algorithm, meaning, if we encrypt something
        //  with a given key, we can run the encrypted data through the same
        //  method with the same key to decrypt it.
        //  Let's do that
        byte[] dataDesencriptada = RC4.Apply(dataEncriptada, key);

        //  Decode the decrypted data
        string fraseDesencriptada = Encoding.UTF8.GetString(dataDesencriptada);


        //  Let's output the data created above to the console so we can see the results
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