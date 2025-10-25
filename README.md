# API de Lista de Tarefas (Projeto de Estudo)

![Último Commit](https://img.shields.io/github/last-commit/MatheusFerGo/ToDoListApp)
![Tamanho do Repositório](https://img.shields.io/github/repo-size/MatheusFerGo/ToDoListApp)

Este é um projeto de estudo de uma API .NET 8 para gestão de tarefas (To-Do List), focado em explorar e comparar diferentes estratégias de persistência de dados. A aplicação foi construída seguindo princípios de Arquitetura Limpa (DDD-Lite) e SOLID, com uma clara separação entre as camadas de Domínio, Aplicação e Infraestrutura.

O objetivo principal é demonstrar como a camada de persistência pode ser trocada (do local para a nuvem) sem impactar a lógica de negócio, graças ao uso de Interfaces e Injeção de Dependência.

## O Estudo de Persistência

<p>
  <img src="https://img.shields.io/badge/.NET-8.0-blue?logo=.net" alt=".NET 8">
  <img src="https://img.shields.io/badge/C%23-blueviolet?logo=c-sharp" alt="C#">
  <img src="https://img.shields.io/badge/Supabase-green?logo=supabase" alt="Supabase">
  <img src="https://img.shields.io/badge/Docker-blue?logo=docker" alt="Docker">
  <img src="https://img.shields.io/badge/xUnit-blue?logo=xunit" alt="xUnit">
  <img src="https://img.shields.io/badge/MySQL-white?logo=mysql&logoColor=blue" alt="MySQL">
</p>

Este repositório está organizado em branches, onde cada uma representa uma abordagem diferente para o armazenamento de dados:

1.  **Persistência em Memória (Branch `main`)**
    * **Descrição:** A primeira versão da API. Os dados são armazenados numa lista estática em memória.
    * **Propósito:** Ideal para prototipagem rápida, testes unitários e desenvolvimento inicial, pois não requer nenhuma configuração de banco de dados. Os dados são perdidos sempre que a aplicação é reiniciada.

2.  **Persistência com Docker + MySQL (Branch `todolistapp-mysql`)**
    * **Descrição:** A API é conectada a um banco de dados MySQL real, rodando num container Docker local.
    * **Propósito:** Simula um ambiente de produção local. Os dados são persistentes e sobrevivem a reinicializações da API. Utiliza o Entity Framework Core para o mapeamento e migrações do banco de dados.

3.  **Persistência em Nuvem com Supabase (Branch `todolistapp-supabase`)**
    * **Descrição:** A API é conectada a um projeto Supabase, utilizando o seu banco de dados PostgreSQL na nuvem.
    * **Propósito:** Demonstra a integração com um serviço de "Backend as a Service" (BaaS). Esta branch também implementa a autenticação de utilizadores via JWT, onde apenas utilizadores autenticados podem aceder aos seus próprios dados.

## Tecnologias Utilizadas

* **.NET 8** (C#)
* **ASP.NET Core Web API**
* **Arquitetura Limpa** (DDD-Lite)
* **Princípios SOLID** e Injeção de Dependência
* **xUnit** & **Moq** (Para Testes Unitários)
* **Entity Framework Core** (Na branch MySQL)
* **Supabase.Client** (Na branch Supabase)
* **Docker**
* **Git** (com estratégia de branches por funcionalidade)

## Como Executar

Você precisará ter o **SDK do .NET 8** e o **Git** instalados.

### Pré-requisitos Gerais

1.  Clone o repositório:
    ```bash
    git clone [https://github.com/seu-usuario/seu-repositorio.git](https://github.com/seu-usuario/seu-repositorio.git)
    cd seu-repositorio
    ```

2.  Instale as ferramentas do EF Core (necessário para a branch MySQL):
    ```bash
    dotnet tool install --global dotnet-ef
    ```

---

### 1. Para Rodar a Versão em Memória

1.  Mude para a branch principal:
    ```bash
    git checkout main
    ```

2.  Entre na pasta da API:
    ```bash
    cd TodoListApp.API
    ```

3.  Rode a aplicação:
    ```bash
    dotnet run
    ```
A API estará disponível em `https://localhost:xxxx`.

---

### 2. Para Rodar a Versão MySQL com Docker

1.  Mude para a branch correta:
    ```bash
    git checkout feature/mysql-local
    ```

2.  Certifique-se de que o **Docker Desktop** está em execução.

3.  Inicie o container do MySQL (substitua `sua-senha-forte` por uma senha de sua escolha):
    ```bash
    docker run --name todolist-mysql-db -e MYSQL_ROOT_PASSWORD=sua-senha-forte -p 3306:3306 -d mysql:latest
    ```

4.  Configure os "User Secrets" para guardar a sua senha de forma segura (substitua pela senha que você usou acima):
    ```bash
    cd TodoListApp.API
    dotnet user-secrets init
    dotnet user-secrets set "ConnectionStrings:DefaultConnection" "server=localhost;port=3306;database=todolistdb;user=root;password=sua-senha-forte"
    ```

5.  Aplique as migrações do Entity Framework para criar as tabelas:
    ```bash
    dotnet ef database update
    ```

6.  Rode a aplicação:
    ```bash
    dotnet run
    ```

---

### 3. Para Rodar a Versão Supabase

1.  Mude para a branch correta:
    ```bash
    git checkout feature/supabase-auth
    ```
2.  Crie um projeto gratuito no [Supabase](https://supabase.com/).
3.  Vá em **"Table Editor"** e crie a sua tabela `Items` (ou `Tarefas`).
4.  Vá em **"Project Settings" > "API"** e copie a sua **URL** e a sua chave **`service_role`**.
5.  Vá em **"Authentication" > "Providers"** e ative o **Google** (ou outro de sua escolha).

6.  Configure os "User Secrets" com as suas chaves do Supabase:
    ```bash
    cd TodoListApp.API
    dotnet user-secrets init
    dotnet user-secrets set "Supabase:Url" "SUA_URL_AQUI"
    dotnet user-secrets set "Supabase:ApiKey" "SUA_CHAVE_SERVICE_ROLE_AQUI"
    ```

7.  Rode a aplicação:
    ```bash
    dotnet run
    ```