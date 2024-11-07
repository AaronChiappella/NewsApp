using NewsAPI.Constants;
using System.ComponentModel.DataAnnotations;
using System;

public class CreateUpdateNewsDto
{
    [Required]
    public string Author { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public string Url { get; set; }

    public string? UrlToImage { get; set; }

    public Languages? Language { get; set; }

    [Required]
    public DateTime PublishedAt { get; set; }

    [Required]
    public string Content { get; set; }

    public string SourceName { get; set; }
}