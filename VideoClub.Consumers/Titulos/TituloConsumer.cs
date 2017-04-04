using AutoMapper;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoClub.Entities;
using VideoClub.Messages;
using VideoClub.Messages.Titles;
using VideoClub.Repositories;

namespace VideoClub.Consumers.Titles
{
    public class TitleConsumer : 
        IConsumer<CreateTitleMessage>
        , IConsumer<ListTitlesMessage>
    {
        private readonly TitlesRepository _repository;
        private readonly IMapper _mapper;

        public TitleConsumer(TitlesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public Task Consume(ConsumeContext<CreateTitleMessage> context)
        {
            var entity = _mapper.Map<CreateTitleMessage, TitleEntity>(context.Message);
            _repository.Create(entity);

            context.Respond<Response<bool>>(new ResponseMessage<bool> { Success = true });
            return context.CompleteTask;
        }

        public async Task Consume(ConsumeContext<ListTitlesMessage> context)
        {
            var entities = _repository.List();
            var response = new ResponseMessage<List<TitleEntity>> { Data = entities, Success = true };
            await context.RespondAsync<Response<List<TitleEntity>>>(response);
        }
    }
}
