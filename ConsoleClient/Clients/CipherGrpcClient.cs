using ConsoleClient.Interfaces;
using EncoderApp;
using Grpc.Net.Client;

namespace ConsoleClient.Services
{
    public class CipherGrpcClient : ICipher
    {
        private readonly Encoder.EncoderClient _client;

        public CipherGrpcClient(GrpcChannel channel)
        {
            _client = new Encoder.EncoderClient(channel);
        }

        public string Encrypt(string text, int key)
        {
            return _client.Encrypt(new TextForEncrypt { PlainText = text, Key = key}).Response;
        }
    }
}
