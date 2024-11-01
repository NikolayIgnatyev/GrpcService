﻿using EncoderApp;
using Grpc.Net.Client;

using var channel = GrpcChannel.ForAddress("https://localhost:7090");
// создаем клиент
var encoderClient = new Encoder.EncoderClient(channel);
while (true)
{
    Console.Write("Введите текст: ");
    var plainText = Console.ReadLine();
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
    var encryptText = await encoderClient.EncryptAsync(new TextForEncrypt { PlainText = plainText, Key = key });
    var info = await encoderClient.GetInfoAsync(new InfoRequest());
    Console.WriteLine($"Ответ сервера: {encryptText.Response}");
    Console.WriteLine($"Ответ сервера: {info.Content}");
}

