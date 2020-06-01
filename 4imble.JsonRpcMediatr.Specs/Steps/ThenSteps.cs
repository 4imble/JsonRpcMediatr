using FluentAssertions;
using Newtonsoft.Json;
using TechTalk.SpecFlow;
using _4imble.JsonRpcMediatr.RequestsResponses;
using _4imble.JsonRpcMediatr.Specs.Helpers;
using Newtonsoft.Json.Linq;

namespace _4imble.JsonRpcMediatr.Specs.Steps
{
    [Binding]
    public class ThenSteps
    {
        [Then(@"it should respond with the following response error")]
        public void ThenItShouldRespondWithTheFollowingResponseError(dynamic responseData)
        {
            var response = TestContext.Recall<JsonRpcResponseError>("response");
            var expectedResponse = JsonConvert.DeserializeObject<JsonRpcResponseError>((string)responseData.Json);

            response.Should().BeEquivalentTo(expectedResponse);
        }

        [Then(@"it should respond with the following response")]
        public void ThenItShouldRespondWithTheFollowingResponseSuccess(dynamic responseData)
        {
            var response = SerializationHelper.ReserializeObject(TestContext.Recall<JsonRpcResponseSuccess>("response"));
            var expectedResponse = JsonConvert.DeserializeObject<JsonRpcResponseSuccess>((string)responseData.Json);

            response.Should().BeEquivalentTo(expectedResponse);
        }

        
        [Then(@"I should get a '(.*)' string result")]
        public void ThenIShouldGetAStringResult(string result)
        {
            TestContext.Recall<string>("Result").Should().Be(result);
        }

        [Then(@"I should get a '(.*)' number result")]
        public void ThenIShouldGetANumberResult(int result)
        {
            TestContext.Recall<int>("Result").Should().Be(result);
        }

        [Then(@"it should log the following details")]
        public void ThenItShouldLogTheFollowingDetails(dynamic expected)
        {
            string expectedMethod = expected.Method.ToUpper();
            string expectedParams = expected.Params;

            var log = TestContext.Recall<JsonRpcRequest>("Log");
            log.Method.ToUpper().Should().Be(expectedMethod);
            log.Params.Should().BeEquivalentTo(JsonConvert.DeserializeObject<JObject>(expectedParams));
            log.ExecutionTime.TotalMilliseconds.Should().NotBe(0);
        }

    }
}
