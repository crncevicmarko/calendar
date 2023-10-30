using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Model
{
    public class Data
    {
        private static Data instance;
        public User LoggedInUser { get; set; }

        static Data() { }

        private Data()
        {
            LoggedInUser = new User();
        }

        public static Data Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Data();
                }

                return instance;
            }

            private set => instance = value;
        }

    }
}
