using System.Security.Claims;
using TechTalk.SpecFlow;
using _4imble.JsonRpcMediatr.Tests.Helpers;
using _4imble.JsonRpcMediatr.Tests.TestClasses;

namespace _4imble.JsonRpcMediatr.Tests.Steps
{
    [Binding]
    public class GivenSteps
    {
        [Given(@"I have the following RPC request object")]
        public void GivenIHaveTheFollowingRPCRequestObject(Table requestData)
        {
            var identifier = requestData.GetTableValue<string>("Identifier");
            var json = requestData.GetTableValue<string>("Json");
            TestContext.Remember(identifier, json);
        }

        [Given(@"I am the following user")]
        public void GivenIAmTheFollowingUser(Table userData)
        {
            var name = userData.GetTableValue<string>("Name");
            var isAuthenticated = userData.GetTableValue<bool>("IsAuthenticated");

            var identity = new TestIdentity(name, isAuthenticated);
            var user = new ClaimsPrincipal(identity);

            TestContext.Remember("CurrentUser", user);
        }

    }   
}
