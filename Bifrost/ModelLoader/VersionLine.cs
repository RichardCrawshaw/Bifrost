using System.Linq;

namespace Bifrost.Model
{
    public class VersionLine : Line
    {
        #region Properties

        public string Text { get; }

        #endregion

        #region Constructors

        private VersionLine(int number, string text)
            : base(number, text)
        {
            this.Text = string.Join(",", base.Items.Skip(1));
        }

        #endregion

        #region Methods

        public static new VersionLine Create(int number, string text)
        {
            var result = new VersionLine(number, text);
            return result;
        }

        #endregion
    }
}
