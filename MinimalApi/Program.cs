using Microsoft.EntityFrameworkCore;
using MinimalApi.Contexto;
using MinimalApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<Contexto>
   (options => options.UseSqlServer(
       builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();

app.MapPost("AdicionaProduto", async(Produto produto, Contexto contexto) =>
{
    contexto.Produtos.Add(produto);
    await contexto.SaveChangesAsync();
}
    );

app.MapPost("ExcluirProduto/{id}", async (int id, Contexto contexto) =>
{
    var produtoExcluir = await contexto.Produtos.FirstOrDefaultAsync(x => x.Id == id);
    if (produtoExcluir != null)
    {
        contexto.Produtos.Remove(produtoExcluir);
        await contexto.SaveChangesAsync();
    }
});

app.MapGet("ListarProdutos", async (Contexto contexto) =>
{
    return await contexto.Produtos.ToListAsync();
});

app.MapPost("ObterProduto/{id}", async(int id, Contexto contexto) =>
{
    return await contexto.Produtos.FirstOrDefaultAsync(x => x.Id == id);
});

app.UseSwaggerUI();
app.Run();
