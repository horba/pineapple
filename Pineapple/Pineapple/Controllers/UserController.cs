﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pineapple.DBServices;
using Pineapple.Model;

namespace Pineapple.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        // POST
        [HttpPost("register")]
        public UserModel Post(UserModel data)
        {
            data.Email = data.Email.ToLower();
            data.FirstName = data.FirstName == null ? "" : data.FirstName;
            data.SecondName = data.SecondName == null ? "" : data.SecondName;

            UserModel response = new UserModel();
            UserService user = new UserService();

            response.Nick = user.CheckUserNick(data.Nick);
            response.Email = user.CheckUserEmail(data.Email);
            response.FirstName = user.CheckUserFirstName(data.FirstName);
            response.SecondName = user.CheckUserSecondName(data.SecondName);
            response.Password = user.CheckUserPassword(data.Password);
            response.RPassword = user.CheckUserRPassword(data.Password, data.RPassword);

            if (response.Nick == "true" && response.Email == "true" && response.FirstName == "true" && response.SecondName == "true" &&
                response.Password == "true" && response.RPassword == "true")
            {
                response.Status = user.RegisterUser(data);
            }

            return response;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            UserService modelReader = new UserService();
            List<UserModel> allUsers = modelReader.GetAllUsers();
            List<string> stringUsers = new List<string>();
            foreach (UserModel user in allUsers)
            {
                stringUsers.Add(user.ToStringWithoutPassword());
            }
            return stringUsers;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            UserService modelReader = new UserService();
            UserModel user = modelReader.GetUserById(id);
            return user.ToStringWithoutPassword();
        }
    }
}