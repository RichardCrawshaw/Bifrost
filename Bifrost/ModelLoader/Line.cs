namespace Bifrost.Model
{
    public abstract class Line
    {
        #region Properties

        public int Number { get; set; }

        protected string[] Items { get; set; }

        #endregion

        #region Constructors

        protected Line(int number, string text)
        {
            this.Number = number;

            this.Items = text.Split(',');
        }

        #endregion

        #region Methods

        public static Line Create(int number, string text)
        {
            if (text.Trim().StartsWith("#") ||
                text.Trim().StartsWith(";") ||
                text.Trim().StartsWith("!") ||
                text.Trim().StartsWith("//") ||
                text.Trim().StartsWith("--"))
            {
                return CommentLine.Create(number, text);
            }

            if (string.IsNullOrEmpty(text))
                return BlankLine.Create(number);

            if (text.Trim().StartsWith("comment,"))
                return FileCommentLine.Create(number, text);

            if (text.Trim().StartsWith("enumeration,"))
                return EnumerationLine.Create(number, text);

            if (text.Trim().StartsWith("History,"))
                return HistoryLine.Create(number, text);

            if (text.Trim().StartsWith("Licence,"))
                return LicenceLine.Create(number, text);

            if (text.Trim().StartsWith("opcode,"))
                return OpCodeLine.Create(number, text);

            if (text.Trim().StartsWith("property,"))
                return PropertyLine.Create(number, text);

            if (text.Trim().StartsWith("Version,"))
                return VersionLine.Create(number, text);

            return null;
        }

        #endregion
    }
}
