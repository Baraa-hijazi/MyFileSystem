using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using MyFileSystem.Core.DTOs;
using MyFileSystem.Entities;

namespace MyFileSystem.Validators
{
    public class FileValidator:AbstractValidator<IFormFile>
    {
        public FileValidator()
        {
            RuleFor(f => f.FileName).NotEmpty().MinimumLength(1).MaximumLength(20).WithMessage("File Name Must be between 1 and 20 chars. ");
            //RuleFor(f => f.FileSize).InclusiveBetween(0, 1000);
        }
    }
}
