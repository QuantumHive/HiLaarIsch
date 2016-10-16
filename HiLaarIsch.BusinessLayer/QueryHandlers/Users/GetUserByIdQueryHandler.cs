using System;
using HiLaarIsch.Contract.DTOs;
using HiLaarIsch.Contract.Queries;
using QuantumHive.Core;

namespace HiLaarIsch.BusinessLayer.QueryHandlers.Users
{
    public class GetUserByIdQueryHandler : IQueryHandler<GetModelByIdQuery<UserView>, UserView>
    {
        public UserView Handle(GetModelByIdQuery<UserView> query)
        {
            throw new NotImplementedException();
        }
    }
}
