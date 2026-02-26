using System;
using System.ComponentModel.DataAnnotations;

public class AudioFile
{
    public int Id {get; set;}

    [Required, StringLength(100)]
    public string Label {get; set;} = string.Empty;

    [MaxLength(2000)]
    public string? Description {get; set;}

    [Required, MaxLength(500)]
    public string StoragePath {get; set;} = string.Empty;

    [MaxLength(100)]
    public string? ContentType {get; set;}

    public long SizeBytes {get; set;}

    public DateTimeOffset UploadedAt {get; set;} = DateTimeOffset.UtcNow;

    public ICollection<AudioFileCategory> AudioFileCategories {get; set;} = new List<AudioFileCategory>();


}
