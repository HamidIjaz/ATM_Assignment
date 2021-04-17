using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

using ATM_OB;
using System.IO;
using System.Linq;

namespace ATM_DAL
{
    public class Atm_DAL
    {
        public void SaveToFile(Customer bo)
        {

            bo.accountNumber = getAccNum(bo);

           
            string text = $"{bo.userName},{bo.pinCode}," +
                $"{bo.holderName},{bo.type},{bo.Balance},{bo.status},{bo.accountNumber}";
           
            string filePath = Path.Combine(Environment.CurrentDirectory,"Customer.txt");
            StreamWriter sw = new StreamWriter(filePath, append: true);
            sw.WriteLine(text);
            sw.Close();
            
            Console.WriteLine("Account has been created - With account number: "
                +bo.accountNumber);
        }

        double getAccNum(Customer ob)
        {
            int count = 1;
            string line;
            TextReader reader = new StreamReader("Customer.txt");
            while ((line = reader.ReadLine()) != null)
            {
                count++;
            }
            reader.Close();
            return count;
        }

        internal List<string> Read(string fileName)
        {

            List<string> list = new List<string>();
            string filePath = Path.Combine(Environment.CurrentDirectory,
                fileName);
            StreamReader sr = new StreamReader(filePath);
            string line = String.Empty;
            while ((line = sr.ReadLine()) != null)
            {

                list.Add(line);

            }
            sr.Close();
            return list;
        }
        public List<Customer> searchAccount(Customer ob)
        {

            List<String> stringList = Read("Customer.txt");
            List<Customer> custList = new List<Customer>();
            foreach (string s in stringList)
            {

                string[] data = s.Split(',');
                Customer cOb = new Customer();
                cOb.userName = data[0];
                cOb.pinCode= System.Convert.ToInt32(data[1]);
                cOb.holderName = data[2];
                cOb.type = data[3];
                cOb.Balance= System.Convert.ToInt32(data[4]);
                cOb.status = data[5];
                cOb.accountNumber = System.Convert.ToInt32(data[6]);
                if (cOb.userName==ob.userName | cOb.pinCode== ob.pinCode || cOb.status==ob.status||
                    cOb.Balance==ob.Balance | cOb.type==ob.type || cOb.holderName==ob.holderName)
                {
                    custList.Add(cOb);
                }
                
            }
            return custList;
        }




        public bool updateAccount(double accNo, Customer ob)
        {
            bool check = false;
            List<String> stringList = Read("Customer.txt");
            List<Customer> custList = new List<Customer>();
            string filePath = Path.Combine(Environment.CurrentDirectory, "Customer.txt");
            File.WriteAllText(filePath, String.Empty);
            foreach (string s in stringList)
            {
                string[] data = s.Split(',');
                if (data[6]== accNo.ToString())
                {
                    string text = $"{ob.userName},{ob.pinCode}," +
                        $"{ob.holderName},{ob.type},{ob.Balance},{ob.status},{data[6]}";
                    StreamWriter sw = new StreamWriter(filePath, append: true);
                    sw.WriteLine(text);
                    sw.Close();
                    check = true;
                }
                else
                {
                    Customer cOb = new Customer();
                    cOb.userName = data[0];
                    cOb.pinCode = System.Convert.ToInt32(data[1]);
                    cOb.holderName = data[2];
                    cOb.type = data[3];
                    cOb.Balance = System.Convert.ToInt32(data[4]);
                    cOb.status = data[5];
                    cOb.accountNumber = System.Convert.ToInt32(data[6]);
                    string text = $"{cOb.userName},{cOb.pinCode}," +
                        $"{cOb.holderName},{cOb.type},{cOb.Balance},{cOb.status},{cOb.accountNumber}";
                    StreamWriter sw = new StreamWriter(filePath, append: true);
                    sw.WriteLine(text);
                    sw.Close();
                }

            }
            return check;
        }
        public double deleteAccount(int accNo)
        {
            List<String> stringList = Read("Customer.txt");
            List<Customer> custList = new List<Customer>();
            string filePath = Path.Combine(Environment.CurrentDirectory, "Customer.txt");
            double accNum = 0;
            File.WriteAllText(filePath, String.Empty);
            foreach (string s in stringList)
            {
                string[] data = s.Split(',');
                Customer cOb = new Customer();
                cOb.userName = data[0];
                cOb.pinCode = System.Convert.ToInt32(data[1]);
                cOb.holderName = data[2];
                cOb.type = data[3];
                cOb.Balance = System.Convert.ToInt32(data[4]);
                cOb.status = data[5];
                cOb.accountNumber = System.Convert.ToInt32(data[6]);
                if (cOb.accountNumber != accNo)
                {
                    string text = $"{cOb.userName},{cOb.pinCode}," +
                        $"{cOb.holderName},{cOb.type},{cOb.Balance},{cOb.status},{cOb.accountNumber}";
                    StreamWriter sw = new StreamWriter(filePath, append: true);
                    sw.WriteLine(text);
                    sw.Close();
                }
                else
                {
                    accNum = cOb.accountNumber;
                }

            }
            return accNum;
        }

