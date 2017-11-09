using System;

namespace UserStorageServices
{
    /// <summary>
    /// Represents a user.
    /// </summary>
    [Serializable]
    public class User
    {
        /// <summary>
        /// Gets a user id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a user first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets a user last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets a user age.
        /// </summary>
        public int Age { get; set; }

        public static bool operator ==(User lhs, User rhs)
        {
            if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
            {
                return false;
            }

            if (ReferenceEquals(lhs, rhs))
            {
                return true;
            }

            return lhs.Age == rhs.Age
                   && lhs.FirstName == rhs.FirstName
                   && lhs.LastName == rhs.LastName
                   && lhs.Id == rhs.Id;
        }

        public static bool operator !=(User lhs, User rhs)
        {
            return !(lhs == rhs);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is User))
            {
                 return false;
            }

            return (User)obj == this;
        }
    }
}
