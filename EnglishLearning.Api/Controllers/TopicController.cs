using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EnglishLearning.Api.Extensions;
using EnglishLearning.Api.Repositories.Contracts;
using EnglishLearning.Models.Dtos;

namespace EnglishLearning.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly ITopicRepository TopicRepository;

        public TopicController(ITopicRepository TopicRepository)
        {
            this.TopicRepository = TopicRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TopicDto>>> GetTopics()
        {
            try
            {
                var Topics = await this.TopicRepository.GetTopics();

                if (Topics == null)
                {
                    return NotFound();
                }
                else
                {
                    var TopicDtos = Topics.ConvertToDto();

                    return Ok(TopicDtos);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка получения данных из БД");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TopicDto>> GetTopic(int id)
        {
            try
            {
                var Topic = await this.TopicRepository.GetTopic(id);

                if (Topic == null)
                {
                    return BadRequest();
                }
                else
                {
                    var TopicDto = Topic.ConvertToDto();
                    return Ok(TopicDto);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка получения данных из БД");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TopicDto>> PostTopic([FromBody] TopicAddDto TopicAddDto)
        {
            try
            {
                var newTopic = await this.TopicRepository.AddTopic(TopicAddDto);

                if (newTopic == null)
                {
                    return NoContent();
                }

                var newTopicDto = newTopic.ConvertToDto();

                return Ok(newTopicDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult<TopicDto>> UpdateTopic(int id, TopicAddDto TopicAddDto)
        {
            try
            {
                var Topic = await this.TopicRepository.UpdateTopic(id, TopicAddDto);

                if (Topic == null)
                {
                    return NotFound();
                }

                var TopicDto = Topic.ConvertToDto();

                return Ok(TopicDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<TopicDto>> DeleteTopic(int id)
        {
            try
            {
                var Topic = await this.TopicRepository.DeleteTopic(id);

                if (Topic == null)
                {
                    return NotFound();
                }

                var TopicDto = Topic.ConvertToDto();

                return Ok(TopicDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}