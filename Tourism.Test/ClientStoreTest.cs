using Tourism.Idp.ConfigurationStore;
using Tourism.IServer;
using Tourism.Server;
using Tourism.Util;
using Xunit;

namespace Tourism.Test
{
    public class ClientStoreTest
    {
        static IClientConfigService service = new ClientConfigService();
        public ClientStore server = new ClientStore(service);


        [Trait("ClientStoreTest", "GetGrantType")]
        [Fact]
        public void GetGrantType()
        {
            var sut = server.GetGrantType("ResourceOwnerPassword");
            Assert.NotNull(sut);
        }


        [Trait("ClientStoreTest", "GetPwdContent")]
        [Fact]
        public void GetPwdContent()
        {
            var sut = PwdHandler.MD5EncryptTo32("tourismPwd");
            Assert.NotNull(sut);
        }
    }
}
