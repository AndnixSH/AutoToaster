using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using JetBrains.Annotations;
using LongPaths.Logic;
using SaveToGameWpf.Logic.Classes;
using SaveToGameWpf.Resources;
using SmaliParser.Logic;

namespace SaveToGameWpf.Logic.Utils
{
    internal static class CommonUtils
    {
        [NotNull]
        private static readonly Encoding SmaliEncoding = new UTF8Encoding(false);
        [NotNull]
        private static readonly string NewLine = Environment.NewLine;

        public static void GenerateAndSaveSmali(
            [NotNull] string filePath,
            [NotNull] string message,
            int messagesCount
        )
        {
            Guard.NotNullArgument(filePath, nameof(filePath));
            Guard.NotNullArgument(message, nameof(message));

            if (messagesCount < 0)
                throw new ArgumentOutOfRangeException(nameof(messagesCount));

            message = message.Replace("\r", "").Replace("\n", "\\n");

            string text = FileResources.SavesRestoringPortable;

            StringBuilder stringBuilder = new StringBuilder();
            string messageShow = FileResources.MessageShow;
            for (int i = 0; i < messagesCount; i++)
            {
                stringBuilder.Append(messageShow.Replace("[(message)]", PadBoth(message, i, ' ')));
            }
            text = text.Replace("[(message)]", stringBuilder.ToString());
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            text = text.Replace("[(text)]", Convert.ToBase64String(bytes));
            stringBuilder.Clear();
            string newLine = NewLine;
            using (StringReader stringReader = new StringReader(text))
            {
                SmaliClass smaliClass = SmaliClass.ParseStream(stringReader);
                new Dictionary<string, SmaliMethod>();
                using (StringWriter stringWriter = new StringWriter())
                {
                    smaliClass.Save(stringWriter);
                    LFile.WriteAllText(filePath, text, SmaliEncoding);
                }
            }
        }

        private static string PadBoth([CanBeNull] string source, int padding,
            char paddingChar = ' '
        )
        {
            if (padding < 0)
                throw new ArgumentOutOfRangeException(nameof(padding));

            if (source == null)
                return new string(paddingChar, padding * 2);

            return source.PadLeft(source.Length + padding, paddingChar).PadRight(source.Length + padding + padding, paddingChar);
        }
    }
}
