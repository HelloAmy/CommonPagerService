using HelloAmy.CommonPagerService.Business;
using HelloAmy.CommonPagerService.Model.InParam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CommonPagerService.Controllers
{
    public class PagerQueryController : ApiController
    {
        public MPagerOutParam Get([FromBody] MPagerInParam value)
        {
            BPagerQuery bll = new BPagerQuery();
            return bll.PagerQuery(value);
        }
    }
}
