namespace Bifrost.Model
{
    public class CommentLine : Line
    {
        #region Properties

        public string Text { get; }

        #endregion

        #region Constructors

        private CommentLine(int number, string text)
            : base(number, text)
        {
            this.Text = text;
        }

        #endregion

        #region Methods

        public new static CommentLine Create(int number, string text)
        {
            var result = new CommentLine(number, text);
            return result;
        }

        #endregion
    }
}
