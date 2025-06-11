using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malshinon1.dal;
using malshinon1.manager;

namespace malshinon1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dal = new Dal();
            var manager = new Manager(dal);
            manager.StartUsing();

        }
    }
}
