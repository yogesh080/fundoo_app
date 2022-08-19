using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;

namespace RepositoryLayer.Service
{
    public class UserRL:IUserRL
    {
        private readonly FundooContext fundooContext;
        public UserRL(FundooContext fundooContext)
        {
            this.fundooContext=fundooContext;
        }
        public UserEntity Register(UserRegistrationModel userRegistration)
        {
            try
            {
                UserEntity user = new UserEntity();
                user.FirstName = userRegistration.FirstName;
                user.LastName = userRegistration.LastName;
                user.Email = userRegistration.Email;
                user.Password = userRegistration.Password;

                fundooContext.UserTable.Add(user);
                int res = fundooContext.SaveChanges();
                if (res > 0)
                {
                    return user;
                }
                else
                {
                    return null;
                }

            

            }
            catch (Exception )
            {

                throw;
            }

        }
    }
}