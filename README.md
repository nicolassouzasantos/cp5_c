CP5 - Advanced Business Development with .NET - 2025
===========================

Este projeto faz parte do CP5 da disciplina Advanced Business Development with .NET e tem como objetivo criar uma API RESTful, usando .NET 8 ou 9 e banco Oracle, com boas praticas de programação .

## 📜 Instruções Básicas

  - 🔹 O checkpoint deverá ser desenvolvido individualmente
  - 🔹 Não pode utilizar exemplos criados em aulas
  - ❌ Se utilizar o **PROJETO** da sua turma sua nota será 0 (zero).
  - ❌ Se tivermos projetos iguais as notas que copiaram será 0 (zero).
  - ⚠️ O projeto deverá compilar, caso não compile sua nota será 0 (zero).
  - 🤖 Se for usar IA, use da maneira certa. Não copie e cole o que ela mandar.
  - ❌ *OBS: Não será aceito entregas via teams* (Caso entreguem fora do prazo será descontado 3 pontos). 

## 📽️ Clonando Projeto

### 🛠️ Passo a passo

1. Git clone com URL do repositório https://gitlab.com/fiap-dotnet/2tdspy/2025/segundo-semestre/checkpoint/checkpoint_05.git
2. Crie uma nova branch 
	- Exemplo RM123456
3. Faça o checkout no branch (Caso esteja usando alguma IDE ele fará isso automaticamente)
4. Desenvolva o projeto
5. Faça o Commit + Push na sua branch para subir as alterações no repositório da Turma


🎯 O QUE VOCÊ DEVE FAZER
===========================

✅ Criar uma API RESTful que contenha:
- CRUD completo para entidade `Clinica` contendo os verbos (GET, POST, PUT, DELETE)
	- Clinica
		- Campos devem conter:
			- Id - Int
			- Nome - String  (Obrigatório)
			- Email - String (Obrigatório)
			- Telefone - String
	- Profissionais
		- Campos devem conter:
			- Id - Int
			- Nome - String  (Obrigatório)
			- Email - String (Obrigatório)
			- Idade - Int  (Obrigatório)
			- Cargo - String
	- Endereco
		- Campos devem conter:
			- Id - Int
			- Rua - String  (Obrigatório)
			- Numero - Int (Obrigatório)
			- Bairro - String (Obrigatório)
			- CEP - String (Obrigatório)
- Relacionamentos devem seguir:
	- **1:N** → `Clinica` → `Profissionais` 
	- **1:1** → `Clinica` → `Endereco`
- Além dos métodos de CRUD para `Clinica`, a controller deve conter dois métodos:
	- Adicionar o profissional (Passando IdClinica)
	- Editar o profissional (Passando IdProfissional e IdClinica)
- Respostas HTTP apropriadas: 200, 201, 204, 400, 404, etc.

✅ Aplicar boas práticas de desenvolvimento:
- Separação da lógica de banco: Repository Pattern
- DTOs e validações (Adicione as validações necessárias para garantir a entrada de dados)
- Mapper das entidades de entrada
- OperationResult (Para retornar os erros na camada de Aplicação)
- Organização das camadas usando Clean Architecture com `Application`, `Domain` e `Infrastructure`

✅ Banco de Dados:
- Utilização do Oracle via EF Core
- Utilização de Migrations

✅ Documentação:
- Swagger/OpenAPI devidamente configurado e funcional
- Exemplos de Entrada e Saída de dados

📊 O QUE SERÁ AVALIADO
============================

| Critério                                       | Pontos  |
| ---------------------------------------------- | ------- |
| Funcionalidades da API (CRUD, REST, Respostas) | 30      |
| Banco Oracle + Migrations                      | 10      |
| Organização do Repositório                     | 15      |
| Documentação Swagger                           | 10      |
| Uso de Mapping + DTO                           | 15      |
| OperationResult + Organização Camadas          | 20      |
| **Total**                                      | **100** |

💡 DICAS IMPORTANTES
===============================

- Não use o exemplo da aula.
- Escreva código limpo e bem organizado.
- IA pode ajudar, mas entenda cada linha do seu código.

📚 TECNOLOGIAS SUGERIDAS
===============================

- .NET 8 ou 9 / ASP.NET Core Web API
- EF Core + Oracle
- Swagger / Swashbuckle

📅 PRAZO DE ENTREGA
===============================

03 de Outubro de 2025

**OBS:** Entrega via portal FIAP com link do GitLab em um arquivo .TXT.