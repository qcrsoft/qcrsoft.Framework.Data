using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace qcrsoft.Framework.Data.Common
{
    /// <summary>
    /// SqlClient.SqlParameterCollection的对应类
    /// </summary>
    public class ExOleDbParameterCollection : System.Collections.IEnumerable
    {
        /// <summary>
        /// 参数集合
        /// </summary>
        List<ExOleDbParameter> parameters = new List<ExOleDbParameter>();

        /// <summary>
        /// 为集合增加元素
        /// </summary>
        /// <param name="paramemterName">键值明</param>
        /// <param name="dbType">数据类型</param>
        /// <returns></returns>
        public ExOleDbParameter Add(string paramemterName, System.Data.OleDb.OleDbType dbType)
        {
            ExOleDbParameter parameter = new ExOleDbParameter(paramemterName, dbType);
            parameters.Add(parameter);
            return parameter;
        }

        /// <summary>
        /// 枚举接口
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            foreach (ExOleDbParameter p in parameters)
            {
                yield return p;

            }
        }

        /// <summary>
        /// 序号索引器
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ExOleDbParameter this[int index]
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
        public ExOleDbParameter this[string paramemterName]
        {
            get
            {
                foreach (ExOleDbParameter parameter in parameters)
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
