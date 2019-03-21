using AutoMapper;
using FluentValidation.Results;

namespace TCE.CrossCutting.Dto
{
    public class AutoMapperHelper
    {
        public static ValidationResultDto GetValidationResultDto(ValidationResult validationResult)
        {
            return Mapper.Map<ValidationResult, ValidationResultDto>(validationResult);
        }
    }
}
