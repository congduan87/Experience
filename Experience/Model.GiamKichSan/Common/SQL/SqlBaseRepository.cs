using Model.GiamKichSan.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Model.GiamKichSan.Common.SQL
{
    public class SqlBaseRepository
    {
        #region DBTransaction
        private static SqlConnection connection;
        internal string connectionString { get; set; }
        private bool m_Disposed = false;

        protected SqlBaseRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        ~SqlBaseRepository()
        {
            Dispose();
        }

        protected void Dispose()
        {
            Dispose();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!m_Disposed)
            {
                if (disposing)
                {
                    // các đối tượng có Dispose gọi ở đây
                }

                // giải phóng các tài nguyên không quản lý được cửa lớp (unmanaged)

                m_Disposed = true;
            }
        }

        protected SqlConnection DbConnection
        {
            get
            {
                if (connection == null) connection = new SqlConnection(connectionString);
                if (connection.State == ConnectionState.Closed) connection.Open();
                return connection;
            }
        }

        protected ResObject<bool> ExecuteWithTransaction(Func<IDbTransaction, bool> func)
        {
            ResObject<bool> output = new ResObject<bool>() { obj = false };
            using (IDbTransaction dbTransaction = DbConnection.BeginTransaction())
            {
                try
                {
                    bool b = func.Invoke(dbTransaction);
                    if (b) dbTransaction.Commit();
                    else dbTransaction.Rollback();

                    output.obj = b;
                }
                catch (Exception ex)
                {
                    dbTransaction.Rollback();

                    output.codeError = "99";
                    output.strError = string.Format("SqlBaseRepository_ExecuteWithTransaction: ", ex.ToString());
                }
                return output;
            }
        }

        private void CombiCommandO(SqlCommand cmd, object[] paramsValue = null)
        {
            if (paramsValue != null && paramsValue.Length > 0)
            {
                foreach (var param in paramsValue)
                {
                    var sqlParam = new SqlParameter();
                    sqlParam.Value = param;
                    if (param.GetType().Name.Equals("Byte[]"))
                    {
                        sqlParam.SqlDbType = SqlDbType.VarBinary;
                    }
                    cmd.Parameters.Add(sqlParam);
                }
            }
        }

        private void CombiCommandD(SqlCommand cmd, Dictionary<string, object> paramsValue = null)
        {
            if (paramsValue != null && paramsValue.Count > 0)
            {
                foreach (var param in paramsValue)
                {
                    var sqlParam = new SqlParameter(param.Key, param.Value);

                    if (param.GetType().Name.Equals("Byte[]"))
                    {
                        sqlParam.SqlDbType = SqlDbType.VarBinary;
                    }
                    cmd.Parameters.Add(sqlParam);
                }
            }
        }

        protected virtual IDataReader GetDataReader(string commandText, object[] paramsValue = null)
        {
            try
            {
                var cmd = new SqlCommand(commandText, DbConnection);
                CombiCommandO(cmd, paramsValue);
                cmd.CommandType = CommandType.Text;

                return cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}:{1}", "GetDataReader", ex.ToString());
                return null;
            }
        }

        protected virtual IDataReader GetDataReader(string commandText, Dictionary<string, object> paramsValue = null)
        {
            try
            {
                var cmd = new SqlCommand(commandText, DbConnection);
                CombiCommandD(cmd, paramsValue);
                cmd.CommandType = CommandType.Text;

                return cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}:{1}", "GetDataReader", ex.ToString());
                return null;
            }
        }
        protected virtual DataTable GetDataTable(string commandText, object[] paramsValue = null)
        {
            try
            {
                var cmd = new SqlCommand(commandText, DbConnection);
                CombiCommandO(cmd, paramsValue);
                cmd.CommandType = CommandType.Text;
                DataTable dt = new DataTable();
                SqlDataAdapter dataAdap = new SqlDataAdapter(cmd);
                dataAdap.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}:{1}", "GetDataTable", ex.ToString());
                return null;
            }
        }

        protected virtual DataTable GetDataTable(string commandText, Dictionary<string, object> paramsValue = null)
        {
            try
            {
                var cmd = new SqlCommand(commandText, DbConnection);
                CombiCommandD(cmd, paramsValue);
                cmd.CommandType = CommandType.Text;

                DataTable dt = new DataTable();
                SqlDataAdapter dataAdap = new SqlDataAdapter(cmd);
                dataAdap.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}:{1}", "GetDataTable", ex.ToString());
                return new DataTable();
            }
        }

        protected bool ExecuteNonQuery(string commandText, object[] paramsValue = null)
        {
            try
            {
                var cmd = new SqlCommand(commandText, DbConnection);
                CombiCommandO(cmd, paramsValue);
                cmd.CommandType = CommandType.Text;

                int result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}:{1}", "ExecuteNonQuery", ex.ToString());
                return false;
            }
        }

        protected bool ExecuteNonQuery(string commandText, Dictionary<string, object> paramsValue = null)
        {
            try
            {
                var cmd = new SqlCommand(commandText, DbConnection);
                CombiCommandD(cmd, paramsValue);
                cmd.CommandType = CommandType.Text;

                int result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}:{1}", "ExecuteNonQuery", ex.ToString());
                return false;
            }
        }

        protected object ExecuteScalar(string commandText, object[] paramsValue = null)
        {
            try
            {
                var cmd = new SqlCommand(commandText, DbConnection);
                CombiCommandO(cmd, paramsValue);
                cmd.CommandType = CommandType.Text;

                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}:{1}", "ExecuteScalar", ex.ToString());
                return null;
            }
        }

        protected object ExecuteScalar(string commandText, Dictionary<string, object> paramsValue = null)
        {
            try
            {
                var cmd = new SqlCommand(commandText, DbConnection);
                CombiCommandD(cmd, paramsValue);
                cmd.CommandType = CommandType.Text;

                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}:{1}", "ExecuteScalar", ex.ToString());
                return null;
            }
        }

        protected bool ExecuteStoreNonQuery(string procName, object[] paramsValue)
        {
            try
            {
                var cmd = new SqlCommand(procName, DbConnection);
                CombiCommandO(cmd, paramsValue);
                cmd.CommandType = CommandType.StoredProcedure;

                int result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}:{1}", "ExecuteStoreNonQuery", ex.ToString());
                return false;
            }
        }

        protected bool ExecuteStoreNonQuery(string procName, Dictionary<string, object> paramsValue)
        {
            try
            {
                var cmd = new SqlCommand(procName, DbConnection);
                CombiCommandD(cmd, paramsValue);
                cmd.CommandType = CommandType.StoredProcedure;

                int result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}:{1}", "ExecuteStoreNonQuery", ex.ToString());
                return false;
            }
        }

        #endregion
    }

    public class SqlBaseRepository<T> : SqlBaseRepository
    {
        #region Protected Methods
        private string _SkipTake
        {
            get
            {
                return "ORDER BY {0} OFFSET {1} ROWS FETCH NEXT {2} ROWS ONLY";
            }
        }
        protected string GetTableName()
        {
            string tableName = typeof(T).Name;
            var customAttributes = typeof(T).GetCustomAttributes(typeof(TableAttribute), false);
            if (customAttributes.Count() > 0 && !string.IsNullOrEmpty((customAttributes.First() as TableAttribute).Name))
            {
                tableName = (customAttributes.First() as TableAttribute).Name;
            }
            return tableName;
        }
        protected readonly string paramPrefix = "@";

        public SqlBaseRepository(string connectionString) : base(connectionString)
        {
        }

        protected IEnumerable<PropertyInfo> GetListProperty(Type typeName)
        {
            var propDetail = typeName.GetProperties()
                .Where(p => !p.GetCustomAttributes(typeof(NotMappedAttribute), true).Any()
                && !p.GetCustomAttributes<DatabaseGeneratedAttribute>().Any()
                );
            return propDetail;
        }

        protected IEnumerable<PropertyInfo> GetAllProperty(Type typeName)
        {
            var propDetail = typeName.GetProperties();
            return propDetail;
        }

        protected ResObject<T> Insert(object entity, IDbTransaction dbTransaction = null)
        {
            ResObject<T> output = new ResObject<T>();
            try
            {
                string tableName = GetTableName();
                string body = string.Empty;
                string values = string.Empty;

                Dictionary<string, object> parameters = new Dictionary<string, object>();
                foreach (var item in GetListProperty(entity.GetType()))
                {
                    object value = item.GetValue(entity);
                    if (value == null) continue;
                    parameters.Add(item.Name, value);

                    if (!string.IsNullOrEmpty(body)) body += ", ";
                    body += item.Name;

                    if (!string.IsNullOrEmpty(values)) values += ", ";
                    values += paramPrefix + item.Name;
                }

                string commandText = "INSERT INTO " + tableName + " (" + body + ") VALUES (" + values + ")";

                var insertOut = ExecuteNonQuery(commandText, parameters);
                if (!insertOut)
                    output.obj = Activator.CreateInstance<T>();
                else
                {
                    commandText = "SELECT * FROM " + tableName;
                    body = " WHERE ";
                    parameters = new Dictionary<string, object>();

                    foreach (var item in GetListProperty(entity.GetType()))
                    {
                        object value = item.GetValue(entity);
                        if (value == null) continue;
                        parameters.Add(item.Name, value);

                        if (body != " WHERE ") body += " AND ";
                        body += item.Name + "=" + paramPrefix + item.Name;
                    }

                    output.obj = GetDataTable(commandText + body, parameters).GetFirst<T>();
                }
            }
            catch (Exception ex)
            {
                output.codeError = "99";
                output.strError = string.Format("SqlBaseRepository_Insert: ", ex.ToString());
            }

            DbConnection.Close();
            return output;
        }

        protected ResObject<bool> Insert(List<object> entities, IDbTransaction dbTransaction = null)
        {
            ResObject<bool> output = new ResObject<bool>() { obj = false };
            try
            {
                string tableName = GetTableName();
                string body = string.Empty;
                string values = string.Empty;
                string commandText = string.Empty;

                foreach (var entity in entities)
                {
                    Dictionary<string, object> parameters = new Dictionary<string, object>();
                    foreach (var item in GetListProperty(entity.GetType()))
                    {
                        object value = item.GetValue(entity);
                        if (value == null) continue;
                        parameters.Add(item.Name, value);

                        if (!string.IsNullOrEmpty(body)) body += ", ";
                        body += item.Name;

                        if (!string.IsNullOrEmpty(values)) values += ", ";
                        values += paramPrefix + item.Name;
                    }

                    commandText = "INSERT INTO " + tableName + " (" + body + ") VALUES (" + values + ")";

                    output.obj = ExecuteNonQuery(commandText, parameters);

                    if (!Convert.ToBoolean(output.obj))
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                output.codeError = "99";
                output.strError = string.Format("SqlBaseRepository_Insert: ", ex.ToString());
            }

            DbConnection.Close();
            return output;
        }

        protected ResObject<T> Update(object entity, Expression<Func<T, bool>> conditions, IDbTransaction dbTransaction = null)
        {
            ResObject<T> output = new ResObject<T>();
            try
            {
                string tableName = GetTableName();
                string set = string.Empty;
                string clause = string.Empty;

                Dictionary<string, object> parameters = new Dictionary<string, object>();
                foreach (var item in GetListProperty(entity.GetType()))
                {
                    object value = item.GetValue(entity);
                    if (value == null) continue;
                    parameters.Add(item.Name, value);

                    if (!string.IsNullOrEmpty(set)) set += ", ";
                    set += item.Name + " = " + paramPrefix + item.Name;
                }

                clause = conditions.Body.GetKeyValue(parameters, paramPrefix);

                string commandText = "UPDATE " + tableName + " SET " + set + " WHERE " + clause;
                var editOut = ExecuteNonQuery(commandText, parameters);

                if (!editOut)
                    output.obj = Activator.CreateInstance<T>();
                else
                {
                    commandText = "SELECT * FROM " + tableName + " WHERE " + clause;
                    parameters = new Dictionary<string, object>();
                    clause = conditions.Body.GetKeyValue(parameters, paramPrefix);
                    output.obj = GetDataTable(commandText, parameters).GetFirst<T>();
                }
            }
            catch (Exception ex)
            {
                output.codeError = "99";
                output.strError = string.Format("SqlBaseRepository_Update: ", ex.ToString());
            }

            DbConnection.Close();
            return output;
        }

        protected ResObject<bool> UpdateWhere<TKey>(object entity, Expression<Func<T, TKey>> keys, IDbTransaction dbTransaction = null)
        {
            ResObject<bool> output = new ResObject<bool>() { obj = false };
            try
            {
                string tableName = GetTableName();
                string set = string.Empty;
                string clause = string.Empty;
                List<string> where = keys.Body.GetKeyString();

                Dictionary<string, object> parameters = new Dictionary<string, object>();
                foreach (var item in GetListProperty(entity.GetType()))
                {
                    object value = item.GetValue(entity);
                    if (value == null) continue;
                    parameters.Add(item.Name, value);

                    if (where.Contains(item.Name))
                    {
                        if (!string.IsNullOrEmpty(clause)) clause += " AND ";
                        clause += item.Name + " = " + paramPrefix + item.Name;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(set)) set += ", ";
                        set += item.Name + " = " + paramPrefix + item.Name;
                    }
                }

                string commandText = "UPDATE " + tableName + " SET " + set + " WHERE " + clause;
                output.obj = ExecuteNonQuery(commandText, parameters);
            }
            catch (Exception ex)
            {
                output.codeError = "99";
                output.strError = string.Format("SqlBaseRepository_UpdateWhere: ", ex.ToString());
            }

            DbConnection.Close();
            return output;
        }

        protected ResObject<bool> Delete(Expression<Func<T, bool>> conditions, IDbTransaction dbTransaction = null)
        {
            ResObject<bool> output = new ResObject<bool>() { obj = false };
            try
            {
                string tableName = GetTableName();
                string clause = string.Empty;
                Dictionary<string, object> parameters = new Dictionary<string, object>();

                clause = conditions.Body.GetKeyValue(parameters, paramPrefix);
                string commandText = "DELETE FROM " + tableName + " WHERE " + clause;
                output.obj = ExecuteNonQuery(commandText, parameters);
            }
            catch (Exception ex)
            {
                output.codeError = "99";
                output.strError = string.Format("SqlBaseRepository_Delete: ", ex.ToString());
            }

            DbConnection.Close();
            return output;
        }

        protected ResObject<bool> DeleteWhere<TKey>(T entity, Expression<Func<T, TKey>> keys, IDbTransaction dbTransaction = null)
        {
            ResObject<bool> output = new ResObject<bool>() { obj = false };
            try
            {
                string tableName = GetTableName();
                string clause = string.Empty;
                List<string> where = keys.Body.GetKeyString();

                Dictionary<string, object> parameters = new Dictionary<string, object>();
                foreach (var item in GetListProperty(entity.GetType()).Where(x => where.Contains(x.Name)))
                {
                    object value = item.GetValue(entity);
                    parameters.Add(item.Name, value);

                    if (!string.IsNullOrEmpty(clause)) clause += " AND ";
                    clause += item + " = " + paramPrefix + item;
                }

                string commandText = "DELETE FROM " + tableName + " WHERE " + clause;
                output.obj = ExecuteNonQuery(commandText, parameters);
            }
            catch (Exception ex)
            {
                output.codeError = "99";
                output.strError = string.Format("SqlBaseRepository_DeleteWhere: ", ex.ToString());
            }

            DbConnection.Close();
            return output;
        }

        protected ResObject<bool> ExistWhere(Expression<Func<T, bool>> conditions)
        {
            ResObject<bool> output = new ResObject<bool>() { obj = false };
            try
            {
                string tableName = GetTableName();
                string clause = string.Empty;

                Dictionary<string, object> parameters = new Dictionary<string, object>();
                clause = conditions.Body.GetKeyValue(parameters, paramPrefix);

                string commandText = "SELECT COUNT(1) FROM " + tableName + " WHERE " + clause;
                var obj = ExecuteScalar(commandText, parameters); //, parameters
                if (obj is int && Convert.ToInt32(obj) > 0)
                {
                    output.obj = true;
                }
            }
            catch (Exception ex)
            {
                output.codeError = "99";
                output.strError = string.Format("SqlBaseRepository_ExistWhere: ", ex.ToString());
            }

            DbConnection.Close();
            return output;
        }

        protected ResObject<T> GetById<TSelector>(Expression<Func<T, bool>> conditions)
        {
            ResObject<T> output = new ResObject<T>();
            try
            {
                string tableName = GetTableName();
                string clause = string.Empty;

                Dictionary<string, object> parameters = new Dictionary<string, object>();

                clause = conditions.Body.GetKeyValue(parameters, paramPrefix);

                string commandText = "SELECT * FROM " + tableName + " WHERE " + clause;

                output.obj = GetDataTable(commandText, parameters).GetFirst<T>();
            }
            catch (Exception ex)
            {
                output.codeError = "99";
                output.strError = string.Format("SqlBaseRepository_GetById: ", ex.ToString());
            }
            return output;
        }

        protected ResObject<T> GetByFilter<TSelector>(Expression<Func<T, bool>> conditions, string orderby, int pageSize = 10, int pageIndex = 0)
        {
            ResObject<T> output = new ResObject<T>();
            try
            {
                string tableName = GetTableName();
                string clause = string.Empty;
                Dictionary<string, object> parameters = new Dictionary<string, object>();

                clause = conditions.Body.GetKeyValue(parameters, paramPrefix);

                string commandText = "SELECT * FROM " + tableName + " WHERE " + clause + " \n " + string.Format(_SkipTake, orderby, pageSize * pageIndex, pageSize * (pageIndex + 1));

                output.listObj = GetDataTable(commandText, parameters)?.ToList<T>();
            }
            catch (Exception ex)
            {
                output.codeError = "99";
                output.strError = string.Format("SqlBaseRepository_GetByFilter: ", ex.ToString());
            }
            return output;
        }

        protected ResObject<T> GetAll<TSelector>(string orderby, int pageSize = 10, int pageIndex = 0)
        {
            ResObject<T> output = new ResObject<T>();
            try
            {
                string tableName = GetTableName();
                string commandText = "SELECT * FROM " + tableName + " \n " + string.Format(_SkipTake, orderby, pageSize * pageIndex, pageSize * (pageIndex + 1));

                output.listObj = GetDataTable(commandText, new object[] { })?.ToList<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}:{1}", "GetAll", ex.ToString());
            }
            return output;
        }

        /*
         public bool Insert(List<T> entities)
		    {
			    return ExecuteWithTransaction((IDbTransaction dbTransaction) =>
			    {
				    foreach (var entity in entities)
				    {
					    if (!Insert(entity, dbTransaction)) return false;
				    }
				    return true;
			    });
		    }
         */

        #endregion
    }
}
