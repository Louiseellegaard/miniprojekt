﻿using System;

namespace Model
{
	public class Answer
	{
		// Properties
		public int Id { get; set; }
		public Question Question { get; set; }
		public string Text { get; set; }
		public string Username { get; set; }
		public DateTime Date { get; set; }
		public int Upvote { get; set; }
		public int Downvote { get; set; }


		// Konstruktører
		public Answer() { }

<<<<<<< HEAD
      
    }
=======
		public Answer(Question question, string text, string username, DateTime date) 
		{ 
			this.Question = question;
			this.Text = text;
			this.Username = username;
			this.Date = date;
		}
	}
>>>>>>> 1553959b1b1825efaf2dd7b078e22cf21910506e
}