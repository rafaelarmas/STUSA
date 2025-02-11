## SUTSA - Submit Temporary Unique String Application

An example of a ASP.NET Core MVC application that allows users to submit a string which is then stored in a SQLite database. The string (up to 500 characters) is checked for valid words which adhere to a set of rules for which the longest word is then submitted. There is a second page which allows the user to search for already added words.

The validation rules for the words are:

- Must contain at least one uppercase letter
- Must contain at least one lowercase letter
- Must contain at least one number
- No character should be repeated
- Must be at least 8 characters long
