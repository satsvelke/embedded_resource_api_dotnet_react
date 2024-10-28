# API Project with Embedded React.js UI (Chakra UI)

This is a sample .NET Core API project demonstrating how to serve an embedded React.js UI (built with Chakra UI) alongside API endpoints. The project shows how to integrate a front-end application as an embedded resource, allowing the UI to be accessed without a separate server.

## Features

- **ASP.NET Core API** for backend functionality.
- **Embedded React.js UI** built with Chakra UI or whatever ui framework, served as static files.
- **Static File Handling** with embedded resources, allowing seamless access to front-end assets.

## Setup Instructions

1. **Build the React App:**
   Ensure the production build of your React app (located in the `ui` folder) is created:

   ```bash
   cd ui
   npm install
   npm run build
   ```

   This will create a `build` folder with the production-ready files. Move or rename this folder to `ui`, so it aligns with the project’s embedded resource setup.

2. **Configure the Project:**
   Open the `.csproj` file and make sure the `ui` folder is set as an embedded resource:

   ```xml
   <ItemGroup>
       <EmbeddedResource Include="ui\**" />
   </ItemGroup>
   ```

3. **Run the Project:**
   Go back to the project root and run the application:

   ```bash
   dotnet run
   ```

   The API and UI will be served on the specified port (default: `http://localhost:5244`).

## Usage

- Access the **React UI** at:
  ```
  http://localhost:5244/ui/index.html
  ```


## Project Structure

- `Program.cs`: Configures API routes, static file handling, and embedded resources.
- `ui/`: Contains the production build of the React.js app.
- `WeatherForecast.cs`: Sample API endpoint to demonstrate data responses.

## URL Rewrite Handling

The project uses a URL rewrite rule to handle references to assets directly. Requests for paths like `/assets/...` are automatically rewritten to `/ui/assets/...`.

## Dependencies

- **ASP.NET Core**
- **React.js**
- **Microsoft.AspNetCore.Rewrite** for URL rewrite rules
```
