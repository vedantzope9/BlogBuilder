using BlogBuilder.BusinessLayer.Interfaces;
using BlogBuilder.DTOs;
using BlogBuilder.Models;
using BlogBuilder.RepositoryLayer.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq.Expressions;

namespace BlogBuilder.BusinessLayer.Business
{
    public class BlUserServices:IUserServices
    {
        private readonly IUserRepo _userRepo;
        private readonly JwtHelper _jwtHelper;

        public BlUserServices(IUserRepo userRepo , JwtHelper jwtHelper)
        {
            _userRepo = userRepo;
            _jwtHelper = jwtHelper;
        }

        public string? GetUsernameByUserId(int userId)
        {
            try
            {
                var entity= _userRepo.FindUserById(userId);
                if (entity != null)
                    return entity.USERNAME;
                return null;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public JsonResult LoginUser(string email, string password)
        {

            var entity = _userRepo.FindUserByEmail(email);

            if (entity!=null && BCrypt.Net.BCrypt.EnhancedVerify(password, entity.PASSWORD))
            {
                var token = _jwtHelper.GenerateToken(email);
                return new JsonResult(new {
                    success = true ,
                    token = token,
                    userid = entity.USERID,
                    email = entity.EMAIL,
                    username = entity.USERNAME,
                    message = "Login successful!" 
                });
            }

            return new JsonResult(new { success = false, message = "Login Failed!" });
        }

        public JsonResult RegisterUser(UserDTO dto)
        {
            string hashedPassword = HashPassword(dto.PASSWORD);
            try
            {
                if (isEmailRepetitive(dto.EMAIL))
                {
                    return new JsonResult(new { success = false, message = "Registration failed! This Email is already used.  Try with different Email." });
                }

                BLOG_USER user = new BLOG_USER()
                {
                    USERNAME = dto.USERNAME,
                    PASSWORD = hashedPassword,
                    EMAIL = dto.EMAIL
                };

                bool isRegistered = _userRepo.RegisterUser(user);


                if (isRegistered)
                {
                    return new JsonResult(new { success = true });
                }

                return new JsonResult(new { success = false, message = "Registration failed!" });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password , 12);
        }
        
        private bool isEmailRepetitive(string email)
        {
            try
            {
                return _userRepo.FindUserByEmail(email) != null;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Dictionary<int,string> GetAllUsernames()
        {
            try
            {
                var users = _userRepo.GetAllUsers();
                var userMap = users.ToDictionary(u => u.USERID, u => u.USERNAME);
                return userMap;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
