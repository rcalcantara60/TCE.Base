
using System.Collections.Generic;

namespace TCE.CrossCutting.Dto
{
    public class ValidationResultDto
    {
        public bool IsValid { get; set; }

        public IList<ValidationFailureDto> Errors { get; set; }
    }
}
