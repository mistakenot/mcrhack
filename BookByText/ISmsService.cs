using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookByText
{
    public interface ISmsService
    {
        void Send(string number, string body);
    }
}
