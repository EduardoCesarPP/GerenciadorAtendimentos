using GeracaoSenha.Data;
using GeracaoSenha.Data.Dtos;
using GeracaoSenha.Model;
using GeracaoSenha.Model.Enum;

namespace GeracaoSenha.Service.Handlers
{
    public class DefaultPacienteService : IPacienteService
    {
        private IDao<Atendimento> _dao;
        public DefaultPacienteService(IDao<Atendimento> dao)
        {
            _dao = dao;
        }
        public ReadAtendimentoDto RegistrarNovoAtendimento(TipoAtendimento tipoAtendimento)
        {
            return new ReadAtendimentoDto(_dao.Incluir(tipoAtendimento));
        }
        public IEnumerable<ReadAtendimentoDto> ConsultarFila()
        {
            List<ReadAtendimentoDto> listaDto = new List<ReadAtendimentoDto>();
            _dao.ListaOrdenada().ForEach(atendimento => listaDto.Add(new ReadAtendimentoDto(atendimento)));
            return listaDto;
        }
        public ReadAtendimentoDto ConsultarAtendimento(string senha)
        {
            return new ReadAtendimentoDto(_dao.ConsultarPorChave(senha));
        }
        public ReadPosicaoDto ConsultarPosicao(string senha)
        {
            List<Atendimento> pendentes = _dao.ListaOrdenada();
            Atendimento atendimento = _dao.ConsultarPorChave(senha);
            ReadPosicaoDto poscaoDto = new ReadPosicaoDto();
            poscaoDto.Posicao = pendentes.IndexOf(atendimento) + 1;
            return poscaoDto;
        }
    }
}
