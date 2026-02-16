namespace Assignment1
{
    public class Author
    {
        private string firstName;
        private string lastName;

        public Author(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }

        public void SetAuthorName(string firstName, string lastName)
        {
            //checks if first letters are in uppercase
            if (char.IsUpper(firstName[0]) && char.IsUpper(lastName[0]))
            {
                //checks all letters in 'firstName' and 'lastName' with lambda expression
                if (firstName.All(letter => (letter >= 'a' && letter <= 'z') || (letter >= 'A' && letter <= 'Z'))
                    && lastName.All(letter => (letter >= 'a' && letter <= 'z') || (letter >= 'A' && letter <= 'Z')))
                {
                    this.firstName = firstName;
                    this.lastName = lastName;
                }
            }
        }

        public string GetAuthorName()
        {
            return this.firstName + " " + this.lastName; 
        }
    }
}

