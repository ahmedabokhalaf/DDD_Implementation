using Azure.Core;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using TestDDD.Controllers.SubscribersController.Request;
using TestDDD.Controllers.SubscribersController.Response;
using TestDDD.OrderAggregate.Interfaces;
using TestDDD.SubscriberAggregate;
using TestDDD.SubscriberAggregate.Input;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestDDD.Controllers.SubscribersController
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubscribersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/<SubscribersController>
        [HttpGet("GetAllSubscribers")]
        public async Task<IActionResult> GetAllSubscribers()
        {
            var subscribers = await _unitOfWork.SubscribersRepository.GetAllAsync();
            if (!subscribers.Any()) return NotFound();
            return Ok(subscribers.Adapt<IEnumerable<SubscriberResponse>>());
        }

        // GET api/<SubscribersController>/5
        [HttpGet("GetSubscriberById")]
        public async Task<IActionResult> GetSubscriberById(Guid id)
        {
            var subscriber = await _unitOfWork.SubscribersRepository.GetByIdAsync(id);
            if (subscriber is null) return NotFound();
            return Ok(subscriber.Adapt<SubscriberResponse>());
        }

        // POST api/<SubscribersController>
        [HttpPost("CreateSubscriber")]
        public async Task<IActionResult> CreateSubscriber([FromBody] CreateSubscriberRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                var subscriberInput = request.Adapt<SubscriberInput>();
                var subscriber = new Subscriber(subscriberInput);
                await _unitOfWork.SubscribersRepository.CreateAsync(subscriber);
                await _unitOfWork.SaveChangesAsync();
                return Ok(subscriber.Adapt<SubscriberResponse>());
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<SubscribersController>/5
        [HttpPut("UpdateSubscriber")]
        public async Task<IActionResult> UpdateSubscriber([FromBody] UpdateSubscriberRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                var subscriber = await _unitOfWork.SubscribersRepository.GetByIdAsync(request.Id);
                if (subscriber is null) return NotFound();
                var subscriberInput = request.Adapt<SubscriberInput>();
                await subscriber.UpdateSubscriber(subscriberInput);
                await _unitOfWork.SubscribersRepository.UpdateAsync(subscriber);
                await _unitOfWork.SaveChangesAsync();
                return Ok(subscriber.Adapt<SubscriberResponse>());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<SubscribersController>/5
        [HttpDelete("DeleteSubscriber")]
        public async Task<IActionResult> DeleteSubscriber(Guid id)
        {
            var subscriber = await _unitOfWork.SubscribersRepository.GetByIdAsync(id);
            if (subscriber is null) return NotFound();
            subscriber.SoftDelete();
            await _unitOfWork.SaveChangesAsync();
            return Ok(subscriber.Adapt<SubscriberResponse>());
        }
    }
}
