using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        public UserEntity Register(UserRegistrationModel userRegistration);
        public string Login(UserLoginModel userLogin);
        public string ForgetPassword(string Email);
        public bool ResetLink(string Email, string Password, string confirmPassword);


    }

}
