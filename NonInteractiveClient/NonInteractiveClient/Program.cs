using System;

namespace NonInteractiveClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Getting token!");

            var authenticator = new Authenticator();

            Console.WriteLine(authenticator.GetToken());

            var client = new HttpClientWrapper("https://localhost:44338/");

            Console.WriteLine(client.Get("api/values"));

            Console.ReadKey();
        }
    }
}
