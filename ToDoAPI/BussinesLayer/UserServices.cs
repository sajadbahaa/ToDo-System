using DataLayer.Entities;
using Repositary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer
{
    public  class UserServices
    {
        readonly AppUserRepo _repo;
        public UserServices(AppUserRepo repo) 
        {  _repo = repo; }


        public async Task<(AppUser?, List<string>?)> LogainAsync(string UserName, string Password)
        {
            //await _repo.LoginAsync(UserName); ;
            var result = await _repo.LoginAsync(UserName, Password);

            // result هو (AppUser?, List<string>?)
            if (result.Item1 == null)
                return (null, null);
            return (result.Item1, result.Item2);
        }



    }
}
