using GeracaoSenha.Model;
using GeracaoSenha.Model.Enum;

namespace GeracaoSenha.Data
{
    public class AtendimentosContext
    {
        public List<Atendimento> Atendimentos { get; set; }

        public AtendimentosContext()
        {
            Atendimentos = new List<Atendimento>();
        }
    }
}
