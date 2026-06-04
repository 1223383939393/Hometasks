"""
Интеграционный слой для бронирования.
Имитирует работу с БД и валидирует входные данные.
"""

from typing import TypedDict
from .pricing import calculate_total


class BookingInput(TypedDict):
    """Тип для входных данных бронирования"""
    base_price: int
    age: int
    is_weekend: bool
    promo_code: str | None


class BookingService:
    """Сервис бронирования с валидацией и расчётом стоимости"""
    
    def __init__(self):
        # Имитация базы данных билетов
        self._db_bookings = []
    
    def validate_input(self, base_price: int, age: int, is_weekend: bool, promo_code: str | None) -> BookingInput:
        """
        Валидирует входные данные перед расчётом.
        
        Raises:
            ValueError: Если данные некорректны
        """
        # Валидация типа и значения base_price
        if not isinstance(base_price, int):
            raise ValueError(f"base_price должен быть int, получен {type(base_price).__name__}")
        if base_price <= 0:
            raise ValueError(f"base_price должен быть > 0, получено {base_price}")
        
        # Валидация типа и значения age
        if not isinstance(age, int):
            raise ValueError(f"age должен быть int, получен {type(age).__name__}")
        if age < 0:
            raise ValueError(f"age не может быть отрицательным, получено {age}")
        
        # Валидация is_weekend
        if not isinstance(is_weekend, bool):
            raise ValueError(f"is_weekend должен быть bool, получен {type(is_weekend).__name__}")
        
        # Валидация promo_code
        if promo_code is not None and not isinstance(promo_code, str):
            raise ValueError(f"promo_code должен быть str или None, получен {type(promo_code).__name__}")
        
        return {
            "base_price": base_price,
            "age": age,
            "is_weekend": is_weekend,
            "promo_code": promo_code
        }
    
    def create_booking(self, base_price: int, age: int, is_weekend: bool, promo_code: str | None) -> dict:
        """
        Создаёт бронирование с расчётом стоимости.
        
        Args:
            base_price: Базовая цена билета
            age: Возраст покупателя
            is_weekend: Является ли день выходным
            promo_code: Промокод или None
            
        Returns:
            Словарь с данными бронирования
            
        Raises:
            ValueError: Если входные данные некорректны
        """
        # Валидация входных данных
        validated_input = self.validate_input(base_price, age, is_weekend, promo_code)
        
        # Расчёт стоимости через pricing слой
        total_price = calculate_total(
            validated_input["base_price"],
            validated_input["age"],
            validated_input["is_weekend"],
            validated_input["promo_code"]
        )
        
        # Создание записи бронирования (имитация БД)
        booking = {
            "id": len(self._db_bookings) + 1,
            "base_price": validated_input["base_price"],
            "age": validated_input["age"],
            "is_weekend": validated_input["is_weekend"],
            "promo_code": validated_input["promo_code"],
            "total_price": total_price
        }
        
        self._db_bookings.append(booking)
        
        return booking
    
    def get_booking(self, booking_id: int) -> dict | None:
        """Получает бронирование по ID"""
        for booking in self._db_bookings:
            if booking["id"] == booking_id:
                return booking
        return None