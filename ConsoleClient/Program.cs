using ConsoleClient.Interfaces;
using ConsoleClient.Services;
using EncoderApp;
using Grpc.Net.Client;

using var channel = GrpcChannel.ForAddress("https://localhost:7090");
// создаем клиент
ICipher client = new CipherGrpcClient(channel);
while (true)
{
    Console.Write("Введите текст: ");
    string? plainText = Console.ReadLine();
    Console.Write("Введите ключ: ");
    int key;
    try
    {
        key = Convert.ToInt32(Console.ReadLine());
    }
    catch (FormatException ex)
    {
        Console.WriteLine(ex.ToString());
        Console.WriteLine("Введите коректное число!!!");
        return;
    }

    // обмениваемся сообщениями с сервером
    string encryptText = client.Encrypt(plainText, key);
    Console.WriteLine($"Ответ сервера: " + encryptText);
}

