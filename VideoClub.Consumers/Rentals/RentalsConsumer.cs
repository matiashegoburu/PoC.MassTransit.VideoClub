using System;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using VideoClub.Entities;
using VideoClub.Messages.Rentals.Commands;
using VideoClub.Repositories;
using VideoClub.Messages.Rentals.Responses;

namespace VideoClub.Consumers.Rentals
{
    public class RentalsConsumer 
        : IConsumer<ICreateRentalCommand>
        , IConsumer<IListRentalsCommand>
    {
        private readonly RentalsRepository _repository;
        private readonly IMapper _mapper;

        public RentalsConsumer(RentalsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task Consume(ConsumeContext<ICreateRentalCommand> context)
        {
            var entity = _mapper.Map<ICreateRentalCommand, RentalEntity>(context.Message);
            _repository.Create(entity);
            context.Respond<ICreateRentalResponse>(new CreateRentalResponse { Data = true });
            return context.CompleteTask;
        }

        public Task Consume(ConsumeContext<IListRentalsCommand> context)
        {
            var entities = _repository.List();
            context.Respond<IListRentalsResponse>(new ListRentalsResponse { Data = entities, Success = true });
            return context.CompleteTask;
        }
    }
}
