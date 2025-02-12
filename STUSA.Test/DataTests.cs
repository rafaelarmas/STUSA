using Microsoft.Extensions.Configuration;
using STUSA.Data;

namespace STUSA.Test
{
    public class DataTests
    {
        private TopScoreDataContext _context;

        [SetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(
                [
                    new("ConnectionStrings:STUSADatabase", "Data Source=test.db")
                ])
                .Build();
            _context = new TopScoreDataContext(config);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public void TestAddTopScoreWord()
        {
            if (_context.TopScoreWords.Any(x => x.Id == 1))
            {
                _context.TopScoreWords.Remove(_context.TopScoreWords.Single(x => x.Id == 1));
                _context.SaveChanges();
            }

            var word = new TopScoreWord { Id = 1, Content = "Zxcvb1234" };

            _context.TopScoreWords.Add(word);
            _context.SaveChanges();

            var output = _context.TopScoreWords.Single(x => x.Id == 1);

            Assert.That(output.Content, Is.EqualTo(word.Content));
        }
    }
}
