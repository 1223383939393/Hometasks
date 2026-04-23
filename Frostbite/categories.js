// Модуль категорий

function getAllCategories(categories) {
  return categories;
}

function getCategoryById(categories, id) {
  return categories.find(category => category.id === id);
}

module.exports = {
  getAllCategories,
  getCategoryById
};