using Application.Interfaces;
using FoodApplication.Application.DTOs.Item;
using FoodApplication.Domain.Data.Entities;
using MediatR;

namespace FoodApplication.Application.Features.Items.Commands
{
    public record CreateItemCommand(CreateItemDTO createItemDTO): IRequest<ItemDTO>;

    public class CreateItemHandler:IRequestHandler<CreateItemCommand, ItemDTO>
    {
        private readonly IGenericRepository<Item> _itemRepository;

        public CreateItemHandler(IGenericRepository<Item> itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<ItemDTO> Handle(CreateItemCommand req, CancellationToken ct)
        {
            if (req.createItemDTO is null)
            {
                throw new ArgumentNullException(nameof(req.createItemDTO), "CreateItemDTO cannot be null");
            }

            var item = req.createItemDTO.Map<Item>();

            await _itemRepository.AddAsync(item);
            await _itemRepository.SaveChangesAsync();

            var itemDTO = item.Map<ItemDTO>();

            return itemDTO;
        }
    }

}
