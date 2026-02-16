using System.Xml.Linq;

namespace Assignment1
{
    public class Record
    {
        private Book book;
        private Reader reader;
        private DateTime borrowed;
        private bool returned;

        public Record(Book book, Reader reader, DateTime borrowed)
        {
            this.book = book;
            this.reader = reader;
            this.borrowed = borrowed;
            this.returned = false;
        }

        // use this to get book, which next will be used in GetTotalReadingTime in Reader
        public Book theBook
        {
            get { return this.book; }
        }

        // use this to get reader, which next will be used to check if AddReading (in Reader) refers to this reader
        public Reader theReader
        {
            get { return this.reader; }
        }

        // use this to get returned parameter and set it to true, which next will be used in  ReturnBooks in Reader
        public bool setReturn   // property
        {
            get { return this.returned; }   // get method
            set { this.returned = true; }  // set method
        }
        public float GetFee(DateTime date)
        {
            float feeEveryMonth = 5f;
            float feeEveryDay = 0.1f; 
            
            TimeSpan numberOfDays = date - this.borrowed;

            // 'expiredMonths' represents every month of penalty,
            // 'expiredDays' represents days, when the next month is not started ( in the rest of the month)
            int expiredMonths = numberOfDays.Days / 30;
            int expiredDays = numberOfDays.Days % 30;

            // checks if there was expired "non-penalty" month
            if (numberOfDays.Days > 30)
            {
                return expiredDays * feeEveryDay + expiredMonths * feeEveryMonth;
            } 
            return 0.0f;
        }
    }
}
