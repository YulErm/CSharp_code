using System.Reflection.PortableExecutable;

namespace Assignment1
{
    public class Program
    {
        // 1. load from file as a list of records (2p)
        static List<Record> LoadRecords(string path)
        {
            string[] listOfRecords = File.ReadAllLines(path);  // load all lines from file
            List<Record> records = new List<Record>();

            // for each line we create corresponding objects and write them to a list (records) that will be returned
            foreach (string record in listOfRecords)
            {           
                string[] fullLineToParts = record.Split(',');
                
                // debug
                /*
                Console.WriteLine(record);
                if (fullLineToParts.Length < 6)
                {
                    Console.WriteLine($"Skipped line: {record}");
                    continue;
                }
                */


                // write info from string list to parts (help parameters for next objects)
                string fullAuthorName = fullLineToParts[0];
                string nameOfTheBook = fullLineToParts[1];
                int lenghtOfTheBook = Int32.Parse(fullLineToParts[2]);
                int readerID = Int32.Parse(fullLineToParts[3]);

                // tries to convert string to DateTime. If succes, then writes it to the parameter (dateOfTaking or dateOfReturn)
                DateTime.TryParse(fullLineToParts[4], out DateTime dateOfTaking);
                
                // sets date of return to null and if line has data on the 6th position, then converts string to DateTime
                DateTime? dateOfReturn = null;
                if (fullLineToParts.Length > 5)
                {
                    DateTime.TryParse(fullLineToParts[5], out DateTime tempDateOfReturn);  // can't TryParse DateTime? so uses temp parameter
                    dateOfReturn = tempDateOfReturn;
                }

                //  to find name of the reader and his speed we need to check, if there's this information in loaded records
                string readerName = fullLineToParts.Length > 6 ? fullLineToParts[6] : "";
                float readerReadingSpeed = fullLineToParts.Length > 7 ? float.Parse(fullLineToParts[7]) : 0f;

                // making objects
                Book theBook = new Book(nameOfTheBook, lenghtOfTheBook, fullAuthorName);
                Author theAuthor = theBook.getAuthor;
                Reader theReader = new Reader(readerName, readerID, readerReadingSpeed);                             
                Record theRecord = new Record(theBook, theReader, dateOfTaking);  // add all to record

                // updates the book return, if there's dateOfReturn, then sets it's state to returned (true)
                if (dateOfReturn != null)
                {
                    theRecord.setReturn = true;
                }

                // add to list of records                
                records.Add(theRecord);

            }

            return records;
        }

        // 2. find title of most commonly borrowed book from records (0.5p)
        static string FindMostReadBook(string path)
        {
            List<Record> records = LoadRecords(path);

            // creates dictionary with key : value be like "book name" : number of takings
            Dictionary<string, int> dict = new Dictionary<string, int>();  

            // fills the dictionary;
            // for each record in list finds book name, adds +1 to value in dictionary
            foreach (Record record in records)
            {
                string bookName = record.theBook.GetBookName();

                // if there weren't any key with the name of the book, then adds this book to the dictionary
                if (dict.ContainsKey(bookName) == false)
                {
                    dict.Add(bookName, 1);
                }
                else
                {
                    dict[bookName] = dict[bookName] + 1;  // if there was this book in dictionary, then adds 1 to value
                }
            }

            // finds max value of book takings 
            int maxValue = dict.Values.Max();
            List<string> books = new List<string>();

            // for each book in dictionary, checks its max value of takings (Value) and adds it to list of books
            foreach (string book in dict.Keys)
            {
                if (dict[book] == maxValue)
                {
                    books.Add(book);
                }
            }

            // converts list of books to string and return it with space as separation 
            // ex.: we have list {"one", "two", three"}, return will be: "one, two, three"
            return string.Join(", ", books);  
        }

        // 3. find most read author (1p)
        static string FindMostReadAuthor(string path)
        {

            List<Record> records = LoadRecords(path);

            // creates dictionary to track number of borrowings from authors
            Dictionary<string, int> dict = new Dictionary<string, int>();

            // finds author name, adds +1 to value
            foreach (Record record in records)
            {
                string authorName = record.theBook.getAuthor.GetAuthorName();  // gets author name

                // adds this author to the dictionary
                if (dict.ContainsKey(authorName) == false)
                {
                    dict.Add(authorName, 1);
                }
                else
                {
                    dict[authorName] = dict[authorName] + 1;
                    // Console.Write(authorName +":"+ dict[authorName] + ",");  // debug
                }
            }

            int maxValue = dict.Values.Max();
            List<string> authors = new List<string>();

            // checks max value of takings and adds to list
            foreach (string author in dict.Keys)
            {
                if (dict[author] == maxValue)
                {
                    authors.Add(author);
                }
            }

            // converts list to string
            return string.Join(", ", authors);
        }

        // 3. find most avid reader (0.5p)
        static int FindMostAvidReader(string path)
        {
            List<Record> records = LoadRecords(path);

            // creates dictionary to track number of readers
            Dictionary<int, int> dict = new Dictionary<int, int>();
            
            // finds reader ID, adds +1 to value
            foreach (Record record in records)
            {
                int readerID = record.theReader.GetReaderID();  // gets id

                // adds this id to the dictionary
                if (dict.ContainsKey(readerID) == false)
                {
                    dict.Add(readerID, 1);
                }
                else
                {
                    dict[readerID] = dict[readerID] + 1;
                }
            }
            int maxValue = dict.Values.Max();  // finds which ids have the most number

            int theReader = dict.Values.First();   // the most avid reader

            // checks max value and find the most avid reader
            foreach (int readerID in dict.Keys)
            {
                if (dict[readerID] == maxValue)
                {
                    theReader = readerID;
                }
            }
            return theReader;
        }

        // 4. calculate income to a given day (1p)
        static float CalculateIncome(string path, DateTime date)
        {
            return 0.0f;
        }

        static void Main(string[] args)
        {
            List<Record> list = LoadRecords("library_records.csv");

            string readBooks = FindMostReadBook("library_records.csv");
            Console.WriteLine(readBooks);

            string mostAuthors = FindMostReadAuthor("library_records.csv");
            Console.WriteLine(mostAuthors);

            int theAvidReader = FindMostAvidReader("library_records.csv");
            Console.WriteLine(theAvidReader);
            /*  
            string f = "Name", l = "Last";
            Author aut = new Author(f, l);
            Console.WriteLine(aut.GetAuthorName());
            aut.SetAuthorName("ay", "Noan");
            Console.WriteLine(aut.GetAuthorName());         
            
            string t = "Name";
            int l = 60;
            string aName = "Jake Sall";
            Book book = new Book(t, l, aName);
            Console.WriteLine(book.GetBookName());
            */


        }
    }
}