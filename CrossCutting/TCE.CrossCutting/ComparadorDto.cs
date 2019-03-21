
namespace TCE.CrossCutting.Dto
{
    public class ComparadorDto
    {
        private ComparadorDto(string value) { Value = value; }
        private ComparadorDto(){ }
        public string Value { get; set; }
        public static ComparadorDto Contains { get { return new ComparadorDto("Contains"); } }
        public static ComparadorDto StartsWith { get { return new ComparadorDto("StartsWith"); } }
        public static ComparadorDto Equal { get { return new ComparadorDto("=="); } }
        public static ComparadorDto BiggerThen { get { return new ComparadorDto(">"); } }
        public static ComparadorDto BiggerOrEqual { get { return new ComparadorDto(">="); } }
        public static ComparadorDto LessThan { get { return new ComparadorDto("<"); } }
        public static ComparadorDto LessOrEqual { get { return new ComparadorDto("<="); } }
    }
}
