using System;

namespace NewBankAccount  //SUT22 Elin Linderholm
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string userName = "";   //användare
            string pinCode = "";    //pinkod
            int attemptstoLogin = 0;  //för att räkna antal inloggsförsök
            bool isRunning1 = true;   
            bool isRunning2= true;
            bool isLoggedin = false;      // bool för att kontrollera inloggning                                         

            string[,] users = new string[5, 2];  //array med användare och deras pinkoder
            users[0, 0] = "ELIN";
            users[0, 1] = "111";
            users[1, 0] = "MARCUS";
            users[1, 1] = "222";
            users[2, 0] = "HANNA";
            users[2, 1] = "333";
            users[3, 0] = "JESPER";
            users[3, 1] = "444";
            users[4, 0] = "KENNETH";
            users[4, 1] = "555";

            decimal[,] accounts = new decimal[5, 2] {           //array med användarnas konton
                                                { 2500.25m , 33500.95m},
                                                { 8600.95m, 48950.65m}, 
                                                { 900.65m, 1500.95m },
                                                { 100.50m , 11300m },
                                                { 6325.50m, 345000.50m} };
            string[] whatAccount = new string[2];  //array med info om vilket konto som används
            whatAccount[0] = "Lönekonto";
            whatAccount[1] = "Sparkonto";
            
            while(isRunning2)                                
                {
                    Console.WriteLine("Välkommen till banken!");                
                do
                { 
                    try
                    {
                        Console.WriteLine("Skriv in ditt namn:");  //inloggning
                        userName = Console.ReadLine().ToUpper();
                        Console.WriteLine("Skriv in din pinkod:");
                        pinCode = Console.ReadLine(); 
                    }
                    catch (Exception ex)   //fånga upp feltryckning
                    {
                        Console.WriteLine(ex.Message);
                    }
                    for (int i = 0; i < users.GetLength(0); i++)  //loopa igenom alla användare
                    {
                        if (users[i, 0] == userName && users[i, 1] == pinCode) //om både användarnamn och pinkod stämmer
                        {
                            Console.WriteLine("Välkommen {0}:", userName);
                            isLoggedin = true;                                  //blir boolen true och man är inloggad
                            attemptstoLogin = 4;    //avslutar loopen
                        }
                    }
                    attemptstoLogin++;//räknar antal försök att logga in
                    if (attemptstoLogin < 3)
                    {
                        Console.WriteLine("Tyvärr, du lyckades inte logga in, försök igen!");                          
                    }                   
                    if (attemptstoLogin == 3)  //vid tre försök stängs..........
                    {
                        Console.WriteLine("Tyvärr, du lyckades inte logga in på tre försök.");
                        isRunning2 = false;    //avslutar hela programmet
                    }
                    
                } while (attemptstoLogin <3); //loop pågår sålänge antal försök är mindre än 3

                if (isLoggedin)  //om man lyckats logga in
                {
                    do
                    {
                        Console.WriteLine("Ange ditt val:");   //menyn
                        Console.WriteLine("[1] Se dina konton och saldo.");
                        Console.WriteLine("[2] Överföring mellan konton");
                        Console.WriteLine("[3] Ta ut pengar");
                        Console.WriteLine("[4] Logga ut");
                        int choice = int.Parse(Console.ReadLine());

                        switch (choice)
                        {
                            case 1:
                                CheckBalance(userName, whatAccount, users, accounts);   //anrop av kolla saldo-metod
                                Console.WriteLine("Klicka Enter för att komma till huvudmenyn");
                                break;
                            case 2:
                                Transfers(userName, whatAccount, users, accounts);  //anrop av föra över pengar-metod 
                                Console.WriteLine("Klicka Enter för att komma till huvudmenyn");
                                break;
                            case 3:
                                WithDrawal(userName, whatAccount, users, accounts); //anrop av ta ut pengar-metod
                                Console.WriteLine("Klicka Enter för att komma till huvudmenyn");
                                break;
                            case 4:
                                Console.WriteLine("Du loggas nu ut. Klicka Enter för att avsluta.");  //utloggning
                                isRunning1 = false;
                                break;
                            default:
                                Console.WriteLine("Ogiltigt val. Klicka Enter för att återgå till menyn.");  //om användaren knappar in bokstäver
                                break;
                        }
                        Console.ReadLine();
                    } while (isRunning1);   //meny-loop
                }                
            }  //loop för hela programmet
        }  
        public static int CheckIndex(string userName, string[,] users)  //metod för att se vilket konto som ska användas
        {
            int index = 10;              
            for (int i = 0; i < users.GetLength(0); i++) //loopar igenom användare för att hitta rätt plats i arrayen
            {
                if (userName == users[i, 0])
                {
                    index = i;
                }
            }
            return index;
        }
       private static void CheckBalance(string userName, string[] whatAccount, string[,] users, decimal[,] accounts)
        {                                       //metod för att kolla saldo på sitt konto
            int index=CheckIndex(userName, users);  //anrop av metod för att hitta rätt användares konto
            
                for (int i = 0; i < accounts.GetLength(1); i++) //loopar igenom kontona
                {                 
                    if (accounts[index,i]!=0)   //hittar rätt persons konton
                    {
                        Console.WriteLine($"Du har: {accounts[index, i]} kr på ditt {whatAccount[i]}"); //skriver ut saldon på kontona                
                    }
                }
        }
        private static void Transfers(string userName, string [] whatAccount, string[,] users, decimal[,] accounts)
        {                                                               //metod för att föra över pengar
            decimal sum = 0;
            int fromAccount = 0;
            int toAccount = 0;
            decimal newValue = 0;  //nya summan på kontot pengar tas från
            decimal newValue2 = 0;  //nya summan på kontot pengar flyttas till
            int k;  //variabel att använda för att räkna med rätt konto
            k = toAccount;
            int index = CheckIndex(userName, users);  //anrop av metod för att använda rätt persons konto

            try
            {
                Console.WriteLine("Ange summan du vill överföra:");
                sum = decimal.Parse(Console.ReadLine());  //summan som ska flyttas
                Console.WriteLine("Från vilket konto vill du ta pengarna?: Tryck: 0:Lönekonto 1:Sparkonto");
                fromAccount = int.Parse(Console.ReadLine());  //från detta konto ska pengar tas
                Console.WriteLine("Till vilket konto?:Tryck: 0:Lönekonto 1:Sparkonto");
                toAccount = int.Parse(Console.ReadLine());  //till detta konto ska pengar flyttas
            }
            catch (Exception ex)   //fånga upp feltryckning
            {
                Console.WriteLine(ex.Message);
            }
            
            for (int i = 0; i < accounts.GetLength(1); i++)  //loopar igenom konton
            {
                if (accounts[index, i] != 0)
                {
                  if (i == fromAccount)  
                    {
                        newValue = accounts[index, i] - sum;   //summan på konto - summan som ska överföras
                        Console.WriteLine("Du har nu {0} kr på ditt {1}.", newValue, whatAccount[i]);  
                    }    
                    if (i == toAccount)
                    {
                        newValue2 = accounts[index, k] + sum;    //summan på konto + summan som ska öveföras
                        Console.WriteLine("Du har nu {0} kr på ditt {1}.",newValue2, whatAccount[i]);
                    }                        
                }                
            }
        } 
        private static void WithDrawal(string userName, string[] whatAccount, string[,] users, decimal[,] accounts)  //metod för att ta ut kontanter
        {
            int cash = 0;
            decimal sum2 = 0;
            int index = CheckIndex(userName, users);
            decimal cashOnAccount = 0;  // nya saldot på kontot
            int counts = 0;    //variabel för att räkna antal pinkodsförsök
            bool isLoggedin = false;

            try
            {
                Console.WriteLine("Vilket konto vill du göra ett uttag från? Tryck: 0:Lönekonto 1:Sparkonto");
                cash = int.Parse(Console.ReadLine());  // valt konto
                Console.WriteLine("Vilken summa vill du ta ut?");
                sum2 = decimal.Parse(Console.ReadLine());  //kontantsumma
            }
            catch (Exception ex)   //fånga upp feltryckning
            {
                Console.WriteLine(ex.Message);
            }            
            do
            {
                Console.WriteLine("Pinkod måste anges:");
                string tryPin = Console.ReadLine();

                for (int i = 0; i < users.GetLength(0); i++)
                {
                    if (userName==users[i,0] && tryPin == users[i, 1])  //om rätt användare angett rätt pin
                    {
                        Console.WriteLine("Rätt pinkod. Varsågod, ta dina pengar:");
                        isLoggedin = true;
                        counts = 4;
                    }
                }
                counts++;  //räknare
                if (counts < 3)
                {
                    Console.WriteLine("Fel pinkod, försök igen.");
                }                
                if (counts == 3)
                {
                    Console.WriteLine("Tyvärr, din pinkod stämmer inte. Var vänlig börja om. ");
                }
            } while (counts <3);  //loopar sålänge inte användare gjort tre försök

            if (isLoggedin)
            {
                for (int i = 0; i < accounts.GetLength(1); i++)
                {
                    if (accounts[index, i] != 0)
                    {
                        if (i == cash)
                        {
                            cashOnAccount = accounts[index, i] - sum2;    //uträkning av saldo kvar på kontot
                            Console.WriteLine("Du har nu {0} kr kvar på ditt {1}.", cashOnAccount, whatAccount[i]);
                        }
                    }
                }
            }
        }
    } 
} 

