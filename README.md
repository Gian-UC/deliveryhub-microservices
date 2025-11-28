# DeliveryHub‑Microservices

> Microservices architecture built with .NET 8, Docker, YARP Gateway and WSL2 environment.

## 🧾 Tecnologias

| Camada                      | Tecnologia                             |
|-----------------------------|---------------------------------------|
| Linguagem                  | C#                                    |
| Framework                 | .NET 8 Web API                         |
| Containerização           | Docker (Engine via WSL2)              |
| Orquestração              | Docker Compose                        |
| API Gateway              | YARP Reverse Proxy                     |
| Banco de Dados (futura)   | PostgreSQL / MySQL (a definir)        |
| Autenticação              | JWT Token                             |
| Hospedagem (futura)       | Azure Container Apps / AKS            |

## 🏗️ Arquitetura

[ Client ] → [ Gateway (YARP) ] → { Auth Service | Pedidos Service | Entregas Service | Entregadores Service }


Cada microserviço roda em sua própria imagem Docker, escopo isolado, comunicando-se via gateway.

## 🚀 Como rodar localmente

Requisitos:

- Windows 10/11 com WSL2 e Ubuntu 22.04
- .NET SDK 8.0
- Docker Engine via WSL2

```bash
cd deliveryhub-microservices
docker compose build
docker compose up -d
```
Abra o browser e acesse:

http://localhost:8080 → Gateway

http://localhost:8080/pedidos → Pedidos Service

http://localhost:8080/auth → Auth Service

etc.

🧪 Uso dos serviços
Pedidos Service

GET /pedidos — lista todos

POST /pedidos — cria novo

PUT /pedidos/{id} — atualiza status

Entregas Service

GET /entregas — lista

POST /entregas — iniciar entrega

Entregadores Service

GET /entregadores — lista

POST /entregadores — registra entregador

Auth Service

POST /auth/register — registra usuário

POST /auth/login — retorna token JWT

Todos os endpoints são passados via Gateway em http://localhost:8080/*.

✅ Checklist concluído

✅ Cada microserviço em .NET 8

✅ Gateway com YARP

✅ Docker Engine no WSL2

✅ Docker Compose com múltiplos serviços

✅ Build+Run sem erros

✅ Repositório GitHub configurado

📂 Branching & Contribuição

main – versão pronta para produção

develop – versão em desenvolvimento

**feature/*” – novas funcionalidades

Sinta‑se à vontade para abrir Issues e Pull Requests.
