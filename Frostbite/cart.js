// Модуль корзины

function createCart() {
  return [];
}

function addToCart(cart, product) {
  const existingItem = cart.find(item => item.product.id === product.id);

  if (existingItem) {
    existingItem.quantity += 1;
  } else {
    cart.push({
      product,
      quantity: 1
    });
  }

  return cart;
}

function removeFromCart(cart, productId) {
  const index = cart.findIndex(item => item.product.id === productId);

  if (index !== -1) {
    cart.splice(index, 1);
  }

  return cart;
}

function calculateCartTotal(cart) {
  return cart.reduce((total, item) => {
    return total + item.product.price * item.quantity;
  }, 0);
}

function clearCart(cart) {
  cart.length = 0;
  return cart;
}

module.exports = {
  createCart,
  addToCart,
  removeFromCart,
  calculateCartTotal,
  clearCart
};