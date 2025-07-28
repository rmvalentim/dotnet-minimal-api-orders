# Minimal API Orders
Este projeto é um exemplo de API REST em .NET 6+ usando Minimal APIs, Entity Framework Core e SQLite para gerenciar pedidos (Order) e seus produtos (Product).

---

## Tecnologias utilizadas
* .NET 9
* Entity Framework Core
* SQLite
* Minimal APIs

---

## Estrutura das Entidades

### Order
* Id (int) - Identificador único.
* Name (string) - Nome da ordem.
* CreationDate (DateTime) - Data de criação.
* Products (List<Product>) - Lista de produtos (relacionamento 1-N).

### Product
* Id (int) - Identificador único.
* Name (string) - Nome do produto.
* Quantity (int) - Quantidade.
* OrderId (int) - Chave estrangeira para Order.


---

## Como rodar o projeto

1. Clonar o repositório:
```bash
   git clone
   cd MinimalApis
```

2. Restaurar pacotes:
```bash
   dotnet restore
```
3. Aplicar migrações e criar o banco:

Certifique-se de ter o dotnet-ef instalado:
```bash
   dotnet tool install --global dotnet-ef
```
Depois rode:
```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
```
4. Rodar a API:
```bash
   dotnet run
```
A API ficará disponível

---

## Rotas disponíveis

GET /orders
Retorna todas as ordens com seus produtos.

GET /orders/{id}
Retorna uma ordem específica (com produtos).

POST /orders
Cria uma nova ordem.

Exemplo de JSON para POST:

{
  "name": "Primeira Ordem",
  "products": [
    {
      "name": "Notebook",
      "quantity": 1
    },
    {
      "name": "Mouse",
      "quantity": 2
    }
  ]
}

PUT /orders/{id}
Atualiza os dados de uma ordem existente (não atualiza produtos diretamente).

DELETE /orders/{id}
Remove uma ordem e seus produtos.

---

Requisitos
* .NET 9 instalado
* CLI dotnet-ef para migrações

---

Comandos úteis
* Criar uma nova migração:
  dotnet ef migrations add NomeDaMigracao

* Aplicar migrações ao banco:
  dotnet ef database update

* Remover última migração:
  dotnet ef migrations remove