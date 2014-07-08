using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartStopRestart
{
    class Program
    {
        static void Main(string[] args)
        {
            Controller c = new Controller();
            c.Start();
            while (true)
            {
                var k = Console.ReadKey();
                if (k.KeyChar == 'q')
                {
                    break;
                }
                c.Restart();
            }

        }
    }
}
