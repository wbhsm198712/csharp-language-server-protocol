using MediatR;
using OmniSharp.Extensions.JsonRpc;

namespace OmniSharp.Extensions.LanguageServer.Protocol.Models
{
    /// <summary>
    /// The parameters send in a will save text document notification.
    /// </summary>
    [Method(TextDocumentNames.WillSave, Direction.ClientToServer)]
    public class WillSaveTextDocumentParams : IRequest
    {
        /// <summary>
        /// The document that will be saved.
        /// </summary>
        public TextDocumentIdentifier TextDocument { get; set; } = null!;

        /// <summary>
        /// The 'TextDocumentSaveReason'.
        /// </summary>
        public TextDocumentSaveReason Reason { get; set; }
    }
}
