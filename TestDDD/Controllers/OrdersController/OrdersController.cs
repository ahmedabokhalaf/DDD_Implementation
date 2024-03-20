using Mapster;
using Microsoft.AspNetCore.Mvc;
using TestDDD.OrderAggregate;
using TestDDD.OrderAggregate.Input;
using TestDDD.OrderAggregate.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestDDD.Controllers.OrdersController
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrdersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        // GET: api/<OrdersController>
        [HttpGet("GetAllOrders")]
        public async Task<List<OrderResult>> GetAllOrders()
        {
            var orders = await _unitOfWork.OrderRepository.GetOrdersAsync();
            if (!orders.Any())
                return null;
            return orders.Adapt<List<OrderResult>>();
        }

        // GET api/<OrdersController>/5
        [HttpGet("GetOrderById")]
        public async Task<OrderResult> GetOrderById(Guid id)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
            if (order == null)
                return null;
            return order.Adapt<OrderResult>();
        }

        // POST api/<OrdersController>
        [HttpPost("CreateOrder")]
        public async Task<OrderResult> CreateOrder(CreateOrderRequest model)
        {
            if (!ModelState.IsValid)
                return null;
            var orderInput = model.Adapt<OrderInput>();
            var order = new Order(orderInput);
            await order.CalculateTotalPrice(orderInput);
            await _unitOfWork.OrderRepository.CreateAsync(order);
            await _unitOfWork.SaveChangesAsync();
            return order.Adapt<OrderResult>();
        }

        // PUT api/<OrdersController>/5
        [HttpPut("UpdateOrder")]
        public async Task<OrderResult> UpdateOrder(UpdateOrderRequest model)
        {
            if (!ModelState.IsValid)
                return model.Adapt<OrderResult>();
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(model.Id);
            if (order == null)
                return null;
            var updateOrderInput = model.Adapt<UpdateOrderInput>();

            foreach (var item in model.OrderItems)
            {
                var orderItem = item.Adapt<UpdateOrderItemInput>();
                updateOrderInput.OrderItemInputs.Add(orderItem);
            }
            order = updateOrderInput.Adapt<Order>();
            await order.UpdateCalculatedTotalPrice(updateOrderInput);
            await _unitOfWork.OrderRepository.UpdateAsync(order);
            await _unitOfWork.SaveChangesAsync();
            return order.Adapt<OrderResult>();
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("DeleteOrder")]
        public async Task<OrderResult> DeleteOrder(Guid id)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
            if (order == null)
                return null;
            await _unitOfWork.OrderRepository.DeleteAsync(order);
            await _unitOfWork.SaveChangesAsync();
            return order.Adapt<OrderResult>();
        }
    }
}
