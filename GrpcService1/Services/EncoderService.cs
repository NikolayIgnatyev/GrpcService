using Grpc.Core;
using Server.DataBase;
using EncoderApp;
using Server.Interfaces;


namespace GrpcServiceServer.Services
{
    public class EncoderService : Encoder.EncoderBase
    {
        private readonly ICipher _cipherService;

        public EncoderService(ICipher caesarCipher) 
        {
            _cipherService = caesarCipher;
        }

        public override Task<EncryptText> Encrypt(TextForEncrypt request, ServerCallContext context)
        {

            var encryptedText = _cipherService.Encrypt(request.PlainText, request.Key);

            using (ApplicationContext db = new ApplicationContext())
            {

                Log log = new Log {
                    Text = request.PlainText,
                    Key = request.Key,
                    Encrypt = encryptedText
                };

                db.Logs.Add(log);
                db.SaveChanges();
            }


            return Task.FromResult(new EncryptText
            {
                Response = "Зашифрованый текст " + encryptedText
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
