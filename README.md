# Projeto Acadêmico: Integração Backend para Consultas Médicas Online  

🚀 **Descrição do Projeto:**  
Desenvolvi este projeto acadêmico com o objetivo de aprimorar minhas habilidades em backend, utilizando **ASP.NET Web API** para criar uma solução eficiente de gestão de consultas médicas online. A aplicação abrange funcionalidades como gerenciamento de usuários, agendamentos de consultas e controle de especialidades médicas, priorizando desempenho, segurança e automação de processos.

## Funcionalidades  

- 👥 **Gerenciamento de Usuários:**  
  - Usuários recém-cadastrados recebem automaticamente o cargo de **paciente**.  
  - Pacientes podem solicitar a mudança de cargo para **médico**, selecionando uma especialidade: **Psicologia**, **Nutrição**, **Dermatologia** ou **Clínica Geral**.  
  - Apenas administradores podem aprovar ou rejeitar as solicitações de mudança de cargo.  

- 📅 **Agendamento de Consultas:**  
  - Endpoints para buscar médicos disponíveis por especialidade e verificar seus horários livres.  
  - Consultas podem ser agendadas com intervalos de **40 minutos** entre cada uma.  
  - E-mails automáticos de confirmação são enviados tanto para médicos quanto para pacientes após o agendamento.  
  - Médicos podem cancelar consultas, e um e-mail de notificação é enviado automaticamente ao paciente.  

- 📧 **Notificações por E-mail:**  
  - Utilização de **SMTP** para envio de e-mails automáticos de confirmação e cancelamento de consultas.  

- 🔒 **Controle de Acesso:**  
  - Sistema de autenticação e autorização, garantindo que apenas usuários autenticados possam acessar as funcionalidades específicas.  

---

## Tecnologias Utilizadas  

- **ASP.NET Core 8.0** para o desenvolvimento da API  
- **Entity Framework Core** para interação com o banco de dados  
- **SQL Server** como sistema de gerenciamento de banco de dados  
- **Swagger** para documentação e teste dos endpoints  
- **SMTP** para envio de notificações automáticas por e-mail  

---

## Desenvolvimento e Aprendizado  

Durante o desenvolvimento deste projeto, pude aprimorar minhas habilidades em:  
- Criação de APIs RESTful escaláveis e seguras.  
- Implementação de regras de negócios complexas, como o controle de cargos e especialidades médicas.  
- Automação de notificações por e-mail utilizando SMTP.  
- Utilização do Swagger para documentação clara e eficiente da API.  

---


## Como Executar o Projeto

## 1. Clone este repositório:
git clone https://github.com/Higoralmeida702/Sistema-Erp

## 2. Navegue até a pasta do projeto:
cd Sistema-Erp

## 3. Abra o projeto no Visual Studio ou na sua IDE de preferência.

## 4. Restaure as dependências:
dotnet restore

## 5. Execute a aplicação:
dotnet run

## 6. Acesse a documentação da API no Swagger
