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
        Question question = db.Questions.FirstOrDefault()!;
        if (question == null) {
            question = new Question("Kristian", "Hvordan koder man?", DateTime.Now, 12, 23);
            db.Questions.Add(new Question("heidi", "Hvordan får man en kæreste?", DateTime.Now, 12, 23));
            db.Questions.Add(new Question("Louise", "Hvordan bager en kage?", DateTime.Now, 12, 23));
        }

        Subject subject = db.Subjects.FirstOrDefault()!;
        if (subject == null)
        {
            db.Subjects.Add(new Subject("C#"));
        }

        db.SaveChanges();
    }

    public List<Question> GetQuesions() {
        return db.Questions
            .ToList();
    }

    public Question GetQuestionById(int id) {
        var question = db
            .Questions
            .Where(question => question.QuestionId == id)
            .Include(t => t.Subject)
            .First();
        return question;
    }

    public string CreateQuestion(string name, string text, DateTime date, int downvote, int upvote) {
        Subject subject = db.Subjects.Where(subject => Subject.SubjectId == subjectId).First();
        Question task = new Question(text, name, date, upvote, downvote);
        db.Questions.Add(task);
        db.SaveChanges();
        return JsonSerializer.Serialize(
            new { msg = "New task created", newQuestion = question }
        );
    }

    public DbSet<User> GetUsers() {
        return db.Users;
    }

    public string CreateUser(string name) {
        var user = new User(name);
        db.Subjects.Add(user);
        db.SaveChanges();
        return JsonSerializer.Serialize(
            new { msg = "New user created", newUser = user });
    }

    public Subject> GetSubjectById(int id)
    {
        var User = db
            .su
            .Where(User => User.UserId == id)
            .Include(t => t.Tasks)
            .First();
        return User;
    }
}