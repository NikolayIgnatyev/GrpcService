using EncoderApp;
using Grpc.Core;

namespace Server.Interfaces
{
    public interface ICipher
    {
        public string Encrypt(string text, int key);
    }
}
