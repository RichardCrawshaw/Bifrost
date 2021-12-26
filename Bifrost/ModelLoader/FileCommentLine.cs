using System.Linq;

namespace CBUS.Bifrost.Model
{
    public class FileCommentLine : Line
    {
        #region Properties

        public string Text { get; }

        #endregion

        #region Constructors

        private FileCommentLine(int number, string text)
            : base(number, text)
        {
            // # "Comment",text

            this.Text = string.Join(",", base.Items.Skip(1));
        }

        #endregion

        #region Methods

        public new static FileCommentLine Create(int number, string text)
        {
            var result = new FileCommentLine(number, text);
            return result;
        }

        #endregion
    }
}
