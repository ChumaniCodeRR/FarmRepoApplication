using System.Runtime;
using Application_test_repo.Modal;
using Application_test_repo.Repos.Models;
using AutoMapper;
using ClosedXML;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Application_test_repo.Helper
{
    public class AutoMapperHandler : Profile
    {
        public AutoMapperHandler()
        {
            CreateMap<TblCustomer, CustomerModal>()
                .ForMember(item => item.Statusname, 
                opt => opt.MapFrom(
                item => (item.IsActive != null && item.IsActive.Value) ? "Active" : "In active")).ReverseMap();

           /* CreateMap<TblMedical, MedicalModal>(); */

            CreateMap<TblMedical, MedicalModal>().ReverseMap();

            CreateMap<TblBred, BredModal>().ReverseMap();

            CreateMap<TblAnimal, AnimalModal>()
                .ForMember(dest => dest.AnimalId,
                opts => opts.MapFrom(src => src.AnimalId)).ReverseMap();

            CreateMap<TblCrop, CropModal>()
                .ForMember(icon => icon.CropId,
                opts => opts.MapFrom(src => src.CropId)).ReverseMap();

            CreateMap<TblSeed, SeedModal>()
                .ForMember(icon => icon.SeedId,
                opts => opts.MapFrom(src => src.SeedId)).ReverseMap();
        }
    }
}
