using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;


namespace ATM_OB
{
    //customer class
    public class Customer
    {
        public double accountNumber = 0;
        public string userName { get; set; }
        public int pinCode { get; set; }        
        public string holderName { get; set; }
        public string type{ get; set; }         
        public double Balance { get; set; }
        public string status { get; set; }
      
        
    }

}
