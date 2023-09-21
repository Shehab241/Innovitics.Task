using Innovitics.Task.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Innovitics.Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("verify")]
        public IActionResult VerifyUser([FromQuery] VerifyUserVM userVM )
        {
            var User = new User
            {
                CardNumber = userVM.CardNumber,
                PIN = userVM.PIN
            };
            try
            {
                if (!ModelState.IsValid)
                {
                   
                    return BadRequest(ModelState);
                }
                else
                {
                    if (User.CardNumber <= 0 || User.PIN <= 0)
                    {
                        return BadRequest("Card Number or Pin is empty.");
                    }
                    else
                    {
                        var user = _context.User.FirstOrDefault(u => u.CardNumber == User.CardNumber && u.PIN == User.PIN);


                        if (user == null)
                        {
                            return NotFound("User is not found.");
                        }
                        return Ok(user); // Return the user's account details
                    }

                }
            }
            catch (Exception ex)
            {

                return BadRequest("Something went wrong: " + ex.Message + ".");

            }



        }
        [HttpGet("GetBalance")]
        public IActionResult GetBalance([Required]long CardNumber)
        {
            try
            {
            
              if(CardNumber < 10000000000000)
              {
                return BadRequest("Card number must be 14 digits or more");
              }
              var user = _context.User.SingleOrDefault(u => u.CardNumber == CardNumber);

              if (user == null)
              {
                  return NotFound("User is not found.");
              }

              return Ok($"Account balance: {user.Balance} L.E.");
                
            }
            catch (Exception ex)
            {

                return BadRequest("Something went wrong: " + ex.Message+".");

            }

        }

        [HttpPost("withdraw")]
        public IActionResult WithdrawCash([FromQuery]CashWithdrawalRequest request)
        {
            var User = new User
            {
                CardNumber = request.CardNumber,
            };
            try
            {
                if (!ModelState.IsValid)
                {

                    return BadRequest(ModelState);
                }
                else { 
                var user = _context.User.SingleOrDefault(u => u.CardNumber == request.CardNumber);

                if (user == null)
                {
                    return NotFound("User is not found.");
                }

                if (request.Amount <= 0 || request.Amount > 1000)
                {
                    return BadRequest("Invalid withdrawal Amount.");
                }

                if (user.Balance < request.Amount)
                {
                    return BadRequest("Insufficient balance.");
                }

                user.Balance -= request.Amount;
                _context.SaveChanges();

                return Ok($"Withdrawn {request.Amount} L.E. New balance: {user.Balance} L.E.");
             }
            }
            catch (Exception ex)
            {

                return BadRequest("Something went wrong: " + ex.Message + ".");

            }

        }
    }
}
