using System;
using System.Collections.Generic;
using CategoryService.API.Models;
using CategoryService.API.Repository;
using CategoryService.API.Exceptions;

namespace CategoryService.API.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        /// <summary>
        /// Used to create Category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public Category CreateCategory(Category category)
        {
            try
            {
                var result = _categoryRepository.CreateCategory(category);
                if (result == null)
                {
                    throw new CategoryNotCreatedException("This category id already exists");
                }
                else
                    return result;

            }

            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        /// <summary>
        /// Used to delete category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public bool DeleteCategory(int categoryId)
        {
            try
            {
                if (!_categoryRepository.DeleteCategory(categoryId))
                {
                    throw new CategoryNotFoundException("This category id not found");
                }
                else
                { return true; }

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        /// <summary>
        /// Get categories by user Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Category> GetAllCategoriesByUserId(string userId)
        {
            try
            {
                var result = _categoryRepository.GetAllCategoriesByUserId(userId);
                return result;
              
            }

            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        /// <summary>
        /// Get category by Id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public Category GetCategoryById(int categoryId)
        {
            try
            {
                var result = _categoryRepository.GetCategoryById(categoryId);
                if (result == null)
                    throw new CategoryNotFoundException("This category id not found");
                else return result;
            }

            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        /// <summary>
        /// Update category by ID
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public bool UpdateCategory(int categoryId, Category category)
        {
            try
            {
                var result = _categoryRepository.UpdateCategory(categoryId, category);
                if (!result)
                {
                    throw new CategoryNotFoundException("This category id not found");
                }
                else
                    return result;
            }

            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
