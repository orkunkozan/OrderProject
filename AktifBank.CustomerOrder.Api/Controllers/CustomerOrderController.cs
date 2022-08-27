using AktifBank.CustomerOrder.Business.Services.Abstract;
using AktifBank.CustomerOrder.Business.Services.CustomerOrderService.Models.Request;
using AktifBank.CustomerOrder.Business.Services.CustomerOrderService.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AktifBank.CustomerOrder.Api.Controllers
{
    public class CustomerOrderController : Controller
    {
        private readonly ICustomerOrderService _customerOrderService;
        public CustomerOrderController(ICustomerOrderService customerOrderService)
        {
            _customerOrderService = customerOrderService;
        }

        /// <summary>
        /// You can list all of the customer's orders
        /// </summary>
        /// <param name="req"></param>
        /// <returns>all of the customer's orders</returns>
        [AllowAnonymous]
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetCustomerOrdersRes>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getCustomerOrders")]
        public async Task<IActionResult> GetCustomerOrdersAsync([FromQuery] GetCustomerOrdersReq req)
        {
            var result = await _customerOrderService.GetCustomerOrdersAsync(req);
            return Ok(result);
        }

        /// <summary>
        /// Gives detailed information about the order
        /// </summary>
        /// <param name="req"></param>
        /// <returns>order detail</returns>
        [AllowAnonymous]
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOrderRes))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getOrder")]
        public async Task<IActionResult> GetOrderAsync([FromQuery] GetOrderReq req)
        {
            var result = await _customerOrderService.GetOrderAsync(req);
            return Ok(result);
        }

        /// <summary>
        /// You can register a new order
        /// </summary>
        /// <param name="req"></param>
        /// <returns>Ok</returns>
        [AllowAnonymous]
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost("createNewOrder")]
        public async Task<IActionResult> CreateNewOrderAsync([FromBody] CreateNewOrderReq req)
        {
            await _customerOrderService.CreateNewOrderAsync(req);
            return Ok();
        }

        /// <summary>
        /// You can add, remove or update an existing order.
        /// </summary>
        /// <param name="req"></param>
        /// <returns>Ok</returns>
        [AllowAnonymous]
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost("updateOrder")]
        public async Task<IActionResult> UpdateOrderAsync([FromBody] UpdateOrderReq req)
        {
            await _customerOrderService.UpdateOrderAsync(req);
            return Ok();
        }

        /// <summary>
        /// You can change the delivery address of an existing order
        /// </summary>
        /// <param name="req"></param>
        /// <returns>Ok</returns>
        [AllowAnonymous]
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost("updateOrderAddress")]
        public async Task<IActionResult> UpdateOrderAddressAsync([FromBody] UpdateOrderAddressReq req)
        {
            await _customerOrderService.UpdateOrderAddressAsync(req);
            return Ok();
        }

        /// <summary>
        ///  You can delete the order record and related detail information.
        /// </summary>
        /// <param name="req"></param>
        /// <returns>Ok</returns>
        [AllowAnonymous]
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete("deleteOrder")]
        public async Task<IActionResult> DeleteOrderAsync([FromQuery] DeleteOrderReq req)
        {
            await _customerOrderService.DeleteOrderAsync(req);
            return Ok();
        }

        /// <summary>
        /// You can delete a order item.
        /// </summary>
        /// <param name="req"></param>
        /// <returns>Ok</returns>
        [AllowAnonymous]
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete("deleteOrderItem")]
        public async Task<IActionResult> DeleteOrderItemAsync([FromQuery] DeleteOrderItemReq req)
        {
            await _customerOrderService.DeleteOrderItemAsync(req);
            return Ok();
        }
    }
}
