using ChainOfResponsibility;

Console.Title = "Chain of Responsibilty";

var validDocument = new Document("How to Avoid Java Development",
    DateTimeOffset.UtcNow, true, true);

var invalidDocument = new Document("How to Avoid Java Development",
    DateTimeOffset.UtcNow, false, true);

var documentHandlerChain = new DocumentTitleHandler();
documentHandlerChain
    .SetSuccessor(new DocumentLastModifiedHandler())
    .SetSuccessor(new DocumentApprovedByLitigationHandler())
    .SetSuccessor(new DocumentApprovedByManagementHandler());

try
{
    documentHandlerChain.Handle(validDocument);
    Console.WriteLine("Valid document is valid.");
    documentHandlerChain.Handle(invalidDocument);
    Console.WriteLine("InValid document is valid.");
}
catch (Exception e)
{
    Console.WriteLine(e);
}

