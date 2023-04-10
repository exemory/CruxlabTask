using System.Text.RegularExpressions;

const string passwordsFileName = "passwords.txt";

var lines = await File.ReadAllLinesAsync(passwordsFileName);
var validPasswordsCount = lines.Count(IsValidPassword);

Console.WriteLine($"Valid passwords count: {validPasswordsCount}");

static bool IsValidPassword(string line)
{
    var regex = new Regex(@"(.)\s(\d+)-(\d+):\s(.+)");
    var match = regex.Match(line);

    if (!match.Success)
    {
        return false;
    }

    var requiredChar = match.Groups[1].Value[0];
    var minOccurrences = int.Parse(match.Groups[2].Value);
    var maxOccurrences = int.Parse(match.Groups[3].Value);
    var password = match.Groups[4].Value;

    var charCount = CountCharOccurrences(requiredChar, password);

    return charCount >= minOccurrences && charCount <= maxOccurrences;
}

static int CountCharOccurrences(char character, string str)
{
    return str.Count(c => c == character);
}