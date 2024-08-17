using System.Collections.Generic;
using backend.Models;

namespace backend.Repositories;
public interface IXRepository
{
    IEnumerable<X> GetAllX();
    X? GetXById(string xId);
    X CreateX(X newX);
    X? UpdateX(X newX);
    void DeleteXById(string xId);
    IEnumerable<X> GetUserPosts(string username);
}