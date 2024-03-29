﻿using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        public UserEntity Register(UserRegistrationModel userRegistration);
        
        public string Login(UserLoginModel userLogin);

        public string ForgetPassword(string Email);

        public bool ResetLink(string Email, string password, string confirmPassword);




    }
}
