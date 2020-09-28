using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LibraryApplication.Models;
using LibraryApplication.ViewModels;
using LibraryApplication.ViewModels.DTOs;

namespace LibraryApplication.AutoMapper
{
    /// <summary>
    /// The automapper profile.
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<UserDTO, User>();
            CreateMap<User, UserViewModel>();
        }
    }
}
