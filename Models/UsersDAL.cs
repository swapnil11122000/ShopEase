using System.Data;
using System.Data.SqlClient;


namespace ECommWeb.Models
{
    public class UsersDAL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        private readonly IConfiguration configuration;
        public UsersDAL(IConfiguration configuration)
        {
            this.configuration = configuration;
            con = new SqlConnection(this.configuration.GetConnectionString("SqlConnection"));
        }


        public List<Users> GetAllUsers()
        {
            List<Users> list = new List<Users>();
            string qry = "select * from Users";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Users user = new Users();
                    user.UserID = Convert.ToInt32(dr["UserID"]);
                    user.UserName = dr["UserName"].ToString();
                    user.Password = dr["Password"].ToString();
                    user.Email = dr["Email"].ToString();

                    user.Mobile = Convert.ToInt32(dr["Mobile"]);
                    user.Gender = dr["Gender"].ToString();

                    list.Add(user);
                }
            }
            con.Close();
            return list;
        }
        //public Users GetUserByID(int id)
        //{

        //    Users users = new Users();

        //    string qry = "select * from users where Id=@UserID";
        //    cmd = new SqlCommand(qry, con);
        //    cmd.Parameters.AddWithValue("@UserID", id);
        //    con.Open();
        //    SqlDataReader reader = cmd.ExecuteReader();

        //    con.Close();
        //    reader.Read();
        //    if (reader.HasRows)
        //    {
        //        users.Id = Convert.ToInt32(reader["Id"]);
        //        users.Email = reader["Email"].ToString();
        //        users.UserName = reader["UserName"].ToString();
        //        users.Password = reader["Password"].ToString();
        //        users.Mobile = Convert.ToInt32(reader["Mobile"]);
        //        users.Gender = reader["Gender"].ToString();
        //        users.Address = reader["Address"].ToString();
        //        users.Role = Convert.ToBoolean(reader["Role"]);
        //        users.IsActive = Convert.ToBoolean(reader["IsActive"]);
        //    }
        //    reader.Close();
        //    return users;

        //}
        public Users GetUserByID(int id)
        {
            Users user = null; // Initialize as null to handle cases where user is not found

           
                string qry = "SELECT * FROM users WHERE UserID = @UserID";
                SqlCommand cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@UserID", id);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read(); // Read the first (and presumably only) row

                    user = new Users
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        Email = reader["Email"].ToString(),
                        UserName = reader["UserName"].ToString(),
                        Password = reader["Password"].ToString(),
                        Mobile = Convert.ToInt32(reader["Mobile"]),
                        Gender = reader["Gender"].ToString(),
                        Address = reader["Address"].ToString(),
                        Role = Convert.ToBoolean(reader["Role"]),
                        IsActive = Convert.ToBoolean(reader["IsActive"])
                    };
                }

                reader.Close(); // Close the reader explicitly
            

            return user;
        }

        public DataTable GetUser(string Email)
        {
            DataTable dataTable = new DataTable();
            string qry = "select * from users where Email=@Email";
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
            string qry = "select * from Vendors where Email=@Email";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@Email", Email);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            dataTable.Load(reader);

            con.Close();

            return dataTable;
        }
        public bool IsUserExists(Users user)
        {
            bool exists = false;

            string qry = "SELECT COUNT(*) FROM users WHERE Email = @Email";

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
        public bool IsVendorExists(Vendors vendors)
        {
            bool exists = false;

            string qry = "SELECT COUNT(*) FROM Vendors WHERE Email = @Email";

            using (SqlConnection con = new SqlConnection(this.configuration.GetConnectionString("SqlConnection")))
            {
                using (SqlCommand cmd = new SqlCommand(qry, con))
                {
                    cmd.Parameters.AddWithValue("@Email", vendors.Email);
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


        public bool ValidateCredentials(Users user)
        {
            bool Validate = false;

            string qry = "select * from users where Email=@Email and Password=@Password";

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
        public bool ValidateVendorCredentials(Vendors vendors)
        {
            bool Validate = false;

            string qry = "select * from Vendors where Email=@Email and Password=@Password";

            using (SqlConnection con = new SqlConnection(this.configuration.GetConnectionString("SqlConnection")))
            {
                using (SqlCommand cmd = new SqlCommand(qry, con))
                {
                    cmd.Parameters.AddWithValue("@Email", vendors.Email);
                    cmd.Parameters.AddWithValue("@Password", vendors.Password);
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
            string qry = "select * from Vendors where Vendor_ID=@UserID";
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
            string qry = "select * from Users where UserID=@UserID";
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
        public int AddVendor(Vendors vendor)
        {
            int result = 0;
            string qry = "insert into Vendors values(@Vendor_Name,@Address1,@Address2,@Email,@Password,@Phone_Number,@IsActive)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@Vendor_Name", vendor.Vendor_Name);
            cmd.Parameters.AddWithValue("@Address1", vendor.Address1 ?? "");
            cmd.Parameters.AddWithValue("@Address2", vendor.Address2 ?? "");

            if (vendor.Phone_Number != 0)
            {
                cmd.Parameters.AddWithValue("@Phone_Number", vendor.Phone_Number);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Phone_Number", 0);
            }

            cmd.Parameters.AddWithValue("@Password", vendor.Password);
            cmd.Parameters.AddWithValue("@IsActive", true);
            cmd.Parameters.AddWithValue("@Email", vendor.Email);



            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public int AddUser(Users user)
        {
            int result = 0;
            string qry = "insert into Users values(@UserName,@Password,@Email,@Mobile,@Gender,@Role,@Address,@IsActive)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@UserName", user.UserName);
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
            cmd.Parameters.AddWithValue("@Address", user.Address ?? "");

            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        //update record
        public int UpdateUser(Users user)
        {
            int result = 0;
            string qry = "update Users set UserName=@UserName,Password=@Password,Email=@Email,Mobile=@Mobile,Gender=@Gender,Address=@Address,Role=@Role,IsActive=@IsActive where UserID=@Id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@UserName", user.UserName);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@Email", user.Email);

            cmd.Parameters.AddWithValue("@Mobile", user.Mobile);
            cmd.Parameters.AddWithValue("@Gender", user.Gender);
            cmd.Parameters.AddWithValue("@Address", user.Address);
            cmd.Parameters.AddWithValue("@Role", user.Role);
            cmd.Parameters.AddWithValue("@IsActive", user.IsActive);
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
            string qry = "delete from Users where UserID=@Id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@Id", Id);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
    }
}
