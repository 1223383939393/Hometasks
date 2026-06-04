import sys
import os

# Добавляем корень проекта в sys.path, если запускают из подкаталога
CURRENT_DIR = os.path.dirname(__file__)
if CURRENT_DIR not in sys.path:
    sys.path.insert(0, CURRENT_DIR)

from src.booking_service import BookingService


def main():
    print("=== Калькулятор стоимости бронирования (КиноРФ) ===")

    try:
        base_price_str = input("Введите базовую цену (целое число > 0): ")
        base_price = int(base_price_str)

        age_str = input("Введите возраст: ")
        age = int(age_str)

        is_weekend_str = input("Это выходной? (y/n): ").strip().lower()
        if is_weekend_str not in ("y", "n"):
            print("Некорректный ввод для выходного (ожидалось y/n)")
            return
        is_weekend = is_weekend_str == "y"

        promo_code_str = input("Введите промокод (или оставьте пустым): ").strip()
        promo_code = promo_code_str if promo_code_str else None

        service = BookingService()
        booking = service.create_booking(
            base_price=base_price,
            age=age,
            is_weekend=is_weekend,
            promo_code=promo_code
        )

        print("\n=== Результат бронирования ===")
        print(f"ID бронирования:      {booking['id']}")
        print(f"Базовая цена:         {booking['base_price']} руб.")
        print(f"Возраст:              {booking['age']}")
        print(f"Выходной:             {booking['is_weekend']}")
        print(f"Промокод:             {booking['promo_code']}")
        print(f"ИТОГОВАЯ СТОИМОСТЬ:   {booking['total_price']} руб.")

    except ValueError as e:
        print(f"\nОшибка ввода: {e}")
    except Exception as e:
        print(f"\nНеожиданная ошибка: {e}")


if __name__ == "__main__":
    main()