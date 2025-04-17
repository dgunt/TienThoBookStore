using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TienThoBookStore.Application.DTOs.BookDTO;
using TienThoBookStore.Domain.Entities;

namespace TienThoBookStore.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Map Book → BookDTO
            CreateMap<Book, BookDTO>();
        }
    }
}
