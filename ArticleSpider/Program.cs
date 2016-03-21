using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleSpider
{
    class Program
    {
        static void Main(string[] args)
        {
                GetWeb gb = new GetWeb();
                gb.BasicUrl = "http://www.163.com";
                gb.Start();
        }
    }
}
