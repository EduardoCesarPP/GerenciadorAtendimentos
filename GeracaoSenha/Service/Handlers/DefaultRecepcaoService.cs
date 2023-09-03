using GeracaoSenha.Data;
using GeracaoSenha.Data.Dtos;
using GeracaoSenha.Model;

namespace GeracaoSenha.Service.Handlers
{
    public class DefaultRecepcaoService : IRecepcaoService
    {
        private IDao<Atendimento> _dao;
        public DefaultRecepcaoService(IDao<Atendimento> dao)
        {
            _dao = dao;
        }
        public void Chamar(string senha, int guiche)
        {
            Atendimento atendimento = _dao.ConsultarPorChave(senha);
            if (atendimento == null) throw new Exception();
            atendimento.Chamar(guiche);
        }
        public void IniciarAtendimento(string senha)
        {
            Atendimento atendimento = _dao.ConsultarPorChave(senha);
            if (atendimento == null) throw new Exception();
            atendimento.Iniciar();
        }
        public void FinalizarAtendimento(string senha)
        {
            Atendimento atendimento = _dao.ConsultarPorChave(senha);
            if (atendimento == null) throw new Exception();
            atendimento.Finalizar();
        }
        public ReadAtendimentoDto ConsultarProximo()
        {
            return new ReadAtendimentoDto(_dao.ListaOrdenada().FirstOrDefault());
        }
    }
}
