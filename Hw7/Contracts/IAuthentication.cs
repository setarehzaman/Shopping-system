using Hw7.Enums;

namespace Hw7.Contracts
{
    public interface IAuthentication
    {
        public bool Login(string phoneNumber, string password);
        public bool Register(string phoneNumber, string password, RoleEnum role);
        public void LogOut();
    }
}