# E-commerce API with .NET 6 and C#
## Introduction
The E-Commerce API provides endpoints for managing a shopping cart, products, categories, and orders. It allows users to perform actions such as adding products to a cart, creating new carts, getting product information, placing orders, and more.

## API Version
The current version of the API is v1.

## Base URL
The base URL for all API endpoints is /api/v1.

## Authentication (Pending)
Authentication is required to access the API endpoints. Please refer to the authentication documentation for more information on how to authenticate your requests.

## Error Handling
The API follows standard HTTP status codes to indicate the success or failure of a request. In case of an error, additional error information may be included in the response body.

## Getting started
These instructions will help you set up a local instance of the API.

### Prerequisites
- Docker

### Installing
1. Clone the repository to your local machine.
``` bash
git clone https://github.com/JoaoBCoelho/EcommerceAPI
```

2. Navigate to the cloned repository folder.
``` bash
cd EcommerceAPI
```

3. Create the required images, create the network and run the containers
``` bash
docker compose up
```

4. You can access the application on http://localhost:8080/swagger/index.html

## API Endpoints

The API has the following endpoints:

### Cart
- **GET /api/v1/cart/{id}**: Get cart by ID.
- **POST /api/v1/cart**: Create a new cart.
- **POST /api/v1/cart/{id}/checkout**: Checkout cart.
- **POST /api/v1/cart/{id}/product/{productId}**: Add product to cart.
- **PUT /api/v1/cart/{id}/product/{productId}**: Update product in cart.
- **DELETE /api/v1/cart/{id}/product/{productId}**: Remove product from cart.
### Category
- **GET /api/v1/category**: Get all categories.
### Order
- **GET /api/v1/order/{id}**: Get order by ID.
### Product
- **GET /api/v1/product**: Get product by filter.
- **POST /api/v1/product**: Create new product.
- **GET /api/v1/product/{id}**: Get product by ID.
- **POST /api/v1/product/{id}/related/{relatedProductId}**: Add related product.
- **DELETE /api/v1/product/{id}/related/{relatedProductId}**: Remove related product.

## Built with
.NET 6

## Author
[João Borges Coelho](https://github.com/JoaoBCoelho)
