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
            this.user = user;
            this.isActive = isActive;
        }

        private User user;
        private bool isActive;

        public User GetUser()
        {
            return this.user;
        } 
        public bool GetIsActive()
        {
            return this.isActive;
        }

        public static Session Instance(User user) 
        {
            if (instance == null || instance.isActive == false)
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
