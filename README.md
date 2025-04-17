# API Motok

## ⚙️ Como executar a aplicação

Você pode executar a aplicação de diferentes formas, conforme sua preferência:

### ✅ Usando Docker

Certifique-se de que o Docker esteja instalado e em execução, então execute na pasta raiz do projeto:

```bash
docker-compose up
```

Isso iniciará toda a infraestrutura automaticamente, incluindo banco de dados PostgreSQL e RabbitMQ.

### ✅ Usando Visual Studio 2022

1. Abra a solução no Visual Studio 2022.
2. No menu superior, selecione o projeto `docker-compose` como **Startup Project**.
3. Clique no botão verde de **Run** para iniciar a aplicação.

### ✅ Usando perfis padrão (sem Docker)

Caso prefira executar sem Docker, você pode usar os perfis `http` ou `https`:

```bash
dotnet run --launch-profile http
```

> **Importante:** Ao usar os perfis `http` ou `https`, você precisará iniciar manualmente os serviços necessários como banco de dados e RabbitMQ antes de rodar a aplicação.

---

## 👤 Usuários pré-cadastrados

| Role     | Usuário  | Senha      |
|----------|----------|------------|
| DELIVERY | delivery | 1234567890 |
| ADMIN    | admin    | 1234567890 |

---

### 👌 Pronto!
Está tudo certo! Agora basta abrir o endereço do Swagger na porta http://localhost:8080/swagger ou utilizar as rotas disponíveis abaixo:

## 📌 Rotas disponíveis

🔐 AuthController (/auth) — ADMIN
- POST /auth/register — Cadastra um novo usuário.
- POST /auth/login — Autentica um usuário e retorna um token (aberta para anônimos).

🛵 DeliveryController (/entregadores) — DELIVERY
- POST /entregadores — Cria um novo entregador.
- POST /entregadores/{id}/cnh — Envia a imagem da CNH do entregador.

🏍️ MotorcycleController (/motos) — ADMIN
- POST /motos — Cadastra uma nova moto.
- GET /motos — Lista todas as motos (com filtro opcional por placa).
- GET /motos/{id} — Busca uma moto por ID.
- PATCH /motos/{id}/placa — Atualiza a placa de uma moto.
- DELETE /motos/{id} — Remove uma moto.

📄 RentController (/locacao) — DELIVERY
- POST /locacao — Realiza uma locação de moto.
- PUT /locacao/{id}/devolucao — Registra a devolução da moto.
- GET /locacao/{id} — Consulta a locação por ID.

## 📦 Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [Docker](https://www.docker.com/)
- (Opcional) Visual Studio 2022 com suporte a Docker
