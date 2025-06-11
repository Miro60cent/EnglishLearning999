using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EnglishLearning.Api.Extensions;
using EnglishLearning.Api.Repositories.Contracts;
using EnglishLearning.Models.Dtos;

namespace EnglishLearning.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            try
            {
                var users = await this.userRepository.GetUsers();

                if (users == null)
                {
                    return NotFound();
                }
                else
                {
                    var userDtos = users.ConvertToDto();

                    return Ok(userDtos);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка получения данных из БД");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            try
            {
                var user = await this.userRepository.GetUser(id);

                if (user == null)
                {
                    return BadRequest();
                }
                else
                {
                    var userDto = user.ConvertToDto();
                    return Ok(userDto);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка получения данных из БД");
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> PostUser([FromBody] UserAddDto userAddDto)
        {
            try
            {
                var newUser = await this.userRepository.AddUser(userAddDto);

                if (newUser == null)
                {
                    return NoContent();
                }

                var newUserDto = newUser.ConvertToDto();

                return Ok(newUserDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult<UserDto>> UpdateUser(int id, UserUpdateDto userUpdateDto)
        {
            try
            {
                var user = await this.userRepository.UpdateUser(id, userUpdateDto);

                if (user == null)
                {
                    return NotFound();
                }

                var userDto = user.ConvertToDto();

                return Ok(userDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<UserDto>> DeleteUser(int id)
        {
            try
            {
                var user = await this.userRepository.DeleteUser(id);

                if (user == null)
                {
                    return NotFound();
                }

                var userDto = user.ConvertToDto();

                return Ok(userDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch("{userId:int}/CloseUserResourceProfile")]
        public async Task<ActionResult<UserDto>> CloseUserResourceProfile(int userId)
        {
            try
            {
                var ResourceProfile = await this.userRepository.CloseUserResourceProfile(userId);

                if (ResourceProfile == null)
                {
                    return NotFound();
                }

                var ResourceProfileDto = ResourceProfile.ConvertToDto();

                return Ok(ResourceProfileDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}