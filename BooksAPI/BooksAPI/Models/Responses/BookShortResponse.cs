﻿namespace BooksAPI.Models.Responses;

public class BookShortResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Author { get; set; }

    public string Genre { get; set; }   
}
