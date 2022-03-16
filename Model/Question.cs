namespace Model
{
    public class Question
    {
        public Question(string name, string text, DateTime date, int upvote, int downvote) {
            this.Name = name;
            this.Text = text;
            this.Date = date;
            this.Upvote = upvote;
            this.Downvote = downvote;

        }
        public long QuestionId { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int Upvote { get; set; }
        public int Downvote { get; set; }

        public List<Question> Questions { get; set; }
    }
}