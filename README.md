# ASP.NET Core Shared Kernel

This repository provides a **shared kernel** for ASP.NET Core projects. It contains common dependencies, utilities, and abstractions that are frequently used across multiple solutions. By centralizing these components, you can avoid code duplication and streamline development in your .NET ecosystem.

## Purpose

- **Reusability:** Share common code (DTOs, interfaces, services, extensions, etc.) between different ASP.NET Core projects.
- **Maintainability:** Update shared logic in one place instead of copying and pasting code across repositories.
- **Consistency:** Ensure consistent implementation of cross-cutting concerns (e.g., logging, authorization, validation) throughout your applications.

## Structure

The project is organized into several folders, including:

- `Brokers/` – Abstractions for external systems and infrastructure.
- `Databases/` – Common database-related logic.
- `Domain/` – Shared domain models, enums, exceptions, and interfaces.
- `Extensions/` – Extension methods and cross-cutting utilities (authorization, logging, mapping, etc.).
- `Minios/` – MinIO-related models and services.
- `Protobufs/` – Shared Protobuf definitions for gRPC communication.
- `Redis/` – Redis-related services and models.
- `Utilities/` – General-purpose utilities and validators.

## Usage

1. **Reference this project** in your ASP.NET Core solutions as a shared dependency.
2. **Use the provided abstractions and utilities** to implement common functionality without duplicating code.
3. **Contribute improvements** to the shared kernel as your projects evolve.

## Getting Started

To add this shared kernel to your solution:

1. Clone the repository:
   ```sh
   git clone https://github.com/SlavaKuntsov/aspnetcore-shared-kernel.git
   ```
2. Add a project reference to your ASP.NET Core projects.

## Contributing

Contributions are welcome! Please open issues or submit pull requests for improvements, bug fixes, or new shared components.

---

**Note:** This project is intended to be used as a foundational dependency for ASP.NET Core solutions. It is not a standalone application.