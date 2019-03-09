using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using SleepyYuzutan.WaveletTree;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var array = "kjwdlks;k:omewaivpowvmjaiefjcea";
                var obj = WaveletTree.Create(array);
                Console.WriteLine(obj.Select('k', 1));
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
                Debugger.Break();
            }
        }
    }
}
