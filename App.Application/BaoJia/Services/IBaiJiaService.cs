using App.Application.Dtos;
using App.Framwork.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application 
{
    public interface IBaoJiaService
    {
        /// <summary>
        /// 创建报价单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public UnifyResult<CreateBaoJiaDanOutput>  CreateBaoJiaDan(CreateBaoJiaDanInput input);

        /// <summary>
        /// 第一步初始化参数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public UnifyResult<CreateBaoJiaDanInput> Init(InitInput input);

        /// <summary>
        /// 查询报价单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public UnifyResult<SqlSugarPagedList<BaoJiaDanListOutput>> SearchBaoJiaDan(SearchBaoJiaDanInput input);

        /// <summary>
        /// 获取报价单详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UnifyResult<CreateBaoJiaDanInput> GetDetail(string id);

        /// <summary>
        /// 更新报价单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<UnifyResult<CreateBaoJiaDanOutput>> UpdateBaoJiaDan(UpdateBaoJiaDanInput input);

        /// <summary>
        /// 检查委托方是否存在
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public UnifyResult CheckCompany(CheckCompanyInput input);
    }
}
