using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NonInteractiveClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Getting token!");

            var authenticator = new Authenticator();

            Console.WriteLine(authenticator.GetToken());

            var client = new HttpClientWrapper("https://localhost:44338/api/values");

            Console.WriteLine(client.Get("https://localhost:44338/api/values"));

            Console.ReadKey();
        }
    }
}
