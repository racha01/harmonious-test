using Domain.EmployeeWage.Models;
using Domain.EmployeeWage.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using Service.EmployeeWage.API.DTOs;

namespace Service.EmployeeWage.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WageController : BaseController
    {
        private readonly ILogicUnitOfWork _logicUnitOfWork;
        public WageController(ILogicUnitOfWork logicUnitOfWork)
        {
            _logicUnitOfWork = logicUnitOfWork;
        }

        [HttpGet(Name = "GetWageAsync")]
        public async Task<IActionResult> GetWageAsync()
        {
            var wageDatas = await _logicUnitOfWork.WageService.GetWageAsync();

            var wageDataDTO = wageDatas.Select(s => new WageDTO()
            {
                Id = s.Id,
                Position = s.Position,
                Day = s.Day,
                Wagerate = s.Wagerate,
            }).ToList<WageDTO>();

            return APIResponse(wageDataDTO);
        }

        [HttpGet("{id}", Name = "GetWageByIdAsync")]
        public async Task<IActionResult> GetWageByIdAsync(string id)
        {
            var wageDoc = await _logicUnitOfWork.WageService.GetWageByIdAsync(int.Parse(id));

            var wageDataDTO = new WageDTO()
            {
                Id = wageDoc.Id,
                Position = wageDoc.Position,
                Day = wageDoc.Day,
                Wagerate = wageDoc.Wagerate,
            };

            return APIResponse(wageDataDTO);
        }

        [HttpPost(Name = "CreateWageAsync")]
        public async Task<IActionResult> CreateWageAsync([FromBody] CreateWageDTO data)
        {
            var createWageDoc = new CreateWageModel()
            {
                Position = data.Position,
                Day = data.Day,
                Wagerate = data.Wagerate,
            };
            var dataId = await _logicUnitOfWork.WageService.CreateWageAsync(createWageDoc);

            var responseCreateDataDTO = new { Id = dataId };

            return APIResponse(responseCreateDataDTO);
        }

        [HttpPut("{id}", Name = "UpdateWageAsync")]
        public async Task<IActionResult> UpdateWageAsync([FromBody] UpdateWageDTO data, string id)
        {
            var updateWageDoc = new UpdateWageModel()
            {
                Position = data.Position,
                Day = data.Day,
                Wagerate = data.Wagerate,
            };
            await _logicUnitOfWork.WageService.UpdateWageAsync(updateWageDoc, int.Parse(id));

            return APIResponse();
        }

        [HttpDelete("{id}", Name = "DeleteWageAsync")]
        public async Task<IActionResult> DeleteWageAsync(string id)
        {
            await _logicUnitOfWork.WageService.DeleteWageAsync(int.Parse(id));

            return APIResponse();
        }
    }
}
