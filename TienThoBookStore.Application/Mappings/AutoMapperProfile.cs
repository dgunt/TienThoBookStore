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
            CreateMap<Book, BookDTO>()
                  .ForMember(d => d.BookId, o => o.MapFrom(s => s.BookId))
            .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
            .ForMember(d => d.PublishedDate, o => o.MapFrom(s => s.PublishedDate))
            .ForMember(d => d.CategoryId, o => o.MapFrom(s => s.CategoryId))
            // map Category.Name (cần include Category trong EF query nếu dùng lazy-loading).
            .ForMember(d => d.CategoryName, o => o.MapFrom(s => s.Category.Name))
            .ForMember(d => d.CoverImage, o => o.MapFrom(s => s.CoverImage))
            .ForMember(d => d.ContentFull, o => o.MapFrom(s => s.ContentFull))
            .ForMember(d => d.ContentSample, o => o.MapFrom(s => s.ContentSample)); 
            CreateMap<Category, CategoryDTO>();
            CreateMap<Book, BookDetailDTO>()
    .ForMember(dest => dest.CoverImage, opt => opt.MapFrom(src => src.CoverImage))
    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
    .ForMember(dest => dest.ContentSample, opt => opt.MapFrom(src => src.ContentSample))
    .ForMember(dest => dest.ContentFull, opt => opt.MapFrom(src => src.ContentFull));

        }
    }
}
