﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookByText
{
    public interface IBookService
    {
        IEnumerable<KeyValuePair<int, string>> Search(string query);
        void CreateSubscription(string mobileNumber, int bookId);
        string GetNext(string mobileNumber);
        string GetPrevious(string mobileNumber);
        IEnumerable<KeyValuePair<int, string>> GetTitles();
    }
}
