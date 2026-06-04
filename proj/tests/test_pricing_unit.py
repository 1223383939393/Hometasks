"""
Модульные тесты для pricing.py с покрытием 100% ветвлений (branch coverage).
White-box тестирование - тесты написаны с учётом внутренней структуры кода.
"""

import pytest
import sys
sys.path.insert(0, '..')

from src.pricing import calculate_total


class TestCalculateTotalBasePrice:
    """Тесты для валидации базовой цены"""
    
    def test_base_price_zero_raises_error(self):
        """base_price = 0 должен вызывать ошибку"""
        with pytest.raises(ValueError, match="Базовая цена должна быть строго больше 0"):
            calculate_total(0, 25, False, None)
    
    def test_base_price_negative_raises_error(self):
        """Отрицательная base_price должна вызывать ошибку"""
        with pytest.raises(ValueError, match="Базовая цена должна быть строго больше 0"):
            calculate_total(-100, 25, False, None)
    
    def test_base_price_positive_valid(self):
        """Положительная base_price должна быть допустима"""
        result = calculate_total(500, 25, False, None)
        assert result == 500


class TestAgeDiscount:
    """Тесты для возрастной скидки 20%"""
    
    def test_age_under_18_gets_20_percent_discount(self):
        """Возраст < 18 должен получать скидку 20%"""
        # 500 * 0.8 = 400
        result = calculate_total(500, 17, False, None)
        assert result == 400
    
    def test_age_exactly_18_no_discount(self):
        """Возраст 18 НЕ должен получать скидку"""
        result = calculate_total(500, 18, False, None)
        assert result == 500
    
    def test_age_64_no_discount(self):
        """Возраст 64 НЕ должен получать скидку"""
        result = calculate_total(500, 64, False, None)
        assert result == 500
    
    def test_age_65_gets_20_percent_discount(self):
        """Возраст >= 65 должен получать скидку 20%"""
        # 500 * 0.8 = 400
        result = calculate_total(500, 65, False, None)
        assert result == 400
    
    def test_age_80_gets_20_percent_discount(self):
        """Возраст 80 должен получать скидку 20%"""
        result = calculate_total(500, 80, False, None)
        assert result == 400


class TestPromoCode:
    """Тесты для промокода STUDENT2026"""
    
    def test_promo_code_student2026_without_age_discount(self):
        """Промокод без возрастной скидки даёт 15% скидку"""
        # 500 * 0.85 = 425
        result = calculate_total(500, 25, False, "STUDENT2026")
        assert result == 425
    
    def test_promo_code_with_age_discount_ignored(self):
        """Промокод ИГНОРИРУЕТСЯ при возрастной скидке (важное правило!)"""
        # С возрастной скидкой: 500 * 0.8 = 400
        # Промокод ИГНОРИРУЕТСЯ, не 400 * 0.85 = 340!
        result = calculate_total(500, 17, False, "STUDENT2026")
        assert result == 400  # Только возрастная скидка
    
    def test_invalid_promo_code_ignored(self):
        """Невалидный промокод игнорируется"""
        result = calculate_total(500, 25, False, "INVALID2024")
        assert result == 500
    
    def test_none_promo_code_ignored(self):
        """None промокод игнорируется"""
        result = calculate_total(500, 25, False, None)
        assert result == 500


class TestWeekendSurcharge:
    """Тесты для наценки за выходные"""
    
    def test_weekend_adds_100_rubles(self):
        """Выходной добавляет 100 рублей"""
        # 500 + 100 = 600
        result = calculate_total(500, 25, True, None)
        assert result == 600
    
    def test_weekend_with_age_discount(self):
        """Выходной + возрастная скидка"""
        # 500 * 0.8 = 400, 400 + 100 = 500
        result = calculate_total(500, 17, True, None)
        assert result == 500
    
    def test_weekend_with_promo_code(self):
        """Выходной + промокод"""
        # 500 * 0.85 = 425, 425 + 100 = 525
        result = calculate_total(500, 25, True, "STUDENT2026")
        assert result == 525


class TestMinimumPrice:
    """Тесты для минимальной стоимости 50 рублей"""
    
    def test_price_below_minimum_rounds_to_50(self):
        """Цена ниже 50 округляется до 50"""
        # 100 * 0.8 = 80, 80 + 100 = 180 - выше минимума
        # Тестируем с маленькой базой: 50 * 0.8 = 40 -> должно стать 50
        result = calculate_total(50, 17, False, None)
        assert result == 50  # 40 округляется до 50
    
    def test_price_above_minimum_kept(self):
        """Цена выше 50 остаётся без изменений"""
        result = calculate_total(100, 17, False, None)
        assert result == 80  # 100 * 0.8 = 80 > 50, оставляем
    
    def test_very_low_price_with_weekend(self):
        """Очень низкая цена с выходным не падает ниже 50"""
        # 60 * 0.8 = 48, 48 + 100 = 148 > 50
        result = calculate_total(60, 17, True, None)
        assert result == 148


class TestCombinedScenarios:
    """Комбинированные сценарии (все правила вместе)"""
    
    def test_full_scenario_adult_weekend_no_promo(self):
        """Взрослый + выходной + без промокода"""
        # 500 + 100 = 600
        result = calculate_total(500, 30, True, None)
        assert result == 600
    
    def test_full_scenario_child_weekend_no_promo(self):
        """Ребёнок + выходной + без промокода"""
        # 500 * 0.8 = 400, 400 + 100 = 500
        result = calculate_total(500, 15, True, None)
        assert result == 500
    
    def test_full_scenario_adult_weekend_with_promo(self):
        """Взрослый + выходной + промокод"""
        # 500 * 0.85 = 425, 425 + 100 = 525
        result = calculate_total(500, 30, True, "STUDENT2026")
        assert result == 525
    
    def test_full_scenario_senior_no_weekend_promo_ignored(self):
        """Пенсионер + не выходной + промокод (игнорируется)"""
        # 500 * 0.8 = 400 (промокод игнорируется)
        result = calculate_total(500, 70, False, "STUDENT2026")
        assert result == 400
    
    def test_order_matters_weekend_after_discounts(self):
        """Порядок: скидки сначала, потом наценка за выходной"""
        # Правильно: 1000 * 0.8 = 800, 800 + 100 = 900
        # Неправильно (если сначала выходной): 1000 + 100 = 1100, 1100 * 0.8 = 880
        result = calculate_total(1000, 17, True, None)
        assert result == 900


# Тест для демонстрации бага (Шаг А - intentional bug test)
class TestBugDemonstration:
    """Тесты для демонстрации бага (минимальная стоимость)"""
    
    def test_minimum_price_enforced(self):
        """
        Тест, который ПАДАЕТ, если не enforced минимальная цена 50 руб.
        Шаги А-Б: этот тест падает на сломанном коде.
        """
        # 40 * 0.8 = 32, должно быть округлено до 50
        result = calculate_total(40, 17, False, None)
        assert result == 50, "Минимальная цена должна быть 50 рублей"