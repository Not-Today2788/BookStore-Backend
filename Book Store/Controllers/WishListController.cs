using CommonLayer.ResponseModel;
using ManagerLayer.Interfaces;
using ManagerLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Book_Store.Controllers
{
    [Route("api/[controller]")]
    public class WishListController:ControllerBase
    {
        private readonly IWishListManager iwishlistManager;

        public WishListController(IWishListManager iwishlistManager)
        {
            this.iwishlistManager = iwishlistManager;
        }

        [Authorize]
        [HttpPost]
        [Route("AddToWishList")]
        public ActionResult AddtoWishList(int bookId)
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
                var response = iwishlistManager.AddtoWishList(userId, bookId);
                if (response != null)
                {
                    return Ok(new ResModel<WishListEntity> { Success = true, Message = "Book Added to Wishlist!", Data = response });
                }
                return BadRequest(new ResModel<WishListEntity> { Success = false, Message = "Book not Added to Wishlist", Data = null });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<bool> { Success = false, Message = ex.Message, Data = false });
            }
        }
        [Authorize]
        [HttpDelete]
        [Route("RemoveBookFromWishlist")]
        public ActionResult RemoveBookFromWishlist(int wishlistId)
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
                var response = iwishlistManager.RemoveFromWishList(userId, wishlistId);
                if (response != null)
                {
                    return Ok(new ResModel<WishListEntity> { Success = true, Message = "Book removed from Wishlist!", Data = response });
                }
                return BadRequest(new ResModel<WishListEntity> { Success = false, Message = "Something went wrong", Data = null });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<bool> { Success = false, Message = ex.Message, Data = false });
            }
        }
        [Authorize]
        [HttpGet]
        [Route("GetAllBookWishlist")]
        public ActionResult GetAllBookFromWishlist()
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
                var response = iwishlistManager.GetWishList(userId);
                if (response != null)
                {
                    return Ok(new ResModel<List<WishListEntity>> { Success = true, Message = "Display items present in wishlist Successfully!", Data = response });
                }
                return BadRequest(new ResModel<List<WishListEntity>> { Success = false, Message = "Something went wrong", Data = null });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<bool> { Success = false, Message = ex.Message, Data = false });
            }
        }
    }
}
