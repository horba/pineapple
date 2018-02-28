using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace Pineapple.Model
{
    public class TweetViewModel : TweetModel
    {
        public string AuthorNickname { get; set; }
        public string AuthorPhoto { get; set; }

        public TweetViewModel(int id, DateTime date, string text, int idOfAuthor, string nicknameOfAuthor) 
            : base(id, date, text, idOfAuthor)
        {
            AuthorNickname = nicknameOfAuthor;

            if (System.IO.File.Exists(Directory.GetCurrentDirectory() + "\\wwwroot\\img\\" + idOfAuthor + ".jpg"))
            {
                AuthorPhoto = "\\img\\" + idOfAuthor + ".jpg";
            }
            else
            {
                AuthorPhoto = "\\img\\avatar.png";
            }

        }
        
        public TweetViewModel(TweetModel tweetModel, string nicknameOfAuthor)
            : base(tweetModel.Id, tweetModel.Date, tweetModel.Text, tweetModel.AuthorId)
        {
            AuthorNickname = nicknameOfAuthor;
            if (System.IO.File.Exists(Directory.GetCurrentDirectory() + "\\wwwroot\\img\\" + tweetModel.AuthorId + ".jpg"))
            {
                AuthorPhoto = "\\img\\" + tweetModel.AuthorId + ".jpg";
            }
            else
            {
                AuthorPhoto = "\\img\\avatar.png";
            }
        }
    }
}
