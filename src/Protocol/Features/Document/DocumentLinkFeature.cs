using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using DryIoc;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using OmniSharp.Extensions.JsonRpc;
using OmniSharp.Extensions.JsonRpc.Generation;
using OmniSharp.Extensions.LanguageServer.Protocol.Client;
using OmniSharp.Extensions.LanguageServer.Protocol.Client.Capabilities;
using OmniSharp.Extensions.LanguageServer.Protocol.Document;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Progress;
using OmniSharp.Extensions.LanguageServer.Protocol.Serialization;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;
using OmniSharp.Extensions.LanguageServer.Protocol.Server.Capabilities;

// ReSharper disable once CheckNamespace
namespace OmniSharp.Extensions.LanguageServer.Protocol
{
    namespace Models
    {
        [Parallel]
        [Method(TextDocumentNames.DocumentLink, Direction.ClientToServer)]
        [
            GenerateHandler("OmniSharp.Extensions.LanguageServer.Protocol.Document"),
            GenerateHandlerMethods,
            GenerateRequestMethods(typeof(ITextDocumentLanguageClient), typeof(ILanguageClient))
        ]
        [RegistrationOptions(typeof(DocumentLinkRegistrationOptions)), Capability(typeof(DocumentLinkCapability)), Resolver(typeof(DocumentLink))]
        public partial class DocumentLinkParams : ITextDocumentIdentifierParams, IPartialItemsRequest<DocumentLinkContainer, DocumentLink>, IWorkDoneProgressParams
        {
            /// <summary>
            /// The document to provide document links for.
            /// </summary>
            public TextDocumentIdentifier TextDocument { get; set; } = null!;
        }

        /// <summary>
        /// A document link is a range in a text document that links to an internal or external resource, like another
        /// text document or a web site.
        /// </summary>
        [Parallel]
        [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
        [Method(TextDocumentNames.DocumentLinkResolve, Direction.ClientToServer)]
        [
            GenerateHandler("OmniSharp.Extensions.LanguageServer.Protocol.Document", Name = "DocumentLinkResolve"),
            GenerateHandlerMethods,
            GenerateRequestMethods(typeof(ITextDocumentLanguageClient), typeof(ILanguageClient)),
            GenerateTypedData,
            GenerateContainer
        ]
        [RegistrationOptions(typeof(DocumentLinkRegistrationOptions)), Capability(typeof(DocumentLinkCapability))]
        public partial class DocumentLink : ICanBeResolved, IRequest<DocumentLink>
        {
            /// <summary>
            /// The range this link applies to.
            /// </summary>
            public Range Range { get; set; } = null!;

            /// <summary>
            /// The uri this link points to. If missing a resolve request is sent later.
            /// </summary>
            [Optional]
            public DocumentUri? Target { get; set; }

            /// <summary>
            /// A data entry field that is preserved on a document link between a
            /// DocumentLinkRequest and a DocumentLinkResolveRequest.
            /// </summary>
            [Optional]
            public JToken? Data { get; set; }

            /// <summary>
            /// The tooltip text when you hover over this link.
            ///
            /// If a tooltip is provided, is will be displayed in a string that includes instructions on how to
            /// trigger the link, such as `{0} (ctrl + click)`. The specific instructions vary depending on OS,
            /// user settings, and localization.
            ///
            /// @since 3.15.0
            /// </summary>
            [Optional]
            public string? Tooltip { get; set; }

            private string DebuggerDisplay => $"{Range}{( Target is not null ? $" {Target}" : "" )}{( string.IsNullOrWhiteSpace(Tooltip) ? $" {Tooltip}" : "" )}";

            /// <inheritdoc />
            public override string ToString() => DebuggerDisplay;
        }

        [GenerateRegistrationOptions(nameof(ServerCapabilities.DocumentLinkProvider))]
        [RegistrationOptionsConverter(typeof(DocumentLinkRegistrationOptionsConverter))]
        public partial class DocumentLinkRegistrationOptions : ITextDocumentRegistrationOptions, IWorkDoneProgressOptions
        {
            /// <summary>
            /// Document links have a resolve provider as well.
            /// </summary>
            [Optional]
            public bool ResolveProvider { get; set; }

            class DocumentLinkRegistrationOptionsConverter : RegistrationOptionsConverterBase<DocumentLinkRegistrationOptions, StaticOptions>
            {
                private readonly IHandlersManager _handlersManager;

                public DocumentLinkRegistrationOptionsConverter(IHandlersManager handlersManager) : base(nameof(ServerCapabilities.DocumentLinkProvider))
                {
                    _handlersManager = handlersManager;
                }

                public override StaticOptions Convert(DocumentLinkRegistrationOptions source) => new() {
                    ResolveProvider = source.ResolveProvider || _handlersManager.Descriptors.Any(z => z.HandlerType == typeof(IDocumentLinkResolveHandler)),
                    WorkDoneProgress = source.WorkDoneProgress,
                };
            }
        }
    }

    namespace Client.Capabilities
    {
        [CapabilityKey(nameof(ClientCapabilities.TextDocument), nameof(TextDocumentClientCapabilities.DocumentLink))]
        public partial class DocumentLinkCapability : DynamicCapability, ConnectedCapability<IDocumentLinkHandler>
        {
            /// <summary>
            /// Whether the client support the `tooltip` property on `DocumentLink`.
            ///
            /// @since 3.15.0
            /// </summary>
            [Optional]
            public bool TooltipSupport { get; set; }
        }
    }

    namespace Document
    {
    }
}