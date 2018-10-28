using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveletTreeNS;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var array = new[] { "", "", "", "", "", null, "" };
                var obj = WaveletTree.Create(array);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                System.Diagnostics.Debugger.Break();
            }
        }
    }
}
