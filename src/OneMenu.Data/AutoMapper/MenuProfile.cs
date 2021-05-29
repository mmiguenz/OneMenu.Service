using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using OneMenu.Core.Constants;
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

            CreateMap<MenuStepResponseModel, MenuStepResponse>().ReverseMap();
            CreateMap<MenuTransactionModel, MenuTransaction>().ReverseMap();
        }
    }
}