using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Json;

using Data;
using Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS skal sl�es til i app'en. Ellers kan man ikke hente data fra et andet dom�ne.
// Se mere her: https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-6.0
var AllowSomeStuff = "_AllowSomeStuff";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowSomeStuff, builder => {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// Tilf�j DbContext factory som service, s� man kan f� context ind via Dependency Injection.
builder.Services.AddDbContext<QuestionContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("QuestionContextSQLite")));

// Kan vise flotte fejlbeskeder i browseren, hvis der kommer fejl fra databasen
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Tilf�j 'DataService' s� den kan bruges i endpoints
builder.Services.AddScoped<DataService>();

// Her kan man styre, hvordan den laver JSON.
builder.Services.Configure<JsonOptions>(options =>
{
    // Super vigtig option! Den g�r, at programmet ikke smider fejl,
    // n�r man returnerer JSON med objekter, der refererer til hinanden.
    // (alts� dobbelrettede associeringer)
    options.SerializerOptions.ReferenceHandler = 
        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});


// ---------------------------------------------------------------

var app = builder.Build();

// Seeding af data, hvis databasen er tom
using (var scope = app.Services.CreateScope())
{
    // Med 'scope' kan man hente en service.
    var dataService = scope.ServiceProvider.GetRequiredService<DataService>();
    dataService.SeedData(); // 'SeedData()' er defineret i 'DataService.cs', og fylder data p� databasen, hvis den er tom.
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();
app.UseCors(AllowSomeStuff);

// Middlware der k�rer f�r hver request. Alle svar skal have ContentType: JSON.
app.Use(async (context, next) =>
{
    context.Response.ContentType = "application/json; charset=utf-8";
    await next(context);
});

// Endpoints i API'en --------------------------------------------

app.MapGet("/", (HttpContext context, DataService service) =>
{
    context.Response.ContentType = "text/html;charset=utf-8";
    return "Hejsa. Her er der intet at se. Pr�v i stedet: " + 
            "<a href=\"/api/questions\">/api/questions</a>";
});


// ---------------------------------------------------------------
// -- Questions --

app.MapGet("/api/questions/", (DataService service) =>
{
    return service.ListQuestions();
});

app.MapGet("/api/questions/{id}", (DataService service, int id) =>
{
    return service.GetQuestionById(id);
});

app.MapGet("/api/questions/{id}/subject", (DataService service, int id) =>
{
    return service.ListQuestionsBySubjectId(id);
});

app.MapPost("/api/questions/", (DataService service, QuestionData data) =>
{
    return service.createQuestion(data.subjectId, data.title, data.text, data.username);
});

app.MapPut("/api/questions/{id}/upvote", (DataService service, int id) =>
{
    return service.updateQuestionByIdUpvote(id);
});

app.MapPut("/api/questions/{id}/downvote", (DataService service, int id) =>
{
    return service.updateQuestionByIdDownvote(id);
});

// ---------------------------------------------------------------
// -- Subjects --

app.MapGet("/api/subjects/", (DataService service) =>
{
    return service.ListSubjects();
});

app.MapGet("/api/subjects/{id}", (DataService service, int id) =>
{
    return service.GetSubjectById(id);
});

// ---------------------------------------------------------------
// -- Answers --

app.MapGet("/api/answers/{id}/question", (DataService service, int id) =>
{
    return service.ListAnswersByQuestionId(id);
});

app.MapPost("/api/answers/", (DataService service, AnswerData data) =>
{
    return service.CreateAnswer(data.questionId, data.text, data.username);
});

app.MapPut("/api/answers/{id}/upvote", (DataService service, int id) =>
{
    return service.updateAnswerByIdUpvote(id);
});

app.MapPut("/api/answers/{id}/downvote", (DataService service, int id) =>
{
    return service.updateAnswerByIdDownvote(id);
});

// ---------------------------------------------------------------

app.Run();

// -- Records ----------------------------------------------------

record QuestionData(int subjectId, string title, string text, string username);
record SubjectData(string name);
record AnswerData(int questionId, string text, string username);