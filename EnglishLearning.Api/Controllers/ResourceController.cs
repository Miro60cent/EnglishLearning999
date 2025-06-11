using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EnglishLearning.Api.Extensions;
using EnglishLearning.Api.Repositories.Contracts;
using EnglishLearning.Models.Dtos;

namespace EnglishLearning.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly IResourceRepository ResourceRepository;
        //private readonly ITopicRepository TopicRepository;

        public ResourceController(IResourceRepository ResourceRepository/*, ITopicRepository TopicRepository*/)
        {
            this.ResourceRepository = ResourceRepository;
            //this.TopicRepository = TopicRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResourceDto>>> GetResources()
        {
            try
            {
                var Resources = await this.ResourceRepository.GetResources();

                if (Resources == null)
                {
                    return NotFound();
                }
                else
                {
                    var ResourceProfileDtos = Resources.ConvertToDto();

                    return Ok(ResourceProfileDtos);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка получения данных из БД");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ResourceDto>> GetResource(int id)
        {
            try
            {
                var Resource = await this.ResourceRepository.GetResource(id);

                if (Resource == null)
                {
                    return BadRequest();
                }
                else
                {
                    var ResourceProfileDto = Resource.ConvertToDto();
                    return Ok(ResourceProfileDto);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка получения данных из БД");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResourceDto>> PostResource([FromBody] ResourceAddDto ResourceAddDto)
        {
            try
            {
                var newResource = await this.ResourceRepository.AddResource(ResourceAddDto);

                if (newResource == null)
                {
                    return NoContent();
                }

                var Topic = await ResourceRepository.GetTopic(newResource.Topic_id);

                if (Topic == null)
                {
                    throw new Exception($"Не получилось прочитать информацию о наушниках (TopicId:({ResourceAddDto.Topic_id})");
                }

                var newResourceDto = newResource.ConvertToDto(Topic);

                return Ok(newResourceDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult<ResourceDto>> UpdateResource(int id, ResourceAddDto ResourceAddDto)
        {
            try
            {
                var Resource = await this.ResourceRepository.UpdateResource(id, ResourceAddDto);

                if (Resource == null)
                {
                    return NotFound();
                }

                var Topic = await ResourceRepository.GetTopic(Resource.Topic_id);

                if (Topic == null)
                {
                    throw new Exception($"Не получилось прочитать информацию о наушниках (TopicId:({ResourceAddDto.Topic_id})");
                }

                var ResourceDto = Resource.ConvertToDto(Topic);

                return Ok(ResourceDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ResourceDto>> DeleteResource(int id)
        {
            try
            {
                var Resource = await this.ResourceRepository.DeleteResource(id);

                if (Resource == null)
                {
                    return NotFound();
                }

                var Topic = await ResourceRepository.GetTopic(Resource.Topic_id);

                if (Topic == null)
                {
                    throw new Exception($"Не получилось прочитать информацию о наушниках (TopicId:({Resource.Topic_id})");
                }

                var ResourceDto = Resource.ConvertToDto(Topic);

                return Ok(ResourceDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route(nameof(GetTopics))]
        public async Task<ActionResult<IEnumerable<TopicDto>>> GetTopics()
        {
            try
            {
                var ResourceTopics = await ResourceRepository.GetTopics();
                var ResourceTopicDtos = ResourceTopics.ConvertToDto();

                return Ok(ResourceTopicDtos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка получения данных из БД");
            }
        }

        [HttpGet]
        [Route("GetResourcesByTopic/{TopicId}")]
        public async Task<ActionResult<IEnumerable<ResourceDto>>> GetResourcesByTopic(int TopicId)
        {
            try
            {
                var Resources = await ResourceRepository.GetResourceByTopic(TopicId);
                var ResourceDtos = Resources.ConvertToDto();

                return Ok(ResourceDtos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка получения данных из БД");
            }
        }
    }
}