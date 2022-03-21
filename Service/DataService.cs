using Microsoft.EntityFrameworkCore;
using System.Text.Json;

using Data;
using Model;

namespace Service;

public class DataService
{
    private QuestionContext db { get; }

    public DataService(QuestionContext db) {
        this.db = db;
    }
    /// <summary>
    /// Seeder noget nyt data i databasen hvis det er nødvendigt.
    /// </summary>
    public void SeedData() {
        Question question = db.Questions.FirstOrDefault()!;
        if (question == null) {
            question = new Question("Kristian", "Hvad er en Api", DateTime.Now, 12, 23);
            db.Questions.Add(new Question("heidi", "Mcd i dag?", DateTime.Now, 12, 23));
            db.Questions.Add(new Question("Kristipwlsan", "Hjælp mig", DateTime.Now, 12, 23));
        }

        Subject subject = db.Subjects.FirstOrDefault()!;
        if (subject == null)
        {
            db.Subjects.Add(new Subject("C#"));
        }

        db.SaveChanges();
    }

    public List<Question> GetQuestion() {
        return db.Questions
            .Include(question => question.Subject)
            .ToList();
    }

    public Question GetQuestionById(int id) {
        var question = db
            .Questions
            .Where(question => question.QuestionId == id)
            .Include(q => q.Subject)
            .First();
        return question;
    }

    public string CreateQuestion(string name, string text, DateTime date, int upvote, int downvote) {
        Subject subject = db.Subjects.Where(subject => subject.subjectId == subjectId).First();
        Question task = new Question(text, name, date, upvote, downvote);
        db.Tasks.Add(task);
        db.SaveChanges();
        return JsonSerializer.Serialize(
            new { msg = "New task created", newTask = task }
        );
    }

    public DbSet<Subject> GetSubjects() {
        return db.Subjects;
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