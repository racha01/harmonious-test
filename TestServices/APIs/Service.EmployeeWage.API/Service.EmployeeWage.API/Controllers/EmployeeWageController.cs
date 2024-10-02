using Domain.EmployeeWage.Models;
using Domain.EmployeeWage.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using Service.EmployeeWage.API.DTOs;

namespace Service.EmployeeWage.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeWageController: BaseController
    {
        private readonly ILogicUnitOfWork _logicUnitOfWork;
        public EmployeeWageController(ILogicUnitOfWork logicUnitOfWork)
        {
            _logicUnitOfWork = logicUnitOfWork;
        }

        [HttpGet(Name = "GetEmployeeWage")]
        public async Task<IActionResult> GetEmployeeWageAsync()
        {
            var wageDatas = await _logicUnitOfWork.EmployeeWageService.GetEmployeeWageAsync();

            var employeeWageDataDTO = wageDatas.Select(s => new EmployeeWageDTO()
            {
                Id = s.Id,
                Name = s.Name,
                Position = s.Position,
                Day = s.Day,
                Hour = s.Hour
            }).ToList<EmployeeWageDTO>();

            return APIResponse(employeeWageDataDTO);
        }

        [HttpGet("WageOfEachEmployee")]
        public async Task<IActionResult> GetWageOfEachEmployeeAsync()
        {
            var wageDatas = await _logicUnitOfWork.EmployeeWageService.GetWageOfEachEmployeeAsync();

            var employeeWageDataDTO = wageDatas.Select(s => new WageOfEachEmployeeDTO()
            {
                Name = s.Name,
                WageTotal = s.WageTotal,
            }).ToList<WageOfEachEmployeeDTO>();

            return APIResponse(employeeWageDataDTO);
        }

        [HttpGet("{id}", Name = "GetEmployeeWageById")]
        public async Task<IActionResult> GetEmployeeWageByIdAsync(string id)
        {
            var wageDoc = await _logicUnitOfWork.EmployeeWageService.GetEmployeeWageByIdAsync(int.Parse(id));

            var employeeWageDTO = new EmployeeWageDTO()
            {
                Id = wageDoc.Id,
                Name = wageDoc.Name,
                Position = wageDoc.Position,
                Day = wageDoc.Day,  
                Hour = wageDoc.Hour,    
            };

            return APIResponse(employeeWageDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployeeWageAsync([FromBody] CreateEmployeeWageDTO data)
        {
            var createEmployeeWageDoc = new CreateEmployeeWageModel()
            {
                Name = data.Name,
                Position = data.Position,
                Day = data.Day,
                Hour = data.Hour,
            };
            var dataId = await _logicUnitOfWork.EmployeeWageService.CreateEmployeeWageAsync(createEmployeeWageDoc);

            var responseCreateDataDTO = new { Id = dataId };

            return APIResponse(responseCreateDataDTO);
        }

        [HttpPut("{id}", Name = "UpdateEmployeeWage")]
        public async Task<IActionResult> UpdateEmployeeWageAsync([FromBody] UpdateEmployeeWageDTO data, string id)
        {
            var createEmployeeWageDoc = new UpdateEmployeeWageModel()
            {
                Name = data.Name,
                Position = data.Position,
                Day = data.Day,
                Hour = data.Hour,
            };
            await _logicUnitOfWork.EmployeeWageService.UpdateEmployeeWageAsync(createEmployeeWageDoc, int.Parse(id));

            return APIResponse();
        }

        [HttpDelete("{id}", Name = "UpdateEmployeeWage")]
        public async Task<IActionResult> DeleteloyeeWageAsync(string id)
        {
            await _logicUnitOfWork.EmployeeWageService.DeleteEmployeeWageAsync(int.Parse(id));

            return APIResponse();
        }
    }
}
