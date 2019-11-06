using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Test
{
    class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }
        public int SupplierId { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }
    }
    class Program
    {

        public static void ReadProducts()
        {
            SqlConnection con = new SqlConnection(); 
            con.ConnectionString = @"data source =IN5CG9214X5G; database = Test; integrated security = true";

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from tblProduct";
            cmd.Connection = con;
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            Console.WriteLine("PRODUCT\t\tPRODUCT ID");
            Console.WriteLine("-------\t\t----------");
            while (rdr.Read())
            {
                Console.WriteLine($"{rdr[1]}\t\t{rdr[0]}");
            }
            rdr.Close();
            con.Close();

        }

        public static void ReadSupplier(int index)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"data source =IN5CG9214X5G; database = Test; integrated security = true";

            SqlCommand cmd = new SqlCommand(); 
            cmd.CommandText = "select * from tblSupplier where ProdId = @index";
            cmd.Parameters.AddWithValue("index", index);

            cmd.Connection = con;
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            Console.WriteLine("Product ID\t\tSupplier\t\tSupplier ID\t\tLocation\t\tPrice");
            Console.WriteLine("------- --\t\t--------\t\t-------- --\t\t--------\t\t-----");
            while (rdr.Read())
            {
                Console.WriteLine($"{rdr["ProdID"]}\t\t{rdr["Name"]}\t\t{rdr["Id"]}\t\t{rdr["Location"]}\t\t{rdr["Price"]}");
            }
            rdr.Close();
            con.Close();

        }

        public static int RetrievePrice(int prodid, int supplierid)
        {
            int price = 0;

            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"data source =IN5CG9214X5G; database = Test; integrated security = true";

            SqlCommand cmd = new SqlCommand(); 
            cmd.CommandText = "select Price from tblSupplier where ProdId = @p and Id=@s";
            cmd.Parameters.AddWithValue("p", prodid);
            cmd.Parameters.AddWithValue("s", supplierid);
            cmd.Connection = con;
            con.Open();
            price = (int)cmd.ExecuteScalar();
            con.Close();
            return price;

        }

        public static string RetrieveProductName(int prodid)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"data source =IN5CG9214X5G; database = Test; integrated security = true";

            SqlCommand cmd = new SqlCommand(); 
            cmd.CommandText = "select Name from tblProduct where Id = @p ";
            cmd.Parameters.AddWithValue("p", prodid);
            cmd.Connection = con;
            con.Open();
            string name = (string)cmd.ExecuteScalar();
            con.Close();
            return name;
        }

        public static string RetrieveSupplierName(int supplyid)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"data source =IN5CG9214X5G; database = Test; integrated security = true";

            SqlCommand cmd = new SqlCommand(); 
            cmd.CommandText = "select Name from tblSupplier where Id = @s ";
            cmd.Parameters.AddWithValue("s", supplyid);
            cmd.Connection = con;
            con.Open();
            string name = (string)cmd.ExecuteScalar();
            con.Close();
            return name;
        }



        public static void InsertCustomer()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"data source =IN5CG9214X5G; database = Test; integrated security = true";

            SqlCommand cmd = new SqlCommand();
            Customer cust = new Customer();
            Console.WriteLine("Enter Customer ID : ");
            cust.Id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Customer name :");
            cust.Name = Console.ReadLine();

            Console.WriteLine("Select Product Id from list");
            ReadProducts();
            cust.ProductId = int.Parse(Console.ReadLine());
            Console.WriteLine("Select desired Supplier ID");
            ReadSupplier(cust.ProductId);
            cust.SupplierId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter amount of product needed :");
            cust.Quantity = int.Parse(Console.ReadLine());
            int price = RetrievePrice(cust.ProductId, cust.SupplierId);
            cust.Total = cust.Quantity * price;

            cmd.CommandText = "insert into tblCustomer values (@id,@name,@prodid,@supplyid,@quantity,@total)";
            cmd.Parameters.AddWithValue("id", cust.Id);
            cmd.Parameters.AddWithValue("name", cust.Name);
            cmd.Parameters.AddWithValue("prodid", cust.ProductId);
            cmd.Parameters.AddWithValue("supplyid", cust.SupplierId);
            cmd.Parameters.AddWithValue("quantity", cust.Quantity);
            cmd.Parameters.AddWithValue("total", cust.Total);

            cmd.Connection = con;
            con.Open();

            int rowcount = cmd.ExecuteNonQuery();
            if (rowcount > 0)
            {
                Console.WriteLine("Record Inserted Successfully");
            }

            con.Close();

            
        }

        public static void DisplayBill()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"data source =IN5CG9214X5G; database = Test; integrated security = true";

            SqlCommand cmd = new SqlCommand(); 
            cmd.CommandText = "select * from tblCustomer";
            cmd.Connection = con;
            con.Open();

            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Console.WriteLine($"{rdr[0]}\t\t{rdr[1]}\t\t{RetrieveProductName((int)rdr[2])}\t\t{RetrieveSupplierName((int)rdr[3])}\t\t{rdr[4]}\t\t{rdr[5]}");
            }

            con.Close();



        }
        static void DisplayBill(int xId)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"data source =IN5CG9214X5G; database = Test; integrated security = true";

            SqlCommand cmd = new SqlCommand(); 
            cmd.CommandText = "select * from tblCustomer where Id = @id";
            cmd.Parameters.AddWithValue("id", xId);
            cmd.Connection = con;
            con.Open();

            SqlDataReader rdr = cmd.ExecuteReader();
            

            while (rdr.Read())
            {
                Console.WriteLine($"_______________BILL for {rdr["Name"]}_____________");
                Console.WriteLine($"******Product  : {RetrieveProductName((int)rdr[2])}******");
                Console.WriteLine($"******Sold By  : {RetrieveSupplierName((int)rdr[3])}*****");
                Console.WriteLine($"******Quantity : {rdr["Quantity"]}*************");
                Console.WriteLine($"******Price    : {RetrievePrice((int)rdr[2], (int)rdr[3])}******");
                Console.WriteLine($"******Total    : {rdr[4]} X {RetrievePrice((int)rdr[2], (int)rdr[3])} = {rdr[5]}******");
               
            }

            con.Close();


        }
        static void Main(string[] args)
        {
            int y = 0;
            int choice;
            do
            {
                Console.WriteLine("1. New Customer Entry \t\t2. All Customer Details\t\t3.Customer Bill by ID");
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        InsertCustomer();
                        Console.ReadLine();
                        break;
                    case 2:
                        DisplayBill();
                        Console.ReadLine();
                        break;
                    case 3:
                        Console.WriteLine("Enter Customer Id to view bill");
                        int id = int.Parse(Console.ReadLine());
                        DisplayBill(id);
                        Console.ReadLine();
                        break;
                    default: Console.WriteLine("invalid"); break;
                }

                Console.WriteLine("Enter 1 to Continue 0 to exit");
                y = int.Parse(Console.ReadLine());
                if (y == 0)
                    Environment.Exit(1);
            } while (y == 1);

            InsertCustomer();
            Console.ReadLine();
            DisplayBill();
            Console.ReadLine();

        }
    }

}
