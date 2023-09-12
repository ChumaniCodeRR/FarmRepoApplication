using Application_test_repo.Services;
using Application_test_repo.Repos.Models;
using Application_test_repo.Repos;
using Application_test_repo.Modal;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Application_test_repo.Helper;
using Azure;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application_test_repo.Containers
{
    public class CustomerServicecs : ICustomersService
    {
        private readonly Test_DBContext _dbContext;
        private readonly IMapper mapper;
        private readonly ILogger<CustomerServicecs> _logger;
        public CustomerServicecs(Test_DBContext context,IMapper mapper,ILogger<CustomerServicecs>logger)
        {
            this._dbContext = context;
            this.mapper = mapper; 
            this._logger = logger;

        }

        public async Task<APIResponse> Create(CustomerModal data)
        {
            APIResponse response = new APIResponse();

            try
            {
                this._logger.LogInformation("Create Begins");
                TblCustomer _customer = this.mapper.Map<CustomerModal, TblCustomer>(data);
                await this._dbContext.TblCustomers.AddAsync(_customer);
                await this._dbContext.SaveChangesAsync();
                response.ResponseCode = 201;
                response.Result = data.Code;
            }
            catch(Exception ex)
            {
                response.ResponseCode = 400;
                response.ErrorMessage = ex.Message;
                this._logger.LogError(ex.Message,ex);
                
            }
            return response;
        }

        public async Task<List<CustomerModal>> GetAll()
        {
            List<CustomerModal> _response = new List<CustomerModal>();

            var _data = await this._dbContext.TblCustomers.ToListAsync();

            if(_data != null ) 
            {
                _response = this.mapper.Map<List<TblCustomer>,List<CustomerModal>>(_data);
            }
            return _response;

        }

        public async Task<CustomerModal> GetbyCode(string code)
        {
            CustomerModal _response = new CustomerModal();
            var _data = await this._dbContext.TblCustomers.FindAsync(code);
            if (_data != null)
            {
                _response = this.mapper.Map<TblCustomer, CustomerModal>(_data);
            }
            return _response;
        }

        public Task<CustomerModal> Getbycode(string code)
        {
            throw new NotImplementedException();
        }

        public async Task<APIResponse> Remove(string code)
        {
            APIResponse response = new APIResponse();

            try
            {
                var _customer = await this._dbContext.TblCustomers.FindAsync(code);
                if(_customer != null)
                {
                    this._dbContext.TblCustomers.Remove(_customer);
                    await this._dbContext.SaveChangesAsync();
                    response.ResponseCode = 200;
                    response.Result = code;
                }
                else
                {
                    response.ResponseCode = 404;
                    response.ErrorMessage = "Data not found";
                }
               
            }
            catch (Exception ex)
            {
                response.ResponseCode = 400;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> Update(CustomerModal data, string code)
        {
            APIResponse response = new APIResponse();

            try
            {
                var _customer = await this._dbContext.TblCustomers.FindAsync(code);
                if (_customer != null)
                {
                    _customer.Name = data.Name;
                    _customer.Email = data.Email;
                    _customer.Phone = data.Phone;
                    _customer.IsActive = data.IsActive;
                    _customer.CreditLimit = Convert.ToInt32(data.Creditlimit);

                    await this._dbContext.SaveChangesAsync();
                    response.ResponseCode = 200;
                    response.Result = code;
                }
                else
                {
                    response.ResponseCode = 404;
                    response.ErrorMessage = "Data not found";
                }

            }
            catch (Exception ex)
            {
                response.ResponseCode = 400;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }
    }
}
