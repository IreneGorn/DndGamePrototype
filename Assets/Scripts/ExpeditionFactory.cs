public interface IExpeditionFactory
{
    IExpedition CreateExpedition(string expeditionType);
}

public class ExpeditionFactory : IExpeditionFactory
{
    private readonly IDiceRollingService _diceRollingService;

    public ExpeditionFactory(IDiceRollingService diceRollingService)
    {
        _diceRollingService = diceRollingService;
    }

    public IExpedition CreateExpedition(string expeditionType)
    {
        switch (expeditionType.ToLower())
        {
            case "lockpicking":
                return new LockpickingExpedition(_diceRollingService);
            default:
                throw new System.ArgumentException($"Unknown expedition type: {expeditionType}");
        }
    }
}