using BlogyBackend.Models;

namespace BlogyBackend.Interfaces;
public interface IPerson
{
    public void Add(User user);
    public IPerson Get(string username);
}