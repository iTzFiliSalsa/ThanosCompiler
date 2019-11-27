using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace frmMain.semantico.ejecucion
{
    public class ThanosLexer
    {
        private static readonly HashSet<char> BlankCharacters = new HashSet<char>()
        {
            '\r',
            '\n',
            (char) 8,
            (char) 9,
            (char) 11,
            (char) 12,
            (char) 32,
        };

        private string currentToken;
        public string CurrentToken
        {
            get {
                return currentToken;
            }

            private set {
                currentToken = value;
            }
        }

        private string currentLexeme;
        public string CurrentLexeme
        {
            get {
                return currentLexeme;
            }

            private set {
                currentLexeme = value;
            }
        }

        private string errorMessage = "";
        public string ErrorMessage
        {
            get {
                return errorMessage;
            }

            private set {
                errorMessage = value;
            }
        }

        public bool IsSuccessful
        {
            get {
                return ErrorMessage.Length == 0;
            }
        }

        private List<ThanosToken> tokens = new List<ThanosToken>();
        public List<ThanosToken> Tokens
        {
            get {
                return tokens;
            }
        }

        private bool IsExhausted = false;
        private StringBuilder SourceCode = new StringBuilder();

        public ThanosLexer(string sourceCode)
        {
            SourceCode.Append(sourceCode);
        }

        public bool HasNext()
        {
            if (IsExhausted)
                return false;

            if (SourceCode.Length <= 1)
            {
                IsExhausted = true;
                return false;
            }

            IgnoreBlankCharacters();

            if (FindNextToken())
                return true;

            IsExhausted = true;

            if (SourceCode.Length > 0)
                ErrorMessage = $"Simbolo inesperado: '{SourceCode[0]}'";

            return false;
        }

        private bool FindNextToken()
        {
            foreach (var i in ListaThanosTokens.Tokens)
            {
                string lexeme = i.Key;
                string tokenRegex = i.Value;

                int end = ListaThanosTokens.EndOfMatch(SourceCode.ToString(), tokenRegex);

                if (end != -1)
                {
                    CurrentLexeme = lexeme;
                    CurrentToken = SourceCode.ToString().Substring(0, end);

                    Tokens.Add(new ThanosToken(CurrentLexeme, CurrentToken));

                    SourceCode.Remove(0, end);

                    return true;
                }
            }

            return false;
        }

        private void IgnoreBlankCharacters()
        {
            int charsToDelete = 0;

            while (BlankCharacters.Contains(SourceCode[charsToDelete]))
            {
                charsToDelete++;
            }

            if (charsToDelete > 0)
            {
                SourceCode.Remove(0, charsToDelete);
            }
        }
    }
}
