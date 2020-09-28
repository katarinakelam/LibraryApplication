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
            CreateMap<BookRentEventDTO, BookRentEvent>()
                .ForMember(dest => dest.DateOfRenting, opt => opt.MapFrom(src => DateTime.Now.Date))
                .ForMember(dest => dest.DateToReturn, opt => opt.MapFrom(src => DateTime.Now.AddDays(20).Date));
            CreateMap<BookRentEvent, BookRentEventViewModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName +" (" + src.User.DateOfBirth + ")"))
                .ForMember(dest => dest.BookName, opt => opt.MapFrom(src => src.Book.Title + " (" + src.Book.Publisher + ")"));
        }
    }
}
