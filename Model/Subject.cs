<<<<<<< HEAD
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

        public List<Question> Questions { get; set; }
    }
}
=======
﻿using System;

namespace Model
{
	public class Subject
	{
		// Properties
		public int Id { get; set;}
		public string Name { get; set;}


		// Konstruktør
		public Subject(string name)
		{
			this.Name = name;
		}
	}
}
>>>>>>> 1553959b1b1825efaf2dd7b078e22cf21910506e
