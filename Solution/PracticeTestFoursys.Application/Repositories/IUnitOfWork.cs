namespace PracticeTestFoursys.Application.Repositories {
    public interface IUnitOfWork : IDisposable {
        Task<int> CommitAsync();
    }
}
