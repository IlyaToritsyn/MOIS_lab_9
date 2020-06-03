using System.Collections.Generic;

namespace ClassLibrary
{
    /// <summary>
    /// Класс для работы с азбукой Морзе.
    /// </summary>
    public static class WorkWithMorseCode
    {
        /// <summary>
        /// Азбука Морзе для латиницы.
        /// </summary>
        public static Dictionary<char, string> MorseCode { get; } = new Dictionary<char, string>()
        {
            {'A', ".-"},
            {'B', "-..."},
            {'C', "-.-."},
            {'D', "-.."},
            {'E', "."},
            {'F', "..-."},
            {'G', "--."},
            {'H', "...."},
            {'I', ".."},
            {'J', ".---"},
            {'K', "-.-"},
            {'L', ".-.."},
            {'M', "--"},
            {'N', "-."},
            {'O', "---"},
            {'P', ".--."},
            {'Q', "--.-"},
            {'R', ".-."},
            {'S', "..."},
            {'T', "-"},
            {'U', "..-"},
            {'V', "...-"},
            {'W', ".--"},
            {'X', "-..-"},
            {'Y', "-.--"},
            {'Z', "--.."},
        };

        /// <summary>
        /// Кодирование строки при помощи азбуки Морзе.
        /// </summary>
        /// <param name="message">Кодируемое сообщение</param>
        /// <returns>Закодированная строка</returns>
        public static string Encode(string message)
        {
            string encodedMessage = "";

            foreach (char symbol in message)
            {
                encodedMessage += MorseCode[symbol];
            }

            return encodedMessage;
        }

        /// <summary>
        /// Кодирование строк в списке при помощи азбуки Морзе.
        /// </summary>
        /// <param name="messages">Список кодируемых строк</param>
        /// <param name="encodedWords">Список с закодированными строками</param>
        public static void Encode(List<string> messages, out List<string> encodedWords)
        {
            encodedWords = new List<string>();

            foreach (string message in messages)
            {
                encodedWords.Add(Encode(message));
            }
        }
    }
}
