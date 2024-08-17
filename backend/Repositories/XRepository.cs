using backend.Migrations;
using backend.Models;

namespace backend.Repositories;

public class XRepository : IXRepository 
{
    private readonly XDbContext _context;

    public XRepository(XDbContext context)
    {
        _context = context;
    }

    public IEnumerable<X> GetUserPosts(string username)
    {
        return _context.X.Where(x => x.Username == username).ToList();
    }

    public X CreateX(X newX)
    {
        _context.X.Add(newX);
        _context.SaveChanges();
        return newX;
    }

    public void DeleteXById(string xId)
    {
        var x = _context.X.Find(xId);
        if (x != null) {
            _context.X.Remove(x); 
            _context.SaveChanges(); 
        }
    }

    public IEnumerable<X> GetAllX()
    {
        return _context.X.ToList();
    }

    public X? GetXById(string xId)
    {
        return _context.X.SingleOrDefault(c => c.XId == xId);
    }

    public X? UpdateX(X newX)
    {
        var originalX = _context.X.Find(newX.XId);
        if (originalX != null) {
            originalX.Text = newX.Text;
            originalX.Username = newX.Username;
            _context.SaveChanges();
        }
        return originalX;
    }
}