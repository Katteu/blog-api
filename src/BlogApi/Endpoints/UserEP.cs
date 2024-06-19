using UserModel;

namespace UserEndpoint;
public class UserEP{

    public static List<User> Users = new List<User>();
    
    public static List<User> GetAllUsers(){
        return Users;
    }

    public static IResult GetUser(int id){
        User user = Users.Find(user => user.Id == id);

        if (user is null){
            return Results.NotFound("User specified does not exist.");
        }

        return Results.Ok(user);
    }

    public static IResult CreateUser(User user){
        int currId = Users.Count == 0 ? 0 : Users.Max(user => user.Id);
        user.Id = currId + 1;

        User usernameExists = Users.Find(u => u.Username == user.Username);

        if(usernameExists is not null){
            return Results.Conflict("Username is already taken.");
        }

        Users.Add(user);
        return Results.Ok(user);
    }

    public static IResult UpdateUser(User updatedUser,int id){
        User user = Users.Find(user => user.Id == id);
        User usernameExists = Users.Find(u => u.Username == user.Username);

        if (user is null){
            return Results.NotFound("User to be updated does not exist.");
        }

        if(usernameExists is not null){
            return Results.Conflict("Username is already taken.");
        }

        user.Username = updatedUser.Username;
        user.Password = updatedUser.Username;

        return Results.Ok(user);
    }

    public static IResult DeleteUser(int id){
        User user = Users.Find(user => user.Id == id);

        if (user is null){
            return Results.NotFound("User to be removed does not exist.");
        }
        
        Users.Remove(user);
        return Results.Ok(user);
    }

}