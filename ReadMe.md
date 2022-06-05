# Sample App

Sample application demonstrating a Swagger REST API, caching, logging, persistence (using Docker), databaes migrations and calling third-party resources over REST.

# Usage

- Navigate to the Docker/sample-dev folder and run bring up the database

```bash
cd Docker/sample-dev
docker compose up -d
```

This will start a Postgres DB on port 54345

- Signup for a free API key at [IP Stack](https://ipstack.com/)

- Add your IP Stack API Key to the Environment Variable `SAMPLE_IPLOOKUPAPIKEY`. **Don't Forget** After adding an environment variable you may need to reset your shell to pick it up.

- Start the API by using `dotnet run` in the `GeoAPI` directory.

- Using your preferred browser navigate to `https://localhost:7164/swagger/index.html` and click some buttons