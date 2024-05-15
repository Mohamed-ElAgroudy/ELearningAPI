using ELearningAPI.Data;
using ELearningAPI.DTOS;
using ELearningAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ELearningAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private ElearningReusableContext _context;
        public ShoppingCartController(ElearningReusableContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserShoppingCartItem>>> GetShoppingCart(string userEmail)
        {
            return await _context.UserShoppingCartItems.Where(item=>item.UserEmail==userEmail).ToListAsync();
        }

        [HttpPost("addcoursetocart")]
        public async Task<IActionResult> AddCourseToCart(ShoppingCartItemDto item)
        {
            try
            {
                var existingItem = await _context.UserShoppingCartItems
                    .FirstOrDefaultAsync(u => u.CourseId == item.CourseId && u.UserEmail == item.UserEmail);

                if (existingItem != null)
                {
                    return Conflict("item already exists");
                }
                var itemtba = new UserShoppingCartItem
                {
                    CourseId = item.CourseId,
                    UserEmail = item.UserEmail,
                    Quantity = item.Quantity,
                };
                _context.UserShoppingCartItems.Add(itemtba);
                await _context.SaveChangesAsync();
                return Ok("Course added to cart successfully");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
        [HttpPut("EditCartItem")]
        public async Task<ActionResult> EditCartItemQuantity(string userEmail,int courseid, int quantity)
        {
            try
            {
                var existingItem = await _context.UserShoppingCartItems
                    .FirstOrDefaultAsync(u => u.CourseId == courseid && u.UserEmail == userEmail);

                if (existingItem != null)
                {
                    existingItem.Quantity = quantity;  
                    await _context.SaveChangesAsync();
                    return Ok("Item Updated Successfully");  
                }
                return Conflict("item already exists");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpDelete("RemoveShoppingCartItem")]
        public async Task<ActionResult<string>> removeshoppingcartItem(string email, int courseid)
        {
            try
            {
                var itemtbd = await _context.UserShoppingCartItems.FirstOrDefaultAsync(u => u.UserEmail == email && u.CourseId == courseid);

                if (itemtbd != null)
                {
                    _context.UserShoppingCartItems.Remove(itemtbd);
                    await _context.SaveChangesAsync();
                    return Ok("Item Deleted from Cart");
                }
                else
                {
                    return Conflict("Error");
                }
            }
            catch{
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}
