using Moq;
using Structo.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonTestUtilities.Repositories // namespace para utilitários de teste relacionados a repositórios
{
    public class UnitOfWorkBuilder // classe para construir um IUnitOfWork falso para testes
    {
        public static IUnitOfWork Build()// método estático para construir um IUnitOfWork falso
        {
            var mock = new Mock<IUnitOfWork>();// cria um mock do IUnitOfWork

            return mock.Object; // essa é a implementação falsa do IUnitOfWork para testes
        }
    }
}
