using AutoMapper;
using Lojinha.Core.Models;
using Lojinha.Core.Services;
using Lojinha.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lojinha.Controllers
{
    [Authorize]
    public class ProdutosController : Controller
    {
        private readonly IProdutoServices _produtoServices;
        private readonly IMapper _mapper;

        public ProdutosController(IProdutoServices produtoServices, IMapper mapper)
        {
            _produtoServices = produtoServices;
            _mapper = mapper;
        }

        public IActionResult Create()
        {
            var produto = new Produto()
            {
                Id = 330673,
                Nome = "Samsung S7",
                Categoria = new Categoria()
                {
                    Id = 1,
                    Nome = "Celulares"
                },
                Fabricante = new Fabricante()
                {
                    Id = 100,
                    Nome = "Samsung"
                },
                Descricao = "Celular Samsung S7",
                ImagemPrincipalUrl = "",
                Tags = new[] { "Celular", "Samsung", "S7" },
                Preco = 1200m
            };

            //_produtoServices.AddProduto(produto);
            //TODO: ProdutoServico.Add(produto);
            return Content("Ok");
        }

        public async Task<IActionResult> List()
        {
            var produtos = await _produtoServices.ObterProdutos();
            var vm = _mapper.Map<List<ProdutoViewModel>>(produtos);
            return View(vm);
        }
        public async Task<IActionResult> Details(string id)
        {
            var produto = await _produtoServices.ObterProduto(id);
            var vm = _mapper.Map<ProdutoViewModel>(produto);
            return Json(vm);
        }
    }
}
