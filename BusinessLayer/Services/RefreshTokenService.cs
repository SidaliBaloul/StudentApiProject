using Student.Business.Interfaces;
using Student.Data.Entities;
using Student.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.Business.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _Repository;

        public RefreshTokenService(IRefreshTokenRepository repository)
        {
            _Repository = repository;
        }

        public async Task<RefreshToken> GetRefreshTokenAsync(int userid)
        {
            return await _Repository.GetRefreshTokenAsync(userid);
        }

        public async Task AddNewRefreshToken(RefreshToken refreshtoken)
        {
            await _Repository.AddNewRefreshToken(refreshtoken);
        }

        public async Task UpdateRefreshToken(RefreshToken refreshToken)
        {
            await _Repository.UpdateRefreshToken(refreshToken);
        }
    }
}
