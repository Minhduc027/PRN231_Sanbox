using DEMO_ASP_.NET_CORE_Web_API.Data;
using Microsoft.AspNetCore.Mvc;
using DEMO_ASP_.NET_CORE_Web_API.Mapper;
using DEMO_ASP_.NET_CORE_Web_API.Dtos;
using Microsoft.EntityFrameworkCore;
using DEMO_ASP_.NET_CORE_Web_API.Interface;
using DEMO_ASP_.NET_CORE_Web_API.Helper;
using Newtonsoft.Json;
using System.Net.WebSockets;
using System.Text;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace DEMO_ASP_.NET_CORE_Web_API.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {

        private readonly IHubContext<StockHub> _hubContext;
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;
        public StockController(IHubContext<StockHub> context, IStockRepository stockRepo, IMapper mapper)
        {
            _stockRepository = stockRepo;
            _hubContext = context;
            _mapper = mapper;
        }
        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var stocks = await _stockRepository.GetAllAsync(query);
            var response = stocks.Select(s => s.ToDto());
            //var response = _mapper.Map<List<StockDto>>(stocks);
            return Ok(response);
        }


        [HttpGet("{id:long}")]
        [Authorize]
        public async Task<IActionResult> GetIdBy([FromRoute] long id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var stocks = await _stockRepository.GetByIdAsync(id);
            if (stocks == null)
            {
                return NotFound("For input Id: " + id);
            }
            return Ok(stocks.ToDto());
        }

        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> NewStock([FromBody] CreateStockDto stockDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var stockModel = stockDto.toEntity();
                await _stockRepository.CreateStockAsync(stockModel);
                return CreatedAtAction(nameof(GetIdBy), new { id = stockModel.Id }, stockModel.ToDto());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("{id:long}")]
        //[Authorize]
        public async Task<IActionResult> UpdateStock([FromRoute] long id, [FromBody] UpdateStockDto updateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var stockModel = await _stockRepository.UpdateStockAsync(id, updateDto);
            if (stockModel == null)
            {
                return NotFound("For input Id: " + id);
            }

            return Ok(stockModel.ToDto());
        }
        [HttpDelete]
        [Route("{id:long}")]
        public async Task<IActionResult> DeleteStock([FromRoute] long id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var stockModel = await _stockRepository.DeleteStockAsync(id);
            if (stockModel == null)
            {
                return NotFound("For input Id: " + id);
            }
            await _hubContext.Clients.All.SendAsync("Deleted stock: ", stockModel);
            return Ok();
        }
        [HttpGet("signalR")]
        public async Task<IActionResult> GetAllSignalR([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var stocks = await _stockRepository.GetAllAsync(query);
            var response = stocks.Select(s => s.ToDto());
            //await _hubContext.Clients.Clients(HttpContext.Connection.Id).SendAsync("response:", response);
            await _hubContext.Clients.All.SendAsync("response:", response);
            return Ok(response);
        }
    }

}

