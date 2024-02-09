

using LeaveAppManagement.businessLogic.Interfaces;

namespace LeaveAppManagement.businessLogic.Services
{
    public class NotificationEmailToManagerService
    {
        private readonly IUsersService _usersService;

        public NotificationEmailToManagerService(IUsersService _usersService) 
        {
            _usersService = _usersService;
        }




    }
}
