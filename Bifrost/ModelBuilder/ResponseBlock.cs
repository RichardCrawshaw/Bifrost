namespace CBUS.Bifrost.Model
{
    public class ResponseBlock
    {
        //   0           1              2               3    4
        // # "responses",request-opcode,response-opcode,type,comment

        #region Properties

        public string RequestOpCode { get; private set; }
        public string ResponseOpCode { get; private set; }
        public string RelationType { get; private set; } // normal | error
        public string Comment { get; private set; }

        #endregion

        #region Constructors

        private ResponseBlock() { }

        #endregion

        #region Methods

        internal static ResponseBlock Create(ResponseLine value)
        {
            var result = new ResponseBlock()
            {
                Comment = value.Comment,
                RelationType = value.RelationType,
                RequestOpCode = value.RequestOpCode,
                ResponseOpCode = value.ResponseOpCode,
            };
            return result;
        }

        #endregion

        #region Overrides

        public override string ToString() =>
            $"Req: {this.RequestOpCode,-5} Rsp: {this.ResponseOpCode,-6} ({this.RelationType,-6}) // {this.Comment}";

        #endregion
    }
}
