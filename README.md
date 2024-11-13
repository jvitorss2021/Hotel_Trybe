# Trybe Hotel
Este projeto consiste numa API para gerir o sistema de reservas de hotéis, permitindo que os utilizadores realizem consultas de disponibilidade, façam reservas e visualizem informações sobre os quartos disponíveis.
## tecnologias usadas 
### c# e .NET Core
Descrição: C# é a linguagem de programação utilizada para o desenvolvimento desta API, sendo amplamente utilizada em aplicações empresariais devido à sua robustez e produtividade. O .NET Core é o framework que permite criar aplicações de alta performance, flexíveis e cross-platform.
Função no projeto: Desenvolver a lógica de negócio da API, garantindo uma estrutura sólida e fácil manutenção.
### Entity Framework Core
Descrição: O Entity Framework Core (EF Core) é uma ferramenta de ORM (Object-Relational Mapping) que simplifica o acesso a bases de dados em aplicações .NET. Permite mapear as entidades da aplicação para tabelas no banco de dados.
Função no projeto: Facilitar a comunicação entre a API e o banco de dados, utilizando modelos de dados e abstraindo consultas SQL complexas para um nível de código mais compreensível.
### Docker
Descrição: O Docker é uma plataforma de containers que permite empacotar e executar aplicações de forma isolada e portátil. Com Docker, o ambiente da aplicação pode ser configurado uma vez e facilmente replicado em qualquer sistema.
Função no projeto: Gerar containers para a API e para o banco de dados SQL Server, garantindo consistência do ambiente de desenvolvimento para o ambiente de produção e facilitando a escalabilidade e o deploy da aplicação.
### SQL Server
Descrição: SQL Server é um sistema de gestão de bases de dados relacional desenvolvido pela Microsoft, altamente seguro e adequado para grandes volumes de dados.
Função no projeto: Armazenar todas as informações de reservas, detalhes dos hotéis e usuários. O SQL Server é utilizado junto ao Entity Framework Core para realizar consultas, operações de criação, atualização e exclusão de dados.
### Microsoft Azure
Descrição: Azure é a plataforma de cloud computing da Microsoft que oferece uma ampla gama de serviços para hospedagem, gestão e escalabilidade de aplicações e bancos de dados.
Função no projeto: Hospedar a API e o banco de dados, beneficiando-se de uma infraestrutura confiável, escalável e com backup automático. Azure também permite a integração contínua e monitorização do desempenho da API em tempo real.
### Funcionalidades da API
Consulta de Disponibilidade: Verificar a disponibilidade de quartos num determinado período.
Reservas de Quartos: Criar, cancelar ou modificar reservas de acordo com a disponibilidade.
Gestão de Usuários: Criação, autenticação e gestão de perfis de utilizador.
Gestão de Hotéis e Quartos: Permitir que os administradores adicionem, editem e removam informações de hotéis e quartos.

## Como Executar o Projeto Localmente
Para executar a API de reservas de hotéis em ambiente local, siga as instruções abaixo:

Pré-requisitos:

.NET Core SDK instalado.
Docker instalado e em execução.
Conta no Azure (opcional para deploy em produção).
Passos:

Clone este repositório:
```bash
git clone git@github.com:jvitorss2021/Hotel_Trybe.git
```
Navegue até o diretório do projeto:
cd seu-repositorio
Instale as dependências
Entre na pasta src/
Execute o comando: 
```bash
dotnet restore
```
Execute o projeto com Docker Compose:
```bash
docker-compose up -d --build
```
Acessar a API:

A API estará acessível em http://localhost:5000 (ou a porta configurada).
Deploy no Azure:

Configure a sua conta Azure e siga as instruções de deploy da documentação oficial.
### Contribuição
A estrutura do projeto e a implementação de diversos componentes foram realizadas com uma organização modular para facilitar a manutenção e a escalabilidade. As principais pastas e camadas implementadas incluem:

Controller: Responsável por gerir as solicitações HTTP e direcioná-las para os serviços apropriados.
Dto (Data Transfer Object): Define os objetos de transferência de dados para estruturar a comunicação entre a API e o cliente, garantindo uma interação eficiente e segura.
Models: Contém as definições das entidades utilizadas na aplicação, refletindo a estrutura dos dados no banco de dados e o modelo de domínio.
Services: Implementa a lógica de negócio e regras essenciais para o funcionamento do sistema, isolando a lógica do controlador.
Repository: Realiza operações de acesso aos dados, comunicando-se diretamente com o banco de dados através do Entity Framework Core.
