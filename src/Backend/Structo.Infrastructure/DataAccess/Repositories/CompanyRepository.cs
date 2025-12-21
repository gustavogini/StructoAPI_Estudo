using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Structo.Domain.Dtos;
using Structo.Domain.Entities;
using Structo.Domain.Extensions;
using Structo.Domain.Repositories.Company;

namespace Structo.Infrastructure.DataAccess.Repositories
{
    public sealed class CompanyRepository : ICompanyWriteOnlyRepository, ICompanyReadOnlyRepository, ICompanyUpdateOnlyRepository
    {
        private readonly StructoDbContext _dbContext;

        public CompanyRepository(StructoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Company company)
        {
            await _dbContext.Companies.AddAsync(company);
        }

        public async Task Delete(long companyId)
        {
            var company = await _dbContext.Companies.FindAsync(companyId);

            _dbContext.Companies.Remove(company!);
        }

        public async Task<IList<Company>> Filter(FilterCompanyDto filters)
        {
            var query = _dbContext
                .Companies
                .AsNoTracking()
                //.Include(c => c.)
                .Where(company => company.Active);

            if (filters.CompanyName.Any())
            {
                query = query.Where(company => company.CompanyName.Contains(filters.CompanyName));
            }

            if(filters.Cnpj.Any())
            {
                query = query.Where(company => company.Cnpj.Contains(filters.Cnpj));
            }

            if (filters.Phone.Any())
            {
                query = query.Where(company => company.Phone.Contains(filters.Phone));
            }

            if (filters.City.Any())
            {
                query = query.Where(company => company.City.Contains(filters.City));
            }

            /*if (filters.CompanyFantasyName.NotEmpty())
            {
                query = query.Where(company => company.CompanyFantasyName.Contains(filters.CompanyFantasyName)
                || company.);
            }*/

            return await query.ToListAsync(); //a query é executada aqui
        }



        async Task<Company?> ICompanyReadOnlyRepository.GetById(long companyId)
        {
            return await GetFullCompany()
                .AsNoTracking()
                .FirstOrDefaultAsync(company => company.Active);//esse é o unico codigo que precisa continuar duplicado por conta do AsNoTracking()
        }

        async Task<Company?> ICompanyUpdateOnlyRepository.GetById(long companyId)
        {
            return await GetFullCompany()
                .FirstOrDefaultAsync(company => company.Active);//esse é o unico codigo que precisa continuar duplicado por conta do AsNoTracking() - aqui nao pode usar o asnotracking
        }

        public void Update(Company company)
        {
            _dbContext.Companies.Update(company);
        }


        private IIncludableQueryable<Company, IList<Employee>> GetFullCompany()
        {
            return _dbContext
                .Companies
                .Include(c => c.Employees); //coloca o include quando precisa fazer join no banco de outras tabelas. sempre que a entidade tiver navegação para outras entidades

        }


    }
}
