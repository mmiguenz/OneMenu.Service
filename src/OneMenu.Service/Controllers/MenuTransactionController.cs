using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneMenu.Core.actions;
using OneMenu.Core.Model;
using OneMenu.Core.Model.Menus;

namespace OneMenu.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MenuTransactionController : ControllerBase
    {
        private readonly ILogger<MenuTransactionController> _logger;
        private readonly GetCurrentStepMenuTransaction _getCurrentStepMenuTransaction;
        private readonly InitMenuTransaction _initMenuTransaction;
        private readonly SaveStepMenuTransaction _saveStepMenuTransaction;

        public MenuTransactionController(ILogger<MenuTransactionController> logger,
            GetCurrentStepMenuTransaction getCurrentStepMenuTransaction,
            InitMenuTransaction initMenuTransaction,
            SaveStepMenuTransaction saveStepMenuTransaction)
        {
            _logger = logger;
            _getCurrentStepMenuTransaction = getCurrentStepMenuTransaction;
            _initMenuTransaction = initMenuTransaction;
            _saveStepMenuTransaction = saveStepMenuTransaction;
        }

        [HttpGet("{menuTransactionId}/step")]
        public async Task<Step> GetCurrentStep(string menuTransactionId)
        {
            try
            {
                _logger.LogTrace("GetCurrentStep({menuTransactionId})", menuTransactionId);
                
                return await _getCurrentStepMenuTransaction.Execute(menuTransactionId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetCurrentStep({menuTransactionId})", menuTransactionId);
                throw;
            }
        }

        [HttpPost("{menuLabel}")]
        public async Task<MenuTransaction> InitMenuTransaction(string menuLabel)
        {
            _logger.LogTrace("InitMenuTransaction({menuLabel})", menuLabel);
            
            try
            {
                return await _initMenuTransaction.Execute(menuLabel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "InitMenuTransaction({menuLabel})", menuLabel);
                throw;
            }
        }
        
        [HttpPost("{menuTransactionId}/response")]
        public async Task<SaveStepResult> SaveStepResponse(string menuTransactionId, [FromBody] StepResponse stepResponse)
        {
            _logger.LogTrace("SaveStepResponse({menuTransactionId}, {response})", menuTransactionId, stepResponse.Response);
            try
            {
                return await _saveStepMenuTransaction.Execute(menuTransactionId, stepResponse.Response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SaveStepResponse({menuTransactionId}, {response})", menuTransactionId, stepResponse.Response);
                throw;
            }
        }
    }

    public class StepResponse
    {
        public string Response { get; set; }
    }
}