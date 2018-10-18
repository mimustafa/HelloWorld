﻿using API.Contracts.Dog.Messaging;
using WCF.HelloWorld.Data;
using WCF.HelloWorld.Repository;
using WCF.LIB;

namespace WCF.Handler.Dog
{
    public class GetDogHandler : RequestHandler<GetDogReq, GetDogResp>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IHelloWorldRepositoryFactory _repositoryFactory;
        private readonly IHelloWorldMapperFactory _mapperFactory;

        public GetDogHandler(IUnitOfWorkFactory unitOfWorkFactory, IHelloWorldRepositoryFactory repositoryFactory, IHelloWorldMapperFactory mapperFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _repositoryFactory = repositoryFactory;
            _mapperFactory = mapperFactory;
        }
        public override GetDogResp Process(GetDogReq req)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateAndBeginTransactionForHelloWorld(false))
            {
                return new GetDogResp { Dog = _mapperFactory.CreateDogMapper(unitOfWork).Map(_repositoryFactory.CreateDogRepository(unitOfWork).LoadAll()[0]) };
            }
        }
    }
}
