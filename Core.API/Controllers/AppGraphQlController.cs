using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.NewtonsoftJson;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Core.API.Controllers
{
    // [ApiController]
    [Route("graphql")]
    public class AppGraphQlController : ControllerBase
    {
        private readonly ISchema _schema;
        private readonly IDocumentExecuter _executer;
        private readonly ILogger<AppGraphQlController> _logger;

        public AppGraphQlController(
            ISchema schema,
            IDocumentExecuter executer,
            ILogger<AppGraphQlController> logger
        )
        {
            _schema = schema;
            _executer = executer;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(
            [FromBody] GraphQlQuery query
        )
        {
            ExecutionResult result = await _executer.ExecuteAsync(
                conf =>
                {
                    conf.Schema = _schema;
                    conf.Query = query.Query;
                    conf.Inputs = query.Variables?.ToInputs();
                }
            );

            if (result.Errors?.Any() == true)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Data);
        }
    }
}