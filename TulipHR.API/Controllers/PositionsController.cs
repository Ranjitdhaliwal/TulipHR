using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TulipHR.API.Entities;
using TulipHR.API.Models;
using TulipHR.API.Services;

namespace TulipHR.API.Controllers
{
    [Route("api/positions")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private readonly IOrgInfoRepository _positionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PositionsController> _logger;
        public PositionsController(
            ILogger<PositionsController> logger,
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

        // GET: api/positions
        /// <summary>
        /// Get list of positions with employee detail
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PositionDto>>> GetPositions()
        {
            IEnumerable<PositionDto> positions = await GetAllPositionsWithEmpInfo();
            return Ok(positions);
        }

        // GET: api/positions/5
        /// <summary>
        /// Get position by Id
        /// </summary>
        /// <param name="id">The id of the position to get</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetPosition")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PositionDto>> GetPosition(int id)
        {
            var position = await GetAllPositionsWithEmpInfo(id);

            if (position == null || position.Count() == 0)
            {
                return NotFound();
            }

            return Ok(position);
        }

        // DELETE: api/positions/5
        /// <summary>
        /// Delete position by Id
        /// </summary>
        /// <param name="id">The id of the position to be deleted</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeletePosition(int id)
        {
            var position = await _positionRepository.GetPositionAsync(id);

            if (position == null)
            {
                return NotFound();
            }
            else
            {
                if (position.ManagerPositionId == null)
                {
                    return BadRequest("Cannot delete top level position.");
                }
            }

            var employee = await _positionRepository.GetEmployeeAssignedAsync(id);
            if (employee != null)
            {
                return BadRequest("Cannot delete a position with an assigned employee.");
            }
            var posName = position.Title;
            _positionRepository.DeletePositions(position);
            await _positionRepository.SaveChangesAsync();

            var msg = $"Position '{posName}' has been deleted successfully.";
            _logger.LogInformation(msg);

            return Ok(msg);
        }

        //POST: api/positions
        /// <summary>
        /// Create new position
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Position>> PostPosition(PositionCreationDto position)
        {
            try
            {

                var newPos = _mapper.Map<Entities.Position>(position);
                _positionRepository.AddPosition(newPos);
                await _positionRepository.SaveChangesAsync();

                return CreatedAtRoute(nameof(GetPosition),
                     new { id = newPos.Id },
                     newPos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception while creating new position : {position.Title}");
                return StatusCode(500, "A problem happened while handling your request");
            }
        }

        // GET: api/positions/hierarchy
        /// <summary>
        /// Get position hierarhcy
        /// </summary>
        /// <returns></returns>
        [HttpGet("hierarchy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<OrgTreeNode<PositionDto>>> GetHierarchyAsync()
        {
            IEnumerable<PositionDto> positions = await GetAllPositionsWithEmpInfo();
            return Ok(GeneratePositionTree(positions.ToList()));
        }

        private async Task<IEnumerable<PositionDto>> GetAllPositionsWithEmpInfo(int? posId = null)
        {
            var posQry = _positionRepository.GetQueryablePositions();
            if (posId.HasValue)
                posQry = posQry.Where(p => p.Id == posId.Value);

            return await (from pos in posQry
                          join emp in _positionRepository.GetQueryableEmployees()
                          on pos.Id equals emp.PositionId
                          into empPosGroup
                          from ep in empPosGroup.DefaultIfEmpty()
                          select new PositionDto()
                          {
                              Id = pos.Id,
                              Title = pos.Title,
                              Number = pos.Number,
                              ManagerPositionId = pos.ManagerPositionId,
                              Employee = _mapper.Map<EmployeeDTO>(ep)
                          }).ToListAsync();

        }
        private static OrgTreeNode<PositionDto> GeneratePositionTree(List<PositionDto> positions, int? managerPositionId = null)
        {
            OrgTreeNode<PositionDto> root = new OrgTreeNode<PositionDto>(null);

            foreach (var position in positions.Where(p => p.ManagerPositionId == managerPositionId))
            {
                var node = new OrgTreeNode<PositionDto>(position);
                if (managerPositionId == null)
                {
                    root = node;
                }
                else
                {
                    root.Children.Add(node);
                }
                var children = GeneratePositionTree(positions, position.Id);
                if (children.Children.Any())
                {
                    node.Children.AddRange(children.Children);
                }
            }
            return root;
        }


    }
}
