using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/api/v1/libros", (Libro libro) => CrearLibro(libro));
app.MapGet("/api/v1/libros/{id}", (Guid id) => ObtenerLibroPorId(id));
app.MapPut("/api/v1/libros/{id}", (Guid id, Libro libro) => ActualizarLibro(id, libro));
app.MapDelete("/api/v1/libros/{id}", (Guid id) => EliminarLibro(id));

app.MapPost("/api/v1/autores", (Autor autor) => CrearAutor(autor));
app.MapGet("/api/v1/autores/{id}", (Guid id) => ObtenerAutorPorId(id));
app.MapPut("/api/v1/autores/{id}", (Guid id, Autor autor) => ActualizarAutor(id, autor));
app.MapDelete("/api/v1/autores/{id}", (Guid id) => EliminarAutor(id));

app.Run();

// Implementación de las acciones CRUD

private static List<Libro> libros = new List<Libro>();
private static List<Autor> autores = new List<Autor>();

private static Libro CrearLibro(Libro libro)
{
    libro.Id = Guid.NewGuid();
    libros.Add(libro);
    return libro;
}

private static Libro ObtenerLibroPorId(Guid id)
{
    return libros.FirstOrDefault(l => l.Id == id);
}

private static Libro ActualizarLibro(Guid id, Libro libroActualizado)
{
    var libro = libros.FirstOrDefault(l => l.Id == id);
    if (libro != null)
    {
        libro.Título = libroActualizado.Título;
        libro.Resumen = libroActualizado.Resumen;
        libro.AutorId = libroActualizado.AutorId;
    }
    return libro;
}

private static void EliminarLibro(Guid id)
{
    var libro = libros.FirstOrDefault(l => l.Id == id);
    if (libro != null)
    {
        libros.Remove(libro);
    }
}

private static Autor CrearAutor(Autor autor)
{
    autor.Id = Guid.NewGuid();
    autores.Add(autor);
    return autor;
}

private static Autor ObtenerAutorPorId(Guid id)
{
    return autores.FirstOrDefault(a => a.Id == id);
}

private static Autor ActualizarAutor(Guid id, Autor autorActualizado)
{
    var autor = autores.FirstOrDefault(a => a.Id == id);
    if (autor != null)
    {
        autor.Nombre = autorActualizado.Nombre;
        autor.Nacionalidad = autorActualizado.Nacionalidad;
    }
    return autor;
}

private static void EliminarAutor(Guid id)
{
    var autor = autores.FirstOrDefault(a => a.Id == id);
    if (autor != null)
    {
        autores.Remove(autor);
    }
}

// Definición de las entidades

public class Libro
{
    public Guid Id { get; set; }
    public string Título { get; set; }
    public string Resumen { get; set; }
    public Guid AutorId { get; set; }
}

public class Autor
{
    public Guid Id { get; set; }
    public string Nombre { get; set; }
    public string Nacionalidad { get; set; }
}
app.MapPost("/api/v1/libros", (LibroDTO libroDTO) =>
{
    if (string.IsNullOrWhiteSpace(libroDTO.Titulo) || string.IsNullOrWhiteSpace(libroDTO.Resumen))
    {
        return Results.BadRequest("El título y el resumen son obligatorios.");
    }

    var libro = new Libro
    {
        Id = Guid.NewGuid(),
        Titulo = libroDTO.Titulo,
        Resumen = libroDTO.Resumen,
        AutorId = libroDTO.AutorId
    };

    libros.Add(libro);
    return Results.Created($"/api/v1/libros/{libro.Id}", libro);
});

app.MapPut("/api/v1/libros/{id}", (Guid id, LibroDTO libroDTO) =>
{
    var libro = libros.FirstOrDefault(l => l.Id == id);
    if (libro == null)
    {
        return Results.NotFound($"No se encontró un libro con ID {id}.");
    }

    if (string.IsNullOrWhiteSpace(libroDTO.Titulo) || string.IsNullOrWhiteSpace(libroDTO.Resumen))
    {
        return Results.BadRequest("El título y el resumen son obligatorios.");
    }

    libro.Titulo = libroDTO.Titulo;
    libro.Resumen = libroDTO.Resumen;
    libro.AutorId = libroDTO.AutorId;

    return Results.Ok(libro);
});

app.MapDelete("/api/v1/libros/{id}", (Guid id) =>
{
    var libro = libros.FirstOrDefault(l => l.Id == id);
    if (libro == null)
    {
        return Results.NotFound($"No se encontró un libro con ID {id}.");
    }

    libros.Remove(libro);
    return Results.NoContent();
});