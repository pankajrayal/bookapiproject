﻿using BookApiProject.Dtos;
using BookApiProject.Models;
using BookApiProject.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApiProject.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller {
        private ICategoryRepository _categoryRepo;
        public CategoriesController(ICategoryRepository repository) {
            _categoryRepo = repository;
        }

        // api/categories
        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryDto>))]
        public IActionResult GetCategories() {
            var categories = _categoryRepo.GetCategories();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var categoriesDto = new List<CategoryDto>();

            foreach (var category in categories) {
                categoriesDto.Add(new CategoryDto { 
                    Id = category.Id,
                    Name = category.Name
                });
            }
            return Ok(categoriesDto);
        }

        // api/categories/categoryId
        [HttpGet("{categoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(CategoryDto))]
        public IActionResult GetCategory(int categoryId) {
            if (!_categoryRepo.CategoryExists(categoryId))
                return NotFound();

            var category = _categoryRepo.GetCategory(categoryId);

            if (!ModelState.IsValid)
                return BadRequest();

            var categoryDto = new CategoryDto() { 
                Id = category.Id,
                Name = category.Name
            };

            return Ok(categoryDto);
        }

        // api/categories/books/bookId
        [HttpGet("books/{bookId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryDto>))]
        public IActionResult GetAllCategoriesForABook(int bookId) {
            // TODO: Validate the book exists

            var categories = _categoryRepo.GetAllCategoriesOfABook(bookId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoriesDto = new List<CategoryDto>();

            foreach(var category in categories) {
                categoriesDto.Add(new CategoryDto() { 
                    Id = category.Id,
                    Name = category.Name
                });
            }

            return Ok(categoriesDto);
        }

        // To Do: GetAllBooksForCategory

    }
}
