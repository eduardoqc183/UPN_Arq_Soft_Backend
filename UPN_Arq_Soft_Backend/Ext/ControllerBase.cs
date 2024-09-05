using Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace UPN_Arq_Soft_Backend
{
    public class CustomControllerBase : ControllerBase
    {
        internal ActionResult OnError(Exception ex)
        {
            if (ex is NotFoundException nfe)
            {
                return NotFound(nfe.Message);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
