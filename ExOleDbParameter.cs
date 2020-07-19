using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace qcrsoft.Framework.Data.Common
{
    /// <summary>
    /// SqlClient.SqlParameter的对应类
    /// </summary>
    public class ExOleDbParameter
    {
        #region 字段
        private string _paramemterName;
        private OleDbType _dbType;
        private object _value;
        #endregion

        #region 属性
        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParamemterName
        {
            get
            {
                return _paramemterName;
            }
            set
            {
                _paramemterName = value;
            }
        }

        /// <summary>
        /// 数据类型
        /// </summary>
        public OleDbType DbType
        {
            get
            {
                return _dbType;
            }
            set
            {
                _dbType = value;
            }
        }

        /// <summary>
        /// 参数值
        /// </summary>
        public object Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        /// <summary>
        /// 参数对应的数据库字段类型最大宽度
        /// </summary>
        public int Size
        {
            get
            {
                return Utility.Data.DbTypeSize(_dbType);
            }
        }
        #endregion

        /// <summary>
        /// 生成 ExOleDbParameter 实例
        /// </summary>
        /// <param name="paramemterName"></param>
        /// <param name="dbType"></param>
        public ExOleDbParameter(string paramemterName, System.Data.OleDb.OleDbType dbType)
        {
            _paramemterName = paramemterName;
            _dbType = dbType;
        }
    }
}
