using Structo.Communication.Responses;
using Structo.Domain.Repositories;
using Structo.Domain.Repositories.Company;
using Structo.Domain.Services.LoggedUser;
using Structo.Exceptions;
using Structo.Exceptions.ExceptionsBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Structo.Application.UseCases.Company.Delete
{
    public class DeleteCompanyUseCase : IDeleteCompanyUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly ICompanyReadOnlyRepository _repositoryRead;
        private readonly ICompanyWriteOnlyRepository _repositoryWrite;
        private readonly IUnitOfWork _unitOfWork;


        public DeleteCompanyUseCase(
            ILoggedUser loggedUser,
            ICompanyReadOnlyRepository repositoryRead,
            ICompanyWriteOnlyRepository repositoryWrite,
            IUnitOfWork unitOfWork)
        {
            _loggedUser = loggedUser;
            _repositoryRead = repositoryRead;
            _repositoryWrite = repositoryWrite;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(long companyId)
        {
            var loggedUser = await _loggedUser.User();

            var company = await _repositoryRead.GetById(companyId);

            if (company is null)
            {
                throw new NotFoundException(ResourceMessagesException.COMPANY_NOT_FOUND);
            }

            await _repositoryWrite.Delete(companyId);

            await _unitOfWork.Commit();
        }
    }
}
