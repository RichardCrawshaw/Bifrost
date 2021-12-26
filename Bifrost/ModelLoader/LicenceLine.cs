using System.Linq;

namespace CBUS.Bifrost.Model
{
    public class LicenceLine : Line
    {
        #region Properties

        public string Text { get; }

        #endregion

        #region Constructors

        private LicenceLine(int number, string text)
            : base(number, text)
        {
            //   0         1
            // # "Licence",text

            this.Text = string.Join(",", base.Items.Skip(1));
        }

        #endregion

        #region Methods

        public new static LicenceLine Create(int number, string text)
        {
            var result = new LicenceLine(number, text);
            return result;
        }

        #endregion

        #region Overrides

        public override string ToString() => this.Text;

        #endregion
    }
}
