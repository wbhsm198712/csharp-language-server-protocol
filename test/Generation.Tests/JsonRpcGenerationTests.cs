using System.Threading.Tasks;
using OmniSharp.Extensions.JsonRpc.Generators;
using TestingUtils;
using Xunit;
using static Generation.Tests.GenerationHelpers;

namespace Generation.Tests
{
    public class JsonRpcGenerationTests
    {
        [FactWithSkipOn(SkipOnPlatform.Windows)]
        public async Task Supports_Generating_Notifications_And_Infers_Direction_ExitHandler()
        {
            var source = @"
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OmniSharp.Extensions.JsonRpc;
using OmniSharp.Extensions.JsonRpc.Generation;
using OmniSharp.Extensions.LanguageServer.Protocol;
using OmniSharp.Extensions.LanguageServer.Protocol.Client;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;

namespace OmniSharp.Extensions.LanguageServer.Protocol.Test
{
    [Serial, Method(GeneralNames.Exit, Direction.ClientToServer), GenerateHandlerMethods, GenerateRequestMethods]
    public interface IExitHandler : IJsonRpcNotificationHandler<ExitParams>
    {
    }
}";

            var expected = @"
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OmniSharp.Extensions.JsonRpc;
using OmniSharp.Extensions.JsonRpc.Generation;
using OmniSharp.Extensions.LanguageServer.Protocol;
using OmniSharp.Extensions.LanguageServer.Protocol.Client;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OmniSharp.Extensions.LanguageServer.Protocol.Test
{
#nullable enable
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, System.Runtime.CompilerServices.CompilerGeneratedAttribute]
    public static partial class ExitExtensions
    {
        public static ILanguageServerRegistry OnExit(this ILanguageServerRegistry registry, Action<ExitParams> handler) => registry.AddHandler(GeneralNames.Exit, NotificationHandler.For(handler));
        public static ILanguageServerRegistry OnExit(this ILanguageServerRegistry registry, Func<ExitParams, Task> handler) => registry.AddHandler(GeneralNames.Exit, NotificationHandler.For(handler));
        public static ILanguageServerRegistry OnExit(this ILanguageServerRegistry registry, Action<ExitParams, CancellationToken> handler) => registry.AddHandler(GeneralNames.Exit, NotificationHandler.For(handler));
        public static ILanguageServerRegistry OnExit(this ILanguageServerRegistry registry, Func<ExitParams, CancellationToken, Task> handler) => registry.AddHandler(GeneralNames.Exit, NotificationHandler.For(handler));
        public static void SendExit(this ILanguageClient mediator, ExitParams request) => mediator.SendNotification(request);
    }
#nullable restore
}";
            await AssertGeneratedAsExpected<GenerateHandlerMethodsGenerator>(source, expected);
        }

        [FactWithSkipOn(SkipOnPlatform.Windows)]
        public async Task Supports_Generating_Notifications_And_Infers_Direction_CapabilitiesHandler()
        {
            var source = @"
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OmniSharp.Extensions.JsonRpc;
using OmniSharp.Extensions.JsonRpc.Generation;

namespace OmniSharp.Extensions.DebugAdapter.Protocol.Events.Test
{
    [Parallel, Method(EventNames.Capabilities, Direction.ServerToClient), GenerateHandlerMethods, GenerateRequestMethods]
    public interface ICapabilitiesHandler : IJsonRpcNotificationHandler<CapabilitiesEvent> { }
}";

            var expected = @"
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OmniSharp.Extensions.DebugAdapter.Protocol;
using OmniSharp.Extensions.DebugAdapter.Protocol.Client;
using OmniSharp.Extensions.DebugAdapter.Protocol.Server;
using OmniSharp.Extensions.JsonRpc;
using OmniSharp.Extensions.JsonRpc.Generation;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OmniSharp.Extensions.DebugAdapter.Protocol.Events.Test
{
#nullable enable
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, System.Runtime.CompilerServices.CompilerGeneratedAttribute]
    public static partial class CapabilitiesExtensions
    {
        public static IDebugAdapterClientRegistry OnCapabilities(this IDebugAdapterClientRegistry registry, Action<CapabilitiesEvent> handler) => registry.AddHandler(EventNames.Capabilities, NotificationHandler.For(handler));
        public static IDebugAdapterClientRegistry OnCapabilities(this IDebugAdapterClientRegistry registry, Func<CapabilitiesEvent, Task> handler) => registry.AddHandler(EventNames.Capabilities, NotificationHandler.For(handler));
        public static IDebugAdapterClientRegistry OnCapabilities(this IDebugAdapterClientRegistry registry, Action<CapabilitiesEvent, CancellationToken> handler) => registry.AddHandler(EventNames.Capabilities, NotificationHandler.For(handler));
        public static IDebugAdapterClientRegistry OnCapabilities(this IDebugAdapterClientRegistry registry, Func<CapabilitiesEvent, CancellationToken, Task> handler) => registry.AddHandler(EventNames.Capabilities, NotificationHandler.For(handler));
        public static void SendCapabilities(this IDebugAdapterServer mediator, CapabilitiesEvent request) => mediator.SendNotification(request);
    }
#nullable restore
}";
            await AssertGeneratedAsExpected<GenerateHandlerMethodsGenerator>(source, expected);
        }


        [FactWithSkipOn(SkipOnPlatform.Windows)]
        public async Task Supports_Generating_Notifications_ExitHandler()
        {
            var source = @"
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OmniSharp.Extensions.JsonRpc;
using OmniSharp.Extensions.JsonRpc.Generation;
using OmniSharp.Extensions.LanguageServer.Protocol;
using OmniSharp.Extensions.LanguageServer.Protocol.Client;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;

namespace Test
{
    [Serial, Method(GeneralNames.Exit, Direction.ClientToServer), GenerateHandlerMethods(typeof(ILanguageServerRegistry)), GenerateRequestMethods(typeof(ILanguageClient))]
    public interface IExitHandler : IJsonRpcNotificationHandler<ExitParams>
    {
    }
}";

            var expected = @"
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OmniSharp.Extensions.JsonRpc;
using OmniSharp.Extensions.JsonRpc.Generation;
using OmniSharp.Extensions.LanguageServer.Protocol;
using OmniSharp.Extensions.LanguageServer.Protocol.Client;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
#nullable enable
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, System.Runtime.CompilerServices.CompilerGeneratedAttribute]
    public static partial class ExitExtensions
    {
        public static ILanguageServerRegistry OnExit(this ILanguageServerRegistry registry, Action<ExitParams> handler) => registry.AddHandler(GeneralNames.Exit, NotificationHandler.For(handler));
        public static ILanguageServerRegistry OnExit(this ILanguageServerRegistry registry, Func<ExitParams, Task> handler) => registry.AddHandler(GeneralNames.Exit, NotificationHandler.For(handler));
        public static ILanguageServerRegistry OnExit(this ILanguageServerRegistry registry, Action<ExitParams, CancellationToken> handler) => registry.AddHandler(GeneralNames.Exit, NotificationHandler.For(handler));
        public static ILanguageServerRegistry OnExit(this ILanguageServerRegistry registry, Func<ExitParams, CancellationToken, Task> handler) => registry.AddHandler(GeneralNames.Exit, NotificationHandler.For(handler));
        public static void SendExit(this ILanguageClient mediator, ExitParams request) => mediator.SendNotification(request);
    }
