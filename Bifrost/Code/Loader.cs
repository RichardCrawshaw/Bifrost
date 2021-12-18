using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bifrost.Model;

namespace Bifrost
{
    public class Loader
	{
        #region Fields

        private readonly string filename;

        private readonly List<string> textLines = new();

        #endregion

        #region Properties

        public List<EnumerationLine> EnumerationLines { get; } = new();
        public List<FileCommentLine> FileCommentLines { get; } = new();
        public List<HistoryLine> HistoryLines { get; } = new();
        public List<LicenceLine> LicenceLines { get; } = new();
        public List<OpCodeLine> OpCodeLines { get; } = new();
        public List<PropertyLine> PropertyLines { get; } = new();

        public VersionLine VersionLine { get; private set; }

        public List<byte> OpCodeNumbers { get; } = new();
        public List<string> EnumerationNames { get; } = new();

        #endregion

        #region Constructors

        public Loader(string filename)
        {
            this.filename = filename;
        }

        #endregion

        #region Methods

        public void Load()
        {
            var textlines = File.ReadLines(this.filename);
            this.textLines.Clear();
            this.textLines.AddRange(textlines);

            var number = 1;
            var lines =
                this.textLines
                    .Select(n => Line.Create(number, n))
                    .Where(n => n != null)
                    .ToDictionary(n => ++number, n => n);

            this.EnumerationLines.Clear();
            this.EnumerationLines.AddRange(
                lines
                    .Select(n => n.Value as EnumerationLine)
                    .Where(n => n != null));

            this.FileCommentLines.Clear();
            this.FileCommentLines.AddRange(
                lines
                    .Select(n => n.Value as FileCommentLine)
                    .Where(n => n != null));

            this.HistoryLines.Clear();
            this.HistoryLines.AddRange(
                lines
                    .Select(n => n.Value as HistoryLine)
                    .Where(n => n != null));

            this.LicenceLines.Clear();
            this.LicenceLines.AddRange(
                lines
                    .Select(n => n.Value as LicenceLine)
                    .Where(n => n != null));

            this.OpCodeLines.Clear();
            this.OpCodeLines.AddRange(
                lines
                    .Select(n => n.Value as OpCodeLine)
                    .Where(n => n != null));

            this.PropertyLines.Clear();
            this.PropertyLines.AddRange(
                lines
                    .Select(n => n.Value as PropertyLine)
                    .Where(n => n != null));

            this.EnumerationNames.Clear();
            this.EnumerationNames.AddRange(
                this.EnumerationLines
                    .Select(n => n.EnumName)
                    .Distinct());

            this.VersionLine =
                lines
                    .Select(n => n.Value as VersionLine)
                    .Where(n => n != null)
                    .FirstOrDefault();

            this.OpCodeNumbers.Clear();
            this.OpCodeNumbers.AddRange(
                this.OpCodeLines
                    .Where(n => n is not OpCodeReservedLine)
                    .Select(n => n.Value)
                    .Distinct());
        }

        #endregion
    }
}
