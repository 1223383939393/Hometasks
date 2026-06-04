from typing import TypedDict
from .pricing import calculate_total


class BookingInput(TypedDict):
    base_price: int
    age: int
    is_weekend: bool
    promo_code: str | None


class BookingService:
    def __init__(self):
        self._db_bookings = []

    def validate_input(self, base_price: int, age: int, is_weekend: bool, promo_code: str | None) -> BookingInput:
        if not isinstance(base_price, int):
            raise ValueError(f"base_price должен быть int, получен {type(base_price).__name__}")
        if base_price <= 0:
            raise ValueError(f"base_price должен быть > 0, получено {base_price}")

        if not isinstance(age, int):
            raise ValueError(f"age должен быть int, получен {type(age).__name__}")
        if age < 0:
            raise ValueError(f"age не может быть отрицательным, получено {age}")

        if not isinstance(is_weekend, bool):
            raise ValueError(f"is_weekend должен быть bool, получен {type(is_weekend).__name__}")

        if promo_code is not None and not isinstance(promo_code, str):
            raise ValueError(f"promo_code должен быть str или None, получен {type(promo_code).__name__}")

        return {
            "base_price": base_price,
            "age": age,
            "is_weekend": is_weekend,
            "promo_code": promo_code
        }

    def create_booking(self, base_price: int, age: int, is_weekend: bool, promo_code: str | None) -> dict:
        validated_input = self.validate_input(base_price, age, is_weekend, promo_code)

        total_price = calculate_total(
            validated_input["base_price"],
            validated_input["age"],
            validated_input["is_weekend"],
            validated_input["promo_code"]
        )

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
        for booking in self._db_bookings:
            if booking["id"] == booking_id:
                return booking
        return None