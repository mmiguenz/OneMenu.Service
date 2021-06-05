using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneMenu.Core.actions;
using OneMenu.Core.Model;

namespace OneMenu.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly ILogger<MenuController> _logger;
        private readonly ListMenus _listMenuAction;

        public MenuController(ILogger<MenuController> logger, ListMenus listMenuAction)
        {
            _logger = logger;
            _listMenuAction = listMenuAction;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<Menu>> GetAll()
        {
            _logger.LogTrace("GetAll()");
            try
            {
                return await _listMenuAction.Execute();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAll()");
                throw;
            }
        }
    }
}