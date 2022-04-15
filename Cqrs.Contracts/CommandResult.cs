namespace TaxCalculator.Cqrs.Contracts;

public class CommandResult
{
    public CommandStatus Status { get; set; }
    
    public Guid? RecordId { get; set; }
    
    public static CommandResult Success(Guid? recordId = null)
    {
        return new CommandResult
        {
            Status = CommandStatus.Success,
            RecordId = recordId
        };
    }
    
    public static CommandResult Fail(Guid? recordId = null)
    {
        return new CommandResult
        {
            Status = CommandStatus.Fail,
            RecordId = recordId
        };
    }
}