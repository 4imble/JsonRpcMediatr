using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using MediatR;
using MediatR.Pipeline;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using TechTalk.SpecFlow;
using _4imble.JsonRpcMediatr.Specs.Helpers;
using _4imble.JsonRpcMediatr.Specs.TestRequests;
using _4imble.Mediatr.JsonRpc.Specs.TestClasses;

namespace _4imble.JsonRpcMediatr.Specs.Configuration
{
    [Binding]
    public class SpecFlowTestHooks
    {
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            TestIocConfiguration.RegisterCommonConfiguration();

            BuildMediator(ApplicationContext.Container);

            var knownType = typeof(PingRequest);
            var registrations =
                from type in knownType.Assembly.GetExportedTypes()
                where type.Namespace == knownType.Namespace
                where type.GetInterfaces().Any(x => x == typeof(IBaseRequest))
                select type;

            ApplicationContext.Container.RegisterSingleton<IJsonRpcRequestHandler>(() =>
                new JsonRpcRequestHandler(ApplicationContext.Container.GetInstance<IMediator>(), registrations, new TestJsonRpcLogger()));

            ApplicationContext.Container.Verify();
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            AsyncScopedLifestyle.BeginScope(ApplicationContext.Container);
        }

        [BeforeStep]
        public void BeforeStep()
        {
        }

        [AfterStep]
        public void AfterStep()
        {
            
        }

        [AfterScenario]
        public void AfterScenario()
        {
            Lifestyle.Scoped.GetCurrentScope(ApplicationContext.Container)?.Dispose();
            TestContext.Reset();
        }

        public static void BuildMediator(Container container)
        {
            var mediatrAssemblies = GetAssemblies().ToList();

            container.RegisterSingleton<IMediator, Mediator>();
            container.Register(typeof(IRequestHandler<>), mediatrAssemblies);
            container.Register(typeof(IRequestHandler<,>), mediatrAssemblies);
            
            var notificationHandlerTypes = container.GetTypesToRegister(typeof(INotificationHandler<>), mediatrAssemblies, new TypesToRegisterOptions
                {
                    IncludeGenericTypeDefinitions = true,
                    IncludeComposites = false,
                });

            container.Register(typeof(INotificationHandler<>), notificationHandlerTypes);

            container.Collection.Register(typeof(IPipelineBehavior<,>), new[]
                {
                    typeof(RequestPreProcessorBehavior<,>),
                    typeof(RequestPostProcessorBehavior<,>)
                });

            container.Collection.Register(typeof(IRequestPreProcessor<>), Enumerable.Empty<Type>());
            container.Collection.Register(typeof(IRequestPostProcessor<,>), Enumerable.Empty<Type>());

            container.Register(() => new ServiceFactory(container.GetInstance), Lifestyle.Singleton);
        }

        static IEnumerable<Assembly> GetAssemblies()
        {
            yield return typeof(IMediator).GetTypeInfo().Assembly;
            yield return typeof(Ping).GetTypeInfo().Assembly;
            yield return typeof(PingRequest).GetTypeInfo().Assembly;
        }
    }
}