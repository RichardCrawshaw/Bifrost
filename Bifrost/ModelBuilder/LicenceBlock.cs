using System.Collections.Generic;
using System.Text;

namespace Bifrost.Model
{
    public class LicenceBlock
    {
        #region Properties

        public string Text { get; }

        #endregion

        #region Constructors

        private LicenceBlock(List<LicenceLine> lines)
        {
            var sb = new StringBuilder();
            foreach (var line in lines)
                sb.AppendLine($" *\t{line.Text}");
            this.Text = sb.ToString();
        }

        #endregion

        #region Methods

        public static LicenceBlock Create(List<LicenceLine> lines)
        {
            var result = new LicenceBlock(lines);
            return result;
        }

        #endregion
    }
}
