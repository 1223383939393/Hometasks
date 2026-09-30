В Java строки представлены классом String. В отличие от примитивов, String — это объект (ссылочный тип). Поэтому у строк есть методы. 

String greeting = "Привет, мир!";

String — неизменяемый. После создания строку нельзя изменить. 

Строки хранятся в пуле строк. Пул строк (String Pool) в Java — это специальная область памяти в JVM, где хранятся строковые литералы, чтобы избежать дублирования и экономить память. 

Литерал (из пула) 
String s1 = "Java"; 

Новый объект (не из пула) потому что использует new, тем самым принудительно заставляет Java создать новый объект типа String в обычной области памяти 
String s2 = new String("Java");

Сравнивать нужно через .equals(), а не через два равно. Или через .equalsIgnoreCase(), тоже самое что .equals(), но при этом он игнорирует различия в регистре букв. 

String a = "hello"; //из пула 
String b = "hello"; //из пула 
System.out.println(a == b); // true — оба из пула 
System.out.println(a.equals(b)); // true — одинаковое содержимое 

String c = new String("hello"); //создает новый объект 
System.out.println(a == c); // false — разные объекты! 
System.out.println(a.equals(c)); // true — содержимое одинаковое 

String s = "Hello, World"; //для примера ниже 

Методы String: 
- .length() - возвращает длину строки 
	System.out.println(s.length()); // 6 
- .isEmpty() - проверяет, равна ли длина строки нулю 
	System.out.println(s.isEmpty()); // false 
- .isBlank() - проверяет, является ли строка пустой или содержит только пробельные символы 
	System.out.println(s.isBlank()); // false 
- .charAt(номер индекса) - выводит символ по индексу 
	char c1 = s.charAt(0); // 'J' 
- .indexOf() - искать в строке определённый символ или подстроку и возвращать индекс (порядковый номер) первого вхождения 
	System.out.println(s.indexOf("World")); // 7 — первое вхождение System.out.println(s.indexOf("o", 5)); // 8 — ищем с позиции 5 
### `.lastIndexOf(String str)` / `.lastIndexOf(int ch)`

Ищет **последнее** вхождение символа или подстроки и возвращает его индекс. Если не найдено — `-1`.

```
String s = "Hello, World, Hello";
System.out.println(s.lastIndexOf("Hello")); // 14
System.out.println(s.lastIndexOf('o'));     // 17
```

---

### `.contains(CharSequence s)`

Проверяет, содержится ли указанная подстрока в строке. Возвращает `true` или `false`.

```
String s = "Hello, World";
System.out.println(s.contains("World")); // true
System.out.println(s.contains("Java"));  // false
```

---

### `.startsWith(String prefix)`

Проверяет, начинается ли строка с указанного префикса.

```
String s = "Hello, World";
System.out.println(s.startsWith("Hello")); // true
System.out.println(s.startsWith("World")); // false
```

---

### `.endsWith(String suffix)`

Проверяет, заканчивается ли строка указанным суффиксом.

```
String s = "Hello, World";
System.out.println(s.endsWith("World")); // true
System.out.println(s.endsWith("Hello")); // false
```

---

### `.substring(int beginIndex)` / `.substring(int beginIndex, int endIndex)`

Возвращает часть строки.

- `substring(beginIndex)` — от `beginIndex` до конца.
- `substring(beginIndex, endIndex)` — от `beginIndex` (включительно) до `endIndex` (не включительно).

```
String s = "Hello, World";
System.out.println(s.substring(7));      // "World"
System.out.println(s.substring(0, 5));   // "Hello"
```

---

### `.toUpperCase()`

Возвращает строку в верхнем регистре.

```
String s = "Hello";
System.out.println(s.toUpperCase()); // "HELLO"
```

---

### `.toLowerCase()`

Возвращает строку в нижнем регистре.

```
String s = "Hello";
System.out.println(s.toLowerCase()); // "hello"
```

---

### `.trim()`

Удаляет **пробелы и управляющие символы** (с кодом ≤ 32) с начала и конца строки.

```
String s = "  Hello  ";
System.out.println(s.trim()); // "Hello"
```

---

### `.strip()`

