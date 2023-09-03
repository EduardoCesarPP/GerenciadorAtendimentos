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
    public class FilaController : ControllerBase
    {
        private readonly ILogger<AtendimentoController> _logger;
        private IRecepcaoService _recepcaoService;
        private IPacienteService _pacienteService;

        public FilaController(ILogger<AtendimentoController> logger, IRecepcaoService recepcaoService, IPacienteService pacienteService)
        {
            _logger = logger;
            _recepcaoService = recepcaoService;
            _pacienteService = pacienteService;
        }

        /// <summary>
        /// Retorna a lista de atendimentos pendentes por ordem de chegada.
        /// </summary>
        /// <returns>IEnumerable</returns>
        /// <response code="200"></response>
        /// 
        [HttpGet]
        public IEnumerable<ReadAtendimentoDto> ConsultarFila()
        {
            return _pacienteService.ConsultarFila();
        }

        /// <summary>
        /// Retorna o próximo atendimento pendentes.
        /// </summary>
        /// <returns>ReadAtendimentoDto</returns>
        /// <response code="200"></response>
        /// 
        [HttpGet("proximo")]
        public ReadAtendimentoDto ConsultarProximo()
        {
            return _recepcaoService.ConsultarProximo();
        }

        /// <summary>
        /// Retorna a posição de um atendimento na fila com base na senha.
        /// </summary>
        /// <param name="senha">Código da senha do atendimento.</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso a senha exista.</response> 
        /// <response code="404">Caso a senha não exista.</response> 
        /// 
        [HttpGet("posicao/{senha}")]
        public IActionResult ConsultarPosicao(string senha)
        {
            return Ok(_pacienteService.ConsultarPosicao(senha));
        }
    }
}
