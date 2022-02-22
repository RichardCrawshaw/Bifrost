using System.Globalization;

namespace CBUS.Bifrost.Model
{
    public class NodeNumbersIndividualLine : Line
    {
        #region Properties

        public string ModuleName { get; }
        public uint NodeNumber { get; }

        #endregion

        #region Constructors

        private NodeNumbersIndividualLine(int number, string text)
            : base(number, text)
        {
            // # NodeNumbers
            // # "NodeNumbers","individual",Number (hex),Module Name
            // # "NodeNumbers","range",Start (hex),Finish (hex),Description

            if (this.Items.Length < 4) return;

            if (uint.TryParse(this.Items[2].Substring(2), NumberStyles.HexNumber, null, out var nodeNumber))
                this.NodeNumber = nodeNumber;
            this.ModuleName = base.Items[3];
        }

        #endregion

        #region Methods

        public static new NodeNumbersIndividualLine Create(int number, string text)
        {
            var result = new NodeNumbersIndividualLine(number, text);
            if (result.Items.Length < 4) 
                return null;
            return result;
        }

        #endregion

        #region Overrides

        public override string ToString() => 
            $"{this.NodeNumber:X4} {this.ModuleName}";

        #endregion
    }

    public class NodeNumbersRangeLine : Line
    {
        #region Properties

        public uint Start { get; }
        public uint Finish { get; }
        public string Description { get; }

        #endregion

        #region Constructors

        private NodeNumbersRangeLine(int number, string text)
            : base(number, text)
        {
            // # NodeNumbers
            // # "NodeNumbers","individual",Number (hex),Module Name
            // # "NodeNumbers","range",Start (hex),Finish (hex),Description

            if (this.Items.Length < 5) return;

            if (uint.TryParse(this.Items[2].Substring(2), NumberStyles.HexNumber, null, out var start))
                this.Start = start;
            if (uint.TryParse(this.Items[3].Substring(2), NumberStyles.HexNumber, null, out var finish))
                this.Finish = finish;
            this.Description = base.Items[4];
        }

        #endregion

        #region Methods

        public static new NodeNumbersRangeLine Create(int number, string text)
        {
            var result=new NodeNumbersRangeLine(number,text);
            if (result.Items.Length < 5) 
                return null;
            return result;
        }

        #endregion

        #region Overrides

        public override string ToString() => 
            $"{this.Start:X4} - {this.Finish:X4} {this.Description}";

        #endregion
    }
}