Более современный аналог `trim()`. Удаляет **любые пробельные символы Unicode** с краёв строки.

```
String s = "  Hello  ";
System.out.println(s.strip()); // "Hello"
```

---

### `.replace(CharSequence target, CharSequence replacement)`

Заменяет **все** вхождения `target` на `replacement`.

```
String s = "Hello, World";
System.out.println(s.replace("World", "Java")); // "Hello, Java"
```

---

### `.replaceAll(String regex, String replacement)`

Заменяет все вхождения, соответствующие **регулярному выражению**.

```
String s = "Hello123World";
System.out.println(s.replaceAll("\\d", "")); // "HelloWorld" (убрали все цифры)
```

---

### `.join(CharSequence delimiter, CharSequence... elements)`

Статический метод. Соединяет несколько строк через разделитель.

```
String result = String.join(", ", "Java", "Python", "C++");
System.out.println(result); // "Java, Python, C++"
```

---

### `.split(String regex)`

Разбивает строку на массив строк по указанному разделителю (может быть регуляркой).

```
String s = "Java,Python,C++";
String[] arr = s.split(",");
System.out.println(arr[0]); // "Java"
System.out.println(arr[1]); // "Python"
```

---

### `.compareTo(String anotherString)`

Сравнивает строки **лексикографически** (по символам).  
Возвращает:

- `0` — строки равны
- `< 0` — эта строка меньше
- `> 0` — эта строка больше

```
String a = "apple";
String b = "banana";
System.out.println(a.compareTo(b)); // отрицательное число
System.out.println(b.compareTo(a)); // положительное число
```

---

### `.toCharArray()`

Преобразует строку в массив символов.

```
String s = "Hello";
char[] chars = s.toCharArray();
System.out.println(chars[0]); // 'H'
```

---

### `.valueOf(...)`

Статический метод. Преобразует разные типы (int, double, boolean, Object и т.д.) в строку.

```
int x = 123;
String s = String.valueOf(x);
System.out.println(s); // "123"
```

---

### `.toString()`

Возвращает строковое представление объекта. У `String` просто возвращает саму строку.

```
String s = "Hello";
System.out.println(s.toString()); // "Hello"
```

---

### `Integer.parseInt(String s)` / `Double.parseDouble(String s)`

Преобразуют строку в число.

```
String numStr = "123";
int num = Integer.parseInt(numStr);
System.out.println(num); // 123

String doubleStr = "3.14";
double d = Double.parseDouble(doubleStr);
System.out.println(d); // 3.14
```

---

### `.repeat(int count)`

Повторяет строку указанное количество раз (Java 11+).

```
String s = "Ha";
System.out.println(s.repeat(3)); // "HaHaHa"
```

---

## StringBuilder и StringBuffer

Оба класса позволяют **изменять** строки (в отличие от `String`).

### В чём разница?

- `StringBuilder` — **не синхронизированный**, быстрее, для однопоточных задач.
- `StringBuffer` — **синхронизированный**, медленнее, для многопоточных задач.

В 99% случаев используй `StringBuilder`.

---

### `.append(String str)`

Добавляет строку в конец.

```
StringBuilder sb = new StringBuilder("Hello");
sb.append(", World");
System.out.println(sb); // "Hello, World"
```

---

### `.insert(int offset, String str)`

Вставляет строку по указанному индексу.

```
StringBuilder sb = new StringBuilder("Hello");
sb.insert(5, ", World");
System.out.println(sb); // "Hello, World"
```

---

### `.reverse()`

Переворачивает строку.

```
StringBuilder sb = new StringBuilder("Hello");
sb.reverse();
System.out.println(sb); // "olleH"
```

---

### `.deleteCharAt(int index)`

Удаляет символ по индексу.

```
StringBuilder sb = new StringBuilder("Hello");
sb.deleteCharAt(0);
System.out.println(sb); // "ello"
```

---

### `.charAt(int index)`

Возвращает символ по индексу (есть и у `StringBuilder`, и у `StringBuffer`).

```
StringBuilder sb = new StringBuilder("Hello");
char c = sb.charAt(0);
System.out.println(c); // 'H'
```

