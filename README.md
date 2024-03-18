# Hexagonal Architecture

- [Projede Kullanılan Teknolojiler](#usage)

- [Hexagonal Architecture Nedir?](#what-is-hexagonal-arch)

  - [Ports](#ports)
  - [Adapters](#adapters)
  - [Domain](#domain)
  - [Use Case](#use-case)
  - [Infrastructure](#infrastructure)
  - [Driving(Left) Side](#driving-side)
  - [Driven(Right) Side](#driven-side)

- [Domain Driven Design](#domain-driven-design)
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
  - [Architecture design](#architecture-design)


## <a name="usage">Projede Kullanılan Teknolojiler</a>

- .Net Core 8
- Entity Framework Core 8
- Swagger
- MediatR
- API Versioning
- Exception Handling
- Fluent Validation
- Mapster
- Testcontainers

## <a name="what-is-hexagonal-arch">Hexagonal Architecture Nedir?</a>

Hexagonal Architecture; Uygulamanın daha iyi test edilebilir olmasını, sürdürülebilirliğini, güvenirlirliğini, esnekliğini ve bağımlılıklarını azaltmak amacıyla Alistair Cockburn tarafından 2005 yılında yazdığı bir makalede tanıtılmıştır. Bu mimaride katmanlar yoktur. Aşağıda açıklayacağım Port ve Adapter kavramları mimarinin temel yapısını oluşturmaktadır. Uygulama driving ve driven olmak üzere iki bölüme ayrılmaktadır. Her iki tarafta da Port ve Adapter üzerinden dış dünya ile iç dünya arasındaki bağlantı sağlanmaktadır.

Clean Architecture uygulayabilmek için kullanılan tasarım kalıplarından biridir. Aşağıda mimari tasarımı gösterilmektedir.

![hexagonal-arch](https://user-images.githubusercontent.com/16361055/149714561-e41132d2-e196-4246-89e0-3ed3703af2ca.png)

- ### <a name="ports">Ports</a>

Uygulamadaki iş kurallarının ve akışının sağlanmasında kullanılan arayüzlere verilen isimdir. Kısaca interface denilebilir. Bu arayüzler tüm entegrasyonları uygulamadan soyutlamaya yarar. Uygulamanın sol tarafında uygulama içerisiyle iletişim, sağ tarafında ise uygulama entegrasyonları için kullanılır.

- ### <a name="adapters">Adapters</a>

Uygulamadaki arayüzlerin entegrasyonlarıdır. Uygulamanın sol tarafında apiler bu adapter'lara örnek verilebilir. Sağ tarafında ise veritabanı entegrasyonları örnek olarak söylenebilir.

- ### <a name="domain">Domain</a>

Uygulamanın en iç katmanıdır. Tüm entity ve temel araçlar bu kısımda barındırılır.

- ### <a name="use-case">Use Case</a>

Uygulamadaki iş kurallarının yönetildiği kısımdır. Application Service katmanında bulunur. Sol kısımdaki port, use case'i temsil etmektedir.

- ### <a name="infrastructure">Infrastructure</a>

Uygulama içerisindeki tüm entegrasyonların bulunduğu bölümdür. Uygulamadan bağımsızdır ve kolayca değiştirilebilir bir yapıdadır.

- ### <a name="driving-side">Driving(Left) Side</a>

Uygulamaya giriş kısmıdır. Dışarıdan gelen tüm istekler bu bölümde karşılanır. Application Servis katmanında işlenerek geri sonuç döndürülür.

- ### <a name="driven-side">Driven(Right) Side</a>

Uygulamanın dış dünya ile iletişim kurduğu kısımdır (Veritabanı gibi). Application Servis katmanından port arayüzü kullanılarak gelen istekleri dış dünya ile iletişime geçerek veya alt yapı içerisinde tamamlayarak geri sonuç döndürür.


## <a name="domain-driven-design">Domain Driven Design</a>

Domain Driven Design; Program genelinde yaşanan problemlerin domain esas alınarak analiz edilmesi ve çözülmesi gerektiğini savunanmaktadır. Bir mimariden ziyade bir düşünce biçimidir. Domainin net bir şekilde anlaşılmasını sağlamayı amaçlamaktadır.

- ### <a name="strategic-design">Strategic Design</a>
  - #### <a name="domain-model">Domain Model</a>
  - #### <a name="domain-expert">Domain Expert</a>
  - #### <a name="ubiquitous-language">Ubiquitous Language</a>
  - #### <a name="bounded-context">Bounded Context</a>
  - #### <a name="context-mapping">Context Mapping</a>

- ### <a name="tactical-design">Tactical Design</a>
  - #### <a name="entity">Entity</a>
  - #### <a name="value-object">Value Object</a>
  - #### <a name="aggregate-root">Aggregate Root</a>

- ### <a name="architecture-design">Architecture design</a>
