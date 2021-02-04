namespace TouristSpot.UserServices
{
    public interface IUserService
    {
        string GetUserId();
        bool isAuthenticated();
        public string FullName();
    }
}