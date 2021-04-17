using ATM_OB;
using System;
using System.Collections.Generic;
using System.Text;
using ATM_DAL;

namespace ATM_BLL
{
    public class Atm_bll    
    {
        public void CreateCustomer(Customer bo)
        {
            Atm_DAL ob = new Atm_DAL();
            ob.SaveToFile(bo);
            
        }

        public List<Customer> searchCustomer(Customer ob)
        {
            
            Atm_DAL dal_ob = new Atm_DAL();
            List<Customer> list=dal_ob.searchAccount(ob);
            return list;

        }
        public bool updateCustomer(double accNo,Customer ob)
        {

            Atm_DAL dalOb = new Atm_DAL();
            bool check = dalOb.updateAccount(accNo, ob);
            return check;

        }


        public double deleteCustomer(int accNo)
        {

            Atm_DAL ob = new Atm_DAL();
            double accNumber = ob.deleteAccount(accNo);
            return accNumber;
            

        }
        public bool Withdraw(Customer customer, int money)
        {

            Atm_DAL ob = new Atm_DAL();
            bool check = ob.WithdrawFromFile(customer, money);
            return check;

        }

        public bool DepositCash(Customer customer, int money)
        {

            Atm_DAL ob = new Atm_DAL();
            bool check = ob.DepositInFile(customer, money);
            return check;

        }

        public bool cashTransfer(Customer customer, int money, int accNum)
        {

            Atm_DAL ob = new Atm_DAL();
            bool check = ob.TransCash(customer, money, accNum);
            return check;

        }
        public List<Customer> viewReport(int min, int max)
        {

            Atm_DAL dal_ob = new Atm_DAL();
            List<Customer> list = dal_ob.getReports(min, max);
            return list;

        }
        public string getCustmrName(int accNum)
        {
            Atm_DAL ob = new Atm_DAL();
            string cutomerName = ob.getName(accNum);
            return cutomerName;
        }

        public Customer getCustomerData(String custName, int custPin)
        {
            Atm_DAL ob = new Atm_DAL();
            Customer cutomerObj = ob.getCustomer(custName, custPin);
            return cutomerObj;
        }


    }
}
