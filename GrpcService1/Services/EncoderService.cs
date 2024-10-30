using Grpc.Core;
using GrpcServiceServer;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using static System.Net.Mime.MediaTypeNames;

namespace GrpcServiceServer.Services
{
    public class EncoderService : Encoder.EncoderBase
    {
        private readonly ILogger<EncoderService> _logger;
        public EncoderService(ILogger<EncoderService> logger)
        {
            _logger = logger;
        }

        public override Task<EncryptText> Encrypt(TextForEncrypt request, ServerCallContext context)
        {
            //символы русской азбуки
            const string alfabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
            //добавляем в алфавит маленькие буквы
            var fullAlfabet = alfabet + alfabet.ToLower();
            var letterQty = fullAlfabet.Length;
            var retVal = "";
            for (int i = 0; i < request.PlainText.Length; i++)
            {
                var c = request.PlainText[i];
                var index = fullAlfabet.IndexOf(c);
                if (index < 0)
                {
                    //если символ не найден, то добавляем его в неизменном виде
                    retVal += c.ToString();
                }
                else
                {
                    var codeIndex = (letterQty + index + request.Key) % letterQty;
                    retVal += fullAlfabet[codeIndex];
                }
            }



            return Task.FromResult(new EncryptText
            {
                Response = "Зашифрованый текст " + retVal
            });
        }


        public override Task<InfoResponse> GetInfo(InfoRequest request, ServerCallContext context)
        {
            string headers = "";
            // получаем все заголовки запроса
            foreach (var header in context.RequestHeaders)
            {
               headers += header + "\n";    // получаем ключ и значение заголовка
            }
            // получаем один заголовок по названию - user-agent
            
            // отправляем ответ
            return Task.FromResult(new InfoResponse
            {
                Content = headers
            });
        }
    }
}
