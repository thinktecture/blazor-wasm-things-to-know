using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BlazorWasmVirtualization.Server.Model;
using BlazorWasmVirtualization.Shared.Contracts;
using BlazorWasmVirtualization.Shared.DTO;
using Microsoft.EntityFrameworkCore;

namespace BlazorWasmVirtualization.Server.GrpcServices
{
    public class ConferencesService : IConferencesService
    {
        private readonly ConferencesDbContext _conferencesDbContext;
        private readonly IMapper _mapper;

        public ConferencesService(ConferencesDbContext conferencesDbContext, IMapper mapper)
        {
            _conferencesDbContext = conferencesDbContext;
            _mapper = mapper;
        }

        public async Task<ConferenceDetails> AddNewConferenceAsync(ConferenceDetails conference)
        {
            var conf = _mapper.Map<Conference>(conference);
            _conferencesDbContext.Conferences.Add(conf);
            await _conferencesDbContext.SaveChangesAsync();

            return _mapper.Map<ConferenceDetails>(conf);
        }

        public async Task<ConferenceDetails> GetConferenceDetailsAsync(ConferenceDetailsRequest request)
        {
            var conferenceDetails = await _conferencesDbContext.Conferences.FindAsync(request.ID);

            if (conferenceDetails == null)
            {
                return null;
            }

            return _mapper.Map<ConferenceDetails>(conferenceDetails);
        }

        public async Task<List<ConferenceOverview>> ListAllConferencesAsync()
        {
            var conferences = await _conferencesDbContext.Conferences.ToListAsync();
            var confs = _mapper.Map<List<ConferenceOverview>>(conferences);

            return confs;
        }

        public async Task<PagedResult<ConferenceOverview>> ListConferencesAsync(QueryParameters queryParameters)
        {
            var totalSize = await _conferencesDbContext.Conferences.CountAsync();
            var conferences = await _conferencesDbContext.Conferences
                .OrderBy(p => p.DateCreated)
                .Skip(queryParameters.StartIndex)
                .Take(queryParameters.PageSize)
                .ToListAsync();

            var confs = _mapper.Map<List<ConferenceOverview>>(conferences);

            return new PagedResult<ConferenceOverview> { Items = confs, TotalSize = totalSize };
        }
    }
}
