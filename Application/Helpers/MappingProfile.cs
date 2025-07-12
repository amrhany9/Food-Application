using AutoMapper;
using FoodApplication.Domain.Data.Entities;
using FoodApplication.Application.DTOs.Item;
using FoodApplication.Application.DTOs.Menu;
using FoodApplication.Application.DTOs.Recipe;
using FoodApplication.Application.DTOs.Review;
using FoodApplication.Application.DTOs.Category;

namespace FoodApplication.Presentation.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
           
            CreateMap<CreateItemDTO, Item>().ReverseMap();     
            CreateMap<UpdateItemDTO, Item>().ReverseMap();     
            CreateMap<Item, ItemDTO>().ReverseMap();           

            CreateMap<CreateRecipeDTO, Recipe>().ReverseMap();  
            CreateMap<UpdateRecipeDTO, Recipe>().ReverseMap();  
            CreateMap<Recipe, RecipeDTO>().ReverseMap();       

            CreateMap<CreateMenuDTO, Menu>().ReverseMap();      
            CreateMap<EditMenuDTO, Menu>()                     
                .ForMember(dest => dest.Items,
                           opt => opt.MapFrom(src =>
                               src.ItemIds.Select(id => new Item { Id = id }))).ReverseMap()
                .ForMember(dest => dest.ItemIds,
                           opt => opt.MapFrom(src =>
                               src.Items.Select(i => i.Id)));
            CreateMap<Menu, MenuDTO>().ReverseMap();            

            CreateMap<CreateReviewDTO, Review>().ReverseMap(); 
            CreateMap<Review, ReviewDTO>().ReverseMap();

            CreateMap<Recipe, RecipeDTO>();

            CreateMap<Category, CategoryDTO>().ReverseMap();
        }
    }
}
