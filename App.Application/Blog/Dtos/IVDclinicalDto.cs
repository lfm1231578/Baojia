using System;
using System.Collections.Generic;

namespace App.Application.Blog.Dtos
{
    [Serializable]
    public class IVDclinicalDto : EntityDto<string>
    {
        public System.Int32 id { get; set; }
        public string wenxunriqi { get; set; }
        public string changping_zhongwen { get; set; }
        public string changping_kehugongshimingcheng { get; set; }
        public string changping_yingwen { get; set; }
        public string changping_gongshiwangzhi { get; set; }
        public string kehulianchengshiyanfuzheren { get; set; }
        public string lianxidianhua { get; set; }
        public string lianxiyouxinag { get; set; }
        public string lianxidizhi { get; set; }
        public string yuguchangpingjiaqian { get; set; }
        public string yuguchangpingfenlie { get; set; }
        public string yewuliexinjihuifushijianxunhao { get; set; }
        public string yewuliexinjihuifushijiandanxiang { get; set; }
        public string qiyexinxi_zhucezhijin { get; set; }
        public string qiyexinxi_qiyeyuangongrenshu { get; set; }
        public string qiyexinxi_muqianshichangzhudachangping { get; set; }
        public string qiyexinxi_nianyingyeer { get; set; }
        public string qiyexinxi_qiyexinzhi { get; set; }
        public string qiyexinxi_chenglishijian { get; set; }
        public string jichutiaojian_ischangfang { get; set; }
        public string jichutiaojian_isyingjianshebei { get; set; }
        public string cpxx_yuqiyongtu { get; set; }
        public string cpxx_zhuchechangpingzhucheng { get; set; }
        public string cpxx_muqianchuyunagejieduan { get; set; }
        public string cpxx_changpingyanfalaiyuan { get; set; }
        public string cpxx_isyoupeitaoyiqi { get; set; }
        public string cpxx_lianchuangyiyi { get; set; }
        public string cpxx_jianchemubiaorenqun { get; set; }
        public string cpxx_dingxinhuodingliang { get; set; }
        public string cpxx_jianchebuweihuojiancheyanben { get; set; }
        public string cpxx_yangbencaijiteshuyaoqui { get; set; }
        public string cpxx_yanbenbaochuntiaojian { get; set; }
        public string cpxx_shijihebaochuntiaojian { get; set; }
        public string cpxx_jianchefangfa { get; set; }
        public string cpxx_jianchezhibiao { get; set; }
        public string cpxx_yujiyingyongbenchangping { get; set; }
        public string cpxx_changpingyongyubingrenshidefeiyong { get; set; }
        public string tlcpxx_tongliechangpingmingcheng1 { get; set; }
        public string tlcpxx_shengchangchangjia2 { get; set; }
        public string tlcpxx_yiyuanyongyubingrenzhengduan2 { get; set; }
        public string tlcpxx_tongliechangpingmingcheng2 { get; set; }
        public string tlcpxx_shengchangchangjia3 { get; set; }
        public string tlcpxx_yiyuanyongyubingrenzhengduan3 { get; set; }
        public string tlcpxx_tongliechangpingmingcheng3 { get; set; }
        public string tlcpxx_shengchangchangjia4 { get; set; }
        public string tlcpxx_yiyuanyongyubingrenzhengduan4 { get; set; }
        public string tlcpxx_tongliechangpingmingcheng4 { get; set; }
        public string tlcpxx_is { get; set; }
        public string duilianchuanjiguodeyaoqui { get; set; }
        public string duilianchuanjiguodeyaoquineirong { get; set; }
        public string kehudefeiyongyushuan { get; set; }
        public string kehufeiji { get; set; }
        public string wenxunrenqianzhiqueren { get; set; }
        public string shangwujingliqianzhiqueren { get; set; }
    }
}