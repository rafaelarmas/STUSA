using System.ComponentModel.DataAnnotations;

namespace SubmitTempUniqueStringApp.Models
{
    /// <summary>
    /// Valid words must contain at least one uppercase letter, one lowercase letter, and one number.
    /// No character should be repeated.
    /// Must be at least 8 characters long.
    /// </summary>
    public class StringViewModel
    {
        private string _content;
        
        public StringViewModel(string input)
        {
            _content = input;
        }

        [Required]
        public string? Content { get { return _content; } }

        public bool IsEmpty {  get { return string.IsNullOrWhiteSpace(_content); } }

        /// <summary>
        /// Assumption: Content is within the range [1..500]
        /// </summary>
        public bool IsTooLong {  get { return _content?.Length > 500; } }

        /// <summary>
        /// Content consists only of alphanumeric characters and spaces
        /// </summary>
        public bool HasInvalidCharacters 
        {
            get
            {
                if (!IsEmpty && !IsTooLong)
                {
                    for (var i = 0; _content.Length < i; i++)
                    {
                        if (char.IsLetterOrDigit(_content[i]) || _content[i] == ' ')
                            return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// The goal is to choose the longest valid word from the string and return it.
        /// If multiple longest valid words are present, return any one of them.
        /// </summary>
        public string GetChosenValidWord(List<string> validWords)
        {
            if (validWords.Count > 0)
            {
                var firstLongestWord = validWords.OrderByDescending(x => x.Length).First();
                var longestWordList = validWords.Where(x => x.Length == firstLongestWord.Length).ToList();
                var randomNumber = Random.Shared.Next(0, longestWordList.Count);
                return longestWordList[randomNumber];
            }
            return string.Empty;
        }

        /// <summary>
        /// Gets the words from the input sentence that follow the specified rules.
        /// </summary>
        /// <returns>List of valid words. Empty list if words did follow rules.</returns>
        public List<string> GetValidWords()
        {
            var validWords = new List<string>();
            if (!IsEmpty && !IsTooLong && !HasInvalidCharacters)
            {
                foreach (string word in _content.Split(' '))
                {
                    // Must contain at least one uppercase letter, one lowercase letter, and one number. Must be at least 8 characters long.
                    if (word.Any(char.IsUpper) && word.Any(char.IsLower) && word.Any(char.IsDigit) && word.Length >= 8)
                    {
                        // No character should be repeated.
                        if (!(word.Where((c, i) => i > 0 && c == word[i - 1])
                            .Cast<char?>()
                            .FirstOrDefault() != null))
                        {
                            validWords.Add(word);
                        }
                    }
                }
            }
            return validWords;
        }
    }
}
