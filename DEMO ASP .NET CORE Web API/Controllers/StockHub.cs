using DEMO_ASP_.NET_CORE_Web_API.Interface;
using Microsoft.AspNetCore.SignalR;

namespace DEMO_ASP_.NET_CORE_Web_API.Controllers
{
    public class StockHub : Hub
    { private readonly IStockRepository _stockRepository;
        public StockHub(IStockRepository stockRepository) 
        { 
            _stockRepository = stockRepository;
        }
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("Connect received", $"{Context.ConnectionId} established connect to server!");
        }
        public async Task GetAllStock(string message)
        {
            if (message.Equals("GetAllStock"))
            {
                var stocks = await _stockRepository.GetAllAsync(new Helper.QueryObject());
                await Clients.All.SendAsync($"Stock list: {stocks}");
            }
            else
            {
                await Clients.Caller.SendAsync($" message {message} invalid");
            }
        }
    }
}
