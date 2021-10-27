using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

// ExecuteNonQuery: Used to execute a command that will not return any data, for example Insert, update or delete.
// ExecuteReader: Used to execute a command that will return 0 or more records, for example Select.
// ExecuteScalar: Used to execute a command that will return only 1 value, for example Select Count(*).
class DBConnect
{
    public static MySqlConnection connection;
    public static string server;
    public static string database;
    public static string uid;
    public static string password;

    //Constructor
    public static void DBCon()
    {
        Initialize();
    }

    //Initialize values
    public  static void Initialize()
    {
        server = FileVariables.hostport;
        database = FileVariables.DataBase;
        uid = FileVariables.User;
        password = FileVariables.Password;
        string connectionString;
        connectionString = "SERVER=" + server + ";" + "DATABASE=" +
        database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

        connection = new MySqlConnection(connectionString);
    }

    //open connection to database
    public static bool OpenConnection()
    {
        try
        {
            connection.Open();
            return true;
        }
        catch (MySqlException ex)
        {
            //MessageBox.Show(ex.Message);
            WS.MSG = ex.Message;
            return false;
        }
    }

    //Close connection
    public static bool CloseConnection()
    {
        try
        {
            connection.Close();
            return true;
        }
        catch (MySqlException ex)
        {
            MessageBox.Show(ex.Message);
            return false;
        }
    }

    //Insert statement
    public static bool Insert(string query)
    {
        try
        {
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (MySqlException ex)
        {
            //MessageBox.Show(ex.Message);
            WS.MSG = ex.Message;
            return false;
        }
    }

    //Update statement
    public static bool Update(string query)
    {
        try
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = query;
            cmd.Connection = connection;

            cmd.ExecuteNonQuery();
            return true;
        }
        catch (MySqlException ex)
        {
            //MessageBox.Show(ex.Message);
            WS.MSG = ex.Message;
            return false;
        }
    }

    //Delete statement
    public static bool Delete(string query)
    {
        try
        {
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (MySqlException ex)
        {
            //MessageBox.Show(ex.Message);
            WS.MSG = ex.Message;
            return false;
        }
    }

    //Select statement
    public static bool Select(string query, out DataTable table)
     {
        
        // still working on this -- need to test
        try
        {
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
            dataAdapter.SelectCommand = new MySqlCommand(query, connection);
            DataTable table1 = new DataTable();
            dataAdapter.Fill(table1);

            table = table1;
            return true;
        }
        catch (MySqlException ex)
        {
            //MessageBox.Show(ex.Message);
            WS.MSG = ex.Message;
            table = new DataTable();
            return false;
        }
    }

    //Count statement
    public static int Count()
    {
        return 0;
    }

    //Backup
    public static void Backup()
    {
    }

    //Restore
    public static void Restore()
    {
    }
}