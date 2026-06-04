def calculate_total(base_price: int, age: int, is_weekend: bool, promo_code: str | None) -> int:
    if base_price <= 0:
        raise ValueError("Базовая цена должна быть строго больше 0")

    price = base_price
    has_age_discount = age < 18 or age >= 65

    if has_age_discount:
        price = price * 0.8
    else:
        if promo_code == "STUDENT2026":
            price = price * 0.85

    if is_weekend:
        price = price + 100

    price = max(price, 50)

    return int(round(price))