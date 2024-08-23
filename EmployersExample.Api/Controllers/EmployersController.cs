using EmployersExample.Domain.Dtos.Requests;
using EmployersExample.Domain.Dtos.Responses;
using EmployersExample.Domain.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace EmployersExample.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployersController(ILogger<EmployersController> logger) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<EmployerResponse>> PostAsync([FromServices] IPostEmployerUseCase service, [FromBody] PostEmployerRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Starting post employ - CorrelationId: {Guid.NewGuid()}");
            var result = await service.PostEmployerAsync(request, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : StatusCode(int.Parse(result.Error.Code), result.Error.Message);
        }
    }
}
