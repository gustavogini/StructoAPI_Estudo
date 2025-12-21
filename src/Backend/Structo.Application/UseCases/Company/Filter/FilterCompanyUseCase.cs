using AutoMapper;
using Structo.Communication.Requests;
using Structo.Communication.Responses;
using Structo.Domain.Extensions;
using Structo.Domain.Repositories.Company;
using Structo.Domain.Services.LoggedUser;
using Structo.Exceptions.ExceptionsBase;

namespace Structo.Application.UseCases.Company.Filter
{
    public class FilterCompanyUseCase : IFilterCompanyUseCase
    {

        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;
        readonly ICompanyReadOnlyRepository _repository;
        //private readonly IBlobStorageService _blobStorageService;

        public FilterCompanyUseCase(
            IMapper mapper,
            ICompanyReadOnlyRepository repository,
            ILoggedUser loggedUser)
            //IBlobStorageService blobStorageService)
        {
            _mapper = mapper;
            _loggedUser = loggedUser;
            _repository = repository;
            //_blobStorageService = blobStorageService;
        }

        public async Task<ResponseCompaniesJson> Execute(RequestFilterCompanyJson request)
        {
            Validate(request);

            var loggedUser = await _loggedUser.User();

            var filters = new Domain.Dtos.FilterCompanyDto
            {
                /*RecipeTitle_Ingredient = request.RecipeTitle_Ingredient,
                CookingTimes = request.CookingTimes.Distinct().Select(c => (Domain.Enums.CookingTime)c).ToList(),
                Difficulties = request.Difficulties.Distinct().Select(c => (Domain.Enums.Difficulty)c).ToList(),
                DishTypes = request.DishTypes.Distinct().Select(c => (Domain.Enums.DishType)c).ToList()*/
            };

            var companies = await _repository.Filter(filters);

            return new ResponseCompaniesJson
            {
                Companies = _mapper.Map<List<ResponseShortCompanyJson>>(companies)
            };
        }

        private static void Validate(RequestFilterCompanyJson request)
        {
            var validator = new FilterCompanyValidator();

            var result = validator.Validate(request);

            if (result.IsValid.IsFalse())
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).Distinct().ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }

    }
}
