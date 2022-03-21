using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.Identidade.API.Controllers
{
    [ApiController] // decorando com esse atributos o swagger entende e mostra os schemas
    public abstract class MainController : Controller
    {
        protected ICollection<string> Erros = new List<string>();

        protected ActionResult CustomResponse(object result = null) // result setado como nulo para não precisar informar o parametro
        {
            if (OperacaoValida())
            {
                return Ok(result);
            }

            // ValidationProblemDetails implementa um padrão especifica numa RFC, com detalhes de como uma api deve responder sobre detalhes de erros
            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                // novo objeto dictionary chamado Mensagens com as mensagens de erro
                {"Mensagens", Erros.ToArray() }
            }));
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var erros = ModelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                AdicionarErroProcessamento(erro.ErrorMessage);
            }
            return CustomResponse();
        }

        protected bool OperacaoValida()
        {
            return !Erros.Any();
        }

        protected void AdicionarErroProcessamento(string erro)
        {
            Erros.Add(erro);
        }

        protected void LimparErrosProcessamento()
        {
            Erros.Clear();
        }
    }
}
