using LeaveAppManagement.businessLogic.Interfaces;
using LeaveAppManagement.businessLogic.Utility;
using LeaveAppManagement.dataAccess.Dto;
using LeaveAppManagement.dataAccess.Interfaces;
using LeaveAppManagement.dataAccess.Models;


namespace LeaveAppManagement.businessLogic.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }


        public async Task<User?> AddUsersServiceAsync(CreateUserDto usersDto)
        {

            // Check if the email exists in the database
            var emailExists = await _usersRepository.CheckEmailExistsAsync(usersDto.Email);
            if (emailExists)
            {
                return null; // Email already exists
            }


            User? user = null;

            switch (usersDto.RoleId)
            {
                case 3:
                    user = new Employee();
                    break;
                case 2:
                    user = new Manager();
                    break;
                case 1:
                    user = new Admin();
                    break;
                default:
                    return null;
            }

            user.FirstName = usersDto.FirstName;
            user.LastName = usersDto.LastName;
            user.Email = usersDto.Email;
            user.Password = EncryptPassword.HashPswd(usersDto.Password);
            user.PhoneNumber = usersDto.PhoneNumber;
            user.Job = usersDto.Job;
            user.IsActiveUser = usersDto.IsActiveUser;
            user.RoleId = usersDto.RoleId;

            return await _usersRepository.AddUserAsync(user);
        }


        public async Task<User?> UpdateUserServiceAsync(UpdateUserDto usersDto, CancellationToken cancellationToken)
        {
            if (usersDto == null)
            {
                return null;
            }

            User? user = null;

            switch (usersDto.RoleId)
            {
                case 3:
                    user = new Employee();
                    break;
                case 2:
                    user = new Manager();
                    break;
                case 1:
                    user = new Admin();
                    break;
                default:
                    return null; // Gérer le cas où RoleId n'est pas valide.
            }

            // Initialisation des propriétés communes à tous les types d'utilisateurs.
            user.Id = usersDto.Id;
            user.FirstName = usersDto.FirstName;
            user.LastName = usersDto.LastName;
            user.Email = usersDto.Email;
            user.Password = usersDto.Password;
            user.PhoneNumber = usersDto.PhoneNumber;
            user.Job = usersDto.Job;
            user.IsActiveUser = usersDto.IsActiveUser;
            user.RoleId = usersDto.RoleId;
            await _usersRepository.UpdateUserAsync(user, cancellationToken);
            return user;
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
            IEnumerable<User> users = await _usersRepository.GetUsersAsync(cancellationToken);

            IEnumerable<UsersDto> usersdto = users.Select(u => new UsersDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Password = u.Password,
                PhoneNumber = u.PhoneNumber,
                Job = u.Job,
                IsActiveUser = u.IsActiveUser,
                RoleId = u.RoleId,
                RoleName = u.Role.RoleName
            });
            return usersdto;
        }

        public async Task<UsersDto> GetUserServiceByIdAsync(int id, CancellationToken cancellationToken)
        {
            IEnumerable<User> users = await _usersRepository.GetUsersAsync(cancellationToken);
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
                IsActiveUser = user.IsActiveUser,
                RoleId = user.RoleId,
                RoleName = user.Role.RoleName
            };
            return usersDto;
        }







    }
}
