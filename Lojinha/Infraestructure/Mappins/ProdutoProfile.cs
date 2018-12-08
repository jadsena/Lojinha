using AutoMapper;
using Lojinha.Core.Models;
using Lojinha.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lojinha.Infraestructure.Mappins
{
    public class ProdutoProfile : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<Produto, ProdutoViewModel>()
                .ForMember(p => p.Id, vm => vm.MapFrom(x => x.Id))
                .ForMember(p => p.Nome, vm => vm.MapFrom(x => x.Nome))
                .ForMember(p => p.ImagemUrl, vm => vm.MapFrom(x => x.ImagemPrincipalUrl))
                .ForMember(p => p.Preco, vm => vm.MapFrom(x => x.Preco));
        }
    }
}
