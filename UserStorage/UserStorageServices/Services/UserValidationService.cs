using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorageServices.Interfaces;

namespace UserStorageServices.Services
{
    public class UserValidationService: IUserValidationService
    {
        public void Validate(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrWhiteSpace(user.LastName))
            {
                throw new ArgumentException("LastName is null, empty or whitespace", nameof(user));
            }

            if (string.IsNullOrWhiteSpace(user.FirstName))
            {
                throw new ArgumentException("FirstName is null, empty or whitespace", nameof(user));
            }

            if (user.Age < 0 || user.Age > 150)
            {
                throw new ArgumentException("Age is out of range (0 - 150).", nameof(user));
            }
        }
    }
}
