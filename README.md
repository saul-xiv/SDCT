
# Santander - Developer Coding Test

RESTful API to retrieve the details of the best n stories from the Hacker News API, as determined by their score, where n is specified by the caller to the API.

#### Assumptions 

- It doesn't need security
- User always is going to put a qty between 1 and 500
- Response data types won't change

## API Reference

#### Get stories

#### Testing host https://testcodechall.azurewebsites.net/

```http
  GET /api/Stories/${qty}
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `qty` | `int` | **Required**. Quantity of stories to fetch |


Returns the stories and details in the following format.
```json
{
    "title": "Run CUDA, unmodified, on AMD GPUs",
    "uri": "https://docs.scale-lang.com/",
    "postedBy": "Straw",
    "time": "2024-07-15T19:05:07",
    "score": 1276,
    "commentCount": 389
}
```



## Run Locally

Clone the project

```bash
  git clone https://github.com/saul-xiv/SDCT.git
```

Go to the project directory

```bash
  cd SDCT/SDCT
```

Install dependencies

```bash
  dotnet restore
```

Build the project

```bash
  dotnet build
```

Run the project

```bash
    dotnet run
```

## Roadmap

- Implement OAuth2 or JWT to secure endpoints.

- Configure roles and permissions to control access to different parts of the API.

- Implement rate limiting policies to prevent abuse.

- Configure the API to run on multiple instances and load balance between them.

- Change to a distributed caching system like Redis to improve performance and scalability.
