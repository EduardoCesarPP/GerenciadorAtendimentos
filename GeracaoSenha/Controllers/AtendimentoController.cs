using GeracaoSenha.Data;
using GeracaoSenha.Data.Dtos;
using GeracaoSenha.Model;
using GeracaoSenha.Model.Enum;
using GeracaoSenha.Service;
using Microsoft.AspNetCore.Mvc;

namespace GeracaoSenha.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AtendimentoController : ControllerBase
    {
        private readonly ILogger<AtendimentoController> _logger;
        private IRecepcaoService _recepcaoService;
        private IPacienteService _pacienteService;

        public AtendimentoController(ILogger<AtendimentoController> logger, IRecepcaoService recepcaoService, IPacienteService pacienteService)
        {
            _logger = logger;
            _recepcaoService = recepcaoService;
            _pacienteService = pacienteService;
        }



        /// <summary>
        /// Registro inicial do atendimento e geração da senha. Retorna o atendimento com sua senha e link para acompanhamento.
        /// </summary>
        /// <param name="TipoAtendimento">Valor necessário para identificar o tipo do atendimento. [EXAME, CONSULTA, EXAME_PREFERENCIAL, CONSULTA_PREFERENCIAL, CIRURGIA]</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso o registro seja realizado com sucesso</response> 
        /// <response code="400">Caso o tipo de atendimento não exista.</response> 
        /// 
        [HttpPost]
        public IActionResult RegistrarNovoAtendimento([FromBody] CreateAtendimentoDto atendimentoDto)
        {
            ReadAtendimentoDto readAtendimentoDto = _pacienteService.RegistrarNovoAtendimento(atendimentoDto.TipoAtendimento);
            return Ok(readAtendimentoDto);
        }

        /// <summary>
        /// Registra a chamada de um atendimento ao guichê.
        /// </summary>
        /// <param name="senha">Código da senha a ser finalizada.</param>
        /// <param name="guiche">Número do guichê.</param>
        /// <returns>IActionResult</returns>
        /// <response code="204">Caso o registro seja realizado com sucesso</response> 
        /// <response code="404">Caso a senha não exista.</response> 
        /// 
        [HttpPut("chamar/{senha}")]
        public IActionResult Chamar(string senha, [FromBody] ChamarAtendimentoDto chamarDto)
        {
            try
            {
                _recepcaoService.Chamar(senha, chamarDto.Guiche);
            }
            catch (Exception)
            {
                return NotFound();
            }
            return NoContent();
        }

        /// <summary>
        /// Registra o início de um atendimento no guichê.
        /// </summary>
        /// <param name="senha">Código da senha.</param>
        /// <returns>IActionResult</returns>
        /// <response code="204">Caso o registro seja realizado com sucesso</response> 
        /// <response code="404">Caso a senha não exista.</response> 
        /// 
        [HttpPut("iniciar/{senha}")]
        public IActionResult IniciarAtendimento(string senha)
        {
            try
            {
                _recepcaoService.IniciarAtendimento(senha);
            }
            catch (Exception)
            {
                return NotFound();
            }
            return NoContent();
        }

        /// <summary>
        /// Registra a finalização de um atendimento no guichê.
        /// </summary>
        /// <param name="senha">Código da senha a ser finalizada.</param>
        /// <returns>IActionResult</returns>
        /// <response code="204">Caso o registro seja realizado com sucesso</response> 
        /// <response code="404">Caso a senha não exista.</response> 
        /// 
        [HttpPut("finalizar/{senha}")]
        public IActionResult FinalizarAtendimento(string senha)
        {
            try
            {
                _recepcaoService.FinalizarAtendimento(senha);
            }
            catch (Exception)
            {
                return NotFound();
            }
            return NoContent();
        }

        /// <summary>
        /// Consulta um atendimento com base na senha gerada.
        /// </summary>
        /// <param name="senha">Código da senha do atendimento.</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso a senha exista.</response> 
        /// <response code="404">Caso a senha não exista.</response> 
        /// 
        [HttpGet("{senha}")]
        public IActionResult ConsultarAtendimento(string senha)
        {
            try
            {
                ReadAtendimentoDto readAtendimentoDto = _pacienteService.ConsultarAtendimento(senha);
                if (readAtendimentoDto == null) return NotFound();
                return Ok(readAtendimentoDto);
            }
            catch (Exception ex)
            {
                return Problem();
            }
        }
    }
}
