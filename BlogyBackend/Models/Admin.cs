using BlogyBackend.Interfaces;
namespace BlogyBackend.Models;
//TODO inherite from user class
public partial class Admin : User, IPerson
{
    public Admin()
    {
        Comments = new HashSet<Comment>();
        Plans = new HashSet<Plan>();
        Posts = new HashSet<Post>();
    }


}