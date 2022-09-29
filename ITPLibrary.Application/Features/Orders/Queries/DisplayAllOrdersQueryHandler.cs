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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public DisplayAllOrdersQueryHandler(IOrderRepository orderRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDisplayVm>> Handle(DisplayAllOrdersQuery request, CancellationToken cancellationToken)
        {
            int userId = CommonMethods.GetUserIdFromContext(_httpContextAccessor.HttpContext);

            var orders = await _orderRepository.GetOrders(userId);
            if (orders == null)
            {
                return null;
            }

            List<OrderDisplayVm> mappedOrders = _mapper.Map<List<OrderDisplayVm>>(orders);
            return mappedOrders;
        }
    }
}
