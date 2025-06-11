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
                            foreach (var agent in potentialAgent)
                            {
                                Console.WriteLine(agent.firstName);
                                Console.WriteLine(agent.lastName);
                                Console.WriteLine(agent.secretCode);
                                Console.WriteLine(agent.numReports);
                            }
                        }
                        else
                        {
                            Console.WriteLine(" No potential agant found.");
                        }

                            break;

                    case "2":
                        var dangerTarget = this.dal.GetAllDangerTarget();
                        if (dangerTarget.Count > 0)
                        {
                            foreach (var target in dangerTarget)
                            {
                                Console.WriteLine(target.firstName);
                                Console.WriteLine(target.lastName);
                                Console.WriteLine(target.alert);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No danger targets found.");
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
