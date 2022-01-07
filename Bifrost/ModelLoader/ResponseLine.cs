namespace CBUS.Bifrost.Model
{
    public class ResponseLine : Line
    {
        #region Properties

        public string RequestOpCode { get; }
        public string ResponseOpCode { get; }
        public string RelationType { get; } // normal | error
        public string Comment { get; }

        #endregion

        #region Constructors

        private ResponseLine(int number, string text)
            : base(number, text)
        {
            //   0           1              2               3    4
            // # "responses",request-opcode,response-opcode,type,comment

            if (this.Items.Length < 5) return;

            this.RequestOpCode = this.Items[1];
            this.ResponseOpCode = this.Items[2];
            this.RelationType = this.Items[3];
            this.Comment = this.Items[4];
        }

        #endregion

        #region Methods

        public new static ResponseLine Create(int number, string text)
        {
            var result = new ResponseLine(number, text);
            if (result.Items.Length < 5)
                return null;
            return result;
        }

        #endregion

        #region Overrides

        public override string ToString() =>
            $"{this.RequestOpCode} {this.ResponseOpCode} ({this.RelationType}) // {this.Comment}";

        #endregion
    }
}
