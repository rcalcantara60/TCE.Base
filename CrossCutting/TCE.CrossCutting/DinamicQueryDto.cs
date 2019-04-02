using System.Linq;

namespace TCE.CrossCutting.Dto
{
    public class DinamicQueryDto
    {
        private string _valor;
        public string Campo { get; set; }
        public ComparadorDto Comparador { get; set; }
        public string Valor { get { return _valor; } set { _valor = value.ToUpper(); } }

        public override string ToString()
        {
            if (Comparador.Value == ComparadorDto.Contains.Value || Comparador.Value == ComparadorDto.StartsWith.Value)
                return $"{Campo}.ToUpper().Trim().{Comparador.Value}(\"{Valor}\")";
            var valorArray = Valor.Split('/');
            if (valorArray.Count() == 3)
                return $"DbFunctions.TruncateTime({Campo}) {Comparador.Value} DbFunctions.TruncateTime(DateTime({valorArray[2]}, {valorArray[1]}, {valorArray[0]}))";
            return $"{Campo} {Comparador.Value} {Valor}";
        }
    }
}