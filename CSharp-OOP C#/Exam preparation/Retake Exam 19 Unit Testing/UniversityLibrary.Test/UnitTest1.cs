namespace UniversityLibrary.Test
{
    using NUnit.Framework;
    public class Tests
    {
        private TextBook textBook;
        private string title = "1984";
        private string authour = "George Orwell";
        private string category = "Disthopy";

        private UniversityLibrary universityLibrary;

        [SetUp]
        public void Setup()
        {
            textBook = new TextBook(title, authour, category);
            universityLibrary = new UniversityLibrary();
        }

        [Test]
        public void TestTextBookConstructor()
        {
            Assert.AreEqual(textBook.Category, category);
            Assert.AreEqual(textBook.Title, title);
            Assert.AreEqual(textBook.Author, authour);
        }

        [Test]
        public void UnitLibraryIsEmptyWhenNew()
        {
            Assert.AreEqual(universityLibrary.Catalogue.Count, 0);
        }

        [Test]
        public void AddingTextBookWorksCorrectly()
        {
            string result = universityLibrary.AddTextBookToLibrary(textBook);
            Assert.AreEqual(textBook.InventoryNumber, 1);
            Assert.AreEqual(universityLibrary.Catalogue.Count, 1);
            Assert.AreEqual(universityLibrary.Catalogue[0].Title, title);

            Assert.AreEqual(result, "Book: 1984 - 1\r\nCategory: Disthopy\r\nAuthor: George Orwell");
        }

        [Test]
        public void LoanTextBookTests()
        {
            universityLibrary.AddTextBookToLibrary(textBook);
            string result = universityLibrary.LoanTextBook(1, "Pesho");

            Assert.AreEqual(textBook.Holder, "Pesho");
            Assert.AreEqual(result, $"{title} loaned to Pesho.");

            result = universityLibrary.LoanTextBook(1, "Pesho");
            Assert.AreEqual(result, $"Pesho still hasn't returned {textBook.Title}!");
        }

        [Test]
        public void ReturnTextBookTests()
        {
            universityLibrary.AddTextBookToLibrary(textBook);
            string result = universityLibrary.LoanTextBook(1, "Pesho");

            result = universityLibrary.ReturnTextBook(1);

            Assert.AreEqual("", textBook.Holder);
            Assert.AreEqual($"{textBook.Title} is returned to the library.", result);
        }
    }
}