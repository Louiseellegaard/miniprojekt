using Microsoft.EntityFrameworkCore;
using System.Text.Json;

using Data;
using Model;

namespace Service;

public class DataService
{
    private TodoContext db { get; }

    public DataService(TodoContext db) {
        this.db = db;
    }
    /// <summary>
    /// Seeder noget nyt data i databasen hvis det er nødvendigt.
    /// </summary>
    public void SeedData() {
        Question question = db.Users.FirstOrDefault()!;
        if (question == null) {
            question = new Question("Kristian", "hjo", DateTime.Now, 12, 23);
            db.Users.Add(new Que("heidi", "hjo", DateTime.Now, 12, 23));
            db.Users.Add(new User("Kristipwlsan", "hjo", DateTime.Now, 12, 23));
        }

        Subject task = db.Subjects.FirstOrDefault()!;
        if (Subject == null)
        {
            db.Tasks.Add(new Subject("Husk denne opgave"));
        }

        db.SaveChanges();
    }

    public List<TodoTask> GetTasks() {
        return db.Tasks
            .Include(task => task.User)
            .ToList();
    }

    public TodoTask GetTaskById(int id) {
        var task = db
            .Tasks
            .Where(task => task.TodoTaskId == id)
            .Include(t => t.User)
            .First();
        return task;
    }

    public string CreateTask(string text, bool done, int userId) {
        User user = db.Users.Where(user => user.UserId == userId).First();
        TodoTask task = new TodoTask(text, done, user);
        db.Tasks.Add(task);
        db.SaveChanges();
        return JsonSerializer.Serialize(
            new { msg = "New task created", newTask = task }
        );
    }

    public DbSet<User> GetUsers() {
        return db.Users;
    }

    public string CreateUser(string name) {
        var user = new User(name);
        db.Users.Add(user);
        db.SaveChanges();
        return JsonSerializer.Serialize(
            new { msg = "New user created", newUser = user });
    }

    public User GetUserById(int id)
    {
        var User = db
            .Users
            .Where(User => User.UserId == id)
            .Include(t => t.Tasks)
            .First();
        return User;
    }
}