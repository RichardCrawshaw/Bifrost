using System.Linq;

namespace CBUS.Bifrost.Model
{
    public class HistoryLine : Line
    {
        #region Properties

        public string Text { get; }

        #endregion

        #region Constructors

        private HistoryLine(int number, string text)
            : base(number, text)
        {
            // # "History",date(y-m-d),author,text

            this.Text = string.Join("\t", base.Items.Skip(1));
        }

        #endregion

        #region Methods

        public new static HistoryLine Create(int number, string text)
        {
            var result = new HistoryLine(number, text);
            return result;
        }

        #endregion

        #region Overrides

        public override string ToString() => this.Text;

        #endregion
    }
}
