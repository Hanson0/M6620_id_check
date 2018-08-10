using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Production.Server
{

    public enum ReturnCode
    {
        请求管理员处理 = 9999,         //ask admin for help
        未知错误 = 0000,                //unknown error 

        [Description("success")]
        执行成功 = 1000,                //success
        上报执行成功,          //upload success 

        重号或者重做,                 //verify repeat
        前工序未做,                      //verify miss

        工艺方案为空,                 //process scheme null
        工序不在工艺方案中,          //procedure invalid
        工序不在工序表中,               //procedure not found
        EID不在数据库关联表内,           //eid plan base not included

        工站不存在,                  //station invalid
        计划不存在,                  //plan invalid
        IMEI已经用完,                   //imei exhausted
        正在维修,                       //verify fixing
        已经报废,                       //reject

        计划类型无效,                    //plan type invalid
        未进入维修,                      //not repair
        资源没找到,                      //product not found

        IMEI里面未找到,                  //imei invalid
        IMEI未被使用,                   //imei not used

        测试完成,
        EID和IMEI未关联,

        小包装已经完成,
        小包装溢出,
        已经小包装,

        装箱清单为空,
        资源已存在,

        计划单已完成,
        资源超过计划单,
    }



    //package com.ilink.production.constant.mobile.test;

    ///**
    // * Created by LH on 2017/3/4.
    // */
    //public class ResponseConstants
    //{

    //    //请求管理员处理
    //    public static final String CODE_ASK_ADMIN_HELP = "9999";
    //public static final String MSG_ASK_ADMIN_HELP = "请求管理员帮助";

    ////未知错误
    //public static final String CODE_SYSTEM_ERROR = "0000";
    //public static final String MSG_SYSTEM_ERROR = "未知错误";

    ////执行成功
    //public static final String CODE_SUCCESS = "1000";
    //public static final String MSG_SUCCESS = "成功";

    //public static final String CODE_UPLOAD_SUCCESS = "1001";
    //public static final String MSG_UPLOAD_SUCCESS = "上传成功";

    ////重号或者重做
    //public static final String CODE_VERIFY_REPEAT = "1002";
    //public static final String MSG_VERIFY_REPEAT = "重号或者重做";

    ////前工序未做
    //public static final String CODE_VERIFY_MISS = "1003";
    //public static final String MSG_VERIFY_MISS = "前工序未做";

    ////工艺方案为空
    //public static final String CODE_PROCESS_SCHEME_NULL = "1004";
    //public static final String MSG_PROCESS_SCHEME_NULL = "工艺方案为空";

    ////工序不在工艺方案中
    //public static final String CODE_PROCEDURE_INVALID = "1005";
    //public static final String MSG_PROCEDURE_INVALID = "工序不在工艺方案中";

    ////工序不在工序表中
    //public static final String CODE_PROCEDURE_NOT_FOUND = "1006";
    //public static final String MSG_PROCEDURE_NOT_FOUND = "工序不存在";

    ////EID不在数据库关联表内
    //public static final String CODE_EID_PLAN_BASE_NOT_INCLUDED = "1007";
    //public static final String MSG_EID_PLAN_BASE_NOT_INCLUDED = "EID不在数据库关联表内";

    ////工站不存在
    //public static final String CODE_STATION_INVALID = "1008";
    //public static final String MSG_STATION_INVALID = "工站不存在";

    ////计划不存在
    //public static final String CODE_PLAN_INVALID = "1009";
    //public static final String MSG_PLAN_INVALID = "计划不存在";

    ////IMEI已经用完
    //public static final String CODE_IMEI_EXHAUSTED = "1010";
    //public static final String MSG_IMEI_EXHAUSTED = "IMEI已经用完";

    ////正在维修
    //public static final String CODE_REPAIR = "1011";
    //public static final String MSG_REPAIR= "正在维修";

    ////已经报废
    //public static final String CODE_REJECT = "1012";
    //public static final String MSG_REJECT= "已经报废";

    ////计划类型无效
    //public static final String CODE_PLAN_TYPE_INVALID = "1013";
    //public static final String MSG_PLAN_TYPE_INVALID= "计划类型无效";

    ////未进入维修
    //public static final String CODE_NOT_REPAIR = "1014";
    //public static final String MSG_NOT_REPAIR= "未进入维修";

    ////资源没找到
    //public static final String CODE_PRODUCT_NOT_FOUND = "1015";
    //public static final String MSG_PRODUCT_NOT_FOUND ="产品为找到";

    ////IMEI里面未找到
    //public static final String CODE_IMEI_INVALID = "1016";
    //public static final String MSG_IMEI_INVALID = "IMEI未找到";

    ////IMEI未被使用
    //public static final String CODE_IMEI_NOT_USED = "1017";
    //public static final String MSG_IMEI_NOT_USED = "IMEI未被使用";

    ////测试完成
    //public static final String CODE_TEST_FINISHED = "1018";
    //public static final String MSG_TEST_FINISHED = "测试完成";

    ////EID和IMEI未关联
    //public static final String CODE_EID_IMEI_NOT_RELATED = "1019";
    //public static final String MSG_EID_IMEI_NOT_RELATED = "EID和IMEI未关联";

    ////小包装已经完成
    //public static final String CODE_SMALL_PACKAGE_FINISHED = "1020";
    //public static final String MSG_SMALL_PACKAGE_FINISHED = "小包装已经完成";

    ////小包装溢出
    //public static final String CODE_SMALL_PACKAGE_OVERFLOW = "1021";
    //public static final String MSG_SMALL_PACKAGE_OVERFLOW = "小包装溢出";

    ////已小包装
    //public static final String CODE_SMALL_PACKAGED = "1022";
    //public static final String MSG_SMALL_PACKAGED = "已经小包装";

    ////装箱清单为空
    //public static final String CODE_SMALL_PACKAGE_NULL = "1023";
    //public static final String MSG_SMALL_PACKAGE_NULL = "小包装为空";

    ////资源已存在
    //public static final String CODE_EXISTED = "1024";
    //public static final String MSG_EXISTED ="资源已经存在";

    ////计划单的已完成
    //public static final String CODE_FINISHED = "1025";
    //public static final String MSG_FINISHED = "计划已经完成";

    ////资源超过计划单
    //public static final String CODE_OVERFLOWED = "1026";
    //public static final String MSG_OVERFLOWED = "资源溢出";
    //}

}
