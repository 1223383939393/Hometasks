import pytest
import sys
sys.path.insert(0, '..')

from src.booking_service import BookingService


@pytest.fixture
def booking_service():
    return BookingService()


class TestBookingServiceValidation:
    def test_valid_input_creates_booking(self, booking_service):
        booking = booking_service.create_booking(
            base_price=500,
            age=30,
            is_weekend=False,
            promo_code=None
        )

        assert booking["id"] == 1
        assert booking["base_price"] == 500
        assert booking["total_price"] == 500

    def test_negative_base_price_raises_error(self, booking_service):
        with pytest.raises(ValueError, match="base_price должен быть > 0"):
            booking_service.create_booking(
                base_price=-100,
                age=25,
                is_weekend=False,
                promo_code=None
            )

    def test_zero_base_price_raises_error(self, booking_service):
        with pytest.raises(ValueError, match="base_price должен быть > 0"):
            booking_service.create_booking(
                base_price=0,
                age=25,
                is_weekend=False,
                promo_code=None
            )

    def test_negative_age_raises_error(self, booking_service):
        with pytest.raises(ValueError, match="age не может быть отрицательным"):
            booking_service.create_booking(
                base_price=500,
                age=-5,
                is_weekend=False,
                promo_code=None
            )

    def test_string_base_price_raises_error(self, booking_service):
        with pytest.raises(ValueError, match="base_price должен быть int"):
            booking_service.create_booking(
                base_price="500",
                age=25,
                is_weekend=False,
                promo_code=None
            )

    def test_string_age_raises_error(self, booking_service):
        with pytest.raises(ValueError, match="age должен быть int"):
            booking_service.create_booking(
                base_price=500,
                age="25",
                is_weekend=False,
                promo_code=None
            )

    def test_string_is_weekend_raises_error(self, booking_service):
        with pytest.raises(ValueError, match="is_weekend должен быть bool"):
            booking_service.create_booking(
                base_price=500,
                age=25,
                is_weekend="True",
                promo_code=None
            )


class TestIntegrationDataFlow:
    def test_data_flow_from_validation_to_pricing(self, booking_service):
        booking = booking_service.create_booking(
            base_price=1000,
            age=17,
            is_weekend=True,
            promo_code="STUDENT2026"
        )

        # Ожидаем: 1000 * 0.8 = 800, 800 + 100 = 900
        assert booking["total_price"] == 900
        assert booking["age"] == 17
        assert booking["is_weekend"] is True

    def test_promo_code_not_ignored_for_adult(self, booking_service):
        booking = booking_service.create_booking(
            base_price=1000,
            age=25,
            is_weekend=False,
            promo_code="STUDENT2026"
        )

        # Ожидаем: 1000 * 0.85 = 850
        assert booking["total_price"] == 850

    def test_multiple_bookings_increment_id(self, booking_service):
        booking1 = booking_service.create_booking(500, 30, False, None)
        booking2 = booking_service.create_booking(600, 30, False, None)
        booking3 = booking_service.create_booking(700, 30, False, None)

        assert booking1["id"] == 1
        assert booking2["id"] == 2
        assert booking3["id"] == 3


class TestBookingRetrieval:
    def test_get_existing_booking(self, booking_service):
        booking_service.create_booking(500, 30, False, None)
        booking = booking_service.get_booking(1)

        assert booking is not None
        assert booking["id"] == 1
        assert booking["base_price"] == 500

    def test_get_nonexistent_booking_returns_none(self, booking_service):
        booking = booking_service.get_booking(999)
        assert booking is None