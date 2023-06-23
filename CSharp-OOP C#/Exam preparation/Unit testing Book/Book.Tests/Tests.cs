namespace Book.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using NUnit.Framework;

    [TestFixture]
    public class Tests
    {
        private Book defBook;
        [SetUp]
        public void SetUp()
        {
            this.defBook = new Book("Harry Potter", "J. K. Rowling");
        }

        [Test]
        public void ConstructorInitializationForTheNameOfTheBook()
        {
            string bookName = "Harry Potter";
            Book book = new Book(bookName, "J. K. Rowling");

            string actualBookName = book.BookName;
            Assert.AreEqual(bookName, actualBookName);
        }

        [Test]
        public void ConstructorInitializationForTheAuthorName()
        {
            string authorName = "J. K. Rowling";
            Book book = new Book("Harry Potter", authorName);

            string actualAuthorName = book.Author;
            Assert.AreEqual(authorName, actualAuthorName);
        }

        [Test]
        public void ConstructorInitializationForFootNoteDictionary()
        {           
            Type type = this.defBook.GetType();
            FieldInfo dictFieldInfo = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(fi => fi.Name == "footnote");

            object fieldValue = dictFieldInfo.GetValue(this.defBook);
            Assert.IsNotNull(fieldValue);
        }

        [Test]
        public void CountZeroWhenNoFootNotesAdded()
        {
            int count = 0;
            int actualCount = this.defBook.FootnoteCount;

            Assert.AreEqual(count, actualCount);
        }

        [Test]
        public void CountReturningCorrectCountWhenFootNotesAdded()
        {
            int count = 2;
            for(int i = 0; i < count; i++)
            {
                this.defBook.AddFootnote(i, i.ToString());
            }

            int actualCount = this.defBook.FootnoteCount;
            Assert.AreEqual(count, actualCount);
        }

        [TestCase("Real name")]
        [TestCase("1")]
        [TestCase("  ")]

        public void BookNameShouldSetCorrectValues(string bookName)
        {
            Book book = new Book(bookName, "Author");

            string expected = bookName;
            string actualName = book.BookName;

            Assert.AreEqual(expected, actualName);
        }

        [TestCase(null)]
        [TestCase("")]
        public void BookNameGivesErrorWHenBookNameNullOrEmoty(string bookName)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Book book = new Book(bookName, "Author");
            }, "Invalid BookName!");
        }

        [TestCase("Real name")]
        [TestCase("1")]
        [TestCase("  ")]

        public void AuthorNameShouldSetCorrectValues(string authorName)
        {
            Book book = new Book("Book name", authorName);

            string expected = authorName;
            string actualName = book.Author;

            Assert.AreEqual(expected, actualName);
        }

        [TestCase(null)]
        [TestCase("")]
        public void AuthorNameGivesErrorWHenBookNameNullOrEmoty(string authorName)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Book book = new Book("Book name", authorName);
            }, "Invalid Author!");
        }

        [Test]
        public void AddingFootNoteShouldIncreaseCount()
        {
            int count = 1;
            for (int i = 0; i < count; i++)
            {
                this.defBook.AddFootnote(i, i.ToString());
            }

            int actualCount = this.defBook.FootnoteCount;
            Assert.AreEqual(count, actualCount);
        }

        [Test]
        public void AddingFootNotesShouldAddKeyToDictionary()
        {
            int addedKey = 1;
            this.defBook.AddFootnote(addedKey, "Random text");

            Type bookType = this.defBook.GetType();
            FieldInfo dictFieldInfo = bookType
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(fi => fi.Name == "footnote");

            Dictionary<int, string> fieldValue = (Dictionary<int, string>)
                dictFieldInfo.GetValue(this.defBook);
            bool containsKey = fieldValue.ContainsKey(addedKey);

            Assert.IsTrue(containsKey);
        }

        [Test]
        public void AddingExistingFootShouldShowException()
        {
            int sameKey = 1;
            this.defBook.AddFootnote(sameKey, "Random note");

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.defBook.AddFootnote(sameKey, "Another note");
            }, "Footnote already exists!");
        }

        [Test]
        public void SearchingFootNoteReturningCorrectTextIfItExists()
        {
            int footKey = 1;
            string footText = "Text";
            this.defBook.AddFootnote(footKey, footText);

            string expecting = $"Footnote #{footKey}: {footText}";
            string actualResults = this.defBook.FindFootnote(footKey);

            Assert.AreEqual(expecting, actualResults);
        }

        [Test]
        public void ThrowExceptionIfThereAreNotesButPassedKeyDoesntExist()
        {
            int footKey = 1;
            string footText = "Text";
            this.defBook.AddFootnote(footKey, footText);

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.defBook.FindFootnote(999);
            }, "Footnote doesn't exists!");
        }

        [Test]
        public void FIndFootNoteShouldThrowExceptionIfThereAreNoFootNotesAtAll()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                this.defBook.FindFootnote(1);
            }, "Footnote doesn't exists!");
        }

        [Test]
        public void AlterFoodNoteShouldChangeTextWhenFootNoteExists()
        {
            int footKey = 1;
            string footText = "Text";
            this.defBook.AddFootnote(footKey, footText);

            string expectedText = "New text";
            this.defBook.AlterFootnote(footKey, expectedText);

            Type bookType = this.defBook.GetType();
            FieldInfo dictFieldInfo = bookType
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(fi => fi.Name == "footnote");

            Dictionary<int, string> fieldValue = (Dictionary<int, string>)
                dictFieldInfo.GetValue(this.defBook);

            string actualText = fieldValue[footKey];
            Assert.AreEqual(expectedText, actualText);
        }

        [Test]
        public void AlterFoodShouldThrowExceptionIfTjereAreFootNotesWithNonExistingKeys()
        {
            int footKey = 1;
            string footText = "Text";
            this.defBook.AddFootnote(footKey, footText);

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.defBook.AlterFootnote(999, "New invalid text");
            }, "Footnote doesn't exists!");
        }

        [Test]
        public void AlterFootNoteThrowExceptionIfThereAreNoFootNotesAtAll()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                this.defBook.AlterFootnote(1, "Invalid text");
            }, "Footnote doesn't exists!");
        }
    }
}