using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationCourseNTier.Business.DTOs.User;

namespace WebApplicationCourseNTier.Business.Services.Abstractions
{
     public interface IAppUserService
    {
        public Task AddAsync(RegisterUserDto registerUserDto);
    }
}
