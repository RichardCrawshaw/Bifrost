namespace CBUS.Bifrost.Model
{
    public class OpCodePropertyLine : OpCodeLine
    {
        #region Properties

        protected override string TypeName { get; set; } = "Property";

        public string Name { get; }
        public string Source { get; }

        #endregion

        #region Constructors

        private OpCodePropertyLine(int number, string text)
            : base(number, text)
        {
            //   0        1           2          3      4
            // # "opcode",Value (hex),"property",Source,Name

            this.Source = this.Items[3].Trim('"');
            this.Name = this.Items[4].Trim('"');
        }

        #endregion

        #region Methods

        public new static OpCodePropertyLine Create(int number, string text)
        {
            var result = new OpCodePropertyLine(number, text);
            if (result.Items.Length < 5)
                return null;
            return result;
        }

        #endregion

        #region Overrides

        public override string ToString() => $"{base.ToString()}{this.Source} : {this.Name}";

        #endregion
    }
}
