# E-commerce Backend API with .NET 6 and C#
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
cd HiLoAPI
```

3. Create the required images, create the network and run the containers
``` bash
docker-compose up
```

4. You can access the application on http://localhost:8080/swagger/index.html

## API Endpoints

The API has the following endpoints:

### Game
- **POST /api/games**: Start a new game.
- **POST /api/games/{id}/restart**: Restart a finished game.
- **POST /api/games/{id}/guess**: Take a guess in an existing game.
- **GET /api/games/{id}**: Retrieve information about a specific game.
- **GET /api/games**: Retrieve information about all games.

### Players
- **POST /api/players**: Create a new player.
- **GET /api/players/{id}**: Retrieve information about a specific player.
- **GET /api/players**: Retrieve information about all players.

## Request & Response Format
- ### **POST /api/games**

**Request**

Content-Type: application/json
```json
{
  "minValue": 1,
  "maxValue": 10,
  "players": [
    1, 2
  ]
}
```

**Response**

Content-Type: application/json
```json
{
  "minValue": 1,
  "maxValue": 10,
  "id": 1,
  "players": [
    {
      "id": 1,
      "name": "player 1"
    },
    {
      "id": 2,
      "name": "player 2"
    }
  ]
}
```
- ### **GET /api/games**

**Request**

No parameters

**Response**

Content-Type: application/json
```json
[
  {
    "minValue": 1,
    "maxValue": 10,
    "id": 1,
    "round": 1,
    "status": "Ongoing",
    "gamePlayerInfos": [
      {
        "gameId": 1,
        "playerId": 1,
        "playerName": "player 1",
        "attempts": 0,
        "winner": false
      },
      {
        "gameId": 1,
        "playerId": 2,
        "playerName": "player 2",
        "attempts": 0,
        "winner": false
      }
    ]
  }
]
```
- ### **POST /api/games/{id}/restart**

**Request**

Content-Type: path
```
/api/games/1/restart
```

**Response**

Content-Type: application/json
```json
{
  "minValue": 1,
  "maxValue": 10,
  "id": 2,
  "players": [
    {
      "id": 1,
      "name": "player 1"
    },
    {
      "id": 2,
      "name": "player 2"
    }
  ]
}
```
- ### **POST /api/games/{id}/guess**

**Request**

Content-Type: path
```
/api/games/1/guess
```

Content-Type: application/json
```json
{
  "playerId": 1,
  "guess": 5
}
```

**Response**

Content-Type: application/json
```json
{
  "gameId": 1,
  "playerId": 1,
  "playerName": "player 1",
  "attempts": 1,
  "result": "LO",
  "gameStatus": "Ongoing"
}
```
- ### **GET /api/games/{id}**

**Request**

Content-Type: path
```
/api/games/1
```

**Response**

Content-Type: application/json
```json
{
  "id": 1,
  "round": 1,
  "status": "Ongoing",
  "gamePlayerInfos": [
    {
      "gameId": 1,
      "playerId": 1,
      "playerName": "player 1",
      "attempts": 1,
      "winner": false
    },
    {
      "gameId": 1,
      "playerId": 2,
      "playerName": "player 2",
      "attempts": 0,
      "winner": false
    }
  ],
  "minValue": 1,
  "maxValue": 10
}
```
- ### **POST /api/players**

**Request**

Content-Type: application/json
```json
"player 1"
```

**Response**

Content-Type: application/json
```json
{
  "id": 1,
  "name": "player 1",
  "gamesPlayed": 0,
  "wins": 0
}
```
- ### **GET /api/players**

**Request**

No parameters

**Response**

Content-Type: application/json
```json
[
  {
    "id": 1,
    "name": "player 1",
    "gamesPlayed": 0,
    "wins": 0
  },
  {
    "id": 2,
    "name": "player 2",
    "gamesPlayed": 0,
    "wins": 0
  },
]
```
- ### **GET /api/players/{id}**

**Request**

Content-Type: path
```
/api/players/1
```

**Response**

Content-Type: application/json
```json
{
  "id": 1,
  "name": "player 1",
  "gamesPlayed": 0,
  "wins": 0
}
```

## Game Sequence
Here is an example of a game sequence.

![image](https://user-images.githubusercontent.com/32825344/217408108-9e7c2b36-af2a-4f9c-929c-9e5fed33a27c.png)


## Built with
.NET 6

## Author
[João Borges Coelho](https://github.com/JoaoBCoelho)
