﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienThoBookStore.Application.DTOs.CategoryDTO
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = default!;
    }
}
