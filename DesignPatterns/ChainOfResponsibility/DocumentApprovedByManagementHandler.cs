using System.ComponentModel.DataAnnotations;

namespace ChainOfResponsibility
{
    public class DocumentApprovedByManagementHandler : IHandler<Document>
    {
        private IHandler<Document>? _successor;
        public IHandler<Document> SetSuccessor(IHandler<Document> successor)
        {
            _successor = successor;
            return successor;
        }

        public void Handle(Document document)
        {
            if (!document.ApprovedByManagement)
            {
                //validation doesn't check out
                throw new ValidationException(
                    new ValidationResult(
                        "Document must be approved by management",
                        new List<string>() {"ApprovedByManagement"}), null, null);
            }

            // go to the next handler
            _successor?.Handle(document);
        }
    }
}
