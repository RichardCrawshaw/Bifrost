using System.Collections.Generic;
using System.Text;

namespace CBUS.Bifrost.Model
{
    public class FileCommentBlock
    {
        #region Properties

        public string Text { get; }

        #endregion

        #region Constructors

        private FileCommentBlock(List<FileCommentLine> lines)
        {
            var sb = new StringBuilder();
            foreach (var line in lines)
                sb.AppendLine($" *\t{line.Text}");
            this.Text = sb.ToString();
        }

        #endregion

        #region Methods

        public static FileCommentBlock Create(List<FileCommentLine> lines)
        {
            var result = new FileCommentBlock(lines);
            return result;
        }

        #endregion
    }
}

