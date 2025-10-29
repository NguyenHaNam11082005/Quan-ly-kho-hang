﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StockManagementMVC.Models;
using StockManagementMVC.repository;
using System;
using X.PagedList.Extensions;
using X.PagedList;




namespace StockManagementMVC.Areas.Admin.Controllers
{
  
    [Authorize(Roles = "Admin,Nhân Viên")]
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly DataContext _dbContext;


            public ProductController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index(int? page)
        {
            int pageSize = 5;// so san pham tren 1 trang
            int pageNumber = page ?? 1;

           var products = _dbContext.Products.OrderBy(p=>p.Id);
            var pagedProducts = products.ToPagedList(pageNumber,pageSize);
            return View(pagedProducts);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Create(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.UpdatedDate = DateTime.Now;

                await _dbContext.Products.AddAsync(model);

                await _dbContext.SaveChangesAsync();
                TempData["SuccessMessage"] = "Thêm sản phẩm thành công!";
                return RedirectToAction("Index","Product");
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            var product = _dbContext.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();
            return RedirectToAction("Index", "Product");
        }











        [HttpGet]
        public IActionResult Edit( int id)
        {
            var product = _dbContext.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }







        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductModel model, int id)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                var product = _dbContext.Products.FirstOrDefault(p =>p.Id == id);
                if (product == null)
                {
                    return NotFound();
                }
                // Cập nhật các trường cần thiết
                product.ProductCode = model.ProductCode;
                product.ProductName = model.ProductName;
                product.ProductUnit = model.ProductUnit;
                product.ProductQuantity = model.ProductQuantity;
                product.Location = model.Location;
                product.ProductDescription = model.ProductDescription;
                product.UpdatedDate = DateTime.Now;

                _dbContext.SaveChanges();
                TempData["SuccessMessage"] = "Cập nhật sản phẩm thành công!";
                return RedirectToAction("Index","Product");
                
            }

            return View(model);
        }










    }
}
