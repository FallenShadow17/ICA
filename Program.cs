using System;
using System.Xml;
using System.IO;
using System.Data.OleDb;
using System.Data;
using System.Data.SqlClient;

namespace ICAssignment

{
    //used to store the value of a Customer
    public class Customer
    {
        public int ID { get; set; }
        public string CustomerName { get; set; }
        public string AccountNumber { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerState { get; set; }
        public string CustomerZip { get; set; }
        public DateTime Date { get; set; }
    }
    //used to store the value of a bill
    public class Bill
    {
        public int ID { get; set; }
        public DateTime BillDate { get; set; }
        public string BillNumber { get; set; }
        public double BillAmount { get; set; }
        public string FormatGUID { get; set; }
        public double AccountBalance { get; set; }
        public DateTime DueDate { get; set; }
        public string ServiceAddress { get; set; }
        public DateTime FirstEmailDate { get; set; }
        public DateTime SecondEmailDate { get; set; }
        public DateTime DateAdded{ get; set; }
        public int Numberpublic { get; set; }


}
    //creating a class to store the xml information
    public class Invoice
    {
        public string InvoiceNumber { get; set; }
        public string AccountNumber { get; set; }
        public string Name { get; set; }
        public string CycleCd { get; set; }
        public string Date  { get; set; }
        public string DueDate { get; set; }
        public string BillAmount { get; set; }
        public string BalanceDue { get; set; }
        public string BillRunDate { get; set; }
        public string BillRunSeq { get; set; }
        public string CurrentDate { get; set; }
        public string BillRunTime { get; set; }
        public string BillType { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string AccountClass { get; set; }

    }

    class Program
    {
        //enter the path of the .xml file  change if needed
        private const string Path = "C:\\Users\\Red Ryan\\Documents\\ICAssignment\\ICAssignment\\BillFile.xml";



