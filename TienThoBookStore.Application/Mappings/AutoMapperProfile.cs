using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TienThoBookStore.Application.DTOs.BookDTO;
using TienThoBookStore.Application.DTOs.CategoryDTO;
using TienThoBookStore.Domain.Entities;

namespace TienThoBookStore.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Map Book → BookDTO
            CreateMap<Book, BookDTO>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<Book, BookDetailDTO>()
    .ForMember(dest => dest.CoverImage, opt => opt.MapFrom(src => src.CoverImage))
    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
    .ForMember(dest => dest.ContentSample, opt => opt.MapFrom(src => src.ContentSample))
    .ForMember(dest => dest.ContentFull, opt => opt.MapFrom(src => src.ContentFull));

        }
    }
}
