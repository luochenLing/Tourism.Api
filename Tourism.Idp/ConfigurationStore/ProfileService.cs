using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Tourism.IServer;

namespace Tourism.Idp.ConfigurationStore
{
    public class ProfileService : IProfileService
    {
        public ICustomerServer _customerServer;
        private readonly IHttpClientFactory _httpClientFactory;
        public ProfileService(ICustomerServer customerServer, IHttpClientFactory httpClientFactory)
        {
            _customerServer = customerServer;
            _httpClientFactory = httpClientFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            ////var ret = _customerServer.GetCustomerInfoByQueryAsync(new UserQuery
            ////{
            ////    CName = context.Client.ClientName,
            ////    //CPasswd = context.Subject
            ////}).GetAwaiter().GetResult().FirstOrDefault();

            //var userId = context.Subject.Claims.FirstOrDefault(x => x.Type == "sub");
            ////获取User_Id
            //if (!string.IsNullOrEmpty(userId?.Value) && long.Parse(userId.Value) > 0)
            //{
            //    var client = _httpClientFactory.CreateClient();
            //    //已过时
            //    //DiscoveryResponse disco = await DiscoveryClient.GetAsync("http://localhost:5000");
            //    //TokenClient tokenClient = new TokenClient(disco.TokenEndpoint, "AuthServer", "secret");
            //    //var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");

            //    DiscoveryResponse disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
            //    var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            //    {
            //        Address = disco.TokenEndpoint,
            //        ClientId = "AuthServer",
            //        ClientSecret = "secret",
            //        Scope = "api1"
            //    });
            //    if (tokenResponse.IsError)
            //        throw new Exception(tokenResponse.Error);
            //    client.SetBearerToken(tokenResponse.AccessToken);

            //    //根据User_Id获取user
            //    var response = await client.GetAsync("http://localhost:5001/api/values/" + long.Parse(userId.Value));
            //    //get user from db (find user by user id)
            //    //var user = await _userRepository.FindAsync(long.Parse(userId.Value));
            //    var content = await response.Content.ReadAsStringAsync();
            //    User user = JsonConvert.DeserializeObject<User>(content);
            //    // issue the claims for the user
            //    if (user != null)
            //    {
            //        //获取user中的Claims
            //        var claims = GetUserClaims(user);
            //        //context.IssuedClaims = claims.Where(x => context.RequestedClaimTypes.Contains(x.Type)).ToList();
            //        context.IssuedClaims = claims.ToList();
            //    }
            //}
            throw new NotImplementedException();
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            throw new NotImplementedException();
        }

        //public static Claim[] GetUserClaims(User user)
        //{
        //    List<Claim> claims = new List<Claim>();
        //    Claim claim;
        //    foreach (var itemClaim in user.Claims)
        //    {
        //        claim = new Claim(itemClaim.Type, itemClaim.Value);
        //        claims.Add(claim);
        //    }
        //    return claims.ToArray();
        //}
    }
}
