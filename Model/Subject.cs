namespace Model
{
    public class Subject
    {
        public Subject(string subjectName) {
            this.Subjectname = subjectName;
        }

        public long SubjectId { get; set; }
        public string Subjectname { get; set; }

        public override string ToString() {
            return $"{SubjectId}, {Subjectname}";
        }
    }
}