# AddressBook
Address book microservice using Razor Pages and ASP.NET Core

Razor Pages is used for easy CRUD manipulation on a Database Context (SQL in this example)
![Alt text](/Media/addresses_page.png?raw=true "")

Exposed API endpoints can be viewed and tested via /swagger
![Alt text](/Media/swagger_page.png?raw=true "")

Example of a GET response to /api/Addresses/AddressesByCity
```
[
  [
    {
      "id": 1,
      "firstName": "John",
      "lastName": "Smith",
      "streetAddress": "Test St 1",
      "city": "London",
      "country": "England"
    },
    {
      "id": 3,
      "firstName": "Jane",
      "lastName": "Doe",
      "streetAddress": "Test St 2",
      "city": "london",
      "country": "England"
    }
  ],
  [
    {
      "id": 2,
      "firstName": "Tim",
      "lastName": "Jones",
      "streetAddress": "Test St 3",
      "city": "New York",
      "country": "United States"
    }
  ],
  [
    {
      "id": 1002,
      "firstName": "Johnny",
      "lastName": "Test",
      "streetAddress": "123 Test St",
      "city": "New York",
      "country": "Florida"
    }
  ]
]
```