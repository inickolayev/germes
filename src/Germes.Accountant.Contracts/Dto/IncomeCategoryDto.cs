﻿using System;

namespace Germes.Accountant.Contracts.Dto
{
    public class IncomeCategoryDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}