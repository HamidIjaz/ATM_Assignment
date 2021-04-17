using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ATM_OB;
using ATM_BLL;


namespace ATM_View
{
    public class View
    {
        Atm_bll bll = new Atm_bll();

        public Customer getInput()                          //getting input to get customer object
        {
            Console.WriteLine("Enter user name: ");
            string cname = Console.ReadLine();
            Console.WriteLine("Enter 5 digit pin Code: ");
            int cpin = System.Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Holder's Name: ");
            string custoName = Console.ReadLine();
            Console.WriteLine("Enter type of account: ");  
            string ctype = Console.ReadLine();
            Console.WriteLine("Enter starting balance: ");
            double cbalance = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Status of account: ");
            string cststus = Console.ReadLine();
            Customer CusOb = new Customer
            {
                userName = cname,
                pinCode = cpin,
                holderName = custoName,
                type = ctype,
                Balance = cbalance,
                status = cststus,

            };
            return CusOb;
        }
        public void Menu()
        {
            // Hard Coded Administrator
            //*******************
            string adminName = "admin";    
            int adminPin = 12345;
            //*******************
            string close_program = "yes";                                   //to close and not close the program
            Console.WriteLine("*************************");
            Console.WriteLine("\tATM SYSTEM");
            Console.WriteLine("*************************");
            Console.WriteLine("To login as admin, enter 1: ");
            Console.WriteLine("To login as customer, enter 2: ");
            Console.WriteLine("Enter your choice: ");
            int choice = System.Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter your user name: ");
                    String name = Console.ReadLine();
                    Console.WriteLine("Enter your 5 digit PIN: ");
                    int admPin = System.Convert.ToInt32(Console.ReadLine());

                    if (adminName == name && adminPin == admPin)
                    {
                        do
                        {
                            Console.WriteLine("********************");
                            Console.WriteLine("\tAdministrator menu");
                            Console.WriteLine("********************");
                            Console.WriteLine("1----Create New Account");
                            Console.WriteLine("2----Delete Existing Account.");
                            Console.WriteLine("3----Update Account Information.");
                            Console.WriteLine("4----Search for Account. ");
                            Console.WriteLine("5----View Reports");
                            Console.WriteLine("6----EXIT");
                            Console.WriteLine("********************");
                            Console.WriteLine("Enter your choice: ");


                            int administratrCh = System.Convert.ToInt32(Console.ReadLine());
                            switch (administratrCh)
                            {
                                case 1:
                                    Customer customerObject = getInput();
                                    bll.CreateCustomer(customerObject);
                                    break;

                                case 2:
                                    Console.WriteLine("Enter account number: ");
                                    int accNo = System.Convert.ToInt32(Console.ReadLine());
                                    double accountNumber = bll.deleteCustomer(accNo);
                                    if (accountNumber == 0)
                                    {
                                        Console.WriteLine("There's no any account in our system - Account Number : " + accNo);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Account has been deleted - With account number: " + accountNumber);
                                    }
                                    break;

                                case 3:

                                    Console.WriteLine("Enter account number to update account: ");
                                    double accNum = System.Convert.ToInt32(Console.ReadLine());
                                    Customer custmerob = getInput();
                                    bool check = bll.updateCustomer(accNum, custmerob);
                                    if (check == true)
                                    {
                                        Console.WriteLine(" Account has been updated");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Account do not found");
                                    }

                                    break;
                                case 4:
                                    Console.WriteLine("Enter the fields one by one to search accounts");
                                    Customer tempCust = getInput();
                                    List<Customer> lists = bll.searchCustomer(tempCust);
                                    foreach (Customer ob in lists)
                                    {
                                        Console.WriteLine($"Name : {ob.userName}");
                                        Console.WriteLine($"Account pin Code : {ob.pinCode}");
                                        Console.WriteLine($"Account Holder's Name : {ob.holderName}");
                                        Console.WriteLine($"Account Type : {ob.type}");
                                        Console.WriteLine($"Account Balance: {ob.Balance}");
                                        Console.WriteLine($"Account Status: {ob.status}");

                                    }
                                    break;

                                case 5:
                                    Console.WriteLine("1---Accounts By Amount");
                                    Console.WriteLine("2---Accounts By Date");
                                    int ch=System.Convert.ToInt32(Console.ReadLine());
                                    switch(ch)
                                    {
                                        case 1:
                                            Console.WriteLine("Enter the minimum amount");
                                            int minAmount = System.Convert.ToInt32(Console.ReadLine());
                                            Console.WriteLine("Enter the maximum amount");
                                            int maxAmount = System.Convert.ToInt32(Console.ReadLine());

                                            List<Customer> listOfReports = bll.viewReport(minAmount, maxAmount);
                                            foreach (Customer ob in listOfReports)
                                            {
                                                Console.WriteLine($"Name : {ob.userName}");
                                                Console.WriteLine($"Account pin Code : {ob.pinCode}");
                                                Console.WriteLine($"Account Holder's Name : {ob.holderName}");
                                                Console.WriteLine($"Account Type : {ob.type}");
                                                Console.WriteLine($"Account Balance: {ob.Balance}");
                                                Console.WriteLine($"Account Status: {ob.status}\n");
                                            }
                                            break;
                                    }
                                    break;

                                case 6:
                                    System.Environment.Exit(0);
                                    break;

                                default:
                                    Console.WriteLine("Default");
                                    break;
                            }

                            Console.WriteLine("Enter yes to continue or any key to exit : ");
                            close_program = Console.ReadLine();
                            while (string.IsNullOrEmpty(close_program))
                            {
                                Console.WriteLine("Input can't be empty! Enter again.");
                                close_program = Console.ReadLine();
                            }
                            Console.Clear();
                        } while (close_program == "yes");
                    }
                    else
                    {
                        Console.WriteLine("Invalid credentils.");
                    }
                    break;     //end of admin part case


                case 2:         //if user is customer
                    Console.WriteLine("Enter your user name: ");
                    String custName = Console.ReadLine();
                    Console.WriteLine("Enter your 5 digit PIN: ");
                    int custPin = System.Convert.ToInt32(Console.ReadLine());
                    Customer customer = bll.getCustomerData(custName, custPin);
                    if (customer != null)
                    {
                        do
                        {
                            Console.WriteLine("********************");
                            Console.WriteLine("\tCustomer menu");
                            Console.WriteLine("********************");
                            Console.WriteLine("1----Withdraw Cash: ");
                            Console.WriteLine("2----Cash Transfer");
                            Console.WriteLine("3----Deposit Cash");
                            Console.WriteLine("4----Display Balance");
                            Console.WriteLine("5----Exit");

                            Console.WriteLine("********************");
                            Console.WriteLine("Enter your choice: ");
                            int custmrCh = System.Convert.ToInt32(Console.ReadLine());
                            switch (custmrCh)
                            {
                                case 1:
                                    Console.WriteLine("a) Fast Cash");
                                    Console.WriteLine("b) Normal Cash");
                                    int custChoice = System.Convert.ToInt32(Console.ReadLine());

                                    switch (custChoice)
                                    {
                                        case 1:
                                            Console.WriteLine(" 1----500");
                                            Console.WriteLine(" 2----1000");
                                            Console.WriteLine(" 3----2000");
                                            Console.WriteLine(" 4----5000");
                                            Console.WriteLine(" 5----10000");
                                            Console.WriteLine(" 6----15000");
                                            Console.WriteLine(" 7----20000");
                                            Console.WriteLine(" Select one of the denominations of money:");
                                            int withdrawCh = System.Convert.ToInt32(Console.ReadLine());
                                            bool result = false;
                                            switch (withdrawCh)
                                            {

                                                case 1:
                                                    Console.WriteLine("Are you sure you want to withdraw Rs.500(Y / N))");
                                                    char ch = System.Convert.ToChar(Console.ReadLine());
                                                    if (ch == 'y' || ch == 'Y')
                                                    {
                                                        result = bll.Withdraw(customer, 500);
                                                        if (result is true)
                                                        {
                                                            Console.WriteLine("500 withdrwal from account");
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Withdrwal Operation failed");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Invalid choice");
                                                    }

                                                    break;
                                                case 2:
                                                    Console.WriteLine("Are you sure you want to withdraw Rs.1000(Y / N))");
                                                    ch = System.Convert.ToChar(Console.ReadLine());
                                                    if (ch == 'y' || ch == 'Y')
                                                    {
                                                        result = bll.Withdraw(customer, 1000);
                                                        if (result is true)
                                                        {
                                                            Console.WriteLine("1000 withdrwal from account");
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Withdrwal Operation failed");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Invalid choice");
                                                    }

                                                    break;
                                                case 3:
                                                    Console.WriteLine("Are you sure you want to withdraw Rs.2000(Y / N))");
                                                    ch = System.Convert.ToChar(Console.ReadLine());
                                                    if (ch == 'y' || ch == 'Y')
                                                    {
                                                        result = bll.Withdraw(customer, 2000);
                                                        if (result is true)
                                                        {
                                                            Console.WriteLine("2000 withdrwal from account");
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Withdrwal Operation failed");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Invalid choice");
                                                    }

                                                    break;
                                                case 4:
                                                    Console.WriteLine("Are you sure you want to withdraw Rs.5000(Y / N))");
                                                    ch = System.Convert.ToChar(Console.ReadLine());
                                                    if (ch == 'y' || ch == 'Y')
                                                    {
                                                        result = bll.Withdraw(customer, 5000);
                                                        if (result is true)
                                                        {
                                                            Console.WriteLine("5000 withdrwal from account");
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Withdrwal Operation failed");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Invalid choice");
                                                    }

                                                    break;
                                                case 5:
                                                    Console.WriteLine("Are you sure you want to withdraw Rs.10000(Y / N))");
                                                    ch = System.Convert.ToChar(Console.ReadLine());
                                                    if (ch == 'y' || ch == 'Y')
                                                    {
                                                        result = bll.Withdraw(customer, 10000);
                                                        if (result is true)
                                                        {
                                                            Console.WriteLine("10000 withdrwal from account");
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Withdrwal Operation failed");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Invalid choice");
                                                    }

                                                    break;
                                                case 6:
                                                    Console.WriteLine("Are you sure you want to withdraw Rs.15000(Y / N))");
                                                    ch = System.Convert.ToChar(Console.ReadLine());
                                                    if (ch == 'y' || ch == 'Y')
                                                    {
                                                        result = bll.Withdraw(customer, 15000);
                                                        if (result is true)
                                                        {
                                                            Console.WriteLine("15000 withdrwal from account");
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Withdrwal Operation failed");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Invalid choice");
                                                    }

                                                    break;
                                                case 7:
                                                    Console.WriteLine("Are you sure you want to withdraw Rs.20000(Y / N))");
                                                    ch = System.Convert.ToChar(Console.ReadLine());
                                                    if (ch == 'y' || ch == 'Y')
                                                    {
                                                        result = bll.Withdraw(customer, 20000);
                                                        if (result is true)
                                                        {
                                                            Console.WriteLine("20000 withdrwal from account");
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Withdrwal Operation failed");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Invalid choice");
                                                    }

                                                    break;
                                                default:
                                                    Console.WriteLine("Invalid choice");
                                                    break;

                                            }
                                            break;
                                        case 2:
                                            Console.WriteLine("Enter the withdrawal amount:");
                                            int cash = System.Convert.ToInt32(Console.ReadLine());
                                            result = bll.Withdraw(customer, cash);
                                            if (result is true)
                                            {
                                                Console.WriteLine(cash + " cash has been withdraw from your account");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Withdrwal Operation failed");
                                            }
                                            break;
                                    }
                                    break;

                                case 2:
                                    int cashTrans = 0;
                                    do
                                    {
                                        Console.WriteLine("Enter the amount multiples of 500:");
                                        cashTrans = System.Convert.ToInt32(Console.ReadLine());
                                    } while (cashTrans % 500 != 0);

                                    Console.WriteLine("Enter the account number to which you want to transfer: ");
                                    int accNum1 = System.Convert.ToInt32(Console.ReadLine());

                                    string Custname = bll.getCustmrName(accNum1);
                                    if (Custname != "")
                                    {
                                        Console.WriteLine("You wish to deposit " + cashTrans + " in account held by: " + Custname);
                                        Console.WriteLine("If this information is correct please re-enter the account number: ");
                                        int accNum2 = System.Convert.ToInt32(Console.ReadLine());
                                        if (accNum2 == accNum1)
                                        {
                                            bool check2 = bll.cashTransfer(customer, cashTrans, accNum2);
                                            if (check2 == true)
                                            {
                                                Console.WriteLine("Transaction confirmed.");
                                                Console.WriteLine("Do you wish to print a receipt (Y/N)?");
                                                char input = System.Convert.ToChar(Console.ReadLine());
                                                if (input == 'Y' || input == 'y')
                                                {
                                                    Console.WriteLine("You account #:" + customer.accountNumber);
                                                    Console.WriteLine(DateTime.Now.ToString("h:mm:ss tt"));
                                                    Console.WriteLine("Amount Transfered #:" + cashTrans);

                                                    Console.WriteLine("You Balance is :" + customer.Balance);
                                                }

                                            }
                                            else
                                            {
                                                Console.WriteLine("Transaction Failed.");
                                            }

                                        }
                                        else
                                        {
                                            Console.WriteLine("Enter the same account Number");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid account number");
                                    }

                                    break;

                                case 3:
                                    Console.WriteLine("Enter the cash amount to deposit:");
                                    int cashDep = System.Convert.ToInt32(Console.ReadLine());
                                    bool check = bll.DepositCash(customer, cashDep);
                                    if (check is true)
                                    {
                                        Console.WriteLine(cashDep + " cash has been deposited in your account");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Withdrwal Deposition failed");
                                    }
                                    break;
                                case 4:
                                    Console.WriteLine("You account #:" + customer.accountNumber);
                                    Console.WriteLine(DateTime.Now.ToString("h:mm:ss tt"));
                                    Console.WriteLine("You Balance is :" + customer.Balance);

                                    break;
                                case 5:
                                    System.Environment.Exit(0);
                                    break;
                                default:
                                    Console.WriteLine("Default");
                                    break;
                            } // end of switch statement
                            Console.WriteLine("Enter \"yes\" to continue or any key to exit : ");
                            close_program = Console.ReadLine();
                            while (string.IsNullOrEmpty(close_program))
                            {
                                Console.WriteLine("Input can't be empty! Enter again.");
                                close_program = Console.ReadLine();
                            }
                            Console.Clear();
                        } while (close_program == "yes");
                    }
                    else
                    {
                        Console.WriteLine("Invalid credentils.");
                    }
                    break;    //customer part case end here
                default:
                    Console.WriteLine("Invalid choice");
                    break;   //end of main switch

            }
        }
    }
}
