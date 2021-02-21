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
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public class BrandController : ControllerBase
    {
        private readonly IBrandRepository brandRepository;
        private readonly ILoggerService logger;
        private readonly IMapper mapper;

        public BrandController(
            IBrandRepository brandRepository,
            ILoggerService logger,
            IMapper mapper)
        {
            this.brandRepository = brandRepository;
            this.logger = logger;
            this.mapper = mapper;
        }

        /// <summary>
        /// Get all Brands
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBrands()
        {
            var location = GetControllerActionName();
            try
            {
                logger.LogInfo($"{location}: Get all Brands");
                var brands = await brandRepository.GetAll();
                var response = mapper.Map<IList<BrandDTO>>(brands);
                logger.LogInfo($"{location}: Get all Brands successful");
                return Ok(response);
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Get Brand by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBrand(int id)
        {
            var location = GetControllerActionName();
            try
            {
                logger.LogInfo($"{location}: Get Brand with id {id}");
                var isExists = await brandRepository.IsExists(id);
                if (!isExists)
                {
                    logger.LogWarn($"{location}: Brand with id {id} not found");
                    return NotFound();
                }
                var brand = await brandRepository.GetById(id);
                if (brand == null)
                {
                    logger.LogWarn($"{location}: Get Brand with id {id} failed");
                    return BadRequest();
                }
                var response = mapper.Map<BrandDTO>(brand);
                logger.LogInfo($"{location}: Get Brand with id {id} successful");
                return Ok(response);
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Create Brand
        /// </summary>
        /// <param name="brandDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] BrandCreateDTO brandDTO)
        {
            var location = GetControllerActionName();
            try
            {
                logger.LogInfo($"{location}: Create Brand");
                if (brandDTO == null)
                {
                    logger.LogWarn($"{location}: Brand object is empty");
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    logger.LogWarn($"{location}: Brand object is incomplete");
                    return BadRequest(ModelState);
                }
                var brand = mapper.Map<Brand>(brandDTO);
                var isSuccess = await brandRepository.Create(brand);
                if (!isSuccess)
                {
                    InternalError($"{location}: Create Brand failed");
                }
                logger.LogInfo($"{location}: Create Brand successful");
                return Created("Create", new { brand });
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Update Brand by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="brandDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult>Update(int id, [FromBody] BrandUpdateDTO brandDTO)
        {
            var location = GetControllerActionName();
            try
            {
                logger.LogInfo($"{location}: Update Brand with id {id}");
                if (id < 1 || brandDTO == null || id != brandDTO.Id)
                {
                    logger.LogWarn($"{location}: Update Brand with id {id} failed with bad data");
                    return BadRequest();
                }
                var isExists = await brandRepository.IsExists(id);
                if (!isExists)
                {
                    logger.LogWarn($"{location}: Brand with id {id} not found");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    logger.LogWarn($"{location}: Brand object is incomplete");
                    return BadRequest(ModelState);
                }
                var brand = mapper.Map<Brand>(brandDTO);
                var isSuccess = await brandRepository.Update(brand);
                if (!isSuccess)
                {
                    return InternalError($"{location}: Update Brand with id {id} failed");
                }
                logger.LogInfo($"{location}: Update Brand with id {id} successful");
                return NoContent();
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Delete Brand by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult>Delete(int id)
        {
            var location = GetControllerActionName();
            try
            {
                logger.LogInfo($"{location}: Delete Brand with id {id}");
                if (id < 1)
                {
                    logger.LogWarn($"{location}: Delete Brand with id {id} failed with bad data");
                    return BadRequest();
                }
                var isExists = await brandRepository.IsExists(id);
                if (!isExists)
                {
                    logger.LogWarn($"{location}: Brand with id {id} not found");
                    return NotFound();
                }
                var brand = await brandRepository.GetById(id);
                var isSuccess = await brandRepository.Delete(brand);
                if (!isSuccess)
                {
                    return InternalError($"{location}: Delete Brand with id {id} failed");
                }
                logger.LogInfo($"{location}: Delete Brand with id {id} successful");
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