        //  Functions for Customer

        public bool WithdrawFromFile(Customer customer, int money)
        {
            bool check = false;
            List<String> stringList = Read("Customer.txt");
            Customer cOb = new Customer();
            string filePath = Path.Combine(Environment.CurrentDirectory, "Customer.txt");
            File.WriteAllText(filePath, String.Empty);
            foreach (string s in stringList)
            {
                string[] data = s.Split(',');

                cOb.userName = data[0];
                cOb.pinCode = System.Convert.ToInt32(data[1]);
                cOb.holderName = data[2];
                cOb.type = data[3];
                cOb.Balance = System.Convert.ToInt32(data[4]);
                cOb.status = data[5];
                cOb.accountNumber = System.Convert.ToInt32(data[6]);
                if (cOb.accountNumber == customer.accountNumber)
                {
                    cOb.Balance = cOb.Balance - money;
                    customer.Balance = cOb.Balance;
                    string text = $"{cOb.userName},{cOb.pinCode}," +
                        $"{cOb.holderName},{cOb.type},{cOb.Balance},{cOb.status},{cOb.accountNumber}";
                    StreamWriter sw = new StreamWriter(filePath, append: true);
                    sw.WriteLine(text);
                    sw.Close();
                    check = true;
                }
                else
                {
                    string text = $"{cOb.userName},{cOb.pinCode}," +
                        $"{cOb.holderName},{cOb.type},{cOb.Balance},{cOb.status},{cOb.accountNumber}";
                    StreamWriter sw = new StreamWriter(filePath, append: true);
                    sw.WriteLine(text);
                    sw.Close();
                }

            }
            return check;
        }

        public List<Customer> getReports(int min, int max)
        {

            List<String> stringList = Read("Customer.txt");
            List<Customer> custList = new List<Customer>();
            foreach (string s in stringList)
            {

                string[] data = s.Split(',');
                Customer cOb = new Customer();
                cOb.userName = data[0];
                cOb.pinCode = System.Convert.ToInt32(data[1]);
                cOb.holderName = data[2];
                cOb.type = data[3];
                cOb.Balance = System.Convert.ToInt32(data[4]);
                cOb.status = data[5];
                cOb.accountNumber = System.Convert.ToInt32(data[6]);
                if (cOb.Balance >=min && cOb.Balance<=max)
                {
                    custList.Add(cOb);
                }

            }
            return custList;
        }

        public Customer getCustomer(String custName, int custPin)
        {
            Customer cOb = new Customer();
            Customer currentCustomer = new Customer();
            List<String> stringList = Read("Customer.txt");
            string filePath = Path.Combine(Environment.CurrentDirectory, "Customer.txt");
            foreach (string s in stringList)
            {
                string[] data = s.Split(',');
                cOb.userName = data[0];
                cOb.pinCode = System.Convert.ToInt32(data[1]);
                cOb.holderName = data[2];
                cOb.type = data[3];
                cOb.Balance = System.Convert.ToInt32(data[4]);
                cOb.status = data[5];
                cOb.accountNumber = System.Convert.ToInt32(data[6]);
                if (cOb.userName == custName && cOb.pinCode == custPin)
                {
                    currentCustomer = cOb;
                    return currentCustomer;
                }
                else
                {
                    currentCustomer = null;
                }

            }
            return currentCustomer;
        }

