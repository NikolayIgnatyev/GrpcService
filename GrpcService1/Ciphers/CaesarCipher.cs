using Azure.Core;
using Server.Interfaces;

namespace Server.Encoding
{
    public class CaesarCipher : ICipher
    {
        public string Encrypt(string text, int key)
        {
            //символы русской азбуки
            const string alfabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
            //добавляем в алфавит маленькие буквы
            var fullAlfabet = alfabet + alfabet.ToLower();
            var letterQty = fullAlfabet.Length;
            var retVal = "";
            for (int i = 0; i < text.Length; i++)
            {
                var c = text[i];
                var index = fullAlfabet.IndexOf(c);
                if (index < 0)
                {
                    //если символ не найден, то добавляем его в неизменном виде
                    retVal += c.ToString();
                }
                else
                {
                    var codeIndex = (letterQty + index + key) % letterQty;
                    retVal += fullAlfabet[codeIndex];
                }
            }



            return retVal;
        }
    }
}
