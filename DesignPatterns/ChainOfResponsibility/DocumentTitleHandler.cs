using System.ComponentModel.DataAnnotations;

namespace ChainOfResponsibility
{
    internal class DocumentTitleHandler : IHandler<Document>
    {
        private IHandler<Document>? _successor;

        public IHandler<Document> SetSuccessor(IHandler<Document> successor)
        {
            _successor = successor;
            return successor;
        }

        public void Handle(Document document)
        {
            if (document.Title == string.Empty)
            {
                // validation doesn't check out
                throw new ValidationException(
                    new ValidationResult(
                        "Title must be filled out",
                        new List<string>() { "Title"}), null, null);
            }

            // go to the next handler
            _successor?.Handle(document);
        }
    }
}
