using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pineapple.Model
{
    public class Tweet
    {
        private string id { get; set; }
        private string date { get; set; }
        private string text { get; set; }


        public Tweet(string ID, string Date, string Text) {
            this.id = ID;
            this.date = Date;
            this.text = Text;
        }

        public override string ToString()
        {
            return String.Format("{0} {1} {2}", this.id, this.date, this.text);
        }
    }
}
