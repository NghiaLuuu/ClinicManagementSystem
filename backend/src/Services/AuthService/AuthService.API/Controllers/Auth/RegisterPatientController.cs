namespace AuthService.API.Controllers;

using AuthService.Application.Contracts;
using AuthService.Application.Exceptions;
using AuthService.Application.UseCases.Auth.RegisterPatient;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth/register/patient")]
public sealed class RegisterPatientController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<AuthResponse>> RegisterPatient(
        [FromBody] RegisterRequest request,
        [FromServices] IRegisterPatientUseCase useCase,
        CancellationToken ct)
    {
        try
        {
            var response = await useCase.ExecuteAsync(request, ct);
            return Created($"/users/{response.UserId}", response);
        }
        catch (ConflictException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
