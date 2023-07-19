using Microsoft.Data.SqlClient;

namespace ROI_STAFF_APP.Data
{
        public class Repository
        {
            #region Connection String


            private string _connectionString;
            public Repository()
            {
                //AZURE
                _connectionString = "Data Source=ronan123456789.database.windows.net;Initial Catalog=ROI_Staff;User ID=azureuser;Password=R0n4n2006;Connect Timeout=60;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

                //SSMS
                //_connectionString = @"Data Source=DESKTOP-TBL2MHJ;Initial Catalog=StoreDB;Integrated Security=True;Trust Server Certificate=True";

            }
        #endregion

        #region Get Account
        public IEnumerable<Account> GetAccount(bool isVerified = false)
        {
            List<Account> accounts = new List<Account>();

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand())
            {
                command.CommandText = "SELECT * FROM Account WHERE isVerified = @isVerified";
                command.Parameters.AddWithValue("@isVerified", isVerified);
                command.Connection = connection;
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Map the reader data to Account object
                        Account account = new Account
                        {
                            staffID = Convert.ToInt32(reader["staffID"]),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            FullName = reader["FirstName"].ToString() + " " + reader["LastName"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString(),
                            Role = reader["Role"].ToString(),
                            userEmail = reader["UserEmail"].ToString(),
                            password = reader["Password"].ToString(),
                            isVerified = Convert.ToBoolean(reader["isVerified"])
                        };

                        accounts.Add(account);
                    }
                }
            }

            return accounts;
        }
        #endregion

        #region Verify account
        public int UpdateAccountVerification(int staffID)
        {
            int result = 0;
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand())
            {
                command.CommandText = "UPDATE Account SET isVerified=@IsVerified WHERE staffID=@StaffID";
                command.Parameters.AddWithValue("@IsVerified", true); // Set isVerified to true
                command.Parameters.AddWithValue("@StaffID", staffID);
                command.Connection = connection;
                connection.Open();
                result = command.ExecuteNonQuery();
            }
            return result;
        }
        #endregion

        #region Update Account
        public int UpdateAccount(Account p)
        {
            int result = 0;
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand())
            {
                command.CommandText = "UPDATE Account SET FirstName=@FirstName,LastName=@LastName,PhoneNumber=@PhoneNumber Role=@Role UserEmail=@UserEmail Password=@Password isVerified=@isVerified WHERE staffID=@staffID";
                command.Parameters.AddWithValue("@FirstName", p.FirstName);
                command.Parameters.AddWithValue("@LastName", p.LastName);
                command.Parameters.AddWithValue("@PhoneNumber", p.PhoneNumber);
                command.Parameters.AddWithValue("@Role", p.Role);
                command.Parameters.AddWithValue("@UserEmail", p.userEmail);
                command.Parameters.AddWithValue("@Password", p.password);
                command.Parameters.AddWithValue("@isVerified", p.isVerified);
                command.Connection = connection;
                connection.Open();
                result = command.ExecuteNonQuery();
            }
            return result;
        }
        #endregion

        #region Insert Account
        public int InsertAccount(Account p)
            {
                int result = 0;
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand())
                {
                    command.CommandText = "INSERT INTO Account VALUES (@FirstName,@LastName,@PhoneNumber,@Role,@UserEmail,@Password,@isVerified)";
                    //command.Parameters.AddWithValue("@id", p.Id);
                    command.Parameters.AddWithValue("@FirstName", p.FirstName);
                    command.Parameters.AddWithValue("@LastName", p.LastName);
                    command.Parameters.AddWithValue("@PhoneNumber", p.PhoneNumber);
                    command.Parameters.AddWithValue("@Role", p.Role);
                    command.Parameters.AddWithValue("@UserEmail", p.userEmail);
                    command.Parameters.AddWithValue("@Password", p.password);
                    command.Parameters.AddWithValue("@isVerified", p.isVerified);
                    command.Connection = connection;
                    connection.Open();
                    result = command.ExecuteNonQuery();
                }
                return result;
            }
            #endregion

            #region Delete Account
            public int DeleteAccount(int staffID)
            {
                int result = 0;
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand())
                {
                    command.CommandText = "DELETE FROM Account WHERE staffID=@staffID";
                    command.Parameters.AddWithValue("@staffID", staffID);
                    command.Connection = connection;
                    connection.Open();
                    result = command.ExecuteNonQuery();
                }
                return result;
            }
            #endregion
        }
}


