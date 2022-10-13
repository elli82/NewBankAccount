using System;

namespace NewBankAccount
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string userName = "";
            string pinCode = "";
            int attemptstoLogin = 0;
            bool isRunning = true;
            decimal sum = 0;
            int fromAccount = 0;
            int toAccount = 0;
            int index = 0;

            string[,] users = new string[5, 2];
            users[0, 0] = "Elin";
            users[0, 1] = "111";
            users[1, 0] = "Marcus";
            users[1, 1] = "222";
            users[2, 0] = "Hanna";
            users[2, 1] = "333";
            users[3, 0] = "Jesper";
            users[3, 1] = "444";
            users[4, 0] = "Kenneth";
            users[4, 1] = "555";

            decimal[,] accounts = new decimal[5, 2] {
                                                { 2500.25m , 33500.95m},
                                                { 8600.95m, 48950.65m}, 
                                                { 900.65m, 1500.95m },
                                                { 100.50m , 11300m },
                                                { 6325.50m, 345000.50m} };
            {
                Console.WriteLine("Välkommen till banken!");
            }
            do
            {
                try
                {
                    Console.WriteLine("Skriv in ditt namn:");
                    userName = Console.ReadLine();
                    Console.WriteLine("Skriv in din pinkod:");
                    pinCode = Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                for (int i = 0; i < users.GetLength(0); i++)
                {
                    if (users[i, 0] == userName && users[i, 1] == pinCode)
                    {
                        Console.WriteLine("Välkommen {0}:", userName);                        
                        attemptstoLogin = 4;
                    }
                }
                if (attemptstoLogin < 3)
                {
                    Console.WriteLine("Tyvärr, du lyckades inte logga in, försök igen!");
                    attemptstoLogin++;
                }
                else if (attemptstoLogin == 3)
                {
                    Console.WriteLine("Tyvärr, du lyckades inte logga in på tre försök.");
                }
            } while (attemptstoLogin < 3);

            //do{     
            Console.WriteLine("Ange ditt val:");
            Console.WriteLine("[1] Se dina konton och saldo.");
            Console.WriteLine("[2] Överföring mellan konton");
            Console.WriteLine("[3] Ta ut pengar");
            Console.WriteLine("[4] Logga ut");
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    CheckBalance(index, userName, users, accounts);
                    Console.WriteLine("Klicka Enter för att komma till huvudmenyn");
                    break;
                case 2:
                    Transfers(sum, fromAccount, toAccount, userName, users, accounts);
                    Console.WriteLine("Klicka Enter för att komma till huvudmenyn");
                    break;
                case 3:
                   WithDrawal(users, accounts);


                    Console.WriteLine("Klicka Enter för att komma till huvudmenyn");
                    break;
                case 4:
                    Console.WriteLine("Du loggas nu ut.");
                    break;
                default:
                    Console.WriteLine("Ogiltigt val.");
                    break;

            }
            //}while(isRunning)

        }  //main
        public static int CheckIndex(string userName, string[,] users)
        {
            int index = 5;
            for (int i = 0; i < users.GetLength(0); i++)
            {
                if (userName == users[i, 0])
                {
                    index = i;
                }
            }
            return index;
        }
       private static void CheckBalance(int index, string userName, string[,] users, decimal[,] accounts)
        {
            CheckIndex(userName, users);
            
                for (int i = 0; i < accounts.GetLength(0); i++) 
                {                 
                    if (accounts[index,i]!=0)   //foreach (var account in accounts)
                {
                        Console.WriteLine($"Du har: {accounts[index, i]} kr på ditt konto");
                    }
                }
        }
        private static void Transfers(decimal sum, int fromAccount, int toAccount, string userName, string[,] users, decimal[,] accounts)
        {
            Console.WriteLine("Ange summan du vill överföra:");
            sum = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Från vilket konto vill du ta pengarna?: Tryck: 0:Lönekonto 1:Sparkonto");
            fromAccount = int.Parse(Console.ReadLine());
            Console.WriteLine("Till vilket konto?:Tryck: 0:Lönekonto 1:Sparkonto");
            toAccount = int.Parse(Console.ReadLine());
            decimal difference = 0;
            decimal product = 0;
            int k;
            k = toAccount;
            int index = CheckIndex(userName, users);

            for (int i = 0; i < accounts.GetLength(0); i++)
            {
                if (accounts[index, i] != 0)
                {
                  if (i == fromAccount)
                    {
                        difference = accounts[index, i] - sum;
                        Console.WriteLine("Du har nu {0} kr på ditt konto.", difference);  
                    }    
                    if (k == toAccount)
                    {
                        product = accounts[index, k] + sum;
                        Console.WriteLine("Du har nu {0} kr på ditt konto:",product);
                    }                        
                }                
            }
        }
        private static bool CheckPin(string pinCode, string[,] users)
        {
            for (int i = 0; i < users.GetLength(0); i++)
            {
                if (pinCode == users[i, 1])
                {
                    return true;
                }
            }
        }
        private static void WithDrawal(string[,] users, decimal[,]accounts)
        {
            

        }

    } //class

} //namespace 

