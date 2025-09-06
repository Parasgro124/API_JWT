using API_JWT.Data;
using API_JWT.Model;
using API_JWT.Model.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace API_JWT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private ApplicationDBContext _dbContext;
        private readonly IMapper _mapper;

        public UserController(ILogger<UserController> logger, ApplicationDBContext dbContext, IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                _logger.LogInformation("Fetching all users started" + DateTime.UtcNow);
                List<User> users = await _dbContext.Users.ToListAsync();
                List<UserDTO> userDTOs = _mapper.Map<List<UserDTO>>(users);
                _logger.LogInformation("Fetching all users completed" + DateTime.UtcNow);
                return Ok(userDTOs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving users" + DateTime.UtcNow);
                return StatusCode(500, "Internal server error while fetching All Users");
            }
        }
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                _logger.LogInformation("Fetching all users started" + DateTime.UtcNow);
                if (id != 0)
                {
                    var user = await _dbContext.Users.FindAsync(id);
                    var user_dto = _mapper.Map<UserDTO>(user);
                    if (user_dto != null)
                    {
                        return Ok(user_dto);
                    }
                    return NotFound("User not found");
                }
                return BadRequest("Invalid Id.");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving users" + DateTime.UtcNow);
                return StatusCode(500, "Internal server error while fetching All Users");
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateUserDTO user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Creating user started" + DateTime.UtcNow);
                    User userDTOs = _mapper.Map<User>(user);
                    await _dbContext.Users.AddAsync(userDTOs);
                    await _dbContext.SaveChangesAsync();
                    _logger.LogInformation("Created User" + DateTime.UtcNow);
                    return StatusCode(201, $"Created User {user.username}");
                }
                return BadRequest("Invalid data.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Creating User" + DateTime.UtcNow);
                return StatusCode(500, "Internal server error while Creating User");
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUserById(int id, [FromBody] CreateUserDTO updatedUser)
        {
            try
            {
                _logger.LogInformation("Updating users started" + DateTime.UtcNow);
                if (id != 0)
                {
                    var user = await _dbContext.Users.FindAsync(id);
                    if (user == null)
                    {
                        return NotFound("User not found");
                    }
                    user.username = updatedUser.username;
                    user.password = updatedUser.password;
                    user.email = updatedUser.email;
                    _dbContext.Users.Update(user);
                    _dbContext.SaveChanges();
                    return Ok("User updated successfully");
                }
                return BadRequest("Invalid Id.");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving users" + DateTime.UtcNow);
                return StatusCode(500, "Internal server error while fetching All Users");
            }
        }

        [HttpDelete(Name = "DeleteUser")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            try
            {
                if (id != 0)
                {
                    _logger.LogInformation("Deleting user started" + DateTime.UtcNow);
                    var userById = await _dbContext.Users.FindAsync(id);
                    if (userById == null)
                    {
                        return NotFound("User not found");
                    }
                    _dbContext.Users.Remove(userById);
                    await _dbContext.SaveChangesAsync();
                    _logger.LogInformation("Delete User" + DateTime.UtcNow);
                    return NoContent();
                }
                return BadRequest("Invalid data.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Deleting User" + DateTime.UtcNow);
                return StatusCode(500, "Internal server error while Deleting User");
            }
        }
    }
}