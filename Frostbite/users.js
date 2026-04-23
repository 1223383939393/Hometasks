// Модуль пользователей

function validateEmail(email) {
  return email.includes("@") && email.includes(".");
}

function registerUser(users, email, password) {
  if (!validateEmail(email)) {
    return { error: "Некорректный email" };
  }

  const existingUser = users.find(user => user.email === email);
  if (existingUser) {
    return { error: "Пользователь уже существует" };
  }

  const newUser = {
    id: users.length + 1,
    email,
    password,
    isAuthenticated: true,
    createdAt: new Date()
  };

  users.push(newUser);
  return newUser;
}

function loginUser(users, email, password) {
  const user = users.find(user => user.email === email && user.password === password);

  if (!user) {
    return { error: "Неверный email или пароль" };
  }

  user.isAuthenticated = true;
  return user;
}

function logoutUser(user) {
  user.isAuthenticated = false;
  return user;
}

module.exports = {
  validateEmail,
  registerUser,
  loginUser,
  logoutUser
};