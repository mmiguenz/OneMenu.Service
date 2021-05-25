using AutoMapper;
using OneMenu.Core.Model;
using OneMenu.Data.MongoModels;

namespace OneMenu.Data.AutoMapper
{
    public class MenuProfile : Profile
    {
        public MenuProfile()
        {
            CreateMap<MenuModel, Menu>().ReverseMap();
            CreateMap<StepModel, Step>().ReverseMap();
            CreateMap<OptionModel, Option>().ReverseMap();
        }
    }
}