using System;
using System.Text.RegularExpressions;

namespace ControledeGastos.Services
{
    public class PasswordForceService
    {
        public static int PasswordScoreGenerate(string password)
        {
            if (password == null) return 0;
            int scoreBySize = ScoreBySize(password);
            int scoreByLowercase = ScoreByLowercase(password);
            int scoreByUppercase = ScoreByUppercase(password);
            int scoreByNumbers = ScoreByNumbers(password);
            int scoreBySymbols = ScoreBySymbols(password);
            int scoreByRepetition = ScoreByRepetition(password);
            return scoreBySize + scoreByLowercase + scoreByUppercase + scoreByNumbers + scoreBySymbols - scoreByRepetition;
        }


        private static int ScoreBySize(string password)
        {
            return Math.Min(10, password.Length) * 6;
        }

        private static int ScoreByLowercase(string password)
        {
            int rawscore = password.Length - Regex.Replace(password, "[a-z]", "").Length;
            return Math.Min(2, rawscore) * 5;
        }

        private static int ScoreByUppercase(string password)
        {
            int rawscore = password.Length - Regex.Replace(password, "[A-Z]", "").Length;
            return Math.Min(2, rawscore) * 5;
        }

        private static int ScoreByNumbers(string password)
        {
            int rawscore = password.Length - Regex.Replace(password, "[0-9]", "").Length;
            return Math.Min(2, rawscore) * 5;
        }

        private static int ScoreBySymbols(string password)
        {
            int rawscore = Regex.Replace(password, "[a-zA-Z0-9]", "").Length;
            return Math.Min(2, rawscore) * 5;
        }

        private static int ScoreByRepetition(string password)
        {
            Regex regex = new Regex(@"(\w)*.*\1");
            bool repeat = regex.IsMatch(password);
            if (repeat)
            {
                return 30;
            }
            else
            {
                return 0;
            }
        }

        public static ScorePassword GetScorePassword(string password)
        {
            int placar = PasswordScoreGenerate(password);

            if (placar < 50)
                return ScorePassword.Inaceitavel;
            else if (placar < 60)
                return ScorePassword.Fraca;
            else if (placar < 80)
                return ScorePassword.Media;
            else if (placar < 100)
                return ScorePassword.Forte;
            else
                return ScorePassword.Segura;
        }

        public enum ScorePassword
        {
            Inaceitavel,
            Fraca,
            Media,
            Forte,
            Segura
        }
    }
}
