using Structo.Communication.Requests;
using Structo.Domain.Extensions;
using Structo.Domain.Repositories;
using Structo.Domain.Repositories.User;
using Structo.Domain.Services.LoggedUser;
using Structo.Exceptions;
using Structo.Exceptions.ExceptionsBase;

namespace Structo.Application.UseCases.User.Update
{
    public class UpdateUserUseCase : IUpdateUserUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IUserUpdateOnlyRepository _repository;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;


        public UpdateUserUseCase(
            ILoggedUser loggedUser,
            IUserUpdateOnlyRepository repository,
            IUserReadOnlyRepository userReadOnlyRepository,
            IUnitOfWork unitOfWork)
        {
            _loggedUser = loggedUser;
            _repository = repository;
            _userReadOnlyRepository = userReadOnlyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(RequestUpdateUserJson request)
        {
            var loggedUser = await _loggedUser.User();

            await Validate(request, loggedUser.Email);

            var user = await _repository.GetById(loggedUser.Id);

            user.Username = request.Name;
            user.Email = request.Email;

            _repository.Update(user);

            await _unitOfWork.Commit();
        }

        private async Task Validate(RequestUpdateUserJson request, string currentEmail)
        {
            var validator = new UpdateUserValidator();

            var result = await validator.ValidateAsync(request);

            if (currentEmail.Equals(request.Email).IsFalse())
            {
                var userExist = await _userReadOnlyRepository.ExistActiveUserWtihEmail(request.Email);
                if (userExist)
                    result.Errors.Add(new FluentValidation.Results.ValidationFailure("email", ResourceMessagesException.EMAIL_ALREADY_REGISTERED));
            }

            if (result.IsValid.IsFalse())
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }

    
}
