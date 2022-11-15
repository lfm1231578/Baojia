using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HS.SupportComponents
{
    /// <summary>
    /// 操作返回结果信息
    /// </summary>
    public class ResultEntity
    {
        public ResultEntity()
        {

        }

        /// <summary>
        /// 此构造方法使用IsSuccess为false
        /// </summary>
        /// <param name="description"></param>
        public ResultEntity(string description)
            : this()
        {
            this._decription = description;
            this._isSuccess = false;
        }

        public ResultEntity(bool isSuccess, string description)
            : this()
        {
            this._isSuccess = isSuccess;
            this._decription = description;
        }

        /// <summary>
        /// 是否操作成功
        /// </summary>
        private bool _isSuccess = true;
        /// <summary>
        /// 是否操作成功
        /// </summary>
        public bool IsSuccess
        {
            get { return _isSuccess; }
            set { _isSuccess = value; }
        }

        /// <summary>
        /// 操作结果信息：操作失败时返回失败原因
        /// </summary>
        private string _decription = "操作成功！";
        /// <summary>
        /// 操作结果信息：操作失败时返回失败原因
        /// </summary>
        public string Decription
        {
            get { return _decription; }
            set { _decription = value; }
        }
    }
}
