<p align="center">
  <img src="https://i.imgur.com/G3Q8qJd.png" width="820" />
</p>

<h1 align="center">🚀 DeliveryHub-Microservices</h1>
<p align="center">
  Arquitetura de microserviços moderna, performática e divertida — construída com .NET 8, RabbitMQ, Docker, YARP e muito carinho da Aria 💙😎
</p>

---

## 💙 Tecnologias Utilizadas

| Camada / Função            | Tecnologia                              |
|----------------------------|------------------------------------------|
| Linguagem                 | C#                                       |
| Framework Backend         | .NET 8 Web API                           |
| Comunicação Assíncrona    | RabbitMQ                                 |
| Gateway                   | YARP Reverse Proxy                       |
| Banco de Dados            | PostgreSQL                               |
| Autenticação              | JWT Token                                |
| Containerização           | Docker (WSL2 Backend)                    |
| Orquestração              | Docker Compose                           |
| Logs / Observabilidade    | ASP.NET Logging + Docker Logs            |
| Infra futura              | Azure Container Apps / AKS               |

---

## 🏗️ Arquitetura Geral

[ Client SPA / Mobile ]
↓
[ Gateway (YARP) ]
↓
┌────────┼───────────┬──────────────┐
│ │ │ │
│ Auth Service Pedidos Entregas Entregadores
│ Service Service Service
│
└───────────────⇆ RabbitMQ (Event Bus)


• Cada serviço roda em **seu próprio container**  
• Comunicação interna via gateway  
• Eventos (pedido criado, atualização, etc.) trafegam pelo **RabbitMQ**  
• Banco de dados isolado por serviço (modelo real de microserviços)  

---

## 🚀 Rodando Localmente

**Requisitos**

- Windows 10/11  
- WSL2 + Ubuntu 22.04  
- Docker Engine no WSL2  
- .NET SDK 8.0  

**Comandos:**

```bash
cd deliveryhub-microservices
docker compose build
docker compose up -d
🌐 Endpoints via Gateway
O Gateway roda padrão na porta:
http://localhost:8081/
```
🔐 Auth Service
POST /api/auth/register
POST /api/auth/login

📦 Pedidos Service
GET  /api/pedidos
POST /api/pedidos
PUT  /api/pedidos/{id}

🚚 Entregas Service
GET  /api/entregas
POST /api/entregas/iniciar

👤 Entregadores Service
GET  /api/entregadores
POST /api/entregadores

🐇 RabbitMQ (Event Bus)
Eventos publicados:
pedido.criado
pedido.atualizado

Filas:
entregas-pedido-criado
entregas-status-atualizado

Painel do RabbitMQ:
http://localhost:15672/
user: guest
pass: guest

📦 Estrutura do Projeto
deliveryhub-microservices/
│
├── pedidos-service/
├── entregas-service/
├── entregadores-service/
├── auth-service/
├── gateway/
│── docker-compose.yml
└── README.md

🔧 Docker Compose
Cada serviço tem seu Dockerfile próprio e roda isolado:

• gateway expõe a porta 8081
• serviços internos expõem portas 8080-8084
• RabbitMQ + Postgres já sobem automaticamente

🧪 Checklist do Projeto
✔ Microserviços 100% independentes
✔ Banco de dados isolado
✔ Comunicação via RabbitMQ
✔ Gateway YARP configurado
✔ Docker Compose com 7 containers
✔ Build estável no WSL2
✔ Código padronizado com .NET 8
✔ Configurado para GitHub

🧩 Branching
Branch	Descrição
main	versão estável
develop	próxima release
feature/*	novas funcionalidades