        public string getName(int accNum)
        {
            string name = "";
            List<String> stringList = Read("Customer.txt");
            foreach (string s in stringList)
            {
                string[] data = s.Split(',');
               
                if (System.Convert.ToInt32(data[6]) == accNum)
                {
                    name = data[2];
                }
            }
            return name;
        }


        public bool DepositInFile(Customer customer, int ammount)
        {
            bool check = false;
            List<String> stringList = Read("Customer.txt");
            Customer cOb = new Customer();
            string filePath = Path.Combine(Environment.CurrentDirectory, "Customer.txt");
            File.WriteAllText(filePath, String.Empty);
            foreach (string s in stringList)
            {
                string[] data = s.Split(',');

                cOb.userName = data[0];
                cOb.pinCode = System.Convert.ToInt32(data[1]);
                cOb.holderName = data[2];
                cOb.type = data[3];
                cOb.Balance = System.Convert.ToInt32(data[4]);
                cOb.status = data[5];
                cOb.accountNumber = System.Convert.ToInt32(data[6]);
                if (cOb.accountNumber == customer.accountNumber)
                {
                    cOb.Balance = cOb.Balance + ammount;
                    customer.Balance = cOb.Balance;

                    string text = $"{cOb.userName},{cOb.pinCode}," +
                        $"{cOb.holderName},{cOb.type},{cOb.Balance},{cOb.status},{cOb.accountNumber}";
                    StreamWriter sw = new StreamWriter(filePath, append: true);
                    sw.WriteLine(text);
                    sw.Close();
                    check = true;
                }
                else
                {
                    string text = $"{cOb.userName},{cOb.pinCode}," +
                        $"{cOb.holderName},{cOb.type},{cOb.Balance},{cOb.status},{cOb.accountNumber}";
                    StreamWriter sw = new StreamWriter(filePath, append: true);
                    sw.WriteLine(text);
                    sw.Close();
                }

            }
            return check;
        }

        public bool TransCash(Customer customer, int ammount, int accNum)
        {
            bool check = false;
            bool check2 = false;
            List<String> stringList = Read("Customer.txt");
            Customer cOb = new Customer();
            string filePath = Path.Combine(Environment.CurrentDirectory, "Customer.txt");
            File.WriteAllText(filePath, String.Empty);
            foreach (string s in stringList)
            {
                string[] data = s.Split(',');

                cOb.userName = data[0];
                cOb.pinCode = System.Convert.ToInt32(data[1]);
                cOb.holderName = data[2];
                cOb.type = data[3];
                cOb.Balance = System.Convert.ToInt32(data[4]);
                cOb.status = data[5];
                cOb.accountNumber = System.Convert.ToInt32(data[6]);
                if (cOb.accountNumber == customer.accountNumber)
                {
                    cOb.Balance = cOb.Balance - ammount;
                    customer.Balance = cOb.Balance;

                    string text = $"{cOb.userName},{cOb.pinCode}," +
                        $"{cOb.holderName},{cOb.type},{cOb.Balance},{cOb.status},{cOb.accountNumber}";
                    StreamWriter sw = new StreamWriter(filePath, append: true);
                    sw.WriteLine(text);
                    sw.Close();
                    check = true;
                }
                else if (cOb.accountNumber == accNum)
                {
                    cOb.Balance = cOb.Balance + ammount;
                    string text = $"{cOb.userName},{cOb.pinCode}," +
                        $"{cOb.holderName},{cOb.type},{cOb.Balance},{cOb.status},{cOb.accountNumber}";
                    StreamWriter sw = new StreamWriter(filePath, append: true);
                    sw.WriteLine(text);
                    sw.Close();
                    check2 = true;
                }
                else
                {
                    string text = $"{cOb.userName},{cOb.pinCode}," +
                        $"{cOb.holderName},{cOb.type},{cOb.Balance},{cOb.status},{cOb.accountNumber}";
                    StreamWriter sw = new StreamWriter(filePath, append: true);
                    sw.WriteLine(text);
                    sw.Close();
                }

                
            }

            if (check2 == true && check== true)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

    }
}
