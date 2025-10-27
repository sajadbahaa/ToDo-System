using DTLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Repositary.GrenricRepo;

public class Repo<T, TKey> : IRepository<T, TKey> where T : class
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;
    private IDbContextTransaction? _transaction;

    protected Repo(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    // Add Entity
    public async Task<bool> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        return await _context.SaveChangesAsync()>0;
        ;
    }

    // Delete Entity by Key
    public virtual Task<bool> DeleteAsync(TKey id)
    {
        throw new NotImplementedException();
    }

    // Get All Entities
    public virtual async Task<List<T>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    // Get Entity by Key
    public virtual Task<T?> GetByIdAsync(TKey id)
    {
        // Use reflection to find entity by Id property
        throw new NotImplementedException();
    }

    // Update Entity
    public virtual async Task<bool> UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        var rows = await _context.SaveChangesAsync();
        return rows > 0;
    }
}
