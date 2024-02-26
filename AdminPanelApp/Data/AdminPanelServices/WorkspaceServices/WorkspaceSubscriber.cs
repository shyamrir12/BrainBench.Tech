using AdminPanelModels;
using AdminPanelModels.UserMangment;
using LoginModels;
using System.Net.Http.Json;

namespace AdminPanelApp.Data.AdminPanelServices.WorkspaceServices
{
    public class WorkspaceSubscriber : IWorkspaceSubscriber
    {
        private readonly HttpClient _httpClient;

        public WorkspaceSubscriber(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Result<MessageEF>> AddWorkspace(Department model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<Department>($"/Workspace/AddWorkspace", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<MessageEF>>();
        }

        public async Task<Result<Department>> GetWorkspaceBYID(CommanRequest model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<CommanRequest>($"/Workspace/GetWorkspaceBYID", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<Department>>();
        }

        public async Task<Result<List<Department>>> GetWorkspaceList(CommanRequest model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<CommanRequest>($"/Workspace/GetWorkspaceList", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<List<Department>>>();
        }

        public async Task<Result<MessageEF>> ModifyStatusWorkspace(CommanRequest model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<CommanRequest>($"/Workspace/ModifyStatusWorkspace", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<MessageEF>>();
        }

        public async Task<Result<MessageEF>> UpdateWorkspace(Department model)
        {
            var httpMessageReponse = await _httpClient.PostAsJsonAsync<Department>($"/Workspace/UpdateWorkspace", model);

            return await httpMessageReponse.Content.ReadFromJsonAsync<Result<MessageEF>>();
        }
    }
}
