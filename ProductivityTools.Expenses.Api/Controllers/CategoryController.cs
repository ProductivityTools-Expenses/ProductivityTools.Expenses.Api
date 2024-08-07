﻿using Microsoft.AspNetCore.Mvc;
using ProductivityTools.Expenses.Database.Objects;
using ProductivityTools.Expenses.Database;
using ProductivityTools.Expenses.Api.Requests;
using Microsoft.EntityFrameworkCore;
using ProductivityTools.Expenses.Api.Responses;
using Azure.Core;

namespace ProductivityTools.Expenses.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ExpenseBaseController
    {

        public CategoryController(ILogger<WeatherForecastController> logger, ExpensesContext expensesContext) : base(logger, expensesContext)
        {
        }

        [HttpPost]
        [Route("CagetoryList")]
        public List<Category> CategoryList(CategoryListRequest request)
        {
            //it is ok when bagId is null, than only categories where no bag is assigned should be returned
            var r = ExpensesContext.Categories.Where(x => x.BagId == request.BagId);
            //if (request.BagId.HasValue)
            //{
            //     r = r.Where(x => x.BagId== request.BagId.Value);
            //    //r = r.Where(x => x.BagCategories.Any(bc => bc.BagId == request.BagId.Value)).Include(x=>x.BagCategories);
            //}
            //else
            //{
            //    r=r.Where(x=>x.BagId==)
            //}
            //var responseResult = r.Select(x => new CategoryListResponse { CategoryId = x.CategoryId, Name = x.Name, BagCategoryId = x.BagCategories.Where(x=>x.BagId==request.BagId).First().BagCategoryId.Value });
            
            //var xx=responseResult.ToList();
            return r.ToList();
        }

        [HttpGet]
        [Route("CategoryListAll")]
        public List<Category> CategoryListAll()
        {
            var r = ExpensesContext.Categories.AsQueryable();
            return r.ToList();
        }

        [HttpGet]
        [Route("Category")]
        public Category Category(int categoryId)
        {
            var r = ExpensesContext.Categories.FirstOrDefault(x => x.CategoryId == categoryId);
            return r;
        }



        [HttpPost]
        [Route("CategorySave")]
        public StatusCodeResult CategorySave(Category category)
        {


            if (category.CategoryId.HasValue)
            {
                var r = ExpensesContext.Categories.Update(category);
            }
            else
            {
                var r = ExpensesContext.Categories.Add(category);
            }
            ExpensesContext.SaveChanges();
            return Ok();
        }

    }
}
