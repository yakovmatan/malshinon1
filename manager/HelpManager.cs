using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malshinon1.dal;
using malshinon1.people;

namespace malshinon1.manager
{
    internal class HelpManeger
    {
        private Dal Dal;

        public HelpManeger(Dal dal)
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

        public void CreateNewPerson(string firstName, string lastName,string type)
        {
            string secretCode = SecretCode.CreateSecretCode(firstName, lastName);
            Person person = new Person(firstName, lastName,secretCode,type);
            this.Dal.InsertNewPerson(person);

        }
    }
}