        static void Main(string[] args)
        {
            //create a xml reader
            XmlTextReader billFileReader = null;

            //open and load .xml file
            try
            {
                billFileReader = new XmlTextReader(Path);
                billFileReader.WhitespaceHandling = WhitespaceHandling.None;
            }
            catch (Exception e)
            {
                //Exception when file not found
                Console.WriteLine("Exception: " + e.Message);
            }


            Invoice invc = new Invoice();
            int invoiceCounter = 0;
            string text = "";
            double totalBillValue = 0;
            string tDate = DateTime.Now.ToString("MM/dd/yyyy");

            //Parse file
            while (billFileReader.Read())
            {

                switch (billFileReader.NodeType)
                {
                    //read elements to find the data needed
                    case XmlNodeType.Element:
                        switch (billFileReader.Name)
                        {
                            case ("Invoice_No"):
                                //Console.WriteLine("testing");
                                billFileReader.Read();
                                invc.InvoiceNumber = billFileReader.Value.ToString();
                                break;
                            case ("Account_No"):
                                billFileReader.Read();
                                invc.AccountNumber = billFileReader.Value.ToString();
                                break;
                            case ("Customer_Name"):
                                billFileReader.Read();
                                invc.Name = billFileReader.Value.ToString();
                                break;
                            case ("Cycle_Cd"):
                                billFileReader.Read();
                                invc.CycleCd = billFileReader.Value.ToString();
                                break;
                            case ("Bill_Dt"):
                                billFileReader.Read();
                                invc.Date = billFileReader.Value.ToString();
                                invc.Date = DateFormat(invc.Date);
                                break;
                            case ("Due_Dt"):
                                billFileReader.Read();
                                invc.DueDate = billFileReader.Value.ToString();//billFileReader.Value.ToString();
                                invc.DueDate = DateFormat(invc.DueDate);
                                break;
                            case ("Bill_Amount"):
                                billFileReader.Read();
                                invc.BillAmount = billFileReader.Value.ToString();
                                break;
                            case ("Balance_Due"):
                                billFileReader.Read();
                                invc.BalanceDue = billFileReader.Value.ToString();
                                break;


                            //is this current run date?

                            case ("Bill_Run_Dt"):
                                billFileReader.Read();
                                invc.CurrentDate = billFileReader.Value.ToString();
                                invc.CurrentDate = DateFormat(invc.CurrentDate);
                                break;
                            case ("Bill_Run_Seq"):
                                billFileReader.Read();
                                invc.BillRunSeq = billFileReader.Value.ToString();
                                break;
                            case ("Bill_Run_Tm"):
                                billFileReader.Read();
                                invc.BillRunTime = billFileReader.Value.ToString();
                                break;
                            case ("Bill_Tp"):
                                billFileReader.Read();
                                invc.BillType = billFileReader.Value.ToString();
                                break;
                            case ("Mailing_Address_1"):
                                billFileReader.Read();
                                invc.Address1 = billFileReader.Value.ToString();
                                break;
                            case ("Mailing_Address_2"):
                                billFileReader.Read();
                                invc.Address2 = billFileReader.Value.ToString();
                                break;
                            case ("City"):
                                billFileReader.Read();
                                invc.City = billFileReader.Value.ToString();
                                break;
                            case ("State"):
                                billFileReader.Read();
                                invc.State = billFileReader.Value.ToString();
                                break;
                            case ("Zip"):
                                billFileReader.Read();
                                invc.Zip = billFileReader.Value.ToString();
                                break;
                            case ("Account_Class"):
                                billFileReader.Read();
                                invc.AccountClass = billFileReader.Value.ToString();
                                break;
                        }
                        break;
                    case XmlNodeType.EndElement:
                        if (billFileReader.Name == "BILL_HEADER")
                        {
                            //writing the information into a string for output.
                            totalBillValue += Convert.ToDouble(invc.BillAmount);
                            text += "AA~CT|BB~" + invc.AccountNumber + "|VV~" + invc.Name + "|CC~" + invc.Address1 + "|DD~" + invc.Address2 + "|EE~" + invc.City + "|FF~" + invc.State + "|GG~" + invc.Zip + "\n" +
                                "HH~IH|II~R|JJ~8E2FEA69-5D77-4D0F-898E-DFA25677D19E|KK~" + invc.InvoiceNumber + "|LL~" + invc.Date + "|MM~" + invc.DueDate + "|NN~" + invc.BillAmount + "|OO~" + DateTime.Parse(invc.CurrentDate).AddDays(+5).ToString("MM/dd/yyyy") + "|PP~" + DateTime.Parse(invc.DueDate).AddDays(-3).ToString("MM/dd/yyyy") + "|QQ~" + invc.BalanceDue + "|RR~" + invc.CurrentDate + "|SS~\n";
                            invoiceCounter++;
                        }
                        break;
                }

            }

            //adding the first part of the fomating
            string startText = "1~FR|2~8203ACC7-2094-43CC-8F7A-B8F19AA9BDA2|3~Sample UT file|4~" + tDate + "|5~" + invoiceCounter + "|6~" + totalBillValue + "\n";


            //writes text to file
            string rptFile = "BillFile-" + DateTime.Now.ToString("MMddyyyy") + ".rpt";
            File.WriteAllTextAsync(rptFile, startText + text);



            //2. MBD needs to be moved to a routine if time permits
            string fileNamePath = Environment.CurrentDirectory + "\\billing.mdb";

            Bill tempBill = new Bill();
            Customer tempCustomer = new Customer();
            //use to get the information needed from the rpt;
            string tempString;

            //checks if file was/is created
            string rptPath = Environment.CurrentDirectory + "\\" + rptFile;
            try
            {
                if (File.Exists(rptPath))
                {

                    Console.WriteLine("File exists");

                    using StreamReader sr = new StreamReader(rptPath);
                    char[] c = null;

                    while (sr.Peek() >= 0)
                    {
                        //reset tempString
                        tempString = null;
                        c = new char[1];
                        sr.Read(c, 0, c.Length);

                        //gets the first character to see what the data is in the rpt file. then/or reads to | 
                        switch (c[0].ToString())
                        {
                            //1~FR
                            case "1":
                                //nothing read to |
                                while (c[0].ToString() != "|")
                                {
                                    sr.Read(c, 0, c.Length);
                                }
                                break;
                            //FORMAT_GUID
                            case "2":
                                while (c[0].ToString() != "|")
                                {
                                    if (c[0].ToString() == "~")
                                    {
                                        sr.Read(c, 0, c.Length);
                                        while (c[0].ToString() != "|")
                                        {

                                            tempString += c[0];
                                            sr.Read(c, 0, c.Length);
                                        }
                                        break;

                                    }
                                    sr.Read(c, 0, c.Length);
                                }
                                tempBill.FormatGUID = tempString;
                                break;
                            //3~ Sample UT file read to |
                            case "3":
                                while (c[0].ToString() != "|")
                                {
                                    sr.Read(c, 0, c.Length);
                                }
                                break;
                            //4~ CURRENT_DATE read to |
                            case "4":
                                while (c[0].ToString() != "|")
                                {
                                    sr.Read(c, 0, c.Length);
                                }
                                break;
                            //5~ INVOICE_RECORD_COUNT read to |
                            case "5":
                                while (c[0].ToString() != "|")
                                {
                                    sr.Read(c, 0, c.Length);
                                }
                                break;
                            //~6 INVOICE_RECORD_TOTAL_AMOUNT read to |
                            case "6":
                                while (c[0].ToString() != "|")
                                {
                                    sr.Read(c, 0, c.Length);
                                }
                                break;
                            //A-G are for Customers read to |
                            case "A":
                                while (c[0].ToString() != "|")
                                {
                                    sr.Read(c, 0, c.Length);
                                }
                                break;
                            //BB Account_Number
                            case "B":
                                while (c[0].ToString() != "|")
                                {
                                    if (c[0].ToString() == "~")
                                    {
                                        sr.Read(c, 0, c.Length);
                                        while (c[0].ToString() != "|")
                                        {

                                            tempString += c[0];
                                            sr.Read(c, 0, c.Length);
                                        }
                                        break;

                                    }
                                    sr.Read(c, 0, c.Length);
                                }
                                if (tempString == "") { tempString = "NA"; }
                                tempCustomer.AccountNumber = tempString;
                                break;
                            //VV Customer_Name       
                            case "V":
                                while (c[0].ToString() != "|")
                                {
                                    if (c[0].ToString() == "~")
                                    {
                                        sr.Read(c, 0, c.Length);
                                        while (c[0].ToString() != "|")
                                        {

                                            tempString += c[0];
                                            sr.Read(c, 0, c.Length);
                                        }
                                        break;

                                    }
                                    sr.Read(c, 0, c.Length);
                                }
                                if (tempString == "") { tempString = "NA"; }
                                tempCustomer.CustomerName = tempString;
                                break;
                            //CC Address_1
                            case "C":
                                while (c[0].ToString() != "|")
                                {
                                    if (c[0].ToString() == "~")
                                    {
                                        sr.Read(c, 0, c.Length);
                                        while (c[0].ToString() != "|")
                                        {

                                            tempString += c[0];
                                            sr.Read(c, 0, c.Length);
                                        }

                                        break;

                                    }
                                    sr.Read(c, 0, c.Length);
                                }
                                if (tempString == "") { tempString = "NA"; }
                                tempCustomer.CustomerAddress = tempString;
                                break;
                            //DD Address_2 . read to |
                            case "D":
                                while (c[0].ToString() != "|")
                                {
                                    sr.Read(c, 0, c.Length);
                                }
                                break;
                            //EE Customer_City
                            case "E":
                                while (c[0].ToString() != "|")
                                {
                                    if (c[0].ToString() == "~")
                                    {
                                        sr.Read(c, 0, c.Length);
                                        while (c[0].ToString() != "|")
                                        {

                                            tempString += c[0];
                                            sr.Read(c, 0, c.Length);
                                        }
                                        break;

                                    }
                                    sr.Read(c, 0, c.Length);
                                }
                                if (tempString == "") { tempString = "NA"; }
                                tempCustomer.CustomerCity = tempString;
                                break;
                            //FF Customer_State
                            case "F":
                                while (c[0].ToString() != "|")
                                {
                                    if (c[0].ToString() == "~")
                                    {
                                        sr.Read(c, 0, c.Length);
                                        while (c[0].ToString() != "|")
                                        {
                                            tempString += c[0];
                                            sr.Read(c, 0, c.Length);
                                        }
                                        break;
                                    }
                                    sr.Read(c, 0, c.Length);
                                }
                                tempCustomer.CustomerState = tempString;
                                break;
                            //GG Customer_Zip
                            case "G":
                                while (c[0].ToString() != "|")
                                {
                                    if (c[0].ToString() == "~")
                                    {
                                        sr.Read(c, 0, c.Length);
                                        while (c[0].ToString() != "|" && c[0].ToString() != "\n")
                                        {
                                            tempString += c[0];
                                            sr.Read(c, 0, c.Length);
                                        }
                                        break;
                                    }
                                    sr.Read(c, 0, c.Length);
                                }
                                tempCustomer.CustomerZip = tempString;
                                break;
                            //HH-SS are for Bills
                            //HH~IH No need for HH read to |
                            case "H":
                                while (c[0].ToString() != "|")
                                {
                                    sr.Read(c, 0, c.Length);
                                }
                                break;
                            //II~R No need for II read to |
                            case "I":
                                while (c[0].ToString() != "|")
                                {
                                    sr.Read(c, 0, c.Length);
                                }
                                break;
                            //JJ INVOICE_FORMAT 
                            case "J":
                                while (c[0].ToString() != "|")
                                {
                                    if (c[0].ToString() == "~")
                                    {
                                        sr.Read(c, 0, c.Length);
                                        while (c[0].ToString() != "|")
                                        {
                                            tempString += c[0];
                                            sr.Read(c, 0, c.Length);
                                        }
                                        break;
                                    }
                                    sr.Read(c, 0, c.Length);
                                }
                                tempBill.FormatGUID = tempString;
                                break;
                            //KK INVOICE_NUMBER
                            case "K":
                                while (c[0].ToString() != "|")
                                {
                                    if (c[0].ToString() == "~")
                                    {
                                        sr.Read(c, 0, c.Length);
                                        while (c[0].ToString() != "|")
                                        {
                                            tempString += c[0];
                                            sr.Read(c, 0, c.Length);
                                        }
                                        break;
                                    }
                                    sr.Read(c, 0, c.Length);
                                }
                                tempBill.BillNumber = tempString;
                                break;
                            //LL Bill_Date
                            case "L":
                                while (c[0].ToString() != "|")
                                {
                                    if (c[0].ToString() == "~")
                                    {
                                        sr.Read(c, 0, c.Length);
                                        while (c[0].ToString() != "|")
                                        {

                                            tempString += c[0];
                                            sr.Read(c, 0, c.Length);
                                        }
                                        break;

                                    }
                                    sr.Read(c, 0, c.Length);
                                }
                                tempBill.BillDate = DateTime.Parse(tempString);
                                break;
                            //MM DUE_Date
                            case "M":
                                while (c[0].ToString() != "|")
                                {
                                    if (c[0].ToString() == "~")
                                    {
                                        sr.Read(c, 0, c.Length);
                                        while (c[0].ToString() != "|")
                                        {

                                            tempString += c[0];
                                            sr.Read(c, 0, c.Length);
                                        }
                                        break;

                                    }
                                    sr.Read(c, 0, c.Length);
                                }
                                tempBill.DueDate = DateTime.Parse(tempString);

                                break;
                            //NN BILL_AMOUNT
                            case "N":
                                while (c[0].ToString() != "|")
                                {
                                    if (c[0].ToString() == "~")
                                    {
                                        sr.Read(c, 0, c.Length);
                                        while (c[0].ToString() != "|")
                                        {

                                            tempString += c[0];
                                            sr.Read(c, 0, c.Length);
                                        }
                                        break;

                                    }
                                    sr.Read(c, 0, c.Length);
                                }
                                tempBill.BillAmount = Convert.ToDouble(tempString);
                                break;
                            //OO FIRST_NOTIFICATION_EMAIL
                            case "O":
                                while (c[0].ToString() != "|")
                                {
                                    if (c[0].ToString() == "~")
                                    {
                                        sr.Read(c, 0, c.Length);
                                        while (c[0].ToString() != "|")
                                        {

                                            tempString += c[0];
                                            sr.Read(c, 0, c.Length);
                                        }

                                        break;

                                    }
                                    sr.Read(c, 0, c.Length);
                                }
                                tempBill.FirstEmailDate = DateTime.Parse(tempString);
                                break;
                            //PP SECOND_NOTIFICATION _DATE
                            case "P":
                                while (c[0].ToString() != "|")
                                {
                                    if (c[0].ToString() == "~")
                                    {
                                        sr.Read(c, 0, c.Length);
                                        while (c[0].ToString() != "|")
                                        {

                                            tempString += c[0];
                                            sr.Read(c, 0, c.Length);
                                        }
                                        break;

                                    }
                                    sr.Read(c, 0, c.Length);
                                }
                                tempBill.SecondEmailDate = DateTime.Parse(tempString);
                                break;
                            //QQ BALANCE_DUE
                            case "Q":
                                while (c[0].ToString() != "|")
                                {
                                    if (c[0].ToString() == "~")
                                    {
                                        sr.Read(c, 0, c.Length);
                                        while (c[0].ToString() != "|")
                                        {

                                            tempString += c[0];
                                            sr.Read(c, 0, c.Length);
                                        }
                                        break;

                                    }
                                    sr.Read(c, 0, c.Length);
                                }
                                tempBill.AccountBalance = Convert.ToDouble(tempString);
                                break;
                            //RR CURRENT DATE add current date in insertToMdb
                            case "R":
                                while (c[0].ToString() != "|")
                                {
                                    sr.Read(c, 0, c.Length);
                                }
                                break;

                            //SERVICE_ADDRESS
                            case "S":
                                while (c[0].ToString() != "|")
                                {
                                    if (c[0].ToString() == "~")
                                    {
                                        sr.Read(c, 0, c.Length);
                                        while (c[0].ToString() != "|" && c[0].ToString() != "\n")
                                        {

                                            tempString += c[0];
                                            sr.Read(c, 0, c.Length);
                                        }
                                        break;

                                    }
                                    sr.Read(c, 0, c.Length);
                                }
                                tempBill.ServiceAddress = tempString;
                                if (tempString == "") { tempString = "NA"; }
                                InsertToMdbCustomers(fileNamePath, tempCustomer);
                                InsertToMdbBills(fileNamePath, tempBill);

                                break;

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }


            //3. retrieve content and put to Biling.Report.Txt. move to routine if time permits. 
            //below is my code to try to read in the mdb file. 
            //var con = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + fileNamePath);
            /*
            DataTable table = new DataTable("Bills");
            string cmdBills = "SELECT * FROM Bills;";
            string cmdCustomers = "SELECT * FROM Customer;";

            SqlDataReader reader = cmd.ExecuteReader();
            con.Open();
            foreach (DataRow row in table.Rows)
            {
                tempBill.AccountBalance = (double)row["AccountBalance"];
                tempBill.BillAmount = (double)row["BillAmount"];
                Console.WriteLine("test: " + tempBill.AccountBalance);

            }
            con.Close();
            */

            Console.Write("End of Program " + tDate + "\n");
        }

        //this formats the dates from the xml file to MM/dd/yyy
        static string DateFormat(string temp)
        {
            char[] tempMonth = new char[temp.Length];
            char[] tempDayYear = new char[temp.Length];
            string gotMonth = "";
            string gotDayYear = "";
            string switchMan = "";
            using (StringReader sr = new StringReader(temp))
            {
                sr.Read(tempMonth, 0, 3);
                for (int i = 0; i < 3; i++) { switchMan += tempMonth[i]; }
                switch (switchMan)
                {
                    case "Jan":
                        gotMonth = "1";
                        break;
                    case "Feb":
                        gotMonth = "2";
                        break;
                    case "Mar":
                        gotMonth = "3";
                        break;
                    case "Apr":
                        gotMonth = "4";
                        break;
                    case "May":
                        gotMonth = "5";
                        break;
                    case "Jun":
                        gotMonth = "6";
                        break;
                    case "Jul":
                        gotMonth = "7";
                        break;
                    case "Aug":
                        gotMonth = "8";
                        break;
                    case "Sep":
                        gotMonth = "9";
                        break;
                    case "Oct":
                        gotMonth = "10";
                        break;
                    case "Nov":
                        gotMonth = "11";
                        break;
                    case "Dec":
                        gotMonth = "12";
                        break;
                }
            }
            gotDayYear = temp.Substring(temp.Length - 8);
            return gotMonth + gotDayYear.Replace("-", "/");

        }

        //adds A customer to the database
        static void InsertToMdbCustomers(string fileNamePath, Customer customer)
        {
            var con = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + fileNamePath);
            var cmd = new OleDbCommand
            {
                Connection = con,
                //SQL to add the information to the Table
                CommandText = "insert into Customer ([CustomerName],[AccountNumber], [CustomerAddress], [CustomerCity], [CustomerState], [CustomerZip], DateAdded) values( @CustomerName, @AccountNumber, @CustomerAddress, @CustomerCity, @CustomerState, @CustomerZip, @DateAdded);"
            };
            //cmds to add to the Values to Customer Table
           cmd.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
           cmd.Parameters.AddWithValue("@AccountNumber", customer.AccountNumber);
           cmd.Parameters.AddWithValue("@CustomerAddress", customer.CustomerAddress);
           cmd.Parameters.AddWithValue("@CustomerCity", customer.CustomerCity);
           cmd.Parameters.AddWithValue("@CustomerState", customer.CustomerState);
           cmd.Parameters.AddWithValue("@CustomerZip", customer.CustomerZip);
           cmd.Parameters.AddWithValue("@DateAdded", DateTime.Today.ToString("MM/dd/yyyy"));//
           con.Open();
           //execute CommandText
           cmd.ExecuteNonQuery();
           con.Close();

        }

        //adds a bill to the database
        static void InsertToMdbBills(string fileNamePath, Bill bills)
        {
            var con = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + fileNamePath);
            var cmd = new OleDbCommand
            {
                Connection = con,
                //SQL to add the information to the Table
                CommandText = "insert into bills([BillDate], BillNumber, BillAmount, FormatGUID, AccountBalance,  DueDate,FirstEmailDate,SecondEmailDate, DateAdded) values(@BillDate, @BillNumber, @BillAmount, @FormatGUID, @AccountBalance, @DueDate, @FirstEmailDate,@SecondEmailDate, @DateAdded);"
            };
            cmd.Parameters.AddWithValue("@BillDate",bills.BillDate);
            cmd.Parameters.AddWithValue("@BillNumber", bills.BillNumber);
            cmd.Parameters.AddWithValue("@BillAmount", bills.BillAmount);
            cmd.Parameters.AddWithValue("@FormatGUID", bills.FormatGUID);
            cmd.Parameters.AddWithValue("@AccountBalance", bills.AccountBalance);
            cmd.Parameters.AddWithValue("@DueDate", bills.DueDate);
            //cmd.Parameters.AddWithValue("@ServiceAddress", bills.ServiceAddress);
            cmd.Parameters.AddWithValue("@FirstEmailDate", bills.FirstEmailDate);
            cmd.Parameters.AddWithValue("@SecondEmailDate", bills.SecondEmailDate);//bills.SecondEmailDate);
            cmd.Parameters.AddWithValue("@DateAdded", DateTime.Today.ToString("MM/dd/yyyy"));
            //What is theCustomerID? dose it correspond to the Customer ID in Customer table?
            //cmd.Parameters.AddWithValue("@CustomerID",bills.);
            con.Open();
            //execute CommandText
            cmd.ExecuteNonQuery();
            con.Close();
        }

     }
}
