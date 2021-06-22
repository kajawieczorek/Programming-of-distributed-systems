using Gremlin.Net.Driver;
using Gremlin.Net.Driver.Remote;
using Gremlin.Net.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JanusGrph
{
    class Program
    {
        [Obsolete]
        static void Main(string[] args)
        {
            var graph = new Graph();
            var client = new GremlinClient(new GremlinServer("localhost", 8182));

            var g = graph.Traversal().WithRemote(new DriverRemoteConnection(client));

            g.AddV("Client").Property("id", "1").Property("name", "Kaja").Property("surname", "Wieczorek").Next();
            g.AddV("Client").Property("id", "2").Property("name", "Piotr").Property("surname", "Nowak").Next();
            g.AddV("Client").Property("id", "3").Property("name", "Anna").Property("surname", "Kowal").Next();
            g.AddV("Trip").Property("id", "1").Property("country", "Meksyk").Property("city", "Toluca").Property("price", 3000).Next();
            g.AddV("Trip").Property("id", "2").Property("country", "Polska").Property("city", "Gdynia").Property("price", 1500).Next();
            g.AddV("Trip").Property("id", "3").Property("country", "Polska").Property("city", "Kielce").Property("price", 1000).Next();

            var act = new Actions(g);
            act.Run();
            
            Console.ReadLine();
        }

    }
}
