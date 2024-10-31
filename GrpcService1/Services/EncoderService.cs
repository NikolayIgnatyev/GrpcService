using Grpc.Core;
using Server.DataBase;
using EncoderApp;
using Server.Interfaces;
using Server.DataBase.Models;
using Azure;


namespace GrpcServiceServer.Services
{
    public class EncoderService : Encoder.EncoderBase
    {
        private readonly ICipher _cipherService;
        private readonly ICacher _cacher;

        public EncoderService(ICipher caesarCipher, ICacher cacher) 
        {
            _cipherService = caesarCipher;
            _cacher = cacher;
        }

        public override Task<EncryptText> Encrypt(TextForEncrypt request, ServerCallContext context)
        {
            //проверка в кеше
            var cachedEntry = _cacher.Read(request.PlainText, request.Key);

            if (cachedEntry != null)
            {
                return Task.FromResult(new EncryptText { Response = "Зашифрованый текст " + cachedEntry.Encrypt });
            }
            
            // в кеше не нашлось реализуем шифровку
            var encryptedText = _cipherService.Encrypt(request.PlainText, request.Key);

            _cacher.Write(request.PlainText, encryptedText, request.Key);


            return Task.FromResult(new EncryptText { Response = "Зашифрованый текст " + encryptedText });
        }


        public override Task<InfoResponse> GetInfo(InfoRequest request, ServerCallContext context)
        {
            return Task.FromResult(new InfoResponse { Content = _cacher.GetInfo(_cipherService) });

        }
    }
}
