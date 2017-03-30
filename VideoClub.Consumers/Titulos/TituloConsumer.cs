﻿using AutoMapper;
using MassTransit;
using System;
using System.Threading.Tasks;
using VideoClub.Entities;
using VideoClub.Messages;
using VideoClub.Messages.Titulos;
using VideoClub.Repositories;

namespace VideoClub.Consumers.Titulos
{
    public class TituloConsumer : IConsumer<CreateTituloMessage>
    {
        private readonly TitulosRepository _repository;
        private readonly IMapper _mapper;

        public TituloConsumer(TitulosRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public Task Consume(ConsumeContext<CreateTituloMessage> context)
        {
            var entity = _mapper.Map<CreateTituloMessage, TituloEntity>(context.Message);
            _repository.Create(entity);

            context.Respond<Response<bool>>(new ResponseMessage<bool> { Success = true });
            return context.CompleteTask;
        }
    }
}