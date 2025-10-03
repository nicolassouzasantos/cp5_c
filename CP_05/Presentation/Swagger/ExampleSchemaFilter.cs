using System;
using CP_05.Application.Dtos.Clinica;
using CP_05.Application.Dtos.Endereco;
using CP_05.Application.Dtos.Profissional;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CP_05.Presentation.Swagger;

public class ExampleSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        schema.Example = context.Type switch
        {
            Type type when type == typeof(ClinicaCreateDto) => BuildClinicaCreateExample(),
            Type type when type == typeof(ClinicaUpdateDto) => BuildClinicaUpdateExample(),
            Type type when type == typeof(ClinicaReadDto) => BuildClinicaReadExample(),
            Type type when type == typeof(EnderecoCreateDto) => BuildEnderecoCreateExample(),
            Type type when type == typeof(EnderecoUpdateDto) => BuildEnderecoUpdateExample(),
            Type type when type == typeof(EnderecoReadDto) => BuildEnderecoReadExample(),
            Type type when type == typeof(ProfissionalCreateDto) => BuildProfissionalCreateExample(),
            Type type when type == typeof(ProfissionalUpdateDto) => BuildProfissionalUpdateExample(),
            Type type when type == typeof(ProfissionalReadDto) => BuildProfissionalReadExample(),
            _ => schema.Example
        };
    }

    private static IOpenApiAny BuildClinicaCreateExample() => new OpenApiObject
    {
        ["nome"] = new OpenApiString("Clínica Bem-Estar"),
        ["email"] = new OpenApiString("contato@bemestar.com"),
        ["telefone"] = new OpenApiString("+55 11 4002-8922"),
        ["endereco"] = BuildEnderecoCreateExample()
    };

    private static IOpenApiAny BuildClinicaUpdateExample() => new OpenApiObject
    {
        ["nome"] = new OpenApiString("Clínica Bem-Estar Unidade Jardins"),
        ["email"] = new OpenApiString("contato@bemestar.com"),
        ["telefone"] = new OpenApiString("+55 11 98877-6655"),
        ["endereco"] = BuildEnderecoUpdateExample()
    };

    private static IOpenApiAny BuildClinicaReadExample() => new OpenApiObject
    {
        ["id"] = new OpenApiInteger(1),
        ["nome"] = new OpenApiString("Clínica Bem-Estar"),
        ["email"] = new OpenApiString("contato@bemestar.com"),
        ["telefone"] = new OpenApiString("+55 11 4002-8922"),
        ["endereco"] = BuildEnderecoReadExample(),
        ["profissionais"] = new OpenApiArray
        {
            BuildProfissionalReadExample()
        }
    };

    private static IOpenApiAny BuildEnderecoCreateExample() => new OpenApiObject
    {
        ["rua"] = new OpenApiString("Av. Paulista"),
        ["numero"] = new OpenApiInteger(1000),
        ["bairro"] = new OpenApiString("Bela Vista"),
        ["cep"] = new OpenApiString("01310-100")
    };

    private static IOpenApiAny BuildEnderecoUpdateExample() => new OpenApiObject
    {
        ["rua"] = new OpenApiString("Av. Brigadeiro Faria Lima"),
        ["numero"] = new OpenApiInteger(4500),
        ["bairro"] = new OpenApiString("Itaim Bibi"),
        ["cep"] = new OpenApiString("04538-132")
    };

    private static IOpenApiAny BuildEnderecoReadExample() => new OpenApiObject
    {
        ["id"] = new OpenApiInteger(1),
        ["rua"] = new OpenApiString("Av. Paulista"),
        ["numero"] = new OpenApiInteger(1000),
        ["bairro"] = new OpenApiString("Bela Vista"),
        ["cep"] = new OpenApiString("01310-100")
    };

    private static IOpenApiAny BuildProfissionalCreateExample() => new OpenApiObject
    {
        ["nome"] = new OpenApiString("Maria Souza"),
        ["email"] = new OpenApiString("maria.souza@exemplo.com"),
        ["idade"] = new OpenApiInteger(32),
        ["cargo"] = new OpenApiString("Fisioterapeuta")
    };

    private static IOpenApiAny BuildProfissionalUpdateExample() => new OpenApiObject
    {
        ["nome"] = new OpenApiString("Maria Souza"),
        ["email"] = new OpenApiString("maria.souza@exemplo.com"),
        ["idade"] = new OpenApiInteger(33),
        ["cargo"] = new OpenApiString("Coordenadora Clínica")
    };

    private static IOpenApiAny BuildProfissionalReadExample() => new OpenApiObject
    {
        ["id"] = new OpenApiInteger(10),
        ["nome"] = new OpenApiString("Maria Souza"),
        ["email"] = new OpenApiString("maria.souza@exemplo.com"),
        ["idade"] = new OpenApiInteger(32),
        ["cargo"] = new OpenApiString("Fisioterapeuta"),
        ["clinicaId"] = new OpenApiInteger(1)
    };
}
