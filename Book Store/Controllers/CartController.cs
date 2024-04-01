using CommonLayer.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using ManagerLayer.Interfaces;

namespace Book_Store.Controllers
{
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartManager icartManager;
        public CartController(ICartManager icartManager)
        {
            this.icartManager = icartManager;
        }

        [Authorize]
        [HttpPost]
        [Route("AddToCart")]
        public async Task<IActionResult> AddToCart(int BookId)
        {
            try
            {
                var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
                var response = await icartManager.AddToCart(userId, BookId);
                if (response == null)
                {
                    return BadRequest(new ResModel<CartEntity> { Success = false, Message = "Not able to add", Data = null });
                }
                return Ok(new ResModel<CartEntity> { Success = true, Message = "successfully added to the cart", Data = response });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<CartEntity> { Success = false, Message = ex.Message, Data = null });
            }
        }
        [Authorize]
        [HttpDelete]
        [Route("remove")]
        public async Task<IActionResult> RemoveFromCart(int BookId)
        {
            try
            {
                var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
                var response = await icartManager.RemovefromCart(userId, BookId);
                if (response == false)
                {
                    return BadRequest(new ResModel<bool> { Success = false, Message = "Not able to remove", Data = false });
                }
                return Ok(new ResModel<bool> { Success = true, Message = "successfully removed to the cart", Data = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<bool> { Success = false, Message = ex.Message, Data = false });
            }
        }

        [Authorize]
        [HttpPut]
        [Route("changeQuantity")]
        public async Task<IActionResult> ChangeQuantiy(int BookId, bool increase)
        {
            try
            {
                var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
                var response = await icartManager.Increase_Decrease(userId, BookId, increase);
                if (response == false)
                {
                    return BadRequest(new ResModel<bool> { Success = false, Message = "Not able to change quantity", Data = false });
                }
                return Ok(new ResModel<bool> { Success = true, Message = "successfully changed quantity to the cart", Data = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<bool> { Success = false, Message = ex.Message, Data = false });
            }
        }

        [Authorize]
        [HttpGet]
        [Route("getAllItems")]
        public async Task<IActionResult> GetAllItems()
        {
            try
            {
                var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
                var response = await icartManager.GetAllItems(userId);
                if (response == null)
                {
                    return BadRequest(new ResModel<List<CartEntity>> { Success = false, Message = "Not able to display the items of cart", Data = null });
                }
                return Ok(new ResModel<List<CartEntity>> { Success = true, Message = "successfully displayed list of items of cart", Data = response });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<List<CartEntity>> { Success = false, Message = ex.Message, Data = null });
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetTotalPrice")]
        public async Task<IActionResult> GetTotalPrice()
        {
            try
            {
                var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
                var response = await icartManager.TotalCost(userId);
                return Ok(new ResModel<float> { Success = true, Message = "successfully displayed subtoatal price of items of cart", Data = response });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<int> { Success = false, Message = ex.Message, Data = 0 });
            }
        }

        [Authorize]
        [HttpPut]
        [Route("purchase")]
        public async Task<IActionResult> PurchaseItems(bool paymentdone)
        {
            try
            {
                var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
                var response = await icartManager.PurchaseItems(userId, paymentdone);
                return Ok(new ResModel<bool> { Success = true, Message = "Order placed successfully", Data = response });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<bool> { Success = false, Message = ex.Message, Data = false });
            }
        }
    }
}
