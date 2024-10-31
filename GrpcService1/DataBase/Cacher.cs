using Server.DataBase.Models;
using Server.Interfaces;

namespace Server.DataBase
{
    public class Cacher : ICacher
    {
        private readonly ApplicationContext _context;

        public Cacher(ApplicationContext context)
        {
            _context = context;
        }

        public void Write(string text, string encryptText, int key)
        {
            var cacheEntry = new Cache
            {
                Text = text,
                Encrypt = encryptText,
                Key = key,
            };

            _context.Caches.Add(cacheEntry);
            _context.SaveChanges();
        }

        public Cache Read(string text,  int key)
        {
            return _context.Caches.FirstOrDefault(c => c.Text == text && c.Key == key);
        }

        public string GetInfo()
        {
            return $"{_context.Caches.Count()} Записей в кеше.";
        }
    }
}
