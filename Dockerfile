# Use the official .NET SDK image as the base image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the project files to the container
COPY EmployeManagementSystemWidthMongoDb.Api/ ./
RUN dotnet restore

# Copy the rest of the application code to the container

COPY EmployeManagementSystemWidthMongoDb.Api/*.csproj ./

# Build the application
RUN dotnet publish -c Release -o out

# Create a runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

# Set the working directory inside the container
WORKDIR /app

# Copy the build output from the previous stage to the container
COPY --from=build /app/out .

# Expose the port your application listens on
EXPOSE 7704

# Start your application
ENTRYPOINT ["dotnet", "ems_mongodb.api.dll"]
