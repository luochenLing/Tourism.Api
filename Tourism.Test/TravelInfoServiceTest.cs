using System;
using Tourism.QueryModel;
using Tourism.Server;
using Xunit;

namespace Tourism.Test
{
    public class TravelInfoServiceTest
    {
        public TravelInfoService server = new TravelInfoService();
        [Trait("TravelInfo", "GetTravelInfoListAsync")]
        [Fact]
        public async void GetTravelInfoListAsync()
        {
            var res = await server.GetTravelInfoListAsync(new TravelInfoQuery { ProType = "4", PageIndex = 1, PageSize = 5, Sort = "proPrice", Order = "desc" });
            Assert.NotNull(res);
        }

        [Trait("TravelInfo", "GetTravelInfoByQuery")]
        [Fact]
        public async void GetTravelInfoByQuery()
        {
            var res = await server.GetTravelInfoAsync(new TravelInfoQuery { ProId = new Guid("0a6ce470-2386-4416-8716-060f93081cc6") });
            Assert.NotNull(res);
        }


        [Trait("TravelInfo", "GetTravelActivityListByQuery")]
        [Fact]
        public async void GetTravelActivityListByQuery()
        {
            var res = await server.GetTravelActivityListByQuery(new TravelActivityQuery { PId = new Guid("0a6ce470-2386-4416-8716-060f93081cc6") });
            Assert.NotNull(res);
        }

    }
}
