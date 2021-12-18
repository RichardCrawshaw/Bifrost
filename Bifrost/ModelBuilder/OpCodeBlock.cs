using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bifrost.Model
{
    public class OpCodeBlock
    {
        //# "opcode",Value (hex),"values",Code,Name,Priority,Group
        //# "opcode",Value (hex),"description",Text
        //# "opcode",Value (hex),"property",Source,Name
        //# "opcode",Value (hex),"tostring",Format-string
        //# "opcode",Value (hex),"comment",Text

        #region Properties

        public byte Value { get; private set; }
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Priority { get; private set; }
        public string Group { get; private set; }

        public string Description { get; private set; }

        public string ToStringText { get; private set; }

        public List<string> Comments { get; } = new();

        public List<OpCodeProperty> Properties { get; } = new();

        public string BaseClassName => $"OpCodeData{this.Value >> 5}";

        #endregion

        #region Constructors

        private OpCodeBlock(byte value, List<OpCodeLine> opCodeLines, List<PropertyLine> propertyLines)
        {
            this.Value = value;

            var opCodeValueLine =
                opCodeLines
                    .Where(n => n.Value == value)
                    .Select(n => n as OpCodeValueLine)
                    .Where(n => n != null)
                    .FirstOrDefault();

            this.Code = opCodeValueLine.Code;
            this.Name = opCodeValueLine.Name;
            this.Priority = opCodeValueLine.Priority;
            this.Group = opCodeValueLine.Group;

            var sb = new StringBuilder();
            opCodeLines
                .Where(n => n.Value == value)
                .Select(n => n as OpCodeDescriptionLine)
                .Where(n => n != null)
                .ToList()
                .ForEach(n => sb.AppendLine(n.Text));
            this.Description = sb.ToString().Trim(); ;

            this.ToStringText =
                opCodeLines
                    .Where(n => n.Value == value)
                    .Select(n => n as OpCodeToStringLine)
                    .Where(n => n != null)
                    .FirstOrDefault()?.Text;

            this.Comments.AddRange(
                opCodeLines
                    .Where(n => n.Value == value)
                    .Select(n => n as OpCodeCommentLine)
                    .Where(n => n != null)
                    .Select(n => n.Text));

            this.Properties =
                opCodeLines
                    .Where(n => n.Value == value)
                    .Select(n => n as OpCodePropertyLine)
                    .Where(n => n != null)
                    .Select(n => OpCodeProperty.Create(n, propertyLines))
                    .ToList();

            if (string.IsNullOrEmpty(this.ToStringText))
            {
                sb.Clear();
                sb.Append("{" + "this.Number" + "}");
                foreach (var property in this.Properties.OrderBy(p => p.Source))
                {
                    var format = GetFormat(property);
                    var variable = GetValue(property);
                    sb.Append(" {" + variable + format + "}");
                }
                this.ToStringText = "\"" + sb.ToString() + "\"";
            }
        }

        #endregion

        #region Methods

        public static OpCodeBlock Create(byte value, List<OpCodeLine> opCodeLines, List<PropertyLine> propertyLines)
        {
            var result = new OpCodeBlock(value, opCodeLines, propertyLines);
            return result;
        }

        #endregion

        #region Support routines

        private static string GetFormat(OpCodeProperty property)
        {
            return property.Format switch
            {
                "char" => string.Empty,
                "decimal" => string.Empty,
                "Enum" => ":F",
                "hex" => ":X2",
                _ => string.Empty,
            };
        }

        private static string GetValue(OpCodeProperty property)
        {
            switch (property.Format)
            {
                case "char":
                case "decimal":
                case "Enum":
                case "hex":
                    return "this." + property.Name;
                default:
                    if (property.Format.Contains('|'))
                    {
                        var items = property.Format.Split('|').Take(2).ToArray();
                        return "(this." + property.Name + " ? \"" + items[0] + "\" : \"" + items[1] + "\")";
                    }
                    return "this." + property.Name;
            }
        }

        #endregion
    }
}
