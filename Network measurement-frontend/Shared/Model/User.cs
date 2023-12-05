using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network_measurement_frontend.Shared
{
    public partial class User
    {
        public User(string username, string userEmail, string password, int userRoleId, string firstname, string lastname)
        {
            Username = username;
            UserEmail = userEmail;
            Password = password;
            UserRoleId = userRoleId;
            Firstname = firstname;
            Lastname = lastname;
        }

        public User(int userId, string username, string userEmail, string password, int userRoleId, string firstname, string lastname)
        {
            UserId = userId;
            Username = username;
            UserEmail = userEmail;
            Password = password;
            UserRoleId = userRoleId;
            Firstname = firstname;
            Lastname = lastname;
        }

        public int? UserId { get; set; }
        public string Username { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public int UserRoleId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

    }
}
