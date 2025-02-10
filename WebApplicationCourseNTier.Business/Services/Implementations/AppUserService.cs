using System;
using System.Threading.Tasks;
using WebApplicationCourseNTier.Business.DTOs.User;
using WebApplicationCourseNTier.Business.Services.Abstractions;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions.Base;
using WebApplicationCourseNTier.DataAccess.Entities;
using AutoMapper;

namespace WebApplicationCourseNTier.Business.Services.Implementations
{
    public class AppUserService : IAppUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IMapper _mapper;

        public AppUserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _appUserRepository = _unitOfWork.GetRepository<IAppUserRepository>();
            _mapper = mapper;
        }

        public async Task AddAsync(RegisterUserDto registerUserDto)
        {
            //// Check if user already exists by email
            //var existingUser = await _appUserRepository.GetByEmailAsync(postUserDto.Email);
            //if (existingUser != null)
            //{
            //    throw new InvalidOperationException("User with the same email already exists.");
            //}

            //// Map the PostUserDto to AppUser entity using AutoMapper
            //var appUser = _mapper.Map<AppUser>(postUserDto);

            //// Add the new user to the repository
            //await _appUserRepository.AddAsync(appUser);

            //// Commit the changes to the database
            //await _unitOfWork.CommitAsync();
        }
    }
}
