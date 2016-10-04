using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HelloAmy.CommonPagerService.Model.InParam;
using HelloAmy.CommonPagerService.Business;
using Newtonsoft.Json;

namespace MainTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            MPagerInParam param = new MPagerInParam()
            {
                PageSize = 5,
                PageIndex = 2,
                Parameters = new System.Collections.Generic.List<MSqlParameter>(),
                FieldNames = "*",
                Condition = "WHERE UseStatus > -1",
                DataBaseName = "HelloAmyDBRead",
                Sort = "KeyID ASC",
                TableName = "BasicUser",
            };

            BPagerQuery bll = new BPagerQuery();
            var ret = bll.PagerQuery(param);

            string inJson = JsonConvert.SerializeObject(param);
            string outJson = JsonConvert.SerializeObject(ret);
        }
    }
}
