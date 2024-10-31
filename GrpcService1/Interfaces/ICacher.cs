using Server.DataBase.Models;

namespace Server.Interfaces
{
    public interface ICacher
    {
        public void Write(string text, string encryptText, int key);
        public Cache Read(string text, int key);
        public string GetInfo();
    }
}
