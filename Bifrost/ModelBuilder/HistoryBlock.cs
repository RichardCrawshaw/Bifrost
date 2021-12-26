using System.Collections.Generic;
using System.Text;

namespace CBUS.Bifrost.Model
{
    public class HistoryBlock
    {
        #region Properties

        public string Text { get; }

        #endregion

        #region Constructors

        private HistoryBlock(List<HistoryLine> lines)
        {
            var sb = new StringBuilder();
            foreach (var line in lines)
                sb.AppendLine($" *\t{line.Text}");
            this.Text = sb.ToString();
        }

        #endregion

        #region Methods

        public static HistoryBlock Create(List<HistoryLine> lines)
        {
            var result = new HistoryBlock(lines);
            return result;
        }

        #endregion
    }
}
