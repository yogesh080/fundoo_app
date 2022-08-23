using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL:IUserRL
    {
        private readonly IConfiguration _AppSetting;

        private readonly FundooContext fundooContext;

        public object UserId { get; private set; }

        public UserRL(FundooContext fundooContext, IConfiguration _AppSetting)
        {
            this.fundooContext=fundooContext;
            this._AppSetting = _AppSetting;
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
        public string Login(UserLoginModel userLogin)
        {
            try
            {
                var LoginDetails = fundooContext.UserTable.Where(x => x.Email == userLogin.Email && x.Password == userLogin.Password).FirstOrDefault();
                if (LoginDetails != null)
                {
                    var token = GenerateSecurityToken(LoginDetails.Email, LoginDetails.UserId);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public string GenerateSecurityToken(string email, long UserId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this._AppSetting[("JWT:key")]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("UserId", UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }

        public string ForgetPassword(string Email)
        {
            try
            {
                var EmailCheck = fundooContext.UserTable.FirstOrDefault(x => x.Email == Email);
                if(EmailCheck != null)
                {
                    string token = GenerateSecurityToken(EmailCheck.Email, EmailCheck.UserId);
                    MSMQModel msm = new MSMQModel();
                    msm.sendData2Queue(token);
                    return "MAIL SEND";
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool ResetLink(string Email, string Password, string confirmPassword)
        {
            try
            {
                if (Password.Equals(confirmPassword))
                {
                    var EmailCheck = fundooContext.UserTable.FirstOrDefault(x => x.Email == Email);
                    EmailCheck.Password = Password;

                    fundooContext.SaveChanges();
                    return true;

                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}