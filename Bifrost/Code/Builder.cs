using System.Collections.Generic;
using System.Linq;
using CBUS.Bifrost.Model;

namespace CBUS.Bifrost
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
        public List<ResponseBlock> ResponseBlocks { get; } = new List<ResponseBlock>();

        public List<int> OpCodeBaseAbstractClassSuffixes { get; } = new List<int>();

        public string Version { get; private set; }

        public List<NodeNumbersIndividualBlock> NodeNumbersIndividualBlocks { get; } = new List<NodeNumbersIndividualBlock>();

        public List<NodeNumbersRangeBlock> NodeNumbersRangeBlocks { get; } = new List<NodeNumbersRangeBlock>();

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

            this.ResponseBlocks.Clear();
            this.ResponseBlocks.AddRange(
                this.loader.ResponseLines
                    .Select(n => ResponseBlock.Create(n)));

            this.OpCodeBaseAbstractClassSuffixes.Clear();
            this.OpCodeBaseAbstractClassSuffixes.AddRange(
                this.loader.OpCodeNumbers
                    .Select(n => n >> 5)
                    .Distinct());

            this.Version = this.loader.VersionLine?.Text;

            this.NodeNumbersIndividualBlocks.Clear();
            this.NodeNumbersIndividualBlocks.AddRange(
                this.loader.NodeNumbersIndividualLines
                    .Select(n => NodeNumbersIndividualBlock.Create(n)));

            this.NodeNumbersRangeBlocks.Clear();
            this.NodeNumbersRangeBlocks.AddRange(
                this.loader.NodeNumbersRangeLines
                    .Select(n => NodeNumbersRangeBlock.Create(n)));
        }

        #endregion
    }
}
