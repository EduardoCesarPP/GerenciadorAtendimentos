using GeracaoSenha.Model;
using GeracaoSenha.Model.Enum;

namespace GeracaoSenha.Data
{
    public class AtendimentoDao : IDao<Atendimento>
    {
        AtendimentosContext _context;
        public AtendimentoDao(AtendimentosContext context)
        {
            _context = context;
        }
        public Atendimento Incluir(object obj)
        {
            TipoAtendimento tipoAtendimento = (TipoAtendimento)obj;
            Atendimento? Ultimo = _context.Atendimentos
                .Where(atendimento => atendimento.TipoAtendimento == tipoAtendimento)
                .LastOrDefault();
            Atendimento Novo = new Atendimento(Ultimo is null ? 0 : Ultimo.Id, tipoAtendimento);
            _context.Atendimentos.Add(Novo);
            return Novo;
        }
        public Atendimento ConsultarPorChave(object obj)
        {
            string senha = (string)obj;
            return _context.Atendimentos.First(atendimento => atendimento.Senha() == senha);
        }
        public List<Atendimento> ListaOrdenada()
        {
            List<Atendimento> fila = new List<Atendimento>();
            List<Atendimento> pendentes = new List<Atendimento>(_context.Atendimentos
                .Where(atendimento => atendimento.IsPendente())
                .OrderBy(atendimento => atendimento.HorarioChegada));
            Atendimento? preferencial = pendentes
                .Where(atendimento => atendimento.isPreferencial())
                .FirstOrDefault();
            int preferenciaisAdicionados = 0;
            while (pendentes.Count > 0)
            {
                if (preferencial is null || preferenciaisAdicionados == 2)
                {
                    preferenciaisAdicionados = 0;
                    fila.Add(pendentes.Where(atendimento => !atendimento.isPreferencial()).First());
                    pendentes.Remove(pendentes.Where(atendimento => !atendimento.isPreferencial()).First());
                }
                else
                {
                    preferenciaisAdicionados++;
                    fila.Add(preferencial);
                    pendentes.Remove(preferencial);
                    preferencial = pendentes.Where(atendimento => atendimento.isPreferencial()).FirstOrDefault();
                }
            }
            return fila;
        }
    }
}
