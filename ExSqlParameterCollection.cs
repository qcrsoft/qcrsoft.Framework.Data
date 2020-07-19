using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace qcrsoft.Framework.Data.SqlServer
{
    /// <summary>
    /// SqlClient.SqlParameterCollection的对应类
    /// </summary>
    public class ExSqlParameterCollection : System.Collections.IEnumerable
    {
        /// <summary>
        /// 参数集合
        /// </summary>
        List<ExSqlParameter> parameters = new List<ExSqlParameter>();

        /// <summary>
        /// 为集合增加元素
        /// </summary>
        /// <param name="paramemterName">键值明</param>
        /// <param name="dbType">数据类型</param>
        /// <returns></returns>
        public ExSqlParameter Add(string paramemterName, System.Data.SqlDbType dbType)
        {
            ExSqlParameter parameter = new ExSqlParameter(paramemterName, dbType);
            parameters.Add(parameter);
            return parameter;
        }

        /// <summary>
        /// 枚举接口
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            foreach (ExSqlParameter p in parameters)
            {
                yield return p;

            }
        }

        /// <summary>
        /// 序号索引器
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ExSqlParameter this[int index]
        {
            get
            {
                return parameters[index];
            }
        }

        /// <summary>
        /// 简直索引器
        /// </summary>
        /// <param name="paramemterName"></param>
        /// <returns></returns>
        public ExSqlParameter this[string paramemterName]
        {
            get
            {
                foreach (ExSqlParameter parameter in parameters)
                {
                    if (parameter.ParamemterName == paramemterName)
                    {
                        return parameter;
                    }
                }
                throw new Exception("无效的键值");
            }
        }

        /// <summary>
        /// 元素个数
        /// </summary>
        public int Count
        {
            get
            {
                return parameters.Count;
            }
        }
    }
}
