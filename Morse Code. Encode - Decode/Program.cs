using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Morse_Code_Encode_Decode
{
    internal class Program
    {
        private static readonly Dictionary<char, string> morseDictionary = new Dictionary<char, string>()
        {
            {'A', ".-"}, {'B', "-..."}, {'C', "-.-."}, {'D', "-.."}, {'E', "."},
            {'F', "..-."}, {'G', "--."}, {'H', "...."}, {'I', ".."}, {'J', ".---"},
            {'K', "-.-"}, {'L', ".-.."}, {'M', "--"}, {'N', "-."}, {'O', "---"},
            {'P', ".--."}, {'Q', "--.-"}, {'R', ".-."}, {'S', "..."}, {'T', "-"},
            {'U', "..-"}, {'V', "...-"}, {'W', ".--"}, {'X', "-..-"}, {'Y', "-.--"},
            {'Z', "--.."},
            {'1', ".----"}, {'2', "..---"}, {'3', "...--"}, {'4', "....-"}, {'5', "....."},
            {'6', "-...."}, {'7', "--..."}, {'8', "---.."}, {'9', "----."}, {'0', "-----"},
            {',', "--..--"}, {'.', ".-.-.-"}, {'?', "..--.."}, {'!', "-.-.--"}, {'\'', ".----."},
            {'"', ".-..-."}, {'(', "-.--."}, {')', "-.--.-"}, {'&', ".-..."}, {':', "---..."},
            {';', "-.-.-."}, {'=', "-...-"}, {'+', ".-.-."}, {'-', "-....-"}, {'_', "..--.-"},
            {'/', "-..-."}, {'\\', "-..-."}, {'@', ".--.-."}
        };

        public static string Encode(string message)
        {
            StringBuilder morseCode = new StringBuilder();
            foreach (char c in message.ToUpper())
            {
                if (c == ' ') morseCode.Append(" ");
                else morseCode.Append(morseDictionary[c]);
                morseCode.Append(" ");
            }
            return morseCode.ToString();
        }

        public static string Decode(string morseCode)
        {
            StringBuilder message = new StringBuilder();
            string[] separator = { "   " };
            string[] words = morseCode.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in words)
            {
                string[] letters = word.Split(' ');
                foreach (string letter in letters)
                {
                    char decodedChar = morseDictionary.FirstOrDefault(x => x.Value == letter).Key;
                    if (decodedChar != '\0')
                    {
                        message.Append(decodedChar);
                    }
                }
                message.Append(" ");
            }
            return message.ToString().Trim().ToUpper();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("If you want to translate a message consisting of Morse code," +
                "\nthen put 1 space between letters and 3 or more spaces between words.");
            string word = Console.ReadLine().Trim();
            switch (word[0])
            {
                case '-': case '.': Console.WriteLine(Decode(word)); break;
                default: Console.WriteLine(Encode(word)); break;
            }
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
