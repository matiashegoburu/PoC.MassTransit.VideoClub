using AutoMapper;
using MassTransit;
using PoC.MassTransit.VideoClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VideoClub.Common;
using VideoClub.Entities;
using VideoClub.Messages;
using VideoClub.Messages.Members.Commands;
using VideoClub.Messages.Members.Responses;
using VideoClub.Messages.Rentals.Commands;
using VideoClub.Messages.Rentals.Responses;
using VideoClub.Messages.Titles;
using VideoClub.Messages.Titulos;

namespace PoC.MassTransit.VideoClub.Controllers
{
    public class RentalsController : Controller
    {
        private readonly IBus _bus;
        private readonly IMapper _mapper;

        public RentalsController(IBus bus, IMapper mapper)
        {
            _bus = bus;
            _mapper = mapper;
        }

        // GET: Rentals
        public async Task<ActionResult> Index(CancellationToken token)
        {
            var client = new MessageRequestClient<IListRentalsCommand, IListRentalsResponse>(_bus, Endpoints.Rentals, TimeSpan.FromSeconds(10));
            var response = await client.Request(new ListRentalsCommand(), token);
            var models = _mapper.Map<List<RentalEntity>, List<RentalModel>>(response.Data);

            return View(models);
        }

        // GET: Rentals/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Rentals/Create
        public async Task<ActionResult> Create(CancellationToken token)
        {
            await PopulateTitlesInViewBag(token);
            await PopulateMembersInViewBag(token);
            return View();
        }

        // POST: Rentals/Create
        [HttpPost]
        public async Task<ActionResult> Create(RentalModel model, CancellationToken token)
        {
            await PopulateTitlesInViewBag(token);
            try
            {
                // TODO: Add insert logic here
                var client = new MessageRequestClient<ICreateRentalCommand, ICreateRentalResponse>(_bus, Endpoints.Rentals, TimeSpan.FromSeconds(10));
                var command = _mapper.Map<RentalModel, CreateRentalCommand>(model);
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

        // GET: Rentals/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Rentals/Edit/5
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

        // GET: Rentals/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Rentals/Delete/5
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

        private async Task PopulateTitlesInViewBag(CancellationToken token)
        {
            var titlesClient = new MessageRequestClient<ListTitlesMessage, Response<List<TitleEntity>>>(_bus, Endpoints.Titles, TimeSpan.FromSeconds(30));
            var response = await titlesClient.Request(new ListTitlesCommand(), token);
            ViewBag.Titulos = response.Data.Select(_ => new SelectListItem { Text = _.Title, Value = _.Id.ToString() });
        }

        private async Task PopulateMembersInViewBag(CancellationToken token)
        {
            var client = new MessageRequestClient<IListMembersCommand, IListMembersResponse>(_bus, Endpoints.Members, TimeSpan.FromSeconds(10));
            var response = await client.Request(new ListMembersCommand(), token);
            ViewBag.Members = response.Data.Select(_ => new SelectListItem { Text = _.Name, Value = _.Id.ToString() });
        }
    }
}
