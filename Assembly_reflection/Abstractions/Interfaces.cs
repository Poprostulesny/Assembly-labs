namespace Assembly_Reflection.Abstractions;

// Użyjesz tego w zadaniu 5
public interface IRepository<T>
{
    T? GetById(int id);
    void Save(T entity);
}

// Przykładowe implementacje:
public class UserRepository : IRepository<Models.User>
{
    // TODO: implementacja może być pusta / rzucać NotImplementedException,
    // ważne jest tylko to, że klasa implementuje IRepository<User>
    public Models.User? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Save(Models.User entity)
    {
        throw new NotImplementedException();
    }
}

// Jeszcze jedna, dla różnorodności:
public class JobRepository : IRepository<Models.JobBase>
{
    public Models.JobBase? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Save(Models.JobBase entity)
    {
        throw new NotImplementedException();
    }
}