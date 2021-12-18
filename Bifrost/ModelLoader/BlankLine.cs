namespace Bifrost.Model
{
    public class BlankLine : Line
    {
        #region Constructors

        private BlankLine(int number) : base(number, string.Empty) { }

        #endregion

        #region Methods

        public static BlankLine Create(int number)
        {
            var result = new BlankLine(number);
            return result;
        }

        #endregion
    }
}
