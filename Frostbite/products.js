// Модуль товаров

function getAllProducts(products) {
  return products.filter(product => product.isActive);
}

function searchProducts(products, query) {
  return products.filter(product =>
    product.isActive &&
    product.title.toLowerCase().includes(query.toLowerCase())
  );
}

function getProductById(products, id) {
  return products.find(product => product.id === id && product.isActive);
}

function createProduct(id, title, description, price, oldPrice, imageUrl, categoryId) {
  return {
    id,
    title,
    description,
    price,
    oldPrice,
    imageUrl,
    categoryId,
    isActive: true,
    createdAt: new Date()
  };
}

module.exports = {
  getAllProducts,
  searchProducts,
  getProductById,
  createProduct
};