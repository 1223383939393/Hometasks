const { getAllProducts, searchProducts, getProductById, createProduct } = require("./products");
const { createCart, addToCart, removeFromCart, calculateCartTotal, clearCart } = require("./cart");
const { registerUser, loginUser, logoutUser } = require("./users");
const { createOrder, getUserOrders } = require("./orders");
const { getAllCategories } = require("./categories");

console.log("=== Симуляция работы Frostbite Shop ===");

// Данные
const categories = [
  { id: 1, name: "Action" },
  { id: 2, name: "RPG" },
  { id: 3, name: "Racing" }
];

const products = [
  createProduct(1, "Cyberpunk 2077", "Футуристическая RPG", 1999, 3999, "cyberpunk.jpg", 2),
  createProduct(2, "Forza Horizon 5", "Гоночная игра", 2499, 4999, "forza.jpg", 3),
  createProduct(3, "DOOM Eternal", "Шутер от первого лица", 1499, 2999, "doom.jpg", 1)
];

const users = [];
const orders = [];
const cart = createCart();

// 1. Показ категорий
console.log("\nКатегории:");
console.log(getAllCategories(categories));

// 2. Показ товаров
console.log("\nВсе товары:");
console.log(getAllProducts(products));

// 3. Поиск товара
console.log("\nПоиск 'forza':");
console.log(searchProducts(products, "forza"));

// 4. Регистрация пользователя
console.log("\nРегистрация пользователя:");
const user = registerUser(users, "player@mail.com", "123456");
console.log(user);

// 5. Добавление товаров в корзину
console.log("\nДобавляем товары в корзину:");
const product1 = getProductById(products, 2);
const product2 = getProductById(products, 1);

addToCart(cart, product1);
addToCart(cart, product2);
addToCart(cart, product1);

console.log(cart);

// 6. Считаем итог
const total = calculateCartTotal(cart);
console.log("\nИтоговая сумма:", total);

// 7. Удаление одного товара
console.log("\nУдаляем Cyberpunk 2077 из корзины:");
removeFromCart(cart, 1);
console.log(cart);

// 8. Новый итог
const updatedTotal = calculateCartTotal(cart);
console.log("\nНовая итоговая сумма:", updatedTotal);

// 9. Создание заказа
console.log("\nОформление заказа:");
const order = createOrder(orders, user, cart, updatedTotal);
console.log(order);

// 10. История заказов
console.log("\nИстория заказов пользователя:");
console.log(getUserOrders(orders, user.id));

// 11. Очистка корзины после заказа
clearCart(cart);
console.log("\nКорзина после оформления:");
console.log(cart);

// 12. Выход из аккаунта
logoutUser(user);
console.log("\nПользователь после выхода:");
console.log(user);