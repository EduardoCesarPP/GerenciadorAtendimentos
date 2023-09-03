using GeracaoSenha.Model;
using GeracaoSenha.Model.Enum;

namespace GeracaoSenha.Data
{
    public interface IDao<T>
    {
        public abstract T Incluir(object obj);
        public abstract T ConsultarPorChave(object OBJ);
        public abstract List<T> ListaOrdenada();
    }
}
