using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TulipHR.API.Entities;
using TulipHR.API.Models;
using TulipHR.API.Services;

namespace TulipHR.API.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IOrgInfoRepository _positionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(
            ILogger<EmployeesController> logger,
            IOrgInfoRepository positionRepository,
            IMapper mapper)
        {
            _logger = logger ??
               throw new ArgumentNullException(nameof(logger));

            _positionRepository = positionRepository ??
                throw new ArgumentNullException(nameof(positionRepository));
            _mapper = mapper ??
               throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/employees
        /// <summary>
        /// Get list of employees
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployees()
        {
            IEnumerable<Employee> positions = await _positionRepository.GetEmployeeAsync();
            return Ok(_mapper.Map<IEnumerable<EmployeeDTO>>(positions));
        }

        // GET: api/employees/5
        /// <summary>
        /// Get employee detail by Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployee(int id)
        {
            Employee? position = await _positionRepository.GetEmployeeByIdAsync(id);
            if (position == null)
            {
                return NotFound();
            }
            
            return Ok(_mapper.Map<EmployeeDTO>(position));
        }

        /// <summary>
        /// Update employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> UpdateEmployee(EmployeeDTO employee)
        {
            Employee? employeeOld = await _positionRepository.GetEmployeeByIdAsync(employee.Id);
            if (employeeOld == null)
            {
                return NotFound();
            }
            Employee? employeeWithSamePos = employee.PositionId.HasValue? await _positionRepository.GetEmployeeAssignedAsync(employee.PositionId.Value) : null;
            if (employeeWithSamePos != null && employee.Id != employeeWithSamePos.Id)
            {
                return BadRequest("The position is already assigned to another person and cannot be reassigned.");
            }
            _mapper.Map(employee, employeeOld);
            await _positionRepository.SaveChangesAsync();
            return NoContent();
        }

    }
}
