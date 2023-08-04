//using AutoMapper;
//using MediatR;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using OrderSvc.Application.Command.CategoryCommand;
//using OrderSvc.Application.Command.ProductCommand;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BFF.Web.Controllers
//{
//    public class CategoryController :ControllerBase
//    {
//        private readonly IConfiguration _configuration;
//        private readonly ILogger<CategoryController> _logger;
//        private readonly IMapper _mapper;
//        private readonly IMediator _mediator;
//        public CategoryController(IConfiguration configuration, ILogger<CategoryController> logger, IMapper mapper, IMediator mediator)
//        {
//            _configuration = configuration;
//            _logger = logger;
//            _mapper = mapper;
//            _mediator = mediator;
//        }
//        [HttpDelete("Cateproduct/Delete")]
//        public async Task<IActionResult> DeletecateProduct([FromBody] DeleteCateProCommand rq)
//        {
//            try
//            {


//                var res = await _mediator.Send(rq);
//                return Ok(res);
//            }
//            catch (Exception e)
//            {
//                return StatusCode(500, e.Message);
//            }
//        }
//    }
//}
