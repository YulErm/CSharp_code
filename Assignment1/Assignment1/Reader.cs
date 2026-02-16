namespace Assignment1
{
    public class Reader
    {
        private string name;
        private int readerId;
        private float readingSpeed;
        private List<Record> reads;

        public Reader(string name, int readerId, float readingSpeed)
        {
            this.name = name;
            this.readerId = readerId;
            this.readingSpeed = readingSpeed;
            this.reads = new List<Record>();
        }

        public int GetReaderID()
        {
            return this.readerId;
        }

        public int GetTotalReadingTime()
        {
            double totalReadingTime = 0;


            // for each record ('book') gets to the book property, than gets the reading time for this book and adds 'totalReadingTime' on every loop. 
            // rounds up to get whole number (7.1 will be 8, 56.7 will be 57, ets.)
            foreach (Record book in reads)
            {
                totalReadingTime = Math.Ceiling(totalReadingTime + book.theBook.GetReadingTime(this.readingSpeed, false));
            }

            return (int)totalReadingTime;  // converts value to integer and return
        }

        public void AddReading(Record record)
        {
            if (record.theReader.readerId == this.readerId)
            {
                this.reads.Add(record);
            } 
            else
            {
                Console.WriteLine("This is not the right reader.");
            }
        }

        public float ReturnBooks(DateTime date)
        {            
            float fullFee = 0.0f;

            // for each book in list checks 'returned' parameter and set it to true, after that gets fee and adds it to fullFee value
            foreach (Record book in this.reads)
            {
                if (book.setReturn == false)
                {
                    book.setReturn = true;
                    fullFee = fullFee + book.GetFee(date);
                }
            }
            return fullFee;
        }
    }
}
