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
using VideoClub.Messages.Titles;

namespace PoC.MassTransit.VideoClub.Controllers
{
    public class TitlesController : Controller
    {
        private readonly IBus _bus;
        private readonly IMapper _mapper;

        public TitlesController(IBus bus, IMapper mapper)
        {
            _bus = bus;
            _mapper = mapper;
        }

        // GET: Titles
        public async Task<ActionResult> Index(CancellationToken token)
        {
            var client = new MessageRequestClient<ListTitlesMessage, Response<List<TitleEntity>>>(_bus, Endpoints.Titles, TimeSpan.FromSeconds(30));
            var response = await client.Request(new ListTitlesCommand(), token);

            if (response.Success)
            {
                var models = _mapper.Map<List<TitleEntity>, List<TitleModel>>(response.Data);
                return View(models);
            }

            return View();
        }

        // GET: Titles/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Titles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Titles/Create
        [HttpPost]
        public async Task<ActionResult> Create(TitleModel model, CancellationToken token)
        {
            try
            {
                // TODO: Add insert logic here
                var message = new CreateTitleCommand
                {
                    Title = model.Title,
                    Description = model.Description,
                    Category = model.Category
                };

                var sendEnpoint = await _bus.GetSendEndpoint(Endpoints.Titles);
                var client = new MessageRequestClient<CreateTitleCommand, Response<bool>>(_bus, Endpoints.Titles, TimeSpan.FromSeconds(10));
                //await sendEnpoint.Send<CreateTitleMessage>(message);
                var response = await client.Request(message, token);
                return View();
            }
            catch(Exception e)
            {
                return View();
            }
        }

        // GET: Titles/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Titles/Edit/5
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

        // GET: Titles/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Titles/Delete/5
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
