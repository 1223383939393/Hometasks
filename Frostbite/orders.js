// Модуль заказов

function createOrder(orders, user, cart, totalPrice) {
  if (!user || !user.isAuthenticated) {
    return { error: "Пользователь не авторизован" };
  }

  if (cart.length === 0) {
    return { error: "Корзина пуста" };
  }

  const newOrder = {
    id: orders.length + 1,
    userId: user.id,
    items: cart.map(item => ({
      productId: item.product.id,
      title: item.product.title,
      price: item.product.price,
      quantity: item.quantity
    })),
    totalPrice,
    status: "created",
    createdAt: new Date()
  };

  orders.push(newOrder);
  return newOrder;
}

function getUserOrders(orders, userId) {
  return orders.filter(order => order.userId === userId);
}

module.exports = {
  createOrder,
  getUserOrders
};