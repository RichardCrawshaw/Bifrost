namespace Bifrost.Model
{
    public class OpCodeValueLine : OpCodeLine
    {
        #region Properties

        protected override string TypeName { get; set; } = "Value";

        public string Code { get; }
        public string Name { get; }
        public string Priority { get; }
        public string Group { get; }

        #endregion

        #region Constructors

        private OpCodeValueLine(int number, string text)
            : base(number, text)
        {
            //   0        1           2        3    4    5        6
            // # "opcode",Value (hex),"values",Code,Name,Priority,Group

            if (this.Items.Length < 7) return;

            this.Code = this.Items[3].Trim('"');
            this.Name = this.Items[4].Trim('"');
            this.Priority = this.Items[5];
            this.Group = this.Items[6];
        }

        #endregion

        #region Methods

        public new static OpCodeValueLine Create(int number, string text)
        {
            var result = new OpCodeValueLine(number, text);
            if (result.Items.Length < 7)
                return null;
            return result;
        }

        #endregion

        #region Overrides

        public override string ToString() =>
            $"{base.ToString()}{this.Code} : {this.Name} : {this.Priority} : {this.Group}";

        #endregion
    }
}
