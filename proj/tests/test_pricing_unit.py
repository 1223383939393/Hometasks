import pytest
import sys
sys.path.insert(0, '..')

from src.pricing import calculate_total


class TestCalculateTotalBasePrice:
    def test_base_price_zero_raises_error(self):
        with pytest.raises(ValueError, match="Базовая цена должна быть строго больше 0"):
            calculate_total(0, 25, False, None)

    def test_base_price_negative_raises_error(self):
        with pytest.raises(ValueError, match="Базовая цена должна быть строго больше 0"):
            calculate_total(-100, 25, False, None)

    def test_base_price_positive_valid(self):
        result = calculate_total(500, 25, False, None)
        assert result == 500


class TestAgeDiscount:
    def test_age_under_18_gets_20_percent_discount(self):
        # 500 * 0.8 = 400
        result = calculate_total(500, 17, False, None)
        assert result == 400

    def test_age_exactly_18_no_discount(self):
        result = calculate_total(500, 18, False, None)
        assert result == 500

    def test_age_64_no_discount(self):
        result = calculate_total(500, 64, False, None)
        assert result == 500

    def test_age_65_gets_20_percent_discount(self):
        # 500 * 0.8 = 400
        result = calculate_total(500, 65, False, None)
        assert result == 400

    def test_age_80_gets_20_percent_discount(self):
        result = calculate_total(500, 80, False, None)
        assert result == 400


class TestPromoCode:
    def test_promo_code_student2026_without_age_discount(self):
        # 500 * 0.85 = 425
        result = calculate_total(500, 25, False, "STUDENT2026")
        assert result == 425

    def test_promo_code_with_age_discount_ignored(self):
        # С возрастной скидкой: 500 * 0.8 = 400
        # Промокод ИГНОРИРУЕТСЯ, не 400 * 0.85 = 340!
        result = calculate_total(500, 17, False, "STUDENT2026")
        assert result == 400  # Только возрастная скидка

    def test_invalid_promo_code_ignored(self):
        result = calculate_total(500, 25, False, "INVALID2024")
        assert result == 500

    def test_none_promo_code_ignored(self):
        result = calculate_total(500, 25, False, None)
        assert result == 500


class TestWeekendSurcharge:
    def test_weekend_adds_100_rubles(self):
        # 500 + 100 = 600
        result = calculate_total(500, 25, True, None)
        assert result == 600

    def test_weekend_with_age_discount(self):
        # 500 * 0.8 = 400, 400 + 100 = 500
        result = calculate_total(500, 17, True, None)
        assert result == 500

    def test_weekend_with_promo_code(self):
        # 500 * 0.85 = 425, 425 + 100 = 525
        result = calculate_total(500, 25, True, "STUDENT2026")
        assert result == 525


class TestMinimumPrice:
    def test_price_below_minimum_rounds_to_50(self):
        # 100 * 0.8 = 80, 80 + 100 = 180 - выше минимума
        # Тестируем с маленькой базой: 50 * 0.8 = 40 -> должно стать 50
        result = calculate_total(50, 17, False, None)
        assert result == 50  # 40 округляется до 50

    def test_price_above_minimum_kept(self):
        result = calculate_total(100, 17, False, None)
        assert result == 80  # 100 * 0.8 = 80 > 50, оставляем

    def test_very_low_price_with_weekend(self):
        # 60 * 0.8 = 48, 48 + 100 = 148 > 50
        result = calculate_total(60, 17, True, None)
        assert result == 148


class TestCombinedScenarios:
    def test_full_scenario_adult_weekend_no_promo(self):
        # 500 + 100 = 600
        result = calculate_total(500, 30, True, None)
        assert result == 600

    def test_full_scenario_child_weekend_no_promo(self):
        # 500 * 0.8 = 400, 400 + 100 = 500
        result = calculate_total(500, 15, True, None)
        assert result == 500

    def test_full_scenario_adult_weekend_with_promo(self):
        # 500 * 0.85 = 425, 425 + 100 = 525
        result = calculate_total(500, 30, True, "STUDENT2026")
        assert result == 525

    def test_full_scenario_senior_no_weekend_promo_ignored(self):
        # 500 * 0.8 = 400 (промокод игнорируется)
        result = calculate_total(500, 70, False, "STUDENT2026")
        assert result == 400

    def test_order_matters_weekend_after_discounts(self):
        # Правильно: 1000 * 0.8 = 800, 800 + 100 = 900
        # Неправильно (если сначала выходной): 1000 + 100 = 1100, 1100 * 0.8 = 880
        result = calculate_total(1000, 17, True, None)
        assert result == 900


class TestBugDemonstration:
    def test_minimum_price_enforced(self):
        # 40 * 0.8 = 32, должно быть округлено до 50
        result = calculate_total(40, 17, False, None)
        assert result == 50, "Минимальная цена должна быть 50 рублей"