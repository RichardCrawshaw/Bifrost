using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using CBUS.Bifrost.Model;

namespace CBUS.Bifrost
{
    public class Loader
	{
        #region Fields

        private static readonly Lazy<List<string>> resourceNames=
            new Lazy<List<string>>(() => GetEmbeddedResourceNames());

        private readonly string name;

        private readonly List<string> textLines = new List<string>();

        #endregion

        #region Properties

        public List<EnumerationLine> EnumerationLines { get; } = new List<EnumerationLine>();
        public List<FileCommentLine> FileCommentLines { get; } = new List<FileCommentLine>();
        public List<HistoryLine> HistoryLines { get; } = new List<HistoryLine>();
        public List<LicenceLine> LicenceLines { get; } = new List<LicenceLine>();
        public List<OpCodeLine> OpCodeLines { get; } = new List<OpCodeLine>();
        public List<PropertyLine> PropertyLines { get; } = new List<PropertyLine>();

        public VersionLine VersionLine { get; private set; }

        public List<byte> OpCodeNumbers { get; } = new List<byte>();
        public List<string> EnumerationNames { get; } = new List<string>();

        public static List<string> VersionNames => resourceNames.Value;

        #endregion

        #region Constructors

        public Loader(string name)
        {
            this.name = name;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Load the data from the file specified by the name passed to the .ctor.
        /// </summary>
        /// <exception cref="FileNotFoundException">If the file does not exist.</exception>
        public void LoadFile()
        {
            if (!File.Exists(name))
                throw new FileNotFoundException(name);

            var lines = File.ReadLines(this.name);
            Load(lines.ToArray());
        }

        /// <summary>
        /// Load the data from the resource specified by the name passed to the .ctor.
        /// </summary>
        /// <exception cref="InvalidOperationException">If the resource does not exist.</exception>
        public void LoadResource()
        {
            var name = this.name;
            if (!name.StartsWith("CBUS.Bifrost.")) name = "CBUS.Bifrost." + name;
            if (!name.EndsWith(".txt")) name += ".txt";

            if (!resourceNames.Value.Contains(name))
                throw new InvalidOperationException($"'{this.name}' is not a known resource.");

            var text = ReadEmbeddedFile(name);
            var lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            Load(lines);
        }

        #endregion

        #region Support routines

        private static List<string> GetEmbeddedResourceNames()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceNames= assembly.GetManifestResourceNames();
            return resourceNames.ToList();
        }

        private void Load(string[] textlines)
        {
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
                    .Where(n => !(n is OpCodeReservedLine))
                    .Select(n => n.Value)
                    .Distinct());
        }

        private string ReadEmbeddedFile(string name)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using(var stream = assembly.GetManifestResourceStream(name))
            {
                using (var reader = new StreamReader(stream))
                {
                    var result = reader.ReadToEnd();
                    return result;
                }
            }
        }

        #endregion
    }
}
