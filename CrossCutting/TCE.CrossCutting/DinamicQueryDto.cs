using System.Linq;

namespace TCE.CrossCutting.Dto
{
    public class DinamicQueryDto
    {
        public string Campo { get; set; }
        public ComparadorDto Comparador { get; set; }
        public string Valor { get; set; }

        public override string ToString()
        {
            if (Comparador.Value == ComparadorDto.Contains.Value || Comparador.Value == ComparadorDto.StartsWith.Value)
                return $"{Campo}.ToLower().Trim().{Comparador.Value.ToLower()}(\"{Valor}\")";
            var valorArray = Valor.Split('/');
            if (valorArray.Count() == 3)
                return $"DbFunctions.TruncateTime({Campo}) {Comparador.Value} DbFunctions.TruncateTime(DateTime({valorArray[2]}, {valorArray[1]}, {valorArray[0]}))";
            return $"{Campo} {Comparador.Value} {Valor}";
        }
    }
}