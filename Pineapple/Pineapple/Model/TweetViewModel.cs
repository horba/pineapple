using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pineapple.Model
{
    public class TweetViewModel : TweetModel
    {
        public string AuthorNickname { get; set; }

        public TweetViewModel(int id, DateTime date, string text, int idOfAuthor, string nicknameOfAuthor) 
            : base(id, date, text, idOfAuthor)
        {
            AuthorNickname = nicknameOfAuthor;
        }

        public TweetViewModel(TweetModel tweetModel, string nicknameOfAuthor)
            : base(tweetModel.Id, tweetModel.Date, tweetModel.Text, tweetModel.AuthorId)
        {
            AuthorNickname = nicknameOfAuthor;
        }
    }
}
