# 🍽️ PointSale - Backend para Sistema de Pedidos

O **PointSale** é um sistema backend desenvolvido em C# com ASP.NET para gerenciar pedidos em restaurantes. Ele fornece APIs para administração de gerentes, lojas, mesas, produtos e pedidos, permitindo um controle eficiente do estabelecimento. 🏪📦

## 🛠️ Tecnologias Utilizadas
- 💻 C#
- ⚙️ ASP.NET Core
- 🗄️ Entity Framework Core
- 🐘 PostgreSQL
- 📜 Swagger para documentação da API

## 🚀 Funcionalidades
- 👤 **Gerenciamento de Gerentes:** Cadastro e login de gerentes responsáveis pela administração das lojas.
- 🔐 **Autenticação Segura:** Sistema de login e seleção de lojas com autenticação por senha.
- 🏬 **Cadastro e Gerenciamento de Lojas:** Possibilidade de criar lojas e visualizar informações detalhadas.
- 🍽️ **Controle de Mesas:** Criação, listagem e remoção de mesas no restaurante.
- 🛍️ **Gestão de Produtos:** Cadastro de produtos com nome, descrição e preço.
- 📝 **Registro de Pedidos:** Criação e acompanhamento de pedidos por mesa.
- 🔄 **Associação de Produtos aos Pedidos:** Adicionar produtos aos pedidos e atualizar status.

## 📥 Instalação
1. Clone o repositório:
   ```sh
   git clone https://github.com/seu-usuario/pointsale-backend.git
   ```
2. Acesse o diretório do projeto:
   ```sh
   cd pointsale-backend
   ```
3. Configure a string de conexão no `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Host=localhost;Port=5432;Database=pointsale;Username=postgres;Password=senha"
   }
   ```
4. Execute as migrações do banco de dados:
   ```sh
   dotnet ef database update
   ```
5. Execute o projeto:
   ```sh
   dotnet run
   ```

## 🔗 Endpoints da API
A API oferece endpoints para gerenciamento de:
- 👥 **Gerentes:** `/managers`
- 🔑 **Autenticação:** `/auth`
- 🏬 **Lojas:** `/stores`
- 🍽️ **Mesas:** `/tables`
- 🛒 **Produtos:** `/products`
- 📝 **Pedidos:** `/orders`
- 📦 **Itens dos Pedidos:** `/orders-products`

📄 A documentação da API pode ser acessada via Swagger em:
```
http://localhost:5000/swagger
```

## 📌 Futuras Implementações
- 💳 Suporte a pagamento integrado.
- 🔄 Sincronização com servidores remotos.
- 📊 Relatórios de vendas e estoque.
- 🔔 Notificações em tempo real.

## 🤝 Contribuição
Sinta-se à vontade para contribuir com o projeto, enviando sugestões ou pull requests.

## 📜 Licença
Este projeto está sob a licença MIT.