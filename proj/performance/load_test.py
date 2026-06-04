"""
Скрипт для тестирования производительности.
Эмулирует 500 одновременных запросов к функции расчёта за 10 секунд.
"""

import time
import random
import statistics
from concurrent.futures import ThreadPoolExecutor, as_completed
import sys
import os
import json

# Текущая папка (performance)
CURRENT_DIR = os.path.dirname(__file__)
# Корень проекта (proj)
PROJECT_ROOT = os.path.abspath(os.path.join(CURRENT_DIR, ".."))

if PROJECT_ROOT not in sys.path:
    sys.path.insert(0, PROJECT_ROOT)

from src.pricing import calculate_total


def make_request(request_id: int) -> dict:
    """
    Эмулирует один запрос к функции расчёта.
    """
    # Генерируем случайные параметры для реалистичности
    base_price = random.choice([200, 300, 500, 700, 1000, 1500])
    age = random.choice([10, 17, 18, 25, 30, 64, 65, 70, 80])
    is_weekend = random.choice([True, False])
    promo_code = random.choice([None, "STUDENT2026", "INVALID2024"])

    start_time = time.perf_counter()

    try:
        result = calculate_total(base_price, age, is_weekend, promo_code)
        success = True
        error = None
    except Exception as e:
        success = False
        error = str(e)
        result = None

    end_time = time.perf_counter()
    response_time_ms = (end_time - start_time) * 1000

    return {
        "request_id": request_id,
        "success": success,
        "response_time_ms": response_time_ms,
        "result": result,
        "error": error
    }


def run_load_test(num_requests: int = 500, duration_seconds: int = 10):
    """
    Запускает нагрузочное тестирование.
    """
    print("=" * 60)
    print("НАГРУЗОЧНОЕ ТЕСТИРОВАНИЕ: Умный калькулятор бронирования")
    print("=" * 60)
    print(f"Количество запросов: {num_requests}")
    print(f"Длительность: {duration_seconds} секунд")
    print(f"Целевая скорость: {num_requests / duration_seconds:.1f} requests/sec")
    print("=" * 60)
    print()

    start_time = time.time()
    results = []

    with ThreadPoolExecutor(max_workers=50) as executor:
        futures = [
            executor.submit(make_request, i)
            for i in range(num_requests)
        ]

        for future in as_completed(futures):
            results.append(future.result())

    end_time = time.time()
    actual_duration = end_time - start_time

    # Анализируем результаты
    successful_results = [r for r in results if r["success"]]
    failed_results = [r for r in results if not r["success"]]

    response_times = [r["response_time_ms"] for r in successful_results]

    # Вычисляем метрики
    avg_response_time = statistics.mean(response_times) if response_times else 0
    median_response_time = statistics.median(response_times) if response_times else 0
    p95_response_time = statistics.quantiles(response_times, n=20)[18] if len(response_times) >= 20 else max(response_times) if response_times else 0
    min_response_time = min(response_times) if response_times else 0
    max_response_time = max(response_times) if response_times else 0

    success_rate = (len(successful_results) / num_requests) * 100 if num_requests > 0 else 0
    actual_rps = num_requests / actual_duration if actual_duration > 0 else 0

    # Вывод результатов
    print("РЕЗУЛЬТАТЫ ТЕСТИРОВАНИЯ")
    print("=" * 60)
    print(f"Общее количество запросов:     {num_requests}")
    print(f"Успешных запросов:             {len(successful_results)}")
    print(f"Ошибок:                        {len(failed_results)}")
    print(f"Процент успеха:                {success_rate:.2f}%")
    print()
    print("ВРЕМЯ ОТКЛИКА:")
    print(f"  Среднее время:               {avg_response_time:.2f} мс")
    print(f"  Медиана:                     {median_response_time:.2f} мс")
    print(f"  95-й перцентиль:             {p95_response_time:.2f} мс")
    print(f"  Минимальное время:           {min_response_time:.2f} мс")
    print(f"  Максимальное время:          {max_response_time:.2f} мс")
    print()
    print("ПРОИЗВОДИТЕЛЬНОСТЬ:")
    print(f"  Фактическая длительность:    {actual_duration:.2f} сек")
    print(f"  Реальная скорость (RPS):     {actual_rps:.2f} req/sec")
    print(f"  Целевая скорость:            {num_requests / duration_seconds:.2f} req/sec")
    print()

    if failed_results:
        print("ОШИБКИ:")
        error_counts = {}
        for r in failed_results:
            error_msg = r["error"] or "Unknown error"
            error_counts[error_msg] = error_counts.get(error_msg, 0) + 1

        for error, count in error_counts.items():
            print(f"  {error}: {count} раз(а)")
        print()

    print("=" * 60)

    # Сохраняем метрики в файл для отчёта
    metrics = {
        "total_requests": num_requests,
        "successful_requests": len(successful_results),
        "error_count": len(failed_results),
        "success_rate_percent": success_rate,
        "avg_response_time_ms": round(avg_response_time, 2),
        "median_response_time_ms": round(median_response_time, 2),
        "p95_response_time_ms": round(p95_response_time, 2),
        "min_response_time_ms": round(min_response_time, 2),
        "max_response_time_ms": round(max_response_time, 2),
        "actual_rps": round(actual_rps, 2),
        "actual_duration_sec": round(actual_duration, 2)
    }

    metrics_path = os.path.join(CURRENT_DIR, "metrics.json")
    with open(metrics_path, "w", encoding="utf-8") as f:
        json.dump(metrics, f, indent=2, ensure_ascii=False)

    print(f"Метрики сохранены в {metrics_path}")
    print("=" * 60)

    return metrics


if __name__ == "__main__":
    metrics = run_load_test(num_requests=500, duration_seconds=10)

    # Быстрый вывод ключевых метрик
    print("\n📊 КЛЮЧЕВЫЕ МЕТРИКИ (для отчёта):")
    print(f"  Среднее время отклика:       {metrics['avg_response_time_ms']:.2f} мс")
    print(f"  95-й перцентиль:             {metrics['p95_response_time_ms']:.2f} мс")
    print(f"  Количество ошибок:           {metrics['error_count']}")