using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using BlazorWasmVirtualization.Shared.DTO;

namespace BlazorWasmVirtualization.Shared.Contracts
{
    [ServiceContract]
    public interface IConferencesService
    {
        Task<PagedResult<ConferenceOverview>> ListConferencesAsync(QueryParameters queryParameters);
        Task<ConferenceDetails> GetConferenceDetailsAsync(ConferenceDetailsRequest request);
        Task<ConferenceDetails> AddNewConferenceAsync(ConferenceDetails conference);
    }
}