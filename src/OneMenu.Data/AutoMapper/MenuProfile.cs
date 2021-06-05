using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using OneMenu.Core.Model;
using OneMenu.Core.Model.Menus;
using OneMenu.Core.Model.Menus.Validations;
using OneMenu.Data.MongoModels;
using OnstringeMenu.Core.Constants;

namespace OneMenu.Data.AutoMapper
{
    public class MenuProfile : Profile
    {
        public MenuProfile()
        {
            CreateMap<MenuModel, Menu>().ReverseMap();
            CreateMap<StepModel, Step>();
            CreateMap<Step, StepModel>();
            CreateMap<Validation, ValidationModel>();
            CreateMap<ValidationModel, Validation>().ConvertUsing(src => CreateValidation(src));
            CreateMap<OptionModel, Option>().ReverseMap();

            CreateMap<MenuStepResponseModel, MenuStepResponse>().ReverseMap();
            CreateMap<MenuTransactionModel, MenuTransaction>().ReverseMap();
        }

        private List<Validation> MapValidations(StepModel src)
        {
            return src.Validations.Select(CreateValidation).Where(v => v != null).ToList();
        }

        private Validation CreateValidation(ValidationModel validationModel)
        {
            return validationModel.Type switch
            {
                ValidationType.Regex => new RegexValidation(validationModel.ErrorMsj, validationModel.Value, validationModel.Type),
                _ => null
            };
        }
    }
}