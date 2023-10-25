using LeaveAppManagement.businessLogic.Interfaces;
using LeaveAppManagement.dataAccess.Data;
using LeaveAppManagement.dataAccess.Dto;
using LeaveAppManagement.dataAccess.Interfaces;
using LeaveAppManagement.dataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace LeaveAppManagement.businessLogic.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }


        public async Task<Users> AddUsersServiceAsync(UsersDto usersDto)
        {
            Users users = new Users();

            if (usersDto != null)
            {
                if (usersDto.RoleId == 3)
                {
                    Employee employee = new Employee()
                    {
                        FirstName = usersDto.FirstName,
                        LastName = usersDto.LastName,
                        Email = usersDto.Email,
                        Password = usersDto.Password,
                        PhoneNumber = usersDto.PhoneNumber,
                        Job = usersDto.Job,
                        Status = usersDto.Status,
                        RoleId = usersDto.RoleId,
                    };

                 return await _usersRepository.AddUserAsync(employee);
                }
                else if(usersDto.RoleId == 2)
                {
                    Manager manager = new Manager() 
                    {
                        FirstName = usersDto.FirstName,
                        LastName = usersDto.LastName,
                        Email = usersDto.Email,
                        Password = usersDto.Password,
                        PhoneNumber = usersDto.PhoneNumber,
                        Job = usersDto.Job,
                        Status = usersDto.Status,
                        RoleId = usersDto.RoleId,
                    };
                    return await _usersRepository.AddUserAsync(manager);

                }else if(usersDto.RoleId == 1)
                {
                    Admin admin = new Admin() 
                    {
                        FirstName = usersDto.FirstName,
                        LastName = usersDto.LastName,
                        Email = usersDto.Email,
                        Password = usersDto.Password,
                        PhoneNumber = usersDto.PhoneNumber,
                        Job = usersDto.Job,
                        Status = usersDto.Status,
                        RoleId = usersDto.RoleId,
                    };
                    return await _usersRepository.AddUserAsync(admin);
                }
            }
            return users;
        }


        public async Task<Users> UpdateUserServiceAsync(UsersDto usersDto)
        {
            Users users = new Users();

            if (usersDto != null)
            {
                if (usersDto.RoleId == 3)
                {
                    Employee employee = new Employee()
                    {
                        FirstName = usersDto.FirstName,
                        LastName = usersDto.LastName,
                        Email = usersDto.Email,
                        Password = usersDto.Password,
                        PhoneNumber = usersDto.PhoneNumber,
                        Job = usersDto.Job,
                        Status = usersDto.Status,
                        RoleId = usersDto.RoleId,

                    };

                    return await _usersRepository.UpdateUserAsync(employee);
                }
                else if (usersDto.RoleId == 2)
                {
                    Manager manager = new Manager()
                    {
                        FirstName = usersDto.FirstName,
                        LastName = usersDto.LastName,
                        Email = usersDto.Email,
                        Password = usersDto.Password,
                        PhoneNumber = usersDto.PhoneNumber,
                        Job = usersDto.Job,
                        Status = usersDto.Status,
                        RoleId = usersDto.RoleId,
                    };
                    return await _usersRepository.UpdateUserAsync(manager);

                }
                else if (usersDto.RoleId == 1)
                {
                    Admin admin = new Admin()
                    {
                        FirstName = usersDto.FirstName,
                        LastName = usersDto.LastName,
                        Email = usersDto.Email,
                        Password = usersDto.Password,
                        PhoneNumber = usersDto.PhoneNumber,
                        Job = usersDto.Job,
                        Status = usersDto.Status,
                        RoleId = usersDto.RoleId,
                    };
                    return await _usersRepository.UpdateUserAsync(admin);
                }
            }
            return users;
        }



        public async Task<bool> DeleteUserServiceAsync(int userId)
        {
            if (userId < 0)
            {
                return false;
            }
            await _usersRepository.DeleteUserAsync(userId);
            return true;
        }



            public async Task<IEnumerable<UsersDto>> GetUserServiceAsync(CancellationToken cancellationToken)
        {
            IEnumerable<Users> users = await _usersRepository.GetUsersAsync(cancellationToken);

            IEnumerable<UsersDto> usersdto = users.Select(u => new UsersDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Password = u.Password,
                PhoneNumber = u.PhoneNumber,
                Job = u.Job,
                Status = u.Status,
                RoleId = u.RoleId,
                Role = u.Roles.Name
            });
            return usersdto;
        }

        public async Task<UsersDto> GetUserServiceByIdAsync(int id, CancellationToken cancellationToken)
        {
            IEnumerable<Users> users = await _usersRepository.GetUsersAsync(cancellationToken);
            var user = users.Where(us => us.Id == id).FirstOrDefault();
            UsersDto usersDto = new UsersDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                PhoneNumber = user.PhoneNumber,
                Job = user.Job,
                Status = user.Status,
                RoleId = user.RoleId,
                Role = user.Roles.Name
            };
            return usersDto;
        }
 






    }
}
