using Hw7.Contracts;
using Hw7.Data;
using Hw7.Enums;
using System.Data;
using System.Xml.Linq;

namespace Hw7.Service
{
    public class AuthenticationService : IAuthentication
    {
        public bool Login(string phoneNumber, string password)
        {
            foreach (var user in Storage.Users)
            {
                if (user.PhoneNumber == phoneNumber && user.Password == password)
                {
                    Storage.OnlineUser = user;
                    return true;
                }
            }
            return false;
        }
        public bool Register(string phoneNumber, string password,RoleEnum role)
        {
            int count = Storage.Users.Count + 1;
            Storage.Users.Add(new(count, phoneNumber, password,role));

            return true;
        }
        public void LogOut() => Storage.OnlineUser = null;
    }
}
