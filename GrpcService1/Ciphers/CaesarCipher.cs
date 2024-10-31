using Azure.Core;
using Server.Interfaces;

namespace Server.Encoding
{
    public class CaesarCipher : ICipher
    {
        public string Encrypt(string text, int key)
        {
            char[] buffer = text.ToCharArray();
            for (int i = 0; i < buffer.Length; i++)
            {
                char letter = buffer[i];
                letter = (char)(letter + key);
                if (letter > 'z') // Считаем только маленькие буквы
                {
                    letter = (char)(letter - 26);
                }
                else if (letter < 'a')
                {
                    letter = (char)(letter + 26);
                }
                buffer[i] = letter;
            }
            return new string(buffer);
        }
    }
}
