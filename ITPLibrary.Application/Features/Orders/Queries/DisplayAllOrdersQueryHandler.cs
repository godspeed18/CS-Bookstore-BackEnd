using AutoMapper;
using Common;
using ITPLibrary.Application.Contracts.Persistance;
using ITPLibrary.Application.Features.Orders.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ITPLibrary.Application.Features.Orders.Queries
{
    public class DisplayAllOrdersQueryHandler : IRequestHandler<DisplayAllOrdersQuery, IEnumerable<OrderDisplayVm>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public DisplayAllOrdersQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDisplayVm>> Handle(DisplayAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetOrders(request.UserId);
            if (orders == null)
            {
                return null;
            }

            List<OrderDisplayVm> mappedOrders = _mapper.Map<List<OrderDisplayVm>>(orders);
            return mappedOrders;
        }
    }
}
