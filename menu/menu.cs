using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malshinon1.dal;
using malshinon1.manager;

namespace malshinon1.menu
{
    internal class Menu
    {
        private Dal dal = new Dal();
        private Manager manager;

        public Menu()
        {
            manager = new Manager(dal);
        }

        public void ShowMenuToUser()
        {
            Console.WriteLine("\n===== Menu =====");
            Console.WriteLine("Enter your choose");
            Console.WriteLine("1.Log in as an administrator");
            Console.WriteLine("2.log in as an reporter");
            Console.WriteLine("=====================");
        }
        //public void
        public void ShowMenuToManager()
        {
            Console.WriteLine("\n===== Menu =====");
            Console.WriteLine("Enter your choose");
            Console.WriteLine("1.Show potential agents");
            Console.WriteLine("2.Show dangerous targets");
            Console.WriteLine("0.To exit");
            Console.WriteLine("=====================");
        }

        public void ToReporter()
        {
            Console.WriteLine("\n===== Menu =====");
            Console.WriteLine("Enter report");
            Console.WriteLine("=====================");
            manager.StartUsing();
        }

        public void ChooseLogIn()
        {
            while (true)
            {
                ShowMenuToUser();
                string choose = Console.ReadLine();
                switch (choose)
                {
                    case "1":
                        ChooseInformation();
                        return;

                    case "2":
                        ToReporter();
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        public void ChooseInformation()
        {
            bool running = true;

            while (running)
            {
                ShowMenuToManager();
                string choose = Console.ReadLine();
                switch (choose)
                {
                    case "0":
                        running = false;
                        break;

                    case "1":
                        var potentialAgent = this.dal.GetAllPotentialAgent();
                        if (potentialAgent.Count > 0)
                        {
                            Console.WriteLine("\n=========== POTENTIAL AGENTS ===========\n");
                            foreach (var agent in potentialAgent)
                            {
                                Console.WriteLine($"👤 Name        : {agent.firstName} {agent.lastName}");
                                Console.WriteLine($"🆔 Secret Code : {agent.secretCode}");
                                Console.WriteLine($"📄 Reports     : {agent.numReports}");
                                Console.WriteLine("----------------------------------------\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\n⚠️ No potential agant found. \n");
                        }

                            break;

                    case "2":
                        var dangerTarget = this.dal.GetAllDangerTarget();
                        if (dangerTarget.Count > 0)
                        {
                            Console.WriteLine("\n=========== DANGER TARGETS =============\n");
                            foreach (var target in dangerTarget)
                            {
                                Console.WriteLine($"🎯 Name  : {target.firstName} {target.lastName}");
                                Console.WriteLine($"🚨 Alert : {target.alert}");
                                Console.WriteLine("----------------------------------------\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\n⚠️ No danger targets found.");
                        }
                            break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }




    }
}
