namespace CBUS.Bifrost.Model
{
    public class OpCodeCommentLine : OpCodeLine
    {
        #region Properties

        protected override string TypeName { get; set; } = "Comment";

        public string Text { get; }

        #endregion

        #region Constructors

        private OpCodeCommentLine(int number, string text)
            : base(number, text)
        {
            //   0        1           2         3
            // # "opcode",Value (hex),"comment",Text

            if (this.Items.Length < 4) return;

            this.Text = this.Items[3];
        }

        #endregion

        #region Methods

        public new static OpCodeCommentLine Create(int number, string text)
        {
            var result = new OpCodeCommentLine(number, text);
            if (result.Items.Length < 4)
                return null;
            return result;
        }

        #endregion

        #region Overrides

        public override string ToString() => $"{base.ToString()}{this.Text}";

        #endregion
    }
}
