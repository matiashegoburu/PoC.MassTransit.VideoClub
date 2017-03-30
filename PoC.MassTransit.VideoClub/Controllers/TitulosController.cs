﻿using MassTransit;
using PoC.MassTransit.VideoClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VideoClub.Common;
using VideoClub.Messages;
using VideoClub.Messages.Titulos;

namespace PoC.MassTransit.VideoClub.Controllers
{
    public class TitulosController : Controller
    {
        private readonly IBus _bus;

        public TitulosController(IBus bus)
        {
            _bus = bus;
        }

        // GET: Titulos
        public ActionResult Index()
        {
            return View();
        }

        // GET: Titulos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Titulos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Titulos/Create
        [HttpPost]
        public async Task<ActionResult> Create(TituloModel model, CancellationToken token)
        {
            try
            {
                // TODO: Add insert logic here
                var message = new CreateTituloCommand
                {
                    Titulo = model.Titulo,
                    Descripcion = model.Descripcion,
                    Genero = model.Genero
                };

                var sendEnpoint = await _bus.GetSendEndpoint(Endpoints.Titulos);
                var client = new MessageRequestClient<CreateTituloCommand, Response<bool>>(_bus, Endpoints.Titulos, TimeSpan.FromSeconds(10));
                //await sendEnpoint.Send<CreateTituloMessage>(message);
                var response = await client.Request(message, token);
                return View();
            }
            catch(Exception e)
            {
                return View();
            }
        }

        // GET: Titulos/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Titulos/Edit/5
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

        // GET: Titulos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Titulos/Delete/5
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
