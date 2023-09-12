using Application_test_repo.Repos;
using Application_test_repo.Services;
using Application_test_repo.Repos.Models;
using Application_test_repo.Modal;
using Application_test_repo.Helper;

namespace Application_test_repo.Services
{
    public interface ICustomersService
    {
        Task<List<CustomerModal>> GetAll();
        Task<CustomerModal> Getbycode(string code);
        Task<APIResponse> Remove(string code);
        Task<APIResponse> Create (CustomerModal data);
        Task<APIResponse> Update (CustomerModal data,string code);


    }
}
