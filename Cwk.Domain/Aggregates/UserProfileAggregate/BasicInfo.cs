﻿using Cwk.Domain.Exceptions;
using Cwk.Domain.Validators.UserProfileValidators;


namespace Cwk.Domain.Aggregates.UserProfileAggregate
{
    public class BasicInfo
    {

        private BasicInfo()
        {

        }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string EmailAddress { get; private set;  }  
        public string Phone { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string CurrentCity { get; private set; }

        /// <summary>
        /// Creates a new BasicInfo instance
        /// </summary>
        /// <param name="firstName">First Name</param>
        /// <param name="lastName">Last Name</param>
        /// <param name="eMail">Email Address</param>
        /// <param name="phone">Phone number</param>
        /// <param name="dateOfBirth">Date of Birth</param>
        /// <param name="currentCity">Current City</param>
        /// <returns><see cref="BasicInfo"/></returns>
        /// <exception cref="UserProfileNotValidException"></exception>
        public static BasicInfo CreateBasicInfo(string firstName, string lastName, string eMail, string phone,
            DateTime dateOfBirth, string currentCity)
        {
            var validator = new BasicInfoValidator();

            var objToValidate= new BasicInfo
            {
                FirstName = firstName,
                LastName = lastName,
                EmailAddress = eMail,
                Phone = phone,
                DateOfBirth = dateOfBirth,
                CurrentCity = currentCity
            };
            var validationResult = validator.Validate(objToValidate);
            if (validationResult.IsValid) return objToValidate;
            var exception = new UserProfileNotValidException("The user profile is not valid");
            foreach (var error in validationResult.Errors)
            {
                exception.ValidationErrors.Add(error.ErrorMessage);
            }
            throw exception;
        }
    }
}
