using LeaveAppManagement.dataAccess.Dto;


namespace LeaveAppManagement.businessLogic.Interfaces
{
    public interface IUsersService
    {
        Task<IEnumerable<UsersDto>> GetUsersAsync(CancellationToken cancellationToken);
        Task<UsersDto> GetUserByIdAsync(int id, CancellationToken cancellationToken);
    }
}
