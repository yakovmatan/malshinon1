using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malshinon1.dal;
using malshinon1.manager;
using malshinon1.menu;

namespace malshinon1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            menu.ChooseLogIn();
        }
    }
}
