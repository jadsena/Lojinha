using Lojinha.Core.Models;

namespace Lojinha.Core.Services
{
    public interface ICarrinhoServices
    {
        void Limpar(string usuario);
        Carrinho Obter(string usuario);
        void Salvar(string usuario, Carrinho carrinho);
    }
}