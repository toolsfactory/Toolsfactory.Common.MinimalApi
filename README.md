# Toolsfactory.Common.MinimalApi

This project provides a collection of helper classes and extensions for developing Minimal APIs with .NET 9.

## Features

- Base classes and interfaces for defining endpoints
- Extension methods for `IServiceCollection` and `IEndpointRouteBuilder`
- Support for modular and structured API development


## Usage

Example for registering and defining endpoints:


```csharp
// In Program.cs 
builder.Services.AddEndpointDefinitions(typeof(Program).Assembly);
app.UseEndpointDefinitions();
```

```csharp
// In an endpoint definition class
public class MyEndpointDefinition : EndpointDefinition
{
	public override void DefineEndpoints(IEndpointRouteBuilder app)
	{
		app.MapGet("/hello", () => "Hello World!");
	}
}
```