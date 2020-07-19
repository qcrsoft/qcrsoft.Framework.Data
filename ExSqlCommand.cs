using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace qcrsoft.Framework.Data.SqlServer
{
    /// <summary>
    /// SqlClient.SqlCommand的对应类
    /// </summary>
    public class ExSqlCommand
    {
        #region 字段
        /// <summary>
        /// 数据库连接
        /// </summary>
        private SqlConnection _connection;
        /// <summary>
        /// 命令文本
        /// </summary>
        private string _commandText;
        /// <summary>
        /// 数集合
        /// </summary>
        private ExSqlParameterCollection _parameters = new ExSqlParameterCollection();
        #endregion

        #region 属性
        /// <summary>
        /// 获取或设置数据库连接
        /// </summary>
        public SqlConnection Connection
        {
            get
            {
                return _connection;
            }
            set
            {
                _connection = value;
            }
        }

        /// <summary>
        /// 获取或设置命令文本
        /// </summary>
        public string CommandText
        {
            get
            {
                return _commandText;
            }
            set
            {
                _commandText = value;
            }
        }

        /// <summary>
        /// 获取或设置参数集合
        /// </summary>
        public ExSqlParameterCollection Parameters
        {
            get
            {
                return _parameters;
            }
            set
            {
                _parameters = value;
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造 ExSqlCommand 实例
        /// </summary>
        public ExSqlCommand()
        {
        }

        /// <summary>
        /// 构造 ExSqlCommand 实例
        /// </summary>
        /// <param name="commandText">命令文本</param>
        public ExSqlCommand(string commandText) : this(commandText,null)
        {
            
        }

        /// <summary>
        /// 构造 ExSqlCommand 实例
        /// </summary>
        /// <param name="commandText">命令文本</param>
        public ExSqlCommand(string commandText, SqlConnection conn)
        {
            _commandText = commandText;
            _connection = conn;
        }
        #endregion

        /// <summary>
        /// 对连接执行 Transact-SQL 语句并返回受影响的行数
        /// </summary>
        /// <returns></returns>
        public int ExecuteNonQuery()
        {
            SqlCommand cmd = new SqlCommand(_commandText);
            foreach (ExSqlParameter parameter in _parameters)
            {
                if (parameter.Value == null)
                    cmd.Parameters.Add(parameter.ParamemterName, parameter.DbType, parameter.Size).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(parameter.ParamemterName, parameter.DbType, parameter.Size).Value = parameter.Value;
            }
            
            int ret;
            if (_connection == null)
            {
                using (SqlConnection conn = qcrsoft.Framework.Utility.Data.GetSqlConnection())
                {
                    cmd.Connection = conn;
                    ret = cmd.ExecuteNonQuery();
                }
            }
            else
            {
                cmd.Connection = _connection;
                ret = cmd.ExecuteNonQuery();
            }
            return ret;
        }

        /// <summary>
        /// 对连接执行 Transact-SQL 语句并返回DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable ExecuteDataTable()
        {
            SqlCommand cmd = new SqlCommand(_commandText);
            foreach (ExSqlParameter parameter in _parameters)
            {
                if (parameter.Value == null)
                    cmd.Parameters.Add(parameter.ParamemterName, parameter.DbType, parameter.Size).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(parameter.ParamemterName, parameter.DbType, parameter.Size).Value = parameter.Value;
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            if (_connection == null)
            {
                using (SqlConnection conn = qcrsoft.Framework.Utility.Data.GetSqlConnection())
                {
                    cmd.Connection = conn;
                    da.Fill(dt);
                }
            }
            else
            {
                cmd.Connection = _connection;
                da.Fill(dt);
            }

            return dt;
        }

        public object ExecuteScalar()
        {
            SqlCommand cmd = new SqlCommand(_commandText);
            foreach (ExSqlParameter parameter in _parameters)
            {
                if (parameter.Value == null)
                    cmd.Parameters.Add(parameter.ParamemterName, parameter.DbType, parameter.Size).Value = DBNull.Value;
                else
                    if (parameter.Value.GetType().FullName == "Mobao.Monitor.Model.Time")
                        cmd.Parameters.Add(parameter.ParamemterName, parameter.DbType, parameter.Size).Value = parameter.Value.ToString();
                    else
                        cmd.Parameters.Add(parameter.ParamemterName, parameter.DbType, parameter.Size).Value = parameter.Value;
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            object ret = null;
            if (_connection == null)
            {
                using (SqlConnection conn = qcrsoft.Framework.Utility.Data.GetSqlConnection())
                {
                    cmd.Connection = conn;
                    ret = cmd.ExecuteScalar();
                }
            }
            else
            {
                cmd.Connection = _connection;
                ret = cmd.ExecuteScalar();
            }

            return ret;
        }
    }
}