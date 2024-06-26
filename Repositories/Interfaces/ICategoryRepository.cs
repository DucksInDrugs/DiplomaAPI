﻿using DiplomaAPI.Entities;

namespace DiplomaAPI.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();
        Task<IEnumerable<Category>> GetFirstCategories();
        Task<Category> GetById(int id);
        Task<int> Create(Category category);
        Task<bool> Update(Category category);
        Task<bool> Delete(int id);
        Task<IEnumerable<Category>> GetByRole(string role);
    }
}
