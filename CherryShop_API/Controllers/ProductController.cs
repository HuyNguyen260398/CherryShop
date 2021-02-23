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
    /// Product API Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly ILoggerService logger;
        private readonly IMapper mapper;

        public ProductController(
            IProductRepository productRepository,
            ILoggerService logger,
            IMapper mapper)
        {
            this.productRepository = productRepository;
            this.logger = logger;
            this.mapper = mapper;
        }

        /// <summary>
        /// Get all Products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProducts()
        {
            var location = GetControllerActionName();
            try
            {
                logger.LogInfo($"{location}: Get all Products");
                var products = await productRepository.GetAll();
                var response = mapper.Map<IList<ProductDTO>>(products);
                logger.LogInfo($"{location}: Get all Products successful");
                return Ok(response);
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Get Product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProduct(int id)
        {
            var location = GetControllerActionName();
            try
            {
                logger.LogInfo($"{location}: Get Product with id {id}");
                var isExists = await productRepository.IsExists(id);
                if (!isExists)
                {
                    logger.LogWarn($"{location}: Product with id {id} not found");
                    return NotFound();
                }
                var product = await productRepository.GetById(id);
                if (product == null)
                {
                    logger.LogWarn($"{location}: Get Product with id {id} failed");
                    return BadRequest();
                }
                var response = mapper.Map<ProductDTO>(product);
                logger.LogInfo($"{location}: Get Product with id {id} successful");
                return Ok(response);
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Create Product
        /// </summary>
        /// <param name="productDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] ProductCreateDTO productDTO)
        {
            var location = GetControllerActionName();
            try
            {
                logger.LogInfo($"{location}: Create Product");
                if (productDTO == null)
                {
                    logger.LogWarn($"{location}: Product object is empty");
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    logger.LogWarn($"{location}: Product object is incomplete");
                    return BadRequest(ModelState);
                }
                var product = mapper.Map<Product>(productDTO);
                var isSuccess = await productRepository.Create(product);
                if (!isSuccess)
                {
                    InternalError($"{location}: Create Product failed");
                }
                logger.LogInfo($"{location}: Create Product successful");
                return Created("Create", new { product });
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Update Product by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] ProductUpdateDTO productDTO)
        {
            var location = GetControllerActionName();
            try
            {
                logger.LogInfo($"{location}: Update Product with id {id}");
                if (id < 1 || productDTO == null || id != productDTO.Id)
                {
                    logger.LogWarn($"{location}: Update Product with id {id} failed with bad data");
                    return BadRequest();
                }
                var isExists = await productRepository.IsExists(id);
                if (!isExists)
                {
                    logger.LogWarn($"{location}: Product with id {id} not found");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    logger.LogWarn($"{location}: Product object is incomplete");
                    return BadRequest(ModelState);
                }
                var product = mapper.Map<Product>(productDTO);
                var isSuccess = await productRepository.Update(product);
                if (!isSuccess)
                {
                    return InternalError($"{location}: Update Product with id {id} failed");
                }
                logger.LogInfo($"{location}: Update Product with id {id} successful");
                return NoContent();
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Delete Product by id
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
                logger.LogInfo($"{location}: Delete Product with id {id}");
                if (id < 1)
                {
                    logger.LogWarn($"{location}: Delete Product with id {id} failed with bad data");
                    return BadRequest();
                }
                var isExists = await productRepository.IsExists(id);
                if (!isExists)
                {
                    logger.LogWarn($"{location}: Product with id {id} not found");
                    return NotFound();
                }
                var product = await productRepository.GetById(id);
                var isSuccess = await productRepository.Delete(product);
                if (!isSuccess)
                {
                    return InternalError($"{location}: Delete Product with id {id} failed");
                }
                logger.LogInfo($"{location}: Delete Product with id {id} successful");
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
