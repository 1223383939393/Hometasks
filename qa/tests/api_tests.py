import requests
import json

BASE_URL = "https://jsonplaceholder.typicode.com"


def safe_json(response):
    try:
        return response.json()
    except Exception:
        return {"raw": response.text}


def test_create():
    """Test creating a resource (analog of creating an order)."""
    data = {
        "title": "New SmartDelivery order",
        "body": "Test order for SmartDelivery docs",
        "userId": 1
    }
    response = requests.post(f"{BASE_URL}/posts", json=data)
    print("=== POST /posts ===")
    print("Status:", response.status_code)
    print("Response:")
    print(json.dumps(safe_json(response), indent=2))
    return response


def test_get(item_id: int):
    """Test reading a resource."""
    response = requests.get(f"{BASE_URL}/posts/{item_id}")
    print("\n=== GET /posts/{id} ===")
    print("Request id:", item_id)
    print("Status:", response.status_code)
    print("Response:")
    print(json.dumps(safe_json(response), indent=2))
    return response


def test_update(item_id: int):
    """Test updating a resource."""
    data = {
        "id": item_id,
        "title": "Updated SmartDelivery order",
        "body": "Updated order data",
        "userId": 1
    }
    response = requests.put(f"{BASE_URL}/posts/{item_id}", json=data)
    print("\n=== PUT /posts/{id} ===")
    print("Request id:", item_id)
    print("Status:", response.status_code)
    print("Response:")
    print(json.dumps(safe_json(response), indent=2))
    return response


def test_delete(item_id: int):
    """Test deleting a resource."""
    response = requests.delete(f"{BASE_URL}/posts/{item_id}")
    print("\n=== DELETE /posts/{id} ===")
    print("Request id:", item_id)
    print("Status:", response.status_code)
    print("Response:", response.text if response.text else "No content")
    return response.status_code


if __name__ == "__main__":
    print("=== SmartDelivery API tests (JSONPlaceholder) ===\n")

    # Use a stable id for GET/PUT/DELETE
    stable_id = 1

    # Create (id from response not guaranteed to be persisted)
    created_response = test_create()

    # Read / Update / Delete an existing resource
    test_get(stable_id)
    test_update(stable_id)
    test_delete(stable_id)