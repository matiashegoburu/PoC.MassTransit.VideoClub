using AutoMapper;
using Magnum.Pipeline;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Entities;
using VideoClub.Messages.Members.Commands;
using VideoClub.Messages.Members.Responses;
using VideoClub.Repositories;

namespace VideoClub.Consumers.Members
{
    public class MembersConsumer : MassTransit.IConsumer<ICreateMemberCommand>
        , MassTransit.IConsumer<IListMembersCommand>
    {
        private readonly MembersRepository _repository;
        private readonly IMapper _mapper;

        public MembersConsumer(MembersRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task Consume(ConsumeContext<ICreateMemberCommand> context)
        {
            var entity = _mapper.Map<ICreateMemberCommand, MemberEntity>(context.Message);
            _repository.Create(entity);
            context.Respond<ICreateMemberResponse>(new CreateMemberResponse { Data = true, Success = true });
            return context.CompleteTask;
        }

        public Task Consume(ConsumeContext<IListMembersCommand> context)
        {
            var entities = _repository.List();
            context.Respond<IListMembersResponse>(new ListMembersResponse { Data = entities, Success = true });
            return context.CompleteTask;
        }       
    }
}
