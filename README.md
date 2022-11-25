# Authentication Api
### Using ASP.NET 6.0
##### Requests:
1. /api/user/register
Username - string
Password - string
Creates a new user, create a salt and hash the password with it.
Strong Password basic validation - 8 digits with at least one letter and number

2. /api/user/login <br />
Username - string <br />
Password - string <br />
Verify the user details <br />
Returns a json web token

##### Users DB - Postgres
Id - auto generate
Username - string
Password - byte[]
Salt - byte[]

##### Environment variables
DB_URI - Postgres database connection string
TOKEN - Token key to create and verify the token

##### Docker
Run example:
docker run -e DB_URI=Server=host.docker.internal;Database=postgres;Port=5432;UserId=user;Password=mysecretpassword -e TOKEN=testingtoken12345678 -p 8081:80 -d authentication-container
