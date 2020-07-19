using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace qcrsoft.Framework.Data.Common
{
    /// <summary>
    /// SqlClient.ExOleDbCommand的对应类
    /// </summary>
    public class ExOleDbCommand
    {
        #region 字段
        /// <summary>
        /// 数据库连接
        /// </summary>
        private OleDbConnection _connection;
        /// <summary>
        /// 命令文本
        /// </summary>
        private string _commandText;
        /// <summary>
        /// 数集合
        /// </summary>
        private ExOleDbParameterCollection _parameters = new ExOleDbParameterCollection();
        #endregion

        #region 属性
        /// <summary>
        /// 获取或设置数据库连接
        /// </summary>
        public OleDbConnection Connection
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
        public ExOleDbParameterCollection Parameters
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
        /// 构造 DbCommand 实例
        /// </summary>
        public ExOleDbCommand()
        {
        }

        /// <summary>
        /// 构造 DbCommand 实例
        /// </summary>
        /// <param name="commandText">命令文本</param>
        public ExOleDbCommand(string commandText)
            : this(commandText, null)
        {

        }

        /// <summary>
        /// 构造 DbCommand 实例
        /// </summary>
        /// <param name="commandText">命令文本</param>
        public ExOleDbCommand(string commandText, OleDbConnection conn)
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
            OleDbCommand cmd = new OleDbCommand(_commandText);
            foreach (ExOleDbParameter parameter in _parameters)
            {
                if (parameter.Value == null)
                    cmd.Parameters.Add(parameter.ParamemterName, parameter.DbType, parameter.Size).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(parameter.ParamemterName, parameter.DbType, parameter.Size).Value = parameter.Value;
            }

            int ret;
            if (_connection == null)
            {
                using (OleDbConnection conn = qcrsoft.Framework.Utility.Data.GetOleDbConnection())
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
            OleDbCommand cmd = new OleDbCommand(_commandText);
            foreach (ExOleDbParameter parameter in _parameters)
            {
                if (parameter.Value == null)
                    cmd.Parameters.Add(parameter.ParamemterName, parameter.DbType, parameter.Size).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(parameter.ParamemterName, parameter.DbType, parameter.Size).Value = parameter.Value;
            }
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataTable dt = new DataTable();
            if (_connection == null)
            {
                using (OleDbConnection conn = qcrsoft.Framework.Utility.Data.GetOleDbConnection())
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
            OleDbCommand cmd = new OleDbCommand(_commandText);
            foreach (ExOleDbParameter parameter in _parameters)
            {
                if (parameter.Value == null)
                    cmd.Parameters.Add(parameter.ParamemterName, parameter.DbType, parameter.Size).Value = DBNull.Value;
                else
                    cmd.Parameters.Add(parameter.ParamemterName, parameter.DbType, parameter.Size).Value = parameter.Value;
            }
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            object ret = null;
            if (_connection == null)
            {
                using (OleDbConnection conn = qcrsoft.Framework.Utility.Data.GetOleDbConnection())
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
