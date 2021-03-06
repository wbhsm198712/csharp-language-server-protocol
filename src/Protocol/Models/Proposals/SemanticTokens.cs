﻿using System;
using System.Collections.Immutable;
using OmniSharp.Extensions.LanguageServer.Protocol.Serialization;

namespace OmniSharp.Extensions.LanguageServer.Protocol.Models.Proposals
{
    /// <summary>
    /// @since 3.16.0
    /// </summary>
    [Obsolete(Constants.Proposal)]
    public class SemanticTokens : ISemanticTokenResult
    {
        public SemanticTokens()
        {

        }

        public SemanticTokens(SemanticTokensPartialResult partialResult)
        {
            Data = partialResult.Data;
        }

        /// <summary>
        /// An optional result id. If provided and clients support delta updating
        /// the client will include the result id in the next semantic token request.
        /// A server can then instead of computing all semantic tokens again simply
        /// send a delta.
        /// </summary>
        [Optional]
        public string? ResultId { get; set; }

        /// <summary>
        /// The actual tokens. For a detailed description about how the data is
        /// structured pls see
        /// https://github.com/microsoft/vscode-extension-samples/blob/5ae1f7787122812dcc84e37427ca90af5ee09f14/semantic-tokens-sample/vscode.proposed.d.ts#L71
        /// </summary>
        /// <remarks>
        /// <see cref="uint"/> in the LSP spec
        /// </remarks>
        public ImmutableArray<int> Data { get; set; } = ImmutableArray<int>.Empty;
    }
}
