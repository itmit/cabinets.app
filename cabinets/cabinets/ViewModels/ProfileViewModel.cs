using cabinets.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace cabinets.ViewModels
{
    public class ProfileViewModel
    {
        private List<CabinetModel> _bookings;
        private string _fullName;
        private string _phone;
        private string _email;

        public ProfileViewModel()
        {
            Bookings = new List<CabinetModel>
            {
                new CabinetModel
                {
                    Number = 1,
                    Date = "01.01.2020"
                },
                new CabinetModel
                {
                    Number = 2,
                    Date = "02.01.2020"
                },
                new CabinetModel
                {
                    Number = 3,
                    Date = "03.01.2020"
                }
            };

            var user = new UserModel();
            FullName = user.FullName;
            Phone = user.Phone;
            Email = user.Email;
        }

        public List<CabinetModel> Bookings 
        { 
            get => _bookings; 
            set => _bookings = value; 
        }

        public string FullName 
        { 
            get => _fullName; 
            set => _fullName = value; 
        }

        public string Phone 
        { 
            get => _phone; 
            set => _phone = value; 
        }

        public string Email 
        { 
            get => _email; 
            set => _email = value; 
        }
    }
}
