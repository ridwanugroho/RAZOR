using System;
using System.Text;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

using belajarRazor.Models;
using belajarRazor.Data;

namespace belajarRazor.Controllers
{
    
    public class UserController : Controller
    {
        public AppDbContex appDbContex{get; set;}
        private IConfiguration configuration;

        public UserController(AppDbContex appDbContex, IConfiguration configuration)
        {
            this.appDbContex = appDbContex;
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            return View("Login");
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetUser()
        {
            return Ok(getUser());
        }

        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] User user)
        {
            var existUser = from _user in appDbContex.User select _user.username;

            if(existUser.Contains(user.username))
            {
                return Ok(new
                {
                    ERROR = "Username Exist!"
                });
            }

            appDbContex.User.Add(user);
            appDbContex.SaveChanges();

            return Ok(user);
        }

        [HttpGet("info/alluser")]
        public IActionResult UserInfo()
        {
            return null;
        }

        private User getUser()
        {
            var token = System.IO.File.ReadAllText("token.txt");
            var jwtSecrTokenHandler = new JwtSecurityTokenHandler();
            var secrToken = jwtSecrTokenHandler.ReadToken(token) as JwtSecurityToken;

            var userId = secrToken?.Claims.First(claim => claim.Type == "sub").Value;
            var user= from usr in appDbContex.User where usr.id == Convert.ToInt32(userId) select usr;
            
            return user.First();
        }

        /////////////////////////////// LOGIN LOGOUT  //////////////////////////////
        
        [HttpPost]
        public IActionResult Login(IFormCollection user_inp)
        {
            var _user = new User()
            {
                email = user_inp["email"],
                password = user_inp["password"]
            };

            Console.WriteLine(_user.email);
            Console.WriteLine(_user.password);

            var user = AuthenticatedUser(_user);

            if(user == null)
            {
                ViewBag.info = "Username/ Password salah :(";
                return View("Login");
            }

            var token = generateJwtToken(user);
            var fToken = new StreamWriter("token.txt");
            fToken.Write(token);
            fToken.Close();
            HttpContext.Session.SetString("JWToken", token);

            return RedirectToAction("Index", "Product");
        }

        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }

        public User AuthenticatedUser(User user_input)
        {
            var user = from _user in appDbContex.User where _user.email == user_input.email select _user;

            if(user.FirstOrDefault() != null)
            {
                if(user.First().password == user_input.password)
                    return user.First();
            }

            return null;
        }

        private string generateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                // issuer: Configuration["Jwt:Issuer"],
                // audience: Configuration["Jwt:Audience"],
                null,
                null,
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );

            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

            return encodedToken;
        }
    
    }
}