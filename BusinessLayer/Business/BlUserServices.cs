using BlogBuilder.BusinessLayer.Interfaces;
using BlogBuilder.DTOs;
using BlogBuilder.Models;
using BlogBuilder.RepositoryLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BlogBuilder.BusinessLayer.Business
{
    public class BlUserServices:IUserServices
    {
        private readonly IUserRepo _userRepo;

        public BlUserServices(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public JsonResult RegisterUser(UserDTO dto)
        {
            string password=BCrypt.Net.BCrypt.EnhancedHashPassword(dto.PASSWORD, 12);
            try
            {
                if (isEmailRepetitive(dto.EMAIL))
                {
                    return new JsonResult(new { success = false, message = "Registration failed! This Email is already used.  Try with different Email." });
                }

                BLOG_USER user = new BLOG_USER()
                {
                    USERNAME = dto.USERNAME,
                    PASSWORD = password,
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
        
        private bool isEmailRepetitive(string email)
        {
            try
            {
                return _userRepo.isEmailRepetitive(email);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
