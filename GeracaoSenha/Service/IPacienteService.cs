using GeracaoSenha.Data.Dtos;
using GeracaoSenha.Model.Enum;

namespace GeracaoSenha.Service
{
    public interface IPacienteService
    {
        public abstract ReadAtendimentoDto RegistrarNovoAtendimento(TipoAtendimento tipoAtendimento);
        public abstract IEnumerable<ReadAtendimentoDto> ConsultarFila();
        public abstract ReadAtendimentoDto ConsultarAtendimento(string senha);
        public abstract ReadPosicaoDto ConsultarPosicao(string senha);
    }
}

