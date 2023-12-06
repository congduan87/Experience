using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.GiamKichSan.Common.SQL
{
    public class BaseSQLConnection
    {
        private SqlConnection _DbConnection { get; set; }
        public SqlConnection DbConnection
        {
            get
            {
                if (_DbConnection.State == ConnectionState.Closed)
                    _DbConnection.Open();
                return _DbConnection;
            }
        }
        public BaseSQLConnection(string connectionString)
        {
            if (_DbConnection == null)
                _DbConnection = new SqlConnection(connectionString);
            if (_DbConnection.State == ConnectionState.Closed)
                _DbConnection.Open();
        }

        ~BaseSQLConnection()
        {
            if (_DbConnection == null && _DbConnection.State != ConnectionState.Closed)
                _DbConnection.Close();

            _DbConnection.Dispose();
        }

    }
}
