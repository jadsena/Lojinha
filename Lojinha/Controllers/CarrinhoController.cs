using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lojinha.Core.Models;
using Lojinha.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lojinha.Controllers
{
    [Authorize]
    public class CarrinhoController : Controller
    {
        private readonly IProdutoServices _produtoServices;
        private readonly ICarrinhoServices _carrinhoServices;

        public CarrinhoController(IProdutoServices produtoServices, ICarrinhoServices carrinhoServices)
        {
            _produtoServices = produtoServices;
            _carrinhoServices = carrinhoServices;
        }
        public IActionResult Add(string id)
        {
            var usuario = HttpContext.User.Identity.Name;
            Carrinho carrinho = _carrinhoServices.Obter(usuario);
            carrinho.Add(_produtoServices.ObterProduto(id).Result);
            _carrinhoServices.Salvar(usuario, carrinho);
            return PartialView("Index", carrinho);
        }
        public IActionResult Finalizar()
        {
            var usuario = HttpContext.User.Identity.Name;
            Carrinho carrinho = _carrinhoServices.Obter(usuario);

            //TODO: Inserir mensagem na Queue

            _carrinhoServices.Limpar(usuario);

            return View(carrinho);
        }
    }
}
