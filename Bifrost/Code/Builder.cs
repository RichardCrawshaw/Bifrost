using System.Collections.Generic;
using System.Linq;
using Bifrost.Model;

namespace Bifrost
{
    public class Builder
    {
        #region Fields

        private readonly Loader loader;

        #endregion

        #region Properties

        public FileCommentBlock FileCommentBlock { get; private set; }
        public HistoryBlock HistoryBlock { get; private set; }
        public LicenceBlock LicenceBlock { get; private set; }
        public List<OpCodeBlock> OpCodeBlocks { get; } = new List<OpCodeBlock>();

        public List<int> OpCodeBaseAbstractClassSuffixes { get; } = new List<int>();

        public string Version { get; private set; }

        #endregion

        #region Constructors

        public Builder(Loader loader)
        {
            this.loader = loader;
        }

        #endregion

        #region Methods

        public void Build()
        {
            this.FileCommentBlock = FileCommentBlock.Create(this.loader.FileCommentLines);
            this.HistoryBlock = HistoryBlock.Create(this.loader.HistoryLines);
            this.LicenceBlock = LicenceBlock.Create(this.loader.LicenceLines);

            this.OpCodeBlocks.Clear();
            foreach (var value in this.loader.OpCodeNumbers)
            {
                var opCodeBlock = OpCodeBlock.Create(value, this.loader.OpCodeLines, this.loader.PropertyLines);
                this.OpCodeBlocks.Add(opCodeBlock);
            }

            this.OpCodeBaseAbstractClassSuffixes.Clear();
            this.OpCodeBaseAbstractClassSuffixes.AddRange(
                this.loader.OpCodeNumbers
                    .Select(n => n >> 5)
                    .Distinct());

            this.Version = this.loader.VersionLine?.Text;
        }

        #endregion
    }
}
