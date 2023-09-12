using Application_test_repo.Repos;
using Application_test_repo.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Application_test_repo.Containers
{
    public class RefreshHandler : IRefreshHandlercs
    {
        public readonly Test_DBContext _dbContext;

        public RefreshHandler(Test_DBContext dbContext)
        {
            this._dbContext = dbContext;

        }
        public async Task<string> GenerateToken(string username)
        {
            var randomnumber = new byte[32];
            using (var randomnumbergenerator = RandomNumberGenerator.Create())
            {
                randomnumbergenerator.GetBytes(randomnumber);
                string refreshtoken = Convert.ToBase64String(randomnumber);
                var Existtoken = this._dbContext.TblRefreshtokens.FirstOrDefaultAsync(item => item.UserId == username).Result;

                if (Existtoken != null)
                {
                    Existtoken.RefreshToken = refreshtoken;
                }
                else
                {
                    await this._dbContext.TblRefreshtokens.AddAsync(new Repos.Models.TblRefreshtoken
                    {
                        UserId = username,
                        TokenId = new Random().Next().ToString(),
                        RefreshToken = refreshtoken

                    });
                }

                await this._dbContext.SaveChangesAsync();

                return refreshtoken;
            }
        }
    }
}
