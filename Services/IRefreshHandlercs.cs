namespace Application_test_repo.Services
{
    public interface IRefreshHandlercs
    {
        Task<string> GenerateToken(string username);
    }
}
