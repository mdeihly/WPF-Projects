using System.Xml.Linq;
using System.Configuration;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using DataLibrary.Models;

namespace ConsoleApp;
class Program
{
    static void Main(string[] args)
    {
        // mapMultipleObjects();
        //multipleObjectsWithParameter("John");
        //multipleSets();
        //outputParameters("John", "Cena");
        runWithTransaction("Abd", "Leo");
    }

    public static string getConnection(string connection = "DefaultConnection")
    {
        return ConfigurationManager.ConnectionStrings[connection]?.ConnectionString
                ?? throw new ArgumentException($"Connection string '{connection}' not found."); ;
    }

    public static void mapMultipleObjects()
    {
        using (IDbConnection cnn = new SqlConnection(getConnection()))
        {
            string sql = @"select pe.*, ph.* from dbo.Person pe
                          left join dbo.Phone ph
                          on pe.CellPhoneId = ph.Id";

            var people = cnn.Query<FullPersonModel, PhoneModel, FullPersonModel>(sql,
                (person, phone) => { person.CellPhone = phone; return person; });

            foreach (var person in people)
            {
                Console.WriteLine($"{person.FirstName} {person.LastName}: Cell: {person.CellPhone?.PhoneNumber}");
            }
        }
    }

    public static void multipleObjectsWithParameter(string lastname)
    {
        using (IDbConnection cnn = new SqlConnection(getConnection()))
        {
            // Use an anonymous object to pass parameters
            var p = new
            {
                LastName = lastname
            };

            string sql = @"select pe.*, ph.* from dbo.Person pe
                          left join dbo.Phone ph
                          on pe.CellPhoneId = ph.Id
                          where LastName = @LastName";

            // Map complex query results to a custom object
            var people = cnn.Query<FullPersonModel, PhoneModel, FullPersonModel>(sql,
                map: (person, phone) => { person.CellPhone = phone; return person; }, p);

            foreach (var person in people)
            {
                Console.WriteLine($"{person.FirstName} {person.LastName}: Cell: {person.CellPhone?.PhoneNumber}");
            }
        }
    }

    public static void multipleSets()
    {
        // get different tables

        using (IDbConnection cnn = new SqlConnection(getConnection()))
        {

            string sql = @"select * from dbo.Person
                          select * from dbo.Phone";

            List<PersonModel> people = null;
            List<PhoneModel> phones = null;

            using (var lists = cnn.QueryMultiple(sql))
            {
                people = lists.Read<PersonModel>().ToList();
                phones = lists.Read<PhoneModel>().ToList();
            }

            foreach (var person in people)
            {
                Console.WriteLine($"{person.FirstName} {person.LastName}");
            }

            foreach (var phone in phones)
            {
                Console.WriteLine($"Phone Number: {phone.PhoneNumber}");
            }
        }
    }

    public static void outputParameters(string firstname, string lastname)
    {
        using (IDbConnection cnn = new SqlConnection(getConnection()))
        {
            var parameters = new DynamicParameters();

            if (!string.IsNullOrEmpty(firstname) &&
                !string.IsNullOrEmpty(lastname))
            {
                parameters.Add("@Id", 0, DbType.Int32, ParameterDirection.Output);
                parameters.Add("@FirstName", firstname);
                parameters.Add("@LastName", lastname);
            }

            string sql = @"Insert into dbo.Person (FirstName, LastName)
                        values (@FirstName, @LastName);
                        select @Id = @@Identity";

            cnn.Execute(sql, parameters);

            int newId = parameters.Get<int>("@Id");

            Console.WriteLine($"The new Id is {newId}");

        }
    }

    public static void runWithTransaction(string firstname, string lastname)
    {
        using (IDbConnection cnn = new SqlConnection(getConnection()))
        {
            var parameters = new
            {
                FirstName = firstname,
                LastName = lastname
            };

            // open the connection
            cnn.Open();

            using (var trans = cnn.BeginTransaction())
            {
                try
                {
                    // Step 1
                    string sql = @"insert into dbo.Person (FirstName, LastName)
                           values (@FirstName, @LastName)";

                    int recordUpdated = cnn.Execute(sql, parameters, trans);

                    Console.WriteLine($"Records updated: {recordUpdated}");

                    // Step 2
                    // example of Rollback
                    //cnn.Execute("update dbo.Person set Id = 1", transaction: trans);
                    cnn.Execute("update dbo.Person set LastName = '1'", transaction: trans);


                    // Commit the transaction
                    trans.Commit();
                    Console.WriteLine("Transaction committed successfully!");
                }
                catch (Exception ex)
                {
                    // Roll back the transaction on error
                    trans.Rollback();
                    Console.WriteLine($"Transaction rolled back due to an error: {ex.Message}");
                }
            }
        }
    }

}


