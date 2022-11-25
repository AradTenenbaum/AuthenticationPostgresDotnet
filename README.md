# Authentication Api
### Using ASP.NET 6.0
##### Requests:
1. POST: /api/user/register <br />
Username - string <br />
Password - string <br />
Creates a new user, create a salt and hash the password with it. <br />
Strong Password basic validation - 8 digits with at least one letter and number

2. POST: /api/user/login <br />
Username - string <br />
Password - string <br />
Verify the user details <br />
Returns a json web token

##### Users DB - Postgres
Id - auto generate <br />
Username - string <br />
Password - byte[] <br />
Salt - byte[] 

##### Environment variables
DB_URI - Postgres database connection string <br />
TOKEN - Token key to create and verify the token <br />

##### Docker
Run example: <br />
docker run -e DB_URI=Server=host.docker.internal;Database=postgres;Port=5432;UserId=user;Password=mysecretpassword -e TOKEN=testingtoken12345678 -p 8081:80 -d authentication-container
