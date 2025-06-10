using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malshinon1.dal;

namespace malshinon1.manager
{
    internal class Maneger
    {
        private Dal Dal;

        public Maneger(Dal dal)
        {
            this.Dal = dal;
        }

        public (string firstName, string lastName) EnterName()
        {
            Console.WriteLine("Enter your first name");
            string name = Console.ReadLine();
            Console.WriteLine("Enter your last name");
            string lastName = Console.ReadLine();
            return (name,lastName);
        }

        public bool ExistsInTheSystem(string firstName, string lastName)
        {
            var person = this.Dal.GetPersonByName(firstName, lastName);
            if (person != null)
            {
                return true;
            }
            return false;
        }

    }
}
