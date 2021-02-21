using AutoMapper;
using CherryShop_API.Contracts;
using CherryShop_API.Data;
using CherryShop_API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CherryShop_API.Controllers
{
    /// <summary>
    /// Category API Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly ILoggerService logger;
        private readonly IMapper mapper;

        public CategoryController(
            ICategoryRepository categoryRepository,
            ILoggerService logger,
            IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.logger = logger;
            this.mapper = mapper;
        }

        /// <summary>
        /// Get all Categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCategories()
        {
            var location = GetControllerActionName();
            try
            {
                logger.LogInfo($"{location}: Get all Categories");
                var categories = await categoryRepository.GetAll();
                var response = mapper.Map<IList<CategoryDTO>>(categories);
                logger.LogInfo($"{location}: Get all Categories successful");
                return Ok(response);
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Get Category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCategory(int id)
        {
            var location = GetControllerActionName();
            try
            {
                logger.LogInfo($"{location}: Get Category with id {id}");
                var isExists = await categoryRepository.IsExists(id);
                if (!isExists)
                {
                    logger.LogWarn($"{location}: Category with id {id} not found");
                    return NotFound();
                }
                var category = await categoryRepository.GetById(id);
                if (category == null)
                {
                    logger.LogWarn($"{location}: Get Category with id {id} failed");
                    return BadRequest();
                }
                var response = mapper.Map<CategoryDTO>(category);
                logger.LogInfo($"{location}: Get Category with id {id} successful");
                return Ok(response);
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Create Category
        /// </summary>
        /// <param name="categoryDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDTO categoryDTO)
        {
            var location = GetControllerActionName();
            try
            {
                logger.LogInfo($"{location}: Create Category");
                if (categoryDTO == null)
                {
                    logger.LogWarn($"{location}: Category object is empty");
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    logger.LogWarn($"{location}: Category object is incomplete");
                    return BadRequest(ModelState);
                }
                var category = mapper.Map<Category>(categoryDTO);
                var isSuccess = await categoryRepository.Create(category);
                if (!isSuccess)
                {
                    InternalError($"{location}: Create Category failed");
                }
                logger.LogInfo($"{location}: Create Category successful");
                return Created("Create", new { category });
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Update Category by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categoryDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult>Update(int id, [FromBody] CategoryUpdateDTO categoryDTO)
        {
            var location = GetControllerActionName();
            try
            {
                logger.LogInfo($"{location}: Update Category with id {id}");
                if (id < 1 || categoryDTO == null || id != categoryDTO.Id)
                {
                    logger.LogWarn($"{location}: Update Category with id {id} failed with bad data");
                    return BadRequest();
                }
                var isExists = await categoryRepository.IsExists(id);
                if (!isExists)
                {
                    logger.LogWarn($"{location}: Category with id {id} not found");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    logger.LogWarn($"{location}: Category object is incomplete");
                    return BadRequest(ModelState);
                }
                var category = mapper.Map<Category>(categoryDTO);
                var isSuccess = await categoryRepository.Update(category);
                if (!isSuccess)
                {
                    return InternalError($"{location}: Update Category with id {id} failed");
                }
                logger.LogInfo($"{location}: Update Category with id {id} successful");
                return NoContent();
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Delete Category by id
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
                logger.LogInfo($"{location}: Delete Category with id {id}");
                if (id < 1)
                {
                    logger.LogWarn($"{location}: Delete Category with id {id} failed with bad data");
                    return BadRequest();
                }
                var isExists = await categoryRepository.IsExists(id);
                if (!isExists)
                {
                    logger.LogWarn($"{location}: Category with id {id} not found");
                    return NotFound();
                }
                var category = await categoryRepository.GetById(id);
                var isSuccess = await categoryRepository.Delete(category);
                if (!isSuccess)
                {
                    return InternalError($"{location}: Delete Category with id {id} failed");
                }
                logger.LogInfo($"{location}: Delete Category with id {id} successful");
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
