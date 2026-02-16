namespace Assignment1
{
    public class Book
    {
        private string title;
        private int length;
        private Author author;

        public Book(string title, int length, string authorName)
        {
            string[] fullNametoParts = authorName.Split(' ');  // gets the full name and splits it to 2 separate words. after that sets this parts to this.author
            
            this.title = title;
            this.length = length;
            this.author = new Author(fullNametoParts[0], fullNametoParts[1]);
        }

        /* test of the constructor
          public string GetBookName()
          {
              return this.title + " " + this.length + " " + this.author.GetAuthorName();
          }
         */
        
        // used to get author in LoadRecord
        public Author getAuthor
        {
            get { return this.author; }
        }

        // gets book name (for FindMostReadBook in main program)
        public string GetBookName()
        {
            return this.title;
        }
        public int GetReadingTime(float minutesPerPage, bool inHours)
        {
            double timeOnFullBook = minutesPerPage * this.length;

            // if it's needed to get time in hours, than divides all time on 60 mins and converts to int type (rounded up the time before that)
            if (inHours)
            {
                return (int)Math.Ceiling(timeOnFullBook / 60d);
            }
            return (int)Math.Ceiling(timeOnFullBook);
        }
    }
}
