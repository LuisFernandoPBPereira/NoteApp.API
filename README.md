<h1 align="center">📝 API de Lembretes 📝</h1>

## Sobre

Esta é uma API de Lembretes/Notas, permitindo que você consiga guardar suas anotações do dia a dia!

### Features

- Data de Alerta: Defina uma data de alerta para o lembrete, e futuramente ela poderá ser filtrada como um lembrete que está chegando perto de seu vencimento
- Cor: Pensando em um futuro frontend, guardamos a cor do seu lembrete, como se fossem papéis coloridos!
- Auditoria: Podemos ver quando foi criado o lembrete e quando foi modificado.

## Tecnologias utilizadas

|Tecnologia               |Motivo                                                                                                                                        |
|-------------------------|----------------------------------------------------------------------------------------------------------------------------------------------|
|Entity Framework         |Migrations para definição do banco, definição de relacionamento entre entidades e consultas com LINQ.                                         |
|Entity Framework Design  |Adiciona ferramentas no terminal de Gerenciador de Pacotes para realizarmos Migrations e atualizações de banco.                               |
|Identity Entity Framework|Traz um conjunto de funcionalidades para criarmos usuários na aplicação, envolvendo Roles e questões de segurança.                            |
|Authentication JWT Bearer|A API possui login com autenticação JWT, com isso, podemos realizar logins mais seguros e confiáveis.                                         |
|xUnit                    |Biblioteca de Testes para realizar testes unitários na nossa aplicação, testando pequenos módulos para verificar o funcionamento da nossa API.|

## Arquitetura utilizada

Para esta API, escolhi utilizar a Clean Architecture, pois é uma ótima maneira de separar a regra de negócio (Domínio) de dependências externas (Infraestrutura), fazendo com que os casos de uso sejam claros (Aplicação).
