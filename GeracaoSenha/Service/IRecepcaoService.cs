using GeracaoSenha.Data.Dtos;

namespace GeracaoSenha.Service
{
    public interface IRecepcaoService
    {
        public abstract void Chamar(string senha, int guiche);
        public abstract void IniciarAtendimento(string senha);
        public abstract void FinalizarAtendimento(string senha);
        public abstract ReadAtendimentoDto ConsultarProximo();
    }
}
