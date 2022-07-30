using HandyMan.Dtos;
using HandyMan.Interfaces;
using HandyMan.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HandyMan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration config;
        private readonly IHandymanRepository handymanRepository;
        private readonly IClientRepository clientRepository;
        public LoginController(IConfiguration _config, IHandymanRepository _handymanRepository, IClientRepository _clientRepository)
        {
            config = _config;
            handymanRepository = _handymanRepository;
            clientRepository = _clientRepository;
        }

        public record AuthenticationData(string? UserName, string? Password, string? Role);
        public record UserData(int Id, string UserName, string Role);

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Authenticate([FromBody] AuthenticationData data)
        {
            try
            {
                var user = await ValidateCredentials(data);
                if (user == null)
                    return Unauthorized();
                var token = GenerateToken(user);
                return Ok(new { token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        private string GenerateToken(UserData user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
                config.GetValue<string>("Authentication:SecretKey")));


            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);


            List<Claim> claims = new();
            claims.Add(new(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
            claims.Add(new(JwtRegisteredClaimNames.UniqueName, user.UserName.ToString()));
            claims.Add(new("Role", user.Role));



            var token = new JwtSecurityToken(
                config.GetValue<string>("Authentication:Issuer"),
                config.GetValue<string>("Authentication:Audience"),
                claims,
                DateTime.Now,
                DateTime.Now.AddDays(1),
                signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<UserData>? ValidateCredentials(AuthenticationData data)
        {
            /*if (Compare(data.UserName, "shiho") && Compare(data.Password, "123abc"))
            {
                return new UserData(1, data.UserName!, "Client");
            }

            else if (Compare(data.UserName, "Ahmed") && Compare(data.Password, "123abc"))
            {
                return new UserData(1, data.UserName!, "Handyman");
            }*/
            ////////////////////////////////////////////////////////////

            //not for production
            if (Compare( data.Role , "Handyman"))
            {
                Handyman handyman = await handymanRepository.GetHandymanByIdAsync(int.Parse(data.UserName));
                if (handyman == null)
                    return null;
                if (Compare(data.UserName, handyman.Handyman_SSN.ToString()) && Compare(data.Password, handyman.Password))
                {
                    return new UserData(handyman.Handyman_SSN, handyman.Handyman_Name, "Handyman");
                }
                else return null;

            }

            else if (Compare(data.Role , "Client"))
            {
                Client client = await clientRepository.GetClientByEmail(data.UserName);
                
                if (client == null)
                    return null;
                if (Compare(data.UserName, client.Client_Email) && Compare(data.Password, client.Password))
                {
                    return new UserData(client.Client_ID, client.Client_name, "Client");
                }
                else return null;

            }
            else if (Compare(data.Role, "Admin"))
            {
                if (Compare(data.UserName, "admin") && Compare(data.Password, "admin"))
                {
                    return new UserData(1, data.UserName!, "Admin");
                }
            }



            return null;


        }

        private bool Compare(string? actual, string expected)
        {
            if (actual != null)
            {
                if (actual.Equals(expected))
                    return true;
            }
            return false;
        }

    }
}