using Thread = Diskusjonsforum.Models.Thread;

namespace diskusjonsforum.DAL;

public interface IThreadRepository
{
    IEnumerable<Thread> GetAll();
    Thread GetThreadById(int threadId);
    Task <bool> Add(Thread thread);
    Task<bool>Update(Thread thread);
    Task Remove(Thread thread);
    Task SaveChangesAsync();
}
