using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BookByText
{
    public class BookService : IBookService
    {
        public const int PAGE_SIZE = 150;
        public const int NUMBER_OF_PAGES = 3;
        public static readonly string RootDir = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();

        private readonly ICollection<BookSubscription> subscriptions;
        private IReadOnlyDictionary<int, string> index;

        public BookService()
        {
            var path = RootDir + "/index.json";
            var file = File.ReadAllText(path);

            this.subscriptions = new List<BookSubscription>();
            this.index = JsonConvert.DeserializeObject<Dictionary<int, string>>(file);
        }

        public IEnumerable<KeyValuePair<int, string>> Search(string query)
        {
            var keywords = query
                .Split(' ')
                .Select(q => q.ToLower().Trim());

            var result = keywords
                .SelectMany(k => index.Where(i => i.Value.ToLower().Contains(k)))
                .Distinct();

            return result;
        }
        public void CreateSubscription(string mobileNumber, int bookId)
        {
            // Remove any pre-existing subscriptions
            foreach (var existing in subscriptions.Where(s => s.Number == mobileNumber).ToList())
            {
                subscriptions.Remove(existing);
            }

            // Create new subscription
            if (!FileExists(bookId))
            {
                throw new ArgumentException("ID not found.");
            }

            subscriptions.Add(new BookSubscription()
            {
                Number = mobileNumber,
                BookId = bookId,
                Index = 0
            });
        }
        public string GetNext(string mobileNumber)
        {
            var sub = subscriptions.SingleOrDefault(s => s.Number == mobileNumber);

            if(sub == null)
            {
                throw new ArgumentException("Subscription not found for mobile number: " + mobileNumber);
            }

            var buffer = new char[PAGE_SIZE];

            using (var stream = GetStream(sub.BookId))
            using (var reader = new StreamReader(stream))
            {
                stream.Seek(sub.Index, SeekOrigin.Begin);
                reader.ReadBlock(buffer, 0, PAGE_SIZE);
            }

            sub.Index += PAGE_SIZE;

            return new string(buffer);
        }
        public string GetPrevious(string mobileNumber)
        {
            var sub = subscriptions.SingleOrDefault(s => s.Number == mobileNumber);

            if (sub == null)
            {
                throw new ArgumentException("Subscription not found for mobile number: " + mobileNumber);
            }

            if(sub.Index < (PAGE_SIZE*2))
            {
                throw new IndexOutOfRangeException("Am at the beginning of the book. Can't go backwards.");
            }

            var buffer = new char[PAGE_SIZE];

            using (var stream = GetStream(sub.BookId))
            using (var reader = new StreamReader(stream))
            {
                stream.Seek(sub.Index - (PAGE_SIZE*2), SeekOrigin.Begin);
                reader.ReadBlock(buffer, 0, PAGE_SIZE);
            }

            sub.Index -= PAGE_SIZE;

            return new string(buffer);
        }
        public IEnumerable<KeyValuePair<int, string>> GetTitles()
        {
            return index.ToList();
        }

        private FileStream GetStream(int bookId)
        {
            return File.OpenRead(IdToPath(bookId));
        }
        private bool FileExists(int bookId)
        {
            return File.Exists(IdToPath(bookId));
        }
        private string IdToPath(int id)
        {
            string val;
            if (!index.TryGetValue(id, out val))
            {
                throw new ArgumentException("Id doesn't exist: " + id);
            }

            var path = Path.Combine(
                RootDir,
                val
            );

            return path;
        }
    }

    class BookSubscription
    {
        public string Number { get; set; }
        public int BookId { get; set; }
        public int Index { get; set; }
    }
}