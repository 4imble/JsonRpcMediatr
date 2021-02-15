using FluentAssertions;
using Newtonsoft.Json;
using TechTalk.SpecFlow;
using _4imble.JsonRpcMediatr.RequestsResponses;
using _4imble.JsonRpcMediatr.Tests.Helpers;
using Newtonsoft.Json.Linq;
using System;

namespace _4imble.JsonRpcMediatr.Tests.Steps
{
    [Binding]
    public class ThenSteps
    {
        [Then(@"it should respond with the following response error")]
        public void ThenItShouldRespondWithTheFollowingResponseError(Table responseData)
        {
            var json = responseData.GetTableValue<string>("Json");
            var response = TestContext.Recall<JsonRpcResponseError>("response");
            var expectedResponse = JsonConvert.DeserializeObject<JsonRpcResponseError>(json);

            response.Should().BeEquivalentTo(expectedResponse);
        }

        [Then(@"it should respond with the following response")]
        public void ThenItShouldRespondWithTheFollowingResponseSuccess(Table responseData)
        {
            var json = responseData.GetTableValue<string>("Json");
            var response = SerializationHelper.ReserializeObject(TestContext.Recall<JsonRpcResponseSuccess>("response"));
            var expectedResponse = JsonConvert.DeserializeObject<JsonRpcResponseSuccess>(json);

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
        public void ThenItShouldLogTheFollowingDetails(Table expected)
        {
            string expectedMethod = expected.GetTableValue<string>("Method").ToUpper();
            string expectedParams = expected.GetTableValue<string>("Params");
            string expectedType = expected.GetTableValue<string>("RequestType");

            var log = TestContext.Recall<JsonRpcRequest>("LogRequest");
            log.Method.ToUpper().Should().Be(expectedMethod);
            log.Params.Should().BeEquivalentTo(JsonConvert.DeserializeObject<JObject>(expectedParams));
            log.ExecutionTime.Should().NotBe(-1);

            var type = TestContext.Recall<Type>("LogRequestType");
            type.Name.Should().Be(expectedType);
        }

    }
}
