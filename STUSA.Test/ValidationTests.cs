using SubmitTempUniqueStringApp.Models;

namespace STUSA.Test
{
    public class ValidationTests
    {
        [Test]
        public void EightCharactersMinimumTest()
        {
            var model = new StringViewModel("Zx12");
            var words = model.GetValidWords();
            Assert.That(words.Count, Is.EqualTo(0));
        }

        [Test]
        public void HasUppercaseCharacterTest()
        {
            var model = new StringViewModel("zxcvb1234");
            var words = model.GetValidWords();
            Assert.That(words.Count, Is.EqualTo(0));
        }

        [Test]
        public void HasLowercaseCharacterTest()
        {
            var model = new StringViewModel("ZXCVB1234");
            var words = model.GetValidWords();
            Assert.That(words.Count, Is.EqualTo(0));
        }

        [Test]
        public void HasNumericCharacterTest()
        {
            var model = new StringViewModel("Zxcvbqwerty");
            var words = model.GetValidWords();
            Assert.That(words.Count, Is.EqualTo(0));
        }

        [Test]
        public void HasLetterCharacterTest()
        {
            var model = new StringViewModel("123467890");
            var words = model.GetValidWords();
            Assert.That(words.Count, Is.EqualTo(0));
        }

        [Test]
        public void HasNoRepeatedCharactersTest()
        {
            var model = new StringViewModel("Zzzzzz1234");
            var words = model.GetValidWords();
            Assert.That(words.Count, Is.EqualTo(0));
        }

        [Test]
        public void DoesNotAllowInvalidCharactersTest()
        {
            var model = new StringViewModel("Zxcvb-1234");
            var words = model.GetValidWords();
            Assert.That(words.Count, Is.EqualTo(0));
        }

        [Test]
        public void IsValidWordTest()
        {
            var model = new StringViewModel("Zxcvb1234");
            var words = model.GetValidWords();
            Assert.That(words.Count, Is.EqualTo(1));
        }
    }
}
