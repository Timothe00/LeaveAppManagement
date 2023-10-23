using LeaveAppManagement.businessLogic.Interfaces;
using LeaveAppManagement.dataAccess.Data;
using LeaveAppManagement.dataAccess.Dto;
using LeaveAppManagement.dataAccess.Interfaces;
using LeaveAppManagement.dataAccess.Models;
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


        //public async Task<Users> AddUsersAsync(UsersDto usersDto)
        //{
        //    Users users = new Users();
        //    Employee employee = new Employee();
        //    Manager manager = new Manager();
        //    Admin admin = new Admin();

        //    if (usersDto != null)
        //    {
        //        if (usersDto.Role == "Employee")
        //        {
        //            employee.FirstName = usersDto.FirstName;
        //            employee.LastName = usersDto.LastName;
        //            employee.Email = usersDto.Email;
        //            employee.Password = usersDto.Password;
        //            employee.PhoneNumber = usersDto.PhoneNumber;
        //            employee.Job = usersDto.Job;
        //            employee.Status = usersDto.Status;
        //            employee.Roles.Name = usersDto.Role;

        //            return await _usersRepository.AddUserAsync(employee);
                     
        //        }

        //    }




        //}



        public async Task<IEnumerable<UsersDto>> GetUsersAsync(CancellationToken cancellationToken)
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

        public async Task<UsersDto> GetUserByIdAsync(int id, CancellationToken cancellationToken)
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
