using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace HS.SupportComponents
{
    /// <summary>
    /// 比较运算类型
    /// </summary>
    [DataContract]
    [Flags]
    public enum ComparisonOperator
    {
        /// <summary>
        /// 大于
        /// </summary>
        [EnumMember]
        Greater= 0,       
        /// <summary>
        /// 小于
        /// </summary>
        [EnumMember]
        Less=1,
        /// <summary>
        /// 等于
        /// </summary>
        [EnumMember]
        Equal=2,
        /// <summary>
        /// 不等
        /// </summary>
        [EnumMember]
        NotEquals=3,
        /// <summary>
        /// 大于等于
        /// </summary>
        [EnumMember]
        GreaterorEqual=4,
        /// <summary>
        /// 小于等于
        /// </summary>
        [EnumMember]
        LessorEqual=5
    }
}
