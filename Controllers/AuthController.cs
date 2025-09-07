using API_JWT.Data;
using API_JWT.Model;
using API_JWT.Model.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API_JWT.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        public readonly TokenHelper _tokenHelper;
        public readonly ApplicationDBContext _dbcontext;
        public readonly IMapper _mapper;
        public AuthController(TokenHelper tokenHelper, ApplicationDBContext dbcotext, IMapper mapper)
        {
            _tokenHelper = tokenHelper;
            _dbcontext = dbcotext;
            _mapper = mapper;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] CreateUserDTO userDetails)
        {
            try
            {
                User ifUser  = _dbcontext.Users.FirstOrDefault(u => u.username == userDetails.username && u.email == userDetails.email && u.password == userDetails.password)?? new Model.User();
                
                if (ifUser.isActive==1 && ifUser.username!=null)
                {
                    var token= _tokenHelper.CreateToken(ifUser.username, ifUser.role??"");
                    return Ok(token);
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest("Login Exception"+ ex.Message);
            }
        }
    }
}
