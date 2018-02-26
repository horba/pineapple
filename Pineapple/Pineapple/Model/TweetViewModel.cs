using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pineapple.Model
{
    public class TweetViewModel : TweetModel
    {
        public string NicknameOfAuthor { get; set; }

        public TweetViewModel(int id, DateTime date, string text, int idOfAuthor, string nicknameOfAuthor) 
            : base(id, date, text, idOfAuthor)
        {
            NicknameOfAuthor = nicknameOfAuthor;
        }

        public TweetViewModel(TweetModel tweetModel, string nicknameOfAuthor)
            : base(tweetModel.Id, tweetModel.Date, tweetModel.Text, tweetModel.IdOfAuthor)
        {
            NicknameOfAuthor = nicknameOfAuthor;
        }
    }
}
