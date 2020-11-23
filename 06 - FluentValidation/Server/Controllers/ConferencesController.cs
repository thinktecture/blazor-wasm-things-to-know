﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ConfTool.Server.Hubs;
using ConfTool.Server.Model;
using ConfTool.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ConfTool.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ConferencesController : ControllerBase
    {
        private readonly ILogger<ConferencesController> _logger;
        private readonly ConferencesDbContext _conferencesDbContext;
        private readonly IMapper _mapper;
        private readonly IHubContext<ConferencesHub> _hubContext;

        public ConferencesController(ILogger<ConferencesController> logger, 
            ConferencesDbContext conferencesDbContext, 
            IMapper mapper,
            IHubContext<ConferencesHub> hubContext)
        {
            _logger = logger;
            _conferencesDbContext = conferencesDbContext;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IEnumerable<ConferenceOverview>> Get()
        {
            var conferences = await _conferencesDbContext.Conferences.ToListAsync();

            return _mapper.Map<IEnumerable<ConferenceOverview>>(conferences);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConferenceDetails>> Get(string id)
        {
            var conferenceDetails = await _conferencesDbContext.Conferences.FindAsync(Guid.Parse(id));

            if (conferenceDetails == null)
            {
                return NotFound();
            }

            return _mapper.Map<ConferenceDetails>(conferenceDetails);
        }

        [HttpPost]
        public async Task<ActionResult<ConferenceDetails>> PostConference(ConferenceDetails conference)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var conf = _mapper.Map<Conference>(conference);
            _conferencesDbContext.Conferences.Add(conf);
            await _conferencesDbContext.SaveChangesAsync();

            await _hubContext.Clients.All.SendAsync("NewConferenceAdded");

            return CreatedAtAction("Get", new { id = conference.ID }, _mapper.Map<ConferenceDetails>(conf));
        }
    }
}
