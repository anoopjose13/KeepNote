using System.Collections.Generic;
using System.Linq;
using CategoryService.API.Models;
using MongoDB.Driver;

namespace CategoryService.API.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ICategoryContext _context;
        public CategoryRepository(ICategoryContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Used to create Category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public Category CreateCategory(Category category)
        {
            _context.Categories.InsertOne(category);
            FilterDefinition<Category> filter = Builders<Category>.Filter.Eq(u => u.Id, category.Id);
            return _context.Categories.Find(filter).FirstOrDefault();
        }
        /// <summary>
        /// Delete Category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public bool DeleteCategory(int categoryId)
        {
            var filter = Builders<Category>.Filter.Eq(u => u.Id, categoryId);
            var result = _context.Categories.DeleteOneAsync(filter).Result;
            return result.IsAcknowledged;
        }
        /// <summary>
        /// Get category by UserId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Category> GetAllCategoriesByUserId(string userId)
        {
            FilterDefinition<Category> filter = Builders<Category>.Filter.Eq(u => u.CreatedBy, userId);
            return _context.Categories.Find(filter).ToList();
        }
        /// <summary>
        /// Get category by Id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public Category GetCategoryById(int categoryId)
        {
            FilterDefinition<Category> filter = Builders<Category>.Filter.Eq(u => u.Id, categoryId);
            return _context.Categories.Find(filter).FirstOrDefault();
        }
        /// <summary>
        /// Update Category by Id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public bool UpdateCategory(int categoryId, Category category)
        {
            var filter = Builders<Category>.Filter.Eq(u => u.Id, categoryId);
            var update = Builders<Category>.Update
                .Set("Name", category.Name).Set("Description", category.Description).Set("CreatedBy", category.CreatedBy).Set("CreationDate", category.CreationDate);
            var result = _context.Categories.UpdateOneAsync(filter, update).Result;
            return result.IsAcknowledged;
        }
    }
}
