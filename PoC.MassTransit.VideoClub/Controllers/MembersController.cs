using AutoMapper;
using MassTransit;
using PoC.MassTransit.VideoClub.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using VideoClub.Common;
using VideoClub.Entities;
using VideoClub.Messages.Members.Commands;
using VideoClub.Messages.Members.Responses;

namespace PoC.MassTransit.VideoClub.Controllers
{
    public class MembersController : Controller
    {
        private readonly IBus _bus;
        private readonly IMapper _mapper;

        public MembersController(IBus bus, IMapper mapper)
        {
            _bus = bus;
            _mapper = mapper;
        }

        // GET: Members
        public async Task<ActionResult> Index(CancellationToken token)
        {
            var client = new MessageRequestClient<IListMembersCommand, IListMembersResponse>(_bus, Endpoints.Members, TimeSpan.FromSeconds(10));
            var response = await client.Request(new ListMembersCommand(), token);

            if (response.Success)
            {
                var models = _mapper.Map<List<MemberEntity>, List<MemberModel>>(response.Data);

                return View(models);
            }

            return View();
        }

        // GET: Members/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Members/Create
        public async Task<ActionResult> Create(CancellationToken token)
        {
            return View();
        }

        // POST: Members/Create
        [HttpPost]
        public async Task<ActionResult> Create(RentalModel model, CancellationToken token)
        {
            try
            {
                // TODO: Add insert logic here
                var client = new MessageRequestClient<ICreateMemberCommand, ICreateMemberResponse>(_bus, Endpoints.Members, TimeSpan.FromSeconds(10));
                var command = _mapper.Map<RentalModel, CreateMemberCommand>(model);
                var response = await client.Request(command, token);

                if (response.Success)
                    return RedirectToAction("Index");
                else
                    return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Members/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Members/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Members/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Members/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}