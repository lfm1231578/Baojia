using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HS.SupportComponents
{
    public class ResultEntity<T> : ResultEntity
    {
        public ResultEntity()
            : base()
        {
        }

        public ResultEntity(string description)
            : base(description)
        {
        }

        public ResultEntity(bool isSuccess, string description)
            : base(isSuccess, description)
        {
        }

        private T _t;
        /// <summary>
        /// 返回指定的对象
        /// </summary>
        public T TObject
        {
            get { return _t; }
            set { _t = value; }
        }
    }
}
