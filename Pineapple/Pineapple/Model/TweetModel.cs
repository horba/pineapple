﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pineapple.Model
{
    public class TweetModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public int IdOfAuthor { get; set; }

        public TweetModel(int id, DateTime date, string text, int idOfAuthor)
        {
            Id = id;
            Date = date;
            Text = text;
            IdOfAuthor = idOfAuthor;
        }

        public override string ToString()
        {
            if (IdOfAuthor != 0)
            {
                return String.Format("Tweet #{0} from {1} \"{2}\" by user #{3}", Id, Date.ToShortDateString(), Text, IdOfAuthor);
            }
            else
            {
                return String.Format("Tweet #{0} from {1} \"{2}\"", Id, Date.ToShortDateString(), Text);
            }
        }
    }
}