#nullable restore
}";
            await AssertGeneratedAsExpected<GenerateHandlerMethodsGenerator>(source, expected);
        }

        [FactWithSkipOn(SkipOnPlatform.Windows)]
        public async Task Supports_Generating_Notifications_And_Infers_Direction_DidChangeTextHandler()
        {
            var source = @"
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OmniSharp.Extensions.JsonRpc;
using OmniSharp.Extensions.JsonRpc.Generation;
using OmniSharp.Extensions.LanguageServer.Protocol;
using OmniSharp.Extensions.LanguageServer.Protocol.Client;
using OmniSharp.Extensions.LanguageServer.Protocol.Client.Capabilities;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;

namespace OmniSharp.Extensions.LanguageServer.Protocol.Test
{
    [Serial, Method(TextDocumentNames.DidChange, Direction.ClientToServer), GenerateHandlerMethods, GenerateRequestMethods]
    public interface IDidChangeTextDocumentHandler : IJsonRpcNotificationHandler<DidChangeTextDocumentParams>,
        IRegistration<TextDocumentChangeRegistrationOptions, SynchronizationCapability>
    { }
}";

            var expected = @"
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OmniSharp.Extensions.JsonRpc;
using OmniSharp.Extensions.JsonRpc.Generation;
using OmniSharp.Extensions.LanguageServer.Protocol;
using OmniSharp.Extensions.LanguageServer.Protocol.Client;
using OmniSharp.Extensions.LanguageServer.Protocol.Client.Capabilities;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OmniSharp.Extensions.LanguageServer.Protocol.Test
{
#nullable enable
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, System.Runtime.CompilerServices.CompilerGeneratedAttribute]
    public static partial class DidChangeTextDocumentExtensions
    {
        public static ILanguageServerRegistry OnDidChangeTextDocument(this ILanguageServerRegistry registry, Action<DidChangeTextDocumentParams> handler, Func<TextDocumentChangeRegistrationOptions> registrationOptionsFactory)
        {
            registrationOptionsFactory ??= () => new TextDocumentChangeRegistrationOptions();
            return registry.AddHandler(TextDocumentNames.DidChange, new LanguageProtocolDelegatingHandlers.Notification<DidChangeTextDocumentParams, TextDocumentChangeRegistrationOptions>(handler, registrationOptionsFactory));
        }

        public static ILanguageServerRegistry OnDidChangeTextDocument(this ILanguageServerRegistry registry, Func<DidChangeTextDocumentParams, Task> handler, Func<TextDocumentChangeRegistrationOptions> registrationOptionsFactory)
        {
            registrationOptionsFactory ??= () => new TextDocumentChangeRegistrationOptions();
            return registry.AddHandler(TextDocumentNames.DidChange, new LanguageProtocolDelegatingHandlers.Notification<DidChangeTextDocumentParams, TextDocumentChangeRegistrationOptions>(handler, registrationOptionsFactory));
        }

        public static ILanguageServerRegistry OnDidChangeTextDocument(this ILanguageServerRegistry registry, Action<DidChangeTextDocumentParams, CancellationToken> handler, Func<TextDocumentChangeRegistrationOptions> registrationOptionsFactory)
        {
            registrationOptionsFactory ??= () => new TextDocumentChangeRegistrationOptions();
            return registry.AddHandler(TextDocumentNames.DidChange, new LanguageProtocolDelegatingHandlers.Notification<DidChangeTextDocumentParams, TextDocumentChangeRegistrationOptions>(handler, registrationOptionsFactory));
        }

        public static ILanguageServerRegistry OnDidChangeTextDocument(this ILanguageServerRegistry registry, Func<DidChangeTextDocumentParams, CancellationToken, Task> handler, Func<TextDocumentChangeRegistrationOptions> registrationOptionsFactory)
        {
            registrationOptionsFactory ??= () => new TextDocumentChangeRegistrationOptions();
            return registry.AddHandler(TextDocumentNames.DidChange, new LanguageProtocolDelegatingHandlers.Notification<DidChangeTextDocumentParams, TextDocumentChangeRegistrationOptions>(handler, registrationOptionsFactory));
        }

        public static ILanguageServerRegistry OnDidChangeTextDocument(this ILanguageServerRegistry registry, Action<DidChangeTextDocumentParams, CancellationToken> handler, Func<TextDocumentChangeRegistrationOptions, SynchronizationCapability> registrationOptionsFactory)
        {
            registrationOptionsFactory ??= _ => new TextDocumentChangeRegistrationOptions();
            return registry.AddHandler(TextDocumentNames.DidChange, new LanguageProtocolDelegatingHandlers.Notification<DidChangeTextDocumentParams, TextDocumentChangeRegistrationOptions, SynchronizationCapability>(handler, registrationOptionsFactory));
        }

        public static ILanguageServerRegistry OnDidChangeTextDocument(this ILanguageServerRegistry registry, Func<DidChangeTextDocumentParams, CancellationToken, Task> handler, Func<TextDocumentChangeRegistrationOptions, SynchronizationCapability> registrationOptionsFactory)
        {
            registrationOptionsFactory ??= _ => new TextDocumentChangeRegistrationOptions();
            return registry.AddHandler(TextDocumentNames.DidChange, new LanguageProtocolDelegatingHandlers.Notification<DidChangeTextDocumentParams, TextDocumentChangeRegistrationOptions, SynchronizationCapability>(handler, registrationOptionsFactory));
        }

        public static void DidChangeTextDocument(this ILanguageClient mediator, DidChangeTextDocumentParams request) => mediator.SendNotification(request);
    }
#nullable restore
}
";
            await AssertGeneratedAsExpected<GenerateHandlerMethodsGenerator>(source, expected);
        }

        [FactWithSkipOn(SkipOnPlatform.Windows)]
        public async Task Supports_Generating_Notifications_And_Infers_Direction_FoldingRangeHandler()
        {
            var source = @"
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OmniSharp.Extensions.JsonRpc;
using OmniSharp.Extensions.JsonRpc.Generation;
using OmniSharp.Extensions.LanguageServer.Protocol;
using OmniSharp.Extensions.LanguageServer.Protocol.Client;
using OmniSharp.Extensions.LanguageServer.Protocol.Client.Capabilities;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;

namespace OmniSharp.Extensions.LanguageServer.Protocol.Test
{
    [Parallel, Method(TextDocumentNames.FoldingRange, Direction.ClientToServer)]
    [GenerateHandlerMethods, GenerateRequestMethods(typeof(ITextDocumentLanguageClient), typeof(ILanguageClient))]
    public interface IFoldingRangeHandler : IJsonRpcRequestHandler<FoldingRangeRequestParam, Container<FoldingRange>>,
        IRegistration<FoldingRangeRegistrationOptions, FoldingRangeCapability>
    {
    }
}";

            var expected = @"
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OmniSharp.Extensions.JsonRpc;
using OmniSharp.Extensions.JsonRpc.Generation;
using OmniSharp.Extensions.LanguageServer.Protocol;
using OmniSharp.Extensions.LanguageServer.Protocol.Client;
using OmniSharp.Extensions.LanguageServer.Protocol.Client.Capabilities;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Progress;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OmniSharp.Extensions.LanguageServer.Protocol.Test
{
#nullable enable
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, System.Runtime.CompilerServices.CompilerGeneratedAttribute]
    public static partial class FoldingRangeExtensions
    {
        public static ILanguageServerRegistry OnFoldingRange(this ILanguageServerRegistry registry, Func<FoldingRangeRequestParam, Task<Container<FoldingRange>>> handler, Func<FoldingRangeRegistrationOptions> registrationOptionsFactory)
        {
            registrationOptionsFactory ??= () => new FoldingRangeRegistrationOptions();
            return registry.AddHandler(TextDocumentNames.FoldingRange, new LanguageProtocolDelegatingHandlers.RequestRegistration<FoldingRangeRequestParam, Container<FoldingRange>, FoldingRangeRegistrationOptions>(handler, registrationOptionsFactory));
        }

        public static ILanguageServerRegistry OnFoldingRange(this ILanguageServerRegistry registry, Func<FoldingRangeRequestParam, CancellationToken, Task<Container<FoldingRange>>> handler, Func<FoldingRangeRegistrationOptions> registrationOptionsFactory)
        {
            registrationOptionsFactory ??= () => new FoldingRangeRegistrationOptions();
            return registry.AddHandler(TextDocumentNames.FoldingRange, new LanguageProtocolDelegatingHandlers.RequestRegistration<FoldingRangeRequestParam, Container<FoldingRange>, FoldingRangeRegistrationOptions>(handler, registrationOptionsFactory));
        }

        public static ILanguageServerRegistry OnFoldingRange(this ILanguageServerRegistry registry, Action<FoldingRangeRequestParam, IObserver<IEnumerable<FoldingRange>>> handler)
        {
            registrationOptionsFactory ??= () => new FoldingRangeRegistrationOptions();
            return registry.AddHandler(TextDocumentNames.FoldingRange, _ => new LanguageProtocolDelegatingHandlers.PartialResults<FoldingRangeRequestParam, Container<FoldingRange>, FoldingRange, FoldingRangeRegistrationOptions>(handler, registrationOptionsFactory, _.GetService<IProgressManager>(), values => new Container<FoldingRange>(values)));
        }

        public static ILanguageServerRegistry OnFoldingRange(this ILanguageServerRegistry registry, Action<FoldingRangeRequestParam, IObserver<IEnumerable<FoldingRange>>, CancellationToken> handler)
        {
            registrationOptionsFactory ??= () => new FoldingRangeRegistrationOptions();
            return registry.AddHandler(TextDocumentNames.FoldingRange, _ => new LanguageProtocolDelegatingHandlers.PartialResults<FoldingRangeRequestParam, Container<FoldingRange>, FoldingRange, FoldingRangeRegistrationOptions>(handler, registrationOptionsFactory, _.GetService<IProgressManager>(), values => new Container<FoldingRange>(values)));
        }

        public static ILanguageServerRegistry OnFoldingRange(this ILanguageServerRegistry registry, Action<FoldingRangeRequestParam, IObserver<IEnumerable<FoldingRange>>, FoldingRangeCapability, CancellationToken> handler)
        {
            registrationOptionsFactory ??= _ => new FoldingRangeRegistrationOptions();
            return registry.AddHandler(TextDocumentNames.FoldingRange, _ => new LanguageProtocolDelegatingHandlers.PartialResults<FoldingRangeRequestParam, Container<FoldingRange>, FoldingRange, FoldingRangeRegistrationOptions, FoldingRangeCapability>(handler, registrationOptionsFactory, _.GetService<IProgressManager>(), values => new Container<FoldingRange>(values)));
        }

        public static ILanguageServerRegistry OnFoldingRange(this ILanguageServerRegistry registry, Func<FoldingRangeRequestParam, FoldingRangeCapability, CancellationToken, Task<Container<FoldingRange>>> handler)
        {
            registrationOptionsFactory ??= _ => new FoldingRangeRegistrationOptions();
            return registry.AddHandler(TextDocumentNames.FoldingRange, new LanguageProtocolDelegatingHandlers.Request<FoldingRangeRequestParam, Container<FoldingRange>, FoldingRangeRegistrationOptions, FoldingRangeCapability>(handler, registrationOptionsFactory));
        }

        public static IRequestProgressObservable<IEnumerable<FoldingRange>, Container<FoldingRange>> RequestFoldingRange(this ITextDocumentLanguageClient mediator, FoldingRangeRequestParam request, CancellationToken cancellationToken = default) => mediator.ProgressManager.MonitorUntil(request, value => new Container<FoldingRange>(value), cancellationToken);
        public static IRequestProgressObservable<IEnumerable<FoldingRange>, Container<FoldingRange>> RequestFoldingRange(this ILanguageClient mediator, FoldingRangeRequestParam request, CancellationToken cancellationToken = default) => mediator.ProgressManager.MonitorUntil(request, value => new Container<FoldingRange>(value), cancellationToken);
    }
#nullable restore
}
";
            await AssertGeneratedAsExpected<GenerateHandlerMethodsGenerator>(source, expected);
        }

        [FactWithSkipOn(SkipOnPlatform.Windows)]
        public async Task Supports_Generating_Requests_And_Infers_Direction()
        {
            var source = @"
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OmniSharp.Extensions.JsonRpc;
using OmniSharp.Extensions.JsonRpc.Generation;
using OmniSharp.Extensions.LanguageServer.Protocol;
using OmniSharp.Extensions.LanguageServer.Protocol.Client;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;
using OmniSharp.Extensions.LanguageServer.Protocol.Client.Capabilities;

namespace OmniSharp.Extensions.LanguageServer.Protocol.Test
{
    [Parallel, Method(TextDocumentNames.Definition, Direction.ClientToServer), GenerateHandlerMethods, GenerateRequestMethods, Obsolete(""This is obsolete"")]
    public interface IDefinitionHandler : IJsonRpcRequestHandler<DefinitionParams, LocationOrLocationLinks>, IRegistration<DefinitionRegistrationOptions, DefinitionCapability> { }
}";

            var expected = @"
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OmniSharp.Extensions.JsonRpc;
using OmniSharp.Extensions.JsonRpc.Generation;
using OmniSharp.Extensions.LanguageServer.Protocol;
using OmniSharp.Extensions.LanguageServer.Protocol.Client;
using OmniSharp.Extensions.LanguageServer.Protocol.Client.Capabilities;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Progress;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OmniSharp.Extensions.LanguageServer.Protocol.Test
{
#nullable enable
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, System.Runtime.CompilerServices.CompilerGeneratedAttribute, Obsolete(""This is obsolete"")]
    public static partial class DefinitionExtensions
    {
        public static ILanguageServerRegistry OnDefinition(this ILanguageServerRegistry registry, Func<DefinitionParams, Task<LocationOrLocationLinks>> handler, Func<DefinitionRegistrationOptions> registrationOptionsFactory)
        {
            registrationOptionsFactory ??= () => new DefinitionRegistrationOptions();
            return registry.AddHandler(TextDocumentNames.Definition, new LanguageProtocolDelegatingHandlers.RequestRegistration<DefinitionParams, LocationOrLocationLinks, DefinitionRegistrationOptions>(handler, registrationOptionsFactory));
        }

        public static ILanguageServerRegistry OnDefinition(this ILanguageServerRegistry registry, Func<DefinitionParams, CancellationToken, Task<LocationOrLocationLinks>> handler, Func<DefinitionRegistrationOptions> registrationOptionsFactory)
        {
            registrationOptionsFactory ??= () => new DefinitionRegistrationOptions();
            return registry.AddHandler(TextDocumentNames.Definition, new LanguageProtocolDelegatingHandlers.RequestRegistration<DefinitionParams, LocationOrLocationLinks, DefinitionRegistrationOptions>(handler, registrationOptionsFactory));
        }

        public static ILanguageServerRegistry OnDefinition(this ILanguageServerRegistry registry, Action<DefinitionParams, IObserver<IEnumerable<LocationOrLocationLink>>> handler)
        {
            registrationOptionsFactory ??= () => new DefinitionRegistrationOptions();
            return registry.AddHandler(TextDocumentNames.Definition, _ => new LanguageProtocolDelegatingHandlers.PartialResults<DefinitionParams, LocationOrLocationLinks, LocationOrLocationLink, DefinitionRegistrationOptions>(handler, registrationOptionsFactory, _.GetService<IProgressManager>(), values => new LocationOrLocationLinks(values)));
        }

        public static ILanguageServerRegistry OnDefinition(this ILanguageServerRegistry registry, Action<DefinitionParams, IObserver<IEnumerable<LocationOrLocationLink>>, CancellationToken> handler)
        {
            registrationOptionsFactory ??= () => new DefinitionRegistrationOptions();
            return registry.AddHandler(TextDocumentNames.Definition, _ => new LanguageProtocolDelegatingHandlers.PartialResults<DefinitionParams, LocationOrLocationLinks, LocationOrLocationLink, DefinitionRegistrationOptions>(handler, registrationOptionsFactory, _.GetService<IProgressManager>(), values => new LocationOrLocationLinks(values)));
        }

        public static ILanguageServerRegistry OnDefinition(this ILanguageServerRegistry registry, Action<DefinitionParams, IObserver<IEnumerable<LocationOrLocationLink>>, DefinitionCapability, CancellationToken> handler)
        {
            registrationOptionsFactory ??= _ => new DefinitionRegistrationOptions();
            return registry.AddHandler(TextDocumentNames.Definition, _ => new LanguageProtocolDelegatingHandlers.PartialResults<DefinitionParams, LocationOrLocationLinks, LocationOrLocationLink, DefinitionRegistrationOptions, DefinitionCapability>(handler, registrationOptionsFactory, _.GetService<IProgressManager>(), values => new LocationOrLocationLinks(values)));
        }

        public static ILanguageServerRegistry OnDefinition(this ILanguageServerRegistry registry, Func<DefinitionParams, DefinitionCapability, CancellationToken, Task<LocationOrLocationLinks>> handler)
        {
            registrationOptionsFactory ??= _ => new DefinitionRegistrationOptions();
            return registry.AddHandler(TextDocumentNames.Definition, new LanguageProtocolDelegatingHandlers.Request<DefinitionParams, LocationOrLocationLinks, DefinitionRegistrationOptions, DefinitionCapability>(handler, registrationOptionsFactory));
        }

        public static IRequestProgressObservable<IEnumerable<LocationOrLocationLink>, LocationOrLocationLinks> RequestDefinition(this ILanguageClient mediator, DefinitionParams request, CancellationToken cancellationToken = default) => mediator.ProgressManager.MonitorUntil(request, value => new LocationOrLocationLinks(value), cancellationToken);
    }
#nullable restore
}";
            await AssertGeneratedAsExpected<GenerateHandlerMethodsGenerator>(source, expected);
        }


        [FactWithSkipOn(SkipOnPlatform.Windows)]
        public async Task Supports_Generating_Requests()
        {
            var source = @"
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OmniSharp.Extensions.JsonRpc;
using OmniSharp.Extensions.JsonRpc.Generation;
using OmniSharp.Extensions.LanguageServer.Protocol;
using OmniSharp.Extensions.LanguageServer.Protocol.Client;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;
using OmniSharp.Extensions.LanguageServer.Protocol.Client.Capabilities;

namespace Test
{
    [Parallel, Method(TextDocumentNames.Definition, Direction.ClientToServer), GenerateHandlerMethods(typeof(ILanguageServerRegistry)), GenerateRequestMethods(typeof(ITextDocumentLanguageClient))]
    public interface IDefinitionHandler : IJsonRpcRequestHandler<DefinitionParams, LocationOrLocationLinks>, IRegistration<DefinitionRegistrationOptions, DefinitionCapability> { }
}";

            var expected = @"
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OmniSharp.Extensions.JsonRpc;
using OmniSharp.Extensions.JsonRpc.Generation;
using OmniSharp.Extensions.LanguageServer.Protocol;
using OmniSharp.Extensions.LanguageServer.Protocol.Client;
using OmniSharp.Extensions.LanguageServer.Protocol.Client.Capabilities;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Progress;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
#nullable enable
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, System.Runtime.CompilerServices.CompilerGeneratedAttribute]
    public static partial class DefinitionExtensions
    {
        public static ILanguageServerRegistry OnDefinition(this ILanguageServerRegistry registry, Func<DefinitionParams, Task<LocationOrLocationLinks>> handler, Func<DefinitionRegistrationOptions> registrationOptionsFactory)
        {
            registrationOptionsFactory ??= () => new DefinitionRegistrationOptions();
            return registry.AddHandler(TextDocumentNames.Definition, new LanguageProtocolDelegatingHandlers.RequestRegistration<DefinitionParams, LocationOrLocationLinks, DefinitionRegistrationOptions>(handler, registrationOptionsFactory));
        }

        public static ILanguageServerRegistry OnDefinition(this ILanguageServerRegistry registry, Func<DefinitionParams, CancellationToken, Task<LocationOrLocationLinks>> handler, Func<DefinitionRegistrationOptions> registrationOptionsFactory)
        {
            registrationOptionsFactory ??= () => new DefinitionRegistrationOptions();
            return registry.AddHandler(TextDocumentNames.Definition, new LanguageProtocolDelegatingHandlers.RequestRegistration<DefinitionParams, LocationOrLocationLinks, DefinitionRegistrationOptions>(handler, registrationOptionsFactory));
        }

        public static ILanguageServerRegistry OnDefinition(this ILanguageServerRegistry registry, Action<DefinitionParams, IObserver<IEnumerable<LocationOrLocationLink>>> handler)
        {
            registrationOptionsFactory ??= () => new DefinitionRegistrationOptions();
            return registry.AddHandler(TextDocumentNames.Definition, _ => new LanguageProtocolDelegatingHandlers.PartialResults<DefinitionParams, LocationOrLocationLinks, LocationOrLocationLink, DefinitionRegistrationOptions>(handler, registrationOptionsFactory, _.GetService<IProgressManager>(), values => new LocationOrLocationLinks(values)));
        }

        public static ILanguageServerRegistry OnDefinition(this ILanguageServerRegistry registry, Action<DefinitionParams, IObserver<IEnumerable<LocationOrLocationLink>>, CancellationToken> handler)
        {
            registrationOptionsFactory ??= () => new DefinitionRegistrationOptions();
            return registry.AddHandler(TextDocumentNames.Definition, _ => new LanguageProtocolDelegatingHandlers.PartialResults<DefinitionParams, LocationOrLocationLinks, LocationOrLocationLink, DefinitionRegistrationOptions>(handler, registrationOptionsFactory, _.GetService<IProgressManager>(), values => new LocationOrLocationLinks(values)));
        }

        public static ILanguageServerRegistry OnDefinition(this ILanguageServerRegistry registry, Action<DefinitionParams, IObserver<IEnumerable<LocationOrLocationLink>>, DefinitionCapability, CancellationToken> handler)
        {
            registrationOptionsFactory ??= _ => new DefinitionRegistrationOptions();
            return registry.AddHandler(TextDocumentNames.Definition, _ => new LanguageProtocolDelegatingHandlers.PartialResults<DefinitionParams, LocationOrLocationLinks, LocationOrLocationLink, DefinitionRegistrationOptions, DefinitionCapability>(handler, registrationOptionsFactory, _.GetService<IProgressManager>(), values => new LocationOrLocationLinks(values)));
        }

        public static ILanguageServerRegistry OnDefinition(this ILanguageServerRegistry registry, Func<DefinitionParams, DefinitionCapability, CancellationToken, Task<LocationOrLocationLinks>> handler)
        {
            registrationOptionsFactory ??= _ => new DefinitionRegistrationOptions();
            return registry.AddHandler(TextDocumentNames.Definition, new LanguageProtocolDelegatingHandlers.Request<DefinitionParams, LocationOrLocationLinks, DefinitionRegistrationOptions, DefinitionCapability>(handler, registrationOptionsFactory));
        }

        public static IRequestProgressObservable<IEnumerable<LocationOrLocationLink>, LocationOrLocationLinks> RequestDefinition(this ITextDocumentLanguageClient mediator, DefinitionParams request, CancellationToken cancellationToken = default) => mediator.ProgressManager.MonitorUntil(request, value => new LocationOrLocationLinks(value), cancellationToken);
    }
