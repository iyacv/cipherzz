using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cipherzz
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Would you like to encrypt or decrypt a message? (E, D): ");
            string choice = Console.ReadLine();

            if (choice.ToUpper() == "E")
            {
                ProcessMessage("encrypt");
            }
            else if (choice.ToUpper() == "D")
            {
                DecryptMessage();
            }
            else
            {
                Console.WriteLine("Invalid choice. Please enter 'E' or 'D'.");
            }
        }

        static void ProcessMessage(string operation)
        {
            Console.Write("Enter the key: ");
            string key = Console.ReadLine();

            string modifiedAlphabet = GenerateModifiedAlphabet(key);

            Console.Write("Enter the message to " + operation + ": ");
            string message = Console.ReadLine();

            string processedMessage = PerformOperation(message, modifiedAlphabet, operation);

            if (operation == "encrypt")
            {
                SaveToFile("eMessage.txt", processedMessage);
                Console.WriteLine("Message encrypted and stored in 'eMessage.txt'.");
            }
            else if (operation == "decrypt")
            {
                Console.WriteLine("Decrypted message: " + processedMessage);
            }
        }

        static string PerformOperation(string message, string modifiedAlphabet, string operation)
        {
            string processedMessage = "";

            foreach (char c in message)
            {
                char processedChar = operation == "encrypt" ? EncryptChar(c, modifiedAlphabet) : DecryptChar(c, modifiedAlphabet);
                processedMessage += processedChar;
            }

            return processedMessage;
        }

        static string GenerateModifiedAlphabet(string key)
        {
            string uniqueLetters = "";

            foreach (char c in key)
            {
                if (char.IsLetter(c) && uniqueLetters.IndexOf(c) == -1)
                {
                    uniqueLetters += c;
                }
            }

            string modifiedAlphabet = uniqueLetters;

            string Alphabet = "abcdefghijklmnopqrstuvwxyz";
            foreach (char c in Alphabet)
            {
                if (uniqueLetters.IndexOf(c) == -1)
                    modifiedAlphabet += c;
            }

            return modifiedAlphabet;
        }

        static char EncryptChar(char c, string modifiedAlphabet)
        {
            if (char.IsLetter(c))
            {
                char lowerC = char.ToLower(c);
                int index = lowerC - 'a';

                char encryptedChar = char.ToUpper(modifiedAlphabet[index]);
                return encryptedChar;
            }
            else
            {
                return c;
            }
        }

        static void DecryptMessage()
        {
            Console.Write("Enter the key: ");
            string key = Console.ReadLine();

            string modifiedAlphabet = GenerateModifiedAlphabet(key);

            string encryptedFromFile = File.ReadAllText("eMessage.txt");

            string decryptedMessage = Decrypt(encryptedFromFile, modifiedAlphabet);

            Console.WriteLine("Decrypted message: " + decryptedMessage);
        }

        static string Decrypt(string encryptedMessage, string modifiedAlphabet)
        {
            string decryptedMessage = "";

            foreach (char c in encryptedMessage)
            {
                decryptedMessage += DecryptChar(c, modifiedAlphabet);
            }

            return decryptedMessage;
        }

        static char DecryptChar(char c, string modifiedAlphabet)
        {
            if (char.IsLetter(c))
            {
                char lowerC = char.ToLower(c);
                int index = modifiedAlphabet.IndexOf(lowerC);

                char decryptedChar = char.ToUpper((char)('a' + index));
                return decryptedChar;
            }
            else
            {
                return c;
            }
        }

        static void SaveToFile(string fileName, string content)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.Write(content);
            }

            Console.WriteLine("Message saved to file: " + fileName);
        }
    }
}