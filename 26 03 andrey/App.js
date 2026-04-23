import React, { useMemo, useState } from 'react';
import {
  SafeAreaView,
  View,
  Text,
  TextInput,
  StyleSheet,
  ScrollView,
  TouchableOpacity,
  StatusBar,
} from 'react-native';

// =========================
// ДАННЫЕ ТОВАРОВ МАГАЗИНА
// =========================
const productsData = [
  { id: 1, title: 'UFC 4', oldPrice: 2199, price: 999, badge: 'ХИТ', status: 'new' },
  { id: 2, title: 'The Witcher 3', oldPrice: 2999, price: 1499, badge: '-49%', status: 'popular' },
  { id: 3, title: 'Mortal Kombat 11', oldPrice: 1199, price: 799, badge: 'SALE', status: 'new' },
  { id: 4, title: 'Spider-Man', oldPrice: 3499, price: 2699, badge: 'MARVEL', status: 'processed' },
  { id: 5, title: 'Ghost of Tsushima', oldPrice: 3699, price: 2999, badge: 'TOP', status: 'processed' },
];

// =========================
// ГЛАВНОЕ ПРИЛОЖЕНИЕ
// =========================
export default function App() {
  // Текущий экран приложения
  const [screen, setScreen] = useState('shop');

  // Поиск по товарам
  const [search, setSearch] = useState('');

  // Выбранный товар
  const [selectedGame, setSelectedGame] = useState(null);

  // Email пользователя
  const [email, setEmail] = useState('');

  // Сообщение для подсказок и действий
  const [message, setMessage] = useState('Добро пожаловать в Frostbite Shop!');

  // Корзина
  const [cart, setCart] = useState([]);

  // Статус заказа
  const [orderStatus, setOrderStatus] = useState('empty');

  // =========================
  // ФИЛЬТРАЦИЯ ТОВАРОВ ПО ПОИСКУ
  // =========================
  const filteredProducts = useMemo(() => {
    return productsData.filter((product) =>
      product.title.toLowerCase().includes(search.toLowerCase())
    );
  }, [search]);

  // =========================
  // ДОБАВЛЕНИЕ ТОВАРА В КОРЗИНУ
  // =========================
  const handleBuy = (product) => {
    const exists = cart.find((item) => item.id === product.id);

    if (exists) {
      setMessage(`"${product.title}" уже есть в корзине`);
      setOrderStatus('pending');
      return;
    }

    setCart([...cart, product]);
    setSelectedGame(product.title);
    setMessage(`Товар "${product.title}" добавлен в корзину`);
    setOrderStatus('processed');
  };

  // =========================
  // УДАЛЕНИЕ ТОВАРА ИЗ КОРЗИНЫ
  // =========================
  const handleRemoveFromCart = (id) => {
    const removedItem = cart.find((item) => item.id === id);
    const updatedCart = cart.filter((item) => item.id !== id);

    setCart(updatedCart);
    setMessage(`Товар "${removedItem.title}" удалён из корзины`);

    if (updatedCart.length === 0) {
      setOrderStatus('empty');
    }
  };

  // =========================
  // РЕГИСТРАЦИЯ / ВВОД EMAIL
  // =========================
  const handleLogin = () => {
    if (!email.includes('@') || !email.includes('.')) {
      setMessage('Ошибка: введите корректный email');
      setOrderStatus('error');
      return;
    }

    setMessage(`Пользователь ${email} успешно зарегистрирован`);
    setOrderStatus('processed');
    setScreen('profile');
  };

  // =========================
  // ОФОРМЛЕНИЕ ЗАКАЗА
  // =========================
  const handleCheckout = () => {
    if (cart.length === 0) {
      setMessage('Корзина пуста. Добавьте товары перед оформлением заказа');
      setOrderStatus('error');
      return;
    }

    if (!email) {
      setMessage('Для оформления заказа сначала введите email в профиле');
      setOrderStatus('pending');
      return;
    }

    setMessage(`Заказ оформлен на почту ${email}`);
    setOrderStatus('success');
  };

  // =========================
  // ОЧИСТКА КОРЗИНЫ
  // =========================
  const clearCart = () => {
    setCart([]);
    setMessage('Корзина очищена');
    setOrderStatus('empty');
  };

  // =========================
  // ИТОГОВАЯ СУММА КОРЗИНЫ
  // =========================
  const totalPrice = cart.reduce((sum, item) => sum + item.price, 0);

  // =========================
  // ПОДСКАЗКИ
  // =========================
  const showHint = (text) => {
    setMessage(text);
  };

  // =========================
  // ЦВЕТ СТАТУСА
  // =========================
  const getStatusColor = () => {
    switch (orderStatus) {
      case 'error':
        return '#8B0000'; // тёмно-красный
      case 'empty':
        return '#A9A9A9'; // серый
      case 'processed':
        return '#000000'; // чёрный
      case 'pending':
        return '#4b5dff'; // синий
      case 'success':
        return '#1c9c45'; // зелёный
      default:
        return '#333';
    }
  };

  // =========================
  // ЭКРАН ПРОФИЛЯ
  // =========================
  if (screen === 'profile') {
    return (
      <SafeAreaView style={styles.safeArea}>
        <StatusBar barStyle="dark-content" />
        <View style={styles.profileContainer}>
          <Text style={styles.profileTitle}>Профиль</Text>
          <Text style={styles.profileSubtitle}>
            Введите почту, чтобы зарегистрироваться
          </Text>

          <TextInput
            style={styles.input}
            placeholder="Email"
            value={email}
            onChangeText={setEmail}
            keyboardType="email-address"
            onFocus={() => showHint('Введите email для регистрации и заказа')}
          />

          <TouchableOpacity
            style={styles.mainButton}
            onPress={handleLogin}
            onPressIn={() => showHint('Нажмите, чтобы сохранить email')}
          >
            <Text style={styles.mainButtonText}>Продолжить</Text>
          </TouchableOpacity>

          <TouchableOpacity
            style={styles.secondaryButton}
            onPress={() => setScreen('shop')}
            onPressIn={() => showHint('Вернуться на главный экран магазина')}
          >
            <Text style={styles.secondaryButtonText}>Назад в магазин</Text>
          </TouchableOpacity>

          <TouchableOpacity
            style={styles.secondaryButton}
            onPress={() => setScreen('cart')}
            onPressIn={() => showHint('Открыть корзину с выбранными товарами')}
          >
            <Text style={styles.secondaryButtonText}>Перейти в корзину</Text>
          </TouchableOpacity>

          <View style={styles.infoBox}>
            <Text style={styles.infoTitle}>Данные пользователя</Text>
            <Text style={styles.infoText}>
              Email: {email ? email : 'не указан'}
            </Text>
            <Text style={styles.infoText}>
              Последний выбранный товар: {selectedGame ? selectedGame : 'нет'}
            </Text>
          </View>

          <Text style={[styles.statusText, { color: getStatusColor() }]}>
            Статус: {message}
          </Text>
        </View>
      </SafeAreaView>
    );
  }

  // =========================
  // ЭКРАН КОРЗИНЫ
  // =========================
  if (screen === 'cart') {
    return (
      <SafeAreaView style={styles.safeArea}>
        <StatusBar barStyle="dark-content" />
        <ScrollView contentContainerStyle={styles.cartContainer}>
          <Text style={styles.cartTitle}>Корзина</Text>

          {cart.length === 0 ? (
            <Text style={styles.emptyCartText}>Корзина пока пуста</Text>
          ) : (
            cart.map((item) => (
              <View key={item.id} style={styles.cartItem}>
                <View style={{ flex: 1 }}>
                  <Text style={styles.cartItemTitle}>{item.title}</Text>
                  <Text style={styles.cartItemPrice}>{item.price}₽</Text>
                </View>

                <TouchableOpacity
                  style={styles.removeButton}
                  onPress={() => handleRemoveFromCart(item.id)}
                  onPressIn={() => showHint(`Удалить "${item.title}" из корзины`)}
                >
                  <Text style={styles.removeButtonText}>Удалить</Text>
                </TouchableOpacity>
              </View>
            ))
          )}

          <View style={styles.totalBox}>
            <Text style={styles.totalText}>Итоговая сумма: {totalPrice}₽</Text>
          </View>

          <TouchableOpacity
            style={styles.mainButton}
            onPress={handleCheckout}
            onPressIn={() => showHint('Оформить заказ на все товары в корзине')}
          >
            <Text style={styles.mainButtonText}>Оформить заказ</Text>
          </TouchableOpacity>

          <TouchableOpacity
            style={styles.secondaryButton}
            onPress={clearCart}
            onPressIn={() => showHint('Полностью очистить корзину')}
          >
            <Text style={styles.secondaryButtonText}>Очистить корзину</Text>
          </TouchableOpacity>

          <TouchableOpacity
            style={styles.secondaryButton}
            onPress={() => setScreen('shop')}
            onPressIn={() => showHint('Вернуться к товарам')}
          >
            <Text style={styles.secondaryButtonText}>Назад в магазин</Text>
          </TouchableOpacity>

          <Text style={[styles.statusText, { color: getStatusColor() }]}>
            Статус заказа: {message}
          </Text>
        </ScrollView>
      </SafeAreaView>
    );
  }

  // =========================
  // ГЛАВНЫЙ ЭКРАН МАГАЗИНА
  // =========================
  return (
    <SafeAreaView style={styles.safeArea}>
      <StatusBar barStyle="dark-content" />
      <ScrollView contentContainerStyle={styles.container}>
        {/* Шапка приложения */}
        <View style={styles.header}>
          <View>
            <Text style={styles.logo}>FROSTBITE SHOP ❄️</Text>
            <Text style={styles.logoSub}>Вы можете нам доверять!</Text>
          </View>

          <TouchableOpacity
            onPress={() => setScreen('profile')}
            onPressIn={() => showHint('Открыть экран профиля')}
          >
            <View style={styles.profileBox}>
              <Text style={styles.profileIcon}>👤</Text>
              <Text style={styles.profileText}>Профиль</Text>
            </View>
          </TouchableOpacity>
        </View>

        {/* Поисковая строка */}
        <View style={styles.searchRow}>
          <TextInput
            style={styles.searchInput}
            placeholder="Ваши желания находятся здесь..."
            value={search}
            onChangeText={setSearch}
            onFocus={() => showHint('Введите название игры для поиска')}
          />
          <TouchableOpacity
            style={styles.searchButton}
            onPressIn={() => showHint('Кнопка поиска товаров')}
          >
            <Text style={styles.searchButtonText}>🔍</Text>
          </TouchableOpacity>
        </View>

        {/* Баннер акций */}
        <View style={styles.banner}>
          <Text style={styles.bannerTitle}>ПРОЩАЕМСЯ С ЗИМОЙ</Text>
          <Text style={styles.bannerSubtitle}>
            СКИДКИ ДО <Text style={styles.bannerAccent}>90%</Text>
          </Text>
        </View>

        {/* Текст состояния */}
        <Text style={[styles.statusText, { color: getStatusColor(), marginBottom: 14 }]}>
          {message}
        </Text>

        {/* Список товаров */}
        <ScrollView
          horizontal
          showsHorizontalScrollIndicator={false}
          contentContainerStyle={styles.productsRow}
        >
          {filteredProducts.length > 0 ? (
            filteredProducts.map((product) => (
              <TouchableOpacity
                key={product.id}
                activeOpacity={0.95}
                onPressIn={() => showHint(`Товар: ${product.title}. Цена: ${product.price}₽`)}
              >
                <View style={styles.card}>
                  <View style={styles.cover}>
                    <Text style={styles.coverTop}>ORIGINAL DISC</Text>
                    <Text style={styles.coverTitle}>{product.title}</Text>

                    <View style={styles.badge}>
                      <Text style={styles.badgeText}>{product.badge}</Text>
                    </View>

                    <View style={styles.fakeImage}>
                      <Text style={styles.fakeImageText}>ИГРА</Text>
                    </View>
                  </View>

                  <Text style={styles.oldPrice}>{product.oldPrice}₽</Text>
                  <Text style={styles.price}>{product.price}₽</Text>

                  <TouchableOpacity
                    style={styles.buyButton}
                    onPress={() => handleBuy(product)}
                    onPressIn={() => showHint(`Добавить "${product.title}" в корзину`)}
                  >
                    <Text style={styles.buyButtonText}>КУПИТЬ</Text>
                  </TouchableOpacity>
                </View>
              </TouchableOpacity>
            ))
          ) : (
            <Text style={styles.notFound}>Ничего не найдено</Text>
          )}
        </ScrollView>

        {/* Нижняя навигация */}
        <View style={styles.bottomNav}>
          <TouchableOpacity
            style={styles.navItem}
            onPress={() => setScreen('shop')}
            onPressIn={() => showHint('Главный экран магазина')}
          >
            <Text style={styles.navTextActive}>Дом</Text>
          </TouchableOpacity>

          <TouchableOpacity
            style={styles.navItem}
            onPress={() => setScreen('cart')}
            onPressIn={() => showHint('Открыть корзину')}
          >
            <Text style={styles.navText}>
              Корзина {cart.length > 0 ? `(${cart.length})` : ''}
            </Text>
          </TouchableOpacity>

          <TouchableOpacity
            style={styles.navItem}
            onPress={() => setScreen('profile')}
            onPressIn={() => showHint('Открыть профиль')}
          >
            <Text style={styles.navText}>Профиль</Text>
          </TouchableOpacity>
        </View>
      </ScrollView>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  safeArea: {
    flex: 1,
    backgroundColor: '#dfe8ff',
  },
  container: {
    padding: 16,
    paddingBottom: 30,
    backgroundColor: '#dfe8ff',
  },
  header: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'flex-start',
    marginBottom: 14,
  },
  logo: {
    fontSize: 28,
    fontWeight: '900',
    color: '#111',
  },
  logoSub: {
    fontSize: 18,
    fontWeight: '600',
    color: '#222',
    marginTop: 4,
  },
  profileBox: {
    alignItems: 'center',
    paddingTop: 4,
  },
  profileIcon: {
    fontSize: 28,
  },
  profileText: {
    fontSize: 16,
    fontWeight: '700',
  },
  searchRow: {
    flexDirection: 'row',
    marginBottom: 16,
  },
  searchInput: {
    flex: 1,
    backgroundColor: '#fff',
    borderRadius: 20,
    paddingHorizontal: 16,
    paddingVertical: 10,
    fontSize: 15,
  },
  searchButton: {
    width: 46,
    marginLeft: 8,
    backgroundColor: '#fff',
    borderRadius: 23,
    justifyContent: 'center',
    alignItems: 'center',
  },
  searchButtonText: {
    fontSize: 20,
  },
  banner: {
    backgroundColor: '#d9c9f5',
    borderRadius: 12,
    paddingVertical: 18,
    paddingHorizontal: 16,
    alignItems: 'center',
    marginBottom: 20,
  },
  bannerTitle: {
    fontSize: 18,
    fontWeight: '800',
    color: '#fff',
    textAlign: 'center',
  },
  bannerSubtitle: {
    fontSize: 18,
    fontWeight: '800',
    color: '#fff',
    marginTop: 8,
    textAlign: 'center',
  },
  bannerAccent: {
    color: '#f2e400',
    fontSize: 22,
    fontWeight: '900',
  },
  statusText: {
    fontSize: 15,
    fontWeight: '700',
  },
  productsRow: {
    paddingBottom: 14,
  },
  card: {
    width: 170,
    backgroundColor: 'rgba(255,255,255,0.75)',
    borderRadius: 12,
    padding: 10,
    marginRight: 14,
    alignItems: 'center',
  },
  cover: {
    width: '100%',
    height: 240,
    backgroundColor: '#111a3a',
    borderRadius: 8,
    padding: 8,
    justifyContent: 'space-between',
  },
  coverTop: {
    color: '#b8c7ff',
    fontSize: 10,
    textAlign: 'center',
    letterSpacing: 1,
  },
  coverTitle: {
    color: '#fff',
    fontSize: 26,
    fontWeight: '900',
    textAlign: 'center',
  },
  badge: {
    alignSelf: 'flex-end',
    backgroundColor: '#8b5cf6',
    borderRadius: 6,
    paddingVertical: 4,
    paddingHorizontal: 8,
  },
  badgeText: {
    color: '#fff',
    fontWeight: '800',
    fontSize: 12,
  },
  fakeImage: {
    flex: 1,
    marginTop: 10,
    borderRadius: 8,
    backgroundColor: '#284dbe',
    justifyContent: 'center',
    alignItems: 'center',
  },
  fakeImageText: {
    color: '#fff',
    fontSize: 26,
    fontWeight: '900',
  },
  oldPrice: {
    marginTop: 10,
    fontSize: 14,
    color: '#777',
    textDecorationLine: 'line-through',
    fontWeight: '700',
  },
  price: {
    fontSize: 28,
    fontWeight: '900',
    color: '#111',
    marginTop: 2,
  },
  buyButton: {
    marginTop: 10,
    backgroundColor: '#1542e0',
    borderRadius: 14,
    paddingVertical: 10,
    width: '100%',
    alignItems: 'center',
  },
  buyButtonText: {
    color: '#fff',
    fontSize: 16,
    fontWeight: '900',
    letterSpacing: 1,
  },
  notFound: {
    fontSize: 18,
    fontWeight: '700',
    color: '#333',
    paddingVertical: 30,
  },
  bottomNav: {
    marginTop: 18,
    flexDirection: 'row',
    justifyContent: 'space-around',
    backgroundColor: '#fff',
    borderRadius: 18,
    paddingVertical: 12,
  },
  navItem: {
    paddingHorizontal: 10,
  },
  navTextActive: {
    color: '#1542e0',
    fontWeight: '900',
  },
  navText: {
    color: '#666',
    fontWeight: '700',
  },
  profileContainer: {
    flex: 1,
    justifyContent: 'center',
    padding: 24,
    backgroundColor: '#dfe8ff',
  },
  profileTitle: {
    fontSize: 30,
    fontWeight: '900',
    color: '#111',
    marginBottom: 10,
    textAlign: 'center',
  },
  profileSubtitle: {
    fontSize: 18,
    color: '#333',
    textAlign: 'center',
    marginBottom: 24,
  },
  input: {
    backgroundColor: '#fff',
    borderRadius: 14,
    paddingHorizontal: 16,
    paddingVertical: 14,
    fontSize: 16,
    marginBottom: 16,
  },
  mainButton: {
    backgroundColor: '#1542e0',
    borderRadius: 14,
    paddingVertical: 14,
    alignItems: 'center',
    marginBottom: 12,
  },
  mainButtonText: {
    color: '#fff',
    fontSize: 17,
    fontWeight: '900',
  },
  secondaryButton: {
    backgroundColor: '#fff',
    borderRadius: 14,
    paddingVertical: 14,
    alignItems: 'center',
    marginBottom: 12,
  },
  secondaryButtonText: {
    color: '#1542e0',
    fontSize: 16,
    fontWeight: '800',
  },
  infoBox: {
    backgroundColor: 'rgba(255,255,255,0.7)',
    borderRadius: 14,
    padding: 16,
    marginTop: 6,
    marginBottom: 14,
  },
  infoTitle: {
    fontSize: 18,
    fontWeight: '900',
    marginBottom: 8,
    color: '#111',
  },
  infoText: {
    fontSize: 16,
    color: '#333',
    marginBottom: 6,
    fontWeight: '600',
  },
  cartContainer: {
    padding: 20,
    paddingBottom: 30,
    backgroundColor: '#dfe8ff',
  },
  cartTitle: {
    fontSize: 30,
    fontWeight: '900',
    color: '#111',
    textAlign: 'center',
    marginBottom: 18,
  },
  emptyCartText: {
    fontSize: 18,
    textAlign: 'center',
    color: '#777',
    marginBottom: 20,
    fontWeight: '700',
  },
  cartItem: {
    backgroundColor: 'rgba(255,255,255,0.8)',
    borderRadius: 14,
    padding: 14,
    marginBottom: 12,
    flexDirection: 'row',
    alignItems: 'center',
  },
  cartItemTitle: {
    fontSize: 18,
    fontWeight: '800',
    color: '#111',
  },
  cartItemPrice: {
    fontSize: 16,
    fontWeight: '700',
    color: '#1542e0',
    marginTop: 4,
  },
  removeButton: {
    backgroundColor: '#b22222',
    borderRadius: 10,
    paddingVertical: 10,
    paddingHorizontal: 14,
  },
  removeButtonText: {
    color: '#fff',
    fontWeight: '800',
  },
  totalBox: {
    backgroundColor: '#fff',
    borderRadius: 14,
    padding: 16,
    marginTop: 6,
    marginBottom: 14,
  },
  totalText: {
    fontSize: 20,
    fontWeight: '900',
    textAlign: 'center',
    color: '#111',
  },
});