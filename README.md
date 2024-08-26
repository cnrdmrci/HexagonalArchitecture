# Hexagonal Architecture

- [Technologies and Tools Used in This Project](#usage)

- [What is Hexagonal Architecture?](#what-is-hexagonal-arch)

  - [Ports](#ports)
  - [Adapters](#adapters)
  - [Domain](#domain)
  - [Use Case](#use-case)
  - [Infrastructure](#infrastructure)
  - [Driving(Left) Side](#driving-side)
  - [Driven(Right) Side](#driven-side)

- [What is Domain Driven Design?](#domain-driven-design)
  - [Strategic Design](#strategic-design)
    - [Domain Model](#domain-model)
    - [Domain Expert](#domain-expert)
    - [Ubiquitous Language](#ubiquitous-language)
    - [Bounded Context](#bounded-context)
    - [Context Mapping](#context-mapping)
  - [Tactical Design](#tactical-design)
    - [Entity](#entity)
    - [Value Object](#value-object)
    - [Aggregate Root](#aggregate-root)


## <a name="usage">Technologies and Tools Used in This Project</a>

- .Net Core 8
- Entity Framework Core 8
- Swagger
- MediatR
- API Versioning
- Exception Handling
- Fluent Validation
- Mapster
- Testcontainers
- Open Telemetry
  - Prometheus
  - Jaeger
  - Grafana
  - Loki

## <a name="what-is-hexagonal-arch">What is Hexagonal Architecture?</a>

Hexagonal Architecture, also known as Ports and Adapters, is an architectural pattern that aims to create loosely coupled application components, making the system more maintainable and adaptable to change. The core idea is to separate the business logic from external systems (like databases, user interfaces, or APIs) through "ports" (interfaces) and "adapters" (implementations). This allows the application to be easily tested and extended, as changes to external systems do not impact the core business logic.

## You can review the following diagram for a better understanding;

![hexagonal-arch](https://user-images.githubusercontent.com/16361055/149714561-e41132d2-e196-4246-89e0-3ed3703af2ca.png)

- ### <a name="ports">Ports</a>

  Ports are interfaces that define how the application interacts with the outside world.

  - Driving Ports: Handle incoming requests and interactions from external sources.
  - Driven Ports: Define how the application communicates with external systems and services.

- ### <a name="adapters">Adapters</a>

  Adapters are components that implement the ports' interfaces and handle the actual communication between the application and external systems or users. They convert data between the application's internal format and the format required by the external systems.

- ### <a name="domain">Domain</a>

  Domain refers to the core business logic and rules of the application. It is the central part of the architecture, isolated from external concerns and interfaces.

- ### <a name="use-case">Use Case</a>

  Use Case defines a specific interaction or functionality that the application provides, focusing on how the application fulfills user requirements or business processes.

- ### <a name="infrastructure">Infrastructure</a>

  Infrastructure refers to the external systems and technologies that support the application, such as databases, messaging systems, APIs are used to implement driven ports.

- ### <a name="driving-side">Driving(Left) Side</a>

  Driving Side represents the external components or systems that initiate interactions with the application, such as users, external services or other systems that drive the application's behavior through the driving ports.

- ### <a name="driven-side">Driven(Right) Side</a>

  Driven Side consists of the external systems or services that the application interacts with, such as databases or third-party APIs are accessed through driven ports.


## <a name="domain-driven-design">What is Domain Driven Design?</a>

Domain-Driven Design (DDD) focuses on modeling complex software systems by aligning the design with the business domain, using concepts like entities, value objects, aggregates and domain events to create a shared understanding between developers and domain experts.

- ### <a name="strategic-design">Strategic Design</a>

  Strategic design involves defining the boundaries and relationships between different domains or subdomains within a system, using below concepts to ensure coherent and manageable design.

  - #### <a name="domain-model">Domain Model</a>
  - #### <a name="domain-expert">Domain Expert</a>
  - #### <a name="ubiquitous-language">Ubiquitous Language</a>
  - #### <a name="bounded-context">Bounded Context</a>
  - #### <a name="context-mapping">Context Mapping</a>

- ### <a name="tactical-design">Tactical Design</a>

  Tactical design focuses on detailed modeling and implementation within a bounded context, utilizing patterns such as entities, value objects, aggregates and repositories to manage complex business logic effectively.

  - #### <a name="entity">Entity</a>
  - #### <a name="value-object">Value Object</a>
  - #### <a name="aggregate-root">Aggregate Root</a>
