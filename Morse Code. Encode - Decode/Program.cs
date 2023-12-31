﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Morse_Code_Encode_Decode
{
    internal class Program
    {
        public class MorseTree
        {
            private Node Root { get; set; }
            public MorseTree()
            {
                Root = new Node();
                foreach (var keyValuePair in morseDictionary) Add(Root, keyValuePair);
            }

            public class Node
            {
                public char Character { get; set; }
                public Node Dot { get; set; }
                public Node Dash { get; set; }

                public Node()
                {
                    Character = '\0';
                    Dot = null;
                    Dash = null;
                }
            }

            private void Add(Node node, KeyValuePair<char, string> pair)
            {
                for (int i = 0; i < pair.Value.Length; i++)
                {
                    if (pair.Value[i] == '.')
                    {
                        if (node.Dot == null) node.Dot = new Node();
                        node = node.Dot;
                    }
                    else
                    {
                        if (node.Dash == null) node.Dash = new Node();
                        node = node.Dash;
                    }
                }
                node.Character = pair.Key;
            }

            public char Find(string code)
            {
                Node node = Root;
                for (int i = 0; i < code.Length; i++)
                {
                    if (code[i] == '.') node = node.Dot;
                    else node = node.Dash;
                    if (node == null) return '\0';
                }
                return node.Character;
            }
        }

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
            {'/', "-..-."}, {'@', ".--.-."}
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

        public static string Decode(MorseTree morseTree, string morseCode)
        {
            StringBuilder message = new StringBuilder();
            string[] separator = { "   " };
            string[] words = morseCode.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in words)
            {
                string[] letters = word.Split(' ');
                foreach (string letter in letters)
                {
                    char decodedChar = morseTree.Find(letter);
                    if (decodedChar != '\0') message.Append(decodedChar);
                    else return "Invalid Morse code sequence.";
                }
                message.Append(" ");
            }
            return message.ToString().Trim().ToUpper();
        }

        static void Main(string[] args)
        {
            MorseTree morseTree = new MorseTree();
            Console.WriteLine("If you want to translate a message consisting of Morse code," +
                "\nthen put 1 space between letters and 3 or more spaces between words.");
            while (true)
            {
                string message = Console.ReadLine().Trim();
                if (message.Length == 0) continue;
                char firstCharacter = char.ToUpper(message[0]);
                switch (firstCharacter)
                {
                    case '-': case '.':
                        Console.WriteLine(Decode(morseTree, message)); break;
                    default:
                        if (message.All(x => morseDictionary.ContainsKey(char.ToUpper(x)) || x == ' ')) Console.WriteLine(Encode(message));
                        else Console.WriteLine("Invalid input format.");
                        break;
                }
            }
        }
    }
}
