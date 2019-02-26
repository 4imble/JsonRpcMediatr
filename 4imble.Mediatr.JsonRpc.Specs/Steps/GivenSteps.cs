using System.Security.Claims;
using TechTalk.SpecFlow;
using _4imble.Mediatr.JsonRpc.Specs.Helpers;
using _4imble.Mediatr.JsonRpc.Specs.TestClasses;

namespace _4imble.Mediatr.JsonRpc.Specs.Steps
{
    [Binding]
    public class GivenSteps
    {
        [Given(@"I have the following RPC request object")]
        public void GivenIHaveTheFollowingRPCRequestObject(dynamic requestData)
        {
            TestContext.Remember(requestData.Identifier, requestData.Json);
        }

        [Given(@"I am the following user")]
        public void GivenIAmTheFollowingUser(dynamic userData)
        {
            var name = PropertyAccessHelper.TryGetValue<string>(() => userData.Name);
            var isAuthenticated = PropertyAccessHelper.TryGetValue<bool>(() => userData.IsAuthenticated);

            var identity = new TestIdentity(name, isAuthenticated);
            var user = new ClaimsPrincipal(identity);

            TestContext.Remember("CurrentUser", user);
        }

    }   
}
