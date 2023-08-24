using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OrderSvc.Application.Command.CompanyCommand;
using OrderSvc.Application.Command.ProductCommand;
using OrderSvc.Application.Query.ProductQuery;
using OrderSvc.Application.Command.CategoryCommand;
using BFF.Web.DTOs;
using OrderSvc.Application.Query.CompanyQuery;
using OrderSvc.Application.Command.OrderCommand;
using OrderSvc.Application.Command.TransactionsCommand;
using OrderSvc.Application.Query.OrderQuery;
using OrderSvc.Application.Query.PackageMemberQuery;

namespace BFF.Web.Controllers
{
    [ApiController]
    [Route("gateway/")]
    public class OrderController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<OrderController> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public OrderController(IConfiguration configuration, ILogger<OrderController> logger, IMapper mapper, IMediator mediator)
        {
            _configuration = configuration;
            _logger = logger;
            _mapper = mapper;
            _mediator = mediator;
        }
        private string GetUsernameFromContext()
        {
            return User.FindFirst("name")?.Value;
        }

        private string GetUserIdFromContext()
        {
            return User.FindFirst("sub")?.Value;
        }
        private string GetUserIp()
        {
            return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
        [HttpPost("Product/Create")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDTO rq)
        {
            try
            {
                rq.Id = Guid.NewGuid();
                var pro = new AddProductCommand
                {
                    Id = rq.Id,
                    ProductName = rq.ProductName,
                    Price = rq.Price,
                    CompanyId = rq.CompanyId,
                    Image = rq.Image,
                    PriceNum = rq.PriceNum,

                    ProviderId = rq.ProviderId,
                    Decripstion = rq.Decripstion,
                    CreatedBy = GetUsernameFromContext(),
                    CreatedDate = DateTime.Now,
                };
                var res = await _mediator.Send(pro);

                if (rq.CateId != null)
                {

                    var catepro = new CreateCategoryProductCommand
                    {
                        CategoryId = rq.CateId,
                        ProductId = rq.Id,
                    };
                    await _mediator.Send(catepro);
                }
                var view = new GetProductDetailQuery { Id = rq.Id };
                var result = await _mediator.Send(view);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpPut("Product/Update")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand rq)
        {
            try
            {

                rq.LastModifiedDate = DateTime.Now;
                rq.LastModifiedBy = GetUsernameFromContext();
                var res = await _mediator.Send(rq);
                var view = new GetProductDetailQuery { Id = (Guid)rq.Id };
                var result = await _mediator.Send(view);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpDelete("Product/Delete")]
        public async Task<IActionResult> DeleteProduct([FromBody] DeleteProductCommand rq)
        {
            try
            {
                var deletecatepro = new DeleteCateProCommand { Id = rq.Id };
                var del = await _mediator.Send(deletecatepro);

                var res = await _mediator.Send(rq);
                return Ok(res);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpPost("Product/Search")]
        public async Task<IActionResult> SearchProduct([FromBody] SearchProductQuery rq)
        {
            try
            {


                var res = await _mediator.Send(rq);
                return Ok(res);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpGet("Product/GetDetail")]
        public async Task<IActionResult> ViewDetailProduct([FromQuery] GetProductDetailQuery rq)
        {
            try
            {


                var res = await _mediator.Send(rq);
                return Ok(res);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        //COMPANY
        [HttpPost("Company/Create")]
        public async Task<IActionResult> CreatCompany([FromBody] CreateCompanyCommand rq)
        {
            _logger.LogInformation($"REST request Create Company Product : {JsonConvert.SerializeObject(rq)}");

            try
            {


                var res = await _mediator.Send(rq);
                return Ok(res);
            }
            catch (Exception e)
            {
                _logger.LogError($"REST request to Create Company fail: {e.Message}");

                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("Company/Search")]
        public async Task<IActionResult> SearchCompany([FromBody] SearchComapanyQuery rq)
        {
            _logger.LogInformation($"REST request Search Company Product : {JsonConvert.SerializeObject(rq)}");

            try
            {


                var res = await _mediator.Send(rq);
                return Ok(res);
            }
            catch (Exception e)
            {
                _logger.LogError($"REST request to Search Company fail: {e.Message}");

                return StatusCode(500, e.Message);
            }
        }
        //  Category
        [HttpPost("Category/Create")]
        public async Task<IActionResult> CreatCategory([FromBody] CreateCategoryCommand rq)
        {
            _logger.LogInformation($"REST request Create Category : {JsonConvert.SerializeObject(rq)}");

            try
            {


                var res = await _mediator.Send(rq);
                return Ok(res);
            }
            catch (Exception e)
            {
                _logger.LogError($"REST request to Create Category fail: {e.Message}");

                return StatusCode(500, e.Message);
            }
        }
        [HttpPost("CategoryProduct/Create")]

        public async Task<IActionResult> CreatCategoryPro([FromBody] CreateCategoryProductCommand rq)
        {
            _logger.LogInformation($"REST request Create Category : {JsonConvert.SerializeObject(rq)}");

            try
            {


                var res = await _mediator.Send(rq);
                return Ok(res);
            }
            catch (Exception e)
            {
                _logger.LogError($"REST request to Create Category fail: {e.Message}");

                return StatusCode(500, e.Message);
            }
        }
        [HttpPost("Order/Price")]

        public async Task<IActionResult> OrderPrice([FromBody] DoPriceCommand rq)
        {
            _logger.LogInformation($"REST request Create Category : {JsonConvert.SerializeObject(rq)}");

            try
            {


                var res = await _mediator.Send(rq);
                return Ok(res);
            }
            catch (Exception e)
            {
                _logger.LogError($"REST request to Create Category fail: {e.Message}");

                return StatusCode(500, e.Message);
            }
        }

        // <summary>
        // Tạo giao dịch
        // </summary>
        [HttpPost("Order/Create")]

        public async Task<IActionResult> CreatOrder([FromBody] TransactionDto rq)
        {
            _logger.LogInformation($"REST request Create Order : {JsonConvert.SerializeObject(rq)}");

            try
            {
                if (rq.Id != null)
                {
                    var Order = new ViewDetailOrderQuery()
                    {
                        Id = (Guid)rq.Id,
                    };
                    var orderes = await _mediator.Send(Order);
                    var UpTrasRq = new UpdateTransactionsCommand()
                    {
                        TransactionId = orderes.TransactionId,
                        TransactionName = rq.TransactionName,
                        TransactionType = rq.TransactionType,
                        TransactionDate = rq.TransactionDate == null ? DateTime.Now : rq.TransactionDate,
                        PaymentMethod = rq.PaymentMethod,
                        TotalAmount = rq.TotalAmount,
                    };
                    await _mediator.Send(UpTrasRq);
                    var UpAff = new UpdateAffiliateC()
                    {
                        Id = orderes.affiliate.AffiId,
                        BuyerId = rq.BuyerId,
                        ProviderId = rq.ProviderId,
                        SalerId = rq.SalerId,
                        ReferrerId = rq.ReferrerId,
                        ParticipantsId = rq.ParticipantsId,

                    };
                    await _mediator.Send(UpAff);

                    var orderUp = new UpdateStatusOrderCommand()
                    {
                        Id = (Guid)rq.Id,
                        BoughtPerson = rq.BoughtPerson,
                        SalePerson = rq.SalePerson,

                        VoucherId = rq.VoucherId,
                        LastModifiedDate = DateTime.Now,

                    };
                    var resUpOrder = await _mediator.Send(orderUp);
                    if (orderes.OrderItem != null)
                    {

                    foreach (var i in orderes.OrderItem)
                    {
                       
                        foreach (var p in rq.Products)
                        {

                            var pr = new UpdateOrderItemC
                            {
                                Id = i.Id,
                                OrderId=(Guid) rq.Id,
                                ProductId = p.ProductId,
                                Quantity = p.quantity,

                            };
                             await _mediator.Send(pr);
                        }
                    }
                    }
                    else
                    {
                        foreach (var p in rq.Products)
                        {

                            var pr = new CreateOrderItemC
                            {
                                Id = Guid.NewGuid(),
                                OrderId = (Guid)rq.Id,
                                ProductId = p.ProductId,
                                Quantity = p.quantity,

                            };
                            await _mediator.Send(pr);
                        }
                    }
                }
                else
                {


                    //Create
                    rq.Id = Guid.NewGuid();
                    // tao phien giao dich
                    var tran = new AddTransactionsCommand
                    {
                        TransactionName = rq.TransactionName,
                        TransactionType = rq.TransactionType,
                        TransactionDate = rq.TransactionDate == null ? DateTime.Now : rq.TransactionDate,
                        PaymentMethod = rq.PaymentMethod,
                        TotalAmount = rq.TotalAmount,


                    };
                    var tranres = await _mediator.Send(tran);
                    //  nhap affiiate
                    var aff = new AddAffiliateCommand
                    {
                        BuyerId = rq.BuyerId,
                        ProviderId = rq.ProviderId,
                        SalerId = rq.SalerId,
                        ReferrerId = rq.ReferrerId,
                        ParticipantsId = rq.ParticipantsId,
                    };


                    var affires = await _mediator.Send(aff);

                    //                 Order
                    var order = new CreateOrderCommand
                    {
                        Id = (Guid)rq.Id,
                        BoughtPerson = rq.BoughtPerson,
                        SalePerson = rq.SalePerson,
                        TransactionId = tranres.TransactionId,
                        AffiliateId = affires.Id,
                        VoucherId = rq.VoucherId,
                        Status = 0,
                        CreatedDate = DateTime.Now,
                    };
                    var res = await _mediator.Send(order);

                    // ProductaddOrder
                    foreach (var p in rq.Products)
                    {

                        var pr = new CreateOrderItemC
                        {
                            Id = Guid.NewGuid(),
                            OrderId = (Guid)rq.Id,
                            ProductId = p.ProductId,
                            Quantity = p.quantity,

                        };
                        var prores = await _mediator.Send(pr);
                    }
                }

                var view = new ViewDetailOrderQuery
                {
                    Id = (Guid)rq.Id,
                };
                var result = await _mediator.Send(view);

                var resOrder = _mapper.Map<ResponseSearchOrderDto>(result);
                return Ok(resOrder);
            }
            catch (Exception e)
            {
                _logger.LogError($"REST request to Create Category fail: {e.Message}");

                return StatusCode(500, e.Message);


            }
        }
        [HttpPost("Order/Detail")]

        public async Task<IActionResult> OrderDetail([FromBody] ViewDetailOrderQuery rq)
        {
            _logger.LogInformation($"REST request Create Category : {JsonConvert.SerializeObject(rq)}");

            try
            {

                var res = await _mediator.Send(rq);
                return Ok(res);
            }
            catch (Exception e)
            {
                _logger.LogError($"REST request to Create Category fail: {e.Message}");

                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("Order/SearchOrGetall")]

        public async Task<IActionResult> SearchOrGetAll([FromBody] SearchOrGetAllQuery rq)
        {
            _logger.LogInformation($"REST request Create Category : {JsonConvert.SerializeObject(rq)}");

            try
            {

                var res = await _mediator.Send(rq);
                foreach (var item in res.Data)
                {
                    var p = new PriceIdComand { Id = item.Id };
                    var price = await _mediator.Send(p);
                    item.total = price.total;
                    item.discount = price.discount;
                    item.finalPrice = price.finalPrice;
                }

                return Ok(res);
            }
            catch (Exception e)
            {
                _logger.LogError($"REST request to Create Category fail: {e.Message}");

                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("Order/UpdateStatus")]

        public async Task<IActionResult> UpdateStatusOrder([FromBody] UpdateStatusRq rq)
        {
            _logger.LogInformation($"REST request Create Category : {JsonConvert.SerializeObject(rq)}");

            try
            {
                var result = 0;
                foreach (var i in rq.Id)
                {
                    var up = new UpdateStatusOrderCommand
                    {
                        Id = i,
                        Status = rq.Status,
                    };
                    var res = await _mediator.Send(up);
                    result++;
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError($"REST request to Create Category fail: {e.Message}");

                return StatusCode(500, e.Message);
            }
        }
        [HttpGet("Order/GetAllTotal")]
        public async Task<IActionResult> GetAllTotal()
        {
            try
            {
                var rq = new GetTotalQ { };
                var res = await _mediator.Send(rq);
                return Ok(res);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
