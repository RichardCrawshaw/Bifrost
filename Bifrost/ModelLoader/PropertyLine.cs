namespace Bifrost.Model
{
    public class PropertyLine : Line
    {
        #region Properties

        public string Name { get; }
        public string DataType { get; }
        public string Format { get; }

        #endregion

        #region Constructors

        private PropertyLine(int number, string text)
            : base(number, text)
        {
            //   0          1    2        3
            // # "property",Name,DataType,Format

            if (this.Items.Length < 4) return;

            this.Name = this.Items[1];
            this.DataType = this.Items[2];
            this.Format = this.Items[3];
        }

        #endregion

        #region Methods

        public new static PropertyLine Create(int number, string text)
        {
            var result = new PropertyLine(number, text);
            if (result.Items.Length < 3)
                return null;
            return result;
        }

        #endregion
        
        #region Overrides

        public override string ToString() => $"{this.Name} : {this.DataType}";

        #endregion
    }
}
