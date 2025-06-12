using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malshinon1.alerts;
using malshinon1.dal;
using malshinon1.logger;
using malshinon1.people;
using malshinon1.reports;

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
            Logger.Info($"Created new person: {firstName} {lastName}, Type: {type}, SecretCode: {secretCode}");
        }

        public string EnterReport()
        {
            Console.WriteLine("Enter the report but make sure that target is first");
            string text = Console.ReadLine();
            return text;
        }

        public (string firstName,string lastName) ExtractName(string text)
        {
            string[] words = text.Split(' ');
            if (words.Length >= 2)
            {
                return (words[0], words[1]);
            }
            else
            {
                return ("Unknown", "Unknown");
            }
        }

        public void CreateNewReport(string firstNameTarget,string lastNameTarget,string firstNameReporter,string lastNameReporter,string text)
        {
            var personTarget = this.Dal.GetPersonByName(firstNameTarget, lastNameTarget);
            int idTarget = personTarget.id;
            var personReporter = this.Dal.GetPersonByName(firstNameReporter, lastNameReporter);
            int idReporter = personReporter.id;
            Report report = new Report(idReporter, idTarget, text);
            this.Dal.InsertIntelReport(report);
            Logger.Info($"New report created successfully by {firstNameReporter} {lastNameReporter} on {firstNameTarget} {lastNameTarget}");
            Console.WriteLine("The report was added successfully.");
        }

        public bool DangerTarget(string secretCode)
        {
            int numOfReportsIn15Min = this.Dal.GetTargetStats(secretCode);
            if (numOfReportsIn15Min >= 3)
            {
                return true;
            }
            return false;
        }

        public void createAlert(int targetId)
        {
            Alert alert = new Alert(targetId, "poses a potential threat");
            this.Dal.InsertAlert(alert);
            Logger.Info($"new alart created");
            Console.WriteLine("A new alert has been created.");
        }

        public bool PotentialAgent(int reporterId)
        {
            (int count, double avgLength) = this.Dal.GetReporterStats(reporterId);
            if (count >= 10 || avgLength >= 100)
            {
                return true;
            }
            return false;
        }
    }
}
