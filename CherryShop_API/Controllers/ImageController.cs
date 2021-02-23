using AutoMapper;
using CherryShop_API.Contracts;
using CherryShop_API.Data;
using CherryShop_API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CherryShop_API.Controllers
{
    /// <summary>
    /// Image API Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository imageRepository;
        private readonly ILoggerService logger;
        private readonly IMapper mapper;

        public ImageController(
            IImageRepository imageRepository,
            ILoggerService logger,
            IMapper mapper)
        {
            this.imageRepository = imageRepository;
            this.logger = logger;
            this.mapper = mapper;
        }

        /// <summary>
        /// Get all Images
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetImages()
        {
            var location = GetControllerActionName();
            try
            {
                logger.LogInfo($"{location}: Get all Images");
                var images = await imageRepository.GetAll();
                var response = mapper.Map<IList<ImageDTO>>(images);
                logger.LogInfo($"{location}: Get all Images successful");
                return Ok(response);
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Get Image by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetImage(int id)
        {
            var location = GetControllerActionName();
            try
            {
                logger.LogInfo($"{location}: Get Image with id {id}");
                var isExists = await imageRepository.IsExists(id);
                if (!isExists)
                {
                    logger.LogWarn($"{location}: Image with id {id} not found");
                    return NotFound();
                }
                var image = await imageRepository.GetById(id);
                if (image == null)
                {
                    logger.LogWarn($"{location}: Get Image with id {id} failed");
                    return BadRequest();
                }
                var response = mapper.Map<ImageDTO>(image);
                logger.LogInfo($"{location}: Get Image with id {id} successful");
                return Ok(response);
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Create Image
        /// </summary>
        /// <param name="imageDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] ImageCreateDTO imageDTO)
        {
            var location = GetControllerActionName();
            try
            {
                logger.LogInfo($"{location}: Create Image");
                if (imageDTO == null)
                {
                    logger.LogWarn($"{location}: Image object is empty");
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    logger.LogWarn($"{location}: Image object is incomplete");
                    return BadRequest(ModelState);
                }
                var image = mapper.Map<Image>(imageDTO);
                var isSuccess = await imageRepository.Create(image);
                if (!isSuccess)
                {
                    InternalError($"{location}: Create Image failed");
                }
                logger.LogInfo($"{location}: Create Image successful");
                return Created("Create", new { image });
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Update Image by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="imageDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] ImageUpdateDTO imageDTO)
        {
            var location = GetControllerActionName();
            try
            {
                logger.LogInfo($"{location}: Update Image with id {id}");
                if (id < 1 || imageDTO == null || id != imageDTO.Id)
                {
                    logger.LogWarn($"{location}: Update Image with id {id} failed with bad data");
                    return BadRequest();
                }
                var isExists = await imageRepository.IsExists(id);
                if (!isExists)
                {
                    logger.LogWarn($"{location}: Image with id {id} not found");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    logger.LogWarn($"{location}: Image object is incomplete");
                    return BadRequest(ModelState);
                }
                var image = mapper.Map<Image>(imageDTO);
                var isSuccess = await imageRepository.Update(image);
                if (!isSuccess)
                {
                    return InternalError($"{location}: Update Image with id {id} failed");
                }
                logger.LogInfo($"{location}: Update Image with id {id} successful");
                return NoContent();
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Delete Image by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            var location = GetControllerActionName();
            try
            {
                logger.LogInfo($"{location}: Delete Image with id {id}");
                if (id < 1)
                {
                    logger.LogWarn($"{location}: Delete Image with id {id} failed with bad data");
                    return BadRequest();
                }
                var isExists = await imageRepository.IsExists(id);
                if (!isExists)
                {
                    logger.LogWarn($"{location}: Image with id {id} not found");
                    return NotFound();
                }
                var image = await imageRepository.GetById(id);
                var isSuccess = await imageRepository.Delete(image);
                if (!isSuccess)
                {
                    return InternalError($"{location}: Delete Image with id {id} failed");
                }
                logger.LogInfo($"{location}: Delete Image with id {id} successful");
                return NoContent();
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        private string GetControllerActionName()
        {
            var controller = ControllerContext.ActionDescriptor.ControllerName;
            var action = ControllerContext.ActionDescriptor.ActionName;

            return $"{controller} - {action}";
        }

        private ObjectResult InternalError(string message)
        {
            logger.LogError(message);
            return StatusCode(500, "Internal Server Error. Please contact the Admin.");
        }
    }
}