#nullable restore
}";
            await AssertGeneratedAsExpected<GenerateHandlerMethodsGenerator>(source, expected);
        }

        [FactWithSkipOn(SkipOnPlatform.Windows)]
        public async Task Supports_Custom_Method_Names()
        {
            var source = @"
using System;
using System.Threading;
using System.Threading.Tasks;
using OmniSharp.Extensions.JsonRpc;
using OmniSharp.Extensions.JsonRpc.Generation;
using OmniSharp.Extensions.LanguageServer.Protocol;
using OmniSharp.Extensions.LanguageServer.Protocol.Client;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;

namespace Test
{
    [Serial, Method(GeneralNames.Initialize, Direction.ClientToServer), GenerateHandlerMethods(typeof(ILanguageServerRegistry), MethodName = ""OnLanguageProtocolInitialize""), GenerateRequestMethods(typeof(ITextDocumentLanguageClient), MethodName = ""RequestLanguageProtocolInitialize"")]
    public interface ILanguageProtocolInitializeHandler : IJsonRpcRequestHandler<InitializeParams, InitializeResult> {}
}";
            var expected = @"
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OmniSharp.Extensions.JsonRpc;
using OmniSharp.Extensions.JsonRpc.Generation;
using OmniSharp.Extensions.LanguageServer.Protocol;
using OmniSharp.Extensions.LanguageServer.Protocol.Client;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
#nullable enable
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, System.Runtime.CompilerServices.CompilerGeneratedAttribute]
    public static partial class LanguageProtocolInitializeExtensions
    {
        public static ILanguageServerRegistry OnLanguageProtocolInitialize(this ILanguageServerRegistry registry, Func<InitializeParams, Task<InitializeResult>> handler) => registry.AddHandler(GeneralNames.Initialize, RequestHandler.For(handler));
        public static ILanguageServerRegistry OnLanguageProtocolInitialize(this ILanguageServerRegistry registry, Func<InitializeParams, CancellationToken, Task<InitializeResult>> handler) => registry.AddHandler(GeneralNames.Initialize, RequestHandler.For(handler));
        public static Task<InitializeResult> RequestLanguageProtocolInitialize(this ITextDocumentLanguageClient mediator, InitializeParams request, CancellationToken cancellationToken = default) => mediator.SendRequest(request, cancellationToken);
    }
#nullable restore
}";
            await AssertGeneratedAsExpected<GenerateHandlerMethodsGenerator>(source, expected);
        }

        [FactWithSkipOn(SkipOnPlatform.Windows)]
        public async Task Supports_Allow_Derived_Requests()
        {
            var source = @"
using System;
using System.Threading;
using System.Threading.Tasks;
using OmniSharp.Extensions.JsonRpc;
using OmniSharp.Extensions.JsonRpc.Generation;
using System.Collections.Generic;
using MediatR;

namespace OmniSharp.Extensions.DebugAdapter.Protocol.Requests
{
    [Parallel, Method(RequestNames.Attach, Direction.ClientToServer)]
    [GenerateHandlerMethods(AllowDerivedRequests = true), GenerateRequestMethods]
    public interface IAttachHandler : IJsonRpcRequestHandler<AttachRequestArguments, AttachResponse> { }
}";
            var expected = @"
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OmniSharp.Extensions.DebugAdapter.Protocol;
using OmniSharp.Extensions.DebugAdapter.Protocol.Client;
using OmniSharp.Extensions.DebugAdapter.Protocol.Server;
using OmniSharp.Extensions.JsonRpc;
using OmniSharp.Extensions.JsonRpc.Generation;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OmniSharp.Extensions.DebugAdapter.Protocol.Requests
{
#nullable enable
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, System.Runtime.CompilerServices.CompilerGeneratedAttribute]
    public static partial class AttachExtensions
    {
        public static IDebugAdapterServerRegistry OnAttach(this IDebugAdapterServerRegistry registry, Func<AttachRequestArguments, Task<AttachResponse>> handler) => registry.AddHandler(RequestNames.Attach, RequestHandler.For(handler));
        public static IDebugAdapterServerRegistry OnAttach(this IDebugAdapterServerRegistry registry, Func<AttachRequestArguments, CancellationToken, Task<AttachResponse>> handler) => registry.AddHandler(RequestNames.Attach, RequestHandler.For(handler));
        public static IDebugAdapterServerRegistry OnAttach<T>(this IDebugAdapterServerRegistry registry, Func<T, Task<AttachResponse>> handler)
            where T : AttachRequestArguments => registry.AddHandler(RequestNames.Attach, RequestHandler.For(handler));
        public static IDebugAdapterServerRegistry OnAttach<T>(this IDebugAdapterServerRegistry registry, Func<T, CancellationToken, Task<AttachResponse>> handler)
            where T : AttachRequestArguments => registry.AddHandler(RequestNames.Attach, RequestHandler.For(handler));
        public static Task<AttachResponse> RequestAttach(this IDebugAdapterClient mediator, AttachRequestArguments request, CancellationToken cancellationToken = default) => mediator.SendRequest(request, cancellationToken);
    }
#nullable restore
}";
            await AssertGeneratedAsExpected<GenerateHandlerMethodsGenerator>(source, expected);
        }

        [Fact]
        public async Task Supports_Allows_Nullable_Responses()
        {
            var source = @"
using System;
using System.Threading;
using System.Threading.Tasks;
using OmniSharp.Extensions.JsonRpc;
using OmniSharp.Extensions.JsonRpc.Generation;
using System.Collections.Generic;
using MediatR;

namespace OmniSharp.Extensions.DebugAdapter.Protocol.Requests
{
#nullable enable
    [Parallel, Method(RequestNames.Attach, Direction.ClientToServer)]
    [GenerateHandlerMethods(AllowDerivedRequests = true), GenerateRequestMethods]
    public interface IAttachHandler : IJsonRpcRequestHandler<AttachRequestArguments, AttachResponse?> { }
#nullable restore
}";
            var expected = @"
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OmniSharp.Extensions.DebugAdapter.Protocol;
using OmniSharp.Extensions.DebugAdapter.Protocol.Client;
using OmniSharp.Extensions.DebugAdapter.Protocol.Server;
using OmniSharp.Extensions.JsonRpc;
using OmniSharp.Extensions.JsonRpc.Generation;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OmniSharp.Extensions.DebugAdapter.Protocol.Requests
{
#nullable enable
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, System.Runtime.CompilerServices.CompilerGeneratedAttribute]
    public static partial class AttachExtensions
    {
        public static IDebugAdapterServerRegistry OnAttach(this IDebugAdapterServerRegistry registry, Func<AttachRequestArguments, Task<AttachResponse?>> handler) => registry.AddHandler(RequestNames.Attach, RequestHandler.For(handler));
        public static IDebugAdapterServerRegistry OnAttach(this IDebugAdapterServerRegistry registry, Func<AttachRequestArguments, CancellationToken, Task<AttachResponse?>> handler) => registry.AddHandler(RequestNames.Attach, RequestHandler.For(handler));
        public static IDebugAdapterServerRegistry OnAttach<T>(this IDebugAdapterServerRegistry registry, Func<T, Task<AttachResponse?>> handler)
            where T : AttachRequestArguments => registry.AddHandler(RequestNames.Attach, RequestHandler.For(handler));
        public static IDebugAdapterServerRegistry OnAttach<T>(this IDebugAdapterServerRegistry registry, Func<T, CancellationToken, Task<AttachResponse?>> handler)
            where T : AttachRequestArguments => registry.AddHandler(RequestNames.Attach, RequestHandler.For(handler));
        public static Task<AttachResponse?> RequestAttach(this IDebugAdapterClient mediator, AttachRequestArguments request, CancellationToken cancellationToken = default) => mediator.SendRequest(request, cancellationToken);
    }
#nullable restore
}";
            await AssertGeneratedAsExpected<GenerateHandlerMethodsGenerator>(source, expected);
        }

        [FactWithSkipOn(SkipOnPlatform.Windows)]
        public async Task Supports_Allow_Generic_Types()
        {
            var source = @"
using System;
using System.Threading;
using System.Threading.Tasks;
using OmniSharp.Extensions.JsonRpc;
using OmniSharp.Extensions.JsonRpc.Generation;
using System.Collections.Generic;
using MediatR;

namespace OmniSharp.Extensions.DebugAdapter.Protocol.Bogus
{
    public class AttachResponse { }
    [Method(""attach"", Direction.ClientToServer)]
    public class AttachRequestArguments: IRequest<AttachResponse> { }

    [Parallel, Method(""attach"", Direction.ClientToServer)]
    [GenerateHandlerMethods(AllowDerivedRequests = true), GenerateRequestMethods]
    public interface IAttachHandler<in T> : IJsonRpcRequestHandler<T, AttachResponse> where T : AttachRequestArguments { }
    public interface IAttachHandler : IAttachHandler<AttachRequestArguments> { }
}";
            var expected = @"
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OmniSharp.Extensions.DebugAdapter.Protocol;
using OmniSharp.Extensions.DebugAdapter.Protocol.Client;
using OmniSharp.Extensions.DebugAdapter.Protocol.Server;
using OmniSharp.Extensions.JsonRpc;
using OmniSharp.Extensions.JsonRpc.Generation;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OmniSharp.Extensions.DebugAdapter.Protocol.Bogus
{
#nullable enable
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, System.Runtime.CompilerServices.CompilerGeneratedAttribute]
    public static partial class AttachExtensions
    {
        public static IDebugAdapterServerRegistry OnAttach(this IDebugAdapterServerRegistry registry, Func<AttachRequestArguments, Task<AttachResponse>> handler) => registry.AddHandler(""attach"", RequestHandler.For(handler));
        public static IDebugAdapterServerRegistry OnAttach(this IDebugAdapterServerRegistry registry, Func<AttachRequestArguments, CancellationToken, Task<AttachResponse>> handler) => registry.AddHandler(""attach"", RequestHandler.For(handler));
        public static IDebugAdapterServerRegistry OnAttach<T>(this IDebugAdapterServerRegistry registry, Func<T, Task<AttachResponse>> handler)
            where T : AttachRequestArguments => registry.AddHandler(""attach"", RequestHandler.For(handler));
        public static IDebugAdapterServerRegistry OnAttach<T>(this IDebugAdapterServerRegistry registry, Func<T, CancellationToken, Task<AttachResponse>> handler)
            where T : AttachRequestArguments => registry.AddHandler(""attach"", RequestHandler.For(handler));
        public static Task<AttachResponse> RequestAttach(this IDebugAdapterClient mediator, AttachRequestArguments request, CancellationToken cancellationToken = default) => mediator.SendRequest(request, cancellationToken);
    }
#nullable restore
}";
            await AssertGeneratedAsExpected<GenerateHandlerMethodsGenerator>(source, expected);
        }
    }
}
