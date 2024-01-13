namespace AuctionPlatform.Business._01_Common
{
    /// <summary>
    /// This interface is used for Reflection, to automate the process of Dependency Injection at the Business Layer.
    /// RegisterDependencyInjectionExtension class registers every non-abstract class that implements IService as transient services.
    /// Every interface added to Business Layer should implement this interface
    /// </summary>
    public interface IService
    {
    }
}
