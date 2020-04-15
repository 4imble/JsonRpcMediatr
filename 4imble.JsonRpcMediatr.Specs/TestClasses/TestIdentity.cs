using System.Security.Principal;

namespace _4imble.JsonRpcMediatr.Specs.TestClasses
{
    public class TestIdentity: IIdentity
    {
        public TestIdentity(string name, bool isAuthenticated)
        {
            this.Name = name;
            this.AuthenticationType = isAuthenticated ? "TestClaim" : string.Empty;
        }

        public string Name { get; set; }

        public string AuthenticationType { get; set; }

        /// <summary>
        /// This is calculated from principle based on if there is a AuthenticationType or not?!
        /// </summary>
        public bool IsAuthenticated { get; }
    }
}
