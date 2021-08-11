using System.Linq;

namespace Prime.ViewModels
{
    public class AgreementCompareViewModel
    {
        public int InitialId { get; set; }
        public int FinalId { get; set; }

        /// <summary>
        /// Parses a string in the form {initialId}..{finalId}, à la GitHub's repo/compare/{hash1}..{hash2}.
        /// Returns null if the string is malformed.
        /// </summary>
        /// <param name="compareString"></param>
        public static AgreementCompareViewModel ParseCompareString(string compareString)
        {
            if (string.IsNullOrWhiteSpace(compareString))
            {
                return null;
            }

            var split = compareString.Split("..");

            if (split.Length != 2
                || split.Any(s => !int.TryParse(s, out _)))
            {
                return null;
            }

            return new AgreementCompareViewModel
            {
                InitialId = int.Parse(split[0]),
                FinalId = int.Parse(split[1])
            };
        }
    }
}
