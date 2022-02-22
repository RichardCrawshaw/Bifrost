namespace CBUS.Bifrost.Model
{
    public class NodeNumbersIndividualBlock
    {
        #region Properties

        public string ModuleName { get; }
        public uint NodeNumber { get; }

        #endregion

        #region Constructors

        private NodeNumbersIndividualBlock(NodeNumbersIndividualLine line)
        {
            this.NodeNumber = line.NodeNumber;
            this.ModuleName = line.ModuleName;
        }

        #endregion

        #region Methods

        public static NodeNumbersIndividualBlock Create(NodeNumbersIndividualLine line)
        {
            var result = new NodeNumbersIndividualBlock(line);
            return result;
        }

        #endregion

        #region Overrides

        public override string ToString() => $"0x{this.NodeNumber:X4} {this.ModuleName}";

        #endregion
    }

    public class NodeNumbersRangeBlock
    {
        #region Properties

        public uint Start { get; }
        public uint Finish { get; }
        public string Description { get; }

        #endregion

        #region Constructors

        private NodeNumbersRangeBlock(NodeNumbersRangeLine line)
        {
            this.Start = line.Start;
            this.Finish = line.Finish;
            this.Description = line.Description;
        }

        #endregion

        #region Methods

        public static NodeNumbersRangeBlock Create(NodeNumbersRangeLine line)
        {
            var result = new NodeNumbersRangeBlock(line);
            return result;
        }

        #endregion

        #region Overrides

        public override string ToString() => $"0x{this.Start:X4}-0x{this.Finish:X4} {this.Description}";

        #endregion
    }
}
