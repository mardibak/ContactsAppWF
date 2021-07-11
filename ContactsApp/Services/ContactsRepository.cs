using System.Data;
using System.Data.SqlClient;

namespace ContactsApp
{
    class ContactsRepository : IContactRepository

    {
        // private string connectionString = @"Data Source=MARDi\SQL2019;Initial Catalog=Contact_DB;User ID=sa;Password=9869;Integrated Security=True";
        private string connectionString = @"Data Source=.;Initial Catalog=Contact_DB;Integrated Security=True;";

        //bool IContactRepository.Delete(int contactId)
        // we can use (up line) interface name in class or not use (down line - active line)
        public bool Delete(int contactId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                string query = "Delete From MyContacts where ContactID = @ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", contactId);
                connection.Open();
                command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public bool Insert(string name, string family, int age, string mobile, string email, string address)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {

                string query = "Insert Into MyContacts (Name,Family,Age,Mobile,Email,Address) values (@Name,@Family,@Age,@Mobile,@Email,@Address)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Family", family);
                command.Parameters.AddWithValue("@Age", age);
                command.Parameters.AddWithValue("@Mobile", mobile);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Address", address);
                connection.Open();
                command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public DataTable SelectAll()
        {
            string query = "Select * From MyContacts";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);
            return data;
        }
        public DataTable Search(string parameter)
        {
            string query = "Select * From MyContacts where Name like @parameter or Family like @parameter";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            adapter.SelectCommand.Parameters.AddWithValue("@parameter", "%" + parameter + "%");
            DataTable data = new DataTable();
            adapter.Fill(data);
            return data;
        }
        DataTable IContactRepository.SelectRow(int contactId)
        {
            string query = "Select * From MyContacts where ContactID=" + contactId;
            SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);
            return data;
        }

        public bool Update(int contactId, string name, string family, int age, string mobile, string email, string address)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                string query = "Update MyContacts Set Name=@Name,Family=@Family,Age=@Age,Mobile=@Mobile,Email=@Email,Address=@Address where ContactID=@ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", contactId);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Family", family);
                command.Parameters.AddWithValue("@Age", age);
                command.Parameters.AddWithValue("@Mobile", mobile);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Address", address);
                connection.Open();
                command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
