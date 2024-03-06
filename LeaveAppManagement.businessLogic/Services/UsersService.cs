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

            // Vérifiez si l'e-mail existe dans la base de données
            var emailExists = await _usersRepository.CheckEmailExistsAsync(usersDto.Email);
            if (emailExists)
            {
                return null; // L'email existe déjà
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
            user.RoleId = usersDto.RoleId;
            user.HireDate = usersDto.HireDate;

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
            user.HireDate = usersDto.HireDate;
            user.PhoneNumber = usersDto.PhoneNumber;
            user.Job = usersDto.Job;
            user.RoleId = usersDto.RoleId;
            await _usersRepository.UpdateUserAsync(user, cancellationToken);
            return user;
        }

        public async Task<User> UserPasswordchangeServiceAsync(UserPasswordUpdateDto userpasswordupdateDto, CancellationToken cancellationToken)
        {
            User passwordupdate = new User
            {
                Id = userpasswordupdateDto.Id,
                Password = EncryptPassword.HashPswd(userpasswordupdateDto.Password),
            };
            await _usersRepository.UpdateUserPasswordAsync(passwordupdate, cancellationToken);
            return passwordupdate;
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
            var currentMonth = DateTime.Now.Month;

            IEnumerable<UsersDto> usersdto = users.Select(u => MapUserToDto(u, currentMonth));

            return usersdto;
        }

        private UsersDto MapUserToDto(User user, int currentMonth)
        {
            return new UsersDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                PhoneNumber = user.PhoneNumber,
                Job = user.Job,
                TotaLeaveAvailable = CalculateTotaLeaveAvailable(user.HireDate, currentMonth),
                RoleId = user.RoleId,
                RoleName = user.Role?.RoleName ?? "N/A",
                HireDate = user.HireDate,
            };
        }

        private static double CalculateTotaLeaveAvailable(DateTime hireDate, int currentMonth)
        {
            var difference = (DateTime.Now - hireDate).Days/30.0;
           
            double totaLeaveDaysAvailable = Math.Ceiling(difference) * 2.5; //2.5 est le nombre de jour de congé par mois par defaut selon l'entreprise
            return totaLeaveDaysAvailable;
        }



        public async Task<UsersDto> GetUserServiceByIdAsync(int id, CancellationToken cancellationToken)
        {
            IEnumerable<User> users = await _usersRepository.GetUsersAsync(cancellationToken);
            var user = users.Where(us => us.Id == id).FirstOrDefault();
            var CurrentMonth = DateTime.Now.Month;
            UsersDto usersDto = new UsersDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                PhoneNumber = user.PhoneNumber,
                Job = user.Job,
                TotaLeaveAvailable = CalculateTotaLeaveAvailable(user.HireDate, CurrentMonth),
                RoleId = user.RoleId,
                RoleName = user.Role.RoleName,
                HireDate = user.HireDate,
            };
            return usersDto;
        }

        //recuperer tout les managers
        private enum UserRoles { Admin = 1, Manager, Employee }
        public async Task<IEnumerable<UsersDto>> GetManagersServiceAsync(CancellationToken cancellationToken)
        {
            IEnumerable<User> managers = await _usersRepository.GetUsersByRoleIdAsync((int)UserRoles.Manager, cancellationToken);
            var currentMonth = DateTime.Now.Month;

            IEnumerable<UsersDto> managersDto = managers.Select(u => MapUserToDto(u, currentMonth));

            return managersDto;
        }

        public async Task<UsersDto?> GetSingleManagerServiceAsync(CancellationToken cancellationToken)
        {
            User? manager = await _usersRepository.GetSingleManagerByRoleIdAsync((int)UserRoles.Manager, cancellationToken);

            if (manager != null)
            {
                var currentMonth = DateTime.Now.Month;
                return MapUserToDto(manager, currentMonth);
            }
            else
            {
                return null; // Aucun manager trouvé
            }
        }




    }
}
