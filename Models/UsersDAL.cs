using System.Data;
using System.Data.SqlClient;


namespace ECommWeb.Models
{
    public class UserDAL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        private readonly IConfiguration configuration;
        public UserDAL(IConfiguration configuration)
        {
            this.configuration = configuration;
            con = new SqlConnection(this.configuration.GetConnectionString("SqlConnection"));
        }


        public List<User> GetAllUser()
        {
            List<User> list = new List<User>();
            string qry = "select * from User";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    User user = new User();
                    user.UserID = Convert.ToInt32(dr["UserID"]);
                    user.FirstName = dr["FirstName"].ToString();
                    user.LastName = dr["LastName"].ToString();
                    user.Password = dr["Password"].ToString();
                    user.Email = dr["Email"].ToString();

                    user.Mobile = (dr["Mobile"]).ToString();
                    user.Gender = dr["Gender"].ToString();

                    list.Add(user);
                }
            }
            con.Close();
            return list;
        }
       
        public User GetUserByID(int id)
        {
            User user = null; // Initialize as null to handle cases where user is not found

           
                string qry = "SELECT * FROM User WHERE UserID = @UserID";
                SqlCommand cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@UserID", id);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read(); // Read the first (and presumably only) row

                    user = new User
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        Email = reader["Email"].ToString(),
                        FirstName = reader["FirstName"].ToString(),
                        LastName= reader["LastName"].ToString(),
                        Password = reader["Password"].ToString(),
                        Mobile = reader["Mobile"].ToString(),
                        Status= Convert.ToBoolean(reader["Status"].ToString()),



                    };
                }

                reader.Close(); 

            return user;
        }

        public DataTable GetUser(string Email)
        {
            DataTable dataTable = new DataTable();
            string qry = "select * from User where Email=@Email";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@Email", Email);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            dataTable.Load(reader);

            con.Close();

            return dataTable;
        }
        public DataTable GetVendor(string Email)
        {
            DataTable dataTable = new DataTable();
            string qry = "select * from Vendor where Email=@Email";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@Email", Email);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            dataTable.Load(reader);

            con.Close();

            return dataTable;
        }
        public bool IsUserExists(User user)
        {
            bool exists = false;

            string qry = "SELECT COUNT(*) FROM User WHERE Email = @Email";

            using (SqlConnection con = new SqlConnection(this.configuration.GetConnectionString("SqlConnection")))
            {
                using (SqlCommand cmd = new SqlCommand(qry, con))
                {
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    try
                    {
                        con.Open();
                        int count = (int)cmd.ExecuteScalar();

                        if (count > 0)
                        {
                            exists = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error in IsUserExists: " + ex.Message);
                    }
                }
            }

            return exists;
        }
        public bool IsVendorExists(Vendor Vendor)
        {
            bool exists = false;

            string qry = "SELECT COUNT(*) FROM Vendor WHERE Email = @Email";

            using (SqlConnection con = new SqlConnection(this.configuration.GetConnectionString("SqlConnection")))
            {
                using (SqlCommand cmd = new SqlCommand(qry, con))
                {
                    cmd.Parameters.AddWithValue("@Email", Vendor.Email);
                    try
                    {
                        con.Open();
                        int count = (int)cmd.ExecuteScalar();

                        if (count > 0)
                        {
                            exists = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error in IsUserExists: " + ex.Message);
                    }
                }
            }

            return exists;
        }


        public bool ValidateCredentials(User user)
        {
            bool Validate = false;

            string qry = "select * from User where Email=@Email and Password=@Password";

            using (SqlConnection con = new SqlConnection(this.configuration.GetConnectionString("SqlConnection")))
            {
                using (SqlCommand cmd = new SqlCommand(qry, con))
                {
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    try
                    {
                        con.Open();
                        int count = 0;
                        var obj = cmd.ExecuteScalar();
                        if (obj != null)
                        {
                            count = 1;

                        }


                        if (count > 0)
                        {
                            Validate = true;

                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error in IsUserExists: " + ex.Message);
                    }
                }
            }
            con.Close();
            return Validate;

        }
        public bool ValidateVendorCredentials(Vendor Vendor)
        {
            bool Validate = false;

            string qry = "select * from Vendor where Email=@Email and Password=@Password";

            using (SqlConnection con = new SqlConnection(this.configuration.GetConnectionString("SqlConnection")))
            {
                using (SqlCommand cmd = new SqlCommand(qry, con))
                {
                    cmd.Parameters.AddWithValue("@Email", Vendor.Email);
                    cmd.Parameters.AddWithValue("@Password", Vendor.Password);
                    try
                    {
                        con.Open();
                        int count = 0;
                        var obj = cmd.ExecuteScalar();
                        if (obj != null)
                        {
                            count = 1;

                        }


                        if (count > 0)
                        {
                            Validate = true;

                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error in IsUserExists: " + ex.Message);
                    }
                }
            }
            con.Close();
            return Validate;

        }
        public bool ValidateData(int UserID)
        {
            bool Present = true;
            DataTable dataTable = new DataTable();
            string qry = "select * from Vendor where Vendor_ID=@UserID";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            dataTable.Load(reader);

            con.Close();
            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    foreach (DataColumn col in dataTable.Columns)
                    {
                        object value = dr[col.ColumnName];
                        if (value == DBNull.Value || string.IsNullOrWhiteSpace(value.ToString()) || value.ToString() == "0")
                        {
                            Present = false;

                        }
                    }
                }
            }
            return Present;
        }
        public bool ValidateUserData(int UserID)
        {
            bool Present = true;
            DataTable dataTable = new DataTable();
            string qry = "select * from User where UserID=@UserID";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            dataTable.Load(reader);

            con.Close();
            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    foreach (DataColumn col in dataTable.Columns)
                    {
                        object value = dr[col.ColumnName];
                        if (value == DBNull.Value || string.IsNullOrWhiteSpace(value.ToString()) || value.ToString() == "0")
                        {
                            Present = false;

                        }
                    }
                }
            }
            return Present;
        }
        public int AddVendor(Vendor vendor)
        {
            int result = 0;
            string qry = "insert into Vendor values(@Vendor_Name,@Address1,@Address2,@Email,@Password,@Phone_Number,@IsActive)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@VendorName", vendor.ContactPerson);
            cmd.Parameters.AddWithValue("@Address1", vendor.AddressID);
           

         
            cmd.Parameters.AddWithValue("@Password", vendor.Password);
            cmd.Parameters.AddWithValue("@IsActive", true);
            cmd.Parameters.AddWithValue("@Email", vendor.Email);



            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public int AddUser(User user)
        {
            int result = 0;
            string qry = "insert into User values(@UserName,@Password,@Email,@Mobile,@Gender,@Role,@Address,@IsActive)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@UserName", user.FirstName);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@Email", user.Email);

            if (user.Mobile != null)
            {
                cmd.Parameters.AddWithValue("@Mobile", user.Mobile);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Mobile", "");
            }


           

            
            cmd.Parameters.AddWithValue("@Gender", user.Gender??"");


            cmd.Parameters.AddWithValue("@IsActive", true);

            cmd.Parameters.AddWithValue("@Role",0);
            cmd.Parameters.AddWithValue("@Address", user.AddressID);

            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        //update record
        public int UpdateUser(User user)
        {
            int result = 0;
            string qry = "update User set UserName=@UserName,Password=@Password,Email=@Email,Mobile=@Mobile,Gender=@Gender,Address=@Address,Role=@Role,IsActive=@IsActive where UserID=@Id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@UserName", user.FirstName);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@Email", user.Email);

            cmd.Parameters.AddWithValue("@Mobile", user.Mobile);
            cmd.Parameters.AddWithValue("@Gender", user.Gender);
            cmd.Parameters.AddWithValue("@Address", user.Address);
            cmd.Parameters.AddWithValue("@Role", user.ProfileID);
            cmd.Parameters.AddWithValue("@IsActive", user.Status);
            cmd.Parameters.AddWithValue("@Id", user.UserID);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        // delete record
        public int DeleteUser(int Id)
        {
            int result = 0;
            string qry = "delete from User where UserID=@Id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@Id", Id);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
    }
}
