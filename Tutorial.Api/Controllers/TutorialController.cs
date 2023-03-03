using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using Tutorial.Api.Models;
using Tutorial.Api.Services;

namespace Tutorial.Api.Controllers
{
    [Route("api/tutorials")]
    [ApiController]
    public class TutorialController : ControllerBase
    {
        #region Property
        private readonly ITutorialService _tutorialService;
        private readonly ILogger _logger;
        #endregion

        #region Constructor
        public TutorialController(ILogger<TutorialController> logger,ITutorialService tutorialService)
        {
            _tutorialService = tutorialService;
            _logger = logger;
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery(Name = "title")] string? title)
        {
            if (title == null)
            {
                try{
                    var _result = await _tutorialService.GetAsync();

                    _logger.LogWarning("Get all Tutorial :",_result);
                    
                    return Ok(_result);
                }catch(Exception e){
                     _logger.LogError("Get all tutotrial error : ",e.Message);
                }
            }

            try{
                var result = await _tutorialService.GetAyncByTitle(title);
                _logger.LogDebug("Get all tutorial :",result);
                return Ok(result);

            }catch(Exception e){
                _logger.LogError("Get all error : ",e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> GetTutorialById(string id)
        { 
            try{
                var result = await _tutorialService.GetAsync(id);
                _logger.LogDebug("GetTutorialById : ",result);
                return Ok(result);
            }catch(Exception e){
                _logger.LogError("GetTutorialById ERROR : ",e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddTutorial([FromBody] Models.Tutorial tutorial)
        {
            try{
                await _tutorialService.CreateAsync(tutorial);
                _logger.LogDebug("AddTutorial : Done");
                return Ok(new ReturnMessage { Code="200" , Message = "Inserted a single document Success"});
            }catch(Exception e){
                _logger.LogError("GetTutorialById ERROR : ",e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateTutorial(string id,[FromBody] Models.Tutorial tutorial)
        {
            try{
                var _tutorial = await _tutorialService.GetAsync(id);
                
                if (_tutorial is null)
                {
                    _logger.LogDebug("UpdateTutorial tutorial : NULL");
                    return NotFound();
                }

                tutorial.Id = _tutorial.Id;

                await _tutorialService.UpdateAsync(id, tutorial);
    
                _logger.LogDebug("UpdateTutorial tutorial : Done");
                return Ok(new ReturnMessage { Code = "200", Message = "Updated a single document Success" });
            }catch(Exception e){
                _logger.LogError("UpdateTutorial ERROR : ",e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }            
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            try{
                await _tutorialService.RemoveAsync();
                _logger.LogDebug("DeleteAll tutorial : Done");
                return Ok(new ReturnMessage { Code="200", Message="All deleted" });
            }catch(Exception e){
                _logger.LogError("DeleteAll ERROR : ",e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteById(string id)
        {
            try{
                var tutorial = await _tutorialService.GetAsync(id);

                if (tutorial is null)
                {
                    _logger.LogDebug("DeleteById tutorial : NULL");
                    return NotFound();
                }

                await _tutorialService.RemoveAsync(id);
                _logger.LogDebug("DeleteById tutorial :",id);
                return Ok(new ReturnMessage{ Code = "200", Message = "Deleted id "+id });
            }catch(Exception e){
                _logger.LogError("DeleteAll ERROR : ",e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}