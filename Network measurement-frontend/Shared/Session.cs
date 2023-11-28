using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network_measurement_frontend.Shared
{
    public class Session
    {
        private static Session instance;
        private Session(User user, bool isActive)
        {
            User = user;
            IsActive = isActive;
        }

        public User User { get; }
        public bool IsActive { get; }

        public static Session Instance(User user) 
        {
            if (instance == null || instance.IsActive == false)
            {
                instance = new Session(user, true);
            }
            return instance;
        }

        public static void LogOut()
        {
            instance = new Session(null, false);
        }
    }
}
