using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EnglishLearning.Api.Extensions;
using EnglishLearning.Api.Repositories.Contracts;
using EnglishLearning.Models.Dtos;

namespace EnglishLearning.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileActivitieController : ControllerBase
    {
        private readonly IUserProfileActivitieRepository UserProfileActivitieRepository;
        private readonly IResourceRepository ResourceRepository;

        public UserProfileActivitieController(IUserProfileActivitieRepository UserProfileActivitieRepository, IResourceRepository ResourceRepository)
        {
            this.UserProfileActivitieRepository = UserProfileActivitieRepository;
            this.ResourceRepository = ResourceRepository;
        }

        [HttpGet]
        [Route("GetUserProfileActivities")]
        public async Task<ActionResult<IEnumerable<UserProfileActivitieDto>>> GetUserProfileActivities(int userId)
        {
            try
            {
                var UserProfileActivities = await this.UserProfileActivitieRepository.GetUserProfileActivities();

                if (UserProfileActivities == null)
                {
                    return NoContent();
                }

                var Resources = await this.ResourceRepository.GetResources();

                if (Resources == null)
                {
                    throw new Exception("В системе нет товаров");
                }

                var UserProfileActivitiesDto = UserProfileActivities.ConvertToDto();

                return Ok(UserProfileActivitiesDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("{userId}/GetItems")]
        public async Task<ActionResult<IEnumerable<UserProfileActivitie_ItemDto>>> GetItems(int userId)
        {
            try
            {
                var UserProfileActivitie_Items = await this.UserProfileActivitieRepository.GetItems(userId);

                if (UserProfileActivitie_Items == null)
                {
                    return NoContent();
                }

                var Resources = await this.ResourceRepository.GetResources();

                if (Resources == null)
                {
                    throw new Exception("В системе нет товаров");
                }

                var UserProfileActivitie_ItemsDto = UserProfileActivitie_Items.ConvertToDto(Resources);

                return Ok(UserProfileActivitie_ItemsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserProfileActivitie_ItemDto>> GetItem(int id)
        {
            try
            {
                var UserProfileActivitie_Item = await this.UserProfileActivitieRepository.GetItem(id);

                if (UserProfileActivitie_Item == null)
                {
                    return NotFound();
                }

                var Resource = await ResourceRepository.GetResource(UserProfileActivitie_Item.ResourceId);

                if (Resource == null)
                {
                    return NotFound();
                }

                var UserProfileActivitie_ItemDto = UserProfileActivitie_Item.ConvertToDto(Resource);

                return Ok(UserProfileActivitie_ItemDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserProfileActivitie_ItemDto>> PostItem([FromBody] UserProfileActivitie_ItemToAddDto UserProfileActivitie_ItemToAddDto)
        {
            try
            {
                var newUserProfileActivitie_Item = await this.UserProfileActivitieRepository.AddItem(UserProfileActivitie_ItemToAddDto);

                if (newUserProfileActivitie_Item == null)
                {
                    return NoContent();
                }

                var Resource = await ResourceRepository.GetResource(newUserProfileActivitie_Item.ResourceId);

                if (Resource == null)
                {
                    throw new Exception($"Не получилось прочитать информацию о наушниках (ResourceId:({UserProfileActivitie_ItemToAddDto.ResourceId})");
                }

                var newUserProfileActivitie_ItemDto = newUserProfileActivitie_Item.ConvertToDto(Resource);

                return CreatedAtAction(nameof(GetItem), new { id = newUserProfileActivitie_ItemDto.Id }, newUserProfileActivitie_ItemDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult<UserProfileActivitie_ItemDto>> UpdateQuantity(int id, UserProfileActivitie_ItemQuantityUpdateDto UserProfileActivitie_ItemQuantityUpdateDto)
        {
            try
            {
                var UserProfileActivitie_Item = await this.UserProfileActivitieRepository.UpdateQuantity(id, UserProfileActivitie_ItemQuantityUpdateDto);

                if (UserProfileActivitie_Item == null)
                {
                    return NotFound();
                }

                var Resource = await ResourceRepository.GetResource(UserProfileActivitie_Item.ResourceId);
                var UserProfileActivitie_ItemDto = UserProfileActivitie_Item.ConvertToDto(Resource);

                return Ok(UserProfileActivitie_ItemDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<UserProfileActivitie_ItemDto>> DeleteItem(int id)
        {
            try
            {
                var UserProfileActivitie_Item = await this.UserProfileActivitieRepository.DeleteItem(id);

                if (UserProfileActivitie_Item == null)
                {
                    return NotFound();
                }

                var Resource = await this.ResourceRepository.GetResource(UserProfileActivitie_Item.ResourceId);

                if (Resource == null) return NotFound();

                var UserProfileActivitie_ItemDto = UserProfileActivitie_Item.ConvertToDto(Resource);

                return Ok(UserProfileActivitie_ItemDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}